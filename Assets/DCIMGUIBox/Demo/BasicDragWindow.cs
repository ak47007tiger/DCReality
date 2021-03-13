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


        public void OnGUI()
        {
//            var rect = GUILayoutUtility.GetRect(100, 100, 200, 200);
            var rect = new Rect(100, 100, 200, 200);

            //            GUILayout.BeginArea(rect);
            //            
            //            GUILayout.Box("box", GUILayout.Height(200), GUILayout.Width(200));
            //
            //
            //            GUILayout.EndArea();
            EditorGUI.DrawRect(rect, Color.blue);


            var current = Event.current;
            if (current.type == EventType.MouseDown && rect.Contains(current.mousePosition))
            {
                Debug.Log("down" + current.mousePosition);
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
        }

    }
}