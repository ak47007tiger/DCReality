using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.GameLogic;

namespace DC.SkillSystem
{
    public class Skill : BaseMonoBehaviour, ISkill
    {
        private ICaster mCaster;

        private SkillCfg mSkillCfg;

        private CastCfg mCastCfg;

        private CacheItem<BoxCollider> mBoxCollider;

        private float mLife;

        private int mHitCnt;

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
            
        }

        public Transform GetTransform()
        {
            return CacheTransform;
        }

        void Update()
        {
            if (mLife > mSkillCfg.mDuration)
            {
                SkillSys.Instance.DestroySkill(this);
                return;
            }

            if (mHitCnt < mSkillCfg.mHitCnt)
            {
                var allHit = Physics.BoxCastAll(CacheTransform.position, mBoxCollider.Value.size * 0.5f, CacheTransform.forward,
                    CacheTransform.rotation);

                if (!Toolkit.IsNullOrEmpty(allHit))
                {
                    foreach (var raycastHit in allHit)
                    {
                        var hitActor = raycastHit.transform.GetComponent<IActor>();
                        if (hitActor != null)
                        {
                            if (mCastCfg.GetTargetActors().Count > 0)
                            {
                                if (mCastCfg.GetTargetActors().Contains(hitActor))
                                {
                                    if (hitActor.GetActorSide() != mCaster.GetActor().GetActorSide())
                                    {
                                        LogDC.LogEx("skill get actor", hitActor.GetTransform().gameObject.name);
                                    }
                                }
                            }
                            else
                            {
                                if (hitActor.GetActorSide() != mCaster.GetActor().GetActorSide())
                                {
                                    LogDC.LogEx("skill get actor", hitActor.GetTransform().gameObject.name);
                                }
                            }
                        }
                    }
                }

                mHitCnt++;
            }
            else
            {
                SkillSys.Instance.DestroySkill(this);
            }

            mLife += Time.deltaTime;
        }
    }
}