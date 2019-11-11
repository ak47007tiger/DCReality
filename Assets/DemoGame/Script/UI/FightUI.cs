using System.Collections;
using System.Collections.Generic;
using DC.SkillSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DC
{
    public class FightUI : BaseMonoBehaviour
    {
        public Image mMousePoint;

        void Awake()
        {
        }

        void Update()
        {
            CacheRectTransform.anchoredPosition = Input.mousePosition;
        }

        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        void OnMouseEnter()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    /*
     *
     */

    /// <summary>
    /// cd
    /// 按键发送
    /// 技能图标
    /// </summary>
    public class SkillItemUI : BaseMonoBehaviour
    {
        public KeyCode mSendKeyCode;

        public Skill mCurrentSkill;

        /// <summary>
        /// 刚释放是1，cd完0
        /// </summary>
        /// <returns></returns>
        public float GetCdPercentage()
        {
            if (mCurrentSkill == null)
            {
                return 0;
            }

            var p = mCurrentSkill.GetTickedLife() / mCurrentSkill.GetSkillCfg().mCdDuration;
            p = Mathf.Max(1, p);
            return 1 - p;
        }

        public void OnSkillCreate(Skill skill)
        {
            mCurrentSkill = skill;
        }

        public void DoCdAnimation()
        {

        }

        public void SetIcon(Sprite sprite)
        {

        }

        public void SetSilence(bool silence)
        {

        }

        public void OnIconClick()
        {
            
        }
    }
}