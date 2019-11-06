using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

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

        public bool AllowCast(ISkill skill)
        {
            return true;
        }

        public void OnAdd(IActor actor)
        {

        }

        public void OnRemove(IActor actor)
        {

        }

        public void OnUpdate()
        {

        }
    }
}