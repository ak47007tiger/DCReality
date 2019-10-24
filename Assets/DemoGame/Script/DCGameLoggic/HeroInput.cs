using System.Collections.Generic;
using DC.ActorSystem;
using DC.SkillSystem;
using UnityEngine;
using UnityEngine.AI;

namespace DC.GameLogic
{
    /// <summary>
    /// 选择技能
    /// 选择目标
    /// </summary>
    public class HeroInput : GameElement
    {
        private IActor mGameActor;
        private ICaster mCaster;

        public HeroCfg mHeroCfg;

        private SkillCfg mSelectedSkillCfg;
        private CastCfg mCastCfg;

        private HeroInput_State mState;
        private HeroInput_State mIdle = new HeroInput_Idle();
        private HeroInput_State mDeployCast = new HeroInput_DeployCast();

        public NavMeshAgent mMeshAgent;

        List<KeyCode> mSkillKeyCodeList = new List<KeyCode>();

        void Awake()
        {
            mGameActor = GetComponent<GameActor>();
            mCaster = GetComponent<Caster>();
        }

        public void SetForward(Vector3 forward)
        {
            CacheTransform.forward = forward;
        }

        public void MoveTo(Vector3 position)
        {
            SetForward((position - CacheTransform.position).normalized);
            mMeshAgent.destination = position;
        }

        void Update()
        {
            HandleSkillKey();
            HandleLeftMouseBtn();
            HandleRightMouseBtn();
        }

        #region skill key code

        private void HandleSkillKey()
        {
            foreach (var code in mSkillKeyCodeList)
            {
                //准备技能 设置释放参数 or 直接释放
                if (Input.GetKeyDown(code))
                {
                    var skillId = mHeroCfg.GetSkillId(code);
                    var skillCfg = GetSkillSystem().GetSkillCfg(skillId);
                    //不需要目标 直接释放技能
                    if (skillCfg.mTargetType == SkillTargetType.None)
                    {
                        mCaster.Cast(skillCfg);
                    }
                    else
                    {
                        //选中某个技能 准备调参
                        mSelectedSkillCfg = skillCfg;
                        mCastCfg = new CastCfg();
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
                    var mPos = Input.mousePosition;
                    var ray = Camera.main.ScreenPointToRay(mPos);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        switch (mSelectedSkillCfg.mTargetType)
                        {
                            case SkillTargetType.Actor:
                            {
                                var target = hit.transform.GetComponent<GameActor>();
                                var distance = Toolkit.ComputeDistance(target, CacheTransform);
                                if (distance > mSelectedSkillCfg.mCastRange)
                                {
                                    mGameActor.TryCatch(target, mSelectedSkillCfg.mCastRange, OnCatchActor);
                                    return;
                                }

                                if (null != target)
                                {
                                    CastSelectedSkill(target);
                                }
                            }
                                break;
                            case SkillTargetType.Direction:
                            {
                                //计算一个方向，并且平行于地面。让y值都为0
                                var hitPos = hit.point;
                                hitPos.y = 0;
                                var playerPos = CacheTransform.position;
                                playerPos.y = 0;
                                var rawDirection = hitPos - playerPos;
                                var castCfg = new CastCfg();
                                castCfg.SetDirection(Toolkit.FloatToInt(rawDirection));
                                mCaster.Cast(mSelectedSkillCfg, castCfg);

                                ClearSkill();
                            }
                                break;
                            case SkillTargetType.Position:
                            {
                                var hitPos = hit.point;
                                var castCfg = new CastCfg();
                                castCfg.SetTargetPosition(Toolkit.FloatToInt(hitPos));
                                mCaster.Cast(mSelectedSkillCfg, castCfg);

                                ClearSkill();
                            }
                                break;
                        }
                    }
                }
            }
        }

        #endregion


        private void OnCatchActor(IActor actor, float distance)
        {
            CastSelectedSkill(actor);
        }

        private void CastSelectedSkill(IActor target)
        {
            var castCfg = new CastCfg();
            var targets = new List<IActor>();
            targets.Add(target);
            castCfg.SetTargetActors(targets);
            mCaster.Cast(mSelectedSkillCfg, castCfg);

            ClearSkill();
        }

        #region right mouse btn
        private void HandleRightMouseBtn()
        {
            //1 非施法准备期 普攻 2 施法准备期 取消
            if (Input.GetMouseButtonDown(1))
            {
                //cancel skill
                if (IsPreparingCast())
                {
                    StopPreparingCast();
                    return;
                }

                //normal attack
                var mPos = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(mPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    var target = hit.transform.GetComponent<GameActor>();
                    //是目标
                    if (target != null)
                    {
                        var targets = new List<IActor>();
                        targets.Add(target);

                        var skillCfg = GetSkillSystem().GetSkillCfg(mHeroCfg.GetSkillId(KeyCode.A));
                        var castCfg = new CastCfg();
                        castCfg.SetTargetActors(targets);
                        mCaster.Cast(skillCfg, castCfg);
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