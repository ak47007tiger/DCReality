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
    }
}
