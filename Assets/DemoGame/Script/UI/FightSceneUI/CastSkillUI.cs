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
    public class CastSkillUI : BaseUI
    {
        public SpriteRenderer mImgRange;

        public Transform mArrowTf;
        public SpriteRenderer mImgArrow;

        /// <summary>
        /// position类型
        /// </summary>
        public SpriteRenderer mImgArea;

        public SkillCfg mSkillCfg;

        public TargetUI mTargetUi;

        void Awake()
        {
            mImgRange.enabled = false;
            mImgArrow.enabled = false;
            mImgArea.enabled = false;

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
                if (Physics.Raycast(ray, out var castHit))
                {
                    switch (mSkillCfg.mTargetType)
                    {
                        case SkillTargetType.Position:
                            OnWatchPosition(mainActor, castHit);
                            break;
                        case SkillTargetType.Direction:
                            OnWatchDirection(mainActor, castHit);
                            break;
                        case SkillTargetType.Actor:
                            OnWatchTarget(mainActor, castHit);
                            break;
                    }
                }
                //计算目标方向
            }
        }

        private void OnWatchTarget(IActor mainActor,RaycastHit casthit)
        {
            var point = casthit.point;
            point.y = 1;

            mImgArea.transform.position = point;
            //            mTargetUi.ShowCastTarget(Input.mousePosition);
        }

        private void OnWatchPosition(IActor mainActor, RaycastHit casthit)
        {
            var point = casthit.point;
            point.y = 1;

            mImgArea.transform.position = point;
        }

        private void OnWatchDirection(IActor mainActor, RaycastHit casthit)
        {
            var position = mainActor.GetTransform().position;
            position.y = 0;

            var point = casthit.point;
            point.y = 0;

            var dir = (point - position).normalized;
//            mArrowTf.forward = dir;
//            mArrowTf.Rotate();
//            Vector3.RotateTowards(Vector3.zero, Vector3.zero, 1, 1);

            var localDir = transform.worldToLocalMatrix.MultiplyVector(dir);

            var rotation = Quaternion.FromToRotation(new Vector3(0, -1, 0), localDir);
            //如果父节点有个转换，那么怎么计算无关父节点的子节点的旋转
//            mImgArrow.transform.rotation = rotation;
            mImgArrow.transform.localRotation = rotation;
            /*var localEulerAngles = mImgArrow.transform.localEulerAngles;
            mImgArrow.transform.localEulerAngles = localEulerAngles;*/
            //            mImgArrow.transform.localRotation = rotation;

            //            Debug.DrawRay(position, point - position, Color.blue);
            Debug.DrawLine(position, point);

            /*var quaternion = Quaternion.Euler(dir);
            var rotate = mImgArrow.transform.localEulerAngles;
            var quaternionEulerAngles = quaternion.eulerAngles;
            rotate.z = quaternionEulerAngles.z;
            mImgArrow.transform.localEulerAngles = rotate;*/

//            mImgArrow.transform.localRotation = dir;
        }

        public void OnPrepareCast(SkillCfg skillCfg)
        {
            mSkillCfg = skillCfg;

            mImgRange.enabled = false;
            mImgArrow.enabled = false;
            mImgArea.enabled = true;

            if (skillCfg.mTargetType == SkillTargetType.Direction)
            {
                if (SystemPreset.max_skill_cast_range < mSkillCfg.mCastRange)
                {
                    mImgRange.enabled = true;
                }
                mImgArrow.enabled = true;
            }

            if (skillCfg.mTargetType == SkillTargetType.Position)
            {
                mImgArea.enabled = true;
                mImgRange.enabled = true;
            }

            if (skillCfg.mTargetType == SkillTargetType.Actor)
            {
                mImgArea.enabled = true;
                mImgArea.transform.localScale = Vector3.one;

                mImgRange.enabled = true;
            }
        }

        public void OnCastEnd()
        {
            mImgRange.enabled = false;
            mImgArrow.enabled = false;
            mImgArea.enabled = false;
            mSkillCfg = null;
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