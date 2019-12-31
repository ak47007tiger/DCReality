using System.Collections.Generic;

namespace DC.Collections.Generic
{
    public static class DictionaryExtension
    {
        public static V GetValEx<K, V>(this Dictionary<K, V> dic, K k)
        {
            if (dic.TryGetValue(k, out var v))
            {
                return v;
            }

            return default(V);
        }
    }
}