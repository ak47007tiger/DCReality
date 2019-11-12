using System;
using System.Collections.Generic;
using DC.Collections.Generic;
using UnityEngine;

namespace DC
{
    public class DCTimer : SingletonMono<DCTimer>
    {
        HashSet<DCBaseTimer> mTimerSet = new HashSet<DCBaseTimer>();
        HashSet<DCBaseTimer> mToDelTimerSet = new HashSet<DCBaseTimer>();
        List<DCBaseTimer> mTimersToInvoke = new List<DCBaseTimer>();

        /// <summary>
        /// 物理帧执行
        /// </summary>
        HashSet<DCBaseTimer> mPhysicTimerSet = new HashSet<DCBaseTimer>();
        HashSet<DCBaseTimer> mToDelPhysicTimerSet = new HashSet<DCBaseTimer>();
        List<DCBaseTimer> mPhysicTimersToInvoke = new List<DCBaseTimer>();

        List<Action> mNextFixedUpdate = new List<Action>();
        List<Action> mNextFixedUpdateToInvokes = new List<Action>();

        HashSet<ActionRecord> mActionRecords = new HashSet<ActionRecord>();
        HashSet<ActionRecord> mActionRecordsToDel = new HashSet<ActionRecord>();

        void Update()
        {
            //update 类型的timer
            //delete first
            if (mToDelTimerSet.Count > 0)
            {
                mTimerSet.RemoveWhere(MatchForUpdate);
                mToDelTimerSet.Clear();
            }

            if (mTimerSet.Count > 0)
            {
                mTimersToInvoke.AddRange(mTimerSet);
                LogDC.LogEx("timer count ", mTimersToInvoke.Count);
                foreach (var timer in mTimersToInvoke)
                {
                    timer.Update();
                }
                mTimersToInvoke.Clear();
            }

            //延时执行部分
            foreach (var actionRecord in mActionRecords)
            {
                actionRecord.Update();
                if (actionRecord.IsComplete())
                {
                    mActionRecordsToDel.Add(actionRecord);
                }
            }

            //防止action update的时候有往集合里面增加的操作
            if (mActionRecordsToDel.Count > 0)
            {
                mActionRecords.RemoveWhere(MatchDelRecord);
                foreach (var record in mActionRecordsToDel)
                {
                    record.Notify();
                }
                mActionRecordsToDel.Clear();
            }
            
        }

        private bool MatchDelRecord(ActionRecord record)
        {
            return mActionRecordsToDel.Contains(record);
        }

        void FixedUpdate()
        {
            //delete first
            if (mToDelPhysicTimerSet.Count > 0)
            {
                mPhysicTimerSet.RemoveWhere(MatchForFixedUpdate);
                mToDelPhysicTimerSet.Clear();
            }

            if (mPhysicTimerSet.Count > 0)
            {
                mPhysicTimersToInvoke.AddRange(mPhysicTimerSet);
                LogDC.LogEx("physic timer count ", mPhysicTimersToInvoke.Count);
                foreach (var timer in mPhysicTimersToInvoke)
                {
                    timer.Update();
                }
                mPhysicTimersToInvoke.Clear();
            }

            //avoid执行中添加新action
            mNextFixedUpdateToInvokes.AddRange(mNextFixedUpdate);
            mNextFixedUpdate.Clear();
            foreach (var action in mNextFixedUpdateToInvokes)
            {
                if (action != null) action();
            }
            mNextFixedUpdateToInvokes.Clear();
        }

        private bool MatchForUpdate(DCBaseTimer obj)
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
            LogDC.Log("add to remove");
            Instance.mToDelTimerSet.Add(timer);
        }

        private bool MatchForFixedUpdate(DCBaseTimer obj)
        {
            return mToDelPhysicTimerSet.Contains(obj);
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

        public static void RunAction(float delay, Action action)
        {
            Instance.mActionRecords.Add(new ActionRecord(delay, action));
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
            LogDC.Log("Destroy 1");
            if (mDestroyed) return;
            mDestroyed = true;
            LogDC.Log("Destroy 2");

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
            if (mTargetLoop > 0 && mTrackedLoop >= mTargetLoop && mAutoDestroy)
            {
                Destroy();
                return;
            }

            OnUpdate();
        }

        protected abstract void OnUpdate();

        protected virtual void Invoke()
        {
            if (null != mOnEnd)
            {
                mOnEnd();
            }
        }

        public abstract float GetPercentage();

        public float GetLoopPercentage()
        {
            return (float) mTrackedLoop / mTargetLoop;
        }

        public bool IsEnd()
        {
            return mTargetLoop > 0 && mTrackedLoop >= mTargetLoop;
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

        public override float GetPercentage()
        {
            return Mathf.Min((float) mTrackedCnt / mTargetCnt, 1);
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

        public override float GetPercentage()
        {
            return Mathf.Min(mTrackedDuration / mTargetDuration , 1);
        }
    }

    public class ActionRecord
    {
        private float mTickedDuration;
        public Action mAction;
        public float mDelay;

        public ActionRecord(float delay, Action action)
        {
            mDelay = delay;
            mAction = action;
        }

        public bool IsComplete()
        {
            return mTickedDuration >= mDelay;
        }

        public void Update()
        {
            mTickedDuration += Time.deltaTime;
        }

        public void Notify()
        {
            if (mAction != null) mAction();
        }
    }
}