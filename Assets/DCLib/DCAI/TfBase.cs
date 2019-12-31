using System;
using UnityEngine;
using UnityEngine.AI;

namespace DC.AI
{
    public class TfBase : BaseMonoBehaviour
    {
        [HideInInspector]
        public float mSpeed;

        protected bool mStop;

        public bool StopAfterCatchTarget = true;

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;

            if (!value && this.TryGetComponent<NavMeshAgent>(out var agent))
            {
                agent.enabled = false;
            }
        }
    }
}