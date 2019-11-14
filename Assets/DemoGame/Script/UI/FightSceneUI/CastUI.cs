using DC.ActorSystem;
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
    public class CastUI : BaseUI
    {
        public Image mImgRange;

        public Image mImgArrow;

        public SkillCfg mSkillCfg;

        public TargetUI mTargetUi;

        void Awake()
        {
            MsgSys.Add<SkillCfg>(GameEvent.CastEvt, OnPrepareCast);
        }

        void OnDestroy()
        {
            MsgSys.Remove<SkillCfg>(GameEvent.CastEvt, OnPrepareCast);
        }

        void Update()
        {
            if (null == mSkillCfg)
            {
                return;
            }

            var mainActor = ActorSys.Instance.GetMainActor();
            if (mainActor != null)
            {
                var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var casthit))
                {
                    switch (mSkillCfg.mTargetType)
                    {
                        case SkillTargetType.Position:
                            OnWatchPosition(mainActor, casthit);
                            break;
                        case SkillTargetType.Direction:
                            OnWatchDirection(mainActor, casthit);
                            break;
                        case SkillTargetType.Actor:
                            OnWatchTarget(mainActor, casthit);
                            break;
                    }
                }
                //计算目标方向
            }
        }

        private void OnWatchTarget(IActor mainActor,RaycastHit casthit)
        {
            mTargetUi.ShowCastTarget(Input.mousePosition);
        }

        private void OnWatchPosition(IActor mainActor, RaycastHit casthit)
        {
            var position = mainActor.GetTransform().position;
            position.y = 0;

            var point = casthit.point;
            point.y = 0;

            var dir = (point - position).normalized;
        }

        private void OnWatchDirection(IActor mainActor, RaycastHit casthit)
        {
            var position = mainActor.GetTransform().position;
            position.y = 0;

            var point = casthit.point;
            point.y = 0;

            var dir = (point - position).normalized;

        }

        public void OnPrepareCast(SkillCfg skillCfg)
        {
            if (skillCfg.mTargetType == SkillTargetType.Position || skillCfg.mTargetType == SkillTargetType.Direction)
            {
                mImgArrow.enabled = true;
            }
            else
            {
                mImgRange.enabled = true;
            }

            if ((skillCfg.mSkillType == SkillType.area || skillCfg.mSkillType == SkillType.bullet) && skillCfg.mCastRange > 0)
            {
            }
        }

        public void OnCastEnd()
        {
            mImgRange.enabled = false;
            mImgArrow.enabled = false;
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