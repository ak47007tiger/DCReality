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
        int GetActorId();
        void SetActorId(int actorId);

        #region skill system

        ICaster GetCaster();
        
        BuffCmpt GetBuffCmpt();

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

        bool IsPlayer();

        ActorSide GetActorSide();
        void SetActorSide(ActorSide side);

        RoleType GetRoleType();
        void SetRoleType(RoleType type);

        Transform GetActorPos(ActorPos pos);

        Transform GetTransform();

        HeroCfg GetHeroCfg();
        void SetHeroCfg(HeroCfg cfg);

        void FaceTo(Transform targetTf);
        void FaceTo(Vector3 direction);
    }
    
    public class ActorData
    {
        public int mLevel;

    }

}
