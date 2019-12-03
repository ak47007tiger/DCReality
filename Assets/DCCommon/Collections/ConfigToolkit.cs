using System;
using System.Collections.Generic;

namespace DC.Collections
{
    public class ConfigToolkit
    {
        public delegate K VToK<K, V>(V v);

        public static Dictionary<K, V> ListToDictionary<K, V>(List<V> list, VToK<K, V> vToK)
        {
            var dic = new Dictionary<K, V>();
            for (var i = 0; i < list.Count; i++)
            {
                var v = list[i];
                dic.Add(vToK(v), v);
            }

            return dic;
        }

        public static Dictionary<K, V> ListToDictionary<K, V>(V[] array, VToK<K, V> vToK)
        {
            var dic = new Dictionary<K, V>();
            for (var i = 0; i < array.Length; i++)
            {
                var v = array[i];
                dic.Add(vToK(v), v);
            }

            return dic;
        }


        public static Dictionary<int, T> ConvertEnumToDic<T>()
        {
            var dic = new Dictionary<int, T>();
            var type = typeof(T);
            var values = Enum.GetValues(type);
            foreach (var value in values)
            {
                T t = (T)value;
                dic.Add(t.ToString().GetExtHashCode(), t);
            }

            return dic;
        }
    }
}