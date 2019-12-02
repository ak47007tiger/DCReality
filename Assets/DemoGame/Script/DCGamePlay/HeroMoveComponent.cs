using System;
using DC.ActorSystem;
using DC.AI;
using DC.DCResourceSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    /*
        nav移动导致 transform 移动不好用，因为限制了位置
        需要在使用 transform 移动时停止nav agent
     */

    public enum MoveType
    {
        Idle,
        NavPos,
        NavTarget,
        TfPos,
        TfTarget,
    }

    public class HeroMoveComponent : GameElement
    {
        public NavArrivePosition mNavArrivePos;
        public TfArrivePosition mTfArrivePos;

        public NavTraceTarget mNavTraceTarget;
        public TfTraceTarget mTfTraceTarget;

        public NavMeshAgent mNavAgent;

        public DCFSM mFsm;

        public MoveType mMoveType;
        public Transform mTargetTf;
        public Vector3 mTargetPos;
        public float mStopDistance;

        public HeroEntity mHero;

        public void StopNav()
        {
            mNavArrivePos.SetStop(true);
            mNavTraceTarget.SetStop(true);
        }

        public void StopTf()
        {
            mTfArrivePos.SetStop(true);
            mTfTraceTarget.SetStop(true);
        }

        public void StopTrace()
        {
            mNavTraceTarget.SetStop(true);
            mTfTraceTarget.SetStop(true);
        }

        public void StopPos()
        {
            mNavArrivePos.SetStop(true);
            mTfArrivePos.SetStop(true);
        }

        public void SetPosListener(Action<NavArrivePosition, float> onCatchTarget)
        {
            mNavArrivePos.mOnCatchTarget = onCatchTarget;
        }

        public void SetTargetListener(Action<NavTraceTarget, float> onCatchTarget)
        {
            mNavTraceTarget.mOnCatchTarget = onCatchTarget;
        }

        public void Move(MoveType type)
        {
            mMoveType = type;

            mFsm.CurrentState.Reason();
            mFsm.CurrentState.Act();
        }

        public void Move(MoveType type, Transform target, float speed, float stopDistance)
        {
            mMoveType = type;
            mTargetTf = target;
            mStopDistance = stopDistance;

            mFsm.CurrentState.Reason();
            mFsm.CurrentState.Act();
        }

        public void Move(MoveType type, Vector3 pos, float speed, float stopDistance)
        {
            mMoveType = type;
            mTargetPos = pos;
            mStopDistance = stopDistance;

            mFsm.CurrentState.Reason();
            mFsm.CurrentState.Act();
        }

        public void SetSpeed(float speed)
        {
            mNavAgent.speed = speed;
        }

        protected override void Awake()
        {
            base.Awake();
            mHero = gameObject.GetComponent<HeroEntity>();

            mNavArrivePos = gameObject.GetOrAdd<NavArrivePosition>();
            mNavTraceTarget = gameObject.GetOrAdd<NavTraceTarget>();
            mTfArrivePos = gameObject.GetOrAdd<TfArrivePosition>();
            mTfTraceTarget = gameObject.GetOrAdd<TfTraceTarget>();
            mNavAgent = gameObject.GetComponent<NavMeshAgent>();

            var path = string.Format("Configs/fsm/{0}", "HeroMove");
            var jsonStr = ResourceSys.Instance.Load<TextAsset>(path).text;
            mFsm = DCAnimatorToFSM.Instance.Convert(jsonStr, CreateDCFSMState);
        }

        void Start()
        {
        }

        private void FixedUpdate()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(mousePosition);
            //nav move pos
            if (Physics.Raycast(ray, out var hit, 10, SystemPreset.layer_ground))
            {
            }

            mFsm.CurrentState.Reason();
            mFsm.CurrentState.Act();
        }

        void OnClickEnvGround(Vector3 pos)
        {
            if (Actor.IsAutoMoving())
            {
                Actor.StopAutoMove();
            }

            mNavArrivePos.StartTrace(pos, SystemPreset.move_stop_distance);
        }

        public DCFSMState CreateDCFSMState(int state)
        {
            var enumState = (MoveState) state;
            var type = Type.GetType(string.Format("DC.AI.{0}", enumState.ToString()));
            var instance = (MoveBaseState)Activator.CreateInstance(type);
            instance.MoveCmpt = this;
            //todo d.c set up entity
            return instance;
        }

    }

}
