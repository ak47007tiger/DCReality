namespace DC.SkillSystem
{
    public enum EffectType
    {
        animation,
        visual_effect,
        translate,//type, dir, distance
        buff,
        camera,
        create_npc,
        terrier,
        /// <summary>
        /// 子技能
        /// </summary>
        child_skill,
        /// <summary>
        /// 对目标施加影响，回调skill
        /// </summary>
        effect_target,
    }
}