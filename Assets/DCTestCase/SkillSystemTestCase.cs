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

        void TimeTrigger();
    }

    public class TSkillSystemTestCaseImpl : GameContextObject, ITestCase
    {
        public void NormalAttack()
        {
        }

        public void NormalAttackTarget()
        {
        }

        public void CastToActor()
        {
        }

        public void CastToDirection()
        {
        }

        public void CastToPosition()
        {
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

        public void TimeTrigger()
        {
        }
    }
    
}
