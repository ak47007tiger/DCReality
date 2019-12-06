using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.ValueSys;

namespace DC.SkillSystem
{
    /*public enum RoleEffectedByBuff
    {
        Caster,
        Friend,
        Enemy,
        Neutral,
        All
    }*/
    /*public class RoleEffectedByBuff
    {
        public static readonly uint Owner = Convert.ToUInt32("1", 2);
        public static readonly uint Friend_Hero = Convert.ToUInt32("10", 2);
        public static readonly uint Friend_Soldier = Convert.ToUInt32("100", 2);
        public static readonly uint Enemy = Convert.ToUInt32("1000", 2);
    }*/

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

        /// <summary>
        /// aoe状态buff，可以生成buff的buff
        /// </summary>
        state_buff,
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
        /// <summary>
        /// 持续时间
        /// </summary>
        public float mDuration;
        public string mName;
        public string mDesc;
        public string mUiIcon;
        /// <summary>
        /// aoe还是单体
        /// </summary>
        public EffectRangeType mEffectRangeType;
        /// <summary>
        /// 技能影响到的所属关系
        /// </summary>
        public List<SideRelation> mEffectSideRelations;
        /// <summary>
        /// 视觉特效
        /// </summary>
        public VisualEffectCfg mVfxCfg;
        /// <summary>
        /// 数值影响
        /// </summary>
        public ValueEffectConfig mValueEffectCfg;
        /// <summary>
        /// 施加buff的buff
        /// </summary>
        public BuffEffectConfig mBuffEffectConfig;

        public bool IsInEffectRelation(SideRelation relation)
        {
            return mEffectSideRelations.Contains(relation);
        }

        /// <summary>
        /// -2 同一个buff
        /// 0 相同
        /// -1 弱于
        /// 1 强于
        /// 2 不相干
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int Compare(BuffCfg config)
        {
            if (config.mId == mId)
            {
                return -2;
            }

            if (config.mBuffType != mBuffType)
            {
                return 2;
            }

            return mValueEffectCfg.Compare(config.mValueEffectCfg);
        }
    }
}