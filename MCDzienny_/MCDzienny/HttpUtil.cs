using System;

namespace MCDzienny
{
    // Token: 0x0200002E RID: 46
    public class HttpUtil
    {
        // Token: 0x06000118 RID: 280 RVA: 0x00007760 File Offset: 0x00005960
        public static bool IsValidUrl(string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}