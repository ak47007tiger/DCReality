using DC.ActorSystem;
using DC.EffectSys;
using DC.DCPhysics;
using DC.ResourceSys;
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
            throw new System.NotImplementedException();
        }

        public ITargetSystem GetTargetSystem()
        {
            throw new System.NotImplementedException();
        }

        public ISkillSystem GetSkillSystem()
        {
            throw new System.NotImplementedException();
        }

        public ICastSystem GetCastSystem()
        {
            throw new System.NotImplementedException();
        }

        public IPhysicSystem GetPhysicSystem()
        {
            throw new System.NotImplementedException();
        }

        public IEffectSystem GetEffectSystem()
        {
            throw new System.NotImplementedException();
        }

        public IResourceSystem GetResourceSystem()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SystemProvider : Singleton<SystemProvider>, ISystemProvider
    {
        public IActorSystem GetActorSystem()
        {
            throw new System.NotImplementedException();
        }

        public ITargetSystem GetTargetSystem()
        {
            throw new System.NotImplementedException();
        }

        public ISkillSystem GetSkillSystem()
        {
            throw new System.NotImplementedException();
        }

        public ICastSystem GetCastSystem()
        {
            throw new System.NotImplementedException();
        }

        public IPhysicSystem GetPhysicSystem()
        {
            throw new System.NotImplementedException();
        }

        public IEffectSystem GetEffectSystem()
        {
            throw new System.NotImplementedException();
        }

        public IResourceSystem GetResourceSystem()
        {
            return ResourceSystem.Instance;
        }
    }

    public class GameElement : GameContextObject
    {

    }
}
