using System;
using System.Collections.Generic;

namespace DC.ss
{
    public class Skill
    {
        protected DCSkillSystem sys;

        protected SkillDesc desc;

        protected List<TargetFinder> targetFinderList;

        protected List<TimeLine> timeLineList;

        protected Dictionary<int, List<Handler>> evtId2handlerList;

        protected float tickTimer;

        public void Attach(DCSkillSystem skillSys)
        {
            sys = skillSys;
        }

        public void Init(SkillDesc desc)
        {
            this.desc = desc;

            evtId2handlerList = new Dictionary<int, List<Handler>>();

            for (int i = 0; i < desc.handlerDescList.Length; i++)
            {
                
            }

            timeLineList = new List<TimeLine>(desc.timeLineDescList.Length);
            for (int i = 0; i < desc.timeLineDescList.Length; i++)
            {
                timeLineList.Add(sys.BuildTimeLine(desc.timeLineDescList[i]));
            }

        }

        public void OnEvt(int evtId, object userData)
        {
            if (evtId2handlerList.TryGetValue(evtId, out var handlerList))
            {
                for (int i = 0; i < handlerList.Count; i++)
                {
                    var handler = handlerList[i];
                    handler.Exec(userData);
                }
            }
        }

        public void Tick(float delta)
        {
            tickTimer += delta;

        }
    }
}
