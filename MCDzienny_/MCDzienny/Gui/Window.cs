using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using MCDzienny_.Gui;
using MCDzienny.GUI.RemoteAccess;
using MCDzienny.InfectionSystem;
using MCDzienny.Misc;
using MCDzienny.Plugins;
using MCDzienny.Properties;
using MCDzienny.RemoteAccess;
using MCDzienny.Settings;
using Message = System.Windows.Forms.Message;
using Timer = System.Windows.Forms.Timer;

namespace MCDzienny.Gui
{
    public class Window : Form
    {

        const uint OBJID_VSCROLL = 4294967291u;

        const int EM_LINESCROLL = 182;

        static readonly object chatSynchronizationObject = new object();

        internal static Server s;

        public static Window thisWindow;

        public static bool prevLoaded;

        public static bool lavaSettingsPrevLoaded;

        public static bool zombieSettingsPrevLoaded;

        public static volatile bool showWarning = true;

        readonly SplashScreen2 splashScreen2;

        readonly UpdateListViewDelegate UpdateMapsList;

        readonly UpdateListViewDelegate UpdatePlayerList;

        CustomListView accountsList;

        PropertyGrid allMapsGrid;

        ToolStripMenuItem animalAIToolStripMenuItem;

        CheckBox banCheck;

        TextBox banText;

        ToolStripMenuItem banToolStripMenuItem;

        ToolStripPanel BottomToolStripPanel;

        Button btnCreateMap;

        Button btnMute;

        Button btnProperties;

        Button button10;

        Button button11;

        Button button12;

        Button button13;

        Button button14;

        Button button15;

        Button button18;

        Button button2;

        Button button3;

        Button button4;

        Button button5;

        Button button6;

        Button button7;

        Button button8;

        Button button9;

        Label cAqua;

        Label cBlack;

        Label cBlue;

        Label cBrightGreen;

        Label cDarkBlue;

        Label cDarkGray;

        Label cDarkGreen;

        Label cDarkRed;

        Label cGold;

        Label cGray;

        Button changeAccountBtn;

        TabPage changelogTab;

        TextBox chatInputBox;

        RichTextBox chatMainBox;

        Button chatOnOff_btn;

        ListBox chatPlayerList;

        Queue chatQueue = Queue.Synchronized(new Queue(200));

        TabPage chatTab;

        Label chatWarningLabel;

        CheckBox checkBox1;

        CheckBox checkBox2;

        ColorChatSettings colorChatSettings;

        IContainer components;

        ToolStripContentPanel ContentPanel;

        Label cPink;

        Label cPurple;

        CreateMap createMap;

        Label cRed;

        Label cTeal;

        Label cWhite;

        Label cYellow;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

        DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

        ToolStripMenuItem edgeWaterToolStripMenuItem;

        TabPage errorsTab;

        ToolStripMenuItem finiteModeToolStripMenuItem;

        FontDialog fontDialog1;

        GroupBox gBChat;

        GroupBox gBCommands;

        GroupBox groupBox1;

        GroupBox groupBox2;

        GroupBox groupBox3;

        GroupBox groupBox4;

        ToolStripMenuItem growingGrassToolStripMenuItem;

        ToolStripMenuItem hideConsole;

        ContextMenuStrip iconContext;

        DataGridViewEnumerated infectionMapsGrid;

        CheckBox kickCheck;

        TextBox kickText;

        ToolStripMenuItem kickToolStripMenuItem;

        ToolStripMenuItem killerBlocksToolStripMenuItem;

        Label label1;

        Label label10;

        Label label11;

        Label label12;

        Label label17;

        Label label2;

        Label label20;

        Label label22;

        Label label25;

        Label label3;

        Label label4;

        Label label5;

        Label label6;

        Label label7;

        Label label8;

        Label label9;

        Form LavaPropertiesForm;

        TabPage lavaTab;

        Label lblPluginAuthor;

        Label lblPluginDesc;

        Label lblPluginName;

        Label lblPluginVersion;

        ToolStripPanel LeftToolStripPanel;

        LinkLabel linkLabel1;

        CustomListView listViewMaps;

        CustomListView listViewPlayers;

        public volatile bool loaded;

        TabPage mainTab;

        TabControl mainTabs;

        ColumnHeader mapColumnName;

        ColumnHeader mapColumnPhysics;

        ColumnHeader mapColumnPlayers;

        ColumnHeader mapColumnWeight;

        DataGridViewTextBoxColumn mapName;

        DataGridViewEnumerated mapsGrid;

        ListBox mapsList;

        ContextMenuStrip mapsStrip;

        TabPage mapsTab;

        Label mCount;

        Button minimizeButton;

        ComboBox mode;

        Button newAccountBtn;

        public NotifyIcon notifyIcon1 = new NotifyIcon();

        ToolStripMenuItem openConsole;

        Label pCount;

        public volatile int pendingPacketsAvg;

        public volatile int pendingPacketsSum;

        DataGridViewTextBoxColumn phase1;

        DataGridViewTextBoxColumn phase2;

        ToolStripMenuItem physicsToolStripMenuItem;

        PictureBox pictureBox1;

        ComboBox playerColorCombo;

        ColumnHeader PlayersColumnAfk;

        ColumnHeader PlayersColumnMap;

        ColumnHeader PlayersColumnName;

        PropertyGrid playersGrid;

        ListView playersListView;

        TabPage playersTab;

        ContextMenuStrip playerStrip;

        ColumnHeader PlMap;

        ColumnHeader PlName;

        ColumnHeader PlRank;

        Panel pnlPlugin;

        PropertyWindow PropertyForm;

        Regex regex = new Regex("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

        ColumnHeader remoteAccount;

        Button removeAccountBtn;

        ToolStripPanel RightToolStripPanel;

        ToolStripMenuItem rPChatToolStripMenuItem;

        ToolStripMenuItem saveToolStripMenuItem;

        Player selectedPlayer;

        string selectedPlayerName = "";

        ToolStripMenuItem settingsToolStripMenuItem;

        ToolStripMenuItem shutdownServer;

        bool shuttingDown;

        DataGridViewTextBoxColumn sourceX;

        DataGridViewTextBoxColumn sourceY;

        DataGridViewTextBoxColumn sourceZ;

        int split2Height;

        int split3Width;

        int split4Height;

        int split5Width;

        SplitContainer splitContainer2;

        SplitContainer splitContainer3;

        SplitContainer splitContainer4;

        SplitContainer splitContainer5;

        StatusStrip statusStrip1;

        ToolStripMenuItem survivalDeathToolStripMenuItem;

        TabPage systemTab;

        TabControl tabControl1;

        TabPage tabPage1;

        TabPage tabPage2;

        TabPage tabPagePlugins;

        ComboBox targetMapCombo;

        TextBox textBox1;

        TextBox textBox4;

        TextBox titleText;

        Timer tmrRestart;

        Tools toolsForm;

        ToolStripContainer toolStripContainer1;

        ToolStripMenuItem toolStripMenuItem2;

        ToolStripMenuItem toolStripMenuItem3;

        ToolStripMenuItem toolStripMenuItem4;

        ToolStripMenuItem toolStripMenuItem5;

        ToolStripMenuItem toolStripMenuItem6;

        internal ToolStripStatusLabel toolStripStatusLabelLagometer;

        internal ToolStripStatusLabel toolStripStatusLabelRoundTime;

        ToolStripStatusLabel toolStripStatusLabelUptime;

        ToolTip toolTip1;

        ToolStripPanel TopToolStripPanel;

        TreeView treeView1;

        TextBox txtChangelog;

        TextBox txtCommands;

        TextBox txtCommandsUsed;

        TextBox txtErrors;

        TextBox txtInput;

        TextBox txtLog;

        TextBox txtSystem;

        TextBox txtUrl;

        DataGridViewTextBoxColumn typeOfLava;

        ListBox unloadedMapsList;

        ToolStripMenuItem unloadToolStripMenuItem;

        Form UpdateForm;

        ToolStripMenuItem voiceToolStripMenuItem;

        ToolStripMenuItem whoisToolStripMenuItem;

        int x;

        CheckBox xbanCheck;

        TextBox xbanText;

        Form ZombiePropertiesForm;

        Button zombieSettings;

        TabPage zombieSurvivalTab;

        public Window(FormWindowState startState = 0)
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_0041: Unknown result type (might be due to invalid IL or missing references)
            //IL_0043: Invalid comparison between Unknown and I4
            if ((int)startState == 1)
            {
                StartMinimized = true;
                WindowState = (FormWindowState)1;
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
            if (!showWarning)
            {
                chatWarningLabel.Visible = false;
            }
        }

        public bool StartMinimized { get; set; }

        Point FormCenter
        {
            get
            {
                int num = Location.X + Size.Width / 2;
                int y = Location.Y + Size.Height / 2;
                return new Point(num, y);
            }
        }

        public static event EventHandler Minimize;

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetScrollBarInfo(IntPtr hWnd, uint idObject, ref SCROLLBARINFO psbi);

        void InitializeStatus()
        {
            System.Timers.Timer timer = new System.Timers.Timer(50000.0);
            timer.Elapsed += delegate
            {
                string timeFormat = "";
                TimeSpan timeSpan = DateTime.Now.Subtract(Server.TimeOnline);
                if (timeSpan.Days > 0)
                {
                    timeFormat = timeSpan.Days + "d " + timeSpan.Hours + "h " + timeSpan.Minutes + "min";
                }
                else if (timeSpan.Hours > 0)
                {
                    timeFormat = timeSpan.Hours + "h " + timeSpan.Minutes + "min";
                }
                else
                {
                    timeFormat = timeSpan.Minutes + "min";
                }
                thisWindow.toolStripStatusLabelUptime.GetCurrentParent().BeginInvoke((Action)delegate { toolStripStatusLabelUptime.Text = "Uptime : " + timeFormat; });
                RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.SendUptime(timeSpan); });
            };
            timer.Start();
            toolStripStatusLabelRoundTime.Visible = Server.mode == Mode.Lava || Server.mode == Mode.LavaFreebuild ? true : false;
        }

        void Window_Minimize(object sender, EventArgs e) {}

        protected override void OnShown(EventArgs e) {}

        void Window_Load(object sender, EventArgs e)
        {
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            //IL_0031: Expected O, but got Unknown
            //IL_00cb: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d1: Expected O, but got Unknown
            //IL_01d0: Unknown result type (might be due to invalid IL or missing references)
            //IL_01da: Expected O, but got Unknown
            //IL_0328: Unknown result type (might be due to invalid IL or missing references)
            //IL_0332: Expected O, but got Unknown
            //IL_051e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0525: Expected O, but got Unknown
            Hide();
            thisWindow = this;
            Text = "<server name here>";
            Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("MCDzienny.mcdzienny.ico"));
            if (!StartMinimized)
            {
                WindowState = 0;
            }
            else
            {
                Hide();
            }
            LoadGuiSettings();
            s = new Server();
            s.OnLog += WriteLine;
            s.OnCommand += newCommand;
            s.OnError += newError;
            s.OnSystem += newSystem;
            foreach (TabPage tabPage in mainTabs.TabPages)
            {
                TabPage val = tabPage;
                mainTabs.SelectTab(val);
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
            System.Timers.Timer timer = new System.Timers.Timer(10000.0);
            timer.Elapsed += delegate
            {
                if (!Server.shuttingDown)
                {
                    UpdateMapList("'");
                    FillMainMapListView();
                }
            };
            timer.Start();
            System.Timers.Timer timer2 = new System.Timers.Timer(60000.0);
            timer2.Elapsed += delegate
            {
                if (!Server.shuttingDown)
                {
                    UpdateClientList();
                }
            };
            timer2.Start();
            if (File.Exists(Logger.ErrorLogPath))
            {
                string[] array = File.ReadAllLines(Logger.ErrorLogPath);
                if (array.Length > 200)
                {
                    string[] array2 = new string[200];
                    Array.Copy(array, array.Length - 200, array2, 0, 200);
                    txtErrors.Lines = array2;
                }
                else
                {
                    txtErrors.Lines = array;
                }
            }
            if (File.Exists("extra/Changelog.txt"))
            {
                string[] array3 = File.ReadAllLines("extra/Changelog.txt");
                foreach (string text in array3)
                {
                    txtChangelog.AppendText("\r\n           " + text);
                }
            }
            FontFamily val2 = null;
            FontFamily[] families = FontFamily.Families;
            foreach (FontFamily val3 in families)
            {
                if (string.Equals(val3.Name, GeneralSettings.All.ChatFontFamily))
                {
                    val2 = val3;
                    break;
                }
            }
            if (val2 != null)
            {
                chatMainBox.Font = new Font(val2, GeneralSettings.All.ChatFontSize);
            }
            object[] array4 = new object[16]
            {
                "black", "navy", "green", "teal", "maroon", "purple", "gold", "silver", "gray", "blue", "lime", "aqua", "red", "pink", "yellow", "white"
            };
            playerColorCombo.Items.AddRange(array4);
            RefreshUnloadedMapsList();
            string path = Application.StartupPath + "\\Plugins";
            DirectoryUtil.CreateIfNotExists(path);
            AddPlugin instance = new AddPlugin();
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
            foreach (AvailablePlugin availablePlugin in Server.Plugins.AvailablePlugins)
            {
                TreeNode val4 = new TreeNode(availablePlugin.Instance.Name);
                treeView1.Nodes.Add(val4);
            }
            if (splashScreen2 != null)
            {
                splashScreen2.Hide();
            }
            Show();
        }

