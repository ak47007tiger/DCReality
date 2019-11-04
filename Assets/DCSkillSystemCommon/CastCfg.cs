using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{

    /// <summary>
    /// 释放技能的配置
    /// 位置，方向，目标
    /// 默认配置 + 运行时玩家修改的配置
    /// </summary>
    public interface ICastCfg
    {
    }

    public class CastCfg : ICastCfg
    {
        public List<IActor> mTargets;
        public Vector3 mDirection;
        public Vector3 mTargetPosition;
        /// <summary>
        /// 释放力度
        /// </summary>
        public int mPower;
        public List<int> mExtParams;

        public List<IActor> GetTargetActors()
        {
            return mTargets;
        }

        public bool IsTarget(IActor actor)
        {
            if (Toolkit.IsNullOrEmpty(mTargets)) return false;
            return mTargets.Contains(actor);
        }

        public IActor GetTarget()
        {
            if (Toolkit.IsNullOrEmpty(mTargets)) return null;
            return mTargets[0];
        }

        public void SetTargetActors(List<IActor> targets)
        {
            mTargets = targets;
        }

        public void SetDirection(Vector3 direction)
        {
            mDirection = direction;
        }

        public Vector3 GetTargetPosition()
        {
            return mTargetPosition;
        }

        public void SetTargetPosition(Vector3 position)
        {
            mTargetPosition = position;
        }

        public int GetPower()
        {
            return mPower;
        }

        public void SetPower(int power)
        {
            mPower = power;
        }

        public List<int> GetExtParams()
        {
            return mExtParams;
        }

        public void SetExtParams(List<int> extParams)
        {
            mExtParams = extParams;
        }
    }
}