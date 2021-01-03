using System;
using System.Text;

namespace MCDzienny
{
    // Token: 0x0200039C RID: 924
    public class Utils
    {
        // Token: 0x06001A53 RID: 6739 RVA: 0x000B9A18 File Offset: 0x000B7C18
        public static string TimeSpanToString(TimeSpan timeSpan)
        {
            var stringBuilder = new StringBuilder();
            if (timeSpan.Hours > 0) stringBuilder.Append(timeSpan.Hours).Append("h");
            if (timeSpan.Hours > 0 || timeSpan.Minutes > 0) stringBuilder.Append(timeSpan.Minutes).Append("m");
            if (timeSpan.Seconds >= 0) stringBuilder.Append(timeSpan.Seconds).Append("s");
            return stringBuilder.ToString();
        }
    }
}