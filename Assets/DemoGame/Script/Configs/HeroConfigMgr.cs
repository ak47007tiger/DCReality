using System.Collections.Generic;
using DC.Collections;
using DC.Collections.Generic;
using DC.DCResourceSystem;

namespace DC.GameLogic
{
    public class HeroConfigMgr : Singleton<HeroConfigMgr>
    {
        Dictionary<int, HeroCfg> mIdToHeroCfg;

        List<HeroCfg> mHeroCfgs = new List<HeroCfg>();

        public HeroConfigMgr()
        {
            Init();
        }

        public void Init()
        {
            var configPath = SystemPreset.GetConfigPath<HeroCfg>();
            var heroCfgs = ResourceSys.Instance.LoadAll<HeroCfg>(configPath);
            for (var i = 0; i < heroCfgs.Length; i++)
            {
                heroCfgs[i].BuildDerivedData();
            }
            mHeroCfgs.AddRange(heroCfgs);
            mIdToHeroCfg = ConfigToolkit.ListToDictionary(heroCfgs, (v) => v.mId);
        }

        public HeroCfg GetHeroCfg(int id)
        {
            return mIdToHeroCfg.GetValEx(id);
        }

        public List<HeroCfg> GetAllHeroCfgs()
        {
            return mHeroCfgs;
        }
    }
}