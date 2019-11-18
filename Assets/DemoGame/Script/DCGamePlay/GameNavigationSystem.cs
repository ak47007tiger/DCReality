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
                        MsgSys.Send(GameEvent.ClickEnvGround, hit.point);
                    }
                }
            }
        }
    }

    
}