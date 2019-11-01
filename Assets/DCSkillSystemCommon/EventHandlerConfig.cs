using System;

namespace DC.SkillSystem
{
    public enum HandlerType
    {
        none,
        time,
        on_cast_target,
        on_player_die,
    }

    public enum EffectType
    {
        animation,
        visual_effect,
        translate,//type, dir, distance
        buff,
        camera,
        create_npc,
        terrier,
    }

    public class BaseEffect
    {
        public EffectType mType;


    }

    [Serializable]
    public class EventHandlerConfig
    {
        public HandlerType mHandlerType;
        public float mEffectDelay;
        public int mHandleMaxCnt;
        public float mInterval;
    }

    public class BaseEvtHandler
    {
        public EventHandlerConfig mHandlerCfg;

        public BaseEvtHandler SetConfig(EventHandlerConfig cfg)
        {
            mHandlerCfg = cfg;
            return this;
        }

        /// <summary>
        /// time tick
        /// </summary>
        public void Update()
        {

        }

        public void OnEvt(params object[] objs)
        {

        }

    }

    public class TimeEvtHandler : BaseEvtHandler
    {

    }

}
