using System.Text;
using UnityEngine;

namespace DC
{
    public static class LogDC
    {
        public static readonly StringBuilder sBd = new StringBuilder();

        public static void Log(string log, params object[] objs)
        {
            Debug.Log(string.Format(log, objs));
        }

        public static void LogEx(params object[] objs)
        {
            sBd.Clear();
            foreach (var o in objs)
            {
                sBd.Append(o).Append(',');
            }
            Debug.Log(sBd.ToString());
        }

        public static void Err(string log, params object[] objs)
        {
            Debug.Log(string.Format(log, objs));
        }

        public static void Waring(string log, params object[] objs)
        {
            Debug.Log(string.Format(log, objs));
        }
    }
}