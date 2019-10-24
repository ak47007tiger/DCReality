using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
using DC.DCPhysics;

namespace DC.SkillSystem
{

    public interface ISkillCfg
    {
    }

    public enum SkillTargetType
    {
        None,
        Actor,
        Direction,
        Position,
    }

    public class SkillCfg : ISkillCfg
    {
        public int mId;
        public int mDuration;
        public List<KeyValuePair<ValueType, int>> mConsumeList;
        public List<KeyValuePair<ValueType, int>> mValueEffectList;

        public SkillAnimationCfg mSkillAnimationCfg;

        public List<KeyValuePair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt;
        public bool mNeedTarget;
        public bool mNeedDirection;
        public bool mNeedPosition;

        /// <summary>
        /// 施法距离
        /// </summary>
        /// <returns></returns>
        public int mCastRange;

        public string mPrefabPath;

        public List<KeyValuePair<ValueType, int>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KeyValuePair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }
    }
}