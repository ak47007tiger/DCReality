using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    /// <summary>
    /// 跟踪到某个hero
    /// 查看某个区域
    /// 移动查看的区域
    /// 拥有焦点时 按esc 停止监听，按其他按键重新处理
    /// </summary>
    public class MobaCamera : BaseMonoBehaviour
    {
        public float mEdgeRange;

        public float mMoveSpeed;

        public Vector2 mFieldOfViewRange;
        public float mFieldOfViewSpeed;

        public List<Transform> mTraceList;

        private Camera mCamera;

        private bool mStop;

        private bool mHasFocus = true;

        void Awake()
        {
            mCamera = GetComponent<Camera>();
        }

        void OnApplicationFocus(bool hasFocus)
        {
            mHasFocus = hasFocus;
        }

        void OnApplicationPause(bool pauseStatus)
        {
        }

        public void LookTo(Transform targetTf)
        {

        }

        public void LookTo(Vector3 targetPos)
        {

        }

        public void MoveBy(Vector3 delta)
        {
            //            var cameraPosDelta = Vector3.zero;
            CacheTransform.position += delta;
        }

        void LateUpdate()
        {
            if (!mHasFocus)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                mStop = Input.GetKey(KeyCode.Escape);
            }

            if (mStop)
            {
                return;
            }

            var pos = Input.mousePosition;
            var op = ComputeOp(pos);

            if (op.InEdge)
            {
                MoveBy(op.MoveDelta * Time.deltaTime);
            }
        }

        Op ComputeOp(Vector3 pos)
        {
            var op = new Op();
            //left
            if (pos.x < mEdgeRange)
            {
                op.InEdge = true;
                op.MoveDelta.x -= mMoveSpeed;
            }
            //top
            var sh = Screen.height;
            if (pos.y > sh - mEdgeRange)
            {
                op.InEdge = true;
                op.MoveDelta.z += mMoveSpeed;
            }
            //right
            var sw = Screen.width;
            if (pos.x > sw - mEdgeRange)
            {
                op.InEdge = true;
                op.MoveDelta.x += mMoveSpeed;
            }
            //bottom
            if (pos.y < mEdgeRange)
            {
                op.InEdge = true;
                op.MoveDelta.z -= mMoveSpeed;
            }
            return op;
        }

        class Op
        {
            public bool InEdge;
            public Vector3 MoveDelta = Vector3.zero;
        }
    }
}
