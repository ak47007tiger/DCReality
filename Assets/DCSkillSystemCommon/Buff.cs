using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.GameLogic;

namespace DC.SkillSystem
{
    /*
     
        buff的生命周期管理是被管理的
        buff是信息提供者和被动的执行者
     */

    public class Buff : IBuff
    {
        public int mId;

        public BuffCfg mBuffCfg;

        List<GameActor> mEfxActorList = new List<GameActor>();

        public bool AllowCast(ISkill skill)
        {
            return true;
        }

        public void OnUpdate()
        {

        }
    }
}