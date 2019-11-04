using System;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class DCTimer : SingletonMono<DCTimer>
    {
        HashSet<DCBaseTimer> mTimerSet = new HashSet<DCBaseTimer>();
        HashSet<DCBaseTimer> mToDelTimerSet = new HashSet<DCBaseTimer>();

        /// <summary>
        /// 物理帧执行
        /// </summary>
        HashSet<DCBaseTimer> mPhysicTimerSet = new HashSet<DCBaseTimer>();
        HashSet<DCBaseTimer> mToDelPhysicTimerSet = new HashSet<DCBaseTimer>();

        List<Action> mNextFixedUpdate = new List<Action>();

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

        void FixedUpdate()
        {
            foreach (var timer in mPhysicTimerSet)
            {
                timer.Update();
            }

            if (mToDelPhysicTimerSet.Count > 0)
            {
                mTimerSet.RemoveWhere(Match);
            }

            foreach (var action in mNextFixedUpdate)
            {
                if (action != null) action();
            }
        }

        private bool Match(DCBaseTimer obj)
        {
            return mToDelTimerSet.Contains(obj);
        }

        public static void AddNormal(DCBaseTimer timer)
        {
            Instance.mTimerSet.Add(timer);
        }

        public static void RemoveNormal(DCBaseTimer timer)
        {
            if (null == Instance) return;

            Instance.mToDelTimerSet.Add(timer);
        }

        public static void AddPhysic(DCBaseTimer timer)
        {
            Instance.mPhysicTimerSet.Add(timer);
        }

        public static void RemovePhysic(DCBaseTimer timer)
        {
            Instance.mToDelPhysicTimerSet.Add(timer);
        }

        public static void RunNextFixedUpdate(Action action)
        {
            Instance.mNextFixedUpdate.Add(action);
        }
    }

    public abstract class DCBaseTimer
    {
        public Action mOnEnd;
        protected bool mPause;

        protected int mTargetLoop;
        protected int mTrackedLoop;

        protected bool mDestroyed;

        protected bool mAutoDestroy;

        protected bool mPhysic;

        public DCBaseTimer CreateNormal()
        {
            DCTimer.AddNormal(this);
            return this;
        }

        public void Destroy()
        {
            if (mDestroyed) return;
            mDestroyed = true;

            if (mPhysic)
            {
                DCTimer.RemovePhysic(this);
            }
            else
            {
                DCTimer.RemoveNormal(this);
            }
        }

        public DCBaseTimer SetAutoDestroy(bool auto)
        {
            mAutoDestroy = auto;
            return this;
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
            if (mTargetLoop > 0 && mTrackedLoop == mTargetLoop && mAutoDestroy)
            {
                Destroy();
                return;
            }

            OnUpdate();
        }

        protected abstract void OnUpdate();

        protected virtual void Invoke()
        {
            if (null != mOnEnd) mOnEnd();
        }
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

        public DCBaseTimer CreatePhysic()
        {
            mPhysic = true;
            DCTimer.AddPhysic(this);
            return this;
        }

        protected override void OnUpdate()
        {
            mTrackedCnt++;
            if (mTrackedCnt >= mTargetCnt)
            {
                mTrackedLoop++;

                Invoke();

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

                Invoke();

                //for next loop
                mTrackedDuration = 0;
            }
        }
    }
}