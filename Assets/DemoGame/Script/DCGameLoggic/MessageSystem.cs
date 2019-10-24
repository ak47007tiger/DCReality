using System;
using System.Collections.Generic;

namespace DC.GameLogic
{
    class Handler
    {
        private Delegate mDelegate;

        private readonly HashSet<Delegate> mDelegates = new HashSet<Delegate>();

        public Handler()
        {
        }

        public Handler(int type)
        {
            EvtType = type;
        }

        public int EvtType { get; set; }

        public void Add(Delegate d)
        {
            if (mDelegates.Contains(d))
            {
                return;
            }

            mDelegates.Add(d);
            mDelegate = Delegate.Combine(mDelegate, d);
        }

        public void Remove(Delegate d)
        {
            if (mDelegates.Remove(d))
            {
                mDelegate = Delegate.Remove(mDelegate, d);
            }
        }

        public Delegate GetDelegate()
        {
            return mDelegate;
        }

        public void Call(params object[] objs)
        {
            if (null != mDelegate)
            {
                mDelegate.DynamicInvoke(objs);
            }
        }
    }

    public class MsgSys : Singleton<MsgSys>
    {
        public void Send(GameEvent evt, params object[] objs)
        {
            MessageSystem.Instance.Send((int) evt, objs);
        }

        public void AddDelegate(GameEvent type, Delegate action)
        {
            MessageSystem.Instance.AddDelegate((int) type, action);
        }

        public void Add(GameEvent type, Action action)
        {
            AddDelegate(type, action);
        }

        public void Add<T>(GameEvent type, Action<T> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2>(GameEvent type, Action<T1, T2> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3>(GameEvent type, Action<T1, T2, T3> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3, T4>(GameEvent type, Action<T1, T2, T3, T4> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3, T4, T5>(GameEvent type, Action<T1, T2, T3, T4, T5> action)
        {
            AddDelegate(type, action);
        }
    }

    public class MessageSystem : Singleton<MessageSystem>
    {
        Dictionary<int, Handler> mIdToDelegate = new Dictionary<int, Handler>();

        private Handler GetOrCreateHandler(int type)
        {
            if (!mIdToDelegate.TryGetValue(type, out var handler))
            {
                handler = new Handler(type);
                mIdToDelegate.Add(type, handler);
            }

            return handler;
        }

        private Handler GetHandler(int type)
        {
            if (mIdToDelegate.TryGetValue(type, out var handler))
            {
                return handler;
            }

            return null;
        }

        public void AddDelegate(int type, Delegate action)
        {
            GetOrCreateHandler(type).Add(action);
        }

        public void Add(int type, Action action)
        {
            AddDelegate(type, action);
        }

        public void Add<T>(int type, Action<T> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2>(int type, Action<T1, T2> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3>(int type, Action<T1, T2, T3> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3, T4>(int type, Action<T1, T2, T3, T4> action)
        {
            AddDelegate(type, action);
        }

        public void Add<T1, T2, T3, T4, T5>(int type, Action<T1, T2, T3, T4, T5> action)
        {
            AddDelegate(type, action);
        }

        public void Send(int type, params object[] paramArray)
        {
            var handler = GetHandler(type);
            if (null != handler)
            {
                handler.Call(paramArray);
            }
        }

//        public void Dispatch<T>(int type, T t)
//        {
//        }
//
//        public void Dispatch<T1, T2>(int type, T1 t1, T2 t2)
//        {
//        }
//
//        public void Dispatch<T1, T2, T3>(int type, T1 t1, T2 t2, T3 t3)
//        {
//        }
//
//        public void Dispatch<T1, T2, T3, T4>(int type, T1 t1, T2 t2, T3 t3, T4 t4)
//        {
//        }
//
//        public void Dispatch<T1, T2, T3, T4, T5>(int type, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
//        {
//        }
    }
}