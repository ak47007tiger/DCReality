using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class HeroIdleState : HeroBaseState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            var lastAddBuffCfg = Actor.GetBuffCmpt().GetLastAddBuffCfg();
            if(null != lastAddBuffCfg)
            {
                switch (lastAddBuffCfg.mBuffType)
                {
                    //to dizzy state
                    case BuffType.dizzy:
                        Hero.ToState(EnumHeroTrans.ToHeroDizzyState);
                        return;
                    case BuffType.die:
                        Hero.ToState(EnumHeroTrans.ToHeroDieState);
                        return;
                }
            }

            if (Hero.GetSelectedSkillCfg() != null)
            {
                Hero.ToState(EnumHeroTrans.ToHeroSkillState);

                return;
            }
        }

    }

}
