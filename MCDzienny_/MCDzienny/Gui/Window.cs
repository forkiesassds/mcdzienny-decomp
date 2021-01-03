using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MCDzienny.GUI;
using MCDzienny.InfectionSystem;
using MCDzienny.Misc;
using MCDzienny.Plugins;
using MCDzienny.Properties;
using MCDzienny.Settings;
using MCDzienny_.Gui;
using Message = System.Windows.Forms.Message;
using Timer = System.Timers.Timer;

namespace MCDzienny.Gui
{
    // Token: 0x0200035F RID: 863
    public partial class Window : Form
    {
        // Token: 0x04000CEB RID: 3307
        private const uint OBJID_VSCROLL = 4294967291U;

        // Token: 0x04000CEC RID: 3308
        private const int EM_LINESCROLL = 182;

        // Token: 0x04000CEE RID: 3310
        private static readonly object chatSynchronizationObject = new object();

        // Token: 0x04000CF3 RID: 3315
        internal static Server s;

        // Token: 0x04000CF8 RID: 3320
        public static Window thisWindow;

        // Token: 0x04000CF9 RID: 3321
        public static bool prevLoaded;

        // Token: 0x04000CFA RID: 3322
        public static bool lavaSettingsPrevLoaded;

        // Token: 0x04000CFB RID: 3323
        public static bool zombieSettingsPrevLoaded;

        // Token: 0x04000D09 RID: 3337
        public static volatile bool showWarning = true;

        // Token: 0x04000D02 RID: 3330
        private Queue chatQueue = Queue.Synchronized(new Queue(200));

        // Token: 0x04000CFF RID: 3327
        private ColorChatSettings colorChatSettings;

        // Token: 0x04000D06 RID: 3334
        private CreateMap createMap;

        // Token: 0x04000CFD RID: 3325
        private Form LavaPropertiesForm;

        // Token: 0x04000CF5 RID: 3317
        public volatile bool loaded;

        // Token: 0x04000CF0 RID: 3312
        public NotifyIcon notifyIcon1 = new NotifyIcon();

        // Token: 0x04000CF1 RID: 3313
        public volatile int pendingPacketsAvg;

        // Token: 0x04000CF2 RID: 3314
        public volatile int pendingPacketsSum;

        // Token: 0x04000CFE RID: 3326
        private PropertyWindow PropertyForm;

        // Token: 0x04000CED RID: 3309
        private Regex regex =
            new Regex(
                "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

        // Token: 0x04000D05 RID: 3333
        private Player selectedPlayer;

        // Token: 0x04000D04 RID: 3332
        private string selectedPlayerName = "";

        // Token: 0x04000CF7 RID: 3319
        private bool shuttingDown;

        // Token: 0x04000CF4 RID: 3316
        private readonly SplashScreen2 splashScreen2;

        // Token: 0x04000D0A RID: 3338
        private int split2Height;

        // Token: 0x04000D0D RID: 3341
        private int split3Width;

        // Token: 0x04000D0B RID: 3339
        private int split4Height;

        // Token: 0x04000D0C RID: 3340
        private int split5Width;

        // Token: 0x04000CF6 RID: 3318

        // Token: 0x04000D00 RID: 3328
        private Tools toolsForm;

        // Token: 0x04000D01 RID: 3329
        private Form UpdateForm;

        // Token: 0x04000D08 RID: 3336
        private readonly UpdateListViewDelegate UpdateMapsList;

        // Token: 0x04000D07 RID: 3335
        private readonly UpdateListViewDelegate UpdatePlayerList;

        // Token: 0x04000CFC RID: 3324
        private Form ZombiePropertiesForm;

        // Token: 0x060018C2 RID: 6338 RVA: 0x000A9B00 File Offset: 0x000A7D00
        public Window(FormWindowState startState = FormWindowState.Normal)
        {
            if (startState == FormWindowState.Minimized)
            {
                StartMinimized = true;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
            else
            {
                splashScreen2 = new SplashScreen2();
                splashScreen2.SetBitmap(Resources.splashScreen);
                splashScreen2.Show();
            }

            UpdatePlayerList = UpdatePlayerListView;
            UpdateMapsList = UpdateMapsListView;
            InitializeComponent();
            InitializeStatus();
            if (!showWarning) chatWarningLabel.Visible = false;
        }

        // Token: 0x170008FA RID: 2298
        // (get) Token: 0x060018C0 RID: 6336 RVA: 0x000A9AEC File Offset: 0x000A7CEC
        // (set) Token: 0x060018C1 RID: 6337 RVA: 0x000A9AF4 File Offset: 0x000A7CF4
        public bool StartMinimized { get; set; }

        // Token: 0x170008FB RID: 2299
        // (get) Token: 0x0600194A RID: 6474 RVA: 0x000ADEBC File Offset: 0x000AC0BC
        private Point FormCenter
        {
            get
            {
                var num = Location.X + Size.Width / 2;
                var y = Location.Y + Size.Height / 2;
                return new Point(num, y);
            }
        }

        // Token: 0x060018B8 RID: 6328
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);

        // Token: 0x060018B9 RID: 6329
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int value);

        // Token: 0x060018BA RID: 6330
        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);

        // Token: 0x060018BB RID: 6331
        [DllImport("user32.dll")]
        private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        // Token: 0x060018BC RID: 6332
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Token: 0x060018BD RID: 6333
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetScrollBarInfo(IntPtr hWnd, uint idObject, ref SCROLLBARINFO psbi);

        // Token: 0x14000027 RID: 39
        // (add) Token: 0x060018BE RID: 6334 RVA: 0x000A9A84 File Offset: 0x000A7C84
        // (remove) Token: 0x060018BF RID: 6335 RVA: 0x000A9AB8 File Offset: 0x000A7CB8
        public static event EventHandler Minimize;

        // Token: 0x060018C3 RID: 6339 RVA: 0x000A9BD4 File Offset: 0x000A7DD4
        private void InitializeStatus()
        {
            var timer = new Timer(50000.0);
            timer.Elapsed += delegate
            {
                var timeFormat = "";
                var timeSpan = DateTime.Now.Subtract(Server.TimeOnline);
                if (timeSpan.Days > 0)
                    timeFormat = string.Concat(timeSpan.Days, "d ", timeSpan.Hours, "h ", timeSpan.Minutes, "min");
                else if (timeSpan.Hours > 0)
                    timeFormat = string.Concat(timeSpan.Hours, "h ", timeSpan.Minutes, "min");
                else
                    timeFormat = timeSpan.Minutes + "min";
                thisWindow.toolStripStatusLabelUptime.GetCurrentParent().BeginInvoke(new Action(delegate
                {
                    toolStripStatusLabelUptime.Text = "Uptime : " + timeFormat;
                }));
            };
            timer.Start();
            toolStripStatusLabelRoundTime.Visible = Server.mode == Mode.Lava || Server.mode == Mode.LavaFreebuild;
        }

        // Token: 0x060018C4 RID: 6340 RVA: 0x000A9C2C File Offset: 0x000A7E2C
        private void Window_Minimize(object sender, EventArgs e)
        {
        }

        // Token: 0x060018C5 RID: 6341 RVA: 0x000A9C30 File Offset: 0x000A7E30
        protected override void OnShown(EventArgs e)
        {
        }

        // Token: 0x060018C6 RID: 6342 RVA: 0x000A9C34 File Offset: 0x000A7E34
        private void Window_Load(object sender, EventArgs e)
        {
            Hide();
            thisWindow = this;
            Text = "<server name here>";
            Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("MCDzienny.mcdzienny.ico"));
            if (!StartMinimized)
                WindowState = FormWindowState.Normal;
            else
                Hide();
            LoadGuiSettings();
            s = new Server();
            s.OnLog += WriteLine;
            s.OnCommand += newCommand;
            s.OnError += newError;
            s.OnSystem += newSystem;
            foreach (var obj in mainTabs.TabPages)
            {
                var tabPage = (TabPage) obj;
                mainTabs.SelectTab(tabPage);
            }

            mainTabs.SelectTab(mainTabs.TabPages[0]);
            s.HeartBeatFail += HeartBeatFail;
            s.OnURLChange += UpdateUrl;
            s.OnPlayerListChange += UpdateClientList;
            s.OnSettingsUpdate += SettingsUpdate;
            s.Start();
            notifyIcon1.Text = "MCDzienny Server: " + Server.name;
            notifyIcon1.ContextMenuStrip = iconContext;
            notifyIcon1.Icon = Icon;
            notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            var timer = new Timer(10000.0);
            timer.Elapsed += delegate
            {
                if (Server.shuttingDown) return;
                UpdateMapList("'");
                FillMainMapListView();
            };
            timer.Start();
            var timer2 = new Timer(60000.0);
            timer2.Elapsed += delegate
            {
                if (Server.shuttingDown) return;
                UpdateClientList();
            };
            timer2.Start();
            if (File.Exists(Logger.ErrorLogPath))
            {
                var array = File.ReadAllLines(Logger.ErrorLogPath);
                if (array.Length > 200)
                {
                    var array2 = new string[200];
                    Array.Copy(array, array.Length - 200, array2, 0, 200);
                    txtErrors.Lines = array2;
                }
                else
                {
                    txtErrors.Lines = array;
                }
            }

