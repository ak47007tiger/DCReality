using DC.GameLogic;

namespace DC.AI
{
    public enum MoveState
    {
        Idle,
        MovePosition,
        MoveStop,
        MoveTarget,
        MoveForceTranslate,
    }

    public enum EnumMoveTranslate
    {
        ToIdle,
        ToMovePosition,
        ToMoveStop,
        ToMoveTarget,
        ToMoveTranslate,
        ToMoveForceTranslate,
    }

    public class EnumMoveTranslateCls
    {
        public static readonly int ToIdle = "ToIdle".GetExtHashCode();
        public static readonly int ToMovePosition = "ToMovePosition".GetExtHashCode();
        public static readonly int ToMoveStop = "ToMoveStop".GetExtHashCode();
        public static readonly int ToMoveTarget = "ToMoveTarget".GetExtHashCode();
        public static readonly int ToMoveForceTranslate = "ToMoveForceTranslate".GetExtHashCode();
    }

    public abstract class MoveBaseState : DCFSMState
    {
        public HeroMoveComponent MoveCmpt;

        public void ToState(int trans)
        {
            MoveCmpt.mFsm.PerformTransition(trans);
        }
    }

}