using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class ScrollWindow_02 : EditorWindow
    {
        [MenuItem("DC/IMGUI/ScrollWindow_02")]
        public static void Open()
        {
            var window = GetWindow<ScrollWindow_02>();
            window.minSize = new Vector2(800, 600);
        }

        Vector2 scrollPos;
        string t = "This is a string inside a Scroll view!";

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            scrollPos =
                EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));
            GUILayout.Label(t);
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
                t += " \nAnd this is more text!";
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Clear"))
                t = "";
        }
    }
}