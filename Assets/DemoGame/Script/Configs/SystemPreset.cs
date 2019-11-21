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

        public static readonly float move_stop_distance = 0.3f;

        public static string GetConfigPath<T>()
        {
            return "Configs/" + typeof(T).Name;
        }
    }
}