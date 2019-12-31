using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        private Transform mCacheTf;
        public Transform CacheTransform
        {
            get
            {
                if (mCacheTf == null) mCacheTf = transform;
                return mCacheTf;
            }
        }

        private RectTransform mCacheRTf;
        public RectTransform CacheRectTransform
        {
            get
            {
                if (mCacheRTf == null) mCacheRTf = transform as RectTransform;
                return mCacheRTf;
            }
        }
    }
}
