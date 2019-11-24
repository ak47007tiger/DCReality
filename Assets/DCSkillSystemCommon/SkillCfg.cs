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

    public enum TranslateType
    {
        off_target,
        close_target,
    }

    public enum HpType
    {
        physics,
        magic,
        real,
    }

    public enum AreaTargetSortType
    {
        Normal,
        Empty,
    }

    public enum AreaHitType
    {
        Normal,
        Nearest,
    }

    [CreateAssetMenu(fileName = "SkillCfg", menuName = "DC/ScriptableObjects/SkillCfg", order = 1)]
    public class SkillCfg : ScriptableObject, ISkillCfg
    {
        public int mId;
        public string mName;
        public string mDesc;
        public float mCdDuration;
        /// <summary>
        /// 存活时间
        /// </summary>
        public float mDuration;

        /// <summary>
        /// 前摇动画时间
        /// </summary>
        public float mPreCastDuration;
        /// <summary>
        /// 后摇动画时间
        /// </summary>
        public float mAfterCastDuration;
        /// <summary>
        /// 释放后不立即cd要等待的时间
        /// </summary>
        public float mCdWaitDuration;
        public string mUiIcon;
        public int mHitCnt = 1;
        public List<KVPair<GValueType, float>> mConsumeList;
        public List<KVPair<GValueType, float>> mValueEffectList;

        public List<KVPair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt = 1000;

        /// <summary>
        /// 施法距离
        /// </summary>
        /// <returns></returns>
        public float mCastRange;

        /// <summary>
        /// 生效区域范围
        /// </summary>
        public float mRayCastRange;

        public string mPrefabPath;

        public SkillType mSkillType;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float mSpeed;

        public bool mTimer = true;

        /// <summary>
        /// 区域技能生效间隔
        /// </summary>
        public float mAffectInterval;

        /// <summary>
        /// 技能施加影响的延迟
        /// </summary>
        public float mEffectDelay;

        /// <summary>
        /// 生效后死亡
        /// </summary>
        public bool mDieAfterDone = true;

        /// <summary>
        /// 可以影响到的关系
        /// </summary>
        public List<SideRelation> mEffectSideRelations;

        public AreaTargetSortType mAreaTargetSortType = AreaTargetSortType.Normal;

        public AreaHitType mAreaHitType = AreaHitType.Normal;

        public List<EventHandlerConfig> mEvtHandlerCfgs;

        public List<KVPair<GValueType, float>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KVPair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }

        public void OnCreate()
        {

        }

        public string GetUiIconPath()
        {
            return "Texture/icon_skill/" + mUiIcon;
        }
    }

}
