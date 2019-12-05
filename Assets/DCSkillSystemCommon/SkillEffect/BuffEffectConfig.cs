namespace DC.SkillSystem
{
    public enum AddBuffTargetType
    {
        /// <summary>
        /// 施法者
        /// </summary>
        Caster,
        /// <summary>
        /// 受击者
        /// </summary>
        Target,
    }

    public class BuffEffectConfig
    {
        public int mBuffCfgId;

        /// <summary>
        /// 给谁加buff
        /// </summary>
        public AddBuffTargetType mAddBuffTargetType;


    }
}