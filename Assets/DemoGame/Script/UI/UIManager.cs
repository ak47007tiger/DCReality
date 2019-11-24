using System;
using System.Collections;
using System.Collections.Generic;
using DC.Collections.Generic;
using DC.DCResourceSystem;
using DC.UI;
using UnityEngine;

namespace DC
{
    public enum ShowType
    {
        LastInstance,
        NewInstance,
    }

    public class UIManager : SingletonMono<UIManager>
    {
        Dictionary<string, BaseUI> mKeyToUi = new Dictionary<string, BaseUI>();
        Stack<BaseUI> mUiStack = new Stack<BaseUI>();
        Stack<BaseUI> mUiStackBuffer = new Stack<BaseUI>();

        private Transform _mUiRoot;

        public Transform UiRoot
        {
            get
            {
                if (null == _mUiRoot)
                {
                    _mUiRoot = GameObject.Find("UIRoot").transform.Find("UICanvas");
                }

                return _mUiRoot;
            }
        }

        public T GetUi<T>() where T : BaseUI
        {
            var uiName = GetUiName<T>();
            var baseUi = mKeyToUi.GetValEx(uiName);
            if (null != baseUi)
            {
                return baseUi as T;
            }

            return null;
        }

        public BaseUI GetUi(string uiName)
        {
            var baseUi = mKeyToUi.GetValEx(uiName);
            if (null != baseUi)
            {
                return baseUi;
            }

            return null;
        }

        public T ShowUi<T>(params object[] param) where T : BaseUI
        {
            var uiName = GetUiName<T>();
            return ShowUi(uiName, param) as T;
        }

        public BaseUI ShowUi(string uiName, params object[] param)
        {
            var baseUi = mKeyToUi.GetValEx(uiName);
            if (null != baseUi)
            {
                baseUi.OnShow();
                return baseUi;
            }

            var assetPath = GetAssetPath(uiName);
            var prefab = ResourceSys.Instance.Load<GameObject>(assetPath);

            var instance = Instantiate(prefab, UiRoot);
            baseUi = instance.GetComponent<BaseUI>();
            baseUi.Init(param);

            mKeyToUi.Add(uiName, baseUi);
            mUiStack.Push(baseUi);

            return baseUi;
        }

        public void HideUi<T>()
        {
            HideUi(GetUiName(typeof(T)));
        }

        public void HideUi(string uiName)
        {
            var baseUi = GetUi(uiName);
            if (null != baseUi)
            {
                baseUi.OnHide();
            }
        }

        public void CloseUi<T>()
        {
            CloseUi(GetUiName<T>());
        }

        public void CloseUi(string uiName)
        {
            var baseUi = mKeyToUi.GetValEx(uiName);
            if (null == baseUi)
            {
                return;
            }

            RemoveFromStack(baseUi);

            mKeyToUi.Remove(uiName);

            //destroy
            baseUi.OnWindowDestroy();
            Destroy(baseUi.gameObject);

            //pop top window
            if (mUiStack.Count > 0)
            {
                ShowUi(GetUiName(mUiStack.Peek().GetType()));
            }
        }

        public void RemoveFromStack(BaseUI baseUi)
        {
            if (mUiStack.Contains(baseUi))
            {
                //倒出去，找到，倒回来
                mUiStackBuffer.Clear();
                var ui = mUiStack.Pop();
                while(ui != baseUi)
                {
                    mUiStackBuffer.Push(ui);
                    ui = mUiStack.Pop();
                }

                while (mUiStackBuffer.Count > 0)
                {
                    mUiStack.Push(mUiStackBuffer.Pop());
                }
            }
        }

        public T CreateUiInstance<T>(Transform parent)
        {
            var uiName = GetUiName<T>();
            return CreateUiInstance(parent, uiName).GetComponent<T>();
        }

        public GameObject CreateUiInstance(Transform parent, string uiName)
        {
            var prefab = LoadPrefab(uiName);
            return Instantiate(prefab, parent);
        }

        public GameObject LoadPrefab<T>()
        {
            var uiName = GetUiName<T>();
            return LoadPrefab(uiName);
        }

        public GameObject LoadPrefab(string uiName)
        {
            var assetPath = GetAssetPath(uiName);
            var prefab = ResourceSys.Instance.Load<GameObject>(assetPath);
            return prefab;
        }

        public static string GetUiName<T>()
        {
            return GetUiName(typeof(T));
        }

        public static string GetUiName(Type type)
        {
            return type.Name;
        }

        public static string GetAssetPath(string name)
        {
            return "UI/" + name;
        }

    }

}
