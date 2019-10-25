using DC.ActorSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    public class HeroMoveComponent : BaseMonoBehaviour
    {
        public NavMeshAgent mMeshAgent;
        public IActor mActor;

        void Awake()
        {
            mMeshAgent = GetComponent<NavMeshAgent>();
            mActor = GetComponent<IActor>();

            MsgSys.Instance.Add<Vector3>(GameEvent.ClickEnvGround, OnClickEnvGround);
        }

        void OnClickEnvGround(Vector3 pos)
        {
            if (null == mActor || !mActor.IsPlayer())
            {
                return;
            }

            mMeshAgent.SetDestination(pos);
        }
    }
}