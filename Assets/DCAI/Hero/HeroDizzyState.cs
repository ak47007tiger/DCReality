using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    public class HeroDizzyState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            //to idle state
            var buffCmpt = Actor.GetBuffCmpt();
            if (!buffCmpt.Contains(BuffType.dizzy))
            {
                Hero.ToState(EnumHeroTrans.ToIdle);
            }
        }
    }
}