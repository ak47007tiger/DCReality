using System;

namespace DC.ss
{
    public class DCSkillSystem
    {
        public Action CreateAction(int id)
        {
            throw new NotImplementedException();
        }

        public TimeLine BuildTimeLine(TimeLineDesc desc)
        {
            throw new NotImplementedException();
        }

        public Handler BuildHandler(HandlerDesc desc)
        {
            throw new NotImplementedException();
        }
    }

    public enum NodeType
    {
        action = 1,
        condition = 2,
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NodeInfo : Attribute
    {
        public string desc;

        public int id;

        public NodeInfo(string desc)
        {
            this.desc = desc;
        }

        public NodeInfo(int id)
        {
            this.id = id;
        }

        public NodeInfo(int id, string desc)
        {
            this.id = id;
            this.desc = desc;
        }
    }

}
