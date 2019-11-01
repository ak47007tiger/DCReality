using System.Collections.Generic;

namespace DC.SkillSystem
{
    public class EnvContext
    {
        Dictionary<string, object> mNameToInstance = new Dictionary<string, object>();

        T Get<T>(string name)
        {
            if (mNameToInstance.TryGetValue(name, out var val))
            {
                return (T) val;
            }

            return default(T);
        }
    }

    public class SkillSysContext : EnvContext
    {
    }
}