using DC.GameLogic;

namespace DC.AI
{
    public abstract class MoveBaseState : DCFSMState
    {
        public HeroMoveComponent MoveCmpt;

        public void ToState(EnumMoveTrans trans)
        {
            MoveCmpt.mFsm.PerformTransition(trans.ToHashId());
        }
    }

}