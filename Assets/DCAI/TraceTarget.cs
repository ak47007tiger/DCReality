using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace DC.AI
{
    public class TraceTarget : BaseMonoBehaviour
    {
        public Transform mTargetTf;

        public float mSpeed;

        public float mStopDistance = 1;

        void Update()
        {
            if (mTargetTf == null) return;

            if (CatchTarget())
            {
                return;
            }

            CacheTransform.position = ComputeNextPosition(CacheTransform, mTargetTf, mSpeed);
        }

        public void UpdateTraceInfo(Transform targetTf, float speed, float stopDistance)
        {
            mTargetTf = targetTf;
            mSpeed = speed;
            mStopDistance = stopDistance;
        }

        public bool CatchTarget()
        {
            return Vector3.Distance(mTargetTf.position, CacheTransform.position) <= mStopDistance;
        }

        public static Vector3 ComputeNextPosition(Transform traceTf, Transform targetTf, float speed)
        {
            var dir = (targetTf.position - traceTf.position).normalized;
            var delta = speed * dir * Time.deltaTime;
            return traceTf.position + delta;
        }
    }
}
