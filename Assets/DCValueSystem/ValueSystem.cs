using System.Collections.Generic;

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
    }

    public class ValueComponent
    {
        private Dictionary<GValueType, int> mDicTypeToValue;

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
        hp_recover,

        mp,
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
    }
}