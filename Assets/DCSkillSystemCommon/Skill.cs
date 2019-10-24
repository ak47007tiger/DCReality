using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
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

        public ICaster GetCaster()
        {
            throw new NotImplementedException();
        }

        public void SetCaster(ICaster caster)
        {
            throw new NotImplementedException();
        }

        public CastCfg GetCastCfg()
        {
            throw new NotImplementedException();
        }

        public void SetCastCfg(CastCfg castCfg)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void OnCatchTarget(IActor target)
        {
            throw new NotImplementedException();
        }

        public List<IActor> TryCollectTargets()
        {
            throw new NotImplementedException();
        }

        public void OnSkillLifeRecycle(SkillLifeCycle lifeCycle)
        {
            throw new NotImplementedException();
        }

        public void Apply()
        {
            throw new NotImplementedException();
        }
    }
}