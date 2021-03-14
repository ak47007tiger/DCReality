using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class ScrollWindow_03 : EditorWindow
    {
        [MenuItem("DC/IMGUI/ScrollWindow_03")]
        public static void Open()
        {
            var window = GetWindow<ScrollWindow_03>();
            window.minSize = new Vector2(800, 600);
        }

        Vector2 scrollPosition;


        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(200));
            GUILayout.BeginHorizontal();
            GUILayout.Space(1000);
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }
    }
}