using System;
using System.Collections;
using System.Collections.Generic;
using DC.DCResourceSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DC
{
    public class UIManager : SingletonMono<UIManager>
    {
        Dictionary<string, GameObject> mKeyToUi = new Dictionary<string, GameObject>();

        private Transform _mUiRoot;

        public Transform UiRoot
        {
            get
            {
                if (null == _mUiRoot)
                {
                    _mUiRoot = GameObject.Find("UIRoot").transform;
                }

                return _mUiRoot;
            }
        }

        public void ShowUi(string key)
        {

        }

        public void ShowUi<T>()
        {
            var uiName = typeof(T).Name;
            var assetPath = GetAssetPath(uiName);
            var prefab = ResourceSys.Instance.Load<GameObject>(assetPath);
            var instance = Instantiate(prefab, UiRoot);
            mKeyToUi.Add(uiName, instance);
        }

        public void CloseUi<T>()
        {

        }

        public static string GetAssetPath(string name)
        {
            return "UI/" + name;
        }

    }

}
