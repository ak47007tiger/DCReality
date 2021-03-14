using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{

    public class LayoutAndAreaWindow_01 : EditorWindow
    {
        [MenuItem("DC/IMGUI/LayoutAndAreaWindow_01")]
        public static void Open()
        {
            var window = GetWindow<LayoutAndAreaWindow_01>();
            window.minSize = new Vector2(800, 600);


        }

        public void OnGUI()
        {
            
            GUILayout.BeginArea(new Rect(0,0, 200, 200));
            EditorGUI.DrawRect(new Rect(0, 0, 100, 100), Color.blue);
            if (Event.current.type == EventType.MouseDown)
            {
                Debug.Log("box a" + Event.current.mousePosition);
            }

            GUILayout.EndArea();


            GUILayout.BeginArea(new Rect(210, 0, 200, 200));
            EditorGUI.DrawRect(new Rect(0, 0, 100, 100), Color.red);
            if (Event.current.type == EventType.MouseDown)
            {
                Debug.Log("box b" + Event.current.mousePosition);
            }

            GUILayout.EndArea();

        }

    }

}
