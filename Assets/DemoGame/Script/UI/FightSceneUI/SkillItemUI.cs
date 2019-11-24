using DC.DCResourceSystem;
using DC.GameLogic;
using DC.GameLogic.UI;
using DC.SkillSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DC.UI
{
    /// <summary>
    /// cd
    /// 按键发送
    /// 技能图标
    /// 某种按键的技能生效时发送消息，这里接受，同步显示信息到ui
    /// </summary>
    public class SkillItemUI : BaseItemUI<SkillItemUIGen>
    {
        public KeyCode mSendKeyCode;

        [HideInInspector]
        public SkillCfg mSkillCfg;

        private DCDurationTimer mWaitCdTimer;

        private DCDurationTimer mCdTimer;

        void Start()
        {
            ViewGen.silenceImage.enabled = false;
            ViewGen.cdImage.enabled = false;
            ViewGen.cdImage.fillAmount = 0;

            ViewGen.iconButton.onClick.AddListener(OnIconClick);
        }

        void OnDestroy()
        {

        }

        void Update()
        {
            if (null != mCdTimer)
            {
                ViewGen.cdImage.fillAmount = GetCdPercentage();
            }
        }

        public void UpdateUi(SkillCfg skillCfg)
        {
            mSkillCfg = skillCfg;

            ResourceSys.Instance.SetImage(ViewGen.iconImage, skillCfg.GetUiIconPath());
        }

        /// <summary>
        /// 开始播放cd倒计时
        /// </summary>
        public void OnKeySkillCreate(Skill skill)
        {
            mSkillCfg = skill.GetSkillCfg();
            if (mSkillCfg.mCdWaitDuration > 0)
            {
                mWaitCdTimer = new DCDurationTimer(mSkillCfg.mCdWaitDuration, OnWaitCdEnd);
                mWaitCdTimer.SetAutoDestroy(true);
                mWaitCdTimer.CreateNormal();
            }
        }

        void OnWaitCdEnd()
        {
            SetupCdTimer();
        }

        void SetupCdTimer()
        {
            mCdTimer = new DCDurationTimer(mSkillCfg.mCdDuration, OnCdEnd);
            mCdTimer.SetAutoDestroy(true);
            mCdTimer.CreateNormal();

            ViewGen.cdImage.fillAmount = 1;
            ViewGen.cdImage.enabled = true;
        }

        void OnCdEnd()
        {
            mCdTimer = null;
            ViewGen.cdImage.enabled = false;
            ViewGen.cdImage.fillAmount = 0;
        }

        /// <summary>
        /// 刚释放是1，cd完0
        /// </summary>
        /// <returns></returns>
        public float GetCdPercentage()
        {
            if (mCdTimer == null)
            {
                return 0;
            }

            return 1 - mCdTimer.GetPercentage();
            /*var p = mCurrentSkill.GetTickedLife() / mCurrentSkill.GetSkillCfg().mCdDuration;
            p = Mathf.Max(1, p);
            return 1 - p;*/
        }

        public void DoCdAnimation()
        {

        }

        public void SetIcon(Sprite sprite)
        {
            ViewGen.iconImage.sprite = sprite;
        }

        public void SetSilence(bool silence)
        {
            ViewGen.silenceImage.enabled = silence;
        }

        public void OnIconClick()
        {
            if (mCdTimer != null)
            {
                return;
            }

            GameInput.Instance.AddDownKeyCode(mSendKeyCode);
        }
    }
}