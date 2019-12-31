using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class Singleton<T> where T : new()
    {
        private static T sInstance;

        private static readonly object sLock = new object();

        public static T Instance
        {
            get
            {
                lock (sLock)
                {
                    if (sInstance == null)
                    {
                        sInstance = new T();
                    }
                }

                return sInstance;
            }
        }
    }

}
