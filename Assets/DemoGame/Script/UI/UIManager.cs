using System;
using System.Collections;
using System.Collections.Generic;
using DC.Collections.Generic;
using DC.DCResourceSystem;
using DC.UI;
using UnityEngine;
using Object = UnityEngine.Object;

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
                    _mUiRoot = GameObject.Find("UIRoot").transform;
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
            return ShowUi(uiName) as T;
        }

        public BaseUI ShowUi(string uiName)
        {
            var baseUi = mKeyToUi.GetValEx(uiName);
            if (null != baseUi)
            {
                return baseUi;
            }

            var assetPath = GetAssetPath(uiName);
            var prefab = ResourceSys.Instance.Load<GameObject>(assetPath);
            var instance = Instantiate(prefab, UiRoot);
            var tUi = instance.GetComponent<BaseUI>();
            mKeyToUi.Add(uiName, tUi);
            mUiStack.Push(tUi);
            return tUi;
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
            Destroy(baseUi);
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

        public static string GetUiName<T>()
        {
            return typeof(T).Name;
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
