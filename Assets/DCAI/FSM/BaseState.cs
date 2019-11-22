using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class BaseState : FSMState
    {
        public override void Reason(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void Act(object data)
        {
            throw new System.NotImplementedException();
        }
    }

    public class BaseHeroState : FSMState
    {
        protected HeroEntity mHeroEntity;

        protected Caster mCaster;

        protected GameActor mActor;

        public void SetUp(GameObject ctxObj)
        {
            mHeroEntity = ctxObj.GetComponent<HeroEntity>();
            mCaster = ctxObj.GetComponent<Caster>();
            mActor = ctxObj.GetComponent<GameActor>();
        }

        public override void Reason(object data)
        {

        }

        public override void Act(object data)
        {

        }

        public static BuffEvt GetBuffEvt(object data)
        {
            if (data is BuffEvt buff)
                return (BuffEvt) data;
            return null;
        }
    }

}
