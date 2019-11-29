using System.Collections;
using System.Collections.Generic;
using System.IO;
using DC.ActorSystem;
using DC.Collections.Generic;
using DC.GameLogic;
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

        public static Dictionary<TK, TV> PairListToDictionary<TK, TV>(List<KeyValuePair<TK, TV>> pairList,
            Dictionary<TK, TV> dic)
        {
            foreach (var kv in pairList)
            {
                dic.Add(kv.Key, kv.Value);
            }

            return dic;
        }

        public static Dictionary<TK, TV> PairListToDictionary<TKV, TK, TV>(List<TKV> pairList)
            where TKV : KVPair<TK, TV>
        {
            var dic = new Dictionary<TK, TV>();
            return PairListToDictionary(pairList, dic);
        }

        public static Dictionary<TK, TV> PairListToDictionary<TKV, TK, TV>(List<TKV> pairList, Dictionary<TK, TV> dic)
            where TKV : KVPair<TK, TV>
        {
            foreach (var kv in pairList)
            {
                dic.Add(kv.Key, kv.Value);
            }

            return dic;
        }

        public static bool IsNullOrEmpty<T>(T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static Vector3 Mul(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static ActorSide GetOpSide(ActorSide actorSide)
        {
            switch (actorSide)
            {
                case ActorSide.blue:
                    return ActorSide.red;
                case ActorSide.red:
                    return ActorSide.blue;
            }
            return ActorSide.neutral;
        }

        public static void RemoveFromStack<T>(Stack<T> stack, T item)
        {
            if (stack.Contains(item))
            {
                Stack<T> tempStack = new Stack<T>();

                //倒出去，找到，倒回来
                var ui = stack.Pop();
                while (!ui.Equals(item))
                {
                    tempStack.Push(ui);
                    ui = stack.Pop();
                }

                while (tempStack.Count > 0)
                {
                    stack.Push(tempStack.Pop());
                }
            }
        }
    }

    public static class DCGameObjectExtension
    {

        public static T GetOrAdd<T>(this GameObject gobj) where T : Component
        {
            var t = gobj.GetComponent<T>();
            if (t == null)
            {
                t = gobj.AddComponent<T>();
            }

            return t;
        }

    }

    public static class DCMonoBehaviourExtension
    {
        public static bool TryGetComponent<T>(this MonoBehaviour mono, out T t) where T : Component
        {
            t = mono.GetComponent<T>();
            return t != null;
        }
    }
}