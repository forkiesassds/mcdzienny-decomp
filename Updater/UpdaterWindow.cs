using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Updater
{
    // Token: 0x02000005 RID: 5
    public partial class UpdaterWindow : Form
    {
        // Token: 0x0400001B RID: 27
        private static Version exeLocalVersion;

        // Token: 0x0400001C RID: 28
        private static Version dllLocalVersion;

        // Token: 0x0400001D RID: 29
        private static Version libraryLocalVersion;

        // Token: 0x0400001E RID: 30
        private Version changelogLocalVersion;

        // Token: 0x0400000C RID: 12
        private string changelogVersionNew = "";

        // Token: 0x0400000B RID: 11
        private string dllVersionNew = "";

        // Token: 0x0400000A RID: 10
        private string exeVersionNew = "";

        // Token: 0x04000010 RID: 16
        private bool foundNewChangelog;

        // Token: 0x0400000F RID: 15
        private bool foundNewDll;

        // Token: 0x0400000E RID: 14
        private bool foundNewExe;

        // Token: 0x04000011 RID: 17
        private bool foundNewLibrary;

        // Token: 0x0400000D RID: 13
        private string libraryVersionNew = "";

        // Token: 0x04000008 RID: 8
        private string message = "";

        // Token: 0x04000014 RID: 20
        private string remoteChangelogPath = "";

        // Token: 0x04000019 RID: 25
        private Version remoteChangelogVersion = new Version(1, 0, 0, 0);

        // Token: 0x04000012 RID: 18
        private string remoteDllPath = "";

        // Token: 0x04000018 RID: 24
        private Version remoteDllVersion = new Version(1, 0, 0, 0);

        // Token: 0x04000013 RID: 19
        private string remoteExePath = "";

        // Token: 0x04000017 RID: 23
        private Version remoteExeVersion = new Version(1, 0, 0, 0);

        // Token: 0x04000015 RID: 21
        private string remoteLibraryPath = "";

        // Token: 0x0400001A RID: 26
        private Version remoteLibraryVersion = new Version(1, 0, 0, 0);

        // Token: 0x04000016 RID: 22
        private bool updateFound;

        // Token: 0x04000009 RID: 9
        private string updaterVersionNew = "";

        // Token: 0x0600000C RID: 12 RVA: 0x0000240C File Offset: 0x0000060C
        public UpdaterWindow(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 2)
                {
                    if (args[0] == "quick")
                        QuickUpdate(args[1]);
                    else if (args[0] == "restart")
                        RestartServer(args[1]);
                    else
                        Console.WriteLine("Error: Unknown arguments.");
                }
                else
                {
                    Console.WriteLine("Error: Unknown arguments.");
                }
            }
            else
            {
                InitializeComponent();
                Init();
            }
        }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x0600000B RID: 11 RVA: 0x000023EC File Offset: 0x000005EC
        private bool Mono
        {
            get { return Environment.OSVersion.Platform == PlatformID.Unix; }
        }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000018 RID: 24 RVA: 0x00002EC4 File Offset: 0x000010C4
        public Version LocalExeVersion
        {
            get
            {
                Version result;
                if (CheckIfExists())
                    result = new Version(FileVersionInfo.GetVersionInfo("MCDziennyLava.exe").FileVersion);
                else
                    result = null;
                return result;
            }
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000019 RID: 25 RVA: 0x00002EFC File Offset: 0x000010FC
        public Version LocalDllVersion
        {
            get
            {
                Version result;
                if (CheckIfExists())
                    result = new Version(FileVersionInfo.GetVersionInfo("MCDzienny_.dll").FileVersion);
                else
                    result = null;
                return result;
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x0600001A RID: 26 RVA: 0x00002F34 File Offset: 0x00001134
        public Version LocalLibraryVersion
        {
            get
            {
                Version result;
                if (CheckIfExists())
                    result = new Version(FileVersionInfo.GetVersionInfo("MySql.Data.dll").FileVersion);
                else
                    result = null;
                return result;
            }
        }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x0600001D RID: 29 RVA: 0x00003034 File Offset: 0x00001234
        public Version LocalChangelogVersion
        {
            get { return new Version(changelogLocalVersion.ToString()); }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002580 File Offset: 0x00000780
        public void Init()
        {
            Write("# MCDzienny Updater started...");
            WriteLine("");
            WriteLine("");
            Write("# current version check...");
            WriteLine("");
            WriteLine("");
            Write("'MCDzienny_.dll' version: ");
            if (File.Exists("MCDzienny_.dll"))
            {
                Write(LocalDllVersion.ToString());
                WriteLine("");
            }
            else
            {
                WriteRed("file not found");
                WriteLine("");
            }

            Write("'MCDziennyLava.exe' version: ");
            if (File.Exists("MCDziennyLava.exe"))
            {
                Write(LocalExeVersion.ToString());
                WriteLine("");
            }
            else
            {
                WriteRed("file not found");
                WriteLine("");
            }

            Write("'Changelog.txt' version: ");
            if (File.Exists("extra/Changelog.txt"))
            {
                if (CheckChangelogVersion())
                {
                    Write(LocalChangelogVersion.ToString());
                }
                else
                {
                    WriteRed("unknown version");
                    WriteLine("");
                }
            }
            else
            {
                WriteRed("file not found");
                WriteLine("");
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002700 File Offset: 0x00000900
        public void CheckForNewVersions()
        {
            WriteLine("");
            WriteLine("");
            WriteLine("# looking for a new version...");
            WriteLine("");
            if (CheckForUpdates())
            {
                Write("New version of ");
                if (foundNewDll) Write("'MCDzienny_.dll' ");
                if (foundNewExe) Write("'MCDziennyLava.exe' ");
                if (foundNewChangelog) Write("'Changelog.txt' ");
                Write("was found!");
                WriteLine("");
                updateFound = true;
                btnUpdate.Text = "Perform Update";
            }
            else
            {
                WriteLine("No update was found.");
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x000027F0 File Offset: 0x000009F0
        public void PerformUpdate()
        {
            DownloadFiles();
            if (AreUnlocked())
            {
                ReplaceFiles();
                WriteLine("");
                WriteLine("Update was successful.");
                Thread.Sleep(2000);
                if (chkStartAfterUpdate.Checked)
                {
                    if (GetFileProcess("MCDziennyLava.exe") == null)
                    {
                        WriteLine("# starting server...");
                        Thread.Sleep(2000);
                        StartServer();
                    }

                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002898 File Offset: 0x00000A98
        public void QuickUpdate(string PID)
        {
            try
            {
                var processId = int.Parse(PID);
                var processById = Process.GetProcessById(processId);
                if (!processById.HasExited)
                {
                    Thread.Sleep(5000);
                    if (!processById.HasExited)
                    {
                        Thread.Sleep(5000);
                        processById.Kill();
                    }
                }
            }
            catch
            {
            }

            if (CheckForUpdates())
            {
                DownloadFiles();
                ReplaceFiles();
            }

            StartServer();
            Application.Exit();
            Thread.Sleep(5000);
            Process.GetCurrentProcess().Kill();
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002948 File Offset: 0x00000B48
        public void RestartServer(string PID)
        {
            try
            {
                var processId = int.Parse(PID);
                var processById = Process.GetProcessById(processId);
                if (!processById.HasExited)
                {
                    Thread.Sleep(5000);
                    if (!processById.HasExited)
                    {
                        Thread.Sleep(5000);
                        processById.Kill();
                    }
                }
            }
            catch
            {
            }

            StartServer();
            Application.Exit();
            Thread.Sleep(5000);
            Process.GetCurrentProcess().Kill();
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000029D8 File Offset: 0x00000BD8
        public bool DownloadFiles()
        {
            var webClient = new WebClient();
            var result = true;
            if (foundNewExe)
                try
                {
                    webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteExePath, "MCDziennyLava.new");
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    result = false;
                }

            if (foundNewDll)
                try
                {
                    webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteDllPath, "MCDzienny_.new");
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    result = false;
                }

            if (foundNewLibrary)
                try
                {
                    webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteLibraryPath, "library_.new");
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    result = false;
                }

            if (foundNewChangelog)
                try
                {
                    webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteChangelogPath, "Changelog.new");
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    result = false;
                }

            webClient.Dispose();
            return result;
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002B34 File Offset: 0x00000D34
        public void ReplaceFiles()
        {
            if (foundNewExe)
                try
                {
                    File.Replace("MCDziennyLava.new", "MCDziennyLava.exe", null);
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                }

            if (foundNewDll)
                try
                {
                    File.Replace("MCDzienny_.new", "MCDzienny_.dll", null);
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                }

            if (foundNewLibrary)
                try
                {
                    File.Replace("library_.new", "MySql.Data.dll", null);
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                }

            if (foundNewChangelog)
                try
                {
                    if (File.Exists("extra/Changelog.txt"))
                        File.Replace("Changelog.new", "extra/Changelog.txt", null);
                    else
                        File.Move("Changelog.new", "extra/Changelog.txt");
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                }
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002C68 File Offset: 0x00000E68
        public void StartServer()
        {
            if (Mono)
            {
                Process.Start("mono", "MCDziennyLava.exe");
            }
            else
            {
                var fullPath = Path.GetFullPath("MCDziennyLava.exe");
                Process.Start(fullPath);
            }
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002CAC File Offset: 0x00000EAC
        public bool CheckForUpdates()
        {
            if (DownloadManifestAndRead())
            {
                if (CheckIfExists())
                {
                    if (LocalExeVersion < remoteExeVersion) foundNewExe = true;
                    if (LocalDllVersion < remoteDllVersion) foundNewDll = true;
                    if (LocalLibraryVersion < remoteLibraryVersion) foundNewLibrary = true;
                }

                if (CheckIfChangelogExists())
                {
                    if (CheckChangelogVersion())
                        if (LocalChangelogVersion < remoteChangelogVersion)
                            foundNewChangelog = true;
                }
                else
                {
                    foundNewChangelog = true;
                }
            }

            return foundNewExe || foundNewDll || foundNewLibrary || foundNewChangelog;
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002DB0 File Offset: 0x00000FB0
        public bool CheckIfExists()
        {
            bool result;
            if (!File.Exists("MCDziennyLava.exe"))
            {
                ErrorLog(new FileNotFoundException("The core file wasn't found", "MCDziennyLava.exe"));
                result = false;
            }
            else if (!File.Exists("MCDzienny_.dll"))
            {
                ErrorLog(new FileNotFoundException("The core file wasn't found", "MCDzienny_.dll"));
                result = false;
            }
            else if (!File.Exists("MySql.Data.dll"))
            {
                ErrorLog(new FileNotFoundException("The core file wasn't found", "MySql.Data.dll"));
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002E40 File Offset: 0x00001040
        public bool CheckLocalVersions()
        {
            try
            {
                exeLocalVersion = new Version(FileVersionInfo.GetVersionInfo("MCDziennyLava.exe").FileVersion);
                dllLocalVersion = new Version(FileVersionInfo.GetVersionInfo("MCDzienny_.dll").FileVersion);
                libraryLocalVersion = new Version(FileVersionInfo.GetVersionInfo("MySql.Data.dll").FileVersion);
            }
            catch (Exception e)
            {
                ErrorLog(e);
                return false;
            }

            return true;
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002F6C File Offset: 0x0000116C
        public bool CheckIfChangelogExists()
        {
            return File.Exists("extra/Changelog.txt");
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00002F94 File Offset: 0x00001194
        public bool CheckChangelogVersion()
        {
            try
            {
                using (var streamReader = new StreamReader("extra/Changelog.txt"))
                {
                    changelogLocalVersion = new Version(streamReader.ReadLine().Trim().Split(' ')[0]);
                }
            }
            catch (Exception e)
            {
                ErrorLog(new Exception("Couldn't get changelog version."));
                ErrorLog(e);
                return false;
            }

            return true;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00003058 File Offset: 0x00001258
        private bool DownloadManifestAndRead()
        {
            Stream stream;
            bool result;
            if (DownloadManifest(out stream))
            {
                ReadManifest(stream);
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00003088 File Offset: 0x00001288
        private bool DownloadManifest(out Stream xml)
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

        // Token: 0x06000020 RID: 32 RVA: 0x000030E8 File Offset: 0x000012E8
        private bool AreUnlocked()
        {
            var result = true;
            var array = new Process[4];
            if (foundNewDll)
                if (GetFileProcess("MCDzienny_.dll") != null)
                    array[0] = GetFileProcess("MCDzienny_.dll");
            if (foundNewExe)
                if (GetFileProcess("MCDziennyLava.exe") != null)
                    array[1] = GetFileProcess("MCDziennyLava.exe");
            if (foundNewLibrary)
                if (GetFileProcess("MySql.Data.dll") != null)
                    array[3] = GetFileProcess("MySql.Data.dll");
            if (foundNewChangelog)
                if (CheckIfChangelogExists())
                    if (GetFileProcess("extra/Changelog.txt") != null)
                        array[2] = GetFileProcess("MCDziennyLava.exe");
            foreach (var process in array)
                if (process != null)
                {
                    WriteLine("");
                    WriteRed("You have to close the following process first: " + process.ProcessName);
                    result = false;
                }

            return result;
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00003240 File Offset: 0x00001440
        public Process GetFileProcess(string filePath)
        {
            var processes = Process.GetProcesses();
            var fullPath = Path.GetFullPath(filePath);
            foreach (var process in processes)
                if (process.MainWindowHandle != new IntPtr(0) && !process.HasExited)
                {
                    var array2 = new ProcessModule[process.Modules.Count];
                    foreach (var obj in process.Modules)
                    {
                        var processModule = (ProcessModule) obj;
                        if (processModule.FileName == fullPath) return process;
                    }
                }

            return null;
        }

        // Token: 0x06000022 RID: 34 RVA: 0x0000333C File Offset: 0x0000153C
        private void ReadManifest(Stream stream)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);
                XmlNodeList xmlNodeList = null;
                var firstChild = xmlDocument.FirstChild;
                if (firstChild.Name.ToLower() == "manifest-updates")
                    if (firstChild.FirstChild.Name.ToLower() == "manifest-update")
                        xmlNodeList = firstChild.FirstChild.ChildNodes;
                if (xmlNodeList != null)
                    for (var i = 0; i < xmlNodeList.Count; i++)
                    {
                        var localName = xmlNodeList[i].LocalName;
                        if (localName != null)
                        {
                            if (!(localName == "exe"))
                            {
                                if (!(localName == "dll"))
                                {
                                    if (!(localName == "changelog"))
                                    {
                                        if (!(localName == "library"))
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
                                            for (var j = 0; j < attributes2.Count; j++)
                                            {
                                                if (attributes2[j].Name == "version")
                                                {
                                                    libraryVersionNew = attributes2[j].Value;
                                                    try
                                                    {
                                                        remoteLibraryVersion = new Version(libraryVersionNew);
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }

                                                if (attributes2[j].Name == "link")
                                                    remoteLibraryPath = attributes2[j].Value;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var attributes3 = xmlNodeList[i].Attributes;
                                        for (var j = 0; j < attributes3.Count; j++)
                                        {
                                            if (attributes3[j].Name == "version")
                                            {
                                                changelogVersionNew = attributes3[j].Value;
                                                try
                                                {
                                                    remoteChangelogVersion = new Version(changelogVersionNew);
                                                }
                                                catch
                                                {
                                                }
                                            }

                                            if (attributes3[j].Name == "link")
                                                remoteChangelogPath = attributes3[j].Value;
                                        }
                                    }
                                }
                                else
                                {
                                    var attributes4 = xmlNodeList[i].Attributes;
                                    for (var j = 0; j < attributes4.Count; j++)
                                    {
                                        if (attributes4[j].Name == "version")
                                        {
                                            dllVersionNew = attributes4[j].Value;
                                            try
                                            {
                                                remoteDllVersion = new Version(dllVersionNew);
                                            }
                                            catch
                                            {
                                            }
                                        }

                                        if (attributes4[j].Name == "link") remoteDllPath = attributes4[j].Value;
                                    }
                                }
                            }
                            else
                            {
                                var attributes5 = xmlNodeList[i].Attributes;
                                for (var j = 0; j < attributes5.Count; j++)
                                {
                                    if (attributes5[j].Name == "version")
                                    {
                                        exeVersionNew = attributes5[j].Value;
                                        try
                                        {
                                            remoteExeVersion = new Version(exeVersionNew);
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    if (attributes5[j].Name == "link") remoteExePath = attributes5[j].Value;
                                }
                            }
                        }
                    }
            }
            catch (Exception e)
            {
                ErrorLog(e);
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00003860 File Offset: 0x00001A60
        public void ErrorLog(Exception e)
        {
            WriteRed(e.ToString());
            WriteLine("");
        }

        // Token: 0x06000024 RID: 36 RVA: 0x0000387C File Offset: 0x00001A7C
        public void WriteRed(string text)
        {
            logBox.AppendText(text);
            logBox.Select(logBox.TextLength - text.Length, text.Length);
            logBox.SelectionColor = Color.Red;
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        // Token: 0x06000025 RID: 37 RVA: 0x000038F4 File Offset: 0x00001AF4
        public void WriteLine(string text)
        {
            logBox.AppendText(text + "\r\n");
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00003941 File Offset: 0x00001B41
        public void Write(string text)
        {
            logBox.AppendText(text);
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00003979 File Offset: 0x00001B79
        private void UpdaterWindow_Load(object sender, EventArgs e)
        {
        }

        // Token: 0x06000028 RID: 40 RVA: 0x0000397C File Offset: 0x00001B7C
        private void button1_Click(object sender, EventArgs e)
        {
            if (!updateFound)
                CheckForNewVersions();
            else
                PerformUpdate();
        }
    }
}