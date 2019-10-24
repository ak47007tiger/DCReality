using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DC.ActorSystem;
using DC.SkillSystem;
using DC.GameLogic;

namespace DC.SkillSystem
{
    public interface ITestCase
    {
        void NormalAttack();
        void NormalAttackTarget();

        void CastToActor();
        void CastToDirection();
        void CastToPosition();

        //compose
        void CastComposeSkill();

        //buff
        void CastToActorWithSelfBuff();
        void CastToActorWithOppositeBuff();
    }

    public class TSkillSystemTestCaseImpl : GameContextObject, ITestCase
    {
        public void NormalAttack()
        {
            IActor actor = null;
            ICaster caster = null;
            SkillCfg normalAttackCfg = null;
            //get from user input
            CastCfg castCfg = null;

            var targets = GetTargetSystem().GetTargets(actor, caster, normalAttackCfg);
            castCfg.SetTargetActors(targets);

            caster.Cast(normalAttackCfg, castCfg);
        }

        public void NormalAttackTarget()
        {
            ICaster caster = null;
            SkillCfg normalAttackCfg = null;
            //get from user input
            CastCfg castCfg = null;

            //get from user input
            List<IActor> targets = null;
            castCfg.SetTargetActors(targets);

            caster.Cast(normalAttackCfg, castCfg);
        }

        public void CastToActor()
        {
            ICaster caster = null;
            SkillCfg skillCfg = null;
            //get from user input
            CastCfg castCfg = null;

            //get from user input
            List<IActor> targets = null;
            castCfg.SetTargetActors(targets);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastToDirection()
        {
            ICaster caster = null;
            SkillCfg skillCfg = null;
            //get from user input
            CastCfg castCfg = null;

            //get from user input
            var direction = Vector3Int.zero;
            castCfg.SetDirection(direction);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastToPosition()
        {
            ICaster caster = null;
            SkillCfg skillCfg = null;
            //get from user input
            CastCfg castCfg = null;

            //get from user input
            var position = Vector3Int.zero;
            castCfg.SetDirection(position);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastComposeSkill()
        {
            throw new System.NotImplementedException();
        }

        public void CastToActorWithSelfBuff()
        {
            throw new System.NotImplementedException();
        }

        public void CastToActorWithOppositeBuff()
        {
            throw new System.NotImplementedException();
        }
    }
}


