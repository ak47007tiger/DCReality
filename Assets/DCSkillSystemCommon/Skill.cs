using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.AI;
using DC.DCPhysics;
using DC.GameLogic;

namespace DC.SkillSystem
{
    /*
        区域型技能
        移动型技能

        放一个技能打多个目标 这个技能不能是指向性的

        根据时间流逝释放skill的行为
        根据监听到的事件skill来处理
     */

    public class Skill : BaseMonoBehaviour, ISkill
    {
        private ICaster mCaster;

        private SkillCfg mSkillCfg;

        private CastCfg mCastCfg;

        private CacheItem<BoxCollider> mBoxCollider;

        private float mTickedLife;

        private int mHitCnt;

        private List<DCBaseTimer> mTimerToDestroy = new List<DCBaseTimer>();

        private int mDoSkillEffectRemaingCnt = 1;

        private List<BaseEvtHandler> mEvthandlerList = new List<BaseEvtHandler>();

        private Dictionary<HandlerType, List<BaseEvtHandler>> mTypeToHandlerList =
            new Dictionary<HandlerType, List<BaseEvtHandler>>();

        protected void Awake()
        {
            mBoxCollider = new CacheItem<BoxCollider>(GetComponent<BoxCollider>);
        }

        public ICaster GetCaster()
        {
            return mCaster;
        }

        public void SetCaster(ICaster caster)
        {
            mCaster = caster;
        }

        public CastCfg GetCastCfg()
        {
            return mCastCfg;
        }

        public void SetCastCfg(CastCfg castCfg)
        {
            mCastCfg = castCfg;
        }

        public SkillCfg GetSkillCfg()
        {
            return mSkillCfg;
        }

        public void SetSkillCfg(SkillCfg skillCfg)
        {
            mSkillCfg = skillCfg;
        }

        public bool AllowCastTo(IActor actor)
        {
            return true;
        }

        public void OnCatchTarget(IActor target)
        {
        }

        public List<IActor> GetEffectTargets()
        {
            return null;
        }

        public void OnSkillLifeRecycle(SkillLifeCycle lifeCycle)
        {
        }

        private void CreateBullet()
        {
            switch (mSkillCfg.mTargetType)
            {
                case SkillTargetType.Actor:
                {
                    var transformTraceTarget = gameObject.AddComponent<TfTraceTarget>();
                    transformTraceTarget.StartTrace(mCastCfg.mTargets[0].GetTransform(), 0.5f, mSkillCfg.mSpeed);
                    break;
                }
                case SkillTargetType.Position:
                {
                    var arriveCmp = gameObject.AddComponent<TfArrivePosition>();
                    arriveCmp.StartTrace(mCastCfg.mTargetPosition, 0.5f, mSkillCfg.mSpeed);
                    break;
                }
                case SkillTargetType.Direction:
                {
                    var moveDir = gameObject.AddComponent<TfMoveToDirection>();
                    moveDir.StartMove(mCastCfg.mDirection, mSkillCfg.mDuration, mSkillCfg.mSpeed);
                    break;
                }
            }

            //因为要检查是否获取目标，所以子弹类型每帧更新
            if (mSkillCfg.mEffectDelay > 0)
            {
                var delayTimer = new DCDurationTimer(mSkillCfg.mEffectDelay, SetupBulletTimerStep2)
                    .SetAutoDestroy(true).CreateNormal();
                mTimerToDestroy.Add(delayTimer);
            }
            else
            {
                SetupBulletTimerStep2();
            }
        }

        private void SetupBulletTimerStep2()
        {
            var frameTimer = new DCFrameTimer(1, DoSkillEffectForTimer, -1);
            frameTimer.CreatePhysic();
            mTimerToDestroy.Add(frameTimer);
        }

