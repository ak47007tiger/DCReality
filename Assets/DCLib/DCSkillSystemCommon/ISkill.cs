using System.Collections.Generic;
using DC.ActorSystem;
using DC.GameLogic;
using UnityEngine;

namespace DC.SkillSystem
{
    public interface ISkill
    {
        Caster GetCaster();
        void SetCaster(Caster caster);

        CastCfg GetCastCfg();
        void SetCastCfg(CastCfg castCfg);

        SkillCfg GetSkillCfg();
        void SetSkillCfg(SkillCfg skillCfg);

        bool AllowCastTo(IActor actor);

        void OnCatchTarget(IActor target);

        List<IActor> GetEffectTargets();

        void OnSkillLifeRecycle(SkillLifeCycle lifeCycle);

        void Create();

        Transform GetTransform();

        float GetTickedLife();

        bool IsComplete();

        void ClearSkill();
    }
}