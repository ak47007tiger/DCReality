using System.Collections.Generic;
using UnityEngine;

namespace DC.DCEffectSys
{
    public class LinkedEffectRenderer : BaseMonoBehaviour
    {
        public LineRenderer mLineRenderer;

        public void Awake()
        {
            mLineRenderer = GetComponent<LineRenderer>();
        }

        public void Start()
        {
            UpdateRenderer();
        }

        public void UpdateRenderer()
        {
            /*
             
            1,2 1
            1,2,3 1,2
             */

            var allActiveChildren = new List<Transform>();

            for (var i = 0; i < CacheTransform.childCount; i++)
            {
                var childTf = CacheTransform.GetChild(i);
                if (childTf.gameObject.activeSelf)
                {
                    allActiveChildren.Add(childTf);
                }
            }

            var positions = new Vector3[allActiveChildren.Count];
            for (var i = 0; i < positions.Length; i++)
            {
                positions[i] = allActiveChildren[i].position;
            }

            mLineRenderer.positionCount = positions.Length;
            mLineRenderer.SetPositions(positions);
        }
    }
}
