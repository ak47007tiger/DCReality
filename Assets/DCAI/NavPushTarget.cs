using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC.AI
{
    public class NavPushTarget : BaseMonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        public static Vector3 ComputeTargetPosition(Vector3 curPos, Vector3 planPos)
        {
            var ray = new Ray(curPos, (planPos - curPos).normalized);

            if (Physics.Raycast(ray, Vector3.Distance(planPos, curPos)))
            {

            }

            return planPos;
        }

    }
}
