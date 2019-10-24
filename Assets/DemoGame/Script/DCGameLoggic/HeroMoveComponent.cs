using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    public class HeroMoveComponent : BaseMonoBehaviour
    {
        public NavMeshAgent mMeshAgent;

        void Awake()
        {
            mMeshAgent = GetComponent<NavMeshAgent>();

            MsgSys.Instance.Add<Vector3>(GameEvent.Move, OnMoveEvt);
        }

        void OnMoveEvt(Vector3 pos)
        {
            Debug.Log("target: " + pos);
            mMeshAgent.SetDestination(pos);
        }
    }
}