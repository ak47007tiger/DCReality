namespace DC.ss
{
    /// <summary>
    /// 条件执行
    /// </summary>
    public class Condition : SkillNode
    {
        public SkillNode passNode;

        public SkillNode notPassNode;

        public virtual bool CanPass()
        {
            return true;
        }

        public virtual void OnExecPassNode()
        {

        }

        public virtual void OnExecNotPassNode()
        {

        }

        public override void OnTick(float delta)
        {
            if (CanPass())
            {
                OnExecPassNode();
                passNode.Tick(delta);
            }
            else
            {
                OnExecNotPassNode();
                notPassNode.Tick(delta);
            }
        }

    }

}
