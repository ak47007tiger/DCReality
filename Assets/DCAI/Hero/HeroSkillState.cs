using UnityEngine;

namespace DC.AI
{
    public class HeroSkillState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);
            //如果点击地面，判断是否可以切换状态

            //如果被强制位移，判断是否可以切换状态
        }

        public override void Act(object data)
        {
            base.Act(data);
            var heroCfg = Hero.mHeroCfg;

            SkillSystem.SkillCfg skillCfg = Hero.GetSelectedSkillCfg();
            if (null != skillCfg)
            {
                Caster.Cast(skillCfg, Hero.GetCastCfg());
            }

            foreach (var code in heroCfg.GetSkillKeyList())
            {
                //准备技能 设置释放参数 or 直接释放
                if (Input.GetKeyDown(code))
                {
                    Hero.OnKeyEvent(code);
                    break;
                }
            }
        }
    }
}