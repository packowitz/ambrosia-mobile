using System.Linq;
using ModestTree;

namespace Utils
{
    public static class StringUtil
    {
        public static string Readable(string identifier)
        {
            return identifier.Split('_').Select(str =>
            {
                var lower = str.ToLower();
                return lower.Substring(0, 1).ToUpper() + lower.Substring(1);
            }).Join(" ");
        }
    }
}