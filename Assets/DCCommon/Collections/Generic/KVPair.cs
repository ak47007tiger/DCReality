﻿namespace DC.Collections.Generic
{
    public class KVPair<TKey, TValue>
    {
        public KVPair() { }

        public KVPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key;

        public TValue Value;
    }
}