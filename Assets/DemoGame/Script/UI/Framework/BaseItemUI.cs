using UnityEngine;

namespace DC.GameLogic.UI
{
    public class BaseItemUI<VGen> : BaseMonoBehaviour where VGen : MonoBehaviour
    {
        protected VGen mVGen;

        public VGen ViewGen
        {
            get
            {
                if (null == mVGen)
                {
                    mVGen = GetComponent<VGen>();
                }

                return mVGen;
            }
        }

        public object Param;
    }
}