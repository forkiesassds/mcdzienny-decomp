using System;
using System.IO;
using System.Security.Cryptography;

namespace MCDzienny.Updaters
{
    public class Hash
    {
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

        public static string GetMD5Hash(Stream fileStream)
        {
            string result = "";
            string text = "";
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            try
            {
                byte[] array = mD5CryptoServiceProvider.ComputeHash(fileStream);
                fileStream.Close();
                text = BitConverter.ToString(array);
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