using System;
using System.Collections.Generic;
using DC.ValueSys;
using DC.SkillSystem;
using UnityEngine;

namespace DC.ActorSystem
{
    public interface IActor
    {
        #region skill system

        ICaster GetCaster();

        List<Buff> GetOwnerBuffs();
        void SetOwnerBuffs(List<Buff> buffs);

        IValueComponent GetValueComponent();

        void AddBuff(Buff buff);

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

        void TryCatch(IActor actor, float stopDistance, Action<IActor, float> onCatch);
        void StopCatch();

        bool IsPlayer();
        void SetIsPlayer(bool player);
    }
}