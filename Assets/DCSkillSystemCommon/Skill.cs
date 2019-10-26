using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    public interface ISkill
    {
        ICaster GetCaster();
        void SetCaster(ICaster caster);

        CastCfg GetCastCfg();
        void SetCastCfg(CastCfg castCfg);

        SkillCfg GetSkillCfg();
        void SetSkillCfg(SkillCfg skillCfg);

        bool AllowCastTo(IActor actor);

        void OnCatchTarget(IActor target);

        List<IActor> TryCollectTargets();

        void OnSkillLifeRecycle(SkillLifeCycle lifeCycle);

        void Apply();
    }

    public class Skill : BaseMonoBehaviour, ISkill
    {
        private ICaster mCaster;

        private SkillCfg mSkillCfg;

        private CastCfg mCastCfg;

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
    }
}