using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

/*
    生成excel表里面的内容 id,paramCnt,content,comment
    startId-Enum
    prefix,comment
    content
        收集一个文件里所有字符串
        加工收集到的字符串
    替换文件里面的字符串
        EnumName
 */


namespace DodGame.DEditor
{
    public class CodeTextDefineWindow : EditorWindow
    {
        private CodeTextProcessor m_processor = new CodeTextProcessor();
        private IOHelper m_ioHelper = new IOHelper();

        /// <summary>
        /// generate filed name prefix
        /// </summary>
        public string m_prefix;

        public string m_comment;

        public string m_targetFilePath;

        /// <summary>
        /// search file name
        /// </summary>
        public string m_keyword;

        public string m_searchRoot;

        private List<FileSystemInfo> m_searchedFiles = new List<FileSystemInfo>();

        private CodeTextInfo m_curCodeTextInfo;

        [MenuItem("Window/CodeTextDefineWindow")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            var window = (CodeTextDefineWindow)EditorWindow.GetWindow(typeof(CodeTextDefineWindow));
            window.minSize = new Vector2(800, 500);
            window.Show();
        }

        void Awake()
        {
            m_searchRoot = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
            m_searchRoot += ('/' + "GameProject/GameLogic/src");
        }

        void OnGUI()
        {
            m_searchRoot = EditorGUILayout.TextField("searchRoot", m_searchRoot);

            m_keyword = EditorGUILayout.TextField("keyward", m_keyword);

            m_prefix = EditorGUILayout.TextField("prefix", m_prefix);

            m_comment = EditorGUILayout.TextField("m_comment", m_comment);

            m_targetFilePath = EditorGUILayout.TextField("targetFilePath", m_targetFilePath);

            if (GUILayout.Button("set root"))
            {
                OnSetRootClick();
            }

            if (GUILayout.Button("search"))
            {
                OnSearchClick();
            }

            if (GUILayout.Button("specific"))
            {
                OnSpecificClick();
            }

            if (GUILayout.Button("generate"))
            {
                OnGenerateClick();
            }

            GUI_FileList();

            GUI_TxtList();
        }

