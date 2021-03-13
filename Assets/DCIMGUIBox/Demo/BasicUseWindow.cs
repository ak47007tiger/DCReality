using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace DC.DCIMGUIBox
{
    public class BasicUseWindow : EditorWindow
    {
        [MenuItem("DC/IMGUI/BasicUseWindow")]
        public static void Open()
        {
            var window = GetWindow<BasicUseWindow>();
            window.minSize = new Vector2(800, 600);
        }

        private string textFieldString = "text field";

        private string textAreaString = "text area";

        private bool toggleBool = true;

        private int toolbarInt = 0;
        private string[] toolbarStrings = { "Toolbar1", "Toolbar2", "Toolbar3" };

        private int selectionGridInt = 0;
        private string[] selectionStrings = { "Grid 1", "Grid 2", "Grid 3", "Grid 4" };

        public void OnGUI()
        {
            // Make a background box
            GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
            {
                Debug.Log("1");
            }

            // Make the second button.
            if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
            {
                Debug.Log("2");
            }

            GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top-right");
            GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
            GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");

            if (GUI.RepeatButton(new Rect(25, 100, 100, 30), "RepeatButton"))
            {
                // This code is executed every frame that the RepeatButton remains clicked
                Debug.Log("r 1");

            }

            textFieldString = GUI.TextField(new Rect(25, 150, 100, 30), textFieldString);

            textAreaString = GUI.TextArea(new Rect(25, 200, 100, 30), textAreaString);

            toggleBool = GUI.Toggle(new Rect(200, 25, 100, 30), toggleBool, "Toggle");

            toolbarInt = GUI.Toolbar(new Rect(25, 250, 250, 30), toolbarInt, toolbarStrings);

            selectionGridInt = GUI.SelectionGrid(new Rect(25, 300, 300, 60), selectionGridInt, selectionStrings, 2);
        }
    }
}
