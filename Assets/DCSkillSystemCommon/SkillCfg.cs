using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.Collections.Generic;
using DC.DCPhysics;
using DC.ValueSys;

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

    [CreateAssetMenu(fileName = "SkillCfg", menuName = "DC/ScriptableObjects/SkillCfg", order = 1)]
    public class SkillCfg : ScriptableObject, ISkillCfg
    {
        public int mId;
        public int mDuration;
        public List<KVPair<GValueType, int>> mConsumeList;
        public List<KVPair<GValueType, int>> mValueEffectList;

        public SkillAnimationCfg mSkillAnimationCfg;

        public List<KVPair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt;

        /// <summary>
        /// 施法距离
        /// </summary>
        /// <returns></returns>
        public int mCastRange;

        public string mPrefabPath;

        public List<KVPair<GValueType, int>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KVPair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }
    }
}