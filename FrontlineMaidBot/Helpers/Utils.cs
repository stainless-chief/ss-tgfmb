using System.Linq;

namespace FrontlineMaidBot.Helpers
{
    /// <summary>Adopter for utility methods</summary>
    public static class Utils
    {
        /// <summary> Convert all kinds of strings to string with time on format "hhmm" </summary>
        /// <param name="time">Any kind of string, that probably contains time</param>
        /// <returns>Time of format "hhmm"</returns>
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

        /// <summary>Convert time string of format hhmm to time string of format hh:mm</summary>
        /// <param name="time">String with 4 digits</param>
        /// <returns>Time string of format hh:mm</returns>
        public static string NormalizeTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return "0000";

            return string.Join("", time.Where(x => char.IsDigit(x)).TakeLast(4)).PadLeft(4, '0');
        }
    }
}