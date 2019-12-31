using DC.GameLogic;

namespace DC.SkillSystem
{
    public interface IBuff
    {
        /// <summary>
        /// buff拥有者是否可以释放技能
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        bool AllowCast(ISkill skill);

        void OnAttach(GameActor actor);

        void OnDetach();
    }
}