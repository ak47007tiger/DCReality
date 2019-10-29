using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.AI;
using DC.DCPhysics;
using DC.GameLogic;

namespace DC.SkillSystem
{
    /*
        区域型技能
        移动型技能

        放一个技能打多个目标 这个技能不能是指向性的
     */

    public class Skill : BaseMonoBehaviour, ISkill
    {
        private ICaster mCaster;

        private SkillCfg mSkillCfg;

        private CastCfg mCastCfg;

        private CacheItem<BoxCollider> mBoxCollider;

        private float mLife;

        private int mHitCnt;

        private DCBaseTimer mTimerForApply;

        private int mTryApplyToTargetCnt = 1;

        protected void Awake()
        {
            mBoxCollider = new CacheItem<BoxCollider>(GetComponent<BoxCollider>);
        }

        public ICaster GetCaster()
        {
            return mCaster;
        }

        public void SetCaster(ICaster caster)
        {
            mCaster = caster;
        }

        public CastCfg GetCastCfg()
        {
            return mCastCfg;
        }

        public void SetCastCfg(CastCfg castCfg)
        {
            mCastCfg = castCfg;
        }

        public SkillCfg GetSkillCfg()
        {
            return mSkillCfg;
        }

        public void SetSkillCfg(SkillCfg skillCfg)
        {
            mSkillCfg = skillCfg;
        }

        public bool AllowCastTo(IActor actor)
        {
            return true;
        }

        public void OnCatchTarget(IActor target)
        {

        }

        public List<IActor> TryCollectTargets()
        {
            return null;
        }

        public void OnSkillLifeRecycle(SkillLifeCycle lifeCycle)
        {

        }

        public void Apply()
        {
            LogDC.LogEx("apply skill id :", GetSkillCfg().mId);
            if (mSkillCfg.mSkillType == SkillType.bullet)
            {
                switch (mSkillCfg.mTargetType)
                {
                    case SkillTargetType.Actor:
                    {
                        var transformTraceTarget = gameObject.AddComponent<TransformTraceTarget>();
                        transformTraceTarget.StartTrace(mCastCfg.mTargets[0].GetTransform(), 0.5f, mSkillCfg.mSpeed);
                        break;
                    }
                    case SkillTargetType.Position:
                    {
                        var arriveCmp = gameObject.AddComponent<ArrivePosition>();
                        arriveCmp.StartTrace(mCastCfg.mTargetPosition, 0.5f, mSkillCfg.mSpeed);
                        break;
                    }
                    case SkillTargetType.Direction:
                    {
                        var moveDir = gameObject.AddComponent<MoveToDirection>();
                        moveDir.StartMove(mCastCfg.mDirection, mSkillCfg.mDuration, mSkillCfg.mSpeed);
                        break;
                    }
                }
            }

            mTimerForApply = new DCDurationTimer(mSkillCfg.mAffectInterval, AddApplyCnt, -1).Create();
        }

        private void OnTraceTransformEnd(TransformTraceTarget cmp, float distance)
        {

        }

        private void OnArrivePosEnd(ArrivePosition cmp)
        {

        }

        private void OnMoveDirEnd(MoveToDirection cmp)
        {

        }

        public Transform GetTransform()
        {
            return CacheTransform;
        }

        void OnDestroy()
        {
            if (null != mTimerForApply) mTimerForApply.Destroy();
        }

        void Update()
        {
            if (mLife > mSkillCfg.mDuration)
            {
                SkillSys.Instance.DestroySkill(this);
                return;
            }

            mLife += Time.deltaTime;
        }

        void AddApplyCnt()
        {
            mTryApplyToTargetCnt++;
        }

        private void TryApplyToTarget()
        {
            var halfExtents = mBoxCollider.Value.size * 0.5f;
            var center = CacheTransform.position;
            var allHit = Physics.BoxCastAll(center, halfExtents, CacheTransform.forward, CacheTransform.rotation,
                halfExtents.x * 2);
            var bound = new Bounds(CacheTransform.position, mBoxCollider.Value.size);
            DebugExtension.DebugBounds(bound, Color.green);

            if (!Toolkit.IsNullOrEmpty(allHit))
            {
                foreach (var raycastHit in allHit)
                {
                    var hitActor = raycastHit.transform.GetComponent<IActor>();
                    if (hitActor != null)
                    {
                        var targetActors = mCastCfg.GetTargetActors();
                        if (!Toolkit.IsNullOrEmpty(targetActors))
                        {
                            if (mCastCfg.GetTargetActors().Contains(hitActor))
                            {
                                if (hitActor.GetActorSide() != mCaster.GetActor().GetActorSide())
                                {
                                    LogDC.LogEx("skill get actor", hitActor.GetTransform().gameObject.name);
                                    mHitCnt++;
                                }
                            }
                        }
                        else
                        {
                            if (hitActor.GetActorSide() != mCaster.GetActor().GetActorSide())
                            {
                                LogDC.LogEx("skill get actor", hitActor.GetTransform().gameObject.name);
                                mHitCnt++;
                            }
                        }
                    }
                }
            }
        }

        void FixedUpdate()
        {
            switch (mSkillCfg.mSkillType)
            {
                case SkillType.area:
                {
                    if (mTryApplyToTargetCnt > 0)
                    {
                        TryApplyToTarget();
                        mTryApplyToTargetCnt--;
                    }

                    break;
                }
                default:
                {
                    if (mHitCnt < mSkillCfg.mHitCnt)
                    {
                        TryApplyToTarget();
                    }

                    if (mHitCnt >= mSkillCfg.mHitCnt)
                    {
                        SkillSys.Instance.DestroySkill(this);
                    }

                    break;
                }
            }
        }
    }
}