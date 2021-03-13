using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class BasicAutoLayout : EditorWindow
    {
        [MenuItem("DC/IMGUI/BasicAutoLayout")]
        public static void Open()
        {
            var window = GetWindow<BasicAutoLayout>();
            window.minSize = new Vector2(800, 600);
        }

        public AnimationCurve aniCurve;

        public GUIContent aniCurveLabel;

        void Awake()
        {
            aniCurveLabel = new GUIContent("ani");
            aniCurve = new AnimationCurve();
        }

        public void OnGUI()
        {
            GUILayout.BeginVertical();
            aniCurve = EditorGUILayout.CurveField(aniCurveLabel, aniCurve, Color.green, new Rect(0,0, 200, 100), GUILayout.Height(100), GUILayout.Width(200));
            GUILayout.BeginHorizontal();

        }
    }
}