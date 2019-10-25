using System.Collections;
using System.Collections.Generic;
using DC.GameLogic;
using DC.ResourceSys;
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
            return ResourceSystem.Instance.Load<HeroCfg>("Configs/Hero/demo_fighter");
        }

        public HeroCfg DemoEnemyFighterCfg()
        {
            return ResourceSystem.Instance.Load<HeroCfg>("Configs/Hero/demo_enemy_fighter");
        }
    }
}
