using System;
using System.Collections;
using System.Collections.Generic;
using DC.ActorSystem;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class TraceTarget : BaseMonoBehaviour
    {
        public Transform mTargetTf;

        public float mStopDistance = 1;

        public NavMeshAgent mNavMeshAgent;

        public IActor mTracingActor;

        private bool mStop;

        public Action<IActor, float> mOnCatchTarget;

        public bool IsStop()
        {
            return mStop;
        }

        public void StopTrace()
        {
            mStop = true;
            mNavMeshAgent.destination = CacheTransform.position;
        }

        void Awake()
        {
            mNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

            var catchTarget = CatchTarget();
            if (catchTarget.Item1)
            {
                StopTrace();

                if (null != mOnCatchTarget)
                {
                    mOnCatchTarget(mTracingActor, catchTarget.Item2);
                }
                return;
            }
            mNavMeshAgent.destination = mTargetTf.position;
        }

        public void StartTrace(Transform targetTf, float stopDistance)
        {
            mTargetTf = targetTf;
            mStopDistance = stopDistance;
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
