using System;
using System.Collections.Generic;
using DC.AI;
using DC.GameLogic;
using DC.ValueSys;
using DC.SkillSystem;
using UnityEngine;

namespace DC.ActorSystem
{
    public interface IActor
    {
        #region skill system

        ICaster GetCaster();
        
        BuffCmpnt GetBuffCmpnt();

        ValueComponent GetValueComponent();

        #endregion

        void SetModel(string model);
        void UpdateModel();

        void SetVisibility(bool show);
        void Destroy();

        void UpdateAnimator(int animatorId);
        void UpdateAnimatorParam(int paramId, int value);
        void UpdateAnimatorParam(int paramId, float value);
        void UpdateAnimatorParam(int paramId, bool value);

        void TryCatch(Transform actor, float stopDistance, Action<NavTraceTarget, float> onCatch);
        void TryArrive(Vector3 targetPos, float stopDistance, Action<NavArrivePosition, float> onArrive);

        bool IsPlayer();
        void SetIsPlayer(bool player);

        ActorSide GetActorSide();
        void SetActorSide(ActorSide side);

        RoleType GetRoleType();
        void SetRoleType(RoleType type);

        Transform GetActorPos(ActorPos pos);

        Transform GetTransform();

        HeroCfg GetHeroCfg();
        void SetHeroCfg(HeroCfg cfg);

        bool IsAutoMoving();
        void StopAutoMove();

        void FaceTo(Transform targetTf);
        void FaceTo(Vector3 direction);
    }
    
    public class ActorData
    {
        public int mHp;
        public int mHpRecover;
        public int mMp;
        public int mMpRecover;

        public int mPhysicAttack;
        public int mMagicAttack;

        public int mPhysicDefense;
        public int mMagicDefense;

        //穿透
        public int mPhysicWeaken;
        public int mMagicWeaken;

        public int mAttackSpeed;
        public int mMoveSpeed;
        /// <summary>
        /// 暴击
        /// </summary>
        public int mCritRate;
    }

}
