using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    public class RoleEffectedByBuff
    {
        public static readonly uint Owner = Convert.ToUInt32("1", 2);
        public static readonly uint Friend_Hero = Convert.ToUInt32("10", 2);
        public static readonly uint Friend_Soldier = Convert.ToUInt32("100", 2);
        public static readonly uint Enemy = Convert.ToUInt32("1000", 2);
    }

    public interface IBuffCfg
    {
    }

    [CreateAssetMenu(fileName = "BuffCfg", menuName = "DC/ScriptableObjects/BuffCfg", order = 1)]
    public class BuffCfg : IBuffCfg
    {
        public int mRange;
        public uint mEffectRole;
    }
}