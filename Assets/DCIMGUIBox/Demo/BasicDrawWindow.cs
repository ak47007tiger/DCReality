using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class BasicDrawWindow : EditorWindow
    {
        [MenuItem("DC/IMGUI/BasicDrawWindow")]
        public static void Open()
        {
            var window = GetWindow<BasicDrawWindow>();
            window.minSize = new Vector2(800, 600);
        }



        public Texture2D texture2d;

        public Vector3 startPos = new Vector3();
        public Vector3 endPos = new Vector3(500, 100);
        public Vector3 startTangent = new Vector3(20, 0);
        public Vector3 endTangent;

        public Vector3 Vec3(Vector3 input)
        {
            EditorGUILayout.BeginHorizontal();
            input.x = EditorGUILayout.FloatField(input.x);
            input.y = EditorGUILayout.FloatField(input.y);
            input.z = EditorGUILayout.FloatField(input.z);
            EditorGUILayout.EndHorizontal();
            return input;
        }

        public void OnGUI()
        {
            if (!texture2d) texture2d = new Texture2D(1, 1);

            startPos = Vec3(startPos);
            endPos = Vec3(endPos);
            startTangent = Vec3(startTangent);
            endTangent = Vec3(endTangent);

            Handles.DrawBezier(startPos, endPos, startTangent, endTangent, Color.blue, texture2d, 1);
        }
    }
}