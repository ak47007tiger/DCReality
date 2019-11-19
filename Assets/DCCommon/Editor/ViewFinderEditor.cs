using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace DC
{
    public class ViewFinderEditor : Editor
    {
        private static readonly int subStartIndex = "id_".Length;

        public static readonly List<Type> support_types = new List<Type>
        {
            typeof(GameObject),
            typeof(RectTransform),

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
namespace DC
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

        public static ViewFinderSetting GetSetting()
        {
            return EditorGUIUtility.Load("ViewFinderSetting") as ViewFinderSetting;
        }

        public static void GenerateViewResource()
        {
            /*
             
            field 
                type name
            find 
                path type

             */
            var root = Selection.activeGameObject;
            var rootTf = root.transform;
            var dictionary = new Dictionary<string, Transform>();
            SearchAllTarget(dictionary, rootTf);

            var className = root.name;

            var fieldStrBuffer = new StringBuilder();
            var findStrBuffer = new StringBuilder();

            foreach (var kv in dictionary)
            {
                var name = kv.Key;
                var tf = kv.Value;

                var fieldName = name.Substring(subStartIndex);
                foreach (var supportType in support_types)
                {
                    var cmpt = tf.GetComponent(supportType);
                    
                    if (null != cmpt)
                    {
                        var typeName = supportType.Name;
                        var fieldStr = string.Format("public {0} {1}{2};", typeName, fieldName, typeName);
                        fieldStrBuffer.AppendLine(fieldStr);

                        var fieldNameType = fieldName + typeName;
                        var path = "";
                        var findStr = string.Format(
                            "{0} = transform.Find({1}).GetComponent<{2}>()", 
                            fieldNameType, path, typeName);
                        findStrBuffer.AppendLine(findStr);
                    }
                }
            }

            var classScript = new StringBuilder(class_template);
            classScript
                .Replace("#className", className)
                .Replace("#fieldStr", fieldStrBuffer.ToString())
                .Replace("#findStr", findStrBuffer.ToString())
                ;
            //save to file
            //refresh asset
        }

        public static void SearchAllTarget(Dictionary<string, Transform> dic, Transform root)
        {
            
        }
    }

    public class DemoViewPart : MonoBehaviour
    {
        public Text mNameText;
        public Image mHeaderImage;
        public Button mConfirmButton;

        void Awake()
        {
            mNameText = transform.Find("").GetComponent<Text>();
            mHeaderImage = transform.Find("").GetComponent<Image>();
            mConfirmButton = transform.Find("").GetComponent<Button>();
        }

    }

}
