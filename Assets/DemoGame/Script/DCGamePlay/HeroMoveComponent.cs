using System;
using System.Collections.Generic;
using DC.ActorSystem;
using DC.AI;
using DC.Collections;
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

        private Dictionary<int, MoveState> mIntToState;

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
        }

        public void Move(MoveType type, Transform target, float speed, float stopDistance)
        {
            mMoveType = type;
            mTargetTf = target;
            mStopDistance = stopDistance;

            mNavTraceTarget.mSpeed = speed;
            mTfTraceTarget.mSpeed = speed;
        }

        public void Move(MoveType type, Vector3 pos, float speed, float stopDistance)
        {
            mMoveType = type;
            mTargetPos = pos;
            mStopDistance = stopDistance;

            mNavArrivePos.mSpeed = speed;
            mTfArrivePos.mSpeed = speed;
        }

        protected override void Awake()
        {
            base.Awake();
            mIntToState = ConfigToolkit.ConvertEnumToDic<MoveState>();

            mHero = gameObject.GetComponent<HeroEntity>();

            mNavArrivePos = gameObject.GetOrAdd<NavArrivePosition>();
            mNavTraceTarget = gameObject.GetOrAdd<NavTraceTarget>();
            mTfArrivePos = gameObject.GetOrAdd<TfArrivePosition>();
            mTfTraceTarget = gameObject.GetOrAdd<TfTraceTarget>();
            mNavAgent = gameObject.GetComponent<NavMeshAgent>();

            var path = string.Format("Configs/fsm/{0}", "HeroMoveFSMCfg");
            var jsonStr = ResourceSys.Instance.Load<TextAsset>(path).text;
            mFsm = DCAnimatorToFSM.Instance.Convert(jsonStr, CreateDCFSMState);
        }

        void Start()
        {
        }

        private void FixedUpdate()
        {
            var mousePosition = Input.mousePosition;
            var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(mousePosition);
            //nav move pos
            if (Physics.Raycast(ray, out var hit, 10, SystemPreset.layer_ground))
            {
                Move(MoveType.NavPos, hit.point, mHero.GetSpeed(), SystemPreset.move_stop_distance);
            }

            EmitFfs();
        }

        public void EmitFfs()
        {
            if (null != mFsm)
            {
                mFsm.CurrentState.Reason();
                mFsm.CurrentState.Act();
            }
        }


        public DCFSMState CreateDCFSMState(int state)
        {
            var enumState = mIntToState[state];
            var type = Type.GetType(string.Format("DC.AI.{0}", enumState.ToString()));
            var instance = (MoveBaseState)Activator.CreateInstance(type);
            instance.MoveCmpt = this;
            //todo d.c set up entity
            return instance;
        }

    }

}
