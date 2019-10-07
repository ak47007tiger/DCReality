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

        ICastCfg GetCastCfg();
        void SetCastCfg(ICastCfg castCfg);

        ISkillCfg GetSkillCfg();
        void SetSkillCfg(ISkillCfg skillCfg);

        bool AllowCastTo(IActor actor);

        void OnCatchTarget(IActor target);

        List<IActor> TryCollectTargets();

        void OnSkillLifeRecycle(SkillLifeCycle lifeCycle);

        void Apply();
    }

    public class Skill : ISkill
    {
        private ICaster mCaster;


        public ICaster GetCaster()
        {
            throw new NotImplementedException();
        }

        public void SetCaster(ICaster caster)
        {
            throw new NotImplementedException();
        }

        public ICastCfg GetCastCfg()
        {
            throw new NotImplementedException();
        }

        public void SetCastCfg(ICastCfg castCfg)
        {
            throw new NotImplementedException();
        }

        public ISkillCfg GetSkillCfg()
        {
            throw new NotImplementedException();
        }

        public void SetSkillCfg(ISkillCfg skillCfg)
        {
            throw new NotImplementedException();
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