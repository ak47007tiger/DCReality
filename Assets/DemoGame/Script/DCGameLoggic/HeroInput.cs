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
            //准备q技能 设置释放参数 or 直接释放
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var skillId = mHeroCfg.GetSkillId(KeyCode.Q);
                var skillCfg = GetSkillSystem().GetSkillCfg(skillId);
                //不需要目标 直接释放技能
                if (skillCfg.GetMaxTargetCnt() == 0 && !skillCfg.NeedDirection() && !skillCfg.NeedPosition())
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
                        var target = hit.transform.GetComponent<GameActor>();
                        if (null != target)
                        {
                            var castCfg = new CastCfg();
                            var targets = new List<IActor>();
                            targets.Add(target);
                            castCfg.SetTargetActors(targets);
                            mCaster.Cast(mSelectedSkillCfg, castCfg);

                            ClearSkill();

                            return;
                        }

                        //position or direction
                        if (mSelectedSkillCfg.NeedPosition())
                        {
                            var hitPos = hit.point;
                            var castCfg = new CastCfg();
                            castCfg.SetTargetPosition(Toolkit.FloatToInt(hitPos));
                            mCaster.Cast(mSelectedSkillCfg, castCfg);

                            ClearSkill();

                            return;
                        }

                        if (mSelectedSkillCfg.NeedDirection())
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

                            return;
                        }
                    }
                }
            }

            //1 非施法准备期 普攻 2 施法准备期 取消
            if (Input.GetMouseButtonDown(1))
            {
                //cancel skill
                if (mSelectedSkillCfg != null)
                {
                    ClearSkill();
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

    public interface IHeroCfg
    {
        int GetAttackRange();
        void SetAttackRange(int range);

        int GetPassiveSkillId();
        void SetPassiveSkillId(int id);

        List<int> GetSkillIds();
        void SetSkillIds(List<int> ids);

        int GetSkillId(KeyCode position);
        void SetSkillId(KeyCode position, int id);

        /// <summary>
        /// 换肤用
        /// </summary>
        /// <returns></returns>
        string GetModelPath();
        void SetModelPath(string path);

        string GetPrefabPath();
        void SetPrefabPath(string path);

        string GetName();
        void SetName(string name);

        string GetDesc();
        void SetDesc(string desc);
    }

    public class HeroCfg : IHeroCfg
    {
        private int mAttackRange;
        private int mPassiveSkillId;
        private List<int> mSkillList = new List<int>();
        private Dictionary<KeyCode, int> mKeyToSkillId = new Dictionary<KeyCode, int>();
        private string mModelPath;
        private string mPrefabPath;
        private string mName;
        private string mDesc;

        public int GetAttackRange()
        {
            return mAttackRange;
        }

        public void SetAttackRange(int range)
        {
            mAttackRange = range;
        }

        public int GetPassiveSkillId()
        {
            return mPassiveSkillId;
        }

        public void SetPassiveSkillId(int id)
        {
            mPassiveSkillId = id;
        }

        public List<int> GetSkillIds()
        {
            return mSkillList;
        }

        public void SetSkillIds(List<int> ids)
        {
            mSkillList = ids;
        }

        public int GetSkillId(KeyCode position)
        {
            if(mKeyToSkillId.TryGetValue(position, out var id))
            {
                return id;
            }

            return 0;
        }

        public void SetSkillId(KeyCode position, int id)
        {
            mKeyToSkillId[position] = id;
        }

        public string GetModelPath()
        {
            return mModelPath;
        }

        public void SetModelPath(string path)
        {
            mModelPath = path;
        }

        public string GetPrefabPath()
        {
            return mPrefabPath;
        }

        public void SetPrefabPath(string path)
        {
            mPrefabPath = path;
        }

        public string GetName()
        {
            return mName;
        }

        public void SetName(string name)
        {
            mName = name;
        }

        public string GetDesc()
        {
            return mDesc;
        }

        public void SetDesc(string desc)
        {
            mDesc = desc;
        }
    }

}