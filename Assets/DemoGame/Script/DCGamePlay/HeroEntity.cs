using System;
using System.Collections.Generic;
using DC.ActorSystem;
using DC.AI;
using DC.SkillSystem;
using DC.UI;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    /// <summary>
    /// 选择技能
    /// 选择目标
    /// 一个hero对象
    /// </summary>
    public class HeroEntity : GameElement
    {
        public HeroCfg mHeroCfg;

        private SkillCfg mSelectedSkillCfg;
        private CastCfg mCastCfg;
        private RaycastHit mCastHit;

        public DCFSM HeroFsm;

        public CastSkillUI mCastSkillUi;

        public HeroMoveComponent MoveCmpt;

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            if (Actor.IsPlayer())
            {
                MsgSys.Add<KeyCode>(GameEvent.KeyCodeEvt, OnKeyEvent);
                CreateCastUI();
            }

            Actor.GetBuffCmpt().AddOnBuffAddListener(OnAddBuff);
            Actor.GetBuffCmpt().AddOnRemoveAddListener(OnRemoveBuff);
        }

        /*public void SetFSM(AnimatorController animatorController)
        {
            mHeroFsm = AnimatorToFSM.Instance.ConvertHero(animatorController, gameObject);
        }*/

        public DCFSMState CreateDCFSMState(int state)
        {
            var enumState = (EnumHeroState)state;
            var type = Type.GetType(string.Format("DC.AI.{0}", enumState.ToString()));
            var instance = (BaseHeroState)Activator.CreateInstance(type);
            //todo d.c set up entity
            return instance;
        }

        public void CreateCastUI()
        {
            mCastSkillUi = UIManager.Instance.CreateUiInstance<CastSkillUI>(CacheTransform);
            mCastSkillUi.CacheTransform.localPosition =
                UIManager.Instance.LoadPrefab<CastSkillUI>().transform.localPosition;
        }

        private void OnAddBuff(Buff buff)
        {
            if (null != HeroFsm)
            {
                HeroFsm.CurrentState.Reason(new BuffEvt(buff, BuffOperate.Add));
                HeroFsm.CurrentState.Act();
            }
        }

        public void OnRemoveBuff(Buff buff)
        {
            if (null != HeroFsm)
            {
                HeroFsm.CurrentState.Reason(new BuffEvt(buff, BuffOperate.Remove));
                HeroFsm.CurrentState.Act();
            }
        }

        void OnDestroy()
        {
            if (Actor.IsPlayer())
            {
                MsgSys.Remove<KeyCode>(GameEvent.KeyCodeEvt, OnKeyEvent);
            }
        }

        public void SetForward(Vector3 forward)
        {
            CacheTransform.forward = forward;
        }

        void Update()
        {
            if (!Actor.IsPlayer())
            {
                return;
            }

            HandleSkillKey();
            HandleLeftMouseBtn();
            HandleRightMouseBtn();
        }

        void FixedUpdate()
        {
            EmitFSM();
        }

        private void EmitFSM(){
          if (null != HeroFsm)
          {
              HeroFsm.CurrentState.Reason();
              HeroFsm.CurrentState.Act();
          }
        }

        #region skill key code

        private void HandleSkillKey()
        {
            if (mHeroCfg == null)
            {
                return;
            }

            foreach (var code in mHeroCfg.GetSkillKeyList())
            {
                //准备技能 设置释放参数 or 直接释放
                if (Input.GetKeyDown(code))
                {
                    OnKeyEvent(code);
                    break;
                }
            }
        }

        public void OnKeyEvent(KeyCode code)
        {
            LogDC.LogEx("press  ", code);

            var currentSkill = Caster.GetSkill(code);

            int skillId;
            mCastCfg = new CastCfg();
            mCastCfg.mFromKey = code;

            if (currentSkill != null)
            {
                skillId = mHeroCfg.GetNextSkill(code, currentSkill.GetSkillCfg().mId);
                mCastCfg.mIsSubSkill = true;
            }
            else
            {
                skillId = mHeroCfg.GetSkillId(code);
            }

            var skillCfg = SkillConfigMgr.Instance.GetSkillCfg(skillId);
            if (null == skillCfg)
            {
                LogDC.LogEx("no such skill: ", skillId);
                return;
            }

            //不需要目标 直接释放技能
            if (skillCfg.mTargetType == SkillTargetType.None)
            {
                mSelectedSkillCfg = skillCfg;
                // Caster.Cast(skillCfg, mCastCfg);
                EmitFSM();
            }
            else
            {
                //选中某个技能 准备调参
                mSelectedSkillCfg = skillCfg;
                if (null != mCastSkillUi)
                {
                    mCastSkillUi.OnPrepareCast(mSelectedSkillCfg);
                }
                LogDC.LogEx("prepare skill ", skillId);
            }
        }

        #endregion

        public CastCfg GetCastCfg(){
          return mCastCfg;
        }

        #region set target

        public RaycastHit mTargetHit;

        private void HandleLeftMouseBtn()
        {
            //选择目标 or 方位
            if (Input.GetMouseButtonDown(0))
            {
                if (null != mSelectedSkillCfg)
                {
                    LogDC.Log("try get target");
                    var mPos = Input.mousePosition;
                    var ray = Camera.main.ScreenPointToRay(mPos);
                    if (Physics.Raycast(ray, out mTargetHit, 100))
                    {
                        PrepareCastSelectedSkill(mSelectedSkillCfg, mTargetHit.transform, mTargetHit.point);
                    }
                }
            }
        }

        public void OnLostActor(GameActor actor)
        {
            //todo d.c 如果是施法目标则停止施法
            if (GetCastCfg() != null)
            {

            }
        }

        private void PrepareCastSelectedSkill(SkillCfg selectedSkillCfg, Transform targetTf, Vector3 hitPos)
        {
            switch (selectedSkillCfg.mTargetType)
            {
                case SkillTargetType.Actor:
                    {
                        var target = targetTf.GetComponent<GameActor>();
                        if (null != target)
                        {
                            LogDC.Log("find actor");
                            var distance = Toolkit.ComputeDistance(targetTf, CacheTransform);
                            if (distance > selectedSkillCfg.mCastRange)
                            {
                                LogDC.Log("try catch actor");
                                MoveCmpt.Move(MoveType.NavTarget, target.CacheTransform, mHeroCfg.mSpeed, selectedSkillCfg.mCastRange);
                                Actor.TryCatch(target.GetTransform(), selectedSkillCfg.mCastRange, OnCatchActor);
                                return;
                            }

                            CastSelectedSkill(target);
                        }
                    }
                    break;
                case SkillTargetType.Direction:
                    {
                        //计算一个方向，并且平行于地面。让y值都为0
                        hitPos.y = 0;
                        var playerPos = CacheTransform.position;
                        playerPos.y = 0;

                        var rawDirection = (hitPos - playerPos).normalized;
                        var castCfg = mCastCfg;
                        castCfg.SetDirection(rawDirection);
                        Caster.Cast(selectedSkillCfg, castCfg);

                        ClearSkill();
                    }
                    break;
                case SkillTargetType.Position:
                    {
                        var playerPos = CacheTransform.position;
                        hitPos.y = 0;
                        mLastCastPosition = hitPos;
                        var distance = Vector3.Distance(hitPos, playerPos);
                        if (distance > selectedSkillCfg.mCastRange)
                        {
                            var targetPos = Vector3.MoveTowards(playerPos, hitPos, distance - mSelectedSkillCfg.mCastRange);
                            Actor.TryArrive(targetPos, 0.1f, OnArrive);
                            return;
                        }

                        var castCfg = mCastCfg;
                        castCfg.SetTargetPosition(hitPos);
                        Caster.Cast(selectedSkillCfg, castCfg);

                        ClearSkill();
                    }
                    break;
            }
        }

        #endregion

        private Vector3 mLastCastPosition;
        private void OnArrive(NavArrivePosition arrive, float distance)
        {
            arrive.mOnCatchTarget = null;

            var castCfg = new CastCfg();
            castCfg.SetTargetPosition(mLastCastPosition);
            Caster.Cast(mSelectedSkillCfg, castCfg);
        }

        private void OnCatchActor(NavTraceTarget tracer, float distance)
        {
            tracer.mOnCatchTarget = null;

            CastSelectedSkill(tracer.mTargetTf.GetComponent<IActor>());
        }

        private void CastSelectedSkill(IActor target)
        {
            var castCfg = mCastCfg;
            var targets = new List<IActor>();
            targets.Add(target);
            castCfg.SetTargetActors(targets);
            Caster.Cast(mSelectedSkillCfg, castCfg);

            ClearSkill();
        }

        public void ToState(EnumHeroTrans trans)
        {
            HeroFsm.PerformTransition(trans.ToString().GetExtHashCode());
        }

        #region right mouse btn
        private void HandleRightMouseBtn()
        {
            //1 非施法准备期 普攻 2 施法准备期 取消
            if (Input.GetMouseButtonDown(1))
            {
                LogDC.Log("right btn click");

                //cancel skill
                if (IsPreparingCast())
                {
                    LogDC.Log("cancel cast");
                    StopPreparingCast();
                    return;
                }

                //normal attack
                var mPos = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(mPos);
                if (Physics.Raycast(ray, out mTargetHit, 100))
                {
                    LogDC.LogEx("cast obj ", mTargetHit.transform.gameObject.name);
                    
                    var target = mTargetHit.transform.GetComponent<GameActor>();
                    //是目标
                    if (target != null)
                    {
                        var skillId = mHeroCfg.GetSkillId(KeyCode.A);
                        var skillCfg = SkillConfigMgr.Instance.GetSkillCfg(skillId);
                        mSelectedSkillCfg = skillCfg;
                        PrepareCastSelectedSkill(mSelectedSkillCfg, mTargetHit.transform, Vector3.zero);
                    }
                }
            }
        }
        #endregion

        public bool IsPreparingCast()
        {
            return mSelectedSkillCfg != null;
        }

        public SkillCfg GetSelectedSkillCfg()
        {
            return mSelectedSkillCfg;
        }

        public void StopPreparingCast()
        {
            ClearSkill();
        }

        public void ClearSkill()
        {
            if (null != mCastSkillUi)
            {
                mCastSkillUi.OnCastEnd();
            }

            mSelectedSkillCfg = null;
            mCastCfg = null;
        }

    }

}
