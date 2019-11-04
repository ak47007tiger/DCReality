using System;
using UnityEngine;

namespace DC.SkillSystem
{
    /// <summary>
    ///
    /// </summary>
    public enum VFXPointType
    {
        /// <summary>
        /// 单个位置的技能
        /// </summary>
        single_point,

        /// <summary>
        /// 多个位置的技能
        /// 闪电链，电刀效果
        /// 一个技能有多个子节点，技能对象提供所有的point或者transform
        /// </summary>
        multi_point,
    }

    public enum LocalPosType
    {
        skill,
        caster,
        target,
    }

    public enum TransformType
    {
        world,
        target,
        skill,
        caster,
    }

    [Serializable]
    public class VisualEffectCfg
    {
        public string mEffectPath;

        public LocalPosType mPosType;

        public Vector3 mLocalPosOfXX;

        public VFXPointType mEffectPosType;

        public TransformType mTransformType;
    }
}