        private void GUI_TxtList()
        {
            if (null != m_curCodeTextInfo && m_curCodeTextInfo.m_originTxtList.Count > 0)
            {
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginScrollView(Vector2.zero, false, true, GUIStyle.none, GUIStyle.none, GUIStyle.none,
                    new GUILayoutOption[]
                    {
                        GUILayout.Height(400)
                    });

                foreach (var content in m_curCodeTextInfo.m_originTxtList)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(content, GUIStyle.none, new GUILayoutOption[]
                    {
                        GUILayout.MaxWidth(400)
                    });
                    if (GUILayout.Toggle(false, "ignore"))
                    {
                        if (!m_curCodeTextInfo.m_ignoreSet.Contains(content))
                        {
                            m_curCodeTextInfo.m_ignoreSet.Add(content);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.LabelField("no text");
            }
        }

        private void GUI_FileList()
        {
            if (m_searchedFiles != null && m_searchedFiles.Count > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginScrollView(
                    Vector2.zero, false, true, 
                    GUIStyle.none, GUI.skin.verticalScrollbar, GUI.skin.scrollView,
                    new GUILayoutOption[]
                    {
                        GUILayout.Height(200)
                    });

                foreach (var file in m_searchedFiles)
                {
                    if (GUILayout.Button(file.Name))
                    {
                        SelectTargetFile(file.FullName);
                    }
                }

                EditorGUILayout.EndScrollView();

                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.LabelField("no file");
            }
        }

        void OnSetRootClick()
        {
            /*var dirRoot = new DirectoryInfo(searchRoot);
                m_files.Clear();
                CollectFiles(dirRoot, m_files);*/
        }

        void OnSpecificClick()
        {
            SelectTargetFile(m_targetFilePath);
        }

        void OnSearchClick()
        {
            m_searchedFiles = IOHelper.GetAllFileInRoot(new DirectoryInfo(m_searchRoot), m_keyword);
        }

        void OnGenerateClick()
        {

        }

        public void SelectTargetFile(string filePath)
        {
            m_targetFilePath = filePath;

            m_curCodeTextInfo = new CodeTextInfo();
            m_curCodeTextInfo.m_file = new FileInfo(filePath);
            var srcCode = File.ReadAllText(filePath);
            m_curCodeTextInfo.m_srcCode = srcCode;
            m_curCodeTextInfo.m_originTxtList = m_processor.GetTextList(srcCode);
        }

    }

    public class IOHelper
    {
        public static List<FileSystemInfo> GetAllFileInRoot(DirectoryInfo dir, string searchKeyWord)
        {
            var searchPattern = "*" + searchKeyWord + "*";

            var allFileInRoot = dir.GetFiles(searchPattern, SearchOption.AllDirectories);
            return new List<FileSystemInfo>(allFileInRoot);
            /*var list = new List<FileSystemInfo>();
            CollectFiles(dir, searchPattern, list);
            return list;*/
            /*var fileSystemInfos = dir.GetFileSystemInfos(searchPattern);
            var list = new List<FileSystemInfo>(fileSystemInfos);
            var allFileInRoot = list.FindAll((item) => (item.Attributes & FileAttributes.Directory) == 0);
            return allFileInRoot;*/
        }

        public static void CollectFiles(DirectoryInfo dirInfo, string pattern, List<FileSystemInfo> collect)
        {
            var allFiles = dirInfo.GetFiles(pattern, SearchOption.AllDirectories);
            collect.AddRange(allFiles);

            var allDirs = dirInfo.GetDirectories(pattern);
            foreach (var item in allDirs)
            {
                CollectFiles(item, pattern, collect);
            }

        }

        public static void CollectFiles(DirectoryInfo dirInfo, List<FileInfo> collect)
        {
            collect.AddRange(dirInfo.GetFiles());

            var dirInfos = dirInfo.GetDirectories();
            foreach (var directoryInfo in dirInfos)
            {
                CollectFiles(directoryInfo, collect);
            }

        }
    }

    public class CodeTextInfo
    {
        public FileInfo m_file;
        public string m_srcCode;
        public List<string> m_originTxtList;
        public HashSet<string> m_ignoreSet = new HashSet<string>();
    }

    public class CodeTextProcessor
    {

        public CodeTextProcessor()
        {
        }

        public string GeneNewSourceCode(CodeTextInfo codeTextInfo, int startId, string prefix)
        {
            //todo d.c
            return null;
        }

        public string GeneTextDefineContent(CodeTextInfo codeTextInfo, int startId, string prefix,
            FileInfo textDefineClsFile)
        {
            //todo d.c
            return null;
        }

        public string GeneExcelContent(CodeTextInfo codeTextInfo, int startId, string comment)
        {
            //todo d.c
            return null;
        }

        enum CType2
        {
            outC,
            left,
            middle,
            right,
        }

        /// <summary>
        /// @param content
        /// </summary>
        public List<string> GetTextList(string content)
        {
            var textList = new List<string>();
            var state = CType.outC;
            var leftStartIndex = 0;
            foreach (var c in content)
            {
                switch (state)
                {
                    case CType.outC:
                        switch (c)
                        {
                            case '\"':
                                state = CType.left;
                                break;
                            default:
                                break;
                        }
                        break;
                    case CType.left:
                        switch (c)
                        {
                            //有空字符串
                            case '\"':
                                state = CType.right;
                                break;
                            default:
                                state = CType.middle;
                                break;
                        }
                        break;
                    case CType.middle:
                        switch (c)
                        {
                            //找到一个字符串
                            case '\"':
                                state = CType.right;
                                break;
                            default:
                                break;
                        }
                        break;
                    case CType.right:
                        switch (c)
                        {
                            case '\"':
                                state = CType.left;
                                break;
                            default:
                                state = CType.outC;
                                break;
                        }
                        break;
                }
            }

            return textList;
        }

        /// <summary>
        /// @param prefix
        /// </summary>
        public List<string> GenerateTextDefine(string prefix, List<string> txt)
        {
            var textList = new List<string>();
            return textList;
        }

        /// <summary>
        /// 获得一个枚举的最后一个枚举int值
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static int GetLastIntValue(string typeInfo)
        {
            var type = Type.GetType(typeInfo);
            var values = Enum.GetValues(type);
            var value = values.GetValue(values.Length - 1);
            var intValue = (int)value;
            return intValue;
        }

        enum CType
        {
            //大括号外
            outC = 0,
            //{
            left,
            //大括号中间
            middle,
            //}
            right,
        }

        public static int ComputeParamCnt(string content)
        {
            var cnt = 0;
            var state = CType.outC;
            for (var i = 0; i < content.Length; i++)
            {
                var c = content[i];
                switch (state)
                {
                    case CType.outC:
                        switch (c)
                        {
                            case '{':
                                state = CType.left;
                                break;
                            case '}':
                                state = CType.right;
                                break;
                        }
                        break;
                    case CType.left:
                        switch (c)
                        {
                            case '{':
                                break;
                            case '}':
                                //{}这样的当直接展示的内容
                                state = CType.outC;
                                break;
                            default:
                                state = CType.middle;
                                break;
                        }
                        break;
                    case CType.middle:
                        switch (c)
                        {
                            case '{':
                                break;
                            case '}':
                                cnt++;
                                state = CType.right;
                                break;
                            default:
                                //这里如果出现非数字应报错
                                break;
                        }
                        break;
                    case CType.right:
                        switch (c)
                        {
                            case '{':
                                state = CType.left;
                                break;
                            case '}':
                                state = CType.outC;
                                break;
                            default:
                                state = CType.outC;
                                break;
                        }
                        break;
                }

            }
            return cnt;
        }

    }

}

