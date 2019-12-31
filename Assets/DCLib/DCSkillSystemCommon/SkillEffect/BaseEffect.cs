using DC.DCResourceSystem;
using UnityEngine;

namespace DC.SkillSystem
{
    public class BaseEffect
    {
        public EffectType mType;

        public static void PlayVisualEffect(Transform actorTf, string prefabPath, Vector3 localPos)
        {
            var prefab = ResourceSys.Instance.Load<GameObject>(prefabPath);
            var instance = Object.Instantiate(prefab, actorTf);
            var worldPos = actorTf.localToWorldMatrix.MultiplyPoint(localPos);
            instance.transform.position = worldPos;
        }

        public static void CastSkill(Skill parentSkill, int childSkillId)
        {

        }
    }
}