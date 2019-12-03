using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class MovePosition : MoveBaseState
    {
        public override void Reason(object data)
        {
            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavTarget:
                    ToState(EnumMoveTrans.ToMoveTarget);
                    return;
                case MoveType.TfTarget:
                    ToState(EnumMoveTrans.ToMoveTarget);
                    return;
            }

            //to idle
            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavPos:
                    if (MoveCmpt.mNavArrivePos.IsComplete())
                    {
                        ToState(EnumMoveTrans.ToMoveIdle);
                    }
                    return;
                case MoveType.TfPos:
                    if (MoveCmpt.mTfArrivePos.IsComplete())
                    {
                        ToState(EnumMoveTrans.ToMoveIdle);
                    }
                    return;
            }

            //to force translate
            var buffCmpt = MoveCmpt.Actor.GetBuffCmpt();
            if (buffCmpt.Contains(BuffType.force_translate))
            {
                ToState(EnumMoveTrans.ToMoveForceTranslate);
                return;
            }
            //to move stop
            if (buffCmpt.Contains(BuffType.can_not_move))
            {
                ToState(EnumMoveTrans.ToMoveStop);
                return;
            }
        }

        public override void DoBeforeEntering()
        {
            MoveCmpt.StopTrace();

            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavPos:
                    MoveCmpt.mNavArrivePos.SetStop(false);
                    MoveCmpt.mTfArrivePos.SetStop(true);
                    return;
                case MoveType.TfPos:
                    MoveCmpt.mNavArrivePos.SetStop(true);
                    MoveCmpt.mTfArrivePos.SetStop(false);
                    return;
            }
        }

        public override void Act(object data)
        {
            
        }
    }
}
