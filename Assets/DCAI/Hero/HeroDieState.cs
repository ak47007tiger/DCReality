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
            var buffCmpt = Actor.GetBuffCmpt();
            if (!buffCmpt.Contains(BuffType.die))
            {
                Hero.ToState(EnumHeroTrans.ToIdle);
                //todo d.c move birth pos
            }
        }
    }
}