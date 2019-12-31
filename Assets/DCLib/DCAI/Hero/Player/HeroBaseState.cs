using System;
using UnityEngine;
using System.Collections;
using DC.GameLogic;

namespace DC.AI
{
    

    public class HeroBaseState : DCFSMState
    {
        protected HeroEntity Hero;

        protected Caster Caster;

        protected GameActor Actor;

        public void SetUp(GameObject ctxObj)
        {
            Hero = ctxObj.GetComponent<HeroEntity>();
            Caster = ctxObj.GetComponent<Caster>();
            Actor = ctxObj.GetComponent<GameActor>();
        }

        public override void Reason(object data)
        {

        }

        public override void Act(object data)
        {

        }

        public static BuffEvt GetBuffEvt(object data)
        {
            if (data is BuffEvt)
                return (BuffEvt)data;
            return null;
        }

        public bool CheckToMoveState()
        {
            //to move state
            if (Input.GetMouseButtonDown(1))
            {
                var ray = DCGraphics.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (SystemPreset.IsGround(hit.transform.gameObject.tag))
                    {
                        var hitPoint = hit.point;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

