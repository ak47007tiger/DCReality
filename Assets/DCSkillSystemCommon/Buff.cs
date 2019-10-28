using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    public class Buff : IBuff
    {
        public int mId;

        public BuffCfg mBuffCfg;

        public bool AllowCast(ISkill skill)
        {
            throw new NotImplementedException();
        }
    }
}