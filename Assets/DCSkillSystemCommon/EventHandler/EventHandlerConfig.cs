using System;
using System.Collections.Generic;
using DC.ActorSystem;
using DC.DCResourceSystem;
using UnityEngine;
using Object = UnityEngine.Object;

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
        public EffectType mType;
        public VisualEffectCfg mVisualEffectCfg;
        public int mChildSkillId;
        #endregion
    }

    public class BaseEvtHandler
    {
        public Skill mSkill;

        public EventHandlerConfig mHandlerCfg;

        public BaseEvtHandler SetSkill(Skill skill)
        {
            mSkill = skill;
            return this;
        }

        public BaseEvtHandler SetConfig(EventHandlerConfig cfg)
        {
            mHandlerCfg = cfg;
            return this;
        }

        /// <summary>
        /// time tick
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// 某种事件发生
        /// </summary>
        /// <param name="objs"></param>
        public virtual void OnEvt(params object[] objs)
        {

        }

        protected virtual void DoAction(List<IActor> targets)
        {
            switch (mHandlerCfg.mType)
            {
                case EffectType.visual_effect:

//                    BaseEffect.PlayVisualEffect();
                    break;
                case EffectType.child_skill:
                    break;
                case EffectType.buff:
                    break;
                case EffectType.effect_target:
                    mSkill.DoSkillEffectForTimer();
                    break;
            }
        }

        public void DoVFXAction(Skill skill, ICaster caster, List<IActor> actors)
        {
            var cfg = mHandlerCfg.mVisualEffectCfg;
            switch (cfg.mTransformType)
            {
                case TransformType.skill:
                    break;
                case TransformType.caster:
                    break;
                case TransformType.target:
                    break;
                case TransformType.world:
                    break;
            }
        }

        public void CreateVFX(Transform anchorTf, List<IActor> targets)
        {
            var cfg = mHandlerCfg.mVisualEffectCfg;
            Vector3 worldPos;
            if (null == anchorTf)
            {
                worldPos = cfg.mLocalPosOfXX;
            }
            else
            {
                worldPos = anchorTf.localToWorldMatrix.MultiplyPoint(cfg.mLocalPosOfXX);
            }

            var prefab = ResourceSys.Instance.Load<GameObject>(cfg.mEffectPath);
            var vfx = Object.Instantiate(prefab, worldPos, Quaternion.identity);
            if (cfg.mPointCnt > 1)
            {
                //设置节点位置
                var tf = vfx.transform;
                var cnt = Mathf.Min(cfg.mPointCnt, targets.Count, tf.childCount);
                for (var i = 0; i < cnt; i++)
                {
                    var childTf = tf.GetChild(i);
                    childTf.position = targets[i].GetTransform().position;
                }
                //隐藏多余节点
                cnt = Mathf.Min(cfg.mPointCnt, targets.Count);
                for (var i = cnt; i < tf.childCount; i++)
                {
                    var childTf = tf.GetChild(i);
                    childTf.gameObject.SetActive(false);
                }
            }
            DCTimer.RunAction(cfg.mDuration, () =>
            {
                Object.Destroy(vfx);
            });
        }

    }

}
