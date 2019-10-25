using UnityEngine;

namespace DC.GameLogic
{
    public class GameNavigationSystem : SingletonMono<GameNavigationSystem>
    {
        private CacheItem<Camera> mMainCamera = new CacheItem<Camera>(() => { return Camera.main;});

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = mMainCamera.Value.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (SystemPreset.IsGround(hit.transform.gameObject.tag))
                    {
                        MsgSys.Instance.Send(GameEvent.ClickEnvGround, hit.point);
                    }
                }
            }
        }
    }

    public delegate T GetInstance<T>();

    public class CacheItem<T>
    {
        private T mItem;

        private GetInstance<T> mGetter;

        public CacheItem(GetInstance<T> getter)
        {
            SetGetter(getter);
        }

        public void SetGetter(GetInstance<T> getter)
        {
            mGetter = getter;
        }

        public T Value
        {
            get
            {
                if (mGetter == null)
                {
                    return default(T);
                }

                if (mItem == null)
                {
                    mItem = mGetter();
                }
                return mItem;
            }
        }
    }
}