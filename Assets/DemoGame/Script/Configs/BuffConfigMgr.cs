using System.Collections.Generic;
using DC.Collections.Generic;
using DC.DCResourceSystem;
using DC.SkillSystem;

namespace DC.GameLogic
{
    public class BuffConfigMgr : Singleton<BuffConfigMgr>
    {
        Dictionary<int, BuffCfg> mIdToBuffCfg = new Dictionary<int, BuffCfg>();

        List<BuffCfg> mBuffCfgs = new List<BuffCfg>();

        Dictionary<int, Buff> mIdToBuff = new Dictionary<int, Buff>();

        public void Init()
        {
            var path = "Configs/Hero";
            mBuffCfgs.AddRange(ResourceSys.Instance.LoadAll<BuffCfg>(path));
        }

        public BuffCfg GetBuffCfg(int id)
        {
            return mIdToBuffCfg.GetValEx(id);
        }

        public Buff GetBuff(int id)
        {
            return mIdToBuff.GetValEx(id);
        }
    }
}