using System.Collections.Generic;
using DC.DCResourceSystem;

namespace DC.GameLogic
{
    public class HeroConfigMgr : Singleton<HeroConfigMgr>
    {
        Dictionary<int, HeroCfg> mIdToHeroCfgs = new Dictionary<int, HeroCfg>();

        List<HeroCfg> mHeroCfgs = new List<HeroCfg>();

        public void Init()
        {
            var path = "Configs/Hero";
            mHeroCfgs.AddRange(ResourceSys.Instance.LoadAll<HeroCfg>(path));
        }
    }
}