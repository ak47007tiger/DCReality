using System;
using System.Collections;
using System.Collections.Generic;
using DC.ActorSystem;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class TransformTraceTarget : BaseMonoBehaviour
    {
        public float mStopDistance = 1;

        public IActor mTracingActor;

        private bool mStop;

        public Action<TransformTraceTarget, float> mOnCatchTarget;

        [HideInInspector]
        public float mSpeed;

        public bool StopAfterCatchTarget;

        public Transform mTargetTf;

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;
        }

        void Awake()
        {
        }

        void Update()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

            var catchTarget = CatchTarget(mTargetTf.position, CacheTransform.position, mStopDistance);
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

            CacheTransform.position = ComputeNextPosition(CacheTransform, mTargetTf, mSpeed);
        }

        public void StartTrace(Transform targetTf, float stopDistance, float speed)
        {
            mTargetTf = targetTf;
            mStopDistance = stopDistance;
            mSpeed = speed;
            mStop = false;
        }

        public static Tuple<bool, float> CatchTarget(Vector3 targetPos, Vector3 curPos, float stopDistance)
        {
            var distance = Vector3.Distance(targetPos, curPos);
            return new Tuple<bool, float>(distance< stopDistance, distance);
        }

        public static Vector3 ComputeNextPosition(Transform traceTf, Transform targetTf, float speed)
        {
            return ComputeNextPosition(traceTf.position, targetTf.position, speed);
        }

        public static Vector3 ComputeNextPosition(Vector3 curPos, Vector3 targetPos, float speed)
        {
            var dir = (targetPos - curPos).normalized;
            var delta = speed * dir * Time.deltaTime;
            return curPos + delta;
        }

    }
}
