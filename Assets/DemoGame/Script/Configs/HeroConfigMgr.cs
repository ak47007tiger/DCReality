using System.Collections.Generic;

namespace DC.GameLogic
{
    public class HeroConfigMgr : Singleton<HeroConfigMgr>
    {
        Dictionary<int, HeroCfg> mIdToHeroCfgs = new Dictionary<int, HeroCfg>();

        List<HeroCfg> mHeroCfgs = new List<HeroCfg>();

        public void Init()
        {

        }
    }
}