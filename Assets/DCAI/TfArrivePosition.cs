using System;
using UnityEngine;

namespace DC.AI
{
    public class TfArrivePosition : TfBase
    {
        public float mStopDistance = 1;

        public Action<TfArrivePosition, float> mOnCatchTarget;

        public Vector3 mTargetPos;

        public void StartTrace(Vector3 targetPos, float stopDistance, float speed)
        {
            mTargetPos = targetPos;
            mStopDistance = stopDistance;
            mSpeed = speed;
            SetStop(false);
        }

        void Update()
        {
            if (mStop) return;

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
                return;
            }

            CacheTransform.position = TfTraceTarget.ComputeNextPositionWithPos(CacheTransform.position, mTargetPos, mSpeed);
        }

        public bool IsComplete()
        {
            var catchTarget = TfTraceTarget.IsCatchTargetWithPos(mTargetPos, CacheTransform.position, mStopDistance);
            return catchTarget.Item1;
        }
    }
}