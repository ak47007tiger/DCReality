using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem.ConfigBased
{
    interface IUseCase
    {

    }

    public class ConfigBasedSkillSystem : IUseCase
    {
        public void NormalAttack()
        {

        }
    }

    interface ISkill
    {
        int Damage();
        int IncreaseHp();

        void Apply();

        void Apply(IActor target);

    }

    class Skill
    {
        public ICaster caster;

        public List<IActor> targets;

        public void DoEffect()
        {

        }
    }

    class Buff : Skill
    {

        public virtual void UpdateTargets()
        {

        }
    }

    class BuffConfig : SkillConfig
    {
        
    }

    enum BuffType
    {
        Value,

    }

    class SkillConfig
    {
        public int id;
        public string uiIcon;
        public string effectPath;
        public int duration;//毫秒数
        public int groupMax;

        public int rangeStart;
        public int rangeEnd;

        public int[] paramArray;
    }

    enum SkillType
    {
        NormalAttack,

    }

    enum SkillTargetType
    {
        Single,
        Group,
    }

    enum TargetType
    {
        
    }

    class SkillEffect
    {
        
    }
}
