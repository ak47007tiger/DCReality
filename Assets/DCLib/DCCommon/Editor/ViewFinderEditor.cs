using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace DC
{
    public class ViewFinderEditor : Editor
    {
        private static readonly ViewScriptGenerator ScriptGenerator = new ViewScriptGenerator();

        [MenuItem("Assets/DC/ViewFinder/GenerateScriptToClipboard", priority = 1)]
        public static void GenerateScriptToClipboard()
        {
            var root = Selection.activeGameObject;
            var rootTf = root.transform;

            var code = ScriptGenerator.GenerateCodeScript(rootTf);
            var textEditor = new TextEditor();
            textEditor.text = code;
            textEditor.SelectAll();
            textEditor.Copy();
        }

        [MenuItem("Assets/DC/ViewFinder/GenerateScriptFile", priority = 1)]
        public static void GenerateScriptFile()
        {
            var root = Selection.activeGameObject;
            var rootTf = root.transform;

            ScriptGenerator.GenerateScriptFile(rootTf);
        }
    }

    public class ViewScriptGenerator
    {
        public static readonly string prefix_id = "id_";
        public static readonly string prefix_go = "go_";
        public static readonly string prefix_tf = "tf_";
        public static readonly string prefix_cm = "cm_";
        //ignore
        public static readonly string prefix_it = "it_";

        public static readonly List<Type> support_compoent = new List<Type>
        {
            typeof(ScrollRect),
            typeof(CanvasGroup),

            typeof(Text),
            typeof(Button),
            typeof(Image),
            typeof(RawImage),
            typeof(InputField),
            typeof(Toggle),

        };

        public static readonly string class_template = @"
using UnityEngine;
using UnityEngine.UI;
namespace DC.UI
{
    public class #className : MonoBehaviour
    {
#fieldStr
        void Awake()
        {
#findStr
        }
    }
}
";
        public static readonly string code_template = @"
#fieldStr
        void Awake()
        {
#findStr
        }
";

        public string GenerateCodeScript(Transform rootTf)
        {
            var dictionary = new Dictionary<string, Transform>();
            SearchAllTarget(ref dictionary, rootTf);

            var fieldStrBuffer = new StringBuilder();
            var findStrBuffer = new StringBuilder();

            var gameObjectType = typeof(GameObject);
            var rectTfType = typeof(RectTransform);

            foreach (var kv in dictionary)
            {
                var name = kv.Key;
                var tf = kv.Value;

                var path = GetPathOfRoot(rootTf, tf);

                var fieldName = name.Substring(name.IndexOf('_') + 1);

                if (name.StartsWith(prefix_cm))
                {
                    foreach (var supportType in support_compoent)
                    {
                        var cmpt = tf.GetComponent(supportType);

                        if (null != cmpt)
                        {
                            FillComponentContent(supportType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                        }
                    }
                }
                else if (name.StartsWith(prefix_id))
                {
                    FillGameObjectContent(gameObjectType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                    FillComponentContent(rectTfType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_go))
                {
                    FillGameObjectContent(gameObjectType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_tf))
                {
                    FillComponentContent(rectTfType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_it))
                {
                    FillGameObjectContent("GameObject", path, fieldName, fieldStrBuffer, findStrBuffer);
                }

                fieldStrBuffer.Append("\r\n");
                findStrBuffer.Append("\r\n");
            }

            var classScript = new StringBuilder(code_template)
                    .Replace("#fieldStr", fieldStrBuffer.ToString())
                    .Replace("#findStr", findStrBuffer.ToString());

            return classScript.ToString();
        }

        public string GenerateClsScript(Transform rootTf)
        {
            /*
             
            field 
                type name
            find 
                path type

             */

            var dictionary = new Dictionary<string, Transform>();
            SearchAllTarget(ref dictionary, rootTf);

            var className = GetClassName(rootTf);

            var fieldStrBuffer = new StringBuilder();
            var findStrBuffer = new StringBuilder();

            var gameObjectType = typeof(GameObject);
            var rectTfType = typeof(RectTransform);

            foreach (var kv in dictionary)
            {
                var name = kv.Key;
                var tf = kv.Value;

                var path = GetPathOfRoot(rootTf, tf);

                var fieldName = name.Substring(name.IndexOf('_') + 1);

                if (name.StartsWith(prefix_cm))
                {
                    foreach (var supportType in support_compoent)
                    {
                        var cmpt = tf.GetComponent(supportType);

                        if (null != cmpt)
                        {
                            FillComponentContent(supportType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                        }
                    }
                }
                else if (name.StartsWith(prefix_id))
                {
                    FillGameObjectContent(gameObjectType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                    FillComponentContent(rectTfType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_go))
                {
                    FillGameObjectContent(gameObjectType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_tf))
                {
                    FillComponentContent(rectTfType.Name, path, fieldName, fieldStrBuffer, findStrBuffer);
                }
                else if (name.StartsWith(prefix_it))
                {
                    FillGameObjectContent("GameObject", path, fieldName, fieldStrBuffer, findStrBuffer);
                }

                fieldStrBuffer.Append("\r\n");
                findStrBuffer.Append("\r\n");
            }

            var classScript = new StringBuilder(class_template)
                    .Replace("#className", className)
                    .Replace("#fieldStr", fieldStrBuffer.ToString())
                    .Replace("#findStr", findStrBuffer.ToString());

            return classScript.ToString();
        }

        public void GenerateScriptFile(Transform rootTf)
        {
            var className = GetClassName(rootTf);

            var generateScript = GenerateClsScript(rootTf);

            //save to file
            var setting = ViewFinderSetting.GetSetting();

            var dir = Path.Combine(Application.dataPath, setting.mScriptSavePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = dir + "/" + className + ".cs";
            Debug.Log("save script: " + filePath);
            File.WriteAllText(filePath, generateScript, Encoding.UTF8);

            //refresh asset
            var assetPath = "Assets/" + setting.mScriptSavePath + "/" + className + ".cs";
            Debug.Log("import: " + assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.Default);
        }

        public string GetClassName(Transform rootTf)
        {
            var className = rootTf.name + "Gen";
            if (className.StartsWith(prefix_it))
            {
                className = className.Substring(prefix_it.Length);
            }

            return className;
        }

        public void FillComponentContent(string typeName, string path, string fieldName, StringBuilder fieldStrBuffer, StringBuilder findStrBuffer)
        {
            var fieldStr = string.Format("public {0} {1}{2};", typeName, fieldName, typeName);
            fieldStrBuffer.Append('\t');
            fieldStrBuffer.Append('\t');
            fieldStrBuffer.Append(fieldStr);
            fieldStrBuffer.Append("\r\n");

            var fieldNameType = fieldName + typeName;
            var findStr = string.Format(
                "{0} = transform.Find(\"{1}\").GetComponent<{2}>();",
                fieldNameType, path, typeName);

            findStrBuffer.Append('\t');
            findStrBuffer.Append('\t');
            findStrBuffer.Append('\t');
            findStrBuffer.Append(findStr);
            findStrBuffer.Append("\r\n");
        }

        public void FillGameObjectContent(string typeName, string path, string fieldName, StringBuilder fieldStrBuffer, StringBuilder findStrBuffer)
        {
            var fieldStr = string.Format("public {0} {1}{2};", typeName, fieldName, typeName);
            fieldStrBuffer.Append('\t');
            fieldStrBuffer.Append('\t');
            fieldStrBuffer.Append(fieldStr);
            fieldStrBuffer.Append("\r\n");

            var fieldNameType = fieldName + typeName;
            var findStr = string.Format(
                "{0} = transform.Find(\"{1}\").gameObject;",
                fieldNameType, path);

            findStrBuffer.Append('\t');
            findStrBuffer.Append('\t');
            findStrBuffer.Append('\t');
            findStrBuffer.Append(findStr);
            findStrBuffer.Append("\r\n");
        }

        public static string GetPathOfRoot(Transform root, Transform leaf)
        {
            var childName = leaf.gameObject.name;

            if (null == root || leaf.parent == null || leaf.parent == root)
            {
                return childName;
            }

            return GetPathOfRoot(root, leaf.parent) + "/" + childName;
        }

        public static void SearchAllTarget(ref Dictionary<string, Transform> dic, Transform root)
        {
            for (var i = 0; i < root.childCount; i++)
            {
                var childTf = root.GetChild(i);
                var objectName = childTf.gameObject.name;
                if (objectName.StartsWith(prefix_it))
                {
                    dic.Add(objectName, childTf);
                    continue;
                }
                if (objectName.StartsWith(prefix_id) || objectName.StartsWith(prefix_go) || objectName.StartsWith(prefix_tf)
                    || objectName.StartsWith(prefix_cm))
                {
                    dic.Add(objectName, childTf);
                }
                SearchAllTarget(ref dic, childTf);
            }
        }
    }

    public class DemoViewPart : MonoBehaviour
    {
        public Text mNameText;
        public Image mHeaderImage;
        public Button mConfirmButton;

        public void Awake()
        {
            mNameText = transform.Find("").GetComponent<Text>();
            mHeaderImage = transform.Find("").GetComponent<Image>();
            mConfirmButton = transform.Find("").GetComponent<Button>();
        }

    }

    public class BaseView
    {
        protected Transform transform;

        public virtual void Init(Transform rootTf)
        {
            transform = rootTf;
        }
    }

    public class XXView : BaseView
    {
        public Text mNameText;
        public Image mHeaderImage;
        public Button mConfirmButton;

        public override void Init(Transform rootTf)
        {
            base.Init(rootTf);

            mNameText = transform.Find("").GetComponent<Text>();
            mHeaderImage = transform.Find("").GetComponent<Image>();
            mConfirmButton = transform.Find("").GetComponent<Button>();
        }

    }

    public class BaseController<View>
    {
        protected View mView;

        public void SetView(View view)
        {
            mView = view;
        }
    }

    public class XXController
    {
        private XXView mViewPart;


    }

    public class Manager
    {
        public XXController Show<XXController, XXView>() where XXView : BaseView
        {
            var uiName = typeof(XXView).Name;
            //load resource with name
            var uiGameObject = new GameObject();

            var viewInstance = Activator.CreateInstance<XXView>();
            viewInstance.Init(uiGameObject.transform);

            var controllerInstance = Activator.CreateInstance<XXController>();

            return controllerInstance;
        }
    }
}
