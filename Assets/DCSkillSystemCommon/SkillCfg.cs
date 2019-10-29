using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.Collections.Generic;
using DC.DCPhysics;
using DC.ValueSys;

namespace DC.SkillSystem
{
    /*
     
        应该有个地方用来统计释放技能过程中的各种信息
            释放技能
            受到攻击
            最后普攻的目标
        
        二次触发的技能：技能创建的技能
        技能组：一次释放一组技能

        蛮王 
            a, 近战伤害
            w, buff
            e, 位移 伤害
            r, buff
        寒冰 
            a, 远程伤害
            w, aoe 伤害
            e, 地图迷雾
            r, 单体 伤害 buff
        ez 
            q, 单体 伤害
            w, aoe 伤害
            e, 位移 伤害
        蚂蚱 
            w, aoe damage
            e, single target create npc
        vn 
            a, damage buff
            q, 位移, 伤害, buff, 增强模式
            w, 给普攻的buff
            e, 位移, buff
            r, buff
        火男 
            w, aoe, buff, damage
            r, single, ai, 多次
        武器 
            e, buff
        皇子 
            r, damage, 地形
        死歌 
            r, damage
        装备技能
            女妖面纱, buff
            金身, buff
            破败, damage, cure, buff
            九头蛇, damage, buff
            饮魔刀, buff
            智慧末刃, buff
            鬼刀,buff

        存活时间
        生效次数
     */
    public interface ISkillCfg
    {
    }

    public enum SkillTargetType
    {
        None,
        Actor,
        Direction,
        Position,
    }

    public enum SkillType
    {
        normal,
        bullet,
        area,
    }

    [CreateAssetMenu(fileName = "SkillCfg", menuName = "DC/ScriptableObjects/SkillCfg", order = 1)]
    public class SkillCfg : ScriptableObject, ISkillCfg
    {
        public int mId;
        public float mDuration;
        public int mHitCnt = 1;
        public List<KVPair<GValueType, float>> mConsumeList;
        public List<KVPair<GValueType, float>> mValueEffectList;

        public SkillAnimationCfg mSkillAnimationCfg;

        public List<KVPair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt;

        /// <summary>
        /// 施法距离
        /// </summary>
        /// <returns></returns>
        public float mCastRange;

        public string mPrefabPath;

        public SkillType mSkillType;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float mSpeed;

        /// <summary>
        /// 区域技能生效间隔
        /// </summary>
        public float mAffectInterval;

        /// <summary>
        /// 生效后死亡
        /// </summary>
        public bool mDieAfterDone;

        public List<KVPair<GValueType, float>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KVPair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }
    }
}