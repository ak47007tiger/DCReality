using System;
using System.Collections;
using System.Collections.Generic;
using DC.ActorSystem;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class NavTraceTarget : NavBase
    {
        public Transform mTargetTf;

        public Action<NavTraceTarget, float> mOnCatchTarget;

        void FixedUpdate()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

            mNavMeshAgent.destination = GetTargetPos();

            var catchTarget = CatchTarget(mTargetTf, CacheTransform, mStopDistance);
            if (catchTarget.Item1)
            {
                SetStop(true);

                if (null != mOnCatchTarget)
                {
                    mOnCatchTarget(this, catchTarget.Item2);
                }
                return;
            }
        }

        public override Vector3 GetTargetPos()
        {
            if(mTargetTf == null)
            {
                return CacheTransform.position;
            }

            return mTargetTf.position;
        }

        public void StartTrace(Transform targetTf, float stopDistance)
        {
            mTargetTf = targetTf;
            mStopDistance = stopDistance;
            SetStop(false);
        }

        public bool IsComplete()
        {
            var catchTarget = CatchTarget(mTargetTf, CacheTransform, mStopDistance);
            return catchTarget.Item1;
        }

        public static Tuple<bool,float> CatchTarget(Transform targetTf, Transform curTf, float stopDistance)
        {
            var distance = Vector3.Distance(targetTf.position, curTf.position);
            return new Tuple<bool, float>(distance < stopDistance, distance);
        }

        public static Vector3 ComputeNextPosition(Transform traceTf, Transform targetTf, float speed)
        {
            var dir = (targetTf.position - traceTf.position).normalized;
            var delta = speed * dir * Time.deltaTime;
            return traceTf.position + delta;
        }
    }
}
