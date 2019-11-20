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
}
