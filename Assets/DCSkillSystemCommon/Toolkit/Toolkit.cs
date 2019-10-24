using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class Toolkit
    {
        public static readonly float conv_base = 100;

        public static Vector3 IntToFloat(Vector3Int v3Int)
        {
            return new Vector3(v3Int.x, v3Int.y, v3Int.z) / conv_base;
        }

        public static Vector3Int FloatToInt(Vector3 v3)
        {
            return new Vector3Int((int) (v3.x * conv_base), (int) (v3.y * conv_base), (int) (v3.z * conv_base));
        }

        public static float ComputeDistance(Component a, Component b)
        {
            return ComputeDistance(a.transform, b.transform);
        }

        public static float ComputeDistance(Transform a, Transform b)
        {
            return Vector3.Distance(a.position, b.position);
        }

        public static Dictionary<TK, TV> PairListToDictionary<TK, TV>(List<KeyValuePair<TK, TV>> pairList)
        {
            var dic = new Dictionary<TK, TV>();
            return PairListToDictionary(pairList, dic);
        }

        public static Dictionary<TK, TV> PairListToDictionary<TK, TV>(List<KeyValuePair<TK, TV>> pairList, Dictionary<TK,TV> dic)
        {
            foreach (var kv in pairList)
            {
                dic.Add(kv.Key, kv.Value);
            }

            return dic;
        }
    }
}