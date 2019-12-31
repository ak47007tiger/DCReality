using UnityEngine;

namespace DC
{
    public class DCGraphics : Singleton<DCGraphics>
    {
        private Camera mMainCamera;

        public Camera MainCamera
        {
            get
            {
                if(null == mMainCamera) mMainCamera = Camera.main;
                return mMainCamera;
            }
        }

    }
}