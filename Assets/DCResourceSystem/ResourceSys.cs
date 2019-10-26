using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC.DCResourceSystem
{
    public interface IResourceSystem
    {
        T Load<T>(string path) where T : Object;

        T[] LoadAll<T>(string path) where T : Object;
    }

    public class ResourceSys : Singleton<ResourceSys>, IResourceSystem
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}
