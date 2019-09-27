using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    protected Transform mCacheTf;
    public Transform CacheTransform
    {
        get
        {
            if(mCacheTf == null) mCacheTf = transform;
            return mCacheTf;
        }
    }
}

public class TraceTarget : BaseMonoBehaviour
{
    public Transform mTargetTf;

    public float mSpeed;

    void Update()
    {
        if (mTargetTf == null) return;

        CacheTransform.position = ComputeNextPosition(mCacheTf, mTargetTf, mSpeed);
    }

    public static Vector3 ComputeNextPosition(Transform traceTf, Transform targetTf, float speed)
    {
        var dir = (targetTf.position - traceTf.position).normalized;
        var delta = speed * dir * Time.deltaTime;
        return traceTf.position + delta;
    }
}
