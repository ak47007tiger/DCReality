using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DC.SkillSystem;
using DC.ValueSys;

/*
 和SkillSystem有耦合关系
 */

namespace DC.ActorSystem
{
    public interface IActorSystem
    {
        IActor CreateActor(int id);
        
    }

    public interface ITargetSystem
    {
        List<IActor> GetTargets(IActor actor, ICaster caster, SkillCfg skillCfg);
    }

    public enum RoleType
    {
        Hero,
        Soldier,
        Building,
    }

    public enum ActorSide
    {
        Neutral,
        Friend,
        Enemy,
    }

    public interface IActor
    {
        #region skill system

        ICaster GetCaster();

        List<IBuff> GetOwnerBuffs();
        void SetOwnerBuffs(List<IBuff> buffs);

        IValueComponent GetValueComponent();

        void AddBuff(IBuff buff);

        void Attack();

        void Attack(List<IActor> targets);

        #endregion

        GameObject GetModel();
        void SetVisibility(bool show);
        void Destroy();

        void UpdateAnimator(int animatorId);
        void UpdateAnimatorParam(int paramId, int value);
        void UpdateAnimatorParam(int paramId, float value);
        void UpdateAnimatorParam(int paramId, bool value);

        void TryCatch(IActor actor, float stopDistance, Action<IActor,float> onCatch);
        void StopCatch();
    }
}