        private void CreateArea()
        {
            switch (mSkillCfg.mTargetType)
            {
                case SkillTargetType.Position:
                {
                    CacheTransform.position = mCastCfg.mTargetPosition;
                    break;
                }
                case SkillTargetType.Actor:
                {
                    break;
                }
            }

            //area类型，1 每过一段时间起效一次 2 经过延迟后直接起效
            if (mSkillCfg.mEffectDelay > 0)
            {
                var delayTimer = new DCDurationTimer(mSkillCfg.mEffectDelay, SetupAreaTimerStep2)
                    .SetAutoDestroy(true).CreateNormal();
                mTimerToDestroy.Add(delayTimer);
            }
            else
            {
                SetupAreaTimerStep2();
            }
        }

        private void SetupAreaTimerStep2()
        {
            if (mSkillCfg.mAffectInterval > 0)
            {
                var intervalTimer = new DCDurationTimer(mSkillCfg.mAffectInterval, PostDoSkillEffect, - 1).CreateNormal();
                mTimerToDestroy.Add(intervalTimer);
            }
            else
            {
                PostDoSkillEffect();
            }
        }

        private void PostDoSkillEffect()
        {
            mDoSkillEffectRemaingCnt++;
        }

        private void CreateNormal()
        {
            switch (mSkillCfg.mTargetType)
            {
                case SkillTargetType.Actor:
                {
                    break;
                }
                case SkillTargetType.Position:
                {
                    break;
                }
                case SkillTargetType.Direction:
                {
                    break;
                }
            }

            //normal类型是直接起效，所以直接施加影响到目标
            DoSkillEffectForTimer();
        }

        private void AddHandleToDic(HandlerType type, BaseEvtHandler handler)
        {
            if (!mTypeToHandlerList.TryGetValue(type, out var list))
            {
                list = new List<BaseEvtHandler>();
                mTypeToHandlerList.Add(type, list);
            }

            list.Add(handler);
        }

        private void InitHandlers()
        {
            foreach (var handlerConfig in mSkillCfg.mEvtHandlerCfgs)
            {
                switch (handlerConfig.mHandlerType)
                {
                    case HandlerType.none:
                        LogDC.Log("none handler cfg");
                        break;
                    case HandlerType.time:
                    {
                        var handler = new TimeEvtHandler();
                        AddHandleToDic(handlerConfig.mHandlerType, handler);
                        mEvthandlerList.Add(handler.SetConfig(handlerConfig).SetSkill(this));
                        break;
                    }
                    case HandlerType.on_cast_target:
                    {
                        var handler = new CastTargetHandler();
                        AddHandleToDic(handlerConfig.mHandlerType, handler);
                        mEvthandlerList.Add(handler.SetConfig(handlerConfig).SetSkill(this));
                        break;
                    }
                }
            }
        }

        public void Create()
        {
            LogDC.LogEx("apply skill id :", GetSkillCfg().mId, mSkillCfg.mSkillType);

            InitHandlers();

            switch (mSkillCfg.mSkillType)
            {
                case SkillType.bullet:
                {
                    CreateBullet();
                    break;
                }

                case SkillType.area:
                {
                    CreateArea();
                    break;
                }

                case SkillType.normal:
                    CreateNormal();
                    break;
            }
        }

        private void OnTraceTransformEnd(TfTraceTarget cmp, float distance)
        {
        }

        private void OnArrivePosEnd(TfArrivePosition cmp)
        {
        }

        private void OnMoveDirEnd(TfMoveToDirection cmp)
        {
        }

        public Transform GetTransform()
        {
            return CacheTransform;
        }

        void OnDestroy()
        {
            foreach (var timer in mTimerToDestroy)
            {
                timer.Destroy();
            }
        }

        void Update()
        {
            if (mTickedLife > mSkillCfg.mDuration)
            {
                SkillSys.Instance.DestroySkill(this);
                return;
            }

            mTickedLife += Time.deltaTime;

            if (mTypeToHandlerList.TryGetValue(HandlerType.time, out var list))
            {
                foreach (var handler in list)
                {
                    handler.Update();
                }
            }
        }

