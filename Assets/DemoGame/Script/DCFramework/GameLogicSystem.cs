using DC.ActorSystem;
using DC.DCEffectSys;
using DC.DCPhysics;
using DC.DCResourceSystem;
using DC.SkillSystem;

namespace DC.GameLogic
{
    public interface ISystemProvider
    {
        ActorSys GetActorSystem();
        TargetSys GetTargetSystem();

        ISkillSystem GetSkillSystem();
        ICastSystem GetCastSystem();

        IPhysicSystem GetPhysicSystem();

        IEffectSystem GetEffectSystem();

        IResourceSystem GetResourceSystem();
    }

    public class GameContextObject : BaseMonoBehaviour, ISystemProvider
    {
        public ActorSys GetActorSystem()
        {
            return ActorSys.Instance;
        }

        public TargetSys GetTargetSystem()
        {
            return TargetSys.Instance;
        }

        public ISkillSystem GetSkillSystem()
        {
            return SkillSys.Instance;
        }

        public ICastSystem GetCastSystem()
        {
            return CastSys.Instance;
        }

        public IPhysicSystem GetPhysicSystem()
        {
            return PhysicSys.Instance;
        }

        public IEffectSystem GetEffectSystem()
        {
            return EffectSys.Instance;
        }

        public IResourceSystem GetResourceSystem()
        {
            return ResourceSys.Instance;
        }
    }

    public class GameElement : GameContextObject
    {
        private CacheItem<GameActor> mCacheActor;
        private CacheItem<Caster> mCacheCaster;

        protected virtual void Awake()
        {
            mCacheActor = new CacheItem<GameActor>(GetComponent<GameActor>);
            mCacheCaster = new CacheItem<Caster>(GetComponent<Caster>);
        }

        public GameActor Actor
        {
            get
            {
                return mCacheActor.Value;
            }
        }

        public Caster Caster
        {
            get
            {
                return mCacheCaster.Value;
            }
        }
    }
}
