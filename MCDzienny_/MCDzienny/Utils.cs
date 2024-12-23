using System;
using System.Text;

namespace MCDzienny
{
    public class Utils
    {
        public static string TimeSpanToString(TimeSpan timeSpan)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (timeSpan.Hours > 0)
            {
                stringBuilder.Append(timeSpan.Hours).Append("h");
            }
            if (timeSpan.Hours > 0 || timeSpan.Minutes > 0)
            {
                stringBuilder.Append(timeSpan.Minutes).Append("m");
            }
            if (timeSpan.Seconds >= 0)
            {
                stringBuilder.Append(timeSpan.Seconds).Append("s");
            }
            return stringBuilder.ToString();
        }
    }
}