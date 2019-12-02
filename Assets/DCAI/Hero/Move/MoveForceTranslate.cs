using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class MoveForceTranslate : MoveBaseState
    {
        public override void Reason(object data)
        {
            switch (MoveCmpt.mMoveType)
            {
                case MoveType.TfPos:
                    if (MoveCmpt.mTfArrivePos.IsComplete())
                    {
                        ToState(EnumMoveTranslateCls.ToIdle);
                    }
                    return;
            }

            var buffCmpt = MoveCmpt.Actor.GetBuffCmpt();
            if (buffCmpt.Contains(BuffType.can_not_move))
            {
                ToState(EnumMoveTranslateCls.ToMoveStop);
                return;
            }
        }

        public override void DoBeforeEntering()
        {
            MoveCmpt.StopNav();

            MoveCmpt.mTfArrivePos.SetStop(false);
            MoveCmpt.mNavArrivePos.SetStop(true);
        }

        public override void Act(object data)
        {
            
        }
    }
}
