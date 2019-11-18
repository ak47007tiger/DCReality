using System.Collections.Generic;
using DC.Collections.Generic;
using DC.DCResourceSystem;

namespace DC.GameLogic
{
    public class HeroConfigMgr : Singleton<HeroConfigMgr>
    {
        Dictionary<int, HeroCfg> mIdToHeroCfg = new Dictionary<int, HeroCfg>();

        List<HeroCfg> mHeroCfgs = new List<HeroCfg>();

        public void Init()
        {
            var path = "Configs/Hero";
            mHeroCfgs.AddRange(ResourceSys.Instance.LoadAll<HeroCfg>(path));
        }

        public HeroCfg GetHeroCfg(int id)
        {
            return mIdToHeroCfg.GetValEx(id);
        }
    }
}