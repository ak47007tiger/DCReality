using UnityEngine;
using UnityEditor;

namespace DC.SkillSystem
{
    public class BaseEditor<T> : Editor where T : Component
    {
        public T TargetComponent
        {
            get { return target as T; }
        }

        public GameObject TargetGameObject
        {
            get { return TargetComponent.gameObject; }
        }
    }
}