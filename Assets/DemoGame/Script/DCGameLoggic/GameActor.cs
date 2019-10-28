using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.AI;
using DC.Collections.Generic;
using DC.SkillSystem;
using DC.ValueSys;

namespace DC.GameLogic
{
    public class GameActor : GameElement, IActor
    {
        private List<SkillCfg> mSkillCfgs;

        private Dictionary<SkillCfg, ISkill> mCfgToSkill;

        private bool mIsPlayer;

        private List<Buff> mBuffList = new List<Buff>();

        private ValueComponent mValueComponent = new ValueComponent();

        private string mModelPath;

        private Dictionary<ActorPos, Transform> mPosToTf = new Dictionary<ActorPos, Transform>();

        private GameObject mModelGo;

        private ActorSide mActorSide;

        private RoleType mRoleType;

        private HeroCfg mHeroCfg;

        private NavTraceTarget mNavTraceTarget;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Attack()
        {
            var targetSys = GetTargetSystem();
            SkillCfg normalAttackSkillCfg = null;
            var targets = targetSys.GetTargets(this, GetCaster(), normalAttackSkillCfg);
            Attack(targets);
        }

        public void Attack(List<IActor> targets)
        {
            SkillCfg normalAttackSkillCfg = null;
            CastCfg normalAttackCastCfg = null;

            GetCaster().Cast(normalAttackSkillCfg, normalAttackCastCfg);
        }

        public void SetModel(string model)
        {
            mModelPath = model;
        }

        public void UpdateModel()
        {
            if (string.IsNullOrEmpty(mModelPath))
            {
                return;
            }

            mPosToTf.Clear();
            if (null != mModelGo)
            {
                Destroy(mModelGo);
            }

            var modelPrefab = GetResourceSystem().Load<GameObject>(mModelPath);
            mModelGo = Instantiate(modelPrefab);

            var modelTf = mModelGo.transform;
            modelTf.SetParent(transform);
            modelTf.localPosition = modelPrefab.transform.localPosition;

            var posArray = Enum.GetValues(typeof(ActorPos));
            
            foreach (var pos in posArray)
            {
                var posEnum = (ActorPos)pos;
                var posTf = modelTf.Find(posEnum.ToString());
                mPosToTf.Add(posEnum, posTf);
            }
        }

        public ICaster GetCaster()
        {
            return Caster;
        }

        public List<Buff> GetOwnerBuffs()
        {
            return mBuffList;
        }

        public void SetOwnerBuffs(List<Buff> buffs)
        {
            mBuffList = buffs;
        }

        public IValueComponent GetValueComponent()
        {
            return mValueComponent;
        }

        public void AddBuff(Buff buff)
        {
            mBuffList.Add(buff);
        }

        public void SetVisibility(bool show)
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimator(int animatorId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, int value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, float value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimatorParam(int paramId, bool value)
        {
            throw new System.NotImplementedException();
        }

        public void TryCatch(IActor actor, float stopDistance, Action<IActor, float> onCatch)
        {
            if (mNavTraceTarget == null)
            {
                mNavTraceTarget = gameObject.AddComponent<NavTraceTarget>();
            }
            mNavTraceTarget.mTracingActor = actor;
            mNavTraceTarget.mOnCatchTarget = onCatch;
            mNavTraceTarget.StartTrace(actor.GetTransform(), stopDistance);
        }

        public void StopCatch()
        {
            throw new NotImplementedException();
        }

        public bool IsPlayer()
        {
            return mIsPlayer;
        }

        public void SetIsPlayer(bool player)
        {
            mIsPlayer = player;
        }

        public ActorSide GetActorSide()
        {
            return mActorSide;
        }

        public void SetActorSide(ActorSide side)
        {
            mActorSide = side;
        }

        public RoleType GetRoleType()
        {
            return mRoleType;
        }

        public void SetRoleType(RoleType type)
        {
            mRoleType = type;
        }

        public Transform GetActorPos(ActorPos pos)
        {
            return mPosToTf.GetVal(pos);
        }

        public Transform GetTransform()
        {
            return CacheTransform;
        }

        public HeroCfg GetHeroCfg()
        {
            return mHeroCfg;
        }

        public void SetHeroCfg(HeroCfg cfg)
        {
            mHeroCfg = cfg;
        }

        public bool IsAutoMoving()
        {
            if (null == mNavTraceTarget) return false;

            return !mNavTraceTarget.IsStop();
        }

        public void StopAutoMove()
        {
            if (null == mNavTraceTarget) return;

            mNavTraceTarget.StopTrace();
        }
    }
}