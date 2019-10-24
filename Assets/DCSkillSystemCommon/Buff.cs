using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    public interface IBuff
    {
        int GetId();

        BuffCfg GetBuffCfg();

        /// <summary>
        /// buff拥有者是否可以释放技能
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        bool AllowCast(ISkill skill);
    }
}