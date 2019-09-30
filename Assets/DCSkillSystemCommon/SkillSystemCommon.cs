using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    /*
     技能
     装备
     角色

     buff

     数值计算

     end
     */

    public interface ISkillSystem
    {
        ISkill CreateSkill(ISkillCfg cfg);

        List<ISkill> GetActiveSkills();

        void AddSkill(ISkill skill);

        void RemoveSkill(ISkill skill);

        ISkillCfg GetSkillCfg(int id);

        /// <summary>
        /// 释放设定
        /// 数值计算
        /// 视觉表现
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillCreate(ISkill skill);

        /// <summary>
        /// 跟踪技能对象 查询变量 触发各类条件
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillUpdate(ISkill skill);

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillComplete(ISkill skill);

        IBuff CreateBuff(IBuffCfg cfg);
    }

    public abstract class SkillSystem : ISkillSystem
    {
        public abstract ISkill CreateSkill(ISkillCfg cfg);
        public abstract List<ISkill> GetActiveSkills();
        public abstract void AddSkill(ISkill skill);
        public abstract void RemoveSkill(ISkill skill);

        public ISkillCfg GetSkillCfg(int id)
        {
            throw new NotImplementedException();
        }

        public void OnSkillCreate(ISkill skill)
        {
            var skillCfg = skill.GetSkillCfg();
            var fxAndTfNames = skillCfg.GetEffectAndTransformNames();
            if (null != fxAndTfNames)
            {

            }
        }

        public void OnSkillUpdate(ISkill skill)
        {
//            Physics.ClosestPoint()
        }

        public void OnSkillComplete(ISkill skill)
        {

        }

        public abstract IBuff CreateBuff(IBuffCfg cfg);
    }

    public interface ICastSystem
    {
        ICastCfg GetDefaultCastCfg(ISkillCfg skillCfg);
    }

    /// <summary>
    /// 转换ui或者游戏中的input为技能理解的输入
    /// </summary>
    public interface ICastInput
    {
        List<IActor> GetTargets();
        void SetTargets(List<IActor> targets);
    }

    public interface ICaster
    {
        /// <summary>
        /// cast with default cast config
        /// </summary>
        /// <param name="skillCfg"></param>
        /// <returns></returns>
        bool Cast(ISkillCfg skillCfg);

        /// <summary>
        /// cast with specific cast config
        /// </summary>
        /// <param name="skillCfg"></param>
        /// <param name="castCfg"></param>
        /// <returns></returns>
        bool Cast(ISkillCfg skillCfg, ICastCfg castCfg);

        /// <summary>
        /// active skill or not
        /// active then player may adjust cast config
        /// </summary>
        /// <param name="skillCfg"></param>
        /// <param name="active"></param>
        void SetSkillActive(ISkillCfg skillCfg, bool active);

        List<ICastCfg> GetActiveCastCfgs();

        void UpdateCastConfig(ICastInput input);

        List<ISkill> GetActiveSkills();

        ISkill GetActiveSkill();

        /// <summary>
        /// 施法者本身的actor
        /// </summary>
        /// <returns></returns>
        IActor GetActor();

        CastMsg BuffAllowCast(ISkill skill);

        CastMsg ConsumeEnough(ISkill skill);

        CastMsg CdEnough(ISkill skill);

        Transform GetCastTransform(string name);
    }

    public enum SkillLifeCycle
    {

    }

    /// <summary>
    /// 释放技能的配置
    /// 位置，方向，目标
    /// 默认配置 + 运行时玩家修改的配置
    /// </summary>
    public interface ICastCfg
    {
        List<IActor> GetTargetActors();
        List<IActor> SetTargetActors(List<IActor> targets);

        Vector3Int GetDirection();
        void SetDirection(Vector3Int direction);

        Vector3Int GetTargetPosition();
        void SetTargetPosition(Vector3Int position);

        int Power();
        void SetPower(int power);

        List<int> ExtParams();
    }

    public interface ISkill
    {
        ICaster GetCaster();
        void SetCaster(ICaster caster);

        ICastCfg GetCastCfg();
        void SetCastCfg(ICastCfg castCfg);

        ISkillCfg GetSkillCfg();
        void SetSkillCfg(ISkillCfg skillCfg);

        bool AllowCastTo(IActor actor);

        void OnCatchTarget(IActor target);

        List<IActor> TryCollectTargets();

        void OnSkillLifeRecycle(SkillLifeCycle lifeCycle);

        void Apply();
    }

    public enum SkillCompleteBehaviour
    {
        UpdateValue,
        AddBuff,
        ClearBuff,
        Dead,
    }

    public interface ISkillCfg
    {
        int GetId();

        int GetDuration();

        List<KeyValuePair<ValueType, int>> GetConsumes();

        List<KeyValuePair<ValueType, int>> GetValueEffects();

        SkillAnimationCfg GetAnimation();

        List<KeyValuePair<string, string>> GetEffectAndTransformNames();

        int GetMaxTargetCnt();

        bool NeedDirection();

        bool NeedPosition();
    }

    public interface SkillAnimationCfg
    {

    }

    public class SkillConsume
    {

    }

    public class RoleEffectedByBuff
    {
        public static readonly uint Owner = Convert.ToUInt32("1", 2);
        public static readonly uint Friend_Hero = Convert.ToUInt32("10", 2);
        public static readonly uint Friend_Soldier = Convert.ToUInt32("100", 2);
        public static readonly uint Enemy = Convert.ToUInt32("1000", 2);
    }

    public interface IBuffCfg
    {
        int GetRange();
        void SetRange();
        uint EffectRole();
    }

    public interface IBuff
    {
        int GetId();

        IBuffCfg GetBuffCfg();

        /// <summary>
        /// buff拥有者是否可以释放技能
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        bool AllowCast(ISkill skill);
    }

    public class Filters
    {
    }

    public enum CastMsgType
    {
        ok,
        hp_low,
        mp_low,
        cd,
        buff_reject,
    }

    public class CastMsg
    {
        public static readonly CastMsg s_Suc = new CastMsg();

        public CastMsgType mMsgType = CastMsgType.ok;

        public object mAttach;

        public CastMsg()
        {
        }

        public CastMsg(CastMsgType type)
        {
            mMsgType = type;
        }

        public CastMsg(CastMsgType type, object attach)
        {
            mMsgType = type;
            mAttach = attach;
        }

        public bool Suc
        {
            get { return CastMsgType.ok == mMsgType; }
        }

        public bool Error
        {
            get { return !Suc; }
        }
    }

    

    
}