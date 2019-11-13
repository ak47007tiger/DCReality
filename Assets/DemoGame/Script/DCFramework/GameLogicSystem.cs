using DC.ActorSystem;
using DC.DCEffectSys;
using DC.DCPhysics;
using DC.DCResourceSystem;
using DC.SkillSystem;

namespace DC.GameLogic
{
    public interface ISystemProvider
    {
        IActorSystem GetActorSystem();
        ITargetSystem GetTargetSystem();

        ISkillSystem GetSkillSystem();
        ICastSystem GetCastSystem();

        IPhysicSystem GetPhysicSystem();

        IEffectSystem GetEffectSystem();

        IResourceSystem GetResourceSystem();
    }

    public class GameContextObject : BaseMonoBehaviour, ISystemProvider
    {
        public IActorSystem GetActorSystem()
        {
            return ActorSys.Instance;
        }

        public ITargetSystem GetTargetSystem()
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

    public class SystemProvider : Singleton<SystemProvider>, ISystemProvider
    {
        public IActorSystem GetActorSystem()
        {
            return ActorSys.Instance;
        }

        public ITargetSystem GetTargetSystem()
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
        private CacheItem<IActor> mCacheActor;
        private CacheItem<ICaster> mCacheCaster;

        protected virtual void Awake()
        {
            mCacheActor = new CacheItem<IActor>(GetComponent<IActor>);
            mCacheCaster = new CacheItem<ICaster>(GetComponent<ICaster>);
        }

        public IActor Actor
        {
            get
            {
                return mCacheActor.Value;
            }
        }

        public ICaster Caster
        {
            get
            {
                return mCacheCaster.Value;
            }
        }
    }
}
