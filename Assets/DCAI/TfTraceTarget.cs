using System;
using UnityEngine;

namespace DC.AI
{
    public class TfTraceTarget : TfBase
    {
        public float mStopDistance = 1;

        public Action<TfTraceTarget, float> mOnCatchTarget;

        public Transform mTargetTf;

        void Awake()
        {
        }

        void Update()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

            var catchTarget = IsCatchTargetWithPos(mTargetTf.position, CacheTransform.position, mStopDistance);
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

            CacheTransform.position = ComputeNextPositionWithTf(CacheTransform, mTargetTf, mSpeed);
        }

        public void StartTrace(Transform targetTf, float stopDistance, float speed)
        {
            mTargetTf = targetTf;
            mStopDistance = stopDistance;
            mSpeed = speed;
            SetStop(true);
        }

        public static Tuple<bool, float> IsCatchTargetWithPos(Vector3 targetPos, Vector3 curPos, float stopDistance)
        {
            var distance = Vector3.Distance(targetPos, curPos);
            return new Tuple<bool, float>(distance < stopDistance, distance);
        }

        public static Vector3 ComputeNextPositionWithTf(Transform traceTf, Transform targetTf, float speed)
        {
            return ComputeNextPositionWithPos(traceTf.position, targetTf.position, speed);
        }

        public static Vector3 ComputeNextPositionWithPos(Vector3 curPos, Vector3 targetPos, float speed)
        {
            var dir = (targetPos - curPos).normalized;
            var delta = speed * dir * Time.deltaTime;
            return curPos + delta;
        }

    }
}
