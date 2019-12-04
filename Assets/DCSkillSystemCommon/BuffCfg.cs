using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.ValueSys;

namespace DC.SkillSystem
{
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
    /// <summary>
    /// buff的影响类型
    /// </summary>
    public enum BuffType
    {
        hp,
        mp,

        physic_attack,
        magic_attack,
        physic_defense,
        magic_defense,
        physic_weaken,
        magic_weaken,

        /// <summary>
        /// 免伤
        /// </summary>
        immune_damage,

        can_not_move,

        force_translate,

        can_not_select,

        /// <summary>
        /// 反伤
        /// </summary>
        feedback_damage,

        /// <summary>
        /// 晕眩
        /// </summary>
        dizzy,

        invisible,

        /// <summary>
        /// 攻击特效
        /// 附带最大生命值
        /// 附带蓝量
        /// 附带魔法伤害
        /// 附带真实伤害
        /// </summary>
        special_attack,

        /// <summary>
        /// 死亡状态
        /// </summary>
        die,
    }

    public enum EffectRangeType
    {
        Single,
        Aoe,
    }

    public interface IBuffCfg
    {
    }

    [CreateAssetMenu(fileName = "BuffCfg", menuName = "DC/ScriptableObjects/BuffCfg", order = 1)]
    public class BuffCfg : ScriptableObject, IBuffCfg
    {
        public int mId;
        public BuffType mBuffType;
        public float mEffectRange;
        public RoleType mEffectRole;
        public string mName;
        public string mDesc;
        public string mUiIcon;
        public EffectRangeType mEffectRangeType;
        /// <summary>
        /// 视觉特效
        /// </summary>
        public VisualEffectCfg mVfxCfg;
        /// <summary>
        /// 数值影响
        /// </summary>
        public ValueEffectConfig MValueEffectCfg;

        public BuffEffectConfig mBuffEffectCfg;
    }
}