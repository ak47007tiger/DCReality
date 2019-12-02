using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class MoveTarget : MoveBaseState
    {
        public override void Reason(object data)
        {
            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavPos:
                    ToState(EnumMoveTranslateCls.ToMovePosition);
                    return;
                case MoveType.TfPos:
                    ToState(EnumMoveTranslateCls.ToMovePosition);
                    return;
            }

            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavTarget:
                    if (MoveCmpt.mNavTraceTarget.IsComplete())
                    {
                        ToState(EnumMoveTranslateCls.ToIdle);
                    }
                    return;
                case MoveType.TfTarget:
                    if (MoveCmpt.mTfTraceTarget.IsComplete())
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
            MoveCmpt.StopPos();

            switch (MoveCmpt.mMoveType)
            {
                case MoveType.NavTarget:
                    MoveCmpt.mNavTraceTarget.SetStop(false);
                    MoveCmpt.mTfTraceTarget.SetStop(true);
                    return;
                case MoveType.TfTarget:
                    MoveCmpt.mNavTraceTarget.SetStop(true);
                    MoveCmpt.mTfTraceTarget.SetStop(false);
                    return;
            }
        }

        public override void Act(object data)
        {

        }

    }

}
