using System;
using DC.ActorSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class NavArrivePosition : BaseMonoBehaviour
    {
        public float mStopDistance = 1;

        public NavMeshAgent mNavMeshAgent;

        private bool mStop;

        public Action<NavArrivePosition, float> mOnCatchTarget;

        [HideInInspector]
        public float mSpeed;

        public bool StopAfterCatchTarget = true;

        public Vector3 mTargetPos;

        void Awake()
        {
            mNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;
        }

        public void StartTrace(Vector3 targetPos, float stopDistance)
        {
            mTargetPos = targetPos;
            mStopDistance = stopDistance;
            mStop = false;
        }

        void Update()
        {
            if (mStop) return;

            var catchTarget = TfTraceTarget.CatchTarget(mTargetPos, CacheTransform.position, mStopDistance);
            if (catchTarget.Item1)
            {
                if (StopAfterCatchTarget)
                {
                    SetStop(true);
                }

                if (null != mOnCatchTarget)
                {
                    mOnCatchTarget(this, catchTarget.Item2);
                }
                return;
            }
            mNavMeshAgent.destination = GetTargetPos();
        }

        private Vector3 GetTargetPos()
        {
            return mTargetPos;
        }
    }
}