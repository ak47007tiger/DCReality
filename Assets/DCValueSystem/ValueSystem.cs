using System;
using System.Collections.Generic;
using DC.Collections.Generic;

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
        int GetValue(GValueType type);
        void SetValue(GValueType type, int value);
        int this[GValueType type] { get; set; }
    }

    public class ValueComponent : IValueComponent
    {
        private Dictionary<GValueType, int> mDicTypeToValue = new Dictionary<GValueType, int>();

        public Action<GValueType, int, int> OnValueChange;

        public int GetValue(GValueType type)
        {
            if (mDicTypeToValue.TryGetValue(type, out var value))
            {
                return value;
            }

            return 0;
        }

        public void SetValue(GValueType type, int value)
        {
            var old = GetValue(type);
            mDicTypeToValue[type] = value;
            if (null != OnValueChange)
            {
                OnValueChange(type, old, value);
            }
        }

        public int this[GValueType type]
        {
            get { return GetValue(type); }
            set { SetValue(type,value);}
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