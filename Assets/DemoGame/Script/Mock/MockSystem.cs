using System.Collections;
using System.Collections.Generic;
using DC.GameLogic;
using UnityEngine;

namespace DC
{
    public interface IMockSystem
    {
        /// <summary>
        /// 战士
        /// </summary>
        /// <returns></returns>
        HeroCfg DemoFighterCfg();
    }

    public class MockSystem : Singleton<MockSystem>, IMockSystem
    {
        public HeroCfg DemoFighterCfg()
        {
            var fighterCfg = new HeroCfg();
            fighterCfg.mPrefabPath = "Actor/hero_demo_fighter";
            return fighterCfg;
        }
    }
}
