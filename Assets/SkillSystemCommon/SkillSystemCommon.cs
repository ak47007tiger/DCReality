using System.Collections;
using System.Collections.Generic;
using SkillSystem.ValueSystem;
using UnityEngine;

namespace SkillSystem
{
    /*
     技能
     装备
     角色

     buff

     数值计算

     end
     */

    public interface ITestCase
    {
        void NormalAttack();
        void NormalAttackTarget();

        void CastToActor();
        void CastToDirection();
        void CastToPosition();

        //compose
        void CastComposeSkill();

        //buff
        void CastToActorWithSelfBuff();
        void CastToActorWithOppositeBuff();
    }

    public class TestCaseImpl : ITestCase, ISystemProvider
    {
        public void NormalAttack()
        {
            IActor actor = null;
            ICaster caster = null;
            ISkillCfg normalAttackCfg = null;
            //get from user input
            ICastCfg castCfg = null;

            var targets = GetTargetSystem().GetTargets(actor, caster, normalAttackCfg);
            castCfg.SetTargetActors(targets);

            caster.Cast(normalAttackCfg, castCfg);
        }

        public void NormalAttackTarget()
        {
            ICaster caster = null;
            ISkillCfg normalAttackCfg = null;
            //get from user input
            ICastCfg castCfg = null;

            //get from user input
            List<IActor> targets = null;
            castCfg.SetTargetActors(targets);

            caster.Cast(normalAttackCfg, castCfg);
        }

