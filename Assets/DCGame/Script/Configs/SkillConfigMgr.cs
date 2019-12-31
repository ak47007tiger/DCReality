using System.Collections.Generic;
using DC.DCResourceSystem;
using DC.SkillSystem;

namespace DC.GameLogic
{
    public class SkillConfigMgr : Singleton<SkillConfigMgr>
    {
        Dictionary<int, SkillCfg> mIdToSkillCfg = new Dictionary<int, SkillCfg>();

        public SkillConfigMgr()
        {
            var skillCfgs = ResourceSys.Instance.LoadAll<SkillCfg>(SystemPreset.GetConfigPath<SkillCfg>());
            foreach (var skillCfg in skillCfgs)
            {
                skillCfg.OnCreate();
                mIdToSkillCfg.Add(skillCfg.mId, skillCfg);
            }
        }

        public SkillCfg GetSkillCfg(int skillId)
        {
            if (mIdToSkillCfg.TryGetValue(skillId, out var cfg))
            {
                return cfg;
            }

            return null;
        }
    }
}