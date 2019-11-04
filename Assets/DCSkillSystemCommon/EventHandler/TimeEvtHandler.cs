using UnityEngine;

namespace DC.SkillSystem
{

    public class TimeEvtHandler : BaseEvtHandler
    {
        public float mTickedDuration;

        public int mTickedHandledCnt;

        public override void Update()
        {
            mTickedDuration += Time.deltaTime;
            if (mTickedDuration < mHandlerCfg.mEffectDelay)
            {
                return;
            }

            var tickedDuration2 = mTickedDuration - mHandlerCfg.mEffectDelay;
            if (tickedDuration2 > mHandlerCfg.mInterval)
            {
                if (mTickedHandledCnt < mHandlerCfg.mHandleMaxCnt)
                {
                    mTickedHandledCnt++;
                    DoAction(null);
                }
            }
        }

        

    }

}
