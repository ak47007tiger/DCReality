using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
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
        List<IActor> GetTargetActors();
        void SetTargetActors(List<IActor> targets);

        Vector3Int GetDirection();
        void SetDirection(Vector3Int direction);

        Vector3Int GetTargetPosition();
        void SetTargetPosition(Vector3Int position);

        int GetPower();
        void SetPower(int power);

        List<int> GetExtParams();
        void SetExtParams(List<int> extParams);
    }

    public class CastCfg : ICastCfg
    {
        public List<IActor> mTargets;
        public Vector3Int mDirection;
        public Vector3Int mTargetPosition;
        public int mPower;
        public List<int> mExtParams;

        public List<IActor> GetTargetActors()
        {
            return mTargets;
        }

        public void SetTargetActors(List<IActor> targets)
        {
            mTargets = targets;
        }

        public Vector3Int GetDirection()
        {
            return mDirection;
        }

        public void SetDirection(Vector3Int direction)
        {
            mDirection = direction;
        }

        public Vector3Int GetTargetPosition()
        {
            return mTargetPosition;
        }

        public void SetTargetPosition(Vector3Int position)
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