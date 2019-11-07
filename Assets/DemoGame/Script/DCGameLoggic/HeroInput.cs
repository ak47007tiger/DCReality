using System.Collections.Generic;
using DC.ActorSystem;
using DC.AI;
using DC.SkillSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    /// <summary>
    /// 选择技能
    /// 选择目标
    /// 更像是一个hero对象
    /// </summary>
    public class HeroInput : GameElement
    {
        public HeroCfg mHeroCfg;

        private SkillCfg mSelectedSkillCfg;
        private CastCfg mCastCfg;

        private HeroInput_State mState;
        private HeroInput_State mIdle = new HeroInput_Idle();
        private HeroInput_State mDeployCast = new HeroInput_DeployCast();

        protected override void Awake()
        {
            base.Awake();
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
                    var skillId = mHeroCfg.GetSkillId(code);
                    var skillCfg = GetSkillSystem().GetSkillCfg(skillId);

                    LogDC.LogEx("press  ", code);

                    //不需要目标 直接释放技能
                    if (skillCfg.mTargetType == SkillTargetType.None)
                    {
                        Caster.Cast(skillCfg);
                    }
                    else
                    {
                        //选中某个技能 准备调参
                        mSelectedSkillCfg = skillCfg;
                        mCastCfg = new CastCfg();
                        LogDC.LogEx("prepare skill ", skillId);
                    }
                }
            }
        }

        #endregion

        #region set target

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
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        PrepareCastSelectedSkill(mSelectedSkillCfg, hit.transform, hit.point);
                    }
                }
            }
        }

        private void PrepareCastSelectedSkill(SkillCfg selectedSkillCfg, Transform targetTf, Vector3 hitPos)
        {
            switch (selectedSkillCfg.mTargetType)
            {
                case SkillTargetType.Actor:
                    {
                        var target = targetTf.GetComponent<IActor>();
                        if (null != target)
                        {
                            LogDC.Log("find actor");
                            var distance = Toolkit.ComputeDistance(targetTf, CacheTransform);
                            if (distance > selectedSkillCfg.mCastRange)
                            {
                                LogDC.Log("try catch actor");
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
                        var castCfg = new CastCfg();
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

                        var castCfg = new CastCfg();
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
            var castCfg = new CastCfg();
            castCfg.SetTargetPosition(mLastCastPosition);
            Caster.Cast(mSelectedSkillCfg, castCfg);
        }

        private void OnCatchActor(NavTraceTarget tracer, float distance)
        {
            CastSelectedSkill(tracer.mTargetTf.GetComponent<IActor>());
        }

        private void CastSelectedSkill(IActor target)
        {
            var castCfg = new CastCfg();
            var targets = new List<IActor>();
            targets.Add(target);
            castCfg.SetTargetActors(targets);
            Caster.Cast(mSelectedSkillCfg, castCfg);

            ClearSkill();
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
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    LogDC.LogEx("cast obj ", hit.transform.gameObject.name);
                    
                    var target = hit.transform.GetComponent<GameActor>();
                    //是目标
                    if (target != null)
                    {
                        var skillId = mHeroCfg.GetSkillId(KeyCode.A);
                        var skillCfg = GetSkillSystem().GetSkillCfg(skillId);
                        mSelectedSkillCfg = skillCfg;
                        PrepareCastSelectedSkill(mSelectedSkillCfg, hit.transform, Vector3.zero);
                    }
                }
            }
        }
        #endregion

        public bool IsPreparingCast()
        {
            return mSelectedSkillCfg != null;
        }

        public void StopPreparingCast()
        {
            ClearSkill();
        }

        public void ClearSkill()
        {
            mSelectedSkillCfg = null;
            mCastCfg = null;
        }

        public bool Attack()
        {
            return false;
        }

        void SetState(HeroInput_State state)
        {
            mState = state;
        }

        class HeroInput_State
        {
            protected virtual void Update()
            {
            }
        }

        class HeroInput_Idle : HeroInput_State
        {
        }

        class HeroInput_DeployCast : HeroInput_State
        {
        }
    }
}