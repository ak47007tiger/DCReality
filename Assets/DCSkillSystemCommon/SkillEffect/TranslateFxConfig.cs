using System;
using UnityEngine;

namespace DC.SkillSystem
{
    [Serializable]
    public class TranslateFxConfig
    {
        /// <summary>
        /// 位移那个类型的对象
        /// </summary>
        public TransformType mTransformType;

        public float mDuration;
    }

}