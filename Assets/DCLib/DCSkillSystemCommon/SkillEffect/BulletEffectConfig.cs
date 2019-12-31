using System;

namespace DC.SkillSystem
{
    public enum BulletType
    {
        target,
        position,
    }

    [Serializable]
    public class BulletEffectConfig
    {
        public BulletType mType;
        public string mPrefabPath;
    }
}