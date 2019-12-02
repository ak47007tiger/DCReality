using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.AI;
using DC.Collections.Generic;
using DC.SkillSystem;
using DC.ValueSys;
using UnityEngine.AI;

namespace DC.GameLogic
{
    public class GameActor : GameElement, IActor
    {
        private List<SkillCfg> mSkillCfgs;

        private Dictionary<SkillCfg, ISkill> mCfgToSkill;

        private int mActorId;

        private ValueComponent mValueComponent = new ValueComponent();

        private string mModelPath;

        private Dictionary<ActorPos, Transform> mPosToTf = new Dictionary<ActorPos, Transform>();

        private GameObject mModelGo;

        private ActorSide mActorSide;

        private RoleType mRoleType;

        private HeroCfg mHeroCfg;

        private NavTraceTarget mNavTraceTarget;

        private NavArrivePosition mNavArrivePosition;

        private BuffCmpt _mBuffCmpt = new BuffCmpt();

        public NavArrivePosition NavArrivePosition
        {
            get { return mNavArrivePosition; }
        }

        public NavTraceTarget NavTraceTarget
        {
            get { return mNavTraceTarget; }
        }

        protected override void Awake()
        {
            base.Awake();
            mNavArrivePosition = gameObject.GetOrAdd<NavArrivePosition>();
        }

        void Start()
        {
            mValueComponent.OnValueChange = OnComponentValueChange;
        }

        void OnComponentValueChange(GValueType type, int old, int cur)
        {
            if (type == GValueType.move_speed)
            {
                gameObject.GetOrAdd<NavArrivePosition>().mNavMeshAgent.speed = cur;
            }
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

        public int GetActorId()
        {
            return mActorId;
        }

        public void SetActorId(int actorId)
        {
            mActorId = actorId;
        }

        public ICaster GetCaster()
        {
            return Caster;
        }

        public BuffCmpt GetBuffCmpt()
        {
            return _mBuffCmpt;
        }

        public ValueComponent GetValueComponent()
        {
            return mValueComponent;
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

        public void TryCatch(Transform actor, float stopDistance, Action<NavTraceTarget, float> onCatch)
        {
            if (mNavTraceTarget == null)
            {
                mNavTraceTarget = gameObject.AddComponent<NavTraceTarget>();
            }
            mNavTraceTarget.mOnCatchTarget = onCatch;
            mNavTraceTarget.StartTrace(actor, stopDistance);
        }

        public void TryArrive(Vector3 targetPos, float stopDistance, Action<NavArrivePosition, float> onArrive)
        {
            mNavArrivePosition.mOnCatchTarget = onArrive;
            mNavArrivePosition.StartTrace(targetPos, stopDistance);
        }

        public bool IsPlayer()
        {
            return ActorSys.Instance.GetMainActor() == this;
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
            return mPosToTf.GetValEx(pos);
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
            if (null != mNavTraceTarget)
            {
                return !mNavTraceTarget.IsStop();
            }

            if (null != mNavArrivePosition)
            {
                return !mNavArrivePosition.IsStop();
            }

            return false;
        }

        public void StopAutoMove()
        {
            if (null != mNavTraceTarget)
            {
                mNavTraceTarget.SetStop(true);
                mNavTraceTarget.mOnCatchTarget = null;
            }

            if (null != mNavArrivePosition)
            {
                mNavArrivePosition.SetStop(true);
                mNavArrivePosition.mOnCatchTarget = null;
            }
        }

        public void FaceTo(Transform targetTf)
        {
            FaceTo((targetTf.position - CacheTransform.position).normalized);
        }

        public void FaceTo(Vector3 direction)
        {
            CacheTransform.forward = direction;
        }
    }
}