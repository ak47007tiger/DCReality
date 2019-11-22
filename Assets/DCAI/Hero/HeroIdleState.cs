using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;

namespace DC.AI
{
    public class HeroIdleState : BaseHeroState
    {
        public override void Reason(object data)
        {
            base.Reason(data);

            if (Input.GetMouseButtonDown(1))
            {
                var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (SystemPreset.IsGround(hit.transform.gameObject.tag))
                    {
                        //to move state
                        var hitPoint = hit.point;
                        MsgSys.Send(GameEvent.ClickEnvGround, hitPoint);
                    }
                }
            }

            //to skill state
            var skillKeyList = mHeroEntity.mHeroCfg.GetSkillKeyList();
            foreach (var keyCode in skillKeyList)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return;
                }
            }

            var buffEvt = GetBuffEvt(data);
            if (buffEvt != null && buffEvt.mOperate == BuffOperate.Add)
            {
                switch (buffEvt.mBuff.mBuffCfg.mBuffType)
                {
                    //to dizzy state
                    case BuffType.dizzy:
                        break;
                    //to stop state
                    case BuffType.can_not_move:
                        break;
                    case BuffType.die:
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
            var skillKeyList = mHeroEntity.mHeroCfg.GetSkillKeyList();
            foreach (var keyCode in skillKeyList)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return;
                }
            }
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
                        MsgSys.Send(GameEvent.ClickEnvGround, hitPoint);
                    }
                }
            }
        }
    }

    public class HeroSkillState : BaseHeroState
    {
        public override void Act(object data)
        {
            base.Act(data);
            var heroCfg = mHeroEntity.mHeroCfg;

            foreach (var code in heroCfg.GetSkillKeyList())
            {
                //准备技能 设置释放参数 or 直接释放
                if (Input.GetKeyDown(code))
                {
                    mHeroEntity.OnKeyEvent(code);
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

    }

}
