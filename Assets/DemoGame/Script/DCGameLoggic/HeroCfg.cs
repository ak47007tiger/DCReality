using System.Collections.Generic;
using UnityEngine;

namespace DC.GameLogic
{

    [CreateAssetMenu(fileName = "HeroCfg", menuName = "DC/ScriptableObjects/HeroCfg", order = 1)]
    public class HeroCfg : ScriptableObject
    {
        public int mAttackRange;
        public int mPassiveSkillId;
        public List<int> mSkillList = new List<int>();

        public List<KeyValuePair<KeyCode,int>> mKeyToSkillPairList = new List<KeyValuePair<KeyCode, int>>();

        public Dictionary<KeyCode, int> mKeyToSkillId = new Dictionary<KeyCode, int>();
        /// <summary>
        /// 换肤用
        /// </summary>
        /// <returns></returns>
        public string mModelPath;
        public string mPrefabPath;
        public string mName;
        public string mDesc;

        public void BuildDerivedData()
        {
            mKeyToSkillId.Clear();
            Toolkit.PairListToDictionary(mKeyToSkillPairList, mKeyToSkillId);
        }

        public int GetSkillId(KeyCode position)
        {
            if (mKeyToSkillId.TryGetValue(position, out var id))
            {
                return id;
            }

            return 0;
        }
    }

}
