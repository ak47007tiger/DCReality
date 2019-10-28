using System;
using System.Collections.Generic;
using DC.Collections.Generic;
using UnityEngine;

namespace DC.GameLogic
{
    [Serializable]
    public class KeyToKill : KVPair<KeyCode, int>
    {
        public KeyToKill(KeyCode key, int value) : base(key, value)
        {
        }
    }

    [CreateAssetMenu(fileName = "HeroCfg", menuName = "DC/ScriptableObjects/HeroCfg", order = 1)]
    public class HeroCfg : ScriptableObject
    {
        public int mPassiveSkillId;
        public List<int> mSkillList = new List<int>();
        public List<KeyToKill> mKeyToSkillPairList = new List<KeyToKill>();

        public Dictionary<KeyCode, int> mKeyToSkillId = new Dictionary<KeyCode, int>();
        /// <summary>
        /// 换肤用
        /// </summary>
        /// <returns></returns>
        public string mModelPath;
        public string mPrefabPath;
        public string mName;
        public string mDesc;

        public float mSpeed;

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
