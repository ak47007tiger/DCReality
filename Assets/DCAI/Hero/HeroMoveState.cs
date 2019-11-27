using DC.GameLogic;

using UnityEngine;

namespace DC.AI
{
  public class HeroMoveState : BaseHeroState
  {
    public override void Reason(object data)
    {
      base.Reason(data);

      //to skill state
      if (CheckToSkillState())
      {
        return;
      }
      // var skillKeyList = Hero.mHeroCfg.GetSkillKeyList();
      // foreach (var keyCode in skillKeyList)
      // {
      //     if (Input.GetKeyDown(keyCode))
      //     {
      //         return;
      //     }
      // }
    }

    public override void DoBeforeLeaving()
    {
      base.DoBeforeLeaving();
      Actor.NavTraceTarget.SetStop(true);
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

            Actor.NavArrivePosition.StartTrace(hitPoint, SystemPreset.move_stop_distance);
            //                        MsgSys.Send(GameEvent.ClickEnvGround, hitPoint);
          }
        }
      }
    }
  }
}
