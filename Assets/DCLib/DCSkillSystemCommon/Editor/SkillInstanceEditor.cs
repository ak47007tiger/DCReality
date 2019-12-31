using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DC.SkillSystem
{
    [CustomEditor(typeof(Skill))]
    public class SkillInstanceEditor : BaseEditor<Skill>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("scale collider size"))
            {
                ScaleColliderSize();
            }
        }

        void ScaleColliderSize()
        {
            TargetGameObject.GetComponent<BoxCollider>().size = Toolkit.Mul(Vector3.one, TargetComponent.transform.localScale);
        }
    }

}
