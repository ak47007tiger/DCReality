using System;
using UnityEngine;

namespace DC.SkillSystem
{
    /// <summary>
    /// 移动施法者
    ///     往技能位置移动
    ///     远离技能位置移动
    /// 移动受击者
    ///     往技能方向移动
    ///     远离技能方向移动
    /// </summary>
    [Serializable]
    public class TranslateFxConfig
    {
        /// <summary>
        /// 位移哪个类型的对象
        /// </summary>
        public TransformType mTransformType;

        public float mSpeed;

        public bool mImmediately;
    }

}