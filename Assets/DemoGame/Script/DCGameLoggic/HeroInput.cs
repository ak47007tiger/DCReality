using System.Collections.Generic;
using DC.ActorSystem;
using DC.SkillSystem;
using UnityEngine;

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

        private IHeroCfg mHeroCfg;

        private ISkillCfg mSelectedSkillCfg;
        private ICastCfg mCastCfg;

        private HeroInput_State mState;
        private HeroInput_State mIdle = new HeroInput_Idle();
        private HeroInput_State mDeployCast = new HeroInput_DeployCast();

        public void SetHeroCfg(IHeroCfg cfg)
        {
            mHeroCfg = cfg;
        }

        void Awake()
        {
            mGameActor = GetComponent<GameActor>();
            mCaster = GetComponent<Caster>();


        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //准备q技能
                var skillId = mHeroCfg.GetSkillId(KeyCode.Q);
                var skillCfg = GetSkillSystem().GetSkillCfg(skillId);
                //不需要目标 直接释放技能
                if (skillCfg.GetMaxTargetCnt() == 0 && !mSelectedSkillCfg.NeedDirection() && !mSelectedSkillCfg.NeedPosition())
                {
                    mCaster.Cast(skillCfg);
                }
                else
                {
                    
                }
            }

            //选择目标 or 方位
            if (Input.GetMouseButtonDown(0))
            {
                if (null != mSelectedSkillCfg)
                {

                }
            }

            //非施法准备期 普攻
            //施法准备期 取消
            if (Input.GetMouseButtonDown(1))
            {
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
                        mGameActor.Attack(targets);
                    }
                }
                else
                {

                }
            }
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

    public interface IHeroCfg
    {
        int GetPassiveSkillId();

        List<int> GetSkillIds();

        int GetSkillId(KeyCode position);

        string GetModelPath();

        string GetName();

        string GetDesc();
    }

}