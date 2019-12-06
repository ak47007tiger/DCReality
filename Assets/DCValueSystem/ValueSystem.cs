using System;
using System.Collections.Generic;
using DC.Collections.Generic;
using DC.SkillSystem;

namespace DC.ValueSys
{
    public class ValueSystem
    {
        
    }

    public enum ValueOp
    {

    }

    public class TypeValue
    {

    }

    public interface IValueComponent
    {
        float GetValue(GValueType type);
        void SetValue(GValueType type, float value);
        float this[GValueType type] { get; set; }
        /// <summary>
        /// 有buff加成的
        /// </summary>
        /// <returns></returns>
        float GetBuffedValue(GValueType type, BuffCmpt cmpt);
    }

    public class ValueComponent : IValueComponent
    {
        private Dictionary<GValueType, float> mDicTypeToValue = new Dictionary<GValueType, float>();

        public Action<GValueType, float, float> OnValueChange;

        public float GetValue(GValueType type)
        {
            if (mDicTypeToValue.TryGetValue(type, out var value))
            {
                return value;
            }

            return 0;
        }

        public void SetValue(GValueType type, float value)
        {
            var old = GetValue(type);
            mDicTypeToValue[type] = value;
            if (null != OnValueChange)
            {
                OnValueChange(type, old, value);
            }
        }

        public float this[GValueType type]
        {
            get { return GetValue(type); }
            set { SetValue(type,value);}
        }

        public float GetBuffedValue(GValueType type, BuffCmpt cmpt)
        {
            var buffList = cmpt.GetBuffList();
            var srcValue = GetValue(type);
            var dstValue = srcValue;
            foreach (var buff in buffList)
            {
                var valueEffectCfg = buff.mBuffCfg.mValueEffectCfg;
                if (valueEffectCfg.mId > 0)
                {
                    var process = valueEffectCfg.Process(srcValue);
                    var delta = process - srcValue;
                    srcValue += delta;
                }
            }

            return 0;
        }

    }

    public enum DamageType
    {
        physic,
        magic,
        real,
    }

    public enum GValueType
    {
        hp,
        /// <summary>
        /// 回血
        /// </summary>
        hp_recover,

        mp,
        /// <summary>
        /// 回蓝
        /// </summary>
        mp_recover,

        /// <summary>
        /// 暴击
        /// </summary>
        critical_strike_rate,
        
        physic_attack,
        physic_weaken,//物穿
        physic_defense,

        magic_attack,
        magic_weaken,//魔穿
        magic_defense,

        move_speed,

        attack_speed,
    }
}