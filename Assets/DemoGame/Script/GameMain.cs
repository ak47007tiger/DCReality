using System.Collections;
using System.Collections.Generic;
using System.IO;
using DC.ActorSystem;
using DC.GameLogic;
using DC.DCResourceSystem;
using UnityEngine;

namespace DC
{
    public class GameMain : GameContextObject
    {
        private Transform mRootTf;
        public Transform RootTf
        {
            get
            {
                if (null == mRootTf)
                {
                    mRootTf = GameObject.Find("Root").transform;
                }

                return mRootTf;
            }
        }
        void Start()
        {
            //init all sys
            GetSkillSystem().Init();

            //load scene

            //create hero
            CreateDemoHeroFighter();

            CreateDemoEnemyFighter();
        }

        private void CreateDemoHeroFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = GetResourceSystem().Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Instantiate(heroPrefab, RootTf);
            hero.GetComponent<IActor>().SetIsPlayer(true);
            hero.GetComponent<HeroInput>().mHeroCfg = fighterCfg;
            hero.transform.position = new Vector3(1,0,0);
        }

        private void CreateDemoEnemyFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoEnemyFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = GetResourceSystem().Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Instantiate(heroPrefab, RootTf);
            hero.GetComponent<HeroInput>().mHeroCfg = fighterCfg;
            hero.transform.position = new Vector3(-1, 0, 0);
        }
    }
}