        public void CastToActor()
        {
            ICaster caster = null;
            ISkillCfg skillCfg = null;
            //get from user input
            ICastCfg castCfg = null;

            //get from user input
            List<IActor> targets = null;
            castCfg.SetTargetActors(targets);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastToDirection()
        {
            ICaster caster = null;
            ISkillCfg skillCfg = null;
            //get from user input
            ICastCfg castCfg = null;

            //get from user input
            var direction = Vector3Int.zero;
            castCfg.SetDirection(direction);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastToPosition()
        {
            ICaster caster = null;
            ISkillCfg skillCfg = null;
            //get from user input
            ICastCfg castCfg = null;

            //get from user input
            var position = Vector3Int.zero;
            castCfg.SetDirection(position);

            caster.Cast(skillCfg, castCfg);
        }

        public void CastComposeSkill()
        {
            throw new System.NotImplementedException();
        }

        public void CastToActorWithSelfBuff()
        {
            throw new System.NotImplementedException();
        }

        public void CastToActorWithOppositeBuff()
        {
            throw new System.NotImplementedException();
        }

        public IActorSystem GetActorSystem()
        {
            throw new System.NotImplementedException();
        }

        public ITargetSystem GetTargetSystem()
        {
            throw new System.NotImplementedException();
        }

        public ISkillSystem GetSkillSystem()
        {
            throw new System.NotImplementedException();
        }

        public ICastSystem GetCastSystem()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IActorSystem
    {

    }

    public interface ITargetSystem
    {
        List<IActor> GetTargets(IActor actor, ICaster caster, ISkillCfg skillCfg);
    }

    public interface ISkillSystem
    {
        ISkill CreateSkill(ISkillCfg cfg);
    }

    public interface ICastSystem
    {
        ICastCfg GetDefaultCastCfg(ISkillCfg skillCfg);
    }

    public interface IActor
    {
        List<IBuff> GetOwnerBuffs();
        void SetOwnerBuffs(List<IBuff> buffs);

        IValueComponent GetValueComponent();
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

        List<IBuff> GetOwnerBuffs();

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

        void Apply();
    }

    public interface ISkillCfg
    {
        int GetId();

        int GetDuration();

        List<KeyValuePair<ValueType, int>> GetConsumes();
    }

    public class SkillConsume
    {

    }

    public class BuffEffectRole
    {
        public const uint Owner = 0x0;
        public const uint Friend_Hero = 0x1;
        public const uint Friend_Soldier = 0x10;
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

    public interface ISystemProvider
    {
        IActorSystem GetActorSystem();
        ITargetSystem GetTargetSystem();
        ISkillSystem GetSkillSystem();
        ICastSystem GetCastSystem();
    }

    public class GameElement : ISystemProvider
    {
        public IActorSystem GetActorSystem()
        {
            throw new System.NotImplementedException();
        }

        public ITargetSystem GetTargetSystem()
        {
            throw new System.NotImplementedException();
        }

        public ISkillSystem GetSkillSystem()
        {
            throw new System.NotImplementedException();
        }

        public ICastSystem GetCastSystem()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {

        }
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

        public CastMsg() { }

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

    public class Filters
    {
        
    }

    public class GameActor : GameElement, IActor, ICaster
    {
        private List<ISkillCfg> mSkillCfgs;

        private Dictionary<ISkillCfg, ISkill> mCfgToSkill;

        public void Attack()
        {
            var targetSys = GetTargetSystem();
            ISkillCfg normalAttackSkillCfg = null;
            var targets = targetSys.GetTargets(this, this, normalAttackSkillCfg);
            Attack(targets);
        }

        public void Attack(List<IActor> targets)
        {
            ISkillCfg normalAttackSkillCfg = null;
            ICastCfg normalAttackCastCfg = null;

            Cast(normalAttackSkillCfg, normalAttackCastCfg);
        }

        public CastMsg ConsumeEnough(ISkill skill)
        {
            var values = GetValueComponent();
            var mp = values.GetValue(ValueType.mp);
            var consumes = skill.GetSkillCfg().GetConsumes();

            throw new System.NotImplementedException();
        }

        public CastMsg CdEnough(ISkill skill)
        {
            throw new System.NotImplementedException();
        }

        public bool Cast(ISkillCfg skillCfg)
        {
            var castCfg = GetCastSystem().GetDefaultCastCfg(skillCfg);
            return Cast(skillCfg, castCfg);
        }

        public bool Cast(ISkillCfg skillCfg, ICastCfg castCfg)
        {
            var skill = GetSkillSystem().CreateSkill(skillCfg);
            skill.SetCaster(this);
            skill.SetCastCfg(castCfg);

            /*
             判断是否可以释放
             buff
                 沉默
                 晕眩
             能量
             cd
             */

            var buffAllowCast = BuffAllowCast(skill);
            if (buffAllowCast.Error)
            {
                return false;
            }

            var consumeEnough = ConsumeEnough(skill);
            if (consumeEnough.Error)
            {
                return false;
            }

            if (CdEnough(skill).Error)
            {
                return false;
            }

            skill.Apply();
            return true;
        }

        public void SetSkillActive(ISkillCfg skillCfg, bool active)
        {
            throw new System.NotImplementedException();
        }

        public List<ICastCfg> GetActiveCastCfgs()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCastConfig(ICastInput input)
        {
            throw new System.NotImplementedException();
        }

        public IActor GetActor()
        {
            throw new System.NotImplementedException();
        }

        public CastMsg BuffAllowCast(ISkill skill)
        {
            var ownerBuffs = GetOwnerBuffs();
            var rejectBuff = ownerBuffs.Find((buff => !buff.AllowCast(skill)));
            if (null == rejectBuff)
            {
                return CastMsg.s_Suc;
            }
            return new CastMsg(CastMsgType.buff_reject, rejectBuff);
        }

        public List<IBuff> GetOwnerBuffs()
        {
            throw new System.NotImplementedException();
        }

        public List<ISkill> GetActiveSkills()
        {
            throw new System.NotImplementedException();
        }

        public ISkill GetActiveSkill()
        {
            throw new System.NotImplementedException();
        }

        public void SetOwnerBuffs(List<IBuff> buffs)
        {
            throw new System.NotImplementedException();
        }

        public IValueComponent GetValueComponent()
        {
            throw new System.NotImplementedException();
        }
    }
}
