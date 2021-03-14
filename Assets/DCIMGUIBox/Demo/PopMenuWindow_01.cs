using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class PopMenuWindow_01 : EditorWindow
    {
        [MenuItem("DC/IMGUI/PopMenuWindow_01")]
        public static void Open()
        {
            var window = GetWindow<PopMenuWindow_01>();
            window.minSize = new Vector2(800, 600);
        }

        int index = 0;
        string[] options = { "Rigidbody", "Box Collider", "Sphere Collider" };

        void OnGUI()
        {
            Event evt = Event.current;
            Rect contextRect = new Rect(10, 10, 100, 100);
            EditorGUI.DrawRect(contextRect, Color.blue);
            if (evt.type == EventType.ContextClick)
            {
                Vector2 mousePos = evt.mousePosition;
                if (contextRect.Contains(mousePos))
                {
                    EditorUtility.DisplayPopupMenu(new Rect(mousePos.x, mousePos.y, 0, 0), "Assets/DC", null);
                }
            }

            var areaRect = new Rect(0, 200, 200, 40);
            EditorGUI.DrawRect(areaRect, Color.blue);
            index = EditorGUI.Popup(areaRect, "Component:", index, options);

        }
    }
}