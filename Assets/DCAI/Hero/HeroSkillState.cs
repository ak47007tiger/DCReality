using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class HeroSkillState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);
            //可能准备施法
            var lastSkill = Caster.GetLastSkill();
            if (lastSkill == null)
            {
                //不是施法，无操作
                if (Hero.GetSelectedSkillCfg() == null)
                {
                    Hero.ToState(EnumHeroTrans.ToIdle);
                }
                return;
            }

            //施法完
            //持续施法结束
            if (lastSkill.GetSkillCfg().mCastType == CastType.persistently
                && lastSkill.IsComplete())
            {
                Hero.ToState(EnumHeroTrans.ToIdle);
                return;
            }

            //瞬时技能释放完毕 or 取消施法
            if (Hero.GetSelectedSkillCfg() == null)
            {
                Hero.ToState(EnumHeroTrans.ToIdle);
                return;
            }

            //如果点击地面，判断是否可以切换状态，暂定所以技能都可以被移动打断
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(mousePosition);
            //nav move pos
            if (Physics.Raycast(ray, out var hit, 10, SystemPreset.layer_ground))
            {
                //清理技能
//                lastSkill.ClearSkill();
            }

            //释放其它技能会不会打断

            //如果被强制位移，判断是否可以切换状态
        }

        public override void Act(object data)
        {
            base.Act(data);
            //如果是持续施法中，忽略其他
            var lastSkill = Caster.GetLastSkill();
            if (null != lastSkill && lastSkill.GetSkillCfg().mCastType == CastType.persistently && !lastSkill.IsComplete())
            {
                return;
            }

            //if 选好了技能和目标 尝试施法
            var skillCfg = Hero.GetSelectedSkillCfg();
            var castCfg = Hero.GetCastCfg();
            if (null != skillCfg && null != castCfg)
            {
                //检查各种条件
                //距离
                switch (skillCfg.mTargetType)
                {
                    case SkillTargetType.Actor:
                        break;
                    case SkillTargetType.Position:
                        break;
                    case SkillTargetType.Direction:
                        break;
                }
                Caster.Cast(skillCfg, castCfg);
            }
        }

    }
}