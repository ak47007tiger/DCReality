using System.Collections.Generic;

namespace DC.SkillSystem
{
    interface IBuffCmpnt
    {
        void AddBuff(Buff buff);

        bool RemoveBuff(Buff buff);

        List<Buff> GetBuffList();

        bool Contains(Buff buff);
    }

    public class BuffCmpnt : IBuffCmpnt
    {
        private List<Buff> mBuffList = new List<Buff>();

        public void AddBuff(Buff buf)
        {
            mBuffList.Add(buf);
        }

        public bool RemoveBuff(Buff buff)
        {
            return mBuffList.Remove(buff);
        }

        public List<Buff> GetBuffList()
        {
            return mBuffList;
        }

        public bool Contains(Buff buff)
        {
            return mBuffList.Contains(buff);
        }
    }
}