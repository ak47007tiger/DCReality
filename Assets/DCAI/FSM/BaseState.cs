using DC.GameLogic;
using DC.SkillSystem;

using UnityEngine;

namespace DC.AI
{
  public class BaseState : FSMState
  {
    public override void Reason(object data)
    {

    }

    public override void Act(object data)
    {

    }
  }

  public class BaseHeroState : FSMState
  {
    protected HeroEntity Hero;

    protected Caster Caster;

    protected GameActor Actor;

    public void SetStateId(StateID pStateId)
    {
      stateID = pStateId;
    }

    public void SetUp(GameObject ctxObj)
    {
      Hero = ctxObj.GetComponent<HeroEntity>();
      Caster = ctxObj.GetComponent<Caster>();
      Actor = ctxObj.GetComponent<GameActor>();
    }

    public override void Reason(object data)
    {

    }

    public override void Act(object data)
    {

    }

    public static BuffEvt GetBuffEvt(object data)
    {
      if (data is BuffEvt)
        return (BuffEvt)data;
      return null;
    }

    public bool CheckToSkillState()
    {
      //to skill state
      if (Input.GetMouseButtonDown(0))
      {
        var selectedSkillCfg = Hero.GetSelectedSkillCfg();

        if (null != selectedSkillCfg && Hero.mIsCastPrepareReady)
        {
          Hero.SetTransition(Transition.ToSkill);
          return true;
        }
      }
      return false;
    }

    public bool CheckToMoveState()
    {
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
            return true;
          }
        }
      }
      return false;
    }
  }

}
