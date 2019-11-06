using System;
using UnityEngine;

namespace DC.AI
{
    public class TfMoveToDirection : TfBase
    {
        public float mDuration;

        public Vector3 mDirection;

        private float mTickedDuration;

        public Action<TfMoveToDirection> mOnEnd;

        public void StartMove(Vector3 direction, float duration, float speed)
        {
            mDirection = direction;
            mDuration = duration;
            mSpeed = speed;
            mTickedDuration = 0;
            SetStop(false);
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

            CacheTransform.position = ComputeNextPositionWithDir(CacheTransform.position, mDirection, mSpeed);
        }

        public static Vector3 ComputeNextPositionWithDir(Vector3 curPos,Vector3 direction, float speed)
        {
            return curPos + (direction * speed * Time.deltaTime);
        }

    }
}