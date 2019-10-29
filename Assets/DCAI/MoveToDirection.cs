using System;
using UnityEngine;

namespace DC.AI
{
    public class MoveToDirection : BaseMonoBehaviour
    {
        public float mDuration;
        public Vector3 mDirection;

        private bool mStop;

        [HideInInspector]
        public float mSpeed;

        private float mTickedDuration;

        public bool StopAfterCatchTarget = true;

        public Action<MoveToDirection> mOnEnd;

        public bool IsStop()
        {
            return mStop;
        }

        public void SetStop(bool value)
        {
            mStop = value;
        }

        public void StartMove(Vector3 direction, float duration, float speed)
        {
            mDirection = direction;
            mDuration = duration;
            mSpeed = speed;
            mTickedDuration = 0;
        }

        void Update()
        {
            if (mStop) return;

            if (mTickedDuration >= mDuration)
            {
                if (StopAfterCatchTarget)
                {
                    SetStop(true);
                }

                if (null != mOnEnd) mOnEnd(this);
                return;
            }

            mTickedDuration += Time.deltaTime;

            CacheTransform.position = ComputeNextPosition(CacheTransform.position, mDirection, mSpeed);
        }

        public static Vector3 ComputeNextPosition(Vector3 curPos,Vector3 direction, float speed)
        {
            return curPos + (direction * speed * Time.deltaTime);
        }

    }
}