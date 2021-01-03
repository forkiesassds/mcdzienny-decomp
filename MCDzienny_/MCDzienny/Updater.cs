using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace MCDzienny
{
    // Token: 0x02000399 RID: 921
    public static class Updater
    {
        // Token: 0x04000E70 RID: 3696
        private static string message = "";

        // Token: 0x04000E71 RID: 3697
        private static string updaterVersionNew = "";

        // Token: 0x04000E72 RID: 3698
        private static string exeVersionNew = "";

        // Token: 0x04000E73 RID: 3699
        private static string dllVersionNew = "";

        // Token: 0x04000E74 RID: 3700
        private static string changelogVersionNew = "";

        // Token: 0x04000E75 RID: 3701
        private static VersionNumber localExeFile;

        // Token: 0x04000E76 RID: 3702
        private static VersionNumber localDllFile;

        // Token: 0x04000E77 RID: 3703
        private static VersionNumber remoteExeFile;

        // Token: 0x04000E78 RID: 3704
        private static VersionNumber remoteDllFile;

        // Token: 0x06001A45 RID: 6725 RVA: 0x000B96A0 File Offset: 0x000B78A0
        // Note: this type is marked as 'beforefieldinit'.
        static Updater()
        {
            var array = new int[4];
            array[0] = 1;
            localExeFile = new VersionNumber(array);
            var array2 = new int[4];
            array2[0] = 1;
            localDllFile = new VersionNumber(array2);
            var array3 = new int[4];
            array3[0] = 1;
            remoteExeFile = new VersionNumber(array3);
            var array4 = new int[4];
            array4[0] = 1;
            remoteDllFile = new VersionNumber(array4);
        }

        // Token: 0x06001A38 RID: 6712 RVA: 0x000B8F9C File Offset: 0x000B719C
        public static bool CheckLocalVersions()
        {
            if (!File.Exists("MCDziennyLava.exe")) return false;
            var versionInfo = FileVersionInfo.GetVersionInfo("MCDziennyLava.exe");
            localExeFile = VersionNumber.Parse(versionInfo.FileVersion);
            if (File.Exists("MCDzienny_.dll"))
            {
                var versionInfo2 = FileVersionInfo.GetVersionInfo("MCDzienny_.dll");
                localDllFile = VersionNumber.Parse(versionInfo2.FileVersion);
                return true;
            }

            return false;
        }

        // Token: 0x06001A39 RID: 6713 RVA: 0x000B9000 File Offset: 0x000B7200
        public static bool CheckRemoteVersions()
        {
            return DownloadManifestAndRead();
        }

        // Token: 0x06001A3A RID: 6714 RVA: 0x000B9008 File Offset: 0x000B7208
        public static bool CompareLocalToRemoteVersions()
        {
            return localDllFile < remoteDllFile || localExeFile < remoteExeFile;
        }

        // Token: 0x06001A3B RID: 6715 RVA: 0x000B9034 File Offset: 0x000B7234
        public static bool CheckForUpdates()
        {
            return CheckLocalVersions() && CheckRemoteVersions() && CompareLocalToRemoteVersions();
        }

        // Token: 0x06001A3C RID: 6716 RVA: 0x000B9050 File Offset: 0x000B7250
        public static void InitUpdate()
        {
            if (Server.CLI) return;
            new Thread(delegate()
            {
                string a;
                if (!Server.CLI && CheckCurrentUpdaterVersion(out a) &&
                    (a == "1.0.0.0" || a == "1.5.0.0" || a == "1.6.0.0" || a == "2.0.0.0" || a == "2.1.0.0") &&
                    DialogResult.Yes == MessageBox.Show(
                        "A new version of 'Updater.exe' was found. \nIt's highly recommended to update to the newest version.\nDo you want to update now?",
                        "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) &&
                    PerformUpdaterUpdate())
                    MessageBox.Show("The update was performed successfully!", "Update", MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                if (!Server.CLI && CheckForUpdates() && DialogResult.Yes ==
                    MessageBox.Show("A new version of MCDzienny was found. Do you want to update now?", "Update",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                {
                    if (PlatformID.Unix == Environment.OSVersion.Platform)
                        Process.Start("mono", "Updater.exe quick " + Process.GetCurrentProcess().Id);
                    else
                        Process.Start("Updater.exe", "quick " + Process.GetCurrentProcess().Id);
                    Process.GetCurrentProcess().Kill();
                }
            }).Start();
        }

        // Token: 0x06001A3D RID: 6717 RVA: 0x000B9084 File Offset: 0x000B7284
        public static bool CheckCurrentUpdaterVersion(out string updaterVersion)
        {
            if (File.Exists("Updater.exe"))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo("Updater.exe");
                updaterVersion = versionInfo.FileVersion;
                return true;
            }

            updaterVersion = null;
            return false;
        }

        // Token: 0x06001A3E RID: 6718 RVA: 0x000B90B8 File Offset: 0x000B72B8
        public static bool PerformUpdaterUpdate()
        {
            var webClient = new WebClient();
            try
            {
                webClient.DownloadFile("http://mcdzienny.cba.pl/download/Updater.exe", "Updater.update");
            }
            catch
            {
                return false;
            }

            if (File.Exists("Updater.update")) File.Replace("Updater.update", "Updater.exe", null);
            return true;
        }

        // Token: 0x06001A3F RID: 6719 RVA: 0x000B9114 File Offset: 0x000B7314
        public static bool CheckForNewUpdater()
        {
            string a;
            return CheckCurrentUpdaterVersion(out a) && DownloadManifestAndRead() && a == updaterVersionNew;
        }

        // Token: 0x06001A40 RID: 6720 RVA: 0x000B9144 File Offset: 0x000B7344
        private static bool DownloadManifestAndRead()
        {
            Stream stream;
            if (DownloadManifest(out stream))
            {
                ReadManifest(stream);
                return true;
            }

            return false;
        }

        // Token: 0x06001A41 RID: 6721 RVA: 0x000B9164 File Offset: 0x000B7364
        private static bool DownloadManifest(out Stream xml)
        {
            var webClient = new WebClient();
            xml = null;
            try
            {
                xml = new MemoryStream(webClient.DownloadData("http://mcdzienny.cba.pl/download/manifest.info"));
            }
            catch
            {
            }

            webClient.Dispose();
            return xml != null;
        }

        // Token: 0x06001A42 RID: 6722 RVA: 0x000B91B0 File Offset: 0x000B73B0
        private static void ReadManifest(Stream stream)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);
                XmlNodeList xmlNodeList = null;
                var firstChild = xmlDocument.FirstChild;
                if (firstChild.Name.ToLower() == "manifest-updates" &&
                    firstChild.FirstChild.Name.ToLower() == "manifest-update")
                    xmlNodeList = firstChild.FirstChild.ChildNodes;
                if (xmlNodeList != null)
                    for (var i = 0; i < xmlNodeList.Count; i++)
                    {
                        string localName;
                        if ((localName = xmlNodeList[i].LocalName) != null)
                        {
                            if (!(localName == "exe"))
                            {
                                if (!(localName == "dll"))
                                {
                                    if (!(localName == "changelog"))
                                    {
                                        if (!(localName == "updater"))
                                        {
                                            if (localName == "message") message = xmlNodeList[i].InnerText;
                                        }
                                        else
                                        {
                                            var attributes = xmlNodeList[i].Attributes;
                                            for (var j = 0; j < attributes.Count; j++)
                                                if (attributes[j].Name == "version")
                                                    updaterVersionNew = attributes[j].Value;
                                        }
                                    }
                                    else
                                    {
                                        var attributes2 = xmlNodeList[i].Attributes;
                                        for (var k = 0; k < attributes2.Count; k++)
                                            if (attributes2[k].Name == "version")
                                                changelogVersionNew = attributes2[k].Value;
                                    }
                                }
                                else
                                {
                                    var attributes3 = xmlNodeList[i].Attributes;
                                    for (var l = 0; l < attributes3.Count; l++)
                                        if (attributes3[l].Name == "version")
                                        {
                                            dllVersionNew = attributes3[l].Value;
                                            try
                                            {
                                                remoteDllFile = VersionNumber.Parse(dllVersionNew);
                                            }
                                            catch
                                            {
                                            }
                                        }
                                }
                            }
                            else
                            {
                                var attributes4 = xmlNodeList[i].Attributes;
                                for (var m = 0; m < attributes4.Count; m++)
                                    if (attributes4[m].Name == "version")
                                    {
                                        exeVersionNew = attributes4[m].Value;
                                        try
                                        {
                                            remoteExeFile = VersionNumber.Parse(exeVersionNew);
                                        }
                                        catch
                                        {
                                        }
                                    }
                            }
                        }
                    }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06001A43 RID: 6723 RVA: 0x000B9478 File Offset: 0x000B7678
        public static void Load(string givenPath)
        {
            if (File.Exists(givenPath))
            {
                var array = File.ReadAllLines(givenPath);
                foreach (var text in array)
                    if (text != "" && text[0] != '#')
                    {
                        var text2 = text.Split('=')[0].Trim();
                        var text3 = text.Split('=')[1].Trim();
                        string a;
                        if ((a = text2.ToLower()) != null)
                        {
                            if (!(a == "autoupdate"))
                            {
                                if (!(a == "notify"))
                                {
                                    if (a == "restartcountdown") Server.restartcountdown = text3;
                                }
                                else
                                {
                                    Server.autonotify = text3.ToLower() == "true";
                                }
                            }
                            else
                            {
                                Server.autoupdate = text3.ToLower() == "true";
                            }
                        }
                    }
            }
        }
    }
}