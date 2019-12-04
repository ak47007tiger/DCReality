using System;
using DC.ActorSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class NavArrivePosition : NavBase
    {
        public Action<NavArrivePosition, float> mOnCatchTarget;

        public Vector3 mTargetPos;

        public void StartTrace(Vector3 targetPos, float stopDistance)
        {
            mTargetPos = targetPos;
            mStopDistance = stopDistance;
            SetStop(false);
        }

        void FixedUpdate()
        {
            if (mStop) return;

            mNavMeshAgent.destination = GetTargetPos();

            var catchTarget = TfTraceTarget.IsCatchTargetWithPos(mTargetPos, CacheTransform.position, mStopDistance);

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
            }
        }

        public override Vector3 GetTargetPos()
        {
            return mTargetPos;
        }

        public bool IsComplete()
        {
            var catchTarget = TfTraceTarget.IsCatchTargetWithPos(mTargetPos, CacheTransform.position, mStopDistance);
            return catchTarget.Item1;
        }

    }

}
