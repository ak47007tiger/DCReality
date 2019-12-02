using DC.GameLogic;

namespace DC.AI
{
    public enum MoveState
    {
        Idle,
        MovePosition,
        MoveStop,
        MoveTarget,
        MoveTranslate,
        MoveForceTranslate,
    }

    public class MoveStateCls
    {
        public static readonly int Idle = "Idle".GetExtHashCode();
        public static readonly int MovePosition = "MovePosition".GetExtHashCode();
        public static readonly int MoveStop = "MoveStop".GetExtHashCode();
        public static readonly int MoveTarget = "MoveTarget".GetExtHashCode();
        public static readonly int MoveTranslate = "MoveTranslate".GetExtHashCode();
        public static readonly int MoveForceTranslate = "MoveForceTranslate".GetExtHashCode();
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
        public static readonly int ToMoveTranslate = "ToMoveTranslate".GetExtHashCode();
        public static readonly int ToMoveForceTranslate = "ToMoveForceTranslate".GetExtHashCode();
    }

    public static class EE
    {
        public static int GetInt(this MoveState state)
        {
            return state.ToString().GetExtHashCode();
        }
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