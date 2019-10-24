using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.SkillSystem;
using DC.ValueSys;

namespace DC.GameLogic
{
    public class GameActor : GameElement, IActor
    {
        private List<SkillCfg> mSkillCfgs;

        private Dictionary<SkillCfg, ISkill> mCfgToSkill;

        public void Attack()
        {
            var targetSys = GetTargetSystem();
            SkillCfg normalAttackSkillCfg = null;
            var targets = targetSys.GetTargets(this, GetCaster(), normalAttackSkillCfg);
            Attack(targets);
        }

        public void Attack(List<IActor> targets)
        {
            SkillCfg normalAttackSkillCfg = null;
            CastCfg normalAttackCastCfg = null;

            GetCaster().Cast(normalAttackSkillCfg, normalAttackCastCfg);
        }

        public ICaster GetCaster()
        {
            throw new System.NotImplementedException();
        }

        public List<IBuff> GetOwnerBuffs()
        {
            throw new System.NotImplementedException();
        }

        public void SetOwnerBuffs(List<IBuff> buffs)
        {
            throw new System.NotImplementedException();
        }

        public IValueComponent GetValueComponent()
        {
            throw new System.NotImplementedException();
        }

        public void AddBuff(IBuff buff)
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetModel()
        {
            throw new System.NotImplementedException();
        }

        public void SetVisibility(bool show)
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimator(int animatorId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, int value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, float value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}