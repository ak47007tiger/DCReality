using System;
using DC.ActorSystem;
using UnityEngine;

namespace DC.AI
{
    public class TfArrivePosition : BaseMonoBehaviour
    {
        public float mStopDistance = 1;

        private bool mStop;

        public Action<TfArrivePosition, float> mOnCatchTarget;

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

            CacheTransform.position = TfTraceTarget.ComputeNextPosition(CacheTransform.position, mTargetPos, mSpeed);
        }

    }
}