        public void RemoveNodeFromPluginList(string name)
        {
            //IL_0054: Unknown result type (might be due to invalid IL or missing references)
            //IL_005a: Expected O, but got Unknown
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { RemoveNodeFromPluginList(name); });
                return;
            }
            TreeNode val = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                TreeNode val2 = node;
                if (val2.Text == name)
                {
                    val = val2;
                    break;
                }
            }
            if (val != null)
            {
                treeView1.Nodes.Remove(val);
            }
        }

        public void AddNodeToPluginList(TreeNode node)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { AddNodeToPluginList(node); });
            }
            else
            {
                treeView1.Nodes.Add(node);
            }
        }

        public void UpdateMainMapListView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateMainMapListView));
            }
            else
            {
                FillMainMapListView();
            }
        }

        void FillMainMapListView()
        {
            var listItems = new List<string[]>();
            Server.levels.ForEach(delegate(Level l)
            {
                string[] array = new string[5];
                if (l.mapType == MapType.Freebuild)
                {
                    array[0] = "Freebuild";
                }
                else if (l.mapType == MapType.Lava)
                {
                    array[0] = "Lava";
                }
                else if (l.mapType == MapType.Zombie)
                {
                    array[0] = "Zombie";
                }
                else if (l.mapType == MapType.Home)
                {
                    array[0] = "Home";
                }
                else
                {
                    array[0] = "MyMap";
                }
                array[1] = l.name;
                array[2] = Level.GetPhysicsNameByNumber(l.physics).ToLower();
                array[3] = l.PlayersCount.ToString();
                array[4] = l.Weight == 0 ? "" : (l.Weight / 1024f).ToString("##,#.##") + "KB";
                listItems.Add(array);
            });
            UpdateMapsListView(listItems);
        }

        void SettingsUpdate()
        {
            if (shuttingDown)
            {
                return;
            }
            if (txtLog.InvokeRequired)
            {
                VoidDelegate voidDelegate = SettingsUpdate;
                Invoke(voidDelegate);
                return;
            }
            Text = Server.name + " MCDzienny Version: " + Server.Version;
            chatOnOff_btn.Text = GeneralSettings.All.UseChat ? "Activated" : "Deactivated";
            if (Server.mode == Mode.Freebuild || Server.mode == Mode.Zombie)
            {
                toolStripStatusLabelRoundTime.Visible = false;
            }
            remoteAccounts_ElementChanged(null, EventArgs.Empty);
            checkBox1.Checked = RemoteSettings.All.AllowRemoteAccess;
            checkBox2.Checked = RemoteSettings.All.ShowInBrowser;
            textBox1.Text = RemoteSettings.All.Port.ToString();
            Server.remoteAccounts.ElementChanged -= remoteAccounts_ElementChanged;
            Server.remoteAccounts.ElementChanged += remoteAccounts_ElementChanged;
        }

        void remoteAccounts_ElementChanged(object sender, EventArgs e)
        {
            try
            {
                var list = new List<string>();
                foreach (string key in Server.remoteAccounts.Accounts.Keys)
                {
                    list.Add(key);
                }
                UpdateAccountsList(list.ToArray());
            }
            catch {}
        }

        void HeartBeatFail()
        {
            WriteLine("Recent Heartbeat Failed");
        }

        void newError(string message)
        {
            try
            {
                if (txtErrors.InvokeRequired)
                {
                    ErrorDelegate errorDelegate = newError;
                    Invoke(errorDelegate, message);
                    return;
                }
                txtErrors.AppendText(Environment.NewLine + message);
                if (txtErrors.Text.Length > 3000)
                {
                    int num = txtLog.Text.IndexOf('\n', txtErrors.Text.Length - 2300) + 1;
                    if (num == -1)
                    {
                        num = txtErrors.Text.Length - 2300;
                    }
                    txtErrors.Text = txtErrors.Text.Substring(num);
                    txtErrors.SelectionStart = txtErrors.TextLength;
                    txtErrors.ScrollToCaret();
                    txtErrors.Refresh();
                }
            }
            catch {}
        }

        void newSystem(string message)
        {
            try
            {
                if (txtSystem.InvokeRequired)
                {
                    SystemDelegate systemDelegate = newSystem;
                    Invoke(systemDelegate, message);
                    return;
                }
                txtSystem.AppendText(Environment.NewLine + message);
                if (txtSystem.Text.Length > 3000)
                {
                    int num = txtSystem.Text.IndexOf('\n', txtSystem.Text.Length - 2300) + 1;
                    if (num == -1)
                    {
                        num = txtSystem.Text.Length - 2300;
                    }
                    txtSystem.Text = txtSystem.Text.Substring(num);
                    txtSystem.SelectionStart = txtSystem.TextLength;
                    txtSystem.ScrollToCaret();
                    txtSystem.Refresh();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void Write(string s)
        {
            if (!shuttingDown)
            {
                if (txtLog.InvokeRequired)
                {
                    LogDelegate logDelegate = Write;
                    Invoke(logDelegate, s);
                }
                else
                {
                    txtLog.AppendText(s);
                }
            }
        }

        public void WriteLine(string s)
        {
            if (shuttingDown)
            {
                return;
            }
            if (InvokeRequired)
            {
                LogDelegate logDelegate = WriteLine;
                Invoke(logDelegate, s);
                return;
            }
            SCROLLBARINFO psbi = default(SCROLLBARINFO);
            psbi.cbSize = Marshal.SizeOf(psbi);
            GetScrollBarInfo(txtLog.Handle, 4294967291u, ref psbi);
            if (psbi.xyThumbBottom > psbi.rcScrollBar.Bottom - psbi.rcScrollBar.Top - 2 * psbi.dxyLineButton)
            {
                txtLog.AppendText("\r\n" + s);
            }
            else
            {
                int scrollPos = GetScrollPos(txtLog.Handle, 1);
                TextBox obj = txtLog;
                obj.Text = obj.Text + "\r\n" + s;
                SetScrollPos(txtLog.Handle, 1, scrollPos, bRedraw: true);
                SendMessage(txtLog.Handle, 182, 1, scrollPos);
            }
            if (txtLog.Text.Length > 20000)
            {
                int num = txtLog.Text.IndexOf('\n', txtLog.Text.Length - 14000) + 1;
                if (num == -1)
                {
                    num = txtLog.Text.Length - 14000;
                }
                txtLog.Text = txtLog.Text.Substring(num);
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
                txtLog.Refresh();
            }
        }

        public void UpdateClientList()
        {
            if (InvokeRequired)
            {
                PlayerListCallback playerListCallback = UpdateClientList;
                Invoke(playerListCallback);
                return;
            }
            try
            {
                chatPlayerList.Items.Clear();
                playersListView.Items.Clear();
                Player.players.ForEach(delegate(Player p)
                {
                    //IL_00aa: Unknown result type (might be due to invalid IL or missing references)
                    //IL_00b0: Expected O, but got Unknown
                    if (p == null)
                    {
                        Server.s.Log("Error gui: p == null");
                    }
                    else if (p.group == null)
                    {
                        Server.s.Log("Error gui: p.group == null");
                    }
                    else if (p.name == null)
                    {
                        Server.s.Log("Error gui: p.name == null");
                    }
                    else if (p.level == null)
                    {
                        Server.s.Log("Error gui: p.level == null");
                    }
                    else if (p.level.name == null)
                    {
                        Server.s.Log("Error gui: p.level.name == null");
                    }
                    else
                    {
                        ListViewItem val = new ListViewItem(new string[3]
                        {
                            p.name, p.group.name, p.level.name
                        });
                        val.Name = p.name;
                        playersListView.Items.Add(val);
                        chatPlayerList.Items.Add(p.name);
                    }
                });
                ListViewItem[] array = playersListView.Items.Find(selectedPlayerName, false);
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

        public void UpdateMapList(string _)
        {
            if (InvokeRequired)
            {
                LogDelegate logDelegate = UpdateMapList;
                Invoke(logDelegate, _);
                return;
            }
            mapsList.Items.Clear();
            int pcount = 0;
            foreach (Level level in Server.levels)
            {
                pcount = 0;
                PlayerCollection players = Player.players;
                Action<Player> action = delegate(Player who)
                {
                    if (who.level == level)
                    {
                        pcount++;
                    }
                };
                players.ForEach(action);
                mapsList.Items.Add(level.name);
                mapsList_DataSourceChanged();
                mCount.Text = Server.levels.Count.ToString();
            }
        }

        public void UpdateProperties()
        {
            mode.SelectedIndex = (int)(Server.mode != Mode.Zombie ? Server.mode : Mode.ZombieFreebuild);
        }

        public void UpdateLavaMaps()
        {
            if (mapsGrid.Rows.Count < LavaSystem.lavaMaps.Count)
            {
                mapsGrid.Rows.Add(LavaSystem.lavaMaps.Count - mapsGrid.Rows.Count);
            }
            for (int i = 0; i < LavaSystem.lavaMaps.Count; i++)
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

        public void UpdateInfectionMaps()
        {
            if (infectionMapsGrid.Rows.Count < InfectionMaps.infectionMaps.Count)
            {
                mapsGrid.Rows.Add(InfectionMaps.infectionMaps.Count - infectionMapsGrid.Rows.Count);
            }
            for (int i = 0; i < InfectionMaps.infectionMaps.Count; i++)
            {
                infectionMapsGrid.Rows[i].Cells[0].Value = InfectionMaps.infectionMaps[i].Name;
                infectionMapsGrid.Rows[i].Cells[4].Value = InfectionMaps.infectionMaps[i].CountdownSeconds;
                infectionMapsGrid.Rows[i].Cells[5].Value = InfectionMaps.infectionMaps[i].RoundTimeMinutes;
            }
        }

        public void UpdatePackets(int packets) {}

        public void UpdateUrl(string s)
        {
            if (InvokeRequired)
            {
                StringCallback stringCallback = UpdateUrl;
                Invoke(stringCallback, s);
            }
            else
            {
                txtUrl.Text = s;
            }
        }

        void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IL_003d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0042: Invalid comparison between I4 and Unknown
            if (shuttingDown)
            {
                return;
            }
            GeneralSettings.All.Save();
            SaveGuiSettings();
            ServerProperties.Save();
            Server.Plugins.ClosePlugins();
            if (!Server.restarting && 6 != (int)MessageBox.Show("Shutdown the server?", "Shutdown", (MessageBoxButtons)4, 0, 0))
            {
                e.Cancel = true;
                return;
            }
            if (notifyIcon1 != null)
            {
                notifyIcon1.Visible = false;
            }
            shuttingDown = true;
            Program.ExitProgram(AutoRestart: false);
        }

        bool ConfirmationQuestionPopup(string actionName)
        {
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Invalid comparison between I4 and Unknown
            if (6 == (int)MessageBox.Show(actionName + " ?", "Confirmation", (MessageBoxButtons)4, 0, 0))
            {
                return true;
            }
            return false;
        }

        void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0008: Invalid comparison between Unknown and I4
            if ((int)e.KeyCode != 13)
            {
                return;
            }
            e.SuppressKeyPress = true;
            e.Handled = true;
            if (txtInput.Text == null || txtInput.Text.Trim() == "")
            {
                return;
            }
            if (!ServerProperties.ValidString(txtInput.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
            {
                txtInput.Text = "Invalid character detected.";
                return;
            }
            string text = txtInput.Text.Trim();
            string text2 = text;
            if (txtInput.Text[0] == '#')
            {
                text2 = text.Remove(0, 1).Trim();
                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 2) + "&f- " + text2);
                Server.s.Log("(OPs): Console: " + text2);
                Player.IRCSay("Console: " + text2, opchat: true);
                txtInput.Clear();
            }
            else
            {
                Player.GlobalMessage(Server.ConsoleRealName + txtInput.Text);
                Player.IRCSay(Server.ConsoleRealNameIRC + txtInput.Text);
                Server.s.Log("<CONSOLE> " + txtInput.Text);
                txtInput.Clear();
            }
        }

        void txtCommands_KeyDown(object sender, KeyEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0008: Invalid comparison between Unknown and I4
            if ((int)e.KeyCode != 13)
            {
                return;
            }
            e.SuppressKeyPress = true;
            e.Handled = true;
            string text = "";
            string sentMsg = "";
            if (txtCommands.Text == null || txtCommands.Text.Trim() == "")
            {
                Server.s.CommandUsed("CONSOLE: Whitespace commands are not allowed.");
                txtCommands.Clear();
                return;
            }
            if (txtCommands.Text[0] == '/' && txtCommands.Text.Length > 1)
            {
                txtCommands.Text = txtCommands.Text.Substring(1);
            }
            if (txtCommands.Text.IndexOf(' ') != -1)
            {
                text = txtCommands.Text.Split(' ')[0];
                sentMsg = txtCommands.Text.Substring(txtCommands.Text.IndexOf(' ') + 1);
            }
            else
            {
                if (!(txtCommands.Text != ""))
                {
                    return;
                }
                text = txtCommands.Text;
            }
            try
            {
                Command found = Command.all.Find(text);
                if (found != null)
                {
                    if (!found.ConsoleAccess)
                    {
                        Server.s.CommandUsed(string.Format("You can't use {0} command from console.", text));
                        txtCommands.Text = "";
                        return;
                    }
                    new Thread((ThreadStart)delegate
                    {
                        try
                        {
                            found.Use(null, sentMsg);
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    }).Start();
                    if (found.HighSecurity)
                    {
                        Server.s.CommandUsed("CONSOLE: USED /" + text + " ***");
                    }
                    else
                    {
                        Server.s.CommandUsed("CONSOLE: USED /" + text + " " + sentMsg);
                    }
                }
                if (found == null)
                {
                    Server.s.CommandUsed("CONSOLE: Command  '/" + text + "'  does not exist.");
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                Server.s.CommandUsed("CONSOLE: Failed command.");
            }
            txtCommands.Clear();
        }

        public void newCommand(string p)
        {
            if (txtCommandsUsed.InvokeRequired)
            {
                LogDelegate logDelegate = newCommand;
                Invoke(logDelegate, p);
            }
            else
            {
                txtCommandsUsed.AppendText("\r\n" + p);
            }
        }

        void ChangeCheck(string newCheck)
        {
            Server.ConsoleName = newCheck;
        }

        void btnProperties_Click_1(object sender, EventArgs e)
        {
            if (!prevLoaded)
            {
                PropertyForm = new PropertyWindow();
                prevLoaded = true;
            }
            PropertyForm.ShowAt(FormCenter);
        }

        void btnUpdate_Click_1(object sender, EventArgs e) {}

        void gBChat_Enter(object sender, EventArgs e) {}

        void btnExtra_Click_1(object sender, EventArgs e)
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

        void Window_Resize(object sender, EventArgs e)
        {
            Hide();
        }

        void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            BringToFront();
            WindowState = 0;
        }

        void button1_Click_1(object sender, EventArgs e)
        {
            UpdateForm = new UpdateWindow();
            UpdateForm.Show();
        }

        void tmrRestart_Tick(object sender, EventArgs e)
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
                Program.ExitProgram(AutoRestart: true);
            }
        }

        void openConsole_Click(object sender, EventArgs e)
        {
            Show();
            BringToFront();
            WindowState = 0;
            Show();
            BringToFront();
            WindowState = 0;
        }

        void hideConsole_Click(object sender, EventArgs e)
        {
            WindowState = (FormWindowState)1;
            Hide();
        }

        void shutdownServer_Click(object sender, EventArgs e)
        {
            ServerProperties.Save();
            if (notifyIcon1 != null)
            {
                notifyIcon1.Visible = false;
            }
            Program.ExitProgram(AutoRestart: false);
        }

        void voiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null)
            {
                Command.all.Find("voice").Use(null, player.name);
            }
        }

        void whoisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null)
            {
                Command.all.Find("whois").Use(null, player.name);
            }
        }

        void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null)
            {
                Command.all.Find("kick").Use(null, player.name + " You have been kicked by the console.");
            }
        }

        void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player player = Player.FindExact(listViewPlayers.LastSelectedItemName);
            if (player != null)
            {
                Command.all.Find("ban").Use(null, player.name);
            }
        }

        void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 0");
                FillMainMapListView();
            }
        }

        void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 1");
                FillMainMapListView();
            }
        }

        void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 2");
                FillMainMapListView();
            }
        }

        void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 3");
                FillMainMapListView();
            }
        }

        void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("physics").Use(null, level.name + " 4");
                FillMainMapListView();
            }
        }

        void unloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("unload").Use(null, level.name);
                FillMainMapListView();
            }
        }

        void finiteModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " finite");
                FillMainMapListView();
            }
        }

        void animalAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " ai");
                FillMainMapListView();
            }
        }

        void edgeWaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " edge");
                FillMainMapListView();
            }
        }

        void growingGrassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " grass");
                FillMainMapListView();
            }
        }

        void survivalDeathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " death");
            }
        }

        void killerBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " killer");
                FillMainMapListView();
            }
        }

        void rPChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("map").Use(null, level.name + " chat");
                FillMainMapListView();
            }
        }

        void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Level level = Level.FindExact(listViewMaps.LastSelectedItemName);
            if (level != null)
            {
                Command.all.Find("save").Use(null, level.name);
                FillMainMapListView();
            }
        }

        void tabControl1_Click(object sender, EventArgs e)
        {
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001f: Expected O, but got Unknown
            //IL_0035: Unknown result type (might be due to invalid IL or missing references)
            //IL_003b: Expected O, but got Unknown
            //IL_0044: Unknown result type (might be due to invalid IL or missing references)
            //IL_004a: Expected O, but got Unknown
            foreach (TabPage tabPage in mainTabs.TabPages)
            {
                TabPage val = tabPage;
                foreach (Control item in (ArrangedElementCollection)val.Controls)
                {
                    Control val2 = item;
                    if (val2 is TextBox)
                    {
                        TextBox val3 = (TextBox)val2;
                        val3.Update();
                    }
                }
            }
        }

        void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = (FormWindowState)1;
            Hide();
        }

        void updateLabel_Click(object sender, EventArgs e) {}

        void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Command.all.Count == 0)
            {
                return;
            }
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

        void button1_Click(object sender, EventArgs e)
        {
            if (!lavaSettingsPrevLoaded)
            {
                LavaPropertiesForm = new LavaProperties();
                lavaSettingsPrevLoaded = true;
            }
            LavaPropertiesForm.Show();
        }

        void zombieSettings_Click(object sender, EventArgs e)
        {
            if (!zombieSettingsPrevLoaded)
            {
                ZombiePropertiesForm = new ZombieProperties();
                zombieSettingsPrevLoaded = true;
            }
            ZombiePropertiesForm.Show();
        }

        void txtSystem_TextChanged(object sender, EventArgs e) {}

        void liClients_SelectedIndexChanged(object sender, EventArgs e) {}

        public void UpdateChat(string msg)
        {
            if (GeneralSettings.All.UseChat && !shuttingDown)
            {
                if (chatMainBox.InvokeRequired)
                {
                    BeginInvoke(new Action<string>(UpdateChat), msg);
                }
                else
                {
                    UpChat(msg);
                }
            }
        }

        public void UpChat(string message)
        {
            try
            {
                var list = new List<Coloring>();
                Player.FilterMessageConsole(ref message);
                message = message.Replace('\u0003', '♥').Replace('\u0004', '♦').Replace('\a', '●')
                    .Replace('\b', '○')
                    .Replace('\v', '♂')
                    .Replace('\f', '♀')
                    .Replace('\u0010', '►')
                    .Replace('\u0011', '◄')
                    .Replace('\u0013', '‼')
                    .Replace('\u000f', '☼')
                    .Replace('\u0016', '▄');
                message = "&0" + message + "\r\n";
                for (int num = message.IndexOf('&'); num != -1; num = message.IndexOf('&'))
                {
                    Color color = Color.White;
                    switch (message[num + 1])
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
                            chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[i].index), chatMainBox.Text.Length - (list[i + 1].index - list[i].index));
                            chatMainBox.SelectionColor = list[i].color;
                        }
                        chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[i].index), chatMainBox.Text.Length - (message.Length - list[i].index));
                        chatMainBox.SelectionColor = list[i].color;
                    }
                    else if (list.Count == 1)
                    {
                        chatMainBox.Select(chatMainBox.Text.Length - (message.Length - list[0].index), chatMainBox.Text.Length - (message.Length - list[0].index));
                        chatMainBox.SelectionColor = list[0].color;
                    }
                    chatMainBox.SelectionStart = chatMainBox.Text.Length;
                    chatMainBox.ScrollToCaret();
                    if (chatMainBox.Text.Length > 10000)
                    {
                        int num2 = chatMainBox.Text.IndexOf('\n', chatMainBox.Text.Length - 8000) + 1;
                        if (num2 == -1)
                        {
                            num2 = chatMainBox.Text.Length - 8000;
                        }
                        chatMainBox.Select(num2, chatMainBox.Text.Length - 1 - num2);
                        string selectedRtf = chatMainBox.SelectedRtf;
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

        void chatTab_Click(object sender, EventArgs e) {}

        void chatTab_Focus(object sender, EventArgs e)
        {
            chatInputBox.Focus();
        }

        void lavaTab_Click(object sender, EventArgs e) {}

        void txtInput_TextChanged(object sender, EventArgs e) {}

        void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0008: Invalid comparison between Unknown and I4
            if ((int)e.KeyCode != 13)
            {
                return;
            }
            e.SuppressKeyPress = true;
            if (chatInputBox.Text == null || chatInputBox.Text.Trim() == "")
            {
                return;
            }
            if (!ServerProperties.ValidString(chatInputBox.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
            {
                chatInputBox.Text = "Invalid character detected.";
                return;
            }
            if (chatInputBox.Text[0] != '/')
            {
                if (chatInputBox.Text != null && !(chatInputBox.Text.Trim() == ""))
                {
                    string text = chatInputBox.Text.Trim();
                    string text2 = text;
                    if (chatInputBox.Text[0] == '#')
                    {
                        text2 = text.Remove(0, 1).Trim();
                        Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1) + "&f- " +
                                                text2);
                        Server.s.Log("(OPs): Console: " + text2);
                        Player.IRCSay("Console: " + text2, opchat: true);
                        chatInputBox.Clear();
                    }
                    else
                    {
                        Player.GlobalMessage(Server.ConsoleRealName + chatInputBox.Text);
                        Player.IRCSay(Server.ConsoleRealNameIRC + chatInputBox.Text);
                        WriteLine("<CONSOLE> " + chatInputBox.Text);
                        chatInputBox.Clear();
                    }
                }
                return;
            }
            string text3 = "";
            string text4 = "";
            if (chatInputBox.Text == null || chatInputBox.Text.Trim() == "")
            {
                Server.s.CommandUsed("CONSOLE: Whitespace commands are not allowed.");
                chatInputBox.Clear();
                return;
            }
            if (chatInputBox.Text[0] == '/' && chatInputBox.Text.Length > 1)
            {
                chatInputBox.Text = chatInputBox.Text.Substring(1);
            }
            if (chatInputBox.Text.IndexOf(' ') != -1)
            {
                text3 = chatInputBox.Text.Split(' ')[0];
                text4 = chatInputBox.Text.Substring(chatInputBox.Text.IndexOf(' ') + 1);
            }
            else
            {
                if (!(chatInputBox.Text != ""))
                {
                    return;
                }
                text3 = chatInputBox.Text;
            }
            try
            {
                Command command = Command.all.Find(text3);
                if (command != null)
                {
                    if (!command.ConsoleAccess)
                    {
                        Server.s.CommandUsed(string.Format("You can't use {0} command from console.", text3));
                        return;
                    }
                    command.Use(null, text4);
                    Server.s.CommandUsed("CONSOLE: USED /" + text3 + " " + text4);
                }
                if (command == null)
                {
                    Server.s.CommandUsed("CONSOLE: Command  '/" + text3 + "'  does not exist.");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Server.s.CommandUsed("CONSOLE: Failed command.");
            }
            chatInputBox.Clear();
        }

        void playersList_SelectedIndexChanged(object sender, EventArgs e) {}

        void playersList_DataSourceChanged()
        {
            if (!Player.players.Contains((Player)playersGrid.SelectedObject))
            {
                playersGrid.SelectedObject = null;
            }
        }

        void mapsList_DataSourceChanged()
        {
            if (!Server.levels.Contains((Level)allMapsGrid.SelectedObject))
            {
                allMapsGrid.SelectedObject = null;
            }
        }

        void mapsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mapsList.SelectedIndex != -1)
                {
                    allMapsGrid.SelectedObject = Level.Find(mapsList.Items[mapsList.SelectedIndex].ToString());
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void chatTab_Click_1(object sender, EventArgs e) {}

        void gBCommands_Enter(object sender, EventArgs e) {}

        void txtLog_TextChanged(object sender, EventArgs e) {}

        void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {}

        void toolStripContainer2_ContentPanel_Load(object sender, EventArgs e) {}

        void playersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {}

        void playersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playersListView.SelectedIndices.Count > 0 && selectedPlayerName != playersListView.Items[playersListView.SelectedIndices[0]].SubItems[0].Text)
            {
                selectedPlayerName = playersListView.Items[playersListView.SelectedIndices[0]].SubItems[0].Text;
                selectedPlayer = Player.Find(selectedPlayerName);
                playersGrid.SelectedObject = selectedPlayer;
            }
            SelectionChangedCommandsUpdate();
        }

        void mapsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {}

        void button3_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                if (kickCheck.Checked)
                {
                    Command.all.Find("kick").Use(null, selectedPlayer.name + " " + kickText.Text);
                }
                else
                {
                    Command.all.Find("kick").Use(null, selectedPlayer.name + " &cKicked.");
                }
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null && ConfirmationQuestionPopup("              Ban " + selectedPlayer.name))
            {
                if (banCheck.Checked)
                {
                    Command.all.Find("ban").Use(null, selectedPlayer.name + " " + banText.Text);
                }
                else
                {
                    Command.all.Find("ban").Use(null, selectedPlayer.name + " &cYou are banned!");
                }
            }
        }

        void button4_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null && ConfirmationQuestionPopup("            Xban " + selectedPlayer.name))
            {
                if (banCheck.Checked)
                {
                    Command.all.Find("xban").Use(null, selectedPlayer.name + " " + xbanText.Text);
                }
                else
                {
                    Command.all.Find("xban").Use(null, selectedPlayer.name + " &cYou are banned!");
                }
            }
        }

        void button15_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null && ConfirmationQuestionPopup("    Undo all " + selectedPlayer.name + " actions"))
            {
                Command.all.Find("undo").Use(null, selectedPlayer.name + " all");
            }
        }

        void kickCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (kickCheck.Checked)
            {
                kickText.ReadOnly = false;
            }
            else
            {
                kickText.ReadOnly = true;
            }
        }

        void banCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (banCheck.Checked)
            {
                banText.ReadOnly = false;
            }
            else
            {
                banText.ReadOnly = true;
            }
        }

        void xbanCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xbanCheck.Checked)
            {
                xbanText.ReadOnly = false;
            }
            else
            {
                xbanText.ReadOnly = true;
            }
        }

        void kickText_TextChanged(object sender, EventArgs e) {}

        void button8_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Command.all.Find("kill").Use(null, selectedPlayer.name);
            }
        }

        void button5_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Command.all.Find("mute").Use(null, selectedPlayer.name);
            }
        }

        void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {}

        void button6_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Command.all.Find("settitle").Use(null, selectedPlayer.name + " " + titleText.Text);
            }
        }

        void button7_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Command.all.Find("setcolor").Use(null, selectedPlayer.name + " " + playerColorCombo.Items[playerColorCombo.SelectedIndex]);
            }
        }

        void button14_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Command.all.Find("move").Use(null, selectedPlayer.name + " " + targetMapCombo.Items[targetMapCombo.SelectedIndex]);
            }
        }

        void SelectionChangedCommandsUpdate()
        {
            if (selectedPlayer != null)
            {
                btnMute.Text = selectedPlayer.muted ? "Unmute" : "Mute";
                var levelNames = new List<string>();
                Server.levels.ForEach(delegate(Level l) { levelNames.Add(l.name); });
                string text = "";
                if (targetMapCombo.SelectedItem != null)
                {
                    text = targetMapCombo.SelectedItem.ToString();
                }
                targetMapCombo.Items.Clear();
                targetMapCombo.Items.AddRange(levelNames.ToArray());
                int num = targetMapCombo.FindString(text);
                if (num != -1)
                {
                    targetMapCombo.SelectedIndex = num;
                }
                playerColorCombo.SelectedIndex = playerColorCombo.FindString(c.Name(selectedPlayer.color));
                if (!titleText.Focused)
                {
                    titleText.Text = selectedPlayer.prefix.Trim().Trim('[', ']');
                }
            }
            else
            {
                titleText.Text = "";
                btnMute.Text = "Mute";
                playerColorCombo.SelectedIndex = 0;
            }
        }

        void consoleName_TextChanged(object sender, EventArgs e) {}

        void button9_Click(object sender, EventArgs e)
        {
            if (createMap == null || createMap.IsDisposed)
            {
                createMap = new CreateMap();
                createMap.Show();
            }
            else
            {
                createMap.BringToFront();
                createMap.Show();
            }
        }

        void button13_Click(object sender, EventArgs e)
        {
            if (mapsList.SelectedIndex != -1)
            {
                Command.all.Find("unload").Use(null, mapsList.Items[mapsList.SelectedIndex].ToString());
            }
        }

        void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (mapsList.SelectedIndex != -1)
                {
                    string text = mapsList.Items[mapsList.SelectedIndex].ToString();
                    if (ConfirmationQuestionPopup("             Delete " + text))
                    {
                        Command.all.Find("deletelvl").Use(null, text);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void RefreshUnloadedMapsList()
        {
            if (unloadedMapsList != null)
            {
                if (unloadedMapsList.InvokeRequired)
                {
                    RefreshMapList refreshMapList = DoRefreshUnloadedMapsList;
                    Invoke(refreshMapList);
                }
                else
                {
                    DoRefreshUnloadedMapsList();
                }
            }
        }

        public void DoRefreshUnloadedMapsList()
        {
            var loadedLevels = new List<string>();
            var list = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo("levels/");
            FileInfo[] files = directoryInfo.GetFiles("*.lvl");
            if (Server.levels == null)
            {
                return;
            }
            Server.levels.ForEach(delegate(Level l) { loadedLevels.Add(l.name.ToLower()); });
            if (files != null)
            {
                FileInfo[] array = files;
                foreach (FileInfo fileInfo in array)
                {
                    if (!loadedLevels.Contains(fileInfo.Name.Replace(".lvl", "").ToLower()))
                    {
                        list.Add(fileInfo.Name.Remove(fileInfo.Name.Length - 4, 4));
                    }
                }
            }
            unloadedMapsList.Items.Clear();
            if (list.Count > 0)
            {
                unloadedMapsList.Items.AddRange(list.ToArray());
            }
        }

        void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (unloadedMapsList.SelectedIndex != -1)
                {
                    Command.all.Find("load").Use(null, unloadedMapsList.Items[unloadedMapsList.SelectedIndex].ToString());
                    RefreshUnloadedMapsList();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void Color_Click(object sender, EventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            Label val = (Label)sender;
            if (chatInputBox.Text.Length > 0 && "%&".Contains(chatInputBox.Text[chatInputBox.Text.Length - 1].ToString()))
            {
                chatInputBox.Text = chatInputBox.Text.Remove(chatInputBox.Text.Length - 1);
            }
            if (chatInputBox.Text.Length > 1 && (chatInputBox.Text[chatInputBox.Text.Length - 2] == '%' || chatInputBox.Text[chatInputBox.Text.Length - 2] == '&') &&
                "abcdef1234567890".Contains(chatInputBox.Text[chatInputBox.Text.Length - 1].ToString()))
            {
                chatInputBox.Text = chatInputBox.Text.Remove(chatInputBox.Text.Length - 2);
            }
            chatInputBox.AppendText(val.Tag.ToString());
        }

        void label24_Click(object sender, EventArgs e) {}

        void txtErrors_TextChanged(object sender, EventArgs e) {}

        void chatOnOff_btn_Click(object sender, EventArgs e)
        {
            if (GeneralSettings.All.UseChat)
            {
                GeneralSettings.All.UseChat = false;
                chatOnOff_btn.Text = "Deactivated";
            }
            else
            {
                GeneralSettings.All.UseChat = true;
                chatOnOff_btn.Text = "Activated";
            }
        }

        void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e) {}

        void label2_Click(object sender, EventArgs e) {}

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
                //IL_005e: Unknown result type (might be due to invalid IL or missing references)
                //IL_0064: Expected O, but got Unknown
                if (elements.Length > 0)
                {
                    if (!listViewPlayers.GroupExists(elements[0]))
                    {
                        listViewPlayers.ClearGroups();
                        try
                        {
                            for (int num = Group.groupList.Count - 1; num >= 0; num--)
                            {
                                listViewPlayers.AddGroup(Group.groupList[num].trueName);
                            }
                        }
                        catch {}
                    }
                    ListViewItem val = new ListViewItem(elements[1]);
                    val.Group = listViewPlayers.GetGroup(elements[0]);
                    if (elements.Length > 2)
                    {
                        for (int i = 2; i < elements.Length; i++)
                        {
                            val.SubItems.Add(elements[i]);
                        }
                    }
                    listViewPlayers.Items.Add(val);
                }
            });
            listViewPlayers.EndUpdate();
            listViewPlayers.Refresh();
        }

        void UpdateMapsListView(List<string[]> listElements)
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
                //IL_0077: Unknown result type (might be due to invalid IL or missing references)
                //IL_007d: Expected O, but got Unknown
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
                    ListViewItem val = new ListViewItem(elements[1]);
                    val.Group = listViewMaps.GetGroup(elements[0]);
                    if (elements.Length > 2)
                    {
                        for (int i = 2; i < elements.Length; i++)
                        {
                            val.SubItems.Add(elements[i]);
                        }
                    }
                    listViewMaps.Items.Add(val);
                }
            });
            listViewMaps.EndUpdate();
            listViewMaps.Refresh();
        }

        void listViewPlayers_SelectedIndexChanged(object sender, EventArgs e) {}

        void SaveGuiSettings()
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Invalid comparison between Unknown and I4
            if (WindowState == 0 || (int)WindowState == 1)
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
                GuiSettings.All.MainSplitter3Distance = (int)(splitContainer3.SplitterDistance * (double)split3Width / splitContainer3.Width);
                GuiSettings.All.MainSplitter2Distance = (int)(splitContainer2.SplitterDistance * (double)split2Height / splitContainer2.Height);
                GuiSettings.All.MainSplitter4Distance = (int)(splitContainer4.SplitterDistance * (double)split4Height / splitContainer4.Height);
                GuiSettings.All.MainSplitter5Distance = (int)(splitContainer5.SplitterDistance * (double)split5Width / splitContainer5.Width);
            }
            GuiSettings.All.Save();
            RemoteSettings.All.Save();
        }

        void LoadGuiSettings()
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

        void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (toolsForm == null || toolsForm.IsDisposed)
                {
                    toolsForm = new Tools();
                    toolsForm.ShowAt(FormCenter);
                    return;
                }
                if (!toolsForm.Visible)
                {
                    toolsForm.ShowAt(FormCenter);
                }
                toolsForm.BringToFront();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void mCount_Click(object sender, EventArgs e) {}

        void button9_Click_1(object sender, EventArgs e)
        {
            //IL_000b: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
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

        void chatBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (Trader.messages.Count > 0)
                {
                    chatMainBox.SuspendLayout();
                    string text = (string)Trader.messages.Dequeue();
                    text = DateTime.Now.ToString("(HH:mm:ss) ") + text;
                    var list = new List<Coloring>();
                    Player.FilterMessageConsole(ref text);
                    text = text.Replace('\u0003', '♥').Replace('\u0004', '♦').Replace('\a', '●')
                        .Replace('\b', '○')
                        .Replace('\v', '♂')
                        .Replace('\f', '♀')
                        .Replace('\u0010', '►')
                        .Replace('\u0011', '◄')
                        .Replace('\u0013', '‼')
                        .Replace('\u000f', '☼')
                        .Replace('\u0016', '▄');
                    text = "&0" + text + "\r\n";
                    try
                    {
                        for (int num = text.IndexOf('&'); num != -1; num = text.IndexOf('&'))
                        {
                            Color color = Color.White;
                            switch (text[num + 1])
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
                                chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[i].index), chatMainBox.Text.Length - (list[i + 1].index - list[i].index));
                                chatMainBox.SelectionColor = list[i].color;
                            }
                            chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[i].index), chatMainBox.Text.Length - (text.Length - list[i].index));
                            chatMainBox.SelectionColor = list[i].color;
                        }
                        else if (list.Count == 1)
                        {
                            chatMainBox.Select(chatMainBox.Text.Length - (text.Length - list[0].index), chatMainBox.Text.Length - (text.Length - list[0].index));
                            chatMainBox.SelectionColor = list[0].color;
                        }
                        chatMainBox.SelectionStart = chatMainBox.Text.Length;
                        chatMainBox.ScrollToCaret();
                        if (chatMainBox.Text.Length > 10000)
                        {
                            int num2 = chatMainBox.Text.IndexOf('\n', chatMainBox.Text.Length - 8000) + 1;
                            if (num2 == -1)
                            {
                                num2 = chatMainBox.Text.Length - 8000;
                            }
                            chatMainBox.Select(num2, chatMainBox.Text.Length - 1 - num2);
                            string selectedRtf = chatMainBox.SelectedRtf;
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
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Listener.Start(int.Parse(textBox1.Text));
            }
            else
            {
                Listener.Stop();
            }
            RemoteSettings.All.AllowRemoteAccess = checkBox1.Checked;
        }

        public static void UpdateAccountsList(string[] accounts)
        {
            if (thisWindow.accountsList.InvokeRequired)
            {
                thisWindow.accountsList.Invoke(new Action<string[]>(UpdateAccountsList), accounts);
                return;
            }
            thisWindow.accountsList.SuspendLayout();
            thisWindow.accountsList.RemoveAllItems();
            foreach (string text in accounts)
            {
                thisWindow.accountsList.Items.Add(text);
            }
            thisWindow.accountsList.ResumeLayout();
        }

        void newAccountBtn_Click(object sender, EventArgs e)
        {
            NewAccount newAccount = new NewAccount(this);
            newAccount.Show();
            ActiveControl = null;
        }

        void changeAccountBtn_Click(object sender, EventArgs e)
        {
            if (accountsList.SelectedIndices.Count == 1)
            {
                new ChangeAccount(this, accountsList.SelectedItems[0].Text).Show();
            }
            ActiveControl = null;
        }

        void removeAccountBtn_Click(object sender, EventArgs e)
        {
            if (accountsList.SelectedIndices.Count == 1)
            {
                new RemoveAccount(this, accountsList.SelectedItems[0].Text).Show();
            }
            ActiveControl = null;
        }

        void textBox1_Validating(object sender, CancelEventArgs e)
        {
            int result;
            if (!int.TryParse(textBox1.Text, out result))
            {
                textBox1.Text = "33434";
                e.Cancel = true;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    //IL_000a: Unknown result type (might be due to invalid IL or missing references)
                    MessageBox.Show("You have to enter an integer.", "Validation Error");
                });
            }
            if (result == Server.port)
            {
                textBox1.Text = "33434";
                e.Cancel = true;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    //IL_000a: Unknown result type (might be due to invalid IL or missing references)
                    MessageBox.Show("This port is already in use.", "Validation Error");
                });
            }
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RemoteSettings.All.ShowInBrowser = checkBox2.Checked;
        }

        void button18_Click(object sender, EventArgs e) {}

        void button9_Click_2(object sender, EventArgs e)
        {
            RemoteSettings.All.Port = int.Parse(textBox1.Text);
            if (checkBox1.Checked)
            {
                Listener.Stop();
                Listener.Start(RemoteSettings.All.Port);
            }
            checkBox1.Checked = true;
            ActiveControl = null;
        }

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://mcdzienny.cba.pl/remote.php");
        }

        public void SetFont(Font f)
        {
            if (chatMainBox.InvokeRequired)
            {
                chatMainBox.Invoke((Action)delegate { SetFont(f); });
                return;
            }
            chatMainBox.SelectAll();
            chatMainBox.SelectionFont = f;
            string selectedRtf = chatMainBox.SelectedRtf;
            chatMainBox.Font = f;
            chatMainBox.Rtf = selectedRtf;
            chatMainBox.AppendText(Environment.NewLine);
        }

        public Font GetFont()
        {
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Expected O, but got Unknown
            return (Font)chatMainBox.Invoke((Func<Font>)(() => chatMainBox.Font));
        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorChatSettings == null || colorChatSettings.IsDisposed)
            {
                colorChatSettings = new ColorChatSettings();
                colorChatSettings.ShowAt(FormCenter);
            }
            else
            {
                colorChatSettings.BringToFront();
            }
        }

        void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e) {}

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                AvailablePlugin availablePlugin = Server.Plugins.AvailablePlugins.Find(treeView1.SelectedNode.Text);
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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 5 && (int)m.WParam == 1 && Minimize != null)
            {
                Window_Minimize(this, EventArgs.Empty);
            }
            if (m.Msg == 274 && !(m.WParam == new IntPtr(61472)) && m.WParam == new IntPtr(61488))
            {
                split2Height = splitContainer2.Height;
                split4Height = splitContainer4.Height;
                split5Width = splitContainer5.Width;
                split3Width = splitContainer3.Width;
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            //IL_000b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0011: Expected O, but got Unknown
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Expected O, but got Unknown
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Expected O, but got Unknown
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0023: Expected O, but got Unknown
            //IL_003b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0045: Expected O, but got Unknown
            //IL_0046: Unknown result type (might be due to invalid IL or missing references)
            //IL_0050: Expected O, but got Unknown
            //IL_0051: Unknown result type (might be due to invalid IL or missing references)
            //IL_005b: Expected O, but got Unknown
            //IL_005c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0066: Expected O, but got Unknown
            //IL_0067: Unknown result type (might be due to invalid IL or missing references)
            //IL_0071: Expected O, but got Unknown
            //IL_0072: Unknown result type (might be due to invalid IL or missing references)
            //IL_007c: Expected O, but got Unknown
            //IL_007d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0087: Expected O, but got Unknown
            //IL_0088: Unknown result type (might be due to invalid IL or missing references)
            //IL_0092: Expected O, but got Unknown
            //IL_0093: Unknown result type (might be due to invalid IL or missing references)
            //IL_009d: Expected O, but got Unknown
            //IL_009e: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a8: Expected O, but got Unknown
            //IL_00a9: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b3: Expected O, but got Unknown
            //IL_00b4: Unknown result type (might be due to invalid IL or missing references)
            //IL_00be: Expected O, but got Unknown
            //IL_00bf: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c9: Expected O, but got Unknown
            //IL_00ca: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d4: Expected O, but got Unknown
            //IL_00d5: Unknown result type (might be due to invalid IL or missing references)
            //IL_00df: Expected O, but got Unknown
            //IL_00e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ea: Expected O, but got Unknown
            //IL_00eb: Unknown result type (might be due to invalid IL or missing references)
            //IL_00f5: Expected O, but got Unknown
            //IL_00fc: Unknown result type (might be due to invalid IL or missing references)
            //IL_0106: Expected O, but got Unknown
            //IL_0107: Unknown result type (might be due to invalid IL or missing references)
            //IL_0111: Expected O, but got Unknown
            //IL_0112: Unknown result type (might be due to invalid IL or missing references)
            //IL_011c: Expected O, but got Unknown
            //IL_011d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0127: Expected O, but got Unknown
            //IL_0128: Unknown result type (might be due to invalid IL or missing references)
            //IL_0132: Expected O, but got Unknown
            //IL_0133: Unknown result type (might be due to invalid IL or missing references)
            //IL_013d: Expected O, but got Unknown
            //IL_013e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0148: Expected O, but got Unknown
            //IL_0149: Unknown result type (might be due to invalid IL or missing references)
            //IL_0153: Expected O, but got Unknown
            //IL_015f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0169: Expected O, but got Unknown
            //IL_016a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0174: Expected O, but got Unknown
            //IL_0175: Unknown result type (might be due to invalid IL or missing references)
            //IL_017f: Expected O, but got Unknown
            //IL_0180: Unknown result type (might be due to invalid IL or missing references)
            //IL_018a: Expected O, but got Unknown
            //IL_018b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0195: Expected O, but got Unknown
            //IL_0196: Unknown result type (might be due to invalid IL or missing references)
            //IL_01a0: Expected O, but got Unknown
            //IL_01a1: Unknown result type (might be due to invalid IL or missing references)
            //IL_01ab: Expected O, but got Unknown
            //IL_01b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_01bc: Expected O, but got Unknown
            //IL_01c3: Unknown result type (might be due to invalid IL or missing references)
            //IL_01cd: Expected O, but got Unknown
            //IL_01ce: Unknown result type (might be due to invalid IL or missing references)
            //IL_01d8: Expected O, but got Unknown
            //IL_01d9: Unknown result type (might be due to invalid IL or missing references)
            //IL_01e3: Expected O, but got Unknown
            //IL_01e4: Unknown result type (might be due to invalid IL or missing references)
            //IL_01ee: Expected O, but got Unknown
            //IL_01ef: Unknown result type (might be due to invalid IL or missing references)
            //IL_01f9: Expected O, but got Unknown
            //IL_01fa: Unknown result type (might be due to invalid IL or missing references)
            //IL_0204: Expected O, but got Unknown
            //IL_0205: Unknown result type (might be due to invalid IL or missing references)
            //IL_020f: Expected O, but got Unknown
            //IL_0210: Unknown result type (might be due to invalid IL or missing references)
            //IL_021a: Expected O, but got Unknown
            //IL_021b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0225: Expected O, but got Unknown
            //IL_0226: Unknown result type (might be due to invalid IL or missing references)
            //IL_0230: Expected O, but got Unknown
            //IL_0231: Unknown result type (might be due to invalid IL or missing references)
            //IL_023b: Expected O, but got Unknown
            //IL_023c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0246: Expected O, but got Unknown
            //IL_0247: Unknown result type (might be due to invalid IL or missing references)
            //IL_0251: Expected O, but got Unknown
            //IL_0252: Unknown result type (might be due to invalid IL or missing references)
            //IL_025c: Expected O, but got Unknown
            //IL_025d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0267: Expected O, but got Unknown
            //IL_0268: Unknown result type (might be due to invalid IL or missing references)
            //IL_0272: Expected O, but got Unknown
            //IL_0273: Unknown result type (might be due to invalid IL or missing references)
            //IL_027d: Expected O, but got Unknown
            //IL_027e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0288: Expected O, but got Unknown
            //IL_0289: Unknown result type (might be due to invalid IL or missing references)
            //IL_0293: Expected O, but got Unknown
            //IL_0294: Unknown result type (might be due to invalid IL or missing references)
            //IL_029e: Expected O, but got Unknown
            //IL_029f: Unknown result type (might be due to invalid IL or missing references)
            //IL_02a9: Expected O, but got Unknown
            //IL_02aa: Unknown result type (might be due to invalid IL or missing references)
            //IL_02b4: Expected O, but got Unknown
            //IL_02b5: Unknown result type (might be due to invalid IL or missing references)
            //IL_02bf: Expected O, but got Unknown
            //IL_02c0: Unknown result type (might be due to invalid IL or missing references)
            //IL_02ca: Expected O, but got Unknown
            //IL_02cb: Unknown result type (might be due to invalid IL or missing references)
            //IL_02d5: Expected O, but got Unknown
            //IL_02d6: Unknown result type (might be due to invalid IL or missing references)
            //IL_02e0: Expected O, but got Unknown
            //IL_02e1: Unknown result type (might be due to invalid IL or missing references)
            //IL_02eb: Expected O, but got Unknown
            //IL_02f7: Unknown result type (might be due to invalid IL or missing references)
            //IL_0301: Expected O, but got Unknown
            //IL_0302: Unknown result type (might be due to invalid IL or missing references)
            //IL_030c: Expected O, but got Unknown
            //IL_030d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0317: Expected O, but got Unknown
            //IL_0318: Unknown result type (might be due to invalid IL or missing references)
            //IL_0322: Expected O, but got Unknown
            //IL_0323: Unknown result type (might be due to invalid IL or missing references)
            //IL_032d: Expected O, but got Unknown
            //IL_0339: Unknown result type (might be due to invalid IL or missing references)
            //IL_0343: Expected O, but got Unknown
            //IL_0344: Unknown result type (might be due to invalid IL or missing references)
            //IL_034e: Expected O, but got Unknown
            //IL_034f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0359: Expected O, but got Unknown
            //IL_035a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0364: Expected O, but got Unknown
            //IL_0365: Unknown result type (might be due to invalid IL or missing references)
            //IL_036f: Expected O, but got Unknown
            //IL_0370: Unknown result type (might be due to invalid IL or missing references)
            //IL_037a: Expected O, but got Unknown
            //IL_037b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0385: Expected O, but got Unknown
            //IL_0386: Unknown result type (might be due to invalid IL or missing references)
            //IL_0390: Expected O, but got Unknown
            //IL_0391: Unknown result type (might be due to invalid IL or missing references)
            //IL_039b: Expected O, but got Unknown
            //IL_039c: Unknown result type (might be due to invalid IL or missing references)
            //IL_03a6: Expected O, but got Unknown
            //IL_03a7: Unknown result type (might be due to invalid IL or missing references)
            //IL_03b1: Expected O, but got Unknown
            //IL_03b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_03bc: Expected O, but got Unknown
            //IL_03bd: Unknown result type (might be due to invalid IL or missing references)
            //IL_03c7: Expected O, but got Unknown
            //IL_03c8: Unknown result type (might be due to invalid IL or missing references)
            //IL_03d2: Expected O, but got Unknown
            //IL_03d3: Unknown result type (might be due to invalid IL or missing references)
            //IL_03dd: Expected O, but got Unknown
            //IL_03de: Unknown result type (might be due to invalid IL or missing references)
            //IL_03e8: Expected O, but got Unknown
            //IL_03e9: Unknown result type (might be due to invalid IL or missing references)
            //IL_03f3: Expected O, but got Unknown
            //IL_03f4: Unknown result type (might be due to invalid IL or missing references)
            //IL_03fe: Expected O, but got Unknown
            //IL_03ff: Unknown result type (might be due to invalid IL or missing references)
            //IL_0409: Expected O, but got Unknown
            //IL_040a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0414: Expected O, but got Unknown
            //IL_0415: Unknown result type (might be due to invalid IL or missing references)
            //IL_041f: Expected O, but got Unknown
            //IL_0420: Unknown result type (might be due to invalid IL or missing references)
            //IL_042a: Expected O, but got Unknown
            //IL_042b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0435: Expected O, but got Unknown
            //IL_0436: Unknown result type (might be due to invalid IL or missing references)
            //IL_0440: Expected O, but got Unknown
            //IL_0441: Unknown result type (might be due to invalid IL or missing references)
            //IL_044b: Expected O, but got Unknown
            //IL_044c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0456: Expected O, but got Unknown
            //IL_0457: Unknown result type (might be due to invalid IL or missing references)
            //IL_0461: Expected O, but got Unknown
            //IL_0462: Unknown result type (might be due to invalid IL or missing references)
            //IL_046c: Expected O, but got Unknown
            //IL_046d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0477: Expected O, but got Unknown
            //IL_0478: Unknown result type (might be due to invalid IL or missing references)
            //IL_0482: Expected O, but got Unknown
            //IL_0483: Unknown result type (might be due to invalid IL or missing references)
            //IL_048d: Expected O, but got Unknown
            //IL_048e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0498: Expected O, but got Unknown
            //IL_0499: Unknown result type (might be due to invalid IL or missing references)
            //IL_04a3: Expected O, but got Unknown
            //IL_04a4: Unknown result type (might be due to invalid IL or missing references)
            //IL_04ae: Expected O, but got Unknown
            //IL_04af: Unknown result type (might be due to invalid IL or missing references)
            //IL_04b9: Expected O, but got Unknown
            //IL_04ba: Unknown result type (might be due to invalid IL or missing references)
            //IL_04c4: Expected O, but got Unknown
            //IL_04c5: Unknown result type (might be due to invalid IL or missing references)
            //IL_04cf: Expected O, but got Unknown
            //IL_04d0: Unknown result type (might be due to invalid IL or missing references)
            //IL_04da: Expected O, but got Unknown
            //IL_04db: Unknown result type (might be due to invalid IL or missing references)
            //IL_04e5: Expected O, but got Unknown
            //IL_04e6: Unknown result type (might be due to invalid IL or missing references)
            //IL_04f0: Expected O, but got Unknown
            //IL_04f1: Unknown result type (might be due to invalid IL or missing references)
            //IL_04fb: Expected O, but got Unknown
            //IL_04fc: Unknown result type (might be due to invalid IL or missing references)
            //IL_0506: Expected O, but got Unknown
            //IL_0507: Unknown result type (might be due to invalid IL or missing references)
            //IL_0511: Expected O, but got Unknown
            //IL_0512: Unknown result type (might be due to invalid IL or missing references)
            //IL_051c: Expected O, but got Unknown
            //IL_051d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0527: Expected O, but got Unknown
            //IL_0528: Unknown result type (might be due to invalid IL or missing references)
            //IL_0532: Expected O, but got Unknown
            //IL_0533: Unknown result type (might be due to invalid IL or missing references)
            //IL_053d: Expected O, but got Unknown
            //IL_053e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0548: Expected O, but got Unknown
            //IL_0549: Unknown result type (might be due to invalid IL or missing references)
            //IL_0553: Expected O, but got Unknown
            //IL_0554: Unknown result type (might be due to invalid IL or missing references)
            //IL_055e: Expected O, but got Unknown
            //IL_055f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0569: Expected O, but got Unknown
            //IL_056a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0574: Expected O, but got Unknown
            //IL_0575: Unknown result type (might be due to invalid IL or missing references)
            //IL_057f: Expected O, but got Unknown
            //IL_0580: Unknown result type (might be due to invalid IL or missing references)
            //IL_058a: Expected O, but got Unknown
            //IL_058b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0595: Expected O, but got Unknown
            //IL_0596: Unknown result type (might be due to invalid IL or missing references)
            //IL_05a0: Expected O, but got Unknown
            //IL_05a1: Unknown result type (might be due to invalid IL or missing references)
            //IL_05ab: Expected O, but got Unknown
            //IL_05ac: Unknown result type (might be due to invalid IL or missing references)
            //IL_05b6: Expected O, but got Unknown
            //IL_05b7: Unknown result type (might be due to invalid IL or missing references)
            //IL_05c1: Expected O, but got Unknown
            //IL_05c2: Unknown result type (might be due to invalid IL or missing references)
            //IL_05cc: Expected O, but got Unknown
            //IL_05cd: Unknown result type (might be due to invalid IL or missing references)
            //IL_05d7: Expected O, but got Unknown
            //IL_05d8: Unknown result type (might be due to invalid IL or missing references)
            //IL_05e2: Expected O, but got Unknown
            //IL_05e3: Unknown result type (might be due to invalid IL or missing references)
            //IL_05ed: Expected O, but got Unknown
            //IL_05ee: Unknown result type (might be due to invalid IL or missing references)
            //IL_05f8: Expected O, but got Unknown
            //IL_05f9: Unknown result type (might be due to invalid IL or missing references)
            //IL_0603: Expected O, but got Unknown
            //IL_0604: Unknown result type (might be due to invalid IL or missing references)
            //IL_060e: Expected O, but got Unknown
            //IL_060f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0619: Expected O, but got Unknown
            //IL_061a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0624: Expected O, but got Unknown
            //IL_0625: Unknown result type (might be due to invalid IL or missing references)
            //IL_062f: Expected O, but got Unknown
            //IL_0630: Unknown result type (might be due to invalid IL or missing references)
            //IL_063a: Expected O, but got Unknown
            //IL_063b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0645: Expected O, but got Unknown
            //IL_0646: Unknown result type (might be due to invalid IL or missing references)
            //IL_0650: Expected O, but got Unknown
            //IL_0651: Unknown result type (might be due to invalid IL or missing references)
            //IL_065b: Expected O, but got Unknown
            //IL_065c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0666: Expected O, but got Unknown
            //IL_0667: Unknown result type (might be due to invalid IL or missing references)
            //IL_0671: Expected O, but got Unknown
            //IL_0672: Unknown result type (might be due to invalid IL or missing references)
            //IL_067c: Expected O, but got Unknown
            //IL_067d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0687: Expected O, but got Unknown
            //IL_0688: Unknown result type (might be due to invalid IL or missing references)
            //IL_0692: Expected O, but got Unknown
            //IL_0693: Unknown result type (might be due to invalid IL or missing references)
            //IL_069d: Expected O, but got Unknown
            //IL_06a9: Unknown result type (might be due to invalid IL or missing references)
            //IL_06b3: Expected O, but got Unknown
            //IL_06b4: Unknown result type (might be due to invalid IL or missing references)
            //IL_06be: Expected O, but got Unknown
            //IL_06bf: Unknown result type (might be due to invalid IL or missing references)
            //IL_06c9: Expected O, but got Unknown
            //IL_06ca: Unknown result type (might be due to invalid IL or missing references)
            //IL_06d4: Expected O, but got Unknown
            //IL_06d5: Unknown result type (might be due to invalid IL or missing references)
            //IL_06df: Expected O, but got Unknown
            //IL_06e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_06ea: Expected O, but got Unknown
            //IL_06eb: Unknown result type (might be due to invalid IL or missing references)
            //IL_06f5: Expected O, but got Unknown
            //IL_06f6: Unknown result type (might be due to invalid IL or missing references)
            //IL_0700: Expected O, but got Unknown
            //IL_0701: Unknown result type (might be due to invalid IL or missing references)
            //IL_070b: Expected O, but got Unknown
            //IL_070c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0716: Expected O, but got Unknown
            //IL_0717: Unknown result type (might be due to invalid IL or missing references)
            //IL_0721: Expected O, but got Unknown
            //IL_0722: Unknown result type (might be due to invalid IL or missing references)
            //IL_072c: Expected O, but got Unknown
            //IL_072d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0737: Expected O, but got Unknown
            //IL_0738: Unknown result type (might be due to invalid IL or missing references)
            //IL_0742: Expected O, but got Unknown
            //IL_0743: Unknown result type (might be due to invalid IL or missing references)
            //IL_074d: Expected O, but got Unknown
            //IL_074e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0758: Expected O, but got Unknown
            //IL_0764: Unknown result type (might be due to invalid IL or missing references)
            //IL_076e: Expected O, but got Unknown
            //IL_076f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0779: Expected O, but got Unknown
            //IL_077a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0784: Expected O, but got Unknown
            //IL_0785: Unknown result type (might be due to invalid IL or missing references)
            //IL_078f: Expected O, but got Unknown
            //IL_0790: Unknown result type (might be due to invalid IL or missing references)
            //IL_079a: Expected O, but got Unknown
            //IL_079b: Unknown result type (might be due to invalid IL or missing references)
            //IL_07a5: Expected O, but got Unknown
            //IL_07a6: Unknown result type (might be due to invalid IL or missing references)
            //IL_07b0: Expected O, but got Unknown
            //IL_07b1: Unknown result type (might be due to invalid IL or missing references)
            //IL_07bb: Expected O, but got Unknown
            //IL_07bc: Unknown result type (might be due to invalid IL or missing references)
            //IL_07c6: Expected O, but got Unknown
            //IL_07c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_07d1: Expected O, but got Unknown
            //IL_07d2: Unknown result type (might be due to invalid IL or missing references)
            //IL_07dc: Expected O, but got Unknown
            //IL_07dd: Unknown result type (might be due to invalid IL or missing references)
            //IL_07e7: Expected O, but got Unknown
            //IL_07e8: Unknown result type (might be due to invalid IL or missing references)
            //IL_07f2: Expected O, but got Unknown
            //IL_07f3: Unknown result type (might be due to invalid IL or missing references)
            //IL_07fd: Expected O, but got Unknown
            //IL_07fe: Unknown result type (might be due to invalid IL or missing references)
            //IL_0808: Expected O, but got Unknown
            //IL_0809: Unknown result type (might be due to invalid IL or missing references)
            //IL_0813: Expected O, but got Unknown
            //IL_0814: Unknown result type (might be due to invalid IL or missing references)
            //IL_081e: Expected O, but got Unknown
            //IL_081f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0829: Expected O, but got Unknown
            //IL_082a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0834: Expected O, but got Unknown
            //IL_083b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0845: Expected O, but got Unknown
            //IL_0846: Unknown result type (might be due to invalid IL or missing references)
            //IL_0850: Expected O, but got Unknown
            //IL_0851: Unknown result type (might be due to invalid IL or missing references)
            //IL_085b: Expected O, but got Unknown
            //IL_085c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0866: Expected O, but got Unknown
            //IL_0867: Unknown result type (might be due to invalid IL or missing references)
            //IL_0871: Expected O, but got Unknown
            //IL_0872: Unknown result type (might be due to invalid IL or missing references)
            //IL_087c: Expected O, but got Unknown
            //IL_1247: Unknown result type (might be due to invalid IL or missing references)
            //IL_129e: Unknown result type (might be due to invalid IL or missing references)
            //IL_12a8: Expected O, but got Unknown
            //IL_144d: Unknown result type (might be due to invalid IL or missing references)
            //IL_1457: Expected O, but got Unknown
            //IL_1819: Unknown result type (might be due to invalid IL or missing references)
            //IL_186d: Unknown result type (might be due to invalid IL or missing references)
            //IL_18c1: Unknown result type (might be due to invalid IL or missing references)
            //IL_1915: Unknown result type (might be due to invalid IL or missing references)
            //IL_199e: Unknown result type (might be due to invalid IL or missing references)
            //IL_1a1e: Unknown result type (might be due to invalid IL or missing references)
            //IL_1b27: Unknown result type (might be due to invalid IL or missing references)
            //IL_1b31: Expected O, but got Unknown
            //IL_1c5f: Unknown result type (might be due to invalid IL or missing references)
            //IL_1d93: Unknown result type (might be due to invalid IL or missing references)
            //IL_1d9d: Expected O, but got Unknown
            //IL_1e08: Unknown result type (might be due to invalid IL or missing references)
            //IL_1e12: Expected O, but got Unknown
            //IL_1e3b: Unknown result type (might be due to invalid IL or missing references)
            //IL_1e45: Expected O, but got Unknown
            //IL_1eba: Unknown result type (might be due to invalid IL or missing references)
            //IL_1ec4: Expected O, but got Unknown
            //IL_1f18: Unknown result type (might be due to invalid IL or missing references)
            //IL_1f22: Expected O, but got Unknown
            //IL_1f4b: Unknown result type (might be due to invalid IL or missing references)
            //IL_1f55: Expected O, but got Unknown
            //IL_1fe4: Unknown result type (might be due to invalid IL or missing references)
            //IL_1fee: Expected O, but got Unknown
            //IL_213b: Unknown result type (might be due to invalid IL or missing references)
            //IL_2145: Expected O, but got Unknown
            //IL_22b9: Unknown result type (might be due to invalid IL or missing references)
            //IL_22c3: Expected O, but got Unknown
            //IL_2334: Unknown result type (might be due to invalid IL or missing references)
            //IL_233e: Expected O, but got Unknown
            //IL_23ef: Unknown result type (might be due to invalid IL or missing references)
            //IL_23f9: Expected O, but got Unknown
            //IL_24b7: Unknown result type (might be due to invalid IL or missing references)
            //IL_24c1: Expected O, but got Unknown
            //IL_257d: Unknown result type (might be due to invalid IL or missing references)
            //IL_2587: Expected O, but got Unknown
            //IL_2690: Unknown result type (might be due to invalid IL or missing references)
            //IL_269a: Expected O, but got Unknown
            //IL_28a7: Unknown result type (might be due to invalid IL or missing references)
            //IL_28b1: Expected O, but got Unknown
            //IL_2927: Unknown result type (might be due to invalid IL or missing references)
            //IL_2931: Expected O, but got Unknown
            //IL_2b26: Unknown result type (might be due to invalid IL or missing references)
            //IL_2b30: Expected O, but got Unknown
            //IL_2ba7: Unknown result type (might be due to invalid IL or missing references)
            //IL_2bb1: Expected O, but got Unknown
            //IL_2e38: Unknown result type (might be due to invalid IL or missing references)
            //IL_2eac: Unknown result type (might be due to invalid IL or missing references)
            //IL_2eb6: Expected O, but got Unknown
            //IL_307e: Unknown result type (might be due to invalid IL or missing references)
            //IL_3088: Expected O, but got Unknown
            //IL_31b7: Unknown result type (might be due to invalid IL or missing references)
            //IL_31c1: Expected O, but got Unknown
            //IL_3234: Unknown result type (might be due to invalid IL or missing references)
            //IL_323e: Expected O, but got Unknown
            //IL_41dd: Unknown result type (might be due to invalid IL or missing references)
            //IL_4498: Unknown result type (might be due to invalid IL or missing references)
            //IL_44a2: Expected O, but got Unknown
            //IL_4589: Unknown result type (might be due to invalid IL or missing references)
            //IL_4593: Expected O, but got Unknown
            //IL_47c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_4de3: Unknown result type (might be due to invalid IL or missing references)
            //IL_4ded: Expected O, but got Unknown
            //IL_519e: Unknown result type (might be due to invalid IL or missing references)
            //IL_51a8: Expected O, but got Unknown
            //IL_5223: Unknown result type (might be due to invalid IL or missing references)
            //IL_522d: Expected O, but got Unknown
            //IL_5581: Unknown result type (might be due to invalid IL or missing references)
            //IL_56cd: Unknown result type (might be due to invalid IL or missing references)
            //IL_56d7: Expected O, but got Unknown
            //IL_59ab: Unknown result type (might be due to invalid IL or missing references)
            //IL_59b5: Expected O, but got Unknown
            //IL_5a30: Unknown result type (might be due to invalid IL or missing references)
            //IL_5a3a: Expected O, but got Unknown
            //IL_5b4f: Unknown result type (might be due to invalid IL or missing references)
            //IL_5bbd: Unknown result type (might be due to invalid IL or missing references)
            //IL_5bc7: Expected O, but got Unknown
            //IL_5d0b: Unknown result type (might be due to invalid IL or missing references)
            //IL_5d15: Expected O, but got Unknown
            //IL_5d9c: Unknown result type (might be due to invalid IL or missing references)
            //IL_5da6: Expected O, but got Unknown
            //IL_5fd2: Unknown result type (might be due to invalid IL or missing references)
            //IL_6894: Unknown result type (might be due to invalid IL or missing references)
            //IL_689e: Expected O, but got Unknown
            //IL_69e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_6afc: Unknown result type (might be due to invalid IL or missing references)
            //IL_6b88: Unknown result type (might be due to invalid IL or missing references)
            //IL_6c3c: Unknown result type (might be due to invalid IL or missing references)
            //IL_6d69: Unknown result type (might be due to invalid IL or missing references)
            //IL_6e78: Unknown result type (might be due to invalid IL or missing references)
            //IL_6e82: Expected O, but got Unknown
            //IL_6f20: Unknown result type (might be due to invalid IL or missing references)
            //IL_6f2a: Expected O, but got Unknown
            //IL_6fd4: Unknown result type (might be due to invalid IL or missing references)
            //IL_6fde: Expected O, but got Unknown
            //IL_7119: Unknown result type (might be due to invalid IL or missing references)
            //IL_717d: Unknown result type (might be due to invalid IL or missing references)
            //IL_71d5: Unknown result type (might be due to invalid IL or missing references)
            //IL_72db: Unknown result type (might be due to invalid IL or missing references)
            //IL_72ed: Unknown result type (might be due to invalid IL or missing references)
            //IL_72f7: Expected O, but got Unknown
            components = new Container();
            DataGridViewCellStyle val = new DataGridViewCellStyle();
            DataGridViewCellStyle val2 = new DataGridViewCellStyle();
            DataGridViewCellStyle val3 = new DataGridViewCellStyle();
            DataGridViewCellStyle val4 = new DataGridViewCellStyle();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Window));
            mapsStrip = new ContextMenuStrip(components);
            physicsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            unloadToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            finiteModeToolStripMenuItem = new ToolStripMenuItem();
            animalAIToolStripMenuItem = new ToolStripMenuItem();
            edgeWaterToolStripMenuItem = new ToolStripMenuItem();
            growingGrassToolStripMenuItem = new ToolStripMenuItem();
            survivalDeathToolStripMenuItem = new ToolStripMenuItem();
            killerBlocksToolStripMenuItem = new ToolStripMenuItem();
            rPChatToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            playerStrip = new ContextMenuStrip(components);
            whoisToolStripMenuItem = new ToolStripMenuItem();
            kickToolStripMenuItem = new ToolStripMenuItem();
            banToolStripMenuItem = new ToolStripMenuItem();
            voiceToolStripMenuItem = new ToolStripMenuItem();
            zombieSurvivalTab = new TabPage();
            label5 = new Label();
            zombieSettings = new Button();
            infectionMapsGrid = new DataGridViewEnumerated();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            tmrRestart = new Timer(components);
            iconContext = new ContextMenuStrip(components);
            openConsole = new ToolStripMenuItem();
            hideConsole = new ToolStripMenuItem();
            shutdownServer = new ToolStripMenuItem();
            BottomToolStripPanel = new ToolStripPanel();
            TopToolStripPanel = new ToolStripPanel();
            RightToolStripPanel = new ToolStripPanel();
            LeftToolStripPanel = new ToolStripPanel();
            ContentPanel = new ToolStripContentPanel();
            toolStripContainer1 = new ToolStripContainer();
            mainTabs = new TabControl();
            mainTab = new TabPage();
            splitContainer5 = new SplitContainer();
            txtInput = new TextBox();
            label1 = new Label();
            txtCommands = new TextBox();
            label2 = new Label();
            label17 = new Label();
            mode = new ComboBox();
            txtUrl = new TextBox();
            splitContainer3 = new SplitContainer();
            splitContainer4 = new SplitContainer();
            gBChat = new GroupBox();
            txtLog = new TextBox();
            gBCommands = new GroupBox();
            txtCommandsUsed = new TextBox();
            splitContainer2 = new SplitContainer();
            listViewPlayers = new CustomListView();
            PlayersColumnName = new ColumnHeader();
            PlayersColumnMap = new ColumnHeader();
            PlayersColumnAfk = new ColumnHeader();
            label3 = new Label();
            pCount = new Label();
            listViewMaps = new CustomListView();
            mapColumnName = new ColumnHeader();
            mapColumnPhysics = new ColumnHeader();
            mapColumnPlayers = new ColumnHeader();
            mapColumnWeight = new ColumnHeader();
            mCount = new Label();
            label4 = new Label();
            chatTab = new TabPage();
            chatWarningLabel = new Label();
            pictureBox1 = new PictureBox();
            chatPlayerList = new ListBox();
            chatMainBox = new RichTextBox();
            chatOnOff_btn = new Button();
            chatInputBox = new TextBox();
            cBlack = new Label();
            cWhite = new Label();
            cDarkBlue = new Label();
            cYellow = new Label();
            cDarkGreen = new Label();
            cPink = new Label();
            cTeal = new Label();
            cRed = new Label();
            cDarkRed = new Label();
            cAqua = new Label();
            cPurple = new Label();
            cBrightGreen = new Label();
            cGold = new Label();
            cBlue = new Label();
            cGray = new Label();
            cDarkGray = new Label();
            label25 = new Label();
            tabPagePlugins = new TabPage();
            pnlPlugin = new Panel();
            groupBox4 = new GroupBox();
            lblPluginDesc = new Label();
            lblPluginAuthor = new Label();
            lblPluginVersion = new Label();
            lblPluginName = new Label();
            treeView1 = new TreeView();
            playersTab = new TabPage();
            playerColorCombo = new ComboBox();
            targetMapCombo = new ComboBox();
            button15 = new Button();
            button14 = new Button();
            banCheck = new CheckBox();
            kickCheck = new CheckBox();
            playersListView = new ListView();
            PlName = new ColumnHeader();
            PlRank = new ColumnHeader();
            PlMap = new ColumnHeader();
            titleText = new TextBox();
            banText = new TextBox();
            kickText = new TextBox();
            label12 = new Label();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            btnMute = new Button();
            button3 = new Button();
            button2 = new Button();
            label10 = new Label();
            label7 = new Label();
            playersGrid = new PropertyGrid();
            button4 = new Button();
            xbanCheck = new CheckBox();
            xbanText = new TextBox();
            mapsTab = new TabPage();
            mapsList = new ListBox();
            button13 = new Button();
            label11 = new Label();
            unloadedMapsList = new ListBox();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            btnCreateMap = new Button();
            label9 = new Label();
            label8 = new Label();
            allMapsGrid = new PropertyGrid();
            lavaTab = new TabPage();
            label6 = new Label();
            mapsGrid = new DataGridViewEnumerated();
            mapName = new DataGridViewTextBoxColumn();
            sourceX = new DataGridViewTextBoxColumn();
            sourceY = new DataGridViewTextBoxColumn();
            sourceZ = new DataGridViewTextBoxColumn();
            phase1 = new DataGridViewTextBoxColumn();
            phase2 = new DataGridViewTextBoxColumn();
            typeOfLava = new DataGridViewTextBoxColumn();
            systemTab = new TabPage();
            groupBox1 = new GroupBox();
            button9 = new Button();
            checkBox1 = new CheckBox();
            label20 = new Label();
            textBox1 = new TextBox();
            checkBox2 = new CheckBox();
            button18 = new Button();
            groupBox2 = new GroupBox();
            accountsList = new CustomListView();
            remoteAccount = new ColumnHeader();
            newAccountBtn = new Button();
            removeAccountBtn = new Button();
            changeAccountBtn = new Button();
            groupBox3 = new GroupBox();
            linkLabel1 = new LinkLabel();
            textBox4 = new TextBox();
            label22 = new Label();
            changelogTab = new TabPage();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            txtChangelog = new TextBox();
            tabPage2 = new TabPage();
            txtSystem = new TextBox();
            errorsTab = new TabPage();
            txtErrors = new TextBox();
            minimizeButton = new Button();
            btnProperties = new Button();
            fontDialog1 = new FontDialog();
            toolTip1 = new ToolTip(components);
            button5 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelUptime = new ToolStripStatusLabel();
            toolStripStatusLabelRoundTime = new ToolStripStatusLabel();
            toolStripStatusLabelLagometer = new ToolStripStatusLabel();
            mapsStrip.SuspendLayout();
            playerStrip.SuspendLayout();
            zombieSurvivalTab.SuspendLayout();
            ((ISupportInitialize)infectionMapsGrid).BeginInit();
            iconContext.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            mainTabs.SuspendLayout();
            mainTab.SuspendLayout();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            gBChat.SuspendLayout();
            gBCommands.SuspendLayout();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            chatTab.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            tabPagePlugins.SuspendLayout();
            groupBox4.SuspendLayout();
            playersTab.SuspendLayout();
            mapsTab.SuspendLayout();
            lavaTab.SuspendLayout();
            ((ISupportInitialize)mapsGrid).BeginInit();
            systemTab.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            changelogTab.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            errorsTab.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            mapsStrip.Items.AddRange(new ToolStripItem[4]
            {
                physicsToolStripMenuItem, unloadToolStripMenuItem, settingsToolStripMenuItem, saveToolStripMenuItem
            });
            mapsStrip.Name = "mapsStrip";
            mapsStrip.Size = new Size(122, 92);
            mapsStrip.Opening += mapsStrip_Opening;
            physicsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
            {
                toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6
            });
            physicsToolStripMenuItem.Name = "physicsToolStripMenuItem";
            physicsToolStripMenuItem.Size = new Size(121, 22);
            physicsToolStripMenuItem.Text = "Physics";
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(104, 22);
            toolStripMenuItem2.Text = "off";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(104, 22);
            toolStripMenuItem3.Text = "1";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(104, 22);
            toolStripMenuItem4.Text = "2";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(104, 22);
            toolStripMenuItem5.Text = "3";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(104, 22);
            toolStripMenuItem6.Text = "door";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            unloadToolStripMenuItem.Size = new Size(121, 22);
            unloadToolStripMenuItem.Text = "Unload";
            unloadToolStripMenuItem.Click += unloadToolStripMenuItem_Click;
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
            {
                finiteModeToolStripMenuItem, animalAIToolStripMenuItem, edgeWaterToolStripMenuItem, growingGrassToolStripMenuItem, survivalDeathToolStripMenuItem,
                killerBlocksToolStripMenuItem, rPChatToolStripMenuItem
            });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(121, 22);
            settingsToolStripMenuItem.Text = "Settings";
            finiteModeToolStripMenuItem.Name = "finiteModeToolStripMenuItem";
            finiteModeToolStripMenuItem.Size = new Size(157, 22);
            finiteModeToolStripMenuItem.Text = "Finite Mode";
            finiteModeToolStripMenuItem.Click += finiteModeToolStripMenuItem_Click;
            animalAIToolStripMenuItem.Name = "animalAIToolStripMenuItem";
            animalAIToolStripMenuItem.Size = new Size(157, 22);
            animalAIToolStripMenuItem.Text = "Animal AI";
            animalAIToolStripMenuItem.Click += animalAIToolStripMenuItem_Click;
            edgeWaterToolStripMenuItem.Name = "edgeWaterToolStripMenuItem";
            edgeWaterToolStripMenuItem.Size = new Size(157, 22);
            edgeWaterToolStripMenuItem.Text = "Edge Water";
            edgeWaterToolStripMenuItem.Click += edgeWaterToolStripMenuItem_Click;
            growingGrassToolStripMenuItem.Name = "growingGrassToolStripMenuItem";
            growingGrassToolStripMenuItem.Size = new Size(157, 22);
            growingGrassToolStripMenuItem.Text = "Grass Growing";
            growingGrassToolStripMenuItem.Click += growingGrassToolStripMenuItem_Click;
            survivalDeathToolStripMenuItem.Name = "survivalDeathToolStripMenuItem";
            survivalDeathToolStripMenuItem.Size = new Size(157, 22);
            survivalDeathToolStripMenuItem.Text = "Survival Death";
            survivalDeathToolStripMenuItem.Click += survivalDeathToolStripMenuItem_Click;
            killerBlocksToolStripMenuItem.Name = "killerBlocksToolStripMenuItem";
            killerBlocksToolStripMenuItem.Size = new Size(157, 22);
            killerBlocksToolStripMenuItem.Text = "Killer Blocks";
            killerBlocksToolStripMenuItem.Click += killerBlocksToolStripMenuItem_Click;
            rPChatToolStripMenuItem.Name = "rPChatToolStripMenuItem";
            rPChatToolStripMenuItem.Size = new Size(157, 22);
            rPChatToolStripMenuItem.Text = "RP Chat";
            rPChatToolStripMenuItem.Click += rPChatToolStripMenuItem_Click;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(121, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            playerStrip.Items.AddRange(new ToolStripItem[4]
            {
                whoisToolStripMenuItem, kickToolStripMenuItem, banToolStripMenuItem, voiceToolStripMenuItem
            });
            playerStrip.Name = "playerStrip";
            playerStrip.Size = new Size(113, 92);
            playerStrip.Opening += playerStrip_Opening;
            whoisToolStripMenuItem.Name = "whoisToolStripMenuItem";
            whoisToolStripMenuItem.Size = new Size(112, 22);
            whoisToolStripMenuItem.Text = "whois";
            whoisToolStripMenuItem.Click += whoisToolStripMenuItem_Click;
            kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            kickToolStripMenuItem.Size = new Size(112, 22);
            kickToolStripMenuItem.Text = "kick";
            kickToolStripMenuItem.Click += kickToolStripMenuItem_Click;
            banToolStripMenuItem.Name = "banToolStripMenuItem";
            banToolStripMenuItem.Size = new Size(112, 22);
            banToolStripMenuItem.Text = "ban";
            banToolStripMenuItem.Click += banToolStripMenuItem_Click;
            voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            voiceToolStripMenuItem.Size = new Size(112, 22);
            voiceToolStripMenuItem.Text = "voice";
            voiceToolStripMenuItem.Click += voiceToolStripMenuItem_Click;
            zombieSurvivalTab.BackColor = Color.Transparent;
            zombieSurvivalTab.Controls.Add(label5);
            zombieSurvivalTab.Controls.Add(zombieSettings);
            zombieSurvivalTab.Controls.Add(infectionMapsGrid);
            zombieSurvivalTab.Location = new Point(4, 22);
            zombieSurvivalTab.Name = "zombieSurvivalTab";
            zombieSurvivalTab.Padding = new Padding(3);
            zombieSurvivalTab.Size = new Size(702, 488);
            zombieSurvivalTab.TabIndex = 7;
            zombieSurvivalTab.Text = "Zombie Survival";
            label5.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.Location = new Point(42, 21);
            label5.Name = "label5";
            label5.Size = new Size(96, 19);
            label5.TabIndex = 6;
            label5.Text = "Infection maps:";
            zombieSettings.Location = new Point(523, 12);
            zombieSettings.Name = "zombieSettings";
            zombieSettings.Size = new Size(106, 23);
            zombieSettings.TabIndex = 0;
            zombieSettings.Text = "Zombie Settings";
            zombieSettings.UseVisualStyleBackColor = true;
            zombieSettings.Click += zombieSettings_Click;
            infectionMapsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            infectionMapsGrid.BackgroundColor = SystemColors.Control;
            infectionMapsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            infectionMapsGrid.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4,
                                               dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7);
            infectionMapsGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            infectionMapsGrid.Location = new Point(43, 49);
            infectionMapsGrid.Name = "infectionMapsGrid";
            val.Alignment = DataGridViewContentAlignment.MiddleCenter;
            val.BackColor = SystemColors.Control;
            val.Font = new Font("Calibri", 8.25f);
            val.ForeColor = SystemColors.WindowText;
            val.SelectionBackColor = SystemColors.Highlight;
            val.SelectionForeColor = SystemColors.HighlightText;
            val.WrapMode = DataGridViewTriState.True;
            infectionMapsGrid.RowHeadersDefaultCellStyle = val;
            val2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            infectionMapsGrid.RowsDefaultCellStyle = val2;
            infectionMapsGrid.SelectionMode = 0;
            infectionMapsGrid.Size = new Size(616, 406);
            infectionMapsGrid.TabIndex = 1;
            dataGridViewTextBoxColumn1.FillWeight = 135.3597f;
            dataGridViewTextBoxColumn1.HeaderText = "Map Name";
            dataGridViewTextBoxColumn1.MinimumWidth = 40;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn2.FillWeight = 88.54781f;
            dataGridViewTextBoxColumn2.HeaderText = "x";
            dataGridViewTextBoxColumn2.MinimumWidth = 15;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn3.FillWeight = 88.54781f;
            dataGridViewTextBoxColumn3.HeaderText = "y";
            dataGridViewTextBoxColumn3.MinimumWidth = 15;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn4.FillWeight = 88.54781f;
            dataGridViewTextBoxColumn4.HeaderText = "z";
            dataGridViewTextBoxColumn4.MinimumWidth = 15;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn5.FillWeight = 103.8501f;
            dataGridViewTextBoxColumn5.HeaderText = "Phase I";
            dataGridViewTextBoxColumn5.MinimumWidth = 30;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn6.FillWeight = 106.599f;
            dataGridViewTextBoxColumn6.HeaderText = "Phase II";
            dataGridViewTextBoxColumn6.MinimumWidth = 30;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn7.FillWeight = 88.54781f;
            dataGridViewTextBoxColumn7.HeaderText = "Block";
            dataGridViewTextBoxColumn7.MinimumWidth = 30;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            tmrRestart.Enabled = true;
            tmrRestart.Interval = 1000;
            iconContext.Items.AddRange(new ToolStripItem[3]
            {
                openConsole, hideConsole, shutdownServer
            });
            iconContext.Name = "iconContext";
            iconContext.Size = new Size(169, 70);
            openConsole.Name = "openConsole";
            openConsole.Size = new Size(168, 22);
            openConsole.Text = "Open Console";
            openConsole.Click += openConsole_Click;
            hideConsole.Name = "hideConsole";
            hideConsole.Size = new Size(168, 22);
            hideConsole.Text = "Hide Console";
            hideConsole.Click += hideConsole_Click;
            shutdownServer.Name = "shutdownServer";
            shutdownServer.Size = new Size(168, 22);
            shutdownServer.Text = "Shutdown Server";
            shutdownServer.Click += shutdownServer_Click;
            BottomToolStripPanel.Location = new Point(0, 0);
            BottomToolStripPanel.Name = "BottomToolStripPanel";
            BottomToolStripPanel.Orientation = 0;
            BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            BottomToolStripPanel.Size = new Size(0, 0);
            TopToolStripPanel.Location = new Point(0, 0);
            TopToolStripPanel.Name = "TopToolStripPanel";
            TopToolStripPanel.Orientation = 0;
            TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            TopToolStripPanel.Size = new Size(0, 0);
            RightToolStripPanel.Location = new Point(0, 0);
            RightToolStripPanel.Name = "RightToolStripPanel";
            RightToolStripPanel.Orientation = 0;
            RightToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            RightToolStripPanel.Size = new Size(0, 0);
            LeftToolStripPanel.Location = new Point(0, 0);
            LeftToolStripPanel.Name = "LeftToolStripPanel";
            LeftToolStripPanel.Orientation = 0;
            LeftToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            LeftToolStripPanel.Size = new Size(0, 0);
            ContentPanel.AutoScroll = true;
            ContentPanel.Size = new Size(709, 514);
            toolStripContainer1.BottomToolStripPanelVisible = false;
            toolStripContainer1.ContentPanel.Size = new Size(709, 514);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.LeftToolStripPanel.Padding = new Padding(0, 0, 25, 0);
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.RightToolStripPanelVisible = false;
            toolStripContainer1.Size = new Size(709, 514);
            toolStripContainer1.TabIndex = 52;
            toolStripContainer1.Text = "toolStripContainer1";
            toolStripContainer1.TopToolStripPanel.Padding = new Padding(0, 0, 25, 25);
            toolStripContainer1.TopToolStripPanelVisible = false;
            mainTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabs.Controls.Add(mainTab);
            mainTabs.Controls.Add(chatTab);
            mainTabs.Controls.Add(tabPagePlugins);
            mainTabs.Controls.Add(playersTab);
            mainTabs.Controls.Add(mapsTab);
            mainTabs.Controls.Add(lavaTab);
            mainTabs.Controls.Add(systemTab);
            mainTabs.Controls.Add(changelogTab);
            mainTabs.Controls.Add(errorsTab);
            mainTabs.Cursor = Cursors.Default;
            mainTabs.Font = new Font("Calibri", 8.25f);
            mainTabs.Location = new Point(2, 13);
            mainTabs.MinimumSize = new Size(710, 512);
            mainTabs.Name = "mainTabs";
            mainTabs.SelectedIndex = 0;
            mainTabs.Size = new Size(728, 512);
            mainTabs.TabIndex = 2;
            mainTabs.Click += tabControl1_Click;
            mainTab.BackColor = Color.Transparent;
            mainTab.Controls.Add(splitContainer5);
            mainTab.Controls.Add(label17);
            mainTab.Controls.Add(mode);
            mainTab.Controls.Add(txtUrl);
            mainTab.Controls.Add(splitContainer3);
            mainTab.Location = new Point(4, 22);
            mainTab.Name = "mainTab";
            mainTab.Padding = new Padding(3);
            mainTab.Size = new Size(720, 486);
            mainTab.TabIndex = 0;
            mainTab.Text = "Main";
            splitContainer5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer5.Location = new Point(13, 442);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Panel1.Controls.Add(txtInput);
            splitContainer5.Panel1.Controls.Add(label1);
            splitContainer5.Panel2.Controls.Add(txtCommands);
            splitContainer5.Panel2.Controls.Add(label2);
            splitContainer5.Size = new Size(688, 34);
            splitContainer5.SplitterDistance = 451;
            splitContainer5.TabIndex = 47;
            txtInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtInput.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInput.Location = new Point(53, 7);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(395, 21);
            txtInput.TabIndex = 27;
            txtInput.TextChanged += txtInput_TextChanged;
            txtInput.KeyDown += txtInput_KeyDown;
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 9f, FontStyle.Bold);
            label1.Location = new Point(15, 10);
            label1.Name = "label1";
            label1.Size = new Size(32, 14);
            label1.TabIndex = 26;
            label1.Text = "Chat:";
            txtCommands.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCommands.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCommands.Location = new Point(72, 8);
            txtCommands.Name = "txtCommands";
            txtCommands.Size = new Size(158, 21);
            txtCommands.TabIndex = 28;
            txtCommands.KeyDown += txtCommands_KeyDown;
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 9f, FontStyle.Bold);
            label2.Location = new Point(6, 10);
            label2.Name = "label2";
            label2.Size = new Size(60, 14);
            label2.TabIndex = 29;
            label2.Text = "Command:";
            label2.Click += label2_Click;
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label17.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label17.Location = new Point(481, 13);
            label17.Name = "label17";
            label17.Size = new Size(78, 18);
            label17.TabIndex = 40;
            label17.Text = "Game mode:";
            mode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mode.DropDownStyle = ComboBoxStyle.DropDownList;
            mode.FormattingEnabled = true;
            mode.Items.AddRange(new object[4]
            {
                "Lava Survival", "Lava/Freebuild", "Freebuild", "Zombie(Beta)"
            });
            mode.Location = new Point(565, 10);
            mode.Name = "mode";
            mode.Size = new Size(136, 21);
            mode.TabIndex = 39;
            mode.SelectedIndexChanged += mode_SelectedIndexChanged;
            txtUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Cursor = Cursors.Default;
            txtUrl.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUrl.Location = new Point(13, 7);
            txtUrl.Name = "txtUrl";
            txtUrl.ReadOnly = true;
            txtUrl.Size = new Size(446, 21);
            txtUrl.TabIndex = 25;
            splitContainer3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer3.Location = new Point(13, 34);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Panel1.Controls.Add(splitContainer4);
            splitContainer3.Panel2.Controls.Add(splitContainer2);
            splitContainer3.Size = new Size(688, 406);
            splitContainer3.SplitterDistance = 406;
            splitContainer3.TabIndex = 46;
            splitContainer4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer4.Location = new Point(3, 6);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = 0;
            splitContainer4.Panel1.Controls.Add(gBChat);
            splitContainer4.Panel2.Controls.Add(gBCommands);
            splitContainer4.Panel2.Paint += splitContainer4_Panel2_Paint;
            splitContainer4.Size = new Size(395, 396);
            splitContainer4.SplitterDistance = 237;
            splitContainer4.TabIndex = 35;
            gBChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gBChat.Controls.Add(txtLog);
            gBChat.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            gBChat.Location = new Point(3, 8);
            gBChat.Name = "gBChat";
            gBChat.Size = new Size(389, 219);
            gBChat.TabIndex = 32;
            gBChat.TabStop = false;
            gBChat.Text = "Chat";
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.BackColor = SystemColors.Window;
            txtLog.BorderStyle = 0;
            txtLog.Cursor = Cursors.Default;
            txtLog.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.Location = new Point(6, 19);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(383, 200);
            txtLog.TabIndex = 1;
            txtLog.TextChanged += txtLog_TextChanged;
            gBCommands.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gBCommands.Controls.Add(txtCommandsUsed);
            gBCommands.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            gBCommands.Location = new Point(3, 3);
            gBCommands.Name = "gBCommands";
            gBCommands.Size = new Size(389, 149);
            gBCommands.TabIndex = 34;
            gBCommands.TabStop = false;
            gBCommands.Text = "Commands";
            gBCommands.Enter += gBCommands_Enter;
            txtCommandsUsed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCommandsUsed.BackColor = Color.White;
            txtCommandsUsed.Cursor = Cursors.Default;
            txtCommandsUsed.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCommandsUsed.Location = new Point(9, 21);
            txtCommandsUsed.Multiline = true;
            txtCommandsUsed.Name = "txtCommandsUsed";
            txtCommandsUsed.ReadOnly = true;
            txtCommandsUsed.ScrollBars = ScrollBars.Vertical;
            txtCommandsUsed.Size = new Size(380, 125);
            txtCommandsUsed.TabIndex = 0;
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = 0;
            splitContainer2.Panel1.Controls.Add(listViewPlayers);
            splitContainer2.Panel1.Controls.Add(label3);
            splitContainer2.Panel1.Controls.Add(pCount);
            splitContainer2.Panel1.Paint += splitContainer2_Panel1_Paint;
            splitContainer2.Panel2.Controls.Add(listViewMaps);
            splitContainer2.Panel2.Controls.Add(mCount);
            splitContainer2.Panel2.Controls.Add(label4);
            splitContainer2.Size = new Size(272, 399);
            splitContainer2.SplitterDistance = 213;
            splitContainer2.SplitterWidth = 8;
            splitContainer2.TabIndex = 45;
            listViewPlayers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewPlayers.Columns.AddRange(new ColumnHeader[3]
            {
                PlayersColumnName, PlayersColumnMap, PlayersColumnAfk
            });
            listViewPlayers.ContextMenuStrip = playerStrip;
            listViewPlayers.FullRowSelect = true;
            listViewPlayers.Location = new Point(7, 23);
            listViewPlayers.MultiSelect = false;
            listViewPlayers.Name = "listViewPlayers";
            listViewPlayers.Size = new Size(262, 176);
            listViewPlayers.Sorting = SortOrder.Ascending;
            listViewPlayers.TabIndex = 44;
            listViewPlayers.UseCompatibleStateImageBehavior = false;
            listViewPlayers.View = View.Details;
            listViewPlayers.SelectedIndexChanged += listViewPlayers_SelectedIndexChanged;
            PlayersColumnName.Text = "Name";
            PlayersColumnName.Width = 101;
            PlayersColumnMap.Text = "Map";
            PlayersColumnMap.Width = 92;
            PlayersColumnAfk.Text = "Afk";
            PlayersColumnAfk.TextAlign = HorizontalAlignment.Center;
            PlayersColumnAfk.Width = 51;
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(4, 6);
            label3.Name = "label3";
            label3.Size = new Size(46, 14);
            label3.TabIndex = 41;
            label3.Text = "Players";
            pCount.AutoSize = true;
            pCount.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            pCount.Location = new Point(107, 6);
            pCount.Name = "pCount";
            pCount.Size = new Size(13, 14);
            pCount.TabIndex = 43;
            pCount.Text = "0";
            listViewMaps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewMaps.Columns.AddRange(new ColumnHeader[4]
            {
                mapColumnName, mapColumnPhysics, mapColumnPlayers, mapColumnWeight
            });
            listViewMaps.ContextMenuStrip = mapsStrip;
            listViewMaps.FullRowSelect = true;
            listViewMaps.Location = new Point(7, 18);
            listViewMaps.MultiSelect = false;
            listViewMaps.Name = "listViewMaps";
            listViewMaps.Size = new Size(262, 148);
            listViewMaps.Sorting = SortOrder.Ascending;
            listViewMaps.TabIndex = 45;
            listViewMaps.UseCompatibleStateImageBehavior = false;
            listViewMaps.View = View.Details;
            mapColumnName.Text = "Name";
            mapColumnName.Width = 84;
            mapColumnPhysics.Text = "Physics";
            mapColumnPhysics.TextAlign = HorizontalAlignment.Right;
            mapColumnPhysics.Width = 47;
            mapColumnPlayers.Text = "Players";
            mapColumnPlayers.TextAlign = HorizontalAlignment.Center;
            mapColumnPlayers.Width = 47;
            mapColumnWeight.Text = "Weight";
            mapColumnWeight.TextAlign = HorizontalAlignment.Center;
            mapColumnWeight.Width = 67;
            mCount.AutoSize = true;
            mCount.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mCount.Location = new Point(107, 2);
            mCount.Name = "mCount";
            mCount.Size = new Size(13, 14);
            mCount.TabIndex = 44;
            mCount.Text = "0";
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(4, 2);
            label4.Name = "label4";
            label4.Size = new Size(37, 14);
            label4.TabIndex = 42;
            label4.Text = "Maps";
            chatTab.BackColor = Color.Transparent;
            chatTab.Controls.Add(chatWarningLabel);
            chatTab.Controls.Add(pictureBox1);
            chatTab.Controls.Add(chatPlayerList);
            chatTab.Controls.Add(chatMainBox);
            chatTab.Controls.Add(chatOnOff_btn);
            chatTab.Controls.Add(chatInputBox);
            chatTab.Controls.Add(cBlack);
            chatTab.Controls.Add(cWhite);
            chatTab.Controls.Add(cDarkBlue);
            chatTab.Controls.Add(cYellow);
            chatTab.Controls.Add(cDarkGreen);
            chatTab.Controls.Add(cPink);
            chatTab.Controls.Add(cTeal);
            chatTab.Controls.Add(cRed);
            chatTab.Controls.Add(cDarkRed);
            chatTab.Controls.Add(cAqua);
            chatTab.Controls.Add(cPurple);
            chatTab.Controls.Add(cBrightGreen);
            chatTab.Controls.Add(cGold);
            chatTab.Controls.Add(cBlue);
            chatTab.Controls.Add(cGray);
            chatTab.Controls.Add(cDarkGray);
            chatTab.Controls.Add(label25);
            chatTab.Location = new Point(4, 22);
            chatTab.Name = "chatTab";
            chatTab.Padding = new Padding(3);
            chatTab.Size = new Size(720, 486);
            chatTab.TabIndex = 7;
            chatTab.Text = "Chat";
            chatTab.Click += chatTab_Click_1;
            chatWarningLabel.AutoSize = true;
            chatWarningLabel.Font = new Font("Calibri", 10f, FontStyle.Bold);
            chatWarningLabel.ForeColor = Color.Red;
            chatWarningLabel.Location = new Point(123, 16);
            chatWarningLabel.Name = "chatWarningLabel";
            chatWarningLabel.Size = new Size(574, 17);
            chatWarningLabel.TabIndex = 64;
            chatWarningLabel.Text = "You have to update MCDzienny.exe file manually in order to make the color chat work properly!";
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Resources.sprocket_dark;
            pictureBox1.Location = new Point(147, 354);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(16, 16);
            pictureBox1.TabIndex = 63;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            chatPlayerList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            chatPlayerList.FormattingEnabled = true;
            chatPlayerList.Location = new Point(581, 43);
            chatPlayerList.Name = "chatPlayerList";
            chatPlayerList.Size = new Size(117, 420);
            chatPlayerList.Sorted = true;
            chatPlayerList.TabIndex = 42;
            chatMainBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chatMainBox.BackColor = Color.FromArgb(100, 100, 100);
            chatMainBox.BorderStyle = 0;
            chatMainBox.Cursor = Cursors.Default;
            chatMainBox.Font = new Font("Calibri", 12f, FontStyle.Regular, GraphicsUnit.Point, 238);
            chatMainBox.Location = new Point(24, 45);
            chatMainBox.MaxLength = 10000;
            chatMainBox.Name = "chatMainBox";
            chatMainBox.ReadOnly = true;
            chatMainBox.Size = new Size(551, 296);
            chatMainBox.TabIndex = 40;
            chatMainBox.Text = Lang.Command.ChangePlayerExpName;
            chatOnOff_btn.BackColor = Color.Transparent;
            chatOnOff_btn.Location = new Point(24, 16);
            chatOnOff_btn.Name = "chatOnOff_btn";
            chatOnOff_btn.Size = new Size(75, 23);
            chatOnOff_btn.TabIndex = 62;
            chatOnOff_btn.Text = "Deactivated";
            chatOnOff_btn.UseVisualStyleBackColor = false;
            chatOnOff_btn.Click += chatOnOff_btn_Click;
            chatInputBox.AcceptsReturn = true;
            chatInputBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chatInputBox.Font = new Font("Calibri", 12f, FontStyle.Regular, GraphicsUnit.Point, 238);
            chatInputBox.Location = new Point(147, 374);
            chatInputBox.Multiline = true;
            chatInputBox.Name = "chatInputBox";
            chatInputBox.ScrollBars = ScrollBars.Vertical;
            chatInputBox.Size = new Size(426, 98);
            chatInputBox.TabIndex = 41;
            chatInputBox.KeyDown += chatInputBox_KeyDown;
            cBlack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cBlack.AutoSize = true;
            cBlack.BackColor = Color.Black;
            cBlack.Cursor = Cursors.Hand;
            cBlack.ForeColor = Color.Black;
            cBlack.Location = new Point(31, 362);
            cBlack.MinimumSize = new Size(20, 20);
            cBlack.Name = "cBlack";
            cBlack.Size = new Size(20, 20);
            cBlack.TabIndex = 45;
            cBlack.Tag = "%0";
            toolTip1.SetToolTip(cBlack, "%0");
            cBlack.Click += Color_Click;
            cWhite.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cWhite.AutoSize = true;
            cWhite.BackColor = Color.White;
            cWhite.Cursor = Cursors.Hand;
            cWhite.ForeColor = Color.DarkBlue;
            cWhite.Location = new Point(109, 362);
            cWhite.MinimumSize = new Size(20, 20);
            cWhite.Name = "cWhite";
            cWhite.Size = new Size(20, 20);
            cWhite.TabIndex = 60;
            cWhite.Tag = "%f";
            toolTip1.SetToolTip(cWhite, "%f");
            cWhite.Click += Color_Click;
            cDarkBlue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cDarkBlue.AutoSize = true;
            cDarkBlue.BackColor = Color.FromArgb(0, 0, 170);
            cDarkBlue.Cursor = Cursors.Hand;
            cDarkBlue.ForeColor = Color.DarkBlue;
            cDarkBlue.Location = new Point(31, 388);
            cDarkBlue.MinimumSize = new Size(20, 20);
            cDarkBlue.Name = "cDarkBlue";
            cDarkBlue.Size = new Size(20, 20);
            cDarkBlue.TabIndex = 46;
            cDarkBlue.Tag = "%1";
            toolTip1.SetToolTip(cDarkBlue, "%1");
            cDarkBlue.Click += Color_Click;
            cYellow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cYellow.AutoSize = true;
            cYellow.BackColor = Color.FromArgb(255, 255, 85);
            cYellow.Cursor = Cursors.Hand;
            cYellow.ForeColor = Color.DarkBlue;
            cYellow.Location = new Point(109, 414);
            cYellow.MinimumSize = new Size(20, 20);
            cYellow.Name = "cYellow";
            cYellow.Size = new Size(20, 20);
            cYellow.TabIndex = 59;
            cYellow.Tag = "%e";
            toolTip1.SetToolTip(cYellow, "%e");
            cYellow.Click += Color_Click;
            cDarkGreen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cDarkGreen.AutoSize = true;
            cDarkGreen.BackColor = Color.FromArgb(0, 170, 0);
            cDarkGreen.Cursor = Cursors.Hand;
            cDarkGreen.ForeColor = Color.DarkBlue;
            cDarkGreen.Location = new Point(31, 414);
            cDarkGreen.MinimumSize = new Size(20, 20);
            cDarkGreen.Name = "cDarkGreen";
            cDarkGreen.Size = new Size(20, 20);
            cDarkGreen.TabIndex = 47;
            cDarkGreen.Tag = "%2";
            toolTip1.SetToolTip(cDarkGreen, "%2");
            cDarkGreen.Click += Color_Click;
            cPink.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cPink.AutoSize = true;
            cPink.BackColor = Color.FromArgb(255, 85, 255);
            cPink.Cursor = Cursors.Hand;
            cPink.ForeColor = Color.DarkBlue;
            cPink.Location = new Point(109, 440);
            cPink.MinimumSize = new Size(20, 20);
            cPink.Name = "cPink";
            cPink.Size = new Size(20, 20);
            cPink.TabIndex = 58;
            cPink.Tag = "%d";
            toolTip1.SetToolTip(cPink, "%d");
            cPink.Click += Color_Click;
            cTeal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cTeal.AutoSize = true;
            cTeal.BackColor = Color.FromArgb(0, 170, 170);
            cTeal.Cursor = Cursors.Hand;
            cTeal.ForeColor = Color.DarkBlue;
            cTeal.Location = new Point(83, 388);
            cTeal.MinimumSize = new Size(20, 20);
            cTeal.Name = "cTeal";
            cTeal.Size = new Size(20, 20);
            cTeal.TabIndex = 48;
            cTeal.Tag = "%3";
            toolTip1.SetToolTip(cTeal, "%3");
            cTeal.Click += Color_Click;
            cRed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cRed.AutoSize = true;
            cRed.BackColor = Color.FromArgb(255, 85, 85);
            cRed.Cursor = Cursors.Hand;
            cRed.ForeColor = Color.DarkBlue;
            cRed.Location = new Point(57, 440);
            cRed.MinimumSize = new Size(20, 20);
            cRed.Name = "cRed";
            cRed.Size = new Size(20, 20);
            cRed.TabIndex = 57;
            cRed.Tag = "%c";
            toolTip1.SetToolTip(cRed, "%c");
            cRed.Click += Color_Click;
            cDarkRed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cDarkRed.AutoSize = true;
            cDarkRed.BackColor = Color.FromArgb(170, 0, 0);
            cDarkRed.Cursor = Cursors.Hand;
            cDarkRed.ForeColor = Color.DarkBlue;
            cDarkRed.Location = new Point(31, 440);
            cDarkRed.MinimumSize = new Size(20, 20);
            cDarkRed.Name = "cDarkRed";
            cDarkRed.Size = new Size(20, 20);
            cDarkRed.TabIndex = 49;
            cDarkRed.Tag = "%4";
            toolTip1.SetToolTip(cDarkRed, "%4");
            cDarkRed.Click += Color_Click;
            cAqua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cAqua.AutoSize = true;
            cAqua.BackColor = Color.FromArgb(85, 255, 255);
            cAqua.Cursor = Cursors.Hand;
            cAqua.ForeColor = Color.DarkBlue;
            cAqua.Location = new Point(109, 388);
            cAqua.MinimumSize = new Size(20, 20);
            cAqua.Name = "cAqua";
            cAqua.Size = new Size(20, 20);
            cAqua.TabIndex = 56;
            cAqua.Tag = "%b";
            toolTip1.SetToolTip(cAqua, "%b");
            cAqua.Click += Color_Click;
            cPurple.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cPurple.AutoSize = true;
            cPurple.BackColor = Color.FromArgb(170, 0, 170);
            cPurple.Cursor = Cursors.Hand;
            cPurple.ForeColor = Color.DarkBlue;
            cPurple.Location = new Point(83, 440);
            cPurple.MinimumSize = new Size(20, 20);
            cPurple.Name = "cPurple";
            cPurple.Size = new Size(20, 20);
            cPurple.TabIndex = 50;
            cPurple.Tag = "%5";
            toolTip1.SetToolTip(cPurple, "%5");
            cPurple.Click += Color_Click;
            cBrightGreen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cBrightGreen.AutoSize = true;
            cBrightGreen.BackColor = Color.FromArgb(85, 255, 85);
            cBrightGreen.Cursor = Cursors.Hand;
            cBrightGreen.ForeColor = Color.DarkBlue;
            cBrightGreen.Location = new Point(57, 414);
            cBrightGreen.MinimumSize = new Size(20, 20);
            cBrightGreen.Name = "cBrightGreen";
            cBrightGreen.Size = new Size(20, 20);
            cBrightGreen.TabIndex = 55;
            cBrightGreen.Tag = "%a";
            toolTip1.SetToolTip(cBrightGreen, "%a");
            cBrightGreen.Click += Color_Click;
            cGold.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cGold.AutoSize = true;
            cGold.BackColor = Color.Gold;
            cGold.Cursor = Cursors.Hand;
            cGold.ForeColor = Color.DarkBlue;
            cGold.Location = new Point(83, 414);
            cGold.MinimumSize = new Size(20, 20);
            cGold.Name = "cGold";
            cGold.Size = new Size(20, 20);
            cGold.TabIndex = 51;
            cGold.Tag = "%6";
            toolTip1.SetToolTip(cGold, "%6");
            cGold.Click += Color_Click;
            cBlue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cBlue.AutoSize = true;
            cBlue.BackColor = Color.FromArgb(85, 85, 255);
            cBlue.Cursor = Cursors.Hand;
            cBlue.ForeColor = Color.DarkBlue;
            cBlue.Location = new Point(57, 388);
            cBlue.MinimumSize = new Size(20, 20);
            cBlue.Name = "cBlue";
            cBlue.Size = new Size(20, 20);
            cBlue.TabIndex = 54;
            cBlue.Tag = "%9";
            toolTip1.SetToolTip(cBlue, "%9");
            cBlue.Click += Color_Click;
            cGray.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cGray.AutoSize = true;
            cGray.BackColor = Color.FromArgb(170, 170, 170);
            cGray.Cursor = Cursors.Hand;
            cGray.ForeColor = Color.DarkBlue;
            cGray.Location = new Point(83, 362);
            cGray.MinimumSize = new Size(20, 20);
            cGray.Name = "cGray";
            cGray.Size = new Size(20, 20);
            cGray.TabIndex = 52;
            cGray.Tag = "%7";
            toolTip1.SetToolTip(cGray, "%7");
            cGray.Click += Color_Click;
            cDarkGray.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cDarkGray.AutoSize = true;
            cDarkGray.BackColor = Color.FromArgb(85, 85, 85);
            cDarkGray.Cursor = Cursors.Hand;
            cDarkGray.ForeColor = Color.DarkBlue;
            cDarkGray.Location = new Point(57, 362);
            cDarkGray.MinimumSize = new Size(20, 20);
            cDarkGray.Name = "cDarkGray";
            cDarkGray.Size = new Size(20, 20);
            cDarkGray.TabIndex = 53;
            cDarkGray.Tag = "%8";
            toolTip1.SetToolTip(cDarkGray, "%8");
            cDarkGray.Click += Color_Click;
            label25.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label25.AutoSize = true;
            label25.BackColor = Color.Gray;
            label25.ForeColor = Color.DarkBlue;
            label25.Location = new Point(21, 350);
            label25.MinimumSize = new Size(120, 120);
            label25.Name = "label25";
            label25.Size = new Size(120, 120);
            label25.TabIndex = 61;
            tabPagePlugins.BackColor = SystemColors.Control;
            tabPagePlugins.Controls.Add(pnlPlugin);
            tabPagePlugins.Controls.Add(groupBox4);
            tabPagePlugins.Controls.Add(treeView1);
            tabPagePlugins.Location = new Point(4, 22);
            tabPagePlugins.Name = "tabPagePlugins";
            tabPagePlugins.Padding = new Padding(3);
            tabPagePlugins.Size = new Size(720, 486);
            tabPagePlugins.TabIndex = 10;
            tabPagePlugins.Text = "Plugins";
            pnlPlugin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPlugin.BorderStyle = BorderStyle.FixedSingle;
            pnlPlugin.Location = new Point(201, 6);
            pnlPlugin.Name = "pnlPlugin";
            pnlPlugin.Size = new Size(513, 474);
            pnlPlugin.TabIndex = 5;
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.Controls.Add(lblPluginDesc);
            groupBox4.Controls.Add(lblPluginAuthor);
            groupBox4.Controls.Add(lblPluginVersion);
            groupBox4.Controls.Add(lblPluginName);
            groupBox4.Location = new Point(7, 336);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(188, 144);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Plugin Information:";
            lblPluginDesc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPluginDesc.Location = new Point(6, 65);
            lblPluginDesc.Name = "lblPluginDesc";
            lblPluginDesc.Size = new Size(176, 64);
            lblPluginDesc.TabIndex = 4;
            lblPluginDesc.Text = "   Plugin Description Goes Here... Test One Two Three, This is a Test...";
            lblPluginAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPluginAuthor.Location = new Point(6, 49);
            lblPluginAuthor.Name = "lblPluginAuthor";
            lblPluginAuthor.Size = new Size(176, 16);
            lblPluginAuthor.TabIndex = 3;
            lblPluginAuthor.Text = "By: <Author's Name>";
            lblPluginVersion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPluginVersion.Location = new Point(6, 33);
            lblPluginVersion.Name = "lblPluginVersion";
            lblPluginVersion.Size = new Size(176, 16);
            lblPluginVersion.TabIndex = 2;
            lblPluginVersion.Text = "(<Version>)";
            lblPluginName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPluginName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPluginName.Location = new Point(6, 17);
            lblPluginName.Name = "lblPluginName";
            lblPluginName.Size = new Size(176, 16);
            lblPluginName.TabIndex = 1;
            lblPluginName.Text = "<Plugin Name Here>";
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView1.FullRowSelect = true;
            treeView1.Location = new Point(6, 6);
            treeView1.Name = "treeView1";
            treeView1.ShowLines = false;
            treeView1.ShowPlusMinus = false;
            treeView1.ShowRootLines = false;
            treeView1.Size = new Size(189, 323);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            playersTab.BackColor = Color.Transparent;
            playersTab.Controls.Add(playerColorCombo);
            playersTab.Controls.Add(targetMapCombo);
            playersTab.Controls.Add(button15);
            playersTab.Controls.Add(button14);
            playersTab.Controls.Add(banCheck);
            playersTab.Controls.Add(kickCheck);
            playersTab.Controls.Add(playersListView);
            playersTab.Controls.Add(titleText);
            playersTab.Controls.Add(banText);
            playersTab.Controls.Add(kickText);
            playersTab.Controls.Add(label12);
            playersTab.Controls.Add(button8);
            playersTab.Controls.Add(button7);
            playersTab.Controls.Add(button6);
            playersTab.Controls.Add(btnMute);
            playersTab.Controls.Add(button3);
            playersTab.Controls.Add(button2);
            playersTab.Controls.Add(label10);
            playersTab.Controls.Add(label7);
            playersTab.Controls.Add(playersGrid);
            playersTab.Controls.Add(button4);
            playersTab.Controls.Add(xbanCheck);
            playersTab.Controls.Add(xbanText);
            playersTab.Location = new Point(4, 22);
            playersTab.Name = "playersTab";
            playersTab.Padding = new Padding(3);
            playersTab.Size = new Size(720, 486);
            playersTab.TabIndex = 8;
            playersTab.Text = "Players";
            playerColorCombo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            playerColorCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            playerColorCombo.FormattingEnabled = true;
            playerColorCombo.Location = new Point(500, 395);
            playerColorCombo.Name = "playerColorCombo";
            playerColorCombo.Size = new Size(102, 21);
            playerColorCombo.TabIndex = 66;
            targetMapCombo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            targetMapCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            targetMapCombo.FormattingEnabled = true;
            targetMapCombo.Location = new Point(501, 422);
            targetMapCombo.Name = "targetMapCombo";
            targetMapCombo.Size = new Size(101, 21);
            targetMapCombo.TabIndex = 65;
            targetMapCombo.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            button15.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button15.Location = new Point(338, 364);
            button15.Name = "button15";
            button15.Size = new Size(75, 23);
            button15.TabIndex = 64;
            button15.Text = "Undo All";
            button15.UseVisualStyleBackColor = true;
            button15.Click += button15_Click;
            button14.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button14.Location = new Point(419, 422);
            button14.Name = "button14";
            button14.Size = new Size(75, 23);
            button14.TabIndex = 63;
            button14.Text = "Move";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            banCheck.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            banCheck.AutoSize = true;
            banCheck.Location = new Point(95, 425);
            banCheck.Name = "banCheck";
            banCheck.Size = new Size(16, 15);
            banCheck.TabIndex = 61;
            banCheck.UseVisualStyleBackColor = true;
            banCheck.Visible = false;
            banCheck.CheckedChanged += banCheck_CheckedChanged;
            kickCheck.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            kickCheck.AutoSize = true;
            kickCheck.Location = new Point(95, 369);
            kickCheck.Name = "kickCheck";
            kickCheck.Size = new Size(16, 15);
            kickCheck.TabIndex = 60;
            kickCheck.UseVisualStyleBackColor = true;
            kickCheck.CheckedChanged += kickCheck_CheckedChanged;
            playersListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            playersListView.Columns.AddRange(new ColumnHeader[3]
            {
                PlName, PlRank, PlMap
            });
            playersListView.FullRowSelect = true;
            playersListView.HideSelection = false;
            playersListView.Location = new Point(18, 34);
            playersListView.MultiSelect = false;
            playersListView.Name = "playersListView";
            playersListView.Size = new Size(304, 284);
            playersListView.Sorting = SortOrder.Ascending;
            playersListView.TabIndex = 59;
            playersListView.UseCompatibleStateImageBehavior = false;
            playersListView.View = View.Details;
            playersListView.SelectedIndexChanged += playersListView_SelectedIndexChanged;
            PlName.Text = "Name";
            PlName.Width = 100;
            PlRank.Text = "Rank";
            PlRank.Width = 100;
            PlMap.Text = "Map";
            PlMap.Width = 100;
            titleText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            titleText.Location = new Point(500, 366);
            titleText.MaxLength = 17;
            titleText.Name = "titleText";
            titleText.Size = new Size(102, 21);
            titleText.TabIndex = 57;
            banText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            banText.Location = new Point(114, 424);
            banText.Name = "banText";
            banText.ReadOnly = true;
            banText.Size = new Size(207, 21);
            banText.TabIndex = 55;
            banText.Visible = false;
            kickText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            kickText.Location = new Point(114, 366);
            kickText.Name = "kickText";
            kickText.ReadOnly = true;
            kickText.Size = new Size(207, 21);
            kickText.TabIndex = 54;
            kickText.TextChanged += kickText_TextChanged;
            label12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label12.AutoSize = true;
            label12.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label12.Location = new Point(15, 344);
            label12.Name = "label12";
            label12.Size = new Size(67, 14);
            label12.TabIndex = 53;
            label12.Text = "Commands";
            button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button8.Location = new Point(338, 393);
            button8.Name = "button8";
            button8.Size = new Size(75, 23);
            button8.TabIndex = 51;
            button8.Text = "Kill";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            button7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button7.Location = new Point(419, 393);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 47;
            button7.Text = "Color";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button6.Location = new Point(419, 364);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 50;
            button6.Text = "Title";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            btnMute.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnMute.Location = new Point(338, 422);
            btnMute.Name = "btnMute";
            btnMute.Size = new Size(75, 23);
            btnMute.TabIndex = 49;
            btnMute.Text = "Mute";
            btnMute.UseVisualStyleBackColor = true;
            btnMute.Click += button5_Click;
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(18, 364);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 47;
            button3.Text = "Kick";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(18, 422);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 46;
            button2.Text = "Ban";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            label10.AutoSize = true;
            label10.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label10.Location = new Point(367, 15);
            label10.Name = "label10";
            label10.Size = new Size(63, 14);
            label10.TabIndex = 45;
            label10.Text = "Properties";
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label7.Location = new Point(15, 15);
            label7.Name = "label7";
            label7.Size = new Size(46, 14);
            label7.TabIndex = 42;
            label7.Text = "Players";
            playersGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            playersGrid.Location = new Point(370, 34);
            playersGrid.Name = "playersGrid";
            playersGrid.Size = new Size(300, 284);
            playersGrid.TabIndex = 1;
            playersGrid.ToolbarVisible = false;
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.Location = new Point(18, 393);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 48;
            button4.Text = "XBan";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            xbanCheck.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            xbanCheck.AutoSize = true;
            xbanCheck.Location = new Point(95, 398);
            xbanCheck.Name = "xbanCheck";
            xbanCheck.Size = new Size(16, 15);
            xbanCheck.TabIndex = 62;
            xbanCheck.UseVisualStyleBackColor = true;
            xbanCheck.CheckedChanged += xbanCheck_CheckedChanged;
            xbanText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            xbanText.Location = new Point(114, 395);
            xbanText.Name = "xbanText";
            xbanText.ReadOnly = true;
            xbanText.Size = new Size(207, 21);
            xbanText.TabIndex = 56;
            mapsTab.BackColor = Color.Transparent;
            mapsTab.Controls.Add(mapsList);
            mapsTab.Controls.Add(button13);
            mapsTab.Controls.Add(label11);
            mapsTab.Controls.Add(unloadedMapsList);
            mapsTab.Controls.Add(button12);
            mapsTab.Controls.Add(button11);
            mapsTab.Controls.Add(button10);
            mapsTab.Controls.Add(btnCreateMap);
            mapsTab.Controls.Add(label9);
            mapsTab.Controls.Add(label8);
            mapsTab.Controls.Add(allMapsGrid);
            mapsTab.Location = new Point(4, 22);
            mapsTab.Name = "mapsTab";
            mapsTab.Padding = new Padding(3);
            mapsTab.Size = new Size(720, 486);
            mapsTab.TabIndex = 9;
            mapsTab.Text = "Maps";
            mapsList.FormattingEnabled = true;
            mapsList.Location = new Point(33, 38);
            mapsList.Name = "mapsList";
            mapsList.Size = new Size(132, 199);
            mapsList.TabIndex = 0;
            mapsList.SelectedIndexChanged += mapsList_SelectedIndexChanged;
            button13.Location = new Point(191, 38);
            button13.Name = "button13";
            button13.Size = new Size(75, 23);
            button13.TabIndex = 53;
            button13.Text = "Unload";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            label11.AutoSize = true;
            label11.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label11.Location = new Point(30, 255);
            label11.Name = "label11";
            label11.Size = new Size(94, 14);
            label11.TabIndex = 52;
            label11.Text = "Unloaded maps";
            unloadedMapsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            unloadedMapsList.FormattingEnabled = true;
            unloadedMapsList.Location = new Point(33, 275);
            unloadedMapsList.Name = "unloadedMapsList";
            unloadedMapsList.Size = new Size(132, 173);
            unloadedMapsList.TabIndex = 51;
            button12.Location = new Point(191, 96);
            button12.Name = "button12";
            button12.Size = new Size(75, 23);
            button12.TabIndex = 50;
            button12.Text = "Delete";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            button11.Enabled = false;
            button11.Location = new Point(191, 67);
            button11.Name = "button11";
            button11.Size = new Size(75, 23);
            button11.TabIndex = 49;
            button11.Text = "Rename";
            button11.UseVisualStyleBackColor = true;
            button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button10.Location = new Point(191, 436);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 48;
            button10.Text = "Load";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            btnCreateMap.Location = new Point(191, 214);
            btnCreateMap.Name = "btnCreateMap";
            btnCreateMap.Size = new Size(75, 23);
            btnCreateMap.TabIndex = 47;
            btnCreateMap.Text = "Create";
            btnCreateMap.UseVisualStyleBackColor = true;
            btnCreateMap.Click += button9_Click;
            label9.AutoSize = true;
            label9.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label9.Location = new Point(395, 20);
            label9.Name = "label9";
            label9.Size = new Size(63, 14);
            label9.TabIndex = 44;
            label9.Text = "Properties";
            label8.AutoSize = true;
            label8.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label8.Location = new Point(30, 19);
            label8.Name = "label8";
            label8.Size = new Size(37, 14);
            label8.TabIndex = 43;
            label8.Text = "Maps";
            allMapsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            allMapsGrid.Location = new Point(398, 38);
            allMapsGrid.Name = "allMapsGrid";
            allMapsGrid.Size = new Size(278, 409);
            allMapsGrid.TabIndex = 1;
            lavaTab.BackColor = Color.Transparent;
            lavaTab.Controls.Add(label6);
            lavaTab.Controls.Add(mapsGrid);
            lavaTab.Location = new Point(4, 22);
            lavaTab.Name = "lavaTab";
            lavaTab.Padding = new Padding(3);
            lavaTab.Size = new Size(720, 486);
            lavaTab.TabIndex = 6;
            lavaTab.Text = "Lava Survival";
            lavaTab.Click += lavaTab_Click;
            label6.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(42, 21);
            label6.Name = "label6";
            label6.Size = new Size(96, 19);
            label6.TabIndex = 5;
            label6.Text = "Lava maps:";
            mapsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mapsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mapsGrid.BackgroundColor = SystemColors.Control;
            mapsGrid.BorderStyle = BorderStyle.Fixed3D;
            mapsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mapsGrid.Columns.AddRange(mapName, sourceX, sourceY, sourceZ, phase1, phase2, typeOfLava);
            mapsGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            mapsGrid.Location = new Point(43, 49);
            mapsGrid.Name = "mapsGrid";
            val3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            val3.BackColor = SystemColors.Control;
            val3.Font = new Font("Calibri", 8.25f);
            val3.ForeColor = SystemColors.WindowText;
            val3.SelectionBackColor = SystemColors.Highlight;
            val3.SelectionForeColor = SystemColors.HighlightText;
            val3.WrapMode = DataGridViewTriState.True;
            mapsGrid.RowHeadersDefaultCellStyle = val3;
            val4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mapsGrid.RowsDefaultCellStyle = val4;
            mapsGrid.SelectionMode = 0;
            mapsGrid.Size = new Size(616, 404);
            mapsGrid.TabIndex = 0;
            mapsGrid.CellContentClick += mapsGrid_CellContentClick;
            mapName.FillWeight = 135.3597f;
            mapName.HeaderText = "Map Name";
            mapName.MinimumWidth = 40;
            mapName.Name = "mapName";
            sourceX.FillWeight = 88.54781f;
            sourceX.HeaderText = "x";
            sourceX.MinimumWidth = 15;
            sourceX.Name = "sourceX";
            sourceY.FillWeight = 88.54781f;
            sourceY.HeaderText = "y";
            sourceY.MinimumWidth = 15;
            sourceY.Name = "sourceY";
            sourceZ.FillWeight = 88.54781f;
            sourceZ.HeaderText = "z";
            sourceZ.MinimumWidth = 15;
            sourceZ.Name = "sourceZ";
            phase1.FillWeight = 103.8501f;
            phase1.HeaderText = "Phase I";
            phase1.MinimumWidth = 30;
            phase1.Name = "phase1";
            phase2.FillWeight = 106.599f;
            phase2.HeaderText = "Phase II";
            phase2.MinimumWidth = 30;
            phase2.Name = "phase2";
            typeOfLava.FillWeight = 88.54781f;
            typeOfLava.HeaderText = "Block";
            typeOfLava.MinimumWidth = 30;
            typeOfLava.Name = "typeOfLava";
            systemTab.BackColor = Color.Transparent;
            systemTab.Controls.Add(groupBox1);
            systemTab.Controls.Add(groupBox2);
            systemTab.Controls.Add(groupBox3);
            systemTab.Location = new Point(4, 22);
            systemTab.Name = "systemTab";
            systemTab.Padding = new Padding(7, 6, 20, 11);
            systemTab.Size = new Size(720, 486);
            systemTab.TabIndex = 4;
            systemTab.Text = "Remote Console";
            groupBox1.BackColor = Color.FromArgb(224, 224, 224);
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label20);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(button18);
            groupBox1.Location = new Point(27, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 201);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "Remote Connection Settings";
            button9.Location = new Point(150, 62);
            button9.Name = "button9";
            button9.Size = new Size(45, 23);
            button9.TabIndex = 22;
            button9.Text = "Set";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click_2;
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(20, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(124, 17);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Allow remote access";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged_1;
            label20.AutoSize = true;
            label20.Location = new Point(17, 67);
            label20.Name = "label20";
            label20.Size = new Size(69, 13);
            label20.TabIndex = 0;
            label20.Text = "Remote port:";
            textBox1.Location = new Point(92, 62);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(50, 21);
            textBox1.TabIndex = 1;
            textBox1.Text = "33434";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Validating += textBox1_Validating;
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(20, 95);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(168, 17);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Listed for web browser access";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            button18.Enabled = false;
            button18.Location = new Point(200, 62);
            button18.Name = "button18";
            button18.Size = new Size(45, 23);
            button18.TabIndex = 10;
            button18.Text = "Test";
            button18.UseVisualStyleBackColor = true;
            button18.Click += button18_Click;
            groupBox2.BackColor = Color.FromArgb(224, 224, 224);
            groupBox2.Controls.Add(accountsList);
            groupBox2.Controls.Add(newAccountBtn);
            groupBox2.Controls.Add(removeAccountBtn);
            groupBox2.Controls.Add(changeAccountBtn);
            groupBox2.Location = new Point(362, 33);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(325, 201);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "AccountManager";
            accountsList.Columns.AddRange(new ColumnHeader[1]
            {
                remoteAccount
            });
            accountsList.FullRowSelect = true;
            accountsList.Location = new Point(131, 39);
            accountsList.MultiSelect = false;
            accountsList.Name = "accountsList";
            accountsList.Size = new Size(121, 135);
            accountsList.Sorting = SortOrder.Ascending;
            accountsList.TabIndex = 21;
            accountsList.UseCompatibleStateImageBehavior = false;
            accountsList.View = View.Details;
            remoteAccount.Text = "Remote Account";
            remoteAccount.Width = 100;
            newAccountBtn.Location = new Point(27, 63);
            newAccountBtn.Name = "newAccountBtn";
            newAccountBtn.Size = new Size(75, 23);
            newAccountBtn.TabIndex = 5;
            newAccountBtn.Text = "New";
            newAccountBtn.UseVisualStyleBackColor = true;
            newAccountBtn.Click += newAccountBtn_Click;
            removeAccountBtn.Location = new Point(27, 121);
            removeAccountBtn.Name = "removeAccountBtn";
            removeAccountBtn.Size = new Size(75, 23);
            removeAccountBtn.TabIndex = 6;
            removeAccountBtn.Text = "Remove";
            removeAccountBtn.UseVisualStyleBackColor = true;
            removeAccountBtn.Click += removeAccountBtn_Click;
            changeAccountBtn.Location = new Point(27, 92);
            changeAccountBtn.Name = "changeAccountBtn";
            changeAccountBtn.Size = new Size(75, 23);
            changeAccountBtn.TabIndex = 7;
            changeAccountBtn.Text = "Change";
            changeAccountBtn.UseVisualStyleBackColor = true;
            changeAccountBtn.Click += changeAccountBtn_Click;
            groupBox3.BackColor = Color.FromArgb(224, 224, 224);
            groupBox3.Controls.Add(linkLabel1);
            groupBox3.Controls.Add(textBox4);
            groupBox3.Controls.Add(label22);
            groupBox3.Location = new Point(27, 259);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(660, 215);
            groupBox3.TabIndex = 25;
            groupBox3.TabStop = false;
            groupBox3.Text = "Guide";
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(22, 36);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(180, 13);
            linkLabel1.TabIndex = 22;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "http://mcdzienny.cba.pl/remote.php";
            linkLabel1.VisitedLinkColor = Color.Blue;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            textBox4.Location = new Point(283, 20);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.ScrollBars = ScrollBars.Vertical;
            textBox4.Size = new Size(354, 174);
            textBox4.TabIndex = 21;
            textBox4.Text = componentResourceManager.GetString("textBox4.Text");
            label22.AutoSize = true;
            label22.Location = new Point(22, 23);
            label22.Name = "label22";
            label22.Size = new Size(164, 13);
            label22.TabIndex = 11;
            label22.Text = "You can find the remote client on:";
            changelogTab.BackColor = Color.Transparent;
            changelogTab.Controls.Add(tabControl1);
            changelogTab.Location = new Point(4, 22);
            changelogTab.Name = "changelogTab";
            changelogTab.Padding = new Padding(7, 6, 20, 11);
            changelogTab.Size = new Size(720, 486);
            changelogTab.TabIndex = 2;
            changelogTab.Text = "Info";
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(7, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(693, 469);
            tabControl1.TabIndex = 1;
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(txtChangelog);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(685, 443);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Changelog";
            txtChangelog.BackColor = Color.White;
            txtChangelog.Cursor = Cursors.Arrow;
            txtChangelog.Dock = DockStyle.Fill;
            txtChangelog.Location = new Point(3, 3);
            txtChangelog.Margin = new Padding(10, 10, 30, 30);
            txtChangelog.Multiline = true;
            txtChangelog.Name = "txtChangelog";
            txtChangelog.ReadOnly = true;
            txtChangelog.ScrollBars = ScrollBars.Vertical;
            txtChangelog.Size = new Size(679, 437);
            txtChangelog.TabIndex = 0;
            tabPage2.BackColor = Color.Transparent;
            tabPage2.Controls.Add(txtSystem);
            tabPage2.Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(685, 443);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "System Log";
            txtSystem.BackColor = Color.White;
            txtSystem.Cursor = Cursors.Arrow;
            txtSystem.Dock = DockStyle.Fill;
            txtSystem.Location = new Point(3, 3);
            txtSystem.Multiline = true;
            txtSystem.Name = "txtSystem";
            txtSystem.ReadOnly = true;
            txtSystem.ScrollBars = ScrollBars.Vertical;
            txtSystem.Size = new Size(679, 437);
            txtSystem.TabIndex = 2;
            errorsTab.BackColor = Color.Transparent;
            errorsTab.Controls.Add(txtErrors);
            errorsTab.Location = new Point(4, 22);
            errorsTab.Name = "errorsTab";
            errorsTab.Padding = new Padding(7, 6, 20, 11);
            errorsTab.Size = new Size(720, 486);
            errorsTab.TabIndex = 3;
            errorsTab.Text = "Errors";
            txtErrors.BackColor = Color.White;
            txtErrors.Cursor = Cursors.Arrow;
            txtErrors.Dock = DockStyle.Fill;
            txtErrors.Location = new Point(7, 6);
            txtErrors.Multiline = true;
            txtErrors.Name = "txtErrors";
            txtErrors.ReadOnly = true;
            txtErrors.ScrollBars = ScrollBars.Vertical;
            txtErrors.Size = new Size(693, 469);
            txtErrors.TabIndex = 1;
            txtErrors.TextChanged += txtErrors_TextChanged;
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            minimizeButton.Location = new Point(659, 5);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.Size = new Size(64, 23);
            minimizeButton.TabIndex = 36;
            minimizeButton.Text = "Minimize";
            minimizeButton.Click += minimizeButton_Click;
            btnProperties.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProperties.Cursor = Cursors.Hand;
            btnProperties.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProperties.Location = new Point(585, 5);
            btnProperties.Name = "btnProperties";
            btnProperties.Size = new Size(70, 23);
            btnProperties.TabIndex = 34;
            btnProperties.Text = "Properties";
            btnProperties.UseVisualStyleBackColor = true;
            btnProperties.Click += btnProperties_Click_1;
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button5.Cursor = Cursors.Hand;
            button5.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(535, 5);
            button5.Name = "button5";
            button5.Size = new Size(46, 23);
            button5.TabIndex = 37;
            button5.Text = "Tools";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            statusStrip1.Items.AddRange(new ToolStripItem[3]
            {
                toolStripStatusLabelUptime, toolStripStatusLabelRoundTime, toolStripStatusLabelLagometer
            });
            statusStrip1.Location = new Point(2, 525);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.RenderMode = ToolStripRenderMode.Professional;
            statusStrip1.Size = new Size(727, 23);
            statusStrip1.TabIndex = 38;
            statusStrip1.Text = "statusStrip1";
            toolStripStatusLabelUptime.AutoSize = false;
            toolStripStatusLabelUptime.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripStatusLabelUptime.Margin = new Padding(10, 3, 0, 2);
            toolStripStatusLabelUptime.Name = "toolStripStatusLabelUptime";
            toolStripStatusLabelUptime.Size = new Size(128, 18);
            toolStripStatusLabelUptime.Text = "Uptime : 0min";
            toolStripStatusLabelUptime.TextAlign = ContentAlignment.MiddleLeft;
            toolStripStatusLabelRoundTime.AutoSize = false;
            toolStripStatusLabelRoundTime.Margin = new Padding(5, 3, 0, 2);
            toolStripStatusLabelRoundTime.Name = "toolStripStatusLabelRoundTime";
            toolStripStatusLabelRoundTime.Size = new Size(260, 18);
            toolStripStatusLabelRoundTime.Text = "Flood starts in : 1h 59min    Round ends in : 1h 59min";
            toolStripStatusLabelRoundTime.TextAlign = ContentAlignment.MiddleLeft;
            toolStripStatusLabelLagometer.Margin = new Padding(5, 3, 0, 2);
            toolStripStatusLabelLagometer.Name = "toolStripStatusLabelLagometer";
            toolStripStatusLabelLagometer.Size = new Size(304, 18);
            toolStripStatusLabelLagometer.Spring = true;
            toolStripStatusLabelLagometer.Text = "Lag (avg.) : ";
            toolStripStatusLabelLagometer.TextAlign = ContentAlignment.MiddleLeft;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 549);
            Controls.Add(statusStrip1);
            Controls.Add(button5);
            Controls.Add(btnProperties);
            Controls.Add(minimizeButton);
            Controls.Add(mainTabs);
            MinimumSize = new Size(740, 580);
            Name = "Window";
            Padding = new Padding(2, 13, 3, 1);
            FormClosing += Window_FormClosing;
            Load += Window_Load;
            mapsStrip.ResumeLayout(false);
            playerStrip.ResumeLayout(false);
            zombieSurvivalTab.ResumeLayout(false);
            ((ISupportInitialize)infectionMapsGrid).EndInit();
            iconContext.ResumeLayout(false);
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            mainTabs.ResumeLayout(false);
            mainTab.ResumeLayout(false);
            mainTab.PerformLayout();
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel1.PerformLayout();
            splitContainer5.Panel2.ResumeLayout(false);
            splitContainer5.Panel2.PerformLayout();
            splitContainer5.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            splitContainer4.ResumeLayout(false);
            gBChat.ResumeLayout(false);
            gBChat.PerformLayout();
            gBCommands.ResumeLayout(false);
            gBCommands.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            splitContainer2.ResumeLayout(false);
            chatTab.ResumeLayout(false);
            chatTab.PerformLayout();
            ((ISupportInitialize)pictureBox1).EndInit();
            tabPagePlugins.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            playersTab.ResumeLayout(false);
            playersTab.PerformLayout();
            mapsTab.ResumeLayout(false);
            mapsTab.PerformLayout();
            lavaTab.ResumeLayout(false);
            ((ISupportInitialize)mapsGrid).EndInit();
            systemTab.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            changelogTab.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            errorsTab.ResumeLayout(false);
            errorsTab.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        void mapsStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listViewMaps.SelectedIndices.Count <= 0)
            {
                e.Cancel = true;
            }
        }

        void playerStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listViewPlayers.SelectedIndices.Count <= 0)
            {
                e.Cancel = true;
            }
        }

        public struct SCROLLBARINFO
        {
            public int cbSize;

            public RECT rcScrollBar;

            public int dxyLineButton;

            public int xyThumbTop;

            public int xyThumbBottom;

            public int reserved;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] rgstate;
        }

        public struct RECT
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
        }

        delegate void StringCallback(string s);

        delegate void PlayerListCallback();

        delegate void ReportCallback(Report r);

        delegate void VoidDelegate();

        delegate void RefreshMapList();

        delegate void LogDelegate(string message);

        delegate void ChatDelegate(string message);

        delegate void SystemDelegate(string message);

        delegate void ErrorDelegate(string message);

        public class Coloring
        {

            public Color color;
            public int index;
        }

        delegate void UpdateListViewDelegate(List<string[]> listElements);
    }
}