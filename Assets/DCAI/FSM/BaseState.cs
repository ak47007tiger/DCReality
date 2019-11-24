using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class BaseState : FSMState
    {
        public override void Reason(object data)
        {

        }

        public override void Act(object data)
        {

        }
    }

    public class BaseHeroState : FSMState
    {
        protected HeroEntity Hero;

        protected Caster Caster;

        protected GameActor Actor;

        public void SetStateId(StateID pStateId)
        {
            stateID = pStateId;
        }

        public void SetUp(GameObject ctxObj)
        {
            Hero = ctxObj.GetComponent<HeroEntity>();
            Caster = ctxObj.GetComponent<Caster>();
            Actor = ctxObj.GetComponent<GameActor>();
        }

        public override void Reason(object data)
        {

        }

        public override void Act(object data)
        {

        }

        public static BuffEvt GetBuffEvt(object data)
        {
            if (data is BuffEvt)
                return (BuffEvt) data;
            return null;
        }
    }

}
