using DC.GameLogic;

namespace DC.AI
{
    public abstract class MoveBaseState : DCFSMState
    {
        public HeroMoveComponent MoveCmpt;

        public void ToState(EnumMoveTrans trans)
        {
            LogDC.LogEx("trans: ", trans.ToString());
            MoveCmpt.mFsm.PerformTransition(trans.ToHashId());
        }
    }

}