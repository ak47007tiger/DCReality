using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

namespace DC.ss
{
    public class SkillDesc : Desc
    {
        public TargetFinderDesc[] targetFinderDescList;

        public TimeLineDesc[] timeLineDescList;

        public HandlerDesc[] handlerDescList;

        public void Serialize(Stream outStream)
        {
            var writer = new BinaryWriter(outStream);
            GetJson().SerializeBinary(writer);
        }

        public void Deserialize(string json)
        {
            var jsonNode = JSON.Parse(json);
            FromJson(jsonNode as JSONObject);
        }

        public override void FromJson(JSONObject json)
        {
            base.FromJson(json);
        }

        public override JSONNode GetJson()
        {
            return base.GetJson();
        }
    }

}
