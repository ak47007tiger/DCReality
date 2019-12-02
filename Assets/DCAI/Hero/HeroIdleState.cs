using DC.GameLogic;
using DC.SkillSystem;

namespace DC.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class HeroIdleState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            var lastAddBuffCfg = Actor.GetBuffCmpt().GetLastAddBuffCfg();
            switch (lastAddBuffCfg.mBuffType)
            {
                //to dizzy state
                case BuffType.dizzy:
                    Hero.ToState(EnumHeroTrans.ToDizzy);
                    return;
                case BuffType.die:
                    Hero.ToState(EnumHeroTrans.ToDie);
                    return;
            }

            if (Hero.GetSelectedSkillCfg() != null)
            {
                Hero.ToState(EnumHeroTrans.ToSkill);

                return;
            }
        }

    }

}
