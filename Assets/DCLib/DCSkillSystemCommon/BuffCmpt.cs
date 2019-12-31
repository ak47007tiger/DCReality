using System;
using System.Collections.Generic;
using DC.GameLogic;

namespace DC.SkillSystem
{
    interface IBuffCmpnt
    {
        void AddBuff(Buff buff);

        bool RemoveBuff(Buff buff);

        List<Buff> GetBuffList();

        bool Contains(Buff buff);
    }

    public class BuffCmpt : IBuffCmpnt
    {
        private List<Buff> mBuffList = new List<Buff>();

        private BuffCfg mLastAddBuffCfg;
        private BuffCfg mLastRemoveBuffCfg;

        private event Action<Buff> mOnAddListeners;
        private event Action<Buff> mOnRemoveListeners;

        public GameActor OwnActor { get; set; }

        public void AddOnBuffAddListener(Action<Buff> listener)
        {
            mOnAddListeners += listener;
        }

        public void AddOnRemoveAddListener(Action<Buff> listener)
        {
            mOnRemoveListeners -= listener;
        }

        public void AddBuff(Buff buff)
        {
            //todo 相同类型的buff不重复添加
            //todo 策略1 在加的时候过滤掉；
            //todo 策略2 在加的时候不过滤但是查询的时候只给出效果最强的buff
            //mBuffList.Add(buff);
            //mLastAddBuffCfg = buff.mBuffCfg;
            //buff.OnAttach(OwnActor);
            
            //if (null != mOnAddListeners)
            //{
            //    mOnAddListeners(buff);
            //}
        }

        public bool RemoveBuff(Buff buff)
        {
            var removeBuff = mBuffList.Remove(buff);
            if (removeBuff)
            {
                mLastRemoveBuffCfg = buff.mBuffCfg;
            }
            if (null != mOnRemoveListeners)
            {
                mOnRemoveListeners(buff);
            }
            return removeBuff;
        }

        public List<Buff> GetBuffList()
        {
            return mBuffList;
        }

        public bool Contains(Buff buff)
        {
            return mBuffList.Contains(buff);
        }

        public bool Contains(BuffType bufType)
        {
            return mBuffList.Find((item) => item.mBuffCfg.mBuffType == bufType) != null;
        }

        public BuffCfg GetLastAddBuffCfg()
        {
            return mLastAddBuffCfg;
        }

        public BuffCfg GetLastRemoveBuffCfg()
        {
            return mLastRemoveBuffCfg;
        }

        public void OnUpdate()
        {
            foreach (var buff in mBuffList)
            {
                buff.OnUpdate();
            }
        }

    }

}