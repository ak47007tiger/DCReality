using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 视觉
     特效
     动画
 听觉
     音频
 触觉
     震动
 */

namespace DC.DCEffectSys
{
    public interface IEffectSystem
    {
        void PlayEffect(EffectType effectType, object pParam);
        void StopEffect(EffectType effectType, object pParam);
    }

    public enum EffectType
    {
        Camera_Shake,
        Camera_Gray,

        Audio,

        Effect,
    }

    public class EffectSys : Singleton<EffectSys>, IEffectSystem
    {
        public void PlayEffect(EffectType effectType, object pParam)
        {
            throw new System.NotImplementedException();
        }

        public void StopEffect(EffectType effectType, object pParam)
        {
            throw new System.NotImplementedException();
        }
    }
}