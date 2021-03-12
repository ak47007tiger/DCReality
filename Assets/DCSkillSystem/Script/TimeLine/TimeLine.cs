using System.Collections.Generic;

namespace DC.ss
{
    /// <summary>
    /// 多个node顺序执行
    /// </summary>
    public class TimeLine : SkillNodeTimerBased
    {
        public List<SkillNode> skillNodeList;

        public override void Tick(float delta)
        {
            base.Tick(delta);

            for (int i = 0; i < skillNodeList.Count; i++)
            {
                skillNodeList[i].Tick(delta);
            }
        }
    }

}
