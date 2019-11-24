using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

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

            //to move state
            if (Input.GetMouseButtonDown(1))
            {
                var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (SystemPreset.IsGround(hit.transform.gameObject.tag))
                    {
                        var hitPoint = hit.point;
                        Hero.SetTransition(Transition.ToMove);
                        return;
                    }
                }
            }

            //to skill state
            var selectedSkillCfg = Hero.GetSelectedSkillCfg();

            if (null != selectedSkillCfg && Hero.mIsCastPrepareReady)
            {
                Hero.SetTransition(Transition.ToSkill);
                return;
            }

            var buffEvt = GetBuffEvt(data);
            if (buffEvt != null && buffEvt.mOperate == BuffOperate.Add)
            {
                switch (buffEvt.mBuff.mBuffCfg.mBuffType)
                {
                    //to dizzy state
                    case BuffType.dizzy:
                        Hero.SetTransition(Transition.ToDizzy);
                        break;
                    //to stop state
                    case BuffType.can_not_move:
                        Hero.SetTransition(Transition.ToStop);
                        break;
                    case BuffType.force_translate:
                        Hero.SetTransition(Transition.ToForceTranslate);
                        break;
                    case BuffType.die:
                        Hero.SetTransition(Transition.ToDie);
                        break;
                }
            }
        }

    }

    public class HeroMoveState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            //to skill state
            var skillKeyList = Hero.mHeroCfg.GetSkillKeyList();
            foreach (var keyCode in skillKeyList)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return;
                }
            }
        }

        public override void DoBeforeLeaving()
        {
            base.DoBeforeLeaving();
            Actor.NavArrivePosition.SetStop(true);
        }

        public override void Act(object data)
        {
            base.Act(data);

            if (Input.GetMouseButtonDown(1))
            {
                var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (SystemPreset.IsGround(hit.transform.gameObject.tag))
                    {
                        //修改移动位置
                        var hitPoint = hit.point;
                        if (Actor.IsAutoMoving())
                        {
                            Actor.StopAutoMove();
                        }

//                        Actor.NavArrivePosition.StartTrace(hitPoint, SystemPreset.move_stop_distance);
//                        MsgSys.Send(GameEvent.ClickEnvGround, hitPoint);
                    }
                }
            }
        }
    }

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

    public class HeroStopState : BaseHeroState
    {

    }

    public class HeroDizzyState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);
        }
    }

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
