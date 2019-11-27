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

      if (CheckToMoveState())
      {
        return;
      }

      if (CheckToSkillState())
      {
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

}
