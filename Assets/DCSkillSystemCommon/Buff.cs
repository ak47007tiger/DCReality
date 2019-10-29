using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    /*
     
        影响数值
        影响交互操作

        速度
        攻击力
        法强
        防御
        魔抗
        物理穿透
        魔法穿透
        免伤
        不可选取
        反伤
        晕眩
        血量
        隐身
        蓝量
        攻击特效
     */

    public class Buff : IBuff
    {
        public int mId;

        public BuffCfg mBuffCfg;

        public bool AllowCast(ISkill skill)
        {
            return true;
        }
    }
}