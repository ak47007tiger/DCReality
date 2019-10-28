using System.Collections.Generic;
using DC.ActorSystem;
using UnityEngine;

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

        Transform GetTransform();
    }
}