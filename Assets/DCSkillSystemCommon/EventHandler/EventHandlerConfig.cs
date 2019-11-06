using System;
using System.Collections.Generic;
using DC.ActorSystem;
using DC.AI;
using DC.DCResourceSystem;
using DC.GameLogic;
using UnityEngine;

namespace DC.SkillSystem
{
    /*
     
        一个技能的视觉效果
            生效时创建特效于特定位置
                技能位置
                施法者位置
                接受者位置
        生命周期事件
            创建
            进行中
            销毁
        时间事件

        视觉和动画
            技能对象，管理数据，处理消息
            效果对象，表现
            比如：
        
        有关物理
            不使用collider component
            技能自己cast

        物理碰撞和视觉分开
            skill根据视觉效果的位置自行physic cast
        
        可能的技能视觉效果
            丢到某一个位置
                需要移动
                不需要移动
            丢向某一个方向
            丢到某一个目标
        
        技能目标数量
            指向性技能只有一个
            aoe的选取，狐狸的w技能，是aoe，选取最近的几个
            cast的时候已经知道打到谁，所以是接触后回调

            子弹型非指向性技能
                位置
                方向
                碰撞回调
                抵达回调
        
        延迟型技能案例1
            子弹：丢出一个技能到目标位置
            定时：过一段时间后技能生效
            物理计算：攻击收集到的对象产生影响
     */

    public enum HandlerType
    {
        none,
        time,
        /// <summary>
        /// 技能创建后
        /// </summary>
        after_create,
        /// <summary>
        /// 技能生效时
        /// </summary>
        on_cast_target,
    }

    [Serializable]
    public class EventHandlerConfig
    {
        public HandlerType mHandlerType;
        
        #region time tick
        public float mEffectDelay;
        public int mHandleMaxCnt = 1;
        public float mInterval;
        #endregion

        #region action
        public EffectType mEffectType;
        public VisualEffectCfg mVisualEffectCfg;
        public int mChildSkillId;
        public TranslateFxConfig mTranslateFxConfig;

        #endregion
    }

}
