using System.Linq;

namespace FrontlineMaidBot.Extensions
{
    public static class ExString
    {
        public static string Replace(this string seed, char[] chars, char replacementCharacter)
        {
            return chars.Aggregate(seed, (str, cItem) => str.Replace(cItem, replacementCharacter));
        }

        public static string Replace(this string seed, string[] strings, string replacementCharacter)
        {
            return strings.Aggregate(seed, (str, cItem) => str.Replace(cItem, replacementCharacter));
        }
    }
}
