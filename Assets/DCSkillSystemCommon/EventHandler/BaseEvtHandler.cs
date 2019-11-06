using System.Collections.Generic;
using DC.ActorSystem;
using DC.AI;
using DC.DCResourceSystem;
using DC.GameLogic;
using UnityEngine;

namespace DC.SkillSystem
{
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
            switch (mHandlerCfg.mHandlerType)
            {
                case HandlerType.after_create:
                    DoAction(null);
                    break;
                case HandlerType.on_cast_target:
                    break;
                case HandlerType.time:
                    break;
            }
        }

        protected virtual void DoAction(List<IActor> targets)
        {
            switch (mHandlerCfg.mType)
            {
                case EffectType.visual_effect:
                    DoVFXAction(mSkill, targets);
                    break;
                case EffectType.child_skill:
                    break;
                case EffectType.translate:
                    DoTranslateAction(mSkill, targets);
                    break;
                case EffectType.effect_target:
                    mSkill.DoSkillEffectForTimer();
                    break;
                case EffectType.animation:

                    break;
            }
        }

        public void DoAnimationFx()
        {

        }

        public void DoTranslateAction(Skill skill, List<IActor> actors)
        {
            var cfg = mHandlerCfg.mTranslateFxConfig;
            switch (cfg.mTransformType)
            {
                case TransformType.skill:
                    DoTranslateActionSub(skill.CacheTransform);
                    break;
                case TransformType.caster:
                    DoTranslateActionSub(skill.GetCaster().GetActor().GetTransform());
                    break;
                case TransformType.target:
                    DoTranslateActionSub(actors[0].GetTransform());
                    break;
            }
        }

        public void DoTranslateActionSub(Transform targetTf)
        {
            var cfg = mHandlerCfg.mTranslateFxConfig;
            var targetPos = mSkill.CacheTransform.position;

            var tfArrivePosition = targetTf.gameObject.GetOrAdd<TfArrivePosition>();
            var distance = Vector3.Distance(targetPos, targetTf.position);
            var speed = distance / cfg.mDuration;
            tfArrivePosition.StartTrace(targetPos, SystemPreset.move_stop_distance, speed);
        }

        public void DoVFXAction(Skill skill, List<IActor> actors)
        {
            var cfg = mHandlerCfg.mVisualEffectCfg;
            switch (cfg.mTransformType)
            {
                case TransformType.skill:
                    CreateVFX(skill.CacheTransform, actors);
                    break;
                case TransformType.caster:
                    CreateVFX(skill.GetCaster().GetActor().GetTransform(), actors);
                    break;
                case TransformType.target:
                    CreateVFX(actors[0].GetTransform(), actors);
                    break;
                case TransformType.world:
                    CreateVFX(null, actors);
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
            //链状技能
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