using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{

    public interface ICastSystem
    {
        CastCfg GetDefaultCastCfg(SkillCfg skillCfg);
    }

    public class CastSys : Singleton<CastSys>, ICastSystem
    {
        public CastCfg GetDefaultCastCfg(SkillCfg skillCfg)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 转换ui或者游戏中的input为技能理解的输入
    /// </summary>
    public interface ICastInput
    {
        List<IActor> GetTargets();
        void SetTargets(List<IActor> targets);
    }
}