            if (File.Exists("extra/Changelog.txt"))
                foreach (var str in File.ReadAllLines("extra/Changelog.txt"))
                    txtChangelog.AppendText("\r\n           " + str);
            FontFamily fontFamily = null;
            foreach (var fontFamily2 in FontFamily.Families)
                if (string.Equals(fontFamily2.Name, GeneralSettings.All.ChatFontFamily))
                {
                    fontFamily = fontFamily2;
                    break;
                }

            if (fontFamily != null) chatMainBox.Font = new Font(fontFamily, GeneralSettings.All.ChatFontSize);
            object[] items =
            {
                "black",
                "navy",
                "green",
                "teal",
                "maroon",
                "purple",
                "gold",
                "silver",
                "gray",
                "blue",
                "lime",
                "aqua",
                "red",
                "pink",
                "yellow",
                "white"
            };
            playerColorCombo.Items.AddRange(items);
            RefreshUnloadedMapsList();
            var path = Application.StartupPath + "\\Plugins";
            DirectoryUtil.CreateIfNotExists(path);
            var instance = new AddPlugin();
            Server.Plugins.AvailablePlugins.Add(new AvailablePlugin
            {
                Instance = instance,
                IsCore = true
            });
            Server.Plugins.AvailablePlugins.Add(new AvailablePlugin
            {
                Instance = new RemovePlugin(),
                IsCore = true
            });
            Server.Plugins.AvailablePlugins.Add(new AvailablePlugin
            {
                Instance = new PluginKeyboardShortcuts(),
                IsCore = true
            });
            Server.Plugins.AvailablePlugins.Add(new AvailablePlugin
            {
                Instance = new ImportFromDat(),
                IsCore = true
            });
            lblPluginAuthor.Text = "";
            lblPluginDesc.Text = "";
            lblPluginName.Text = "";
            lblPluginVersion.Text = "";
            foreach (var availablePlugin in Server.Plugins.AvailablePlugins)
            {
                var node = new TreeNode(availablePlugin.Instance.Name);
                treeView1.Nodes.Add(node);
            }

