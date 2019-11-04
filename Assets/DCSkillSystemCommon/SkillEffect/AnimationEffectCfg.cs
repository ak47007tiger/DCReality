using System;

namespace DC.SkillSystem
{


    public enum ValueType
    {
        int_val,
        float_val,
        bool_val,
        trigger_val,
    }

    [Serializable]
    public class AnimationEffectCfg
    {
        public int mAnimatorId;
        public int mAnimatorParamId;
        public ValueType mValueType;
        public string mValue;
    }
}