using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    public class HeroForceTranslateState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            var buffEvt = GetBuffEvt(data);
            if (null != buffEvt && buffEvt.mOperate == BuffOperate.Add)
            {
                switch (buffEvt.mBuff.mBuffCfg.mBuffType)
                {
                    case BuffType.dizzy:
                        Hero.SetTransition(Transition.ToDizzy);
                        break;
                    case BuffType.die:
                        Hero.SetTransition(Transition.ToDie);
                        break;
                }
            }

            if (null != buffEvt && buffEvt.mOperate == BuffOperate.Remove)
            {
                switch (buffEvt.mBuff.mBuffCfg.mBuffType)
                {
                    case BuffType.force_translate:
                        Hero.SetTransition(Transition.ToIdle);
                        break;
                }
            }
        }
    }
}