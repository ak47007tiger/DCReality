using UnityEngine;
using UnityEngine.UI;

namespace DC.UI
{
    /// <summary>
    /// 处理技能目标的选择 普攻，指向性技能
    /// 目标，一个准星
    /// </summary>
    public class TargetUI : BaseUI
    {

        public Image mImgTarget;

        public void ShowCastTarget(Vector3 position)
        {
            mImgTarget.transform.position = position;
        }

        public void HideCastTarget(bool show)
        {
            mImgTarget.enabled = false;
        }
    }
}