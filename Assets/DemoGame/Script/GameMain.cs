using System.Collections;
using System.Collections.Generic;
using System.IO;
using DC.GameLogic;
using DC.ResourceSys;
using UnityEngine;

namespace DC
{
    public class GameMain : MonoBehaviour
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
            //load scene
            //create hero
            CreateDemoHeroFighter();

            CreateDemoEnemyFighter();
        }

        private void CreateDemoHeroFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoFighterCfg();
            var heroPrefab = ResourceSystem.Instance.Load<GameObject>(fighterCfg.mPrefabPath);
            var hero = Instantiate(heroPrefab, RootTf);
            hero.transform.position = new Vector3(1,0,0);
        }

        private void CreateDemoEnemyFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoEnemyFighterCfg();
            var heroPrefab = ResourceSystem.Instance.Load<GameObject>(fighterCfg.mPrefabPath);
            var hero = Instantiate(heroPrefab, RootTf);
            hero.transform.position = new Vector3(-1, 0, 0);
        }
    }
}
