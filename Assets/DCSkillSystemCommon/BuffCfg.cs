using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    /*
     
        脚本控制和配置控制的比较

        脚本
            提供导出的功能对象，使用脚本组合
            更灵活，对编写脚本的人要求更高

        配置
            提供导出的选项，使用数值组合
            使用起来简单，同样可以灵活，对编写系统的人要求更高
            导出的是方法id和参数
     */

    public class RoleEffectedByBuff
    {
        public static readonly uint Owner = Convert.ToUInt32("1", 2);
        public static readonly uint Friend_Hero = Convert.ToUInt32("10", 2);
        public static readonly uint Friend_Soldier = Convert.ToUInt32("100", 2);
        public static readonly uint Enemy = Convert.ToUInt32("1000", 2);
    }

    public enum BuffType
    {

    }

    public interface IBuffCfg
    {
    }

    [CreateAssetMenu(fileName = "BuffCfg", menuName = "DC/ScriptableObjects/BuffCfg", order = 1)]
    public class BuffCfg : ScriptableObject, IBuffCfg
    {
        public int mRange;
        public uint mEffectRole;
    }
}