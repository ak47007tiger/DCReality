using System.Text;
using xxHashSharp;

namespace DC
{

    public static class StringExt
    {
        public static int GetExtHashCode(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            return (int)xxHash.CalculateHash(bytes);
        }
    }
}

