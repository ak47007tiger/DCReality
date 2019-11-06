using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class NavBase : BaseMonoBehaviour
    {
        public float mStopDistance = 1;

        [HideInInspector]
        public float mSpeed;

        public NavMeshAgent mNavMeshAgent;

        public bool StopAfterCatchTarget = true;

        protected bool mStop;

        protected virtual void Awake()
        {
            mNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;

//            if (value)
//            {
//                mNavMeshAgent.destination = CacheTransform.position;
//            }
//            else
//            {
//                mNavMeshAgent.destination = GetTargetPos();
//            }

            mNavMeshAgent.isStopped = value;
        }

        public virtual Vector3 GetTargetPos()
        {
            return CacheTransform.position;
        }

    }

}