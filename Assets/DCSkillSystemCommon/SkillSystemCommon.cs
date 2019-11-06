using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;
using DC.GameLogic;
using DC.DCResourceSystem;
using Object = UnityEngine.Object;

namespace DC.SkillSystem
{
    /*
     技能
     装备
     角色

     buff

     数值计算

     end
     */

    public interface ISkillSystem
    {
        void Init();

        ISkill CreateSkill(SkillCfg cfg);

        void DestroySkill(ISkill skill);

        List<ISkill> GetActiveSkills();

        void AddSkill(ISkill skill);

        void RemoveSkill(ISkill skill);

        SkillCfg GetSkillCfg(int skillId);

        /// <summary>
        /// 释放设定
        /// 数值计算
        /// 视觉表现
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillCreate(ISkill skill);

        /// <summary>
        /// 跟踪技能对象 查询变量 触发各类条件
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillUpdate(ISkill skill);

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="skill"></param>
        void OnSkillComplete(ISkill skill);

        Buff CreateBuff(BuffCfg cfg);
    }

    public class SkillSys : Singleton<SkillSys>, ISkillSystem
    {
        private HashSet<ISkill> mAllActiveSkill = new HashSet<ISkill>();

        Dictionary<int, SkillCfg> mIdToSkillCfg = new Dictionary<int, SkillCfg>();

        public void Init()
        {
            var skillCfgs = ResourceSys.Instance.LoadAll<SkillCfg>(SystemPreset.path_skill_cfgs);
            foreach (var skillCfg in skillCfgs)
            {
                skillCfg.OnCreate();
                mIdToSkillCfg.Add(skillCfg.mId, skillCfg);
            }
        }

        public ISkill CreateSkill(SkillCfg cfg)
        {
            //create skill from prefab
            var skillPrefab = ResourceSys.Instance.Load<GameObject>(cfg.mPrefabPath);
            var skillGo = Object.Instantiate(skillPrefab);
            var skillInstance = skillGo.GetComponent<Skill>();
            skillInstance.SetSkillCfg(cfg);
            AddSkill(skillInstance);
            return skillInstance;
        }

        public void DestroySkill(ISkill skill)
        {
            if (null == skill)
            {
                return;
            }
            RemoveSkill(skill);
            Object.Destroy(skill.GetTransform().gameObject);
        }

        public List<ISkill> GetActiveSkills()
        {
            throw new NotImplementedException();
        }

        public void AddSkill(ISkill skill)
        {
            if (skill != null)
            {
                mAllActiveSkill.Add(skill);
            }
        }

        public void RemoveSkill(ISkill skill)
        {
            if (skill == null) return;

            mAllActiveSkill.Remove(skill);
        }

        public SkillCfg GetSkillCfg(int skillId)
        {
            if (mIdToSkillCfg.TryGetValue(skillId, out var cfg))
            {
                return cfg;
            }

            return null;
        }

        public void OnSkillCreate(ISkill skill)
        {
            var skillCfg = skill.GetSkillCfg();
            var fxAndTfNames = skillCfg.GetEffectAndTransformNames();
            if (null != fxAndTfNames)
            {
            }
        }

        public void OnSkillUpdate(ISkill skill)
        {
//            Physics.ClosestPoint()
        }

        public void OnSkillComplete(ISkill skill)
        {
        }

        public Buff CreateBuff(BuffCfg cfg)
        {
            throw new NotImplementedException();
        }
    }

    public enum SkillLifeCycle
    {
    }

    public enum SkillCompleteBehaviour
    {
        UpdateValue,
        AddBuff,
        ClearBuff,
        Dead,
    }

    public class Filters
    {
    }
}