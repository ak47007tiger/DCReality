using System.Collections;
using System.Collections.Generic;
using DC.GameLogic;
using UnityEngine;

namespace DC
{
    public class GameMain : MonoBehaviour
    {
        void Start()
        {
            //init all sys
            //load scene
            //create hero
            var fighterCfg = MockSystem.Instance.NewFighterCfg();

            var prefabPath = fighterCfg.GetPrefabPath();
            var resourceSys = SystemProvider.Instance.GetResourceSystem();

            var heroPrefab = resourceSys.Load<GameObject>(prefabPath);

            var rootObj = GameObject.Find("Root");

            Instantiate(heroPrefab, rootObj.transform);
        }
    }
}
