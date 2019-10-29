using System;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class DCTimer : SingletonMono<DCTimer>
    {
        HashSet<DCBaseTimer> mTimerSet = new HashSet<DCBaseTimer>();
        HashSet<DCBaseTimer> mToDelTimerSet = new HashSet<DCBaseTimer>();

        void Update()
        {
            foreach (var timer in mTimerSet)
            {
                timer.Update();
            }

            if (mToDelTimerSet.Count > 0)
            {
                mTimerSet.RemoveWhere(Match);
            }
        }

        private bool Match(DCBaseTimer obj)
        {
            return mToDelTimerSet.Contains(obj);
        }

        public static void Add(DCBaseTimer timer)
        {
            Instance.mTimerSet.Add(timer);
        }

        public static void Remove(DCBaseTimer timer)
        {
            if (null == Instance) return;

            Instance.mToDelTimerSet.Add(timer);
        }
    }

    public abstract class DCBaseTimer
    {
        public Action mOnEnd;
        protected bool mPause;

        protected int mTargetLoop;
        protected int mTrackedLoop;

        protected bool mDestroyed;

        public DCBaseTimer Create()
        {
            DCTimer.Add(this);

            return this;
        }

        public void Destroy()
        {
            if (mDestroyed) return;
            mDestroyed = true;

            DCTimer.Remove(this);
        }

        public void SetPause(bool pause)
        {
            mPause = pause;
        }

        public bool IsPause()
        {
            return mPause;
        }

        public int Loop
        {
            get { return mTargetLoop; }
        }

        public int TrackedLoop
        {
            get { return mTrackedLoop; }
        }

        public void SetLoop(int loop)
        {
            mTargetLoop = loop;
        }

        public void Update()
        {
            if (mPause) return;

            //all loop completed
            if (mTargetLoop > 0 && mTrackedLoop == mTargetLoop)
            {
                Destroy();
                return;
            }

            OnUpdate();
        }

        protected abstract void OnUpdate();
    }

    public class DCFrameTimer : DCBaseTimer
    {

        public int mTargetCnt;
        private int mTrackedCnt;

        public DCFrameTimer(int cnt, Action onEnd, int loop = 1)
        {
            mTargetCnt = cnt;
            mOnEnd = onEnd;
            mTargetLoop = loop;
        }

        protected override void OnUpdate()
        {
            mTrackedCnt++;
            if (mTrackedCnt >= mTargetCnt)
            {
                mTrackedLoop++;

                if (null != mOnEnd) mOnEnd();

                //for next loop
                mTrackedCnt = 0;
            }
        }
    }

    public class DCDurationTimer : DCBaseTimer
    {
        private float mTargetDuration;
        private float mTrackedDuration;

        public DCDurationTimer(float duration, Action onEnd, int loop = 1)
        {
            mTargetDuration = duration;
            mOnEnd = onEnd;
            mTargetLoop = loop;
        }

        protected override void OnUpdate()
        {
            mTrackedDuration += Time.deltaTime;
            if (mTrackedDuration >= mTargetDuration)
            {
                mTrackedLoop++;

                if (null != mOnEnd) mOnEnd();

                //for next loop
                mTrackedDuration = 0;
            }
        }
    }
}