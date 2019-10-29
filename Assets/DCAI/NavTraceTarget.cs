﻿using System;
using System.Collections;
using System.Collections.Generic;
using DC.ActorSystem;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class NavTraceTarget : BaseMonoBehaviour
    {
        public Transform mTargetTf;

        public float mStopDistance = 1;

        public NavMeshAgent mNavMeshAgent;

        public IActor mTracingActor;

        private bool mStop;

        public Action<NavTraceTarget, float> mOnCatchTarget;

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;

            if (value)
            {
                mNavMeshAgent.destination = CacheTransform.position;
            }
            else
            {
                mNavMeshAgent.destination = mTargetTf.position;
            }
        }

        void Awake()
        {
            mNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if(mStop) return;

            if (mTargetTf == null) return;

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
            mNavMeshAgent.destination = mTargetTf.position;
        }

        public void StartTrace(Transform targetTf, float stopDistance)
        {
            mTargetTf = targetTf;
            mStopDistance = stopDistance;
            mStop = false;
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
