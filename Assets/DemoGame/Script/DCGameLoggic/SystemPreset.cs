using UnityEngine;

namespace DC.GameLogic
{
    public class SystemPreset
    {
        public static readonly string tag_env_ground = "env_ground";

        public static bool IsGround(string tag)
        {
            return tag_env_ground.Equals(tag);
        }
    }
}