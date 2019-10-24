using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC.ResourceSys
{
    public interface IResourceSystem
    {
        T Load<T>(string path) where T : Object;
    }

    public class ResourceSystem : Singleton<ResourceSystem>, IResourceSystem
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
