using System;
using DC.ActorSystem;
using UnityEngine;

namespace DC.AI
{
    public class ArrivePosition : BaseMonoBehaviour
    {
        public float mStopDistance = 1;

        public IActor mTracingActor;

        private bool mStop;

        public Action<ArrivePosition, float> mOnCatchTarget;

        [HideInInspector]
        public float mSpeed;

        public bool StopAfterCatchTarget = true;

        public Vector3 mTargetPos;

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;
        }

        public void StartTrace(Vector3 targetPos, float stopDistance, float speed)
        {
            mTargetPos = targetPos;
            mStopDistance = stopDistance;
            mSpeed = speed;
            mStop = false;
        }

        void Update()
        {
            if (mStop) return;

            var catchTarget = TransformTraceTarget.CatchTarget(mTargetPos, CacheTransform.position, mStopDistance);
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

            CacheTransform.position = TransformTraceTarget.ComputeNextPosition(CacheTransform.position, mTargetPos, mSpeed);
        }

    }
}