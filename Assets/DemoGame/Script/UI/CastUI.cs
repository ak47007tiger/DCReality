using DC.GameLogic;
using DC.SkillSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DC.UI
{
    /// <summary>
    /// 施法目标选择，提示施法的目标、范围、方向
    /// 范围，一个圈
    /// 方向，箭头
    /// </summary>
    public class CastUI : BaseMonoBehaviour
    {
        public Image mImgRange;

        public Image mImgArrow;

        void Awake()
        {

        }

        void OnDestroy()
        {

        }

        public float WorldSizeToUiSize(float world)
        {
            return world;
        }

        public void ShowCastRange(SkillCfg skillCfg)
        {
            var uiScale = WorldSizeToUiSize(skillCfg.mCastRange);
            mImgRange.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
        }

        public void HideCastRange()
        {
            mImgRange.enabled = false;
        }

        public void ShowCastArrow(Vector3 dir, SkillCfg skillCfg)
        {
            mImgArrow.transform.forward = dir;
        }

        public void HideCastArrow()
        {
            mImgArrow.enabled = false;
        }

    }
}