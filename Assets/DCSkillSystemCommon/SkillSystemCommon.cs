using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
using DC.DCPhysics;

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
        ISkill CreateSkill(ISkillCfg cfg);

        List<ISkill> GetActiveSkills();

        void AddSkill(ISkill skill);

        void RemoveSkill(ISkill skill);

        ISkillCfg GetSkillCfg(int id);

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

        IBuff CreateBuff(IBuffCfg cfg);
    }

    public class SkillSystem : ISkillSystem
    {
        public ISkill CreateSkill(ISkillCfg cfg)
        {
            throw new NotImplementedException();
        }

        public List<ISkill> GetActiveSkills()
        {
            throw new NotImplementedException();
        }

        public void AddSkill(ISkill skill)
        {
            throw new NotImplementedException();
        }

        public void RemoveSkill(ISkill skill)
        {
            throw new NotImplementedException();
        }

        public ISkillCfg GetSkillCfg(int id)
        {
            throw new NotImplementedException();
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

        public IBuff CreateBuff(IBuffCfg cfg)
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

    

    public interface SkillAnimationCfg
    {

    }

    public class Filters
    {
    }

}