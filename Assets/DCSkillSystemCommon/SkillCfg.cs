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

        经过一段时间的延迟再生效
            较长的施法动画
            先播放动画，在动画进行一段时间后生效
        
        二次触发的技能：技能创建的技能

        技能组：一次释放一组技能
        技能效果：一个技能造成多个效果
            buff
            视觉表现
                相机效果
                人物效果

        时间驱动的技能
            生效时加buff
            一段时间后生效

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

    public enum TranslateType
    {
        off_target,
        close_target,
    }

    public enum HpType
    {
        physics,
        magic,
        real,
    }

    [CreateAssetMenu(fileName = "SkillCfg", menuName = "DC/ScriptableObjects/SkillCfg", order = 1)]
    public class SkillCfg : ScriptableObject, ISkillCfg
    {
        public int mId;
        public string mName;
        public string mDesc;
        public float mDuration;
        public int mHitCnt = 1;
        public List<KVPair<GValueType, float>> mConsumeList;
        public List<KVPair<GValueType, float>> mValueEffectList;

        public List<KVPair<string, string>> mEffectAndTransformNames;

        public SkillTargetType mTargetType;
        public int mMaxTargetCnt = 1000;

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

        public bool mTimer = true;

        /// <summary>
        /// 区域技能生效间隔
        /// </summary>
        public float mAffectInterval;

        /// <summary>
        /// 技能施加影响的延迟
        /// </summary>
        public float mEffectDelay;

        /// <summary>
        /// 生效后死亡
        /// </summary>
        public bool mDieAfterDone = true;

        public List<EventHandlerConfig> mEvtHandlerCfgs;

        /// <summary>
        /// 可以影响的side
        /// </summary>
        public List<ActorSide> mEffectSide;

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
