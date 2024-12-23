using System;

namespace MCDzienny
{
    public class HttpUtil
    {
        public static bool IsValidUrl(string url)
        {
            Uri result;
            if (!Uri.TryCreate(url, UriKind.Absolute, out result))
            {
                return false;
            }
            if (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)
            {
                return true;
            }
            return false;
        }
    }
}