using System.Collections;
using System.Collections.Generic;
using System.IO;
using DC.ActorSystem;
using DC.GameLogic;
using DC.DCResourceSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
            //init all mono singleton
            //init all sys

            GetSkillSystem().Init();

            //load scene

            //create hero
            CreateDemoHeroFighter();

            CreateDemoEnemyFighter(new Vector3(-2, 0, 0));
            CreateDemoEnemyFighter(new Vector3(-4, 0, 0));
            CreateDemoEnemyFighter(new Vector3(-4, 0, 2));
            CreateDemoEnemyFighter(new Vector3(-2, 0, -2));
        }

        private void CreateDemoHeroFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = GetResourceSystem().Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Instantiate(heroPrefab, RootTf);

            var actor = hero.GetComponent<IActor>();
            actor.SetHeroCfg(fighterCfg);
            actor.SetIsPlayer(true);
            actor.SetActorSide(ActorSide.blue);
            actor.SetModel(fighterCfg.mModelPath);
            actor.UpdateModel();

            hero.GetComponent<HeroEntity>().mHeroCfg = fighterCfg;
            hero.transform.position = new Vector3(2,0,0);
        }

        private void CreateDemoEnemyFighter(Vector3 pos)
        {
            var fighterCfg = MockSystem.Instance.DemoEnemyFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = GetResourceSystem().Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Instantiate(heroPrefab, RootTf);

            var actor = hero.GetComponent<IActor>();
            actor.SetHeroCfg(fighterCfg);
            actor.SetModel(fighterCfg.mModelPath);
            actor.UpdateModel();

            hero.GetComponent<HeroEntity>().mHeroCfg = fighterCfg;
            hero.transform.position = pos;
        }
    }
}
