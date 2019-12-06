using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.GameLogic;

namespace DC.SkillSystem
{
    /*
     
        buff的生命周期管理是被管理的
        buff是信息提供者和被动的执行者
     */

    public class Buff : IBuff
    {
        public int mId;

        public BuffCfg mBuffCfg;

        public GameActor OwnActor;
        
        HashSet<GameActor> mEfxActors = new HashSet<GameActor>();

        private float mAttachTime;
        
        public bool AllowCast(ISkill skill)
        {
            return true;
        }

        public void OnAttach(GameActor actor)
        {
            OwnActor = actor;
            mAttachTime = Time.time;
        }

        public void OnDetach()
        {
            OwnActor = null;

            //去除aoe buff产生的buff
            if (mBuffCfg.mEffectRangeType == EffectRangeType.Aoe)
            {

            }
        }

        public void OnUpdate()
        {
            if (Time.time - mAttachTime > mBuffCfg.mDuration)
            {
                OwnActor.GetBuffCmpt().RemoveBuff(this);
                return;
            }
            UpdateEffectActorList();
        }

        HashSet<GameActor> mTempSet = new HashSet<GameActor>();

        /// <summary>
        /// aoe类型的buff需要检测更新生效的actor
        /// </summary>
        public void UpdateEffectActorList()
        {
            if (mBuffCfg.mEffectRangeType != EffectRangeType.Aoe)
            {
                return;
            }
            //检查上一次的受击者是否离开范围，并且移除
            foreach (var actor in mEfxActors)
            {
                if (!IsInEfxRange(actor.CacheTransform.position))
                {
                    actor.GetBuffCmpt().RemoveBuff(this);
                }
            }

            ActorSide ownActorSide = OwnActor.GetActorSide();

            //todo 包围盒子配置
            var halfSize = new Vector3(mBuffCfg.mEffectRange, 1, mBuffCfg.mEffectRange);
            var overlapBox = Physics.OverlapBox(OwnActor.CacheTransform.position, halfSize, Quaternion.identity, SystemPreset.layer_actor);
            foreach (var collider in overlapBox)
            {
                var targetActor = collider.GetComponent<GameActor>();
                var sideRelation = Toolkit.GetSideRelation(ownActorSide, targetActor.GetActorSide());

                if (mBuffCfg.IsInEffectRelation(sideRelation))
                {
                    if (!mEfxActors.Contains(targetActor))
                    {
                        var buffCmpt = targetActor.GetBuffCmpt();
                        buffCmpt.AddBuff(this);
                        mEfxActors.Add(targetActor);
                    }
                }
            }
        }

        public bool IsInEfxRange(Vector3 position)
        {
            var distance = Vector3.Distance(OwnActor.CacheTransform.position, position);
            return distance < mBuffCfg.mEffectRange;
        }

    }

}
