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

    public enum SkillType
    {
        normal,
        bullet,
        area,
    }

    [CreateAssetMenu(fileName = "SkillCfg", menuName = "DC/ScriptableObjects/SkillCfg", order = 1)]
    public class SkillCfg : ScriptableObject, ISkillCfg
    {
        public int mId;
        public float mDuration;
        public int mHitCnt = 1;
        public List<KVPair<GValueType, float>> mConsumeList;
        public List<KVPair<GValueType, float>> mValueEffectList;

        public SkillAnimationCfg mSkillAnimationCfg;

        public List<KVPair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt;

        /// <summary>
        /// 施法距离
        /// </summary>
        /// <returns></returns>
        public float mCastRange;

        public string mPrefabPath;

        public SkillType mSkillType;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float mSpeed;

        /// <summary>
        /// 区域技能生效间隔
        /// </summary>
        public float mAffectInterval;

        public List<KVPair<GValueType, float>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KVPair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }
    }
}