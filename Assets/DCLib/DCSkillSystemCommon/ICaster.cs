using System.Collections.Generic;
using DC.ActorSystem;
using UnityEngine;

namespace DC.SkillSystem
{
    public interface ICaster
    {
        /// <summary>
        /// cast with specific cast config
        /// </summary>
        /// <param name="skillCfg"></param>
        /// <param name="castCfg"></param>
        /// <returns></returns>
        bool Cast(SkillCfg skillCfg, CastCfg castCfg);

        /// <summary>
        /// active skill or not
        /// active then player may adjust cast config
        /// </summary>
        /// <param name="skillCfg"></param>
        /// <param name="active"></param>
        void SetSkillActive(SkillCfg skillCfg, bool active);

        List<CastCfg> GetActiveCastCfgs();

        void UpdateCastConfig(ICastInput input);

        List<ISkill> GetActiveSkills();

        ISkill GetActiveSkill();

        /// <summary>
        /// 施法者本身的actor
        /// </summary>
        /// <returns></returns>
        IActor GetActor();

        CastMsg BuffAllowCast(ISkill skill);

        CastMsg ConsumeEnough(ISkill skill);

        CastMsg CdEnough(ISkill skill);

        Transform GetCastTransform(string name);

        Skill GetSkill(KeyCode key);
        void SetSkill(KeyCode key, Skill skill);
        void RemoveSkill(KeyCode key);

        Skill GetLastSkill();
    }
}