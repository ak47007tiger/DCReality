using System.Collections.Generic;
using DC.ActorSystem;
using DC.ValueSys;
using UnityEngine;

namespace DC.SkillSystem
{
    public class TargetSelector
    {
        public static readonly TargetSelector Shared = new TargetSelector();

        public Vector3 mCastPos;

        public void Sort(List<IActor> list, Vector3 castPos)
        {
            mCastPos = castPos;
            list.Sort(TargetSelectSort);
        }

        /// <summary>
        /// 英雄，距离近，血量少
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual int TargetSelectSort(IActor a, IActor b)
        {
            var weightA = ComputeWeight(a);
            var weightB = ComputeWeight(b);

            if (weightA > weightB)
            {
                return -1;
            }

            if (weightA < weightB)
            {
                return 1;
            }

            return 0;
        }

        public virtual float ComputeWeight(IActor a)
        {
            var weight = 0f;
            switch (a.GetRoleType())
            {
                case RoleType.Hero:
                    weight += 100000;
                    break;
                case RoleType.Soldier:
                    weight += (100000 - 1);
                    break;
                case RoleType.Building:
                    weight += 0;
                    break;
            }

            //距离越远越靠后
            var distance = Vector3.Distance(a.GetTransform().position, mCastPos);
            weight += (1f / distance * 1000);
            
            //hp越小越优先
            var hp = a.GetValueComponent().GetValue(GValueType.hp);
            weight += (1f / hp * 100);

            return weight;
        }
    }
}