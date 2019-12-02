using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class MoveIdle : MoveBaseState
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
                case MoveType.NavTarget:
                    ToState(EnumMoveTranslateCls.ToMoveTarget);
                    return;
                case MoveType.TfTarget:
                    ToState(EnumMoveTranslateCls.ToMoveTarget);
                    return;
            }

            //to force translate
            BuffCmpnt buffCmpnt = MoveCmpt.Actor.GetBuffCmpnt();
            if (buffCmpnt.Contains(BuffType.force_translate))
            {
                ToState(EnumMoveTranslateCls.ToMoveForceTranslate);
                return;
            }
            //to move stop
            if (buffCmpnt.Contains(BuffType.can_not_move))
            {
                ToState(EnumMoveTranslateCls.ToMoveStop);
                return;
            }
        }

        public override void Act(object data)
        {

        }
    }
}
