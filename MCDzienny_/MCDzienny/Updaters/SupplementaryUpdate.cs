using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MCDzienny.Updaters
{
    // Token: 0x02000398 RID: 920
    public class SupplementaryUpdate
    {
        // Token: 0x04000E6D RID: 3693
        public const string hashesFileUrl = "http://mcdzienny.cba.pl/download/libraries/hashes.txt";

        // Token: 0x04000E6E RID: 3694
        public readonly Dictionary<string, string> applicationFilesPathUrl = new Dictionary<string, string>
        {
            {
                "System.Data.SQLite.dll",
                "http://mcdzienny.cba.pl/download/libraries/System.Data.SQLite.dll"
            }
        };

        // Token: 0x04000E6F RID: 3695
        private readonly Dictionary<string, string> hashes = new Dictionary<string, string>();

        // Token: 0x06001A35 RID: 6709 RVA: 0x000B8D30 File Offset: 0x000B6F30
        public void DownloadMissingFiles()
        {
            ReadHashesFromWebsite();
            foreach (var keyValuePair in applicationFilesPathUrl)
                try
                {
                    if (!File.Exists(keyValuePair.Key))
                    {
                        MessageBox.Show(
                            string.Format("The file: {0} is missing. It will get download shortly.", keyValuePair.Key),
                            "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        for (var i = 0; i <= 2; i++)
                        {
                            using (var webClient = new WebClient())
                            {
                                webClient.DownloadFile(keyValuePair.Value, keyValuePair.Key);
                            }

                            if (!hashes.ContainsKey(keyValuePair.Key) || hashes[keyValuePair.Key].ToLower() ==
                                Hash.GetMD5Hash(keyValuePair.Key).ToLower()) goto IL_EA;
                        }

                        File.Delete(keyValuePair.Key);
                        throw new Exception(string.Format("File: {0} hash doesn't match the expected hash.",
                            keyValuePair.Key));
                    }

                    IL_EA: ;
                }
                catch
                {
                    MessageBox.Show(
                        string.Format(
                            "Sorry! MCDzienny was unable to download the missing file: {0} You have to download the file manually from http://mcdzienny.cba.pl website.",
                            keyValuePair.Key), "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    throw;
                }
        }

        // Token: 0x06001A36 RID: 6710 RVA: 0x000B8EB4 File Offset: 0x000B70B4
        private void ReadHashesFromWebsite()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var text = webClient.DownloadString("http://mcdzienny.cba.pl/download/libraries/hashes.txt");
                    foreach (var text2 in text.Split(new[]
                    {
                        '\r',
                        '\n'
                    }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var array2 = text2.Split(' ');
                        hashes.Add(array2[0], array2[1]);
                    }
                }
            }
            catch
            {
            }
        }
    }
}