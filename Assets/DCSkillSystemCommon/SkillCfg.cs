using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using ValueType = DC.ValueSys.ValueType;
using DC.DCPhysics;

namespace DC.SkillSystem
{

    public interface ISkillCfg
    {
        int GetId();

        int GetDuration();

        List<KeyValuePair<ValueType, int>> GetConsumes();

        List<KeyValuePair<ValueType, int>> GetValueEffects();

        SkillAnimationCfg GetAnimation();

        List<KeyValuePair<string, string>> GetEffectAndTransformNames();

        int GetMaxTargetCnt();

        bool NeedDirection();

        bool NeedPosition();
    }


    public class SkillCfg : ISkillCfg
    {
        private int mId;
        private int mDuration;
        private List<KeyValuePair<ValueType, int>> mConsumeList;
        private List<KeyValuePair<ValueType, int>> mValueEffects;
        private List<KeyValuePair<string, string>> mEffectAndTransformNames;
        private int mMaxTargetCnt;
        private bool mNeedDirection;
        private bool mNeedPosition;

        public int GetId()
        {
            return mId;
        }

        public int GetDuration()
        {
            return mDuration;
        }

        public List<KeyValuePair<ValueType, int>> GetConsumes()
        {
            return mConsumeList;
        }

        public List<KeyValuePair<ValueType, int>> GetValueEffects()
        {
            return mValueEffects;
        }

        public SkillAnimationCfg GetAnimation()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string>> GetEffectAndTransformNames()
        {
            return mEffectAndTransformNames;
        }

        public int GetMaxTargetCnt()
        {
            return mMaxTargetCnt;
        }

        public bool NeedDirection()
        {
            return mNeedDirection;
        }

        public bool NeedPosition()
        {
            return mNeedPosition;
        }
    }
}