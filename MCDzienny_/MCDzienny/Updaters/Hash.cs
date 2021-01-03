using System;
using System.IO;
using System.Security.Cryptography;

namespace MCDzienny.Updaters
{
    // Token: 0x02000397 RID: 919
    public class Hash
    {
        // Token: 0x06001A32 RID: 6706 RVA: 0x000B8C84 File Offset: 0x000B6E84
        public static string GetMD5Hash(string pathName)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            return GetMD5Hash(fileStream);
        }

        // Token: 0x06001A33 RID: 6707 RVA: 0x000B8CC0 File Offset: 0x000B6EC0
        public static string GetMD5Hash(Stream fileStream)
        {
            var result = "";
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            try
            {
                var value = md5CryptoServiceProvider.ComputeHash(fileStream);
                fileStream.Close();
                var text = BitConverter.ToString(value);
                text = text.Replace("-", "");
                result = text;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            return result;
        }
    }
}