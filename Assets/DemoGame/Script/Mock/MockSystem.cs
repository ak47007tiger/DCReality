using System.Collections;
using System.Collections.Generic;
using DC.GameLogic;
using UnityEngine;

namespace DC
{
    public interface IMockSystem
    {
        IHeroCfg NewFighterCfg();
    }

    public class MockSystem : Singleton<MockSystem>, IMockSystem
    {
        public IHeroCfg NewFighterCfg()
        {
            var fighterCfg = new HeroCfg();
            fighterCfg.SetPrefabPath("Actor/hero_demo_fighter");
            return fighterCfg;
        }
    }
}
