using System.Collections.Generic;
using DC.SkillSystem;

namespace DC.ActorSystem
{
    public interface ITargetSystem
    {
        List<IActor> GetTargets(IActor actor, ICaster caster, SkillCfg skillCfg);
    }


    public class TargetSys : Singleton<TargetSys>, ITargetSystem
    {
        public List<IActor> GetTargets(IActor actor, ICaster caster, SkillCfg skillCfg)
        {
            return null;
        }
    }
}