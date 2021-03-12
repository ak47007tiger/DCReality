using System.Collections.Generic;

namespace DC.ss
{
    public class Handler
    {
        public HandlerDesc desc;

        public List<SkillNode> actionList;

        public bool Exec(object userData)
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                if (!actionList[i].Exec(userData))
                {
                    return false;
                }
            }

            return true;
        }
    }
}