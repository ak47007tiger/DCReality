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
        public Transform mTargetTf;

        public float mStopDistance = 1;

        public IActor mTracingActor;

        private bool mStop;

        public Action<IActor, float> mOnCatchTarget;

        [HideInInspector]
        public float mSpeed;

        public bool StopAfterCatchTarget;

        public bool IsStop()
        {
            return mStop;
        }

        public void StopTrace()
        {
            mStop = true;
        }

        void Awake()
        {
        }

        void Update()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

            var catchTarget = CatchTarget();
            if (catchTarget.Item1)
            {
                if (StopAfterCatchTarget)
                {
                    StopTrace();
                }

                if (null != mOnCatchTarget)
                {
                    mOnCatchTarget(mTracingActor, catchTarget.Item2);
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

        public Tuple<bool,float> CatchTarget()
        {
            var distance = Vector3.Distance(mTargetTf.position, CacheTransform.position);
            return new Tuple<bool, float>(distance < mStopDistance, distance);
        }

        public static Vector3 ComputeNextPosition(Transform traceTf, Transform targetTf, float speed)
        {
            var dir = (targetTf.position - traceTf.position).normalized;
            var delta = speed * dir * Time.deltaTime;
            return traceTf.position + delta;
        }
    }
}
