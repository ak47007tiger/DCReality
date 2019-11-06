using UnityEngine;

namespace DC.GameLogic
{
    public class SystemPreset
    {
        public static readonly string path_skill_cfgs = "Configs/Skill";

        public static readonly string tag_env_ground = "env_ground";

        public static bool IsGround(string tag)
        {
            return tag_env_ground.Equals(tag);
        }

        public static readonly float move_stop_distance = 0.1f;
    }
}