            if (splashScreen2 != null) splashScreen2.Hide();
            base.Show();
        }

        // Token: 0x060018C7 RID: 6343 RVA: 0x000AA1C8 File Offset: 0x000A83C8
        public void RemoveNodeFromPluginList(string name)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(delegate { RemoveNodeFromPluginList(name); }));
                return;
            }

            TreeNode treeNode = null;
            foreach (var obj in treeView1.Nodes)
            {
                var treeNode2 = (TreeNode) obj;
                if (treeNode2.Text == name)
                {
                    treeNode = treeNode2;
                    break;
                }
            }

            if (treeNode != null) treeView1.Nodes.Remove(treeNode);
        }

        // Token: 0x060018C8 RID: 6344 RVA: 0x000AA28C File Offset: 0x000A848C
        public void AddNodeToPluginList(TreeNode node)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(delegate { AddNodeToPluginList(node); }));
                return;
            }

            treeView1.Nodes.Add(node);
        }

        // Token: 0x060018C9 RID: 6345 RVA: 0x000AA2E8 File Offset: 0x000A84E8
        public void UpdateMainMapListView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateMainMapListView));
                return;
            }

            FillMainMapListView();
        }

        // Token: 0x060018CA RID: 6346 RVA: 0x000AA30C File Offset: 0x000A850C
        private void FillMainMapListView()
        {
            var listItems = new List<string[]>();
            Server.levels.ForEach(delegate(Level l)
            {
                var array = new string[5];
                if (l.mapType == MapType.Freebuild)
                    array[0] = "Freebuild";
                else if (l.mapType == MapType.Lava)
                    array[0] = "Lava";
                else if (l.mapType == MapType.Zombie)
                    array[0] = "Zombie";
                else if (l.mapType == MapType.Home)
                    array[0] = "Home";
                else
                    array[0] = "MyMap";
                array[1] = l.name;
                array[2] = Level.GetPhysicsNameByNumber(l.physics).ToLower();
                array[3] = l.PlayersCount.ToString();
                array[4] = l.Weight == 0 ? "" : (l.Weight / 1024f).ToString("##,#.##") + "KB";
                listItems.Add(array);
            });
            UpdateMapsListView(listItems);
        }

        // Token: 0x060018CB RID: 6347 RVA: 0x000AA34C File Offset: 0x000A854C
        private void SettingsUpdate()
        {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired)
            {
                VoidDelegate method = SettingsUpdate;
                Invoke(method);
                return;
            }

            Text = Server.name + " MCDzienny Version: " + Server.Version;
            chatOnOff_btn.Text = GeneralSettings.All.UseChat ? "Activated" : "Deactivated";
            if (Server.mode == Mode.Freebuild || Server.mode == Mode.Zombie)
                toolStripStatusLabelRoundTime.Visible = false;
        }

        // Token: 0x060018CD RID: 6349 RVA: 0x000AA4E4 File Offset: 0x000A86E4
        private void HeartBeatFail()
        {
            WriteLine("Recent Heartbeat Failed");
        }

        // Token: 0x060018CE RID: 6350 RVA: 0x000AA4F4 File Offset: 0x000A86F4
        private void newError(string message)
        {
            try
            {
                if (txtErrors.InvokeRequired)
                {
                    ErrorDelegate method = newError;
                    Invoke(method, message);
                }
                else
                {
                    txtErrors.AppendText(Environment.NewLine + message);
                    if (txtErrors.Text.Length > 3000)
                    {
                        var num = txtLog.Text.IndexOf('\n', txtErrors.Text.Length - 2300) + 1;
                        if (num == -1) num = txtErrors.Text.Length - 2300;
                        txtErrors.Text = txtErrors.Text.Substring(num);
                        txtErrors.SelectionStart = txtErrors.TextLength;
                        txtErrors.ScrollToCaret();
                        txtErrors.Refresh();
                    }
                }
            }
            catch
            {
            }
        }

        // Token: 0x060018CF RID: 6351 RVA: 0x000AA608 File Offset: 0x000A8808
        private void newSystem(string message)
        {
            try
            {
                if (txtSystem.InvokeRequired)
                {
                    SystemDelegate method = newSystem;
                    Invoke(method, message);
                }
                else
                {
                    txtSystem.AppendText(Environment.NewLine + message);
                    if (txtSystem.Text.Length > 3000)
                    {
                        var num = txtSystem.Text.IndexOf('\n', txtSystem.Text.Length - 2300) + 1;
                        if (num == -1) num = txtSystem.Text.Length - 2300;
                        txtSystem.Text = txtSystem.Text.Substring(num);
                        txtSystem.SelectionStart = txtSystem.TextLength;
                        txtSystem.ScrollToCaret();
                        txtSystem.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060018D0 RID: 6352 RVA: 0x000AA724 File Offset: 0x000A8924
        public void Write(string s)
        {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired)
            {
                LogDelegate method = Write;
                Invoke(method, s);
                return;
            }

            txtLog.AppendText(s);
        }

        // Token: 0x060018D1 RID: 6353 RVA: 0x000AA778 File Offset: 0x000A8978
        public void WriteLine(string s)
        {
            if (shuttingDown) return;
            if (InvokeRequired)
            {
                LogDelegate method = WriteLine;
                Invoke(method, s);
                return;
            }

            var scrollbarinfo = default(SCROLLBARINFO);
            scrollbarinfo.cbSize = Marshal.SizeOf(scrollbarinfo);
            GetScrollBarInfo(txtLog.Handle, 4294967291U, ref scrollbarinfo);
            var flag = scrollbarinfo.xyThumbBottom > scrollbarinfo.rcScrollBar.Bottom - scrollbarinfo.rcScrollBar.Top -
                2 * scrollbarinfo.dxyLineButton;
            if (flag)
            {
                txtLog.AppendText("\r\n" + s);
            }
            else
            {
                var scrollPos = GetScrollPos(txtLog.Handle, 1);
                var textBox = txtLog;
                textBox.Text = textBox.Text + "\r\n" + s;
                SetScrollPos(txtLog.Handle, 1, scrollPos, true);
                SendMessage(txtLog.Handle, 182, 1, scrollPos);
            }

            if (txtLog.Text.Length > 20000)
            {
                var num = txtLog.Text.IndexOf('\n', txtLog.Text.Length - 14000) + 1;
                if (num == -1) num = txtLog.Text.Length - 14000;
                txtLog.Text = txtLog.Text.Substring(num);
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
                txtLog.Refresh();
            }
        }

        // Token: 0x060018D2 RID: 6354 RVA: 0x000AA938 File Offset: 0x000A8B38
        public void UpdateClientList()
        {
            if (InvokeRequired)
            {
                PlayerListCallback method = UpdateClientList;
                Invoke(method);
                return;
            }

            try
            {
                chatPlayerList.Items.Clear();
                playersListView.Items.Clear();
                Player.players.ForEach(delegate(Player p)
                {
                    if (p == null)
                    {
                        Server.s.Log("Error gui: p == null");
                        return;
                    }

                    if (p.group == null)
                    {
                        Server.s.Log("Error gui: p.group == null");
                        return;
                    }

                    if (p.name == null)
                    {
                        Server.s.Log("Error gui: p.name == null");
                        return;
                    }

                    if (p.level == null)
                    {
                        Server.s.Log("Error gui: p.level == null");
                        return;
                    }

                    if (p.level.name == null)
                    {
                        Server.s.Log("Error gui: p.level.name == null");
                        return;
                    }

                    var listViewItem = new ListViewItem(new[]
                    {
                        p.name,
                        p.group.name,
                        p.level.name
                    });
                    listViewItem.Name = p.name;
                    playersListView.Items.Add(listViewItem);
                    chatPlayerList.Items.Add(p.name);
                });
                var array = playersListView.Items.Find(selectedPlayerName, false);
                if (array.Length > 0)
                {
                    array[0].Selected = true;
                }
                else
                {
                    selectedPlayerName = "";
                    selectedPlayer = null;
                }

                playersListView.Update();
                playersList_DataSourceChanged();
                pCount.Text = Player.players.Count.ToString();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060018D3 RID: 6355 RVA: 0x000AAA28 File Offset: 0x000A8C28
        public void UpdateMapList(string _)
        {
            if (InvokeRequired)
            {
                LogDelegate method = UpdateMapList;
                Invoke(method, _);
                return;
            }

            mapsList.Items.Clear();
            var pcount = 0;
            using (var enumerator = Server.levels.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var level = enumerator.Current;
                    pcount = 0;
                    Player.players.ForEach(delegate(Player who)
                    {
                        if (who.level == level) pcount++;
                    });
                    mapsList.Items.Add(level.name);
                    mapsList_DataSourceChanged();
                    mCount.Text = Server.levels.Count.ToString();
                }
            }
        }

        // Token: 0x060018D4 RID: 6356 RVA: 0x000AAB38 File Offset: 0x000A8D38
        public void UpdateProperties()
        {
            mode.SelectedIndex = (int) (Server.mode != Mode.Zombie ? Server.mode : Mode.ZombieFreebuild);
        }

        // Token: 0x060018D5 RID: 6357 RVA: 0x000AAB5C File Offset: 0x000A8D5C
        public void UpdateLavaMaps()
        {
            if (mapsGrid.Rows.Count < LavaSystem.lavaMaps.Count)
                mapsGrid.Rows.Add(LavaSystem.lavaMaps.Count - mapsGrid.Rows.Count);
            for (var i = 0; i < LavaSystem.lavaMaps.Count; i++)
            {
                mapsGrid.Rows[i].Cells[0].Value = LavaSystem.lavaMaps[i].Name;
                mapsGrid.Rows[i].Cells[1].Value = LavaSystem.lavaMaps[i].LavaSources[0].X;
                mapsGrid.Rows[i].Cells[2].Value = LavaSystem.lavaMaps[i].LavaSources[0].Y;
                mapsGrid.Rows[i].Cells[3].Value = LavaSystem.lavaMaps[i].LavaSources[0].Z;
                mapsGrid.Rows[i].Cells[4].Value = LavaSystem.lavaMaps[i].Phase1;
                mapsGrid.Rows[i].Cells[5].Value = LavaSystem.lavaMaps[i].Phase2;
                mapsGrid.Rows[i].Cells[6].Value = LavaSystem.lavaMaps[i].LavaSources[0].Block;
            }
        }

        // Token: 0x060018D6 RID: 6358 RVA: 0x000AAD68 File Offset: 0x000A8F68
        public void UpdateInfectionMaps()
        {
            if (infectionMapsGrid.Rows.Count < InfectionMaps.infectionMaps.Count)
                mapsGrid.Rows.Add(InfectionMaps.infectionMaps.Count - infectionMapsGrid.Rows.Count);
            for (var i = 0; i < InfectionMaps.infectionMaps.Count; i++)
            {
                infectionMapsGrid.Rows[i].Cells[0].Value = InfectionMaps.infectionMaps[i].Name;
                infectionMapsGrid.Rows[i].Cells[4].Value = InfectionMaps.infectionMaps[i].CountdownSeconds;
                infectionMapsGrid.Rows[i].Cells[5].Value = InfectionMaps.infectionMaps[i].RoundTimeMinutes;
            }
        }

        // Token: 0x060018D7 RID: 6359 RVA: 0x000AAE78 File Offset: 0x000A9078
        public void UpdatePackets(int packets)
        {
        }

        // Token: 0x060018D8 RID: 6360 RVA: 0x000AAE7C File Offset: 0x000A907C
        public void UpdateUrl(string s)
        {
            if (InvokeRequired)
            {
                StringCallback method = UpdateUrl;
                Invoke(method, s);
                return;
            }

            txtUrl.Text = s;
        }

        // Token: 0x060018D9 RID: 6361 RVA: 0x000AAEC0 File Offset: 0x000A90C0
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (shuttingDown) return;
            GeneralSettings.All.Save();
            SaveGuiSettings();
            ServerProperties.Save();
            Server.Plugins.ClosePlugins();
            if (!Server.restarting && DialogResult.Yes != MessageBox.Show("Shutdown the server?", "Shutdown",
                MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
            {
                e.Cancel = true;
                return;
            }

            if (notifyIcon1 != null) notifyIcon1.Visible = false;
            shuttingDown = true;
            Program.ExitProgram(false);
        }

        // Token: 0x060018DA RID: 6362 RVA: 0x000AAF3C File Offset: 0x000A913C
        private bool ConfirmationQuestionPopup(string actionName)
        {
            return DialogResult.Yes == MessageBox.Show(actionName + " ?", "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        // Token: 0x060018DB RID: 6363 RVA: 0x000AAF5C File Offset: 0x000A915C
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (txtInput.Text == null || txtInput.Text.Trim() == "") return;
                if (!ServerProperties.ValidString(txtInput.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
                {
                    txtInput.Text = "Invalid character detected.";
                    return;
                }

                var text = txtInput.Text.Trim();
                if (txtInput.Text[0] == '#')
                {
                    var text2 = text.Remove(0, 1).Trim();
                    Player.GlobalMessageOps(string.Concat("To Ops &f-", Server.DefaultColor,
                        Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 2), "&f- ", text2));
                    Server.s.Log("(OPs): Console: " + text2);
                    Player.IRCSay("Console: " + text2, true);
                    txtInput.Clear();
                    return;
                }

                Player.GlobalMessage(Server.ConsoleRealName + txtInput.Text);
                Player.IRCSay(Server.ConsoleRealNameIRC + txtInput.Text);
                Server.s.Log("<CONSOLE> " + txtInput.Text);
                txtInput.Clear();
            }
        }

        // Token: 0x060018DC RID: 6364 RVA: 0x000AB0EC File Offset: 0x000A92EC
        private void txtCommands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                var sentMsg = "";
                if (txtCommands.Text == null || txtCommands.Text.Trim() == "")
                {
                    Server.s.CommandUsed("CONSOLE: Whitespace commands are not allowed.");
                    txtCommands.Clear();
                    return;
                }

                if (txtCommands.Text[0] == '/' && txtCommands.Text.Length > 1)
                    txtCommands.Text = txtCommands.Text.Substring(1);
                string text;
                if (txtCommands.Text.IndexOf(' ') != -1)
                {
                    text = txtCommands.Text.Split(' ')[0];
                    sentMsg = txtCommands.Text.Substring(txtCommands.Text.IndexOf(' ') + 1);
                }
                else
                {
                    if (!(txtCommands.Text != "")) return;
                    text = txtCommands.Text;
                }

                try
                {
                    var found = Command.all.Find(text);
                    if (found != null)
                    {
                        if (!found.ConsoleAccess)
                        {
                            Server.s.CommandUsed(string.Format("You can't use {0} command from console.", text));
                            txtCommands.Text = "";
                            return;
                        }

                        new Thread(delegate()
                        {
                            try
                            {
                                found.Use(null, sentMsg);
                            }
                            catch (Exception ex2)
                            {
                                Server.ErrorLog(ex2);
                            }
                        }).Start();
                        if (found.HighSecurity)
                            Server.s.CommandUsed("CONSOLE: USED /" + text + " ***");
                        else
                            Server.s.CommandUsed("CONSOLE: USED /" + text + " " + sentMsg);
                    }

                    if (found == null) Server.s.CommandUsed("CONSOLE: Command  '/" + text + "'  does not exist.");
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    Server.s.CommandUsed("CONSOLE: Failed command.");
                }

                txtCommands.Clear();
            }
        }

        // Token: 0x060018DD RID: 6365 RVA: 0x000AB360 File Offset: 0x000A9560
        public void newCommand(string p)
        {
            if (txtCommandsUsed.InvokeRequired)
            {
                LogDelegate method = newCommand;
                Invoke(method, p);
                return;
            }

            txtCommandsUsed.AppendText("\r\n" + p);
        }

        // Token: 0x060018DE RID: 6366 RVA: 0x000AB3B4 File Offset: 0x000A95B4
        private void ChangeCheck(string newCheck)
        {
            Server.ConsoleName = newCheck;
        }

        // Token: 0x060018DF RID: 6367 RVA: 0x000AB3BC File Offset: 0x000A95BC
        private void btnProperties_Click_1(object sender, EventArgs e)
        {
            if (!prevLoaded)
            {
                PropertyForm = new PropertyWindow();
                prevLoaded = true;
            }

            PropertyForm.ShowAt(FormCenter);
        }

        // Token: 0x060018E0 RID: 6368 RVA: 0x000AB3E8 File Offset: 0x000A95E8
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
        }

        // Token: 0x060018E1 RID: 6369 RVA: 0x000AB3EC File Offset: 0x000A95EC
        private void gBChat_Enter(object sender, EventArgs e)
        {
        }

        // Token: 0x060018E2 RID: 6370 RVA: 0x000AB3F0 File Offset: 0x000A95F0
        private void btnExtra_Click_1(object sender, EventArgs e)
        {
            if (!prevLoaded)
            {
                PropertyForm = new PropertyWindow();
                prevLoaded = true;
            }

            PropertyForm.Show();
            PropertyForm.Top = Top + Height - txtCommandsUsed.Height;
            PropertyForm.Left = Left;
        }

        // Token: 0x060018E3 RID: 6371 RVA: 0x000AB458 File Offset: 0x000A9658
        private void Window_Resize(object sender, EventArgs e)
        {
            Hide();
        }

        // Token: 0x060018E4 RID: 6372 RVA: 0x000AB460 File Offset: 0x000A9660
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            base.Show();
            BringToFront();
            WindowState = FormWindowState.Normal;
        }

        // Token: 0x060018E5 RID: 6373 RVA: 0x000AB478 File Offset: 0x000A9678
        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateForm = new UpdateWindow();
            UpdateForm.Show();
        }

        // Token: 0x060018E6 RID: 6374 RVA: 0x000AB490 File Offset: 0x000A9690
        private void tmrRestart_Tick(object sender, EventArgs e)
        {
            if (Server.autorestart && DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.TimeOfDay) > 0 &&
                DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.AddSeconds(1.0).TimeOfDay) < 0)
            {
                Player.GlobalMessage("The time is now " + DateTime.Now.TimeOfDay);
                Player.GlobalMessage("The server will now begin auto restart procedures.");
                Server.s.Log("The time is now " + DateTime.Now.TimeOfDay);
                Server.s.Log("The server will now begin auto restart procedures.");
                if (notifyIcon1 != null)
                {
                    notifyIcon1.Icon = null;
                    notifyIcon1.Visible = false;
                }

                Program.ExitProgram(true);
            }
        }

        // Token: 0x060018E7 RID: 6375 RVA: 0x000AB590 File Offset: 0x000A9790
        private void openConsole_Click(object sender, EventArgs e)
        {
            base.Show();
            BringToFront();
            WindowState = FormWindowState.Normal;
            base.Show();
            BringToFront();
            WindowState = FormWindowState.Normal;
        }

        // Token: 0x060018E8 RID: 6376 RVA: 0x000AB5B8 File Offset: 0x000A97B8
        private void hideConsole_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        // Token: 0x060018E9 RID: 6377 RVA: 0x000AB5C8 File Offset: 0x000A97C8
        private void shutdownServer_Click(object sender, EventArgs e)
        {
            ServerProperties.Save();
            if (notifyIcon1 != null) notifyIcon1.Visible = false;
            Program.ExitProgram(false);
        }

        // Token: 0x060018EA RID: 6378 RVA: 0x000AB5EC File Offset: 0x000A97EC
        private void voiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null) Command.all.Find("voice").Use(null, player.name);
        }

        // Token: 0x060018EB RID: 6379 RVA: 0x000AB628 File Offset: 0x000A9828
        private void whoisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null) Command.all.Find("whois").Use(null, player.name);
        }

        // Token: 0x060018EC RID: 6380 RVA: 0x000AB664 File Offset: 0x000A9864
        private void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null)
                Command.all.Find("kick").Use(null, player.name + " You have been kicked by the console.");
        }

        // Token: 0x060018ED RID: 6381 RVA: 0x000AB6AC File Offset: 0x000A98AC
        private void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null) Command.all.Find("ban").Use(null, player.name);
        }

        // Token: 0x060018EE RID: 6382 RVA: 0x000AB6E8 File Offset: 0x000A98E8
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 0");
                FillMainMapListView();
            }
        }

        // Token: 0x060018EF RID: 6383 RVA: 0x000AB734 File Offset: 0x000A9934
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 1");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F0 RID: 6384 RVA: 0x000AB780 File Offset: 0x000A9980
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 2");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F1 RID: 6385 RVA: 0x000AB7CC File Offset: 0x000A99CC
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 3");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F2 RID: 6386 RVA: 0x000AB818 File Offset: 0x000A9A18
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 4");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F3 RID: 6387 RVA: 0x000AB864 File Offset: 0x000A9A64
        private void unloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("unload").Use(null, level.name);
                FillMainMapListView();
            }
        }

        // Token: 0x060018F4 RID: 6388 RVA: 0x000AB8A8 File Offset: 0x000A9AA8
        private void finiteModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " finite");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F5 RID: 6389 RVA: 0x000AB8F4 File Offset: 0x000A9AF4
        private void animalAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " ai");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F6 RID: 6390 RVA: 0x000AB940 File Offset: 0x000A9B40
        private void edgeWaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " edge");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F7 RID: 6391 RVA: 0x000AB98C File Offset: 0x000A9B8C
        private void growingGrassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " grass");
                FillMainMapListView();
            }
        }

        // Token: 0x060018F8 RID: 6392 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
        private void survivalDeathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null) Command.all.Find("map").Use(null, level.name + " death");
        }

        // Token: 0x060018F9 RID: 6393 RVA: 0x000ABA20 File Offset: 0x000A9C20
        private void killerBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " killer");
                FillMainMapListView();
            }
        }

        // Token: 0x060018FA RID: 6394 RVA: 0x000ABA6C File Offset: 0x000A9C6C
        private void rPChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " chat");
                FillMainMapListView();
            }
        }

        // Token: 0x060018FB RID: 6395 RVA: 0x000ABAB8 File Offset: 0x000A9CB8
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("save").Use(null, level.name);
                FillMainMapListView();
            }
        }

        // Token: 0x060018FC RID: 6396 RVA: 0x000ABAFC File Offset: 0x000A9CFC
        private void tabControl1_Click(object sender, EventArgs e)
        {
            foreach (var obj in mainTabs.TabPages)
            {
                var tabPage = (TabPage) obj;
                foreach (var obj2 in tabPage.Controls)
                {
                    var control = (Control) obj2;
                    if (control is TextBox)
                    {
                        var textBox = (TextBox) control;
                        textBox.Update();
                    }
                }
            }
        }

        // Token: 0x060018FD RID: 6397 RVA: 0x000ABBB4 File Offset: 0x000A9DB4
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        // Token: 0x060018FE RID: 6398 RVA: 0x000ABBC4 File Offset: 0x000A9DC4
        private void updateLabel_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x060018FF RID: 6399 RVA: 0x000ABBC8 File Offset: 0x000A9DC8
        private void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Command.all.Count == 0) return;
            try
            {
                switch (mode.SelectedIndex)
                {
                    case 0:
                        Server.ChangeModeToLava();
                        break;
                    case 1:
                        Server.ChangeModeToLavaFreebuild();
                        break;
                    case 2:
                        Server.ChangeModeToFreebuild();
                        break;
                    case 3:
                        Server.ChangeModeToZombie();
                        break;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001900 RID: 6400 RVA: 0x000ABC3C File Offset: 0x000A9E3C
        private void button1_Click(object sender, EventArgs e)
        {
            if (!lavaSettingsPrevLoaded)
            {
                LavaPropertiesForm = new LavaProperties();
                lavaSettingsPrevLoaded = true;
            }

            LavaPropertiesForm.Show();
        }

        // Token: 0x06001901 RID: 6401 RVA: 0x000ABC64 File Offset: 0x000A9E64
        private void zombieSettings_Click(object sender, EventArgs e)
        {
            if (!zombieSettingsPrevLoaded)
            {
                ZombiePropertiesForm = new ZombieProperties();
                zombieSettingsPrevLoaded = true;
            }

            ZombiePropertiesForm.Show();
        }

        // Token: 0x06001902 RID: 6402 RVA: 0x000ABC8C File Offset: 0x000A9E8C
        private void txtSystem_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001903 RID: 6403 RVA: 0x000ABC90 File Offset: 0x000A9E90
        private void liClients_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001904 RID: 6404 RVA: 0x000ABC94 File Offset: 0x000A9E94
        public void UpdateChat(string msg)
        {
            if (GeneralSettings.All.UseChat)
            {
                if (shuttingDown) return;
                if (chatMainBox.InvokeRequired)
                {
                    BeginInvoke(new Action<string>(UpdateChat), msg);
                    return;
                }

                UpChat(msg);
            }
        }

        // Token: 0x06001905 RID: 6405 RVA: 0x000ABCEC File Offset: 0x000A9EEC
        public void UpChat(string message)
        {
            try
            {
                var list = new List<Coloring>();
                Player.FilterMessageConsole(ref message);
                message = message.Replace('\u0003', '♥').Replace('\u0004', '♦').Replace('\a', '●').Replace('\b', '○')
                    .Replace('\v', '♂').Replace('\f', '♀').Replace('\u0010', '►').Replace('\u0011', '◄')
                    .Replace('\u0013', '‼').Replace('\u000f', '☼').Replace('\u0016', '▄');
                message = "&0" + message + "\r\n";
                for (var num = message.IndexOf('&'); num != -1; num = message.IndexOf('&'))
                {
                    var color = Color.White;
                    var c = message[num + 1];
                    switch (c)
                    {
                        case '0':
                            color = Color.Black;
                            break;
                        case '1':
                            color = Color.FromArgb(0, 0, 170);
                            break;
                        case '2':
                            color = Color.DarkGreen;
                            break;
                        case '3':
                            color = Color.FromArgb(0, 160, 160);
                            break;
                        case '4':
                            color = Color.FromArgb(140, 0, 0);
                            break;
                        case '5':
                            color = Color.Purple;
                            break;
                        case '6':
                            color = Color.FromArgb(255, 170, 0);
                            break;
                        case '7':
                            color = Color.FromArgb(180, 180, 180);
                            break;
                        case '8':
                            color = Color.FromArgb(64, 64, 64);
                            break;
                        case '9':
                            color = Color.FromArgb(115, 115, 255);
                            break;
                        default:
                            switch (c)
                            {
                                case 'a':
                                    color = Color.FromArgb(85, 255, 85);
                                    break;
                                case 'b':
                                    color = Color.FromArgb(85, 255, 255);
                                    break;
                                case 'c':
                                    color = Color.FromArgb(250, 70, 70);
                                    break;
                                case 'd':
                                    color = Color.FromArgb(255, 95, 255);
                                    break;
                                case 'e':
                                    color = Color.FromArgb(255, 255, 85);
                                    break;
                                case 'f':
                                    color = Color.White;
                                    break;
                            }

                            break;
                    }

                    list.Add(new Coloring
                    {
                        index = num + 1,
                        color = color
                    });
                    message = message.Remove(num, 2);
                }

                lock (chatSynchronizationObject)
                {
                    chatMainBox.AppendText(message);
                    if (list.Count > 1)
                    {
                        int i;
                        for (i = 0; i < list.Count - 1; i++)
                        {
                            chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[i].index),
                                chatMainBox.Text.Length - (list[i + 1].index - list[i].index));
                            chatMainBox.SelectionColor = list[i].color;
                        }

                        chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[i].index),
                            chatMainBox.Text.Length - (message.Length - list[i].index));
                        chatMainBox.SelectionColor = list[i].color;
                    }
                    else if (list.Count == 1)
                    {
                        chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[0].index),
                            chatMainBox.Text.Length - (message.Length - list[0].index));
                        chatMainBox.SelectionColor = list[0].color;
                    }

                    chatMainBox.SelectionStart = chatMainBox.Text.Length;
                    chatMainBox.ScrollToCaret();
                    if (chatMainBox.Text.Length > 10000)
                    {
                        var num2 = chatMainBox.Text.IndexOf('\n', chatMainBox.Text.Length - 8000) + 1;
                        if (num2 == -1) num2 = chatMainBox.Text.Length - 8000;
                        chatMainBox.Select(num2, chatMainBox.Text.Length - 1 - num2);
                        var selectedRtf = chatMainBox.SelectedRtf;
                        chatMainBox.Rtf = selectedRtf;
                        chatMainBox.AppendText("\r\n");
                        chatMainBox.SelectionStart = chatMainBox.Text.Length;
                        chatMainBox.ScrollToCaret();
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001906 RID: 6406 RVA: 0x000AC238 File Offset: 0x000AA438
        private void chatTab_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x06001907 RID: 6407 RVA: 0x000AC23C File Offset: 0x000AA43C
        private void chatTab_Focus(object sender, EventArgs e)
        {
            chatInputBox.Focus();
        }

        // Token: 0x06001908 RID: 6408 RVA: 0x000AC24C File Offset: 0x000AA44C
        private void lavaTab_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x06001909 RID: 6409 RVA: 0x000AC250 File Offset: 0x000AA450
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600190A RID: 6410 RVA: 0x000AC254 File Offset: 0x000AA454
        private void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                if (chatInputBox.Text == null || chatInputBox.Text.Trim() == "") return;
                if (!ServerProperties.ValidString(chatInputBox.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
                {
                    chatInputBox.Text = "Invalid character detected.";
                    return;
                }

                if (chatInputBox.Text[0] != '/')
                {
                    if (chatInputBox.Text == null || chatInputBox.Text.Trim() == "") return;
                    var text = chatInputBox.Text.Trim();
                    if (chatInputBox.Text[0] == '#')
                    {
                        var text2 = text.Remove(0, 1).Trim();
                        Player.GlobalMessageOps(string.Concat("To Ops &f-", Server.DefaultColor,
                            Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1), "&f- ", text2));
                        Server.s.Log("(OPs): Console: " + text2);
                        Player.IRCSay("Console: " + text2, true);
                        chatInputBox.Clear();
                        return;
                    }

                    Player.GlobalMessage(Server.ConsoleRealName + chatInputBox.Text);
                    Player.IRCSay(Server.ConsoleRealNameIRC + chatInputBox.Text);
                    WriteLine("<CONSOLE> " + chatInputBox.Text);
                    chatInputBox.Clear();
                }
                else
                {
                    var text3 = "";
                    if (chatInputBox.Text == null || chatInputBox.Text.Trim() == "")
                    {
                        Server.s.CommandUsed("CONSOLE: Whitespace commands are not allowed.");
                        chatInputBox.Clear();
                        return;
                    }

                    if (chatInputBox.Text[0] == '/' && chatInputBox.Text.Length > 1)
                        chatInputBox.Text = chatInputBox.Text.Substring(1);
                    string text4;
                    if (chatInputBox.Text.IndexOf(' ') != -1)
                    {
                        text4 = chatInputBox.Text.Split(' ')[0];
                        text3 = chatInputBox.Text.Substring(chatInputBox.Text.IndexOf(' ') + 1);
                    }
                    else
                    {
                        if (!(chatInputBox.Text != "")) return;
                        text4 = chatInputBox.Text;
                    }

                    try
                    {
                        var command = Command.all.Find(text4);
                        if (command != null)
                        {
                            if (!command.ConsoleAccess)
                            {
                                Server.s.CommandUsed(string.Format("You can't use {0} command from console.", text4));
                                return;
                            }

                            command.Use(null, text3);
                            Server.s.CommandUsed("CONSOLE: USED /" + text4 + " " + text3);
                        }

                        if (command == null)
                            Server.s.CommandUsed("CONSOLE: Command  '/" + text4 + "'  does not exist.");
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        Server.s.CommandUsed("CONSOLE: Failed command.");
                    }

                    chatInputBox.Clear();
                }
            }
        }

        // Token: 0x0600190B RID: 6411 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
        private void playersList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600190C RID: 6412 RVA: 0x000AC5F4 File Offset: 0x000AA7F4
        private void playersList_DataSourceChanged()
        {
            if (!Player.players.Contains((Player) playersGrid.SelectedObject)) playersGrid.SelectedObject = null;
        }

        // Token: 0x0600190D RID: 6413 RVA: 0x000AC620 File Offset: 0x000AA820
        private void mapsList_DataSourceChanged()
        {
            if (!Server.levels.Contains((Level) allMapsGrid.SelectedObject)) allMapsGrid.SelectedObject = null;
        }

        // Token: 0x0600190E RID: 6414 RVA: 0x000AC64C File Offset: 0x000AA84C
        private void mapsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mapsList.SelectedIndex != -1)
                    allMapsGrid.SelectedObject = Level.Find(mapsList.Items[mapsList.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600190F RID: 6415 RVA: 0x000AC6B4 File Offset: 0x000AA8B4
        private void chatTab_Click_1(object sender, EventArgs e)
        {
        }

        // Token: 0x06001910 RID: 6416 RVA: 0x000AC6B8 File Offset: 0x000AA8B8
        private void gBCommands_Enter(object sender, EventArgs e)
        {
        }

        // Token: 0x06001911 RID: 6417 RVA: 0x000AC6BC File Offset: 0x000AA8BC
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001912 RID: 6418 RVA: 0x000AC6C0 File Offset: 0x000AA8C0
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        // Token: 0x06001913 RID: 6419 RVA: 0x000AC6C4 File Offset: 0x000AA8C4
        private void toolStripContainer2_ContentPanel_Load(object sender, EventArgs e)
        {
        }

        // Token: 0x06001914 RID: 6420 RVA: 0x000AC6C8 File Offset: 0x000AA8C8
        private void playersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Token: 0x06001915 RID: 6421 RVA: 0x000AC6CC File Offset: 0x000AA8CC
        private void playersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playersListView.SelectedIndices.Count > 0 && selectedPlayerName !=
                playersListView.Items[playersListView.SelectedIndices[0]].SubItems[0].Text)
            {
                selectedPlayerName = playersListView.Items[playersListView.SelectedIndices[0]].SubItems[0].Text;
                selectedPlayer = Player.Find(selectedPlayerName);
                playersGrid.SelectedObject = selectedPlayer;
            }

            SelectionChangedCommandsUpdate();
        }

        // Token: 0x06001916 RID: 6422 RVA: 0x000AC78C File Offset: 0x000AA98C
        private void mapsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Token: 0x06001917 RID: 6423 RVA: 0x000AC790 File Offset: 0x000AA990
        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            if (kickCheck.Checked)
            {
                Command.all.Find("kick").Use(null, selectedPlayer.name + " " + kickText.Text);
                return;
            }

            Command.all.Find("kick").Use(null, selectedPlayer.name + " &cKicked.");
        }

        // Token: 0x06001918 RID: 6424 RVA: 0x000AC814 File Offset: 0x000AAA14
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            if (!ConfirmationQuestionPopup("              Ban " + selectedPlayer.name)) return;
            if (banCheck.Checked)
            {
                Command.all.Find("ban").Use(null, selectedPlayer.name + " " + banText.Text);
                return;
            }

            Command.all.Find("ban").Use(null, selectedPlayer.name + " &cYou are banned!");
        }

        // Token: 0x06001919 RID: 6425 RVA: 0x000AC8B8 File Offset: 0x000AAAB8
        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            if (!ConfirmationQuestionPopup("            Xban " + selectedPlayer.name)) return;
            if (banCheck.Checked)
            {
                Command.all.Find("xban").Use(null, selectedPlayer.name + " " + xbanText.Text);
                return;
            }

            Command.all.Find("xban").Use(null, selectedPlayer.name + " &cYou are banned!");
        }

        // Token: 0x0600191A RID: 6426 RVA: 0x000AC95C File Offset: 0x000AAB5C
        private void button15_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            if (!ConfirmationQuestionPopup("    Undo all " + selectedPlayer.name + " actions")) return;
            Command.all.Find("undo").Use(null, selectedPlayer.name + " all");
        }

        // Token: 0x0600191B RID: 6427 RVA: 0x000AC9C0 File Offset: 0x000AABC0
        private void kickCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (kickCheck.Checked)
            {
                kickText.ReadOnly = false;
                return;
            }

            kickText.ReadOnly = true;
        }

        // Token: 0x0600191C RID: 6428 RVA: 0x000AC9E8 File Offset: 0x000AABE8
        private void banCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (banCheck.Checked)
            {
                banText.ReadOnly = false;
                return;
            }

            banText.ReadOnly = true;
        }

        // Token: 0x0600191D RID: 6429 RVA: 0x000ACA10 File Offset: 0x000AAC10
        private void xbanCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xbanCheck.Checked)
            {
                xbanText.ReadOnly = false;
                return;
            }

            xbanText.ReadOnly = true;
        }

        // Token: 0x0600191E RID: 6430 RVA: 0x000ACA38 File Offset: 0x000AAC38
        private void kickText_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600191F RID: 6431 RVA: 0x000ACA3C File Offset: 0x000AAC3C
        private void button8_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            Command.all.Find("kill").Use(null, selectedPlayer.name);
        }

        // Token: 0x06001920 RID: 6432 RVA: 0x000ACA68 File Offset: 0x000AAC68
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            Command.all.Find("mute").Use(null, selectedPlayer.name);
        }

        // Token: 0x06001921 RID: 6433 RVA: 0x000ACA94 File Offset: 0x000AAC94
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001922 RID: 6434 RVA: 0x000ACA98 File Offset: 0x000AAC98
        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            Command.all.Find("settitle").Use(null, selectedPlayer.name + " " + titleText.Text);
        }

        // Token: 0x06001923 RID: 6435 RVA: 0x000ACAD8 File Offset: 0x000AACD8
        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            Command.all.Find("setcolor").Use(null,
                selectedPlayer.name + " " + playerColorCombo.Items[playerColorCombo.SelectedIndex]);
        }

        // Token: 0x06001924 RID: 6436 RVA: 0x000ACB38 File Offset: 0x000AAD38
        private void button14_Click(object sender, EventArgs e)
        {
            if (selectedPlayer == null) return;
            Command.all.Find("move").Use(null,
                selectedPlayer.name + " " + targetMapCombo.Items[targetMapCombo.SelectedIndex]);
        }

        // Token: 0x06001925 RID: 6437 RVA: 0x000ACB94 File Offset: 0x000AAD94
        private void SelectionChangedCommandsUpdate()
        {
            if (selectedPlayer != null)
            {
                btnMute.Text = selectedPlayer.muted ? "Unmute" : "Mute";
                var levelNames = new List<string>();
                Server.levels.ForEach(delegate(Level l) { levelNames.Add(l.name); });
                var text = "";
                if (targetMapCombo.SelectedItem != null) text = targetMapCombo.SelectedItem.ToString();
                targetMapCombo.Items.Clear();
                targetMapCombo.Items.AddRange(levelNames.ToArray());
                var num = targetMapCombo.FindString(text);
                if (num != -1) targetMapCombo.SelectedIndex = num;
                playerColorCombo.SelectedIndex = playerColorCombo.FindString(c.Name(selectedPlayer.color));
                if (!titleText.Focused) titleText.Text = selectedPlayer.prefix.Trim().Trim('[', ']');
            }
            else
            {
                titleText.Text = "";
                btnMute.Text = "Mute";
                playerColorCombo.SelectedIndex = 0;
            }
        }

        // Token: 0x06001926 RID: 6438 RVA: 0x000ACCF8 File Offset: 0x000AAEF8
        private void consoleName_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001927 RID: 6439 RVA: 0x000ACCFC File Offset: 0x000AAEFC
        private void button9_Click(object sender, EventArgs e)
        {
            if (createMap == null || createMap.IsDisposed)
            {
                createMap = new CreateMap();
                createMap.Show();
                return;
            }

            createMap.BringToFront();
            createMap.Show();
        }

        // Token: 0x06001928 RID: 6440 RVA: 0x000ACD4C File Offset: 0x000AAF4C
        private void button13_Click(object sender, EventArgs e)
        {
            if (mapsList.SelectedIndex != -1)
                Command.all.Find("unload").Use(null, mapsList.Items[mapsList.SelectedIndex].ToString());
        }

        // Token: 0x06001929 RID: 6441 RVA: 0x000ACD9C File Offset: 0x000AAF9C
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (mapsList.SelectedIndex != -1)
                {
                    var text = mapsList.Items[mapsList.SelectedIndex].ToString();
                    if (ConfirmationQuestionPopup("             Delete " + text))
                        Command.all.Find("deletelvl").Use(null, text);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600192A RID: 6442 RVA: 0x000ACE1C File Offset: 0x000AB01C
        public void RefreshUnloadedMapsList()
        {
            if (unloadedMapsList == null) return;
            if (unloadedMapsList.InvokeRequired)
            {
                RefreshMapList method = DoRefreshUnloadedMapsList;
                Invoke(method);
                return;
            }

            DoRefreshUnloadedMapsList();
        }

        // Token: 0x0600192B RID: 6443 RVA: 0x000ACE5C File Offset: 0x000AB05C
        public void DoRefreshUnloadedMapsList()
        {
            var loadedLevels = new List<string>();
            var list = new List<string>();
            var directoryInfo = new DirectoryInfo("levels/");
            var files = directoryInfo.GetFiles("*.lvl");
            if (Server.levels == null) return;
            Server.levels.ForEach(delegate(Level l) { loadedLevels.Add(l.name.ToLower()); });
            if (files != null)
                foreach (var fileInfo in files)
                    if (!loadedLevels.Contains(fileInfo.Name.Replace(".lvl", "").ToLower()))
                        list.Add(fileInfo.Name.Remove(fileInfo.Name.Length - 4, 4));
            unloadedMapsList.Items.Clear();
            if (list.Count > 0) unloadedMapsList.Items.AddRange(list.ToArray());
        }

        // Token: 0x0600192C RID: 6444 RVA: 0x000ACF50 File Offset: 0x000AB150
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (unloadedMapsList.SelectedIndex != -1)
                {
                    Command.all.Find("load")
                        .Use(null, unloadedMapsList.Items[unloadedMapsList.SelectedIndex].ToString());
                    RefreshUnloadedMapsList();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600192D RID: 6445 RVA: 0x000ACFC4 File Offset: 0x000AB1C4
        private void Color_Click(object sender, EventArgs e)
        {
            var label = (Label) sender;
            if (chatInputBox.Text.Length > 0 &&
                "%&".Contains(chatInputBox.Text[chatInputBox.Text.Length - 1].ToString()))
                chatInputBox.Text = chatInputBox.Text.Remove(chatInputBox.Text.Length - 1);
            if (chatInputBox.Text.Length > 1 &&
                (chatInputBox.Text[chatInputBox.Text.Length - 2] == '%' ||
                 chatInputBox.Text[chatInputBox.Text.Length - 2] == '&') &&
                "abcdef1234567890".Contains(chatInputBox.Text[chatInputBox.Text.Length - 1].ToString()))
                chatInputBox.Text = chatInputBox.Text.Remove(chatInputBox.Text.Length - 2);
            chatInputBox.AppendText(label.Tag.ToString());
        }

        // Token: 0x0600192E RID: 6446 RVA: 0x000AD12C File Offset: 0x000AB32C
        private void label24_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x0600192F RID: 6447 RVA: 0x000AD130 File Offset: 0x000AB330
        private void txtErrors_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001930 RID: 6448 RVA: 0x000AD134 File Offset: 0x000AB334
        private void chatOnOff_btn_Click(object sender, EventArgs e)
        {
            if (GeneralSettings.All.UseChat)
            {
                GeneralSettings.All.UseChat = false;
                chatOnOff_btn.Text = "Deactivated";
                return;
            }

            GeneralSettings.All.UseChat = true;
            chatOnOff_btn.Text = "Activated";
        }

        // Token: 0x06001931 RID: 6449 RVA: 0x000AD184 File Offset: 0x000AB384
        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        // Token: 0x06001932 RID: 6450 RVA: 0x000AD188 File Offset: 0x000AB388
        private void label2_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x06001933 RID: 6451 RVA: 0x000AD18C File Offset: 0x000AB38C
        public void UpdatePlayerListView(List<string[]> listElements)
        {
            if (listViewPlayers.InvokeRequired)
            {
                listViewPlayers.Invoke(UpdatePlayerList, listElements);
                return;
            }

            listViewPlayers.BeginUpdate();
            listViewPlayers.RemoveAllItems();
            listElements.ForEach(delegate(string[] elements)
            {
                if (elements.Length > 0)
                {
                    if (!listViewPlayers.GroupExists(elements[0]))
                    {
                        listViewPlayers.ClearGroups();
                        try
                        {
                            for (var i = Group.groupList.Count - 1; i >= 0; i--)
                                listViewPlayers.AddGroup(Group.groupList[i].trueName);
                        }
                        catch
                        {
                        }
                    }

                    var listViewItem = new ListViewItem(elements[1]);
                    listViewItem.Group = listViewPlayers.GetGroup(elements[0]);
                    if (elements.Length > 2)
                        for (var j = 2; j < elements.Length; j++)
                            listViewItem.SubItems.Add(elements[j]);
                    listViewPlayers.Items.Add(listViewItem);
                }
            });
            listViewPlayers.EndUpdate();
            listViewPlayers.Refresh();
        }

        // Token: 0x06001934 RID: 6452 RVA: 0x000AD20C File Offset: 0x000AB40C
        private void UpdateMapsListView(List<string[]> listElements)
        {
            if (listViewMaps.InvokeRequired)
            {
                listViewMaps.Invoke(UpdateMapsList, listElements);
                return;
            }

            listViewMaps.BeginUpdate();
            listViewMaps.RemoveAllItems();
            listElements.ForEach(delegate(string[] elements)
            {
                if (elements.Length > 0)
                {
                    if (!listViewMaps.GroupExists(elements[0]))
                    {
                        listViewMaps.ClearGroups();
                        listViewMaps.AddGroup("Lava");
                        listViewMaps.AddGroup("Zombie");
                        listViewMaps.AddGroup("Freebuild");
                        listViewMaps.AddGroup("Home");
                        listViewMaps.AddGroup("MyMap");
                    }

                    var listViewItem = new ListViewItem(elements[1]);
                    listViewItem.Group = listViewMaps.GetGroup(elements[0]);
                    if (elements.Length > 2)
                        for (var i = 2; i < elements.Length; i++)
                            listViewItem.SubItems.Add(elements[i]);
                    listViewMaps.Items.Add(listViewItem);
                }
            });
            listViewMaps.EndUpdate();
            listViewMaps.Refresh();
        }

        // Token: 0x06001935 RID: 6453 RVA: 0x000AD28C File Offset: 0x000AB48C
        private void listViewPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001936 RID: 6454 RVA: 0x000AD290 File Offset: 0x000AB490
        private void SaveGuiSettings()
        {
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Minimized)
            {
                GuiSettings.All.WindowWidth = Width;
                GuiSettings.All.WindowHeight = Height;
                GuiSettings.All.MainSplitter3Distance = splitContainer3.SplitterDistance;
                GuiSettings.All.MainSplitter2Distance = splitContainer2.SplitterDistance;
                GuiSettings.All.MainSplitter4Distance = splitContainer4.SplitterDistance;
                GuiSettings.All.MainSplitter5Distance = splitContainer5.SplitterDistance;
            }
            else
            {
                GuiSettings.All.WindowWidth = RestoreBounds.Width;
                GuiSettings.All.WindowHeight = RestoreBounds.Height;
                GuiSettings.All.MainSplitter3Distance =
                    (int) (splitContainer3.SplitterDistance * (double) split3Width / splitContainer3.Width);
                GuiSettings.All.MainSplitter2Distance = (int) (splitContainer2.SplitterDistance *
                    (double) split2Height / splitContainer2.Height);
                GuiSettings.All.MainSplitter4Distance = (int) (splitContainer4.SplitterDistance *
                    (double) split4Height / splitContainer4.Height);
                GuiSettings.All.MainSplitter5Distance =
                    (int) (splitContainer5.SplitterDistance * (double) split5Width / splitContainer5.Width);
            }

            GuiSettings.All.Save();
        }

        // Token: 0x06001937 RID: 6455 RVA: 0x000AD41C File Offset: 0x000AB61C
        private void LoadGuiSettings()
        {
            try
            {
                Width = GuiSettings.All.WindowWidth;
                Height = GuiSettings.All.WindowHeight;
                splitContainer3.SplitterDistance = GuiSettings.All.MainSplitter3Distance;
                splitContainer2.SplitterDistance = GuiSettings.All.MainSplitter2Distance;
                splitContainer4.SplitterDistance = GuiSettings.All.MainSplitter4Distance;
                splitContainer5.SplitterDistance = GuiSettings.All.MainSplitter5Distance;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001938 RID: 6456 RVA: 0x000AD4B8 File Offset: 0x000AB6B8
        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (toolsForm == null || toolsForm.IsDisposed)
                {
                    toolsForm = new Tools();
                    toolsForm.ShowAt(FormCenter);
                }
                else
                {
                    if (!toolsForm.Visible) toolsForm.ShowAt(FormCenter);
                    toolsForm.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001939 RID: 6457 RVA: 0x000AD53C File Offset: 0x000AB73C
        private void mCount_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x0600193A RID: 6458 RVA: 0x000AD540 File Offset: 0x000AB740
        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(chatMainBox.Rtf);
                MessageBox.Show(chatMainBox.SelectedRtf);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600193B RID: 6459 RVA: 0x000AD58C File Offset: 0x000AB78C
        private void chatBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (;;)
                if (Trader.messages.Count <= 0)
                {
                    Thread.Sleep(1);
                }
                else
                {
                    chatMainBox.SuspendLayout();
                    var text = (string) Trader.messages.Dequeue();
                    text = DateTime.Now.ToString("(HH:mm:ss) ") + text;
                    var list = new List<Coloring>();
                    Player.FilterMessageConsole(ref text);
                    text = text.Replace('\u0003', '♥').Replace('\u0004', '♦').Replace('\a', '●').Replace('\b', '○')
                        .Replace('\v', '♂').Replace('\f', '♀').Replace('\u0010', '►').Replace('\u0011', '◄')
                        .Replace('\u0013', '‼').Replace('\u000f', '☼').Replace('\u0016', '▄');
                    text = "&0" + text + "\r\n";
                    try
                    {
                        for (var num = text.IndexOf('&'); num != -1; num = text.IndexOf('&'))
                        {
                            var color = Color.White;
                            var c = text[num + 1];
                            switch (c)
                            {
                                case '0':
                                    color = Color.Black;
                                    break;
                                case '1':
                                    color = Color.FromArgb(0, 0, 170);
                                    break;
                                case '2':
                                    color = Color.DarkGreen;
                                    break;
                                case '3':
                                    color = Color.FromArgb(0, 160, 160);
                                    break;
                                case '4':
                                    color = Color.FromArgb(140, 0, 0);
                                    break;
                                case '5':
                                    color = Color.Purple;
                                    break;
                                case '6':
                                    color = Color.FromArgb(255, 170, 0);
                                    break;
                                case '7':
                                    color = Color.FromArgb(180, 180, 180);
                                    break;
                                case '8':
                                    color = Color.FromArgb(64, 64, 64);
                                    break;
                                case '9':
                                    color = Color.FromArgb(115, 115, 255);
                                    break;
                                default:
                                    switch (c)
                                    {
                                        case 'a':
                                            color = Color.FromArgb(85, 255, 85);
                                            break;
                                        case 'b':
                                            color = Color.FromArgb(85, 255, 255);
                                            break;
                                        case 'c':
                                            color = Color.FromArgb(250, 70, 70);
                                            break;
                                        case 'd':
                                            color = Color.FromArgb(255, 95, 255);
                                            break;
                                        case 'e':
                                            color = Color.FromArgb(255, 255, 85);
                                            break;
                                        case 'f':
                                            color = Color.White;
                                            break;
                                    }

                                    break;
                            }

                            list.Add(new Coloring
                            {
                                index = num + 1,
                                color = color
                            });
                            text = text.Remove(num, 2);
                        }

                        chatMainBox.AppendText(text);
                        if (list.Count > 1)
                        {
                            int i;
                            for (i = 0; i < list.Count - 1; i++)
                            {
                                chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[i].index),
                                    chatMainBox.Text.Length - (list[i + 1].index - list[i].index));
                                chatMainBox.SelectionColor = list[i].color;
                            }

                            chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[i].index),
                                chatMainBox.Text.Length - (text.Length - list[i].index));
                            chatMainBox.SelectionColor = list[i].color;
                        }
                        else if (list.Count == 1)
                        {
                            chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[0].index),
                                chatMainBox.Text.Length - (text.Length - list[0].index));
                            chatMainBox.SelectionColor = list[0].color;
                        }

                        chatMainBox.SelectionStart = chatMainBox.Text.Length;
                        chatMainBox.ScrollToCaret();
                        if (chatMainBox.Text.Length > 10000)
                        {
                            var num2 = chatMainBox.Text.IndexOf('\n', chatMainBox.Text.Length - 8000) + 1;
                            if (num2 == -1) num2 = chatMainBox.Text.Length - 8000;
                            chatMainBox.Select(num2, chatMainBox.Text.Length - 1 - num2);
                            var selectedRtf = chatMainBox.SelectedRtf;
                            chatMainBox.Rtf = selectedRtf;
                            chatMainBox.AppendText("\r\n");
                            chatMainBox.SelectionStart = chatMainBox.Text.Length;
                            chatMainBox.ScrollToCaret();
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }

                    chatMainBox.ResumeLayout();
                    Thread.Sleep(1);
                }
        }

        // Token: 0x0600193C RID: 6460 RVA: 0x000ADB0C File Offset: 0x000ABD0C
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        // Token: 0x06001947 RID: 6471 RVA: 0x000ADDB8 File Offset: 0x000ABFB8
        public void SetFont(Font f)
        {
            if (chatMainBox.InvokeRequired)
            {
                chatMainBox.Invoke(new Action(delegate { SetFont(f); }));
                return;
            }

            chatMainBox.SelectAll();
            chatMainBox.SelectionFont = f;
            var selectedRtf = chatMainBox.SelectedRtf;
            chatMainBox.Font = f;
            chatMainBox.Rtf = selectedRtf;
            chatMainBox.AppendText(Environment.NewLine);
        }

        // Token: 0x06001948 RID: 6472 RVA: 0x000ADE5C File Offset: 0x000AC05C
        public Font GetFont()
        {
            return (Font) chatMainBox.Invoke(new Func<Font>(() => chatMainBox.Font));
        }

        // Token: 0x06001949 RID: 6473 RVA: 0x000ADE7C File Offset: 0x000AC07C
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorChatSettings == null || colorChatSettings.IsDisposed)
            {
                colorChatSettings = new ColorChatSettings();
                colorChatSettings.ShowAt(FormCenter);
                return;
            }

            colorChatSettings.BringToFront();
        }

        // Token: 0x0600194B RID: 6475 RVA: 0x000ADF14 File Offset: 0x000AC114
        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        // Token: 0x0600194C RID: 6476 RVA: 0x000ADF18 File Offset: 0x000AC118
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                var availablePlugin = Server.Plugins.AvailablePlugins.Find(treeView1.SelectedNode.Text);
                if (availablePlugin != null)
                {
                    lblPluginName.Text = availablePlugin.Instance.Name;
                    lblPluginVersion.Text = "(" + availablePlugin.Instance.Version + ")";
                    lblPluginAuthor.Text = "By: " + availablePlugin.Instance.Author;
                    lblPluginDesc.Text = availablePlugin.Instance.Description;
                    pnlPlugin.Controls.Clear();
                    availablePlugin.Instance.MainInterface.Dock = DockStyle.Fill;
                    pnlPlugin.Controls.Add(availablePlugin.Instance.MainInterface);
                }
            }
        }

        // Token: 0x0600194D RID: 6477 RVA: 0x000AE010 File Offset: 0x000AC210
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 5 && (int) m.WParam == 1 && Minimize != null) Window_Minimize(this, EventArgs.Empty);
            if (m.Msg == 274 && !(m.WParam == new IntPtr(61472)) && m.WParam == new IntPtr(61488))
            {
                split2Height = splitContainer2.Height;
                split4Height = splitContainer4.Height;
                split5Width = splitContainer5.Width;
                split3Width = splitContainer3.Width;
            }

            base.WndProc(ref m);
        }

        // Token: 0x06001950 RID: 6480 RVA: 0x000B56E8 File Offset: 0x000B38E8
        private void mapsStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listViewMaps.SelectedIndices.Count <= 0) e.Cancel = true;
        }

        // Token: 0x06001951 RID: 6481 RVA: 0x000B5704 File Offset: 0x000B3904
        private void playerStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listViewPlayers.SelectedIndices.Count <= 0) e.Cancel = true;
        }

        // Token: 0x02000360 RID: 864
        public struct SCROLLBARINFO
        {
            // Token: 0x04000DCF RID: 3535
            public int cbSize;

            // Token: 0x04000DD0 RID: 3536
            public RECT rcScrollBar;

            // Token: 0x04000DD1 RID: 3537
            public int dxyLineButton;

            // Token: 0x04000DD2 RID: 3538
            public int xyThumbTop;

            // Token: 0x04000DD3 RID: 3539
            public int xyThumbBottom;

            // Token: 0x04000DD4 RID: 3540
            public int reserved;

            // Token: 0x04000DD5 RID: 3541
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] rgstate;
        }

        // Token: 0x02000361 RID: 865
        public struct RECT
        {
            // Token: 0x04000DD6 RID: 3542
            public int Left;

            // Token: 0x04000DD7 RID: 3543
            public int Top;

            // Token: 0x04000DD8 RID: 3544
            public int Right;

            // Token: 0x04000DD9 RID: 3545
            public int Bottom;
        }

        // Token: 0x02000362 RID: 866
        // (Invoke) Token: 0x0600195D RID: 6493
        private delegate void StringCallback(string s);

        // Token: 0x02000363 RID: 867
        // (Invoke) Token: 0x06001961 RID: 6497
        private delegate void PlayerListCallback();

        // Token: 0x02000364 RID: 868
        // (Invoke) Token: 0x06001965 RID: 6501
        private delegate void ReportCallback(Report r);

        // Token: 0x02000365 RID: 869
        // (Invoke) Token: 0x06001969 RID: 6505
        private delegate void VoidDelegate();

        // Token: 0x02000366 RID: 870
        // (Invoke) Token: 0x0600196D RID: 6509
        private delegate void RefreshMapList();

        // Token: 0x02000367 RID: 871
        // (Invoke) Token: 0x06001971 RID: 6513
        private delegate void LogDelegate(string message);

        // Token: 0x02000368 RID: 872
        // (Invoke) Token: 0x06001975 RID: 6517
        private delegate void ChatDelegate(string message);

        // Token: 0x02000369 RID: 873
        // (Invoke) Token: 0x06001979 RID: 6521
        private delegate void SystemDelegate(string message);

        // Token: 0x0200036A RID: 874
        // (Invoke) Token: 0x0600197D RID: 6525
        private delegate void ErrorDelegate(string message);

        // Token: 0x0200036B RID: 875
        public class Coloring
        {
            // Token: 0x04000DDB RID: 3547
            public Color color;

            // Token: 0x04000DDA RID: 3546
            public int index;
        }

        // Token: 0x0200036C RID: 876
        // (Invoke) Token: 0x06001982 RID: 6530
        private delegate void UpdateListViewDelegate(List<string[]> listElements);
    }
}