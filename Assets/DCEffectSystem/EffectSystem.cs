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

namespace DC.EffectSys
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

    public class EffectSystem : Singleton<EffectSystem>
    {
    }
}