        public void DoSkillEffectForTimer()
        {
            if (mHitCnt > mSkillCfg.mHitCnt)
            {
                LogDC.Log("skill hit max");
                return;
            }

            var halfExtents = mBoxCollider.Value.size * 0.5f;
            var center = CacheTransform.position;
            var allHit = Physics.BoxCastAll(center, halfExtents, CacheTransform.forward, CacheTransform.rotation,
                halfExtents.x * 2);
            var bound = new Bounds(CacheTransform.position, mBoxCollider.Value.size);

            DebugExtension.DebugBounds(bound, Color.green);

            if (!Toolkit.IsNullOrEmpty(allHit))
            {
                LogDC.Log("get hit: " + allHit.Length);

                switch (mSkillCfg.mSkillType)
                {
                    case SkillType.area:
                        DoAreaSkillEffect(allHit);
                        break;
                    case SkillType.bullet:
                        DoBulletSkillEffect(allHit);
                        break;
                }
            }
        }

        void DoBulletSkillEffect(RaycastHit[] allHit)
        {
            if (mSkillCfg.mTargetType == SkillTargetType.Actor)
            {
                foreach (var raycastHit in allHit)
                {
                    /*

                    收集到目标，施加影响，发送事件
                     */
                    if (mHitCnt > mSkillCfg.mHitCnt)
                    {
                        return;
                    }

                    var hitActor = raycastHit.transform.GetComponent<IActor>();

                    if (hitActor != null)
                    {
                        if (mCastCfg.IsTarget(hitActor))
                        {
                            OnBulletHitActor(hitActor);
                        }
                    }
                }
            }
            else
            {
                foreach (var raycastHit in allHit)
                {
                    /*

                    收集到目标，施加影响，发送事件
                     */
                    if (mHitCnt > mSkillCfg.mHitCnt)
                    {
                        return;
                    }

                    var hitActor = raycastHit.transform.GetComponent<IActor>();

                    if (hitActor != null)
                    {
                        OnBulletHitActor(hitActor);
                    }
                }
            }
        }

        void DoAreaSkillEffect(RaycastHit[] allHit)
        {
            /*
             
            找到所有的actor，过滤actor并排序
            1 指向性
            2 非指向性
             */

            var actors = new List<IActor>();
            for (var i = 0; i < allHit.Length; i++)
            {
                var actor = allHit[i].transform.GetComponent<IActor>();
                if (actor != null)
                {
                    actors.Add(actor);
                }
            }
            actors.Sort();
            TargetSelector.Shared.Sort(actors, mCaster.GetActor().GetTransform().position);
            while (actors.Count > mSkillCfg.mMaxTargetCnt)
            {
                actors.RemoveAt(actors.Count - 1);
            }

            if (mTypeToHandlerList.TryGetValue(HandlerType.on_cast_target, out var list))
            {
                foreach (var handler in list)
                {
                    handler.OnEvt(this, CastTargetType.multi, list);
                }
            }
        }

        void OnBulletHitActor(IActor hitActor)
        {
            LogDC.LogEx("skill get actor", hitActor.GetTransform().gameObject.name);

            mHitCnt++;
            if (mHitCnt > mSkillCfg.mHitCnt)
            {
                return;
            }

            //side过滤
            if (mSkillCfg.mEffectSide.Contains(hitActor.GetActorSide()))
            {
                //生效事件
                if (mTypeToHandlerList.TryGetValue(HandlerType.on_cast_target, out var handleList))
                {
                    foreach (var handler in handleList)
                    {
                        handler.OnEvt(this, CastTargetType.single, hitActor);
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (mDoSkillEffectRemaingCnt > 0)
            {
                if (mHitCnt < mSkillCfg.mHitCnt)
                {
                    DoSkillEffectForTimer();
                }
                mDoSkillEffectRemaingCnt--;
            }

            if (mHitCnt >= mSkillCfg.mHitCnt && mSkillCfg.mDieAfterDone)
            {
                SkillSys.Instance.DestroySkill(this);
            }
        }

    }

}