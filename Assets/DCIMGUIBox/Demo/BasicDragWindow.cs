using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class BasicDragWindow : EditorWindow
    {
        [MenuItem("DC/IMGUI/BasicDragWindow")]
        public static void Open()
        {
            var window = GetWindow<BasicDragWindow>();
            window.minSize = new Vector2(800, 600);


        }

        private bool enableArea = false;

        public void OnGUI()
        {
            var rect = new Rect(100, 100, 200, 200);

            EditorGUI.DrawRect(rect, new Color(1, 0, 0, 0.5f));

            enableArea = EditorGUILayout.ToggleLeft("enable area", enableArea);

            if (enableArea)
            {
                GUILayout.BeginArea(new Rect(40, 40, maxSize.x, 400));
                EditorGUI.DrawRect(new Rect(0, 0, maxSize.x, 400), new Color(0, 0.2f, 0, 0.5f));

            }

            EditorGUI.DrawRect(rect, new Color(0, 0, 1, 0.5f));

            GUILayout.BeginHorizontal("box");

            GUILayout.Box("box", GUILayout.Height(200), GUILayout.Width(200));
            GUILayout.Box("box", GUILayout.Width(50));
            GUILayout.EndHorizontal();
            
            var current = Event.current;
            if (current.type == EventType.MouseDown && rect.Contains(current.mousePosition))
            {
                Debug.Log("down" + current.mousePosition + "," + current.button);
            }

            if (current.type == EventType.MouseMove && rect.Contains(current.mousePosition))
            {
                Debug.Log("move" + current.mousePosition);
            }

            if (current.type == EventType.MouseDrag && rect.Contains(current.mousePosition))
            {
                Debug.Log("drag" + current.mousePosition);
            }

            if (current.type == EventType.MouseUp && rect.Contains(current.mousePosition))
            {
                Debug.Log("up" + current.mousePosition);
            }


            if (enableArea)
            {
                GUILayout.EndArea();

            }
        }

    }
}