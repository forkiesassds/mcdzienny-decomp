using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Updater
{
	public class UpdaterWindow : Form
	{
		private IContainer components = null;

		private RichTextBox logBox;

		private CheckBox chkStartAfterUpdate;

		private Button btnUpdate;

		private string message = "";

		private string updaterVersionNew = "";

		private string exeVersionNew = "";

		private string dllVersionNew = "";

		private string changelogVersionNew = "";

		private string libraryVersionNew = "";

		private bool foundNewExe = false;

		private bool foundNewDll = false;

		private bool foundNewChangelog = false;

		private bool foundNewLibrary = false;

		private string remoteDllPath = "";

		private string remoteExePath = "";

		private string remoteChangelogPath = "";

		private string remoteLibraryPath = "";

		private bool updateFound = false;

		private Version remoteExeVersion = new Version(1, 0, 0, 0);

		private Version remoteDllVersion = new Version(1, 0, 0, 0);

		private Version remoteChangelogVersion = new Version(1, 0, 0, 0);

		private Version remoteLibraryVersion = new Version(1, 0, 0, 0);

		private static Version exeLocalVersion;

		private static Version dllLocalVersion;

		private static Version libraryLocalVersion;

		private Version changelogLocalVersion;

		private bool Mono
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Unix;
			}
		}

		public Version LocalExeVersion
		{
			get
			{
				if (CheckIfExists())
				{
					return new Version(FileVersionInfo.GetVersionInfo("MCDziennyLava.exe").FileVersion);
				}
				return null;
			}
		}

		public Version LocalDllVersion
		{
			get
			{
				if (CheckIfExists())
				{
					return new Version(FileVersionInfo.GetVersionInfo("MCDzienny_.dll").FileVersion);
				}
				return null;
			}
		}

		public Version LocalLibraryVersion
		{
			get
			{
				if (CheckIfExists())
				{
					return new Version(FileVersionInfo.GetVersionInfo("MySql.Data.dll").FileVersion);
				}
				return null;
			}
		}

		public Version LocalChangelogVersion
		{
			get
			{
				return new Version(changelogLocalVersion.ToString());
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_023d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UpdaterWindow));
			logBox = new RichTextBox();
			chkStartAfterUpdate = new CheckBox();
			btnUpdate = new Button();
            SuspendLayout();
            logBox.Font = new Font("Arial", 9.75f, 0, (GraphicsUnit)3, 238);
            logBox.Location = new Point(12, 12);
            logBox.Name = "logBox";
            logBox.Size = new Size(394, 238);
            logBox.TabIndex = 0;
            logBox.Text = "";
            chkStartAfterUpdate.AutoSize = true;
            chkStartAfterUpdate.Location = new Point(12, 282);
            chkStartAfterUpdate.Name = "chkStartAfterUpdate";
            chkStartAfterUpdate.Size = new Size(166, 17);
            chkStartAfterUpdate.TabIndex = 1;
            chkStartAfterUpdate.Text = "Start MCDzienny after update";
            chkStartAfterUpdate.UseVisualStyleBackColor = true;
			chkStartAfterUpdate.Checked = true;
            btnUpdate.Location = new Point(270, 278);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(136, 23);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Check For Updates";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += button1_Click;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = (AutoScaleMode)1;
            this.ClientSize = new Size(418, 328);
            this.Controls.Add((Control)(object)btnUpdate);
            this.Controls.Add((Control)(object)chkStartAfterUpdate);
            this.Controls.Add((Control)(object)logBox);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Name = "UpdaterWindow";
            this.StartPosition = (FormStartPosition)1;
            this.Text = "Updater";
            this.Load += UpdaterWindow_Load;
            this.ResumeLayout(false);
            PerformLayout();
		}

		public UpdaterWindow(string[] args)
		{
			if (args.Length > 0)
			{
				if (args.Length == 2)
				{
					if (args[0] == "quick")
					{
						QuickUpdate(args[1]);
					}
					else if (args[0] == "restart")
					{
						RestartServer(args[1]);
					}
					else
					{
						Console.WriteLine("Error: Unknown arguments.");
					}
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

		public void Init()
		{
			Write("# MCDzienny Updater started...");
			WriteLine();
			WriteLine();
			Write("# current version check...");
			WriteLine();
			WriteLine();
			Write("'MCDzienny_.dll' version: ");
			if (File.Exists("MCDzienny_.dll"))
			{
				Write(LocalDllVersion.ToString());
				WriteLine();
			}
			else
			{
				WriteRed("file not found");
				WriteLine();
			}
			Write("'MCDziennyLava.exe' version: ");
			if (File.Exists("MCDziennyLava.exe"))
			{
				Write(LocalExeVersion.ToString());
				WriteLine();
			}
			else
			{
				WriteRed("file not found");
				WriteLine();
			}
			Write("'Changelog.txt' version: ");
			if (File.Exists("extra/Changelog.txt"))
			{
				if (CheckChangelogVersion())
				{
					Write(LocalChangelogVersion.ToString());
					return;
				}
				WriteRed("unknown version");
				WriteLine();
			}
			else
			{
				WriteRed("file not found");
				WriteLine();
			}
		}

		public void CheckForNewVersions()
		{
			WriteLine();
			WriteLine();
			WriteLine("# looking for a new version...");
			WriteLine();
			if (CheckForUpdates())
			{
				Write("New version of ");
				if (foundNewDll)
				{
					Write("'MCDzienny_.dll' ");
				}
				if (foundNewExe)
				{
					Write("'MCDziennyLava.exe' ");
				}
				if (foundNewChangelog)
				{
					Write("'Changelog.txt' ");
				}
				Write("was found!");
				WriteLine();
				updateFound = true;
                btnUpdate.Text = "Perform Update";
			}
			else
			{
				WriteLine("No update was found.");
			}
		}

		public void PerformUpdate()
		{
			DownloadFiles();
			if (!AreUnlocked())
			{
				return;
			}
			ReplaceFiles();
			WriteLine();
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

		public void QuickUpdate(string PID)
		{
			Process process = null;
			try
			{
				int processId = int.Parse(PID);
				process = Process.GetProcessById(processId);
				if (!process.HasExited)
				{
					Thread.Sleep(5000);
					if (!process.HasExited)
					{
						Thread.Sleep(5000);
						process.Kill();
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

		public void RestartServer(string PID)
		{
			Process process = null;
			try
			{
				int processId = int.Parse(PID);
				process = Process.GetProcessById(processId);
				if (!process.HasExited)
				{
					Thread.Sleep(5000);
					if (!process.HasExited)
					{
						Thread.Sleep(5000);
						process.Kill();
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

		public bool DownloadFiles()
		{
			WebClient webClient = new WebClient();
			bool result = true;
			if (foundNewExe)
			{
				try
				{
					webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteExePath, "MCDziennyLava.new");
				}
				catch (Exception e)
				{
					ErrorLog(e);
					result = false;
				}
			}
			if (foundNewDll)
			{
				try
				{
					webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteDllPath, "MCDzienny_.new");
				}
				catch (Exception e)
				{
					ErrorLog(e);
					result = false;
				}
			}
			if (foundNewLibrary)
			{
				try
				{
					webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteLibraryPath, "library_.new");
				}
				catch (Exception e)
				{
					ErrorLog(e);
					result = false;
				}
			}
			if (foundNewChangelog)
			{
				try
				{
					webClient.DownloadFile("http://mcdzienny.cba.pl" + remoteChangelogPath, "Changelog.new");
				}
				catch (Exception e)
				{
					ErrorLog(e);
					result = false;
				}
			}
			webClient.Dispose();
			return result;
		}

		public void ReplaceFiles()
		{
			if (foundNewExe)
			{
				try
				{
					File.Replace("MCDziennyLava.new", "MCDziennyLava.exe", null);
				}
				catch (Exception e)
				{
					ErrorLog(e);
				}
			}
			if (foundNewDll)
			{
				try
				{
					File.Replace("MCDzienny_.new", "MCDzienny_.dll", null);
				}
				catch (Exception e)
				{
					ErrorLog(e);
				}
			}
			if (foundNewLibrary)
			{
				try
				{
					File.Replace("library_.new", "MySql.Data.dll", null);
				}
				catch (Exception e)
				{
					ErrorLog(e);
				}
			}
			if (!foundNewChangelog)
			{
				return;
			}
			try
			{
				if (File.Exists("extra/Changelog.txt"))
				{
					File.Replace("Changelog.new", "extra/Changelog.txt", null);
				}
				else
				{
					File.Move("Changelog.new", "extra/Changelog.txt");
				}
			}
			catch (Exception e)
			{
				ErrorLog(e);
			}
		}

		public void StartServer()
		{
			if (Mono)
			{
				Process.Start("mono", "MCDziennyLava.exe");
				return;
			}
			string fullPath = Path.GetFullPath("MCDziennyLava.exe");
			Process.Start(fullPath);
		}

		public bool CheckForUpdates()
		{
			if (DownloadManifestAndRead())
			{
				if (CheckIfExists())
				{
					if (LocalExeVersion < remoteExeVersion)
					{
						foundNewExe = true;
					}
					if (LocalDllVersion < remoteDllVersion)
					{
						foundNewDll = true;
					}
					if (LocalLibraryVersion < remoteLibraryVersion)
					{
						foundNewLibrary = true;
					}
				}
				if (CheckIfChangelogExists())
				{
					if (CheckChangelogVersion() && LocalChangelogVersion < remoteChangelogVersion)
					{
						foundNewChangelog = true;
					}
				}
				else
				{
					foundNewChangelog = true;
				}
			}
			return foundNewExe || foundNewDll || foundNewLibrary || foundNewChangelog;
		}

		public bool CheckIfExists()
		{
			if (!File.Exists("MCDziennyLava.exe"))
			{
				ErrorLog(new FileNotFoundException("The core file wasn't found", "MCDziennyLava.exe"));
				return false;
			}
			if (!File.Exists("MCDzienny_.dll"))
			{
				ErrorLog(new FileNotFoundException("The core file wasn't found", "MCDzienny_.dll"));
				return false;
			}
			if (!File.Exists("MySql.Data.dll"))
			{
				ErrorLog(new FileNotFoundException("The core file wasn't found", "MySql.Data.dll"));
				return false;
			}
			return true;
		}

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

		public bool CheckIfChangelogExists()
		{
			if (!File.Exists("extra/Changelog.txt"))
			{
				return false;
			}
			return true;
		}

		public bool CheckChangelogVersion()
		{
			try
			{
				using (StreamReader streamReader = new StreamReader("extra/Changelog.txt"))
				{
					changelogLocalVersion = new Version(streamReader.ReadLine().Trim().Split(new char[1]
					{
						' '
					})[0]);
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

		private bool DownloadManifestAndRead()
		{
			Stream xml;
			if (DownloadManifest(out xml))
			{
				ReadManifest(xml);
				return true;
			}
			return false;
		}

		private bool DownloadManifest(out Stream xml)
		{
			WebClient webClient = new WebClient();
			xml = null;
			try
			{
				xml = new MemoryStream(webClient.DownloadData("http://mcdzienny.cba.pl/download/manifest.info"));
			}
			catch
			{
			}
			webClient.Dispose();
			if (xml == null)
			{
				return false;
			}
			return true;
		}

		private bool AreUnlocked()
		{
			bool result = true;
			Process[] array = new Process[4];
			if (foundNewDll && GetFileProcess("MCDzienny_.dll") != null)
			{
				array[0] = GetFileProcess("MCDzienny_.dll");
			}
			if (foundNewExe && GetFileProcess("MCDziennyLava.exe") != null)
			{
				array[1] = GetFileProcess("MCDziennyLava.exe");
			}
			if (foundNewLibrary && GetFileProcess("MySql.Data.dll") != null)
			{
				array[3] = GetFileProcess("MySql.Data.dll");
			}
			if (foundNewChangelog && CheckIfChangelogExists() && GetFileProcess("extra/Changelog.txt") != null)
			{
				array[2] = GetFileProcess("MCDziennyLava.exe");
			}
			Process[] array2 = array;
			foreach (Process process in array2)
			{
				if (process != null)
				{
					WriteLine();
					WriteRed("You have to close the following process first: " + process.ProcessName);
					result = false;
				}
			}
			return result;
		}

		public Process GetFileProcess(string filePath)
		{
			Process[] processes = Process.GetProcesses();
			string fullPath = Path.GetFullPath(filePath);
			Process[] array = processes;
			foreach (Process process in array)
			{
				if (!(process.MainWindowHandle != new IntPtr(0)) || process.HasExited)
				{
					continue;
				}
				ProcessModule[] array2 = new ProcessModule[process.Modules.Count];
				foreach (ProcessModule module in process.Modules)
				{
					if (module.FileName == fullPath)
					{
						return process;
					}
				}
			}
			return null;
		}

		private void ReadManifest(Stream stream)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
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
							XmlAttributeCollection attributes2 = val2[i].Attributes;
							for (int j = 0; j < attributes2.Count; j++)
							{
								if (attributes2[j].Name == "version")
								{
									exeVersionNew = attributes2[j].Value;
									try
									{
										remoteExeVersion = new Version(exeVersionNew);
									}
									catch
									{
									}
								}
								if (attributes2[j].Name == "link")
								{
									remoteExePath = attributes2[j].Value;
								}
							}
							break;
						}
						case "dll":
						{
							XmlAttributeCollection attributes4 = val2[i].Attributes;
							for (int j = 0; j < attributes4.Count; j++)
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
								if (attributes4[j].Name == "link")
								{
									remoteDllPath = attributes4[j].Value;
								}
							}
							break;
						}
						case "changelog":
						{
							XmlAttributeCollection attributes5 = val2[i].Attributes;
							for (int j = 0; j < attributes5.Count; j++)
							{
								if (attributes5[j].Name == "version")
								{
									changelogVersionNew = attributes5[j].Value;
									try
									{
										remoteChangelogVersion = new Version(changelogVersionNew);
									}
									catch
									{
									}
								}
								if (attributes5[j].Name == "link")
								{
									remoteChangelogPath = attributes5[j].Value;
								}
							}
							break;
						}
						case "library":
						{
							XmlAttributeCollection attributes3 = val2[i].Attributes;
							for (int j = 0; j < attributes3.Count; j++)
							{
								if (attributes3[j].Name == "version")
								{
									libraryVersionNew = attributes3[j].Value;
									try
									{
										remoteLibraryVersion = new Version(libraryVersionNew);
									}
									catch
									{
									}
								}
								if (attributes3[j].Name == "link")
								{
									remoteLibraryPath = attributes3[j].Value;
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
			catch (Exception e)
			{
				ErrorLog(e);
			}
		}

		public void ErrorLog(Exception e)
		{
			WriteRed(e.ToString());
			WriteLine();
		}

		public void WriteRed(string text)
		{
            logBox.AppendText(text);
            logBox.Select(logBox.TextLength - text.Length, text.Length);
			logBox.SelectionColor = Color.Red;
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
		}

		public void WriteLine(string text = "")
		{
            logBox.AppendText(text + "\r\n");
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
		}

		public void Write(string text)
		{
            logBox.AppendText(text);
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
		}

		private void UpdaterWindow_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!updateFound)
			{
				CheckForNewVersions();
			}
			else
			{
				PerformUpdate();
			}
		}
	}
}
