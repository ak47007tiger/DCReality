namespace DC.ss
{
    [NodeInfo(20001001, "时间条件节点，过多久后触发")]
    public class Condition_20001001 : Condition
    {
        public float tickTimer;

        public float emitTime;

        public override void OnTick(float delta)
        {
            tickTimer += delta;
            base.OnTick(delta);
        }

        public override bool CanPass()
        {
            return tickTimer > emitTime;
        }
    }

}
