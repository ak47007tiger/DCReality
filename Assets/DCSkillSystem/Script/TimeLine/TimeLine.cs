using System.Collections.Generic;

namespace DC.ss
{
    /// <summary>
    /// 多个node顺序执行
    /// </summary>
    public class TimeLine : SkillNodeTimerBased
    {
        public List<SkillNode> skillNodeList;

        public bool enable;

        public override void Tick(float delta)
        {
            if (!enable) return;

            base.Tick(delta);
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);

            for (int i = 0; i < skillNodeList.Count; i++)
            {
                skillNodeList[i].OnTick(delta);
            }
        }
    }

}
