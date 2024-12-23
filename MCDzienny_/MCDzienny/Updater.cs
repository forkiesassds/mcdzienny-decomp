using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace MCDzienny
{
    public static class Updater
    {
        static string message = "";

        static string updaterVersionNew = "";

        static string exeVersionNew = "";

        static string dllVersionNew = "";

        static string changelogVersionNew = "";

        static VersionNumber localExeFile = new VersionNumber(new int[4]
        {
            1, 0, 0, 0
        });

        static VersionNumber localDllFile = new VersionNumber(new int[4]
        {
            1, 0, 0, 0
        });

        static VersionNumber remoteExeFile = new VersionNumber(new int[4]
        {
            1, 0, 0, 0
        });

        static VersionNumber remoteDllFile = new VersionNumber(new int[4]
        {
            1, 0, 0, 0
        });

        public static bool CheckLocalVersions()
        {
            if (File.Exists("MCDziennyLava.exe"))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo("MCDziennyLava.exe");
                localExeFile = VersionNumber.Parse(versionInfo.FileVersion);
                if (File.Exists("MCDzienny_.dll"))
                {
                    FileVersionInfo versionInfo2 = FileVersionInfo.GetVersionInfo("MCDzienny_.dll");
                    localDllFile = VersionNumber.Parse(versionInfo2.FileVersion);
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool CheckRemoteVersions()
        {
            return DownloadManifestAndRead();
        }

        public static bool CompareLocalToRemoteVersions()
        {
            if (localDllFile < remoteDllFile)
            {
                return true;
            }
            if (localExeFile < remoteExeFile)
            {
                return true;
            }
            return false;
        }

        public static bool CheckForUpdates()
        {
            if (CheckLocalVersions() && CheckRemoteVersions() && CompareLocalToRemoteVersions())
            {
                return true;
            }
            return false;
        }

        public static void InitUpdate()
        {
            if (Server.CLI)
            {
                return;
            }
            new Thread((ThreadStart)delegate
            {
                //IL_009f: Unknown result type (might be due to invalid IL or missing references)
                //IL_00a4: Invalid comparison between I4 and Unknown
                //IL_0060: Unknown result type (might be due to invalid IL or missing references)
                //IL_0065: Invalid comparison between I4 and Unknown
                //IL_007c: Unknown result type (might be due to invalid IL or missing references)
                string updaterVersion;
                if (!Server.CLI && CheckCurrentUpdaterVersion(out updaterVersion))
                {
                    switch (updaterVersion)
                    {
                        case "1.0.0.0":
                        case "1.5.0.0":
                        case "1.6.0.0":
                        case "2.0.0.0":
                        case "2.1.0.0":
                            if (6 == (int)MessageBox.Show(
                                    "A new version of 'Updater.exe' was found. \nIt's highly recommended to update to the newest version.\nDo you want to update now?",
                                    "Update", (MessageBoxButtons)4, (MessageBoxIcon)64, 0) && PerformUpdaterUpdate())
                            {
                                MessageBox.Show("The update was performed successfully!", "Update", 0, (MessageBoxIcon)64, 0);
                            }
                            break;
                    }
                }
                if (!Server.CLI && CheckForUpdates() && 6 == (int)MessageBox.Show("A new version of MCDzienny was found. Do you want to update now?", "Update",
                                                                                  (MessageBoxButtons)4, (MessageBoxIcon)64, 0))
                {
                    if (PlatformID.Unix == Environment.OSVersion.Platform)
                    {
                        Process.Start("mono", "Updater.exe quick " + Process.GetCurrentProcess().Id);
                    }
                    else
                    {
                        Process.Start("Updater.exe", "quick " + Process.GetCurrentProcess().Id);
                    }
                    Process.GetCurrentProcess().Kill();
                }
            }).Start();
        }

        public static bool CheckCurrentUpdaterVersion(out string updaterVersion)
        {
            if (File.Exists("Updater.exe"))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo("Updater.exe");
                updaterVersion = versionInfo.FileVersion;
                return true;
            }
            updaterVersion = null;
            return false;
        }

        public static bool PerformUpdaterUpdate()
        {
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile("http://mcdzienny.cba.pl/download/Updater.exe", "Updater.update");
            }
            catch
            {
                return false;
            }
            if (File.Exists("Updater.update"))
            {
                File.Replace("Updater.update", "Updater.exe", null);
            }
            return true;
        }

        public static bool CheckForNewUpdater()
        {
            string updaterVersion;
            if (CheckCurrentUpdaterVersion(out updaterVersion))
            {
                if (DownloadManifestAndRead() && updaterVersion == updaterVersionNew)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        static bool DownloadManifestAndRead()
        {
            Stream xml;
            if (DownloadManifest(out xml))
            {
                ReadManifest(xml);
                return true;
            }
            return false;
        }

        static bool DownloadManifest(out Stream xml)
        {
            WebClient webClient = new WebClient();
            xml = null;
            try
            {
                xml = new MemoryStream(webClient.DownloadData("http://mcdzienny.cba.pl/download/manifest.info"));
            }
            catch {}
            webClient.Dispose();
            if (xml == null)
            {
                return false;
            }
            return true;
        }

        static void ReadManifest(Stream stream)
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            try
            {
                XmlDocument val = new XmlDocument();
                val.Load(stream);
                XmlNodeList val2 = null;
                XmlNode firstChild = val.FirstChild;
                if (firstChild.Name.ToLower() == "manifest-updates" && firstChild.FirstChild.Name.ToLower() == "manifest-update")
                {
                    val2 = firstChild.FirstChild.ChildNodes;
                }
                if (val2 == null)
                {
                    return;
                }
                for (int i = 0; i < val2.Count; i++)
                {
                    switch (val2[i].LocalName)
                    {
                        case "exe":
                        {
                            XmlAttributeCollection attributes4 = val2[i].Attributes;
                            for (int m = 0; m < attributes4.Count; m++)
                            {
                                if (attributes4[m].Name == "version")
                                {
                                    exeVersionNew = attributes4[m].Value;
                                    try
                                    {
                                        remoteExeFile = VersionNumber.Parse(exeVersionNew);
                                    }
                                    catch {}
                                }
                            }
                            break;
                        }
                        case "dll":
                        {
                            XmlAttributeCollection attributes2 = val2[i].Attributes;
                            for (int k = 0; k < attributes2.Count; k++)
                            {
                                if (attributes2[k].Name == "version")
                                {
                                    dllVersionNew = attributes2[k].Value;
                                    try
                                    {
                                        remoteDllFile = VersionNumber.Parse(dllVersionNew);
                                    }
                                    catch {}
                                }
                            }
                            break;
                        }
                        case "changelog":
                        {
                            XmlAttributeCollection attributes3 = val2[i].Attributes;
                            for (int l = 0; l < attributes3.Count; l++)
                            {
                                if (attributes3[l].Name == "version")
                                {
                                    changelogVersionNew = attributes3[l].Value;
                                }
                            }
                            break;
                        }
                        case "updater":
                        {
                            XmlAttributeCollection attributes = val2[i].Attributes;
                            for (int j = 0; j < attributes.Count; j++)
                            {
                                if (attributes[j].Name == "version")
                                {
                                    updaterVersionNew = attributes[j].Value;
                                }
                            }
                            break;
                        }
                        case "message":
                            message = val2[i].InnerText;
                            break;
                    }
                }
            }
            catch (Exception) {}
        }

        public static void Load(string givenPath)
        {
            if (!File.Exists(givenPath))
            {
                return;
            }
            string[] array = File.ReadAllLines(givenPath);
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (text != "" && text[0] != '#')
                {
                    string text2 = text.Split('=')[0].Trim();
                    string text3 = text.Split('=')[1].Trim();
                    switch (text2.ToLower())
                    {
                        case "autoupdate":
                            Server.autoupdate = text3.ToLower() == "true";
                            break;
                        case "notify":
                            Server.autonotify = text3.ToLower() == "true";
                            break;
                        case "restartcountdown":
                            Server.restartcountdown = text3;
                            break;
                    }
                }
            }
        }
    }
}