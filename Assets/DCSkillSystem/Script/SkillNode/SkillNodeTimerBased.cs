namespace DC.ss
{
    public class SkillNodeTimerBased : SkillNode
    {
        public float tickTimer;

        public override void OnTick(float delta)
        {
            tickTimer += delta;
            base.OnTick(delta);
        }
    }
}