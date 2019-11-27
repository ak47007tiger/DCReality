using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    public class HeroDieState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            //to idle state
            var buffEvt = GetBuffEvt(data);
            if (buffEvt != null && buffEvt.mOperate == BuffOperate.Remove)
            {
                switch (buffEvt.mBuff.mBuffCfg.mBuffType)
                {
                    //to dizzy state
                    case BuffType.die:
                        break;
                    //to stop state
                    case BuffType.can_not_move:
                        break;
                }
            }
        }
    }
}