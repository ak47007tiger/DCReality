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
                    ToState(EnumMoveTranslateCls.ToMoveTarget);
                    return;
                case MoveType.TfTarget:
                    ToState(EnumMoveTranslateCls.ToMoveTarget);
                    return;
            }

            //to idle
            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavPos:
                    if (MoveCmpt.mNavArrivePos.IsComplete())
                    {
                        ToState(EnumMoveTranslateCls.ToIdle);
                    }
                    return;
                case MoveType.TfPos:
                    if (MoveCmpt.mTfArrivePos.IsComplete())
                    {
                        ToState(EnumMoveTranslateCls.ToIdle);
                    }
                    return;
            }

            //to force translate
            var buffCmpt = MoveCmpt.Actor.GetBuffCmpt();
            if (buffCmpt.Contains(BuffType.force_translate))
            {
                ToState(EnumMoveTranslateCls.ToMoveForceTranslate);
                return;
            }
            //to move stop
            if (buffCmpt.Contains(BuffType.can_not_move))
            {
                ToState(EnumMoveTranslateCls.ToMoveStop);
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
