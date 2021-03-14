using SimpleJSON;
using UnityEngine;

namespace DC.ss
{
    public class Desc
    {
        public int id;

        public float[] floatData;

        public int[] intData;

        public long[] longData;

        public virtual JSONNode GetJson()
        {
            return null;
        }

        public virtual void FromJson(JSONObject json)
        {

        }
    }
}