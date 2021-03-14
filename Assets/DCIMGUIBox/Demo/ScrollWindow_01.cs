using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class ScrollWindow_01 : EditorWindow
    {
        [MenuItem("DC/IMGUI/ScrollWindow_01")]
        public static void Open()
        {
            var window = GetWindow<ScrollWindow_01>();
            window.minSize = new Vector2(800, 600);
        }

        public Vector2 scrollPosition;

        public string leftStr = "some content";

        public string leftNewLine = "";

        public void OnGUI()
        {
            //            EditorGUILayout.BeginScrollView();
            //            EditorGUILayout.EndScrollView();
            GUILayout.BeginArea(new Rect(0, 0, 400, 600));
            EditorGUI.DrawRect(new Rect(0, 0, 600, 200), Color.blue);

            leftNewLine = GUILayout.TextField(leftNewLine);
            if (GUILayout.Button("add"))
            {
                leftStr += ("\n" + leftNewLine);
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(200));

            leftStr = EditorGUILayout.TextArea(leftStr);
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}