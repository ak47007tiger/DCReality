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
        /// <summary>
        /// 特效路径，没有就不填
        /// </summary>
        public string mEffectPath;

        public TransformType mTransformType;

        public Vector3 mLocalPosOfXX;

        public int mPointCnt = 1;

        public float mDuration;
    }
}