using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MCDzienny.Updaters
{
    public class SupplementaryUpdate
    {
        public const string hashesFileUrl = "http://mcdzienny.cba.pl/download/libraries/hashes.txt";

        public readonly Dictionary<string, string> applicationFilesPathUrl = new Dictionary<string, string>
        {
            {
                "System.Data.SQLite.dll", "http://mcdzienny.cba.pl/download/libraries/System.Data.SQLite.dll"
            }
        };

        readonly Dictionary<string, string> hashes = new Dictionary<string, string>();

        public void DownloadMissingFiles()
        {
            //IL_0106: Unknown result type (might be due to invalid IL or missing references)
            //IL_0049: Unknown result type (might be due to invalid IL or missing references)
            ReadHashesFromWebsite();
            foreach (KeyValuePair<string, string> item in applicationFilesPathUrl)
            {
                try
                {
                    if (File.Exists(item.Key))
                    {
                        continue;
                    }
                    MessageBox.Show(string.Format("The file: {0} is missing. It will get download shortly.", item.Key), "Missing file", 0, (MessageBoxIcon)64);
                    int num = 0;
                    while (true)
                    {
                        if (num > 2)
                        {
                            File.Delete(item.Key);
                            throw new Exception(string.Format("File: {0} hash doesn't match the expected hash.", item.Key));
                        }
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(item.Value, item.Key);
                        }
                        if (hashes.ContainsKey(item.Key) && !(hashes[item.Key].ToLower() == Hash.GetMD5Hash(item.Key).ToLower()))
                        {
                            num++;
                            continue;
                        }
                        break;
                    }
                }
                catch
                {
                    MessageBox.Show(
                        string.Format(
                            "Sorry! MCDzienny was unable to download the missing file: {0} You have to download the file manually from http://mcdzienny.cba.pl website.",
                            item.Key),
                        "Fatal error", 0, (MessageBoxIcon)16);
                    throw;
                }
            }
        }

        void ReadHashesFromWebsite()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string text = webClient.DownloadString("http://mcdzienny.cba.pl/download/libraries/hashes.txt");
                    string[] array = text.Split(new char[2]
                    {
                        '\r', '\n'
                    }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string text2 in array)
                    {
                        string[] array2 = text2.Split(' ');
                        hashes.Add(array2[0], array2[1]);
                    }
                }
            }
            catch {}
        }
    }
}