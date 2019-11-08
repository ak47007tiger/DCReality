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

    public enum BuffType
    {
        /// <summary>
        /// 速度
        /// </summary>
        move_speed,
        physic_attack,
        magic_attack,
        physic_defense,
        magic_defense,
        physic_weaken,
        magic_weaken,
        immune_damage,
        can_not_select,
        /// <summary>
        /// 反伤
        /// </summary>
        feedback_damage,
        /// <summary>
        /// 晕眩
        /// </summary>
        stun,
        hp,
        mp,
        invisible,
        //攻击特效
        special_attack
    }

    public interface IBuffCfg
    {
    }

    [CreateAssetMenu(fileName = "BuffCfg", menuName = "DC/ScriptableObjects/BuffCfg", order = 1)]
    public class BuffCfg : ScriptableObject, IBuffCfg
    {
        public int mId;
        public BuffType mBuffType;
        public int mEffectRange;
        public uint mEffectRole;
        public string mName;
        public string mDesc;
    }

}