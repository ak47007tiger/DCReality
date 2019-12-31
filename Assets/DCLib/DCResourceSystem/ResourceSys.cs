using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public Sprite GetSprite(string icon)
        {
            return Resources.Load<Sprite>(icon);
        }

        public bool SetImage(Image img, string icon)
        {
            var sprite = GetSprite(icon);
            img.sprite = sprite;
            return sprite != null;
        }
    }
}
