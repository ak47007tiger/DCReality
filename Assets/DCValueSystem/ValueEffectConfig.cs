namespace DC.ValueSys
{
    public enum ValueEffectType
    {
        /// <summary>
        /// 加成 成比例
        /// </summary>
        Additive,
        /// <summary>
        /// 削弱 成比例
        /// </summary>
        Reduce,
        /// <summary>
        /// 增加数值
        /// </summary>
        AddValue,
        /// <summary>
        /// 减少数值
        /// </summary>
        ReduceValue,
    }

    public class ValueEffectConfig
    {
        public int mId;

        public ValueEffectType mValueEffectType;

        public GValueType mGValueType;

        public float mValue;

        /// <summary>
        /// -2 同一个
        /// 0 相同
        /// -1 弱于
        /// 1 强于
        /// 2 不相干
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int Compare(ValueEffectConfig config)
        {
            if (config.mId == mId)
            {
                return -2;
            }

            if (config.mGValueType != mGValueType)
            {
                return 2;
            }

            if (config.mValueEffectType != mValueEffectType)
            {
                return 2;
            }

            if (mValue < config.mValue) return -1;
            if (mValue > config.mValue) return 1;
            return 0;
        }

        public float Process(float srcValue)
        {
            switch (mValueEffectType)
            {
                case ValueEffectType.Additive:
                    return srcValue * (1 + mValue);
                case ValueEffectType.Reduce:
                    return srcValue * (1 - mValue);
                case ValueEffectType.AddValue:
                    return srcValue + mValue;
                case ValueEffectType.ReduceValue:
                    return srcValue - mValue;
            }

            return srcValue;
        }

    }

}
