using System.Linq;

namespace FrontlineMaidBot.Helpers
{
    public static class Utils
    {
        public static string NormalizeTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return "0000";

            return string.Join("", time.Where(x => char.IsDigit(x)).TakeLast(4)).PadLeft(4, '0');
        }

        public static string DeNormalizeTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return "00:00";

            if (time.Length < 4)
                time = time.PadLeft(4, '0');

            if (time.Length > 4)
                time = string.Join("", time.TakeLast(4));

            return time.Insert(2, ":");
        }
    }
}