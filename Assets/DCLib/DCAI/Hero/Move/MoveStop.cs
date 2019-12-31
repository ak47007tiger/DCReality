using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class MoveStop : MoveBaseState
    {
        public override void Reason(object data)
        {
            var buffCmpt = MoveCmpt.Actor.GetBuffCmpt();
            if (buffCmpt.Contains(BuffType.force_translate))
            {
                ToState(EnumMoveTrans.ToMoveForceTranslate);
                return;
            }

            if (!buffCmpt.Contains(BuffType.can_not_move))
            {
                ToState(EnumMoveTrans.ToMoveIdle);
                return;
            }
        }

        public override void DoBeforeEntering()
        {
            MoveCmpt.mNavArrivePos.SetStop(true);
            MoveCmpt.mNavTraceTarget.SetStop(true);
            MoveCmpt.mTfArrivePos.SetStop(true);
            MoveCmpt.mTfTraceTarget.SetStop(true);
        }

        public override void Act(object data)
        {
            //todo 尝试进入该状态是记录一个位置，如果位置变了就还原位置
        }
    }
}
