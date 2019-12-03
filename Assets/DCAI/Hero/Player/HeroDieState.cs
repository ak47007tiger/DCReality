using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    public class HeroDieState : HeroBaseState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            //to idle state
            var buffCmpt = Actor.GetBuffCmpt();
            if (!buffCmpt.Contains(BuffType.die))
            {
                Hero.ToState(EnumHeroTrans.ToHeroIdleState);
                //todo d.c move birth pos
            }
        }
    }
}