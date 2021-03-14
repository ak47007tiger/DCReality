using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class ChildWindowWindow : EditorWindow
    {
        [MenuItem("DC/IMGUI/ChildWindowWindow")]
        public static void Open()
        {
            var window = GetWindow<ChildWindowWindow>();
            window.minSize = new Vector2(800, 600);
        }

        Rect windowRect = new Rect(20, 20, 400, 50);

        Rect windowRect2 = new Rect(20, 120, 120, 50);

        void OnGUI()
        {
            BeginWindows();
            // Register the window. Notice the 3rd parameter
            windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "My Window");


            EndWindows();
        }

        // Make the contents of the window
        void DoMyWindow(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 16));

            // This button will size to fit the window
            if (GUILayout.Button("Hello World"))
            {
                Debug.Log("11");
            }
        }

    }
}