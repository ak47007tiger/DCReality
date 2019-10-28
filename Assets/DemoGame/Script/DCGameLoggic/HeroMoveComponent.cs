using DC.ActorSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    public class HeroMoveComponent : GameElement
    {
        public NavMeshAgent mNavMeshAgent;

        protected override void Awake()
        {
            base.Awake();

            mNavMeshAgent = GetComponent<NavMeshAgent>();

            MsgSys.Instance.Add<Vector3>(GameEvent.ClickEnvGround, OnClickEnvGround);
        }

        void Start()
        {
            mNavMeshAgent.speed = Actor.GetHeroCfg().mSpeed;
        }

        void OnClickEnvGround(Vector3 pos)
        {
            if (null == Actor || !Actor.IsPlayer())
            {
                return;
            }

            if (Actor.IsAutoMoving())
            {
                Actor.StopAutoMove();
            }

            mNavMeshAgent.SetDestination(pos);
        }
    }
}