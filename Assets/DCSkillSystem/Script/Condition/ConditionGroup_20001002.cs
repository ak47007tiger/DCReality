using System.Collections.Generic;

namespace DC.ss
{
    [NodeInfo(20001002, "条件组，用于 (c1 and c2) or (c3 or c4)")]
    public class ConditionGroup_20001002 : Condition
    {
        public List<Condition> andConditionList;

        public List<Condition> orConditionList;

        public override bool CanPass()
        {
            if (orConditionList != null)
            {
                for (int i = 0; i < orConditionList.Count; i++)
                {
                    if (orConditionList[i].CanPass())
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i < andConditionList.Count; i++)
            {
                var andCondition = andConditionList[i];
                if (!andCondition.CanPass())
                {
                    return false;
                }
            }

            return true;
        }

        public override void OnTick(float delta)
        {
            for (int i = 0; i < orConditionList.Count; i++)
            {
                orConditionList[i].OnTick(delta);
            }

            for (int i = 0; i < andConditionList.Count; i++)
            {
                andConditionList[i].OnTick(delta);
            }
            
            base.OnTick(delta);
        }
    }

    
}