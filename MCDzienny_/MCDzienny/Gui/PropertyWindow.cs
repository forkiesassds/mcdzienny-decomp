using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    public class PropertyWindow : Form
    {

        readonly List<Block.Blocks> storedBlocks = new List<Block.Blocks>();

        readonly List<GrpCommands.rankAllowance> storedCommands = new List<GrpCommands.rankAllowance>();

        readonly List<Group> storedRanks = new List<Group>();

        CheckBox autoFlagCheck;
        BadWordsEditor badWordsEditor;

        Button btnAddRank;

        Button btnApply;

        Button btnBlHelp;

        Button btnCmdHelp;

        Button btnDiscard;

        Button btnSave;

        Button btnSetServerMessage;

        Button button1;

        Button button2;

        Button button3;

        Button button6;

        CheckBox checkBoxChatFilterAdvanced;

        CheckBox checkBoxCmdCooldown;

        CheckBox checkBoxMessagesCooldown;

        CheckBox checkBoxMessagesCooldown1;

        CheckBox checkBoxRemoveBadWords;

        CheckBox checkBoxRemoveBadWords1;

        CheckBox checkBoxRemoveCaps;

        CheckBox checkBoxRemoveCaps1;

        CheckBox checkBoxShortenRepetitions;

        CheckBox checkBoxShortenRepetitions1;

        CheckBox checkBoxTempMute;

        CheckBox chk17Dollar;

        CheckBox chkAutoload;

        CheckBox chkBanMessage;

        CheckBox chkCheap;

        CheckBox chkDeath;

        CheckBox chkForceCuboid;

        CheckBox chkHelp;

        CheckBox chkIRC;

        CheckBox chkPhysicsRest;

        CheckBox chkPublic;

        CheckBox chkrankSuper;

        CheckBox chkRepeatMessages;

        CheckBox chkRestart;

        CheckBox chkRestartTime;

        CheckBox chkShutdown;

        CheckBox chkSmile;

        CheckBox ChkTunnels;

        CheckBox chkUpdates;

        CheckBox chkVerify;

        CheckBox chkWorld;

        ComboBox cmbColor;

        ComboBox cmbDefaultColour;

        ComboBox cmbDefaultRank;

        ComboBox cmbIRCColour;

        ComboBox cmbOpChat;

        ComboBox comboBoxDetectionLevel;

        IContainer components;

        TextBox descriptionBox;

        TextBox flagBox;

        HScrollBar FlowControl;

        PropertyGrid generalProperties;

        GroupBox groupBox1;

        GroupBox groupBox2;

        GroupBox groupBox3;

        TextBox heavenMapName;

        Label label1;

        Label label10;

        Label label11;

        Label label12;

        Label label13;

        Label label14;

        Label label15;

        Label label16;

        Label label17;

        Label label18;

        Label label19;

        Label label2;

        Label label20;

        Label label21;

        Label label22;

        Label label23;

        Label label24;

        Label label25;

        Label label26;

        Label label27;

        Label label28;

        Label label29;

        Label label3;

        Label label30;

        Label label31;

        Label label32;

        Label label33;

        Label label34;

        Label label35;

        Label label36;

        Label label37;

        Label label38;

        Label label39;

        Label label4;

        Label label40;

        Label label41;

        Label label42;

        Label label43;

        Label label44;

        Label label45;

        Label label46;

        Label label47;

        Label label48;

        Label label49;

        Label label5;

        Label label50;

        Label label51;

        Label label52;

        Label label53;

        Label label54;

        Label label55;

        Label label56;

        Label label57;

        Label label58;

        Label label59;

        Label label6;

        Label label60;

        Label label61;

        Label label62;

        Label label63;

        Label label64;

        Label label65;

        Label label66;

        Label label67;

        Label label68;

        Label label69;

        Label label7;

        Label label70;

        Label label71;

        Label label72;

        Label label73;

        Label label74;

        Label label75;

        Label label76;

        Label label77;

        Label label78;

        Label label79;

        Label label8;

        Label label80;

        Label label81;

        Label label82;

        Label label83;

        Label label84;

        Label label85;

        Label label86;

        Label label87;

        Label label88;

        Label label89;

        Label label9;

        Label label90;

        PropertyGrid lavaPropertyGrid;

        Label lblDefault;

        Label lblIRC;

        Label lblOpChat;

        ListBox listBlocks;

        ListBox listCommands;

        ListBox listRanks;

        TabPage misc3;

        Label PositionDelay;

        Button PositionDelayUpdate;

        RadioButton radioButtonBadLanguage1;

        RadioButton radioButtonBadLanguage2;

        RadioButton radioButtonBadLanguage3;

        RadioButton radioButtonCharSpam1;

        RadioButton radioButtonCharSpam2;

        RadioButton radioButtonCharSpam3;

        TextBox serverMessage;

        TextBox serverMessageInterval;

        Button setHeavenMapButton;

        bool skip;

        TabControl tabControl;

        TabControl tabControl1;

        TabControl tabControl2;

        TabControl tabControl3;

        TabControl tabControlChat;

        TabPage tabPage1;

        TabPage tabPage10;

        TabPage tabPage11;

        TabPage tabPage12;

        TabPage tabPage13;

        TabPage tabPage14;

        TabPage tabPage15;

        TabPage tabPage16;

        TabPage tabPage2;

        TabPage tabPage3;

        TabPage tabPage4;

        TabPage tabPage5;

        TabPage tabPage6;

        TabPage tabPage8;

        TabPage tabPage9;

        TabPage tabPageChat1;

        TabPage tabPageChatBadWords;

        TabPage tabPageChatCaps;

        TabPage tabPageChatCharSpam;

        TabPage tabPageChatSpam;

        TextBox textBox12;

        TextBox textBox13;

        TextBox textBox14;

        TextBox textBox6;

        TextBox textBoxBadLanguageKickLimit;

        TextBox textBoxBadLanguageKickMsg;

        TextBox textBoxBadLanguageSubstitution;

        TextBox textBoxBadLanguageWarning;

        TextBox textBoxBigMaps;

        TextBox textBoxCharSpamSubstitution;

        TextBox textBoxCharSpamWarning;

        TextBox textBoxCmdMax;

        TextBox textBoxCmdMaxSeconds;

        TextBox textBoxCmdShortcut;

        TextBox textBoxCmdWarning;

        TextBox textBoxDuplicateMessages;

        TextBox textBoxDuplicateMessagesSeconds;

        TextBox textBoxDuplicateMessagesWarning;

        TextBox textBoxMaxCaps;

        TextBox textBoxMaxChars;

        TextBox textBoxMaxIllegalGroups;

        TextBox textBoxMaxMessages;

        TextBox textBoxMaxMessagesSeconds;

        TextBox textBoxMaxMessagesWarning;

        TextBox textBoxMediumMaps;

        TextBox textBoxSmallMaps;

        ToolTip toolTip;

        TextBox txtafk;

        TextBox txtAFKKick;

        TextBox txtBackup;

        TextBox txtBackupLocation;

        TextBox txtBanMessage;

        TextBox txtBlAllow;

        TextBox txtBlDisallow;

        TextBox txtBlLowest;

        TextBox txtBlRanks;

        TextBox txtChannel;

        TextBox txtCheap;

        TextBox txtCmdAllow;

        TextBox txtCmdDisallow;

        TextBox txtCmdLowest;

        TextBox txtCmdRanks;

        TextBox txtDepth;

        TextBox txtFileName;

        TextBox txtHost;

        TextBox txtIRCServer;

        TextBox txtLimit;

        TextBox txtMain;

        TextBox txtMaps;

        TextBox txtMoneys;

        TextBox txtMOTD;

        TextBox txtName;

        TextBox txtNick;

        TextBox txtNormRp;

        TextBox txtOpChannel;

        TextBox txtPermission;

        TextBox txtPlayers;

        TextBox txtPort;

        TextBox txtPositionDelay;

        TextBox txtPromotionPrice;

        TextBox txtRankName;

        TextBox txtRestartTime;

        TextBox txtRP;

        TextBox txtShutdown;

        TextBox txtTime1;

        TextBox txtTime2;

        Label updateLabel;

        Panel updatePanel;

        Button updateTimeSettingsButton;

        CheckBox useHeaven;

        Button vipAdd;

        TextBox vipEntry;

        Label vipLabel;

        ListBox vipList;

        Button vipRemove;

        WhiteListEditor whiteListEditor;

        PropertyGrid zombiePropertyGrid;

        public PropertyWindow()
        {
            InitializeComponent();
            UpdateProperties();
            UpdateLavaProperties();
            UpdateZombieProperties();
            InitChatFilterTab();
        }

        void UpdateZombieProperties()
        {
            zombiePropertyGrid.SelectedObject = InfectionSettings.All;
        }

        public void ShowAt(Point location)
        {
            StartPosition = 0;
            int x = location.X - Width / 2;
            int y = location.Y - Height / 2;
            Location = new Point(x, y);
            Show();
        }

        void PropertyWindow_Load(object sender, EventArgs e)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            object[] array = new object[16]
            {
                "black", "navy", "green", "teal", "maroon", "purple", "gold", "silver", "gray", "blue", "lime", "aqua", "red", "pink", "yellow", "white"
            };
            cmbDefaultColour.Items.AddRange(array);
            cmbIRCColour.Items.AddRange(array);
            cmbColor.Items.AddRange(array);
            string text = "";
            foreach (Group group in Group.groupList)
            {
                cmbDefaultRank.Items.Add(group.name);
                cmbOpChat.Items.Add(group.name);
                if (group.Permission == Server.opchatperm)
                {
                    text = group.name;
                }
            }
            cmbDefaultRank.SelectedIndex = 1;
            cmbOpChat.SelectedIndex = !(text != "") ? 1 : cmbOpChat.Items.IndexOf(text);
            LoadProp("properties/server.properties");
            LoadRanks();
            try
            {
                LoadCommands();
                LoadBlocks();
            }
            catch
            {
                Server.s.Log("Failed to load commands and blocks!");
            }
            vipList.Items.AddRange(VipList.GetArray());
        }

        void PropertyWindow_Unload(object sender, EventArgs e)
        {
            Window.prevLoaded = false;
            GeneralSettings.All.Save();
        }

        public void LoadRanks()
        {
            txtCmdRanks.Text = "The following ranks are available: \r\n\r\n";
            listRanks.Items.Clear();
            storedRanks.Clear();
            storedRanks.AddRange(Group.groupList);
            foreach (Group storedRank in storedRanks)
            {
                TextBox obj = txtCmdRanks;
                object text = obj.Text;
                obj.Text = string.Concat(text, "\t", storedRank.name, " (", (int)storedRank.Permission, ")\r\n");
                listRanks.Items.Add(storedRank.trueName + "  =  " + (int)storedRank.Permission);
            }
            txtBlRanks.Text = txtCmdRanks.Text;
            listRanks.SelectedIndex = 0;
        }

        public void SaveRanks()
        {
            Group.saveGroups(storedRanks);
            Group.InitAll();
            LoadRanks();
        }

        public void LoadCommands()
        {
            listCommands.Items.Clear();
            storedCommands.Clear();
            foreach (GrpCommands.rankAllowance allowedCommand in GrpCommands.allowedCommands)
            {
                storedCommands.Add(allowedCommand);
                listCommands.Items.Add(allowedCommand.commandName);
            }
            if (listCommands.SelectedIndex == -1)
            {
                listCommands.SelectedIndex = 0;
            }
        }

        public void SaveCommands()
        {
            GeneralSettings.All.CooldownCmdUse = checkBoxCmdCooldown.Checked;
            try
            {
                GeneralSettings.All.CooldownCmdMax = int.Parse(textBoxCmdMax.Text);
            }
            catch {}
            try
            {
                GeneralSettings.All.CooldownCmdMaxSeconds = int.Parse(textBoxCmdMaxSeconds.Text);
            }
            catch {}
            GeneralSettings.All.CooldownCmdWarning = textBoxCmdWarning.Text;
            GrpCommands.Save(storedCommands);
            GrpCommands.fillRanks();
            LoadCommands();
        }

        public void LoadBlocks()
        {
            listBlocks.Items.Clear();
            storedBlocks.Clear();
            storedBlocks.AddRange(Block.BlockList);
            foreach (Block.Blocks storedBlock in storedBlocks)
            {
                if (Block.Name(storedBlock.type) != "unknown")
                {
                    listBlocks.Items.Add(Block.Name(storedBlock.type));
                }
            }
            if (listBlocks.SelectedIndex == -1)
            {
                listBlocks.SelectedIndex = 0;
            }
        }

        public void SaveBlocks()
        {
            Block.SaveBlocks(storedBlocks);
            Block.SetBlocks();
            LoadBlocks();
        }

        public void UpdateProperties()
        {
            generalProperties.SelectedObject = GeneralSettings.All;
            serverMessage.Text = Server.serverMessage;
            serverMessageInterval.Text = Server.serverMessageInterval.ToString();
            vipList.Items.AddRange(VipList.GetArray());
            checkBoxCmdCooldown.Checked = GeneralSettings.All.CooldownCmdUse;
            textBoxCmdMax.Text = GeneralSettings.All.CooldownCmdMax.ToString();
            textBoxCmdMaxSeconds.Text = GeneralSettings.All.CooldownCmdMaxSeconds.ToString();
            textBoxCmdWarning.Text = GeneralSettings.All.CooldownCmdWarning;
        }

        public void LoadProp(string givenPath)
        {
            if (!File.Exists(givenPath))
            {
                return;
            }
            string[] array = File.ReadAllLines(givenPath);
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (text == "" || text[0] == '#')
                {
                    continue;
                }
                string text2 = text.Split('=')[0].Trim();
                string text3 = text.Substring(text.IndexOf('=') + 1).Trim();
                string text4 = "";
                switch (text2.ToLower())
                {
                    case "server-name":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                        {
                            txtName.Text = text3;
                        }
                        else
                        {
                            txtName.Text = "MCDzienny Default Server Name";
                        }
                        break;
                    case "motd":
                        if (ValidString(text3, "![]&:.,{}~-+()?_/\\= "))
                        {
                            txtMOTD.Text = text3;
                        }
                        else
                        {
                            txtMOTD.Text = "Welcome to my server!";
                        }
                        break;
                    case "port":
                        try
                        {
                            txtPort.Text = Convert.ToInt32(text3).ToString();
                        }
                        catch
                        {
                            txtPort.Text = "25565";
                        }
                        break;
                    case "verify-names-security":
                        chkVerify.Checked = text3.ToLower() == "true";
                        break;
                    case "public":
                        chkPublic.Checked = text3.ToLower() == "true";
                        break;
                    case "world-chat":
                        chkWorld.Checked = text3.ToLower() == "true";
                        break;
                    case "max-players":
                        try
                        {
                            if (Convert.ToByte(text3) > byte.MaxValue)
                            {
                                text3 = "255";
                            }
                            else if (Convert.ToByte(text3) < 0)
                            {
                                text3 = "0";
                            }
                            txtPlayers.Text = text3;
                        }
                        catch
                        {
                            Server.s.Log("max-players invalid! setting to default.");
                            txtPlayers.Text = "12";
                        }
                        break;
                    case "max-maps":
                        try
                        {
                            if (Convert.ToByte(text3) > 100)
                            {
                                text3 = "100";
                            }
                            else if (Convert.ToByte(text3) < 1)
                            {
                                text3 = "1";
                            }
                            txtMaps.Text = text3;
                        }
                        catch
                        {
                            Server.s.Log("max-maps invalid! setting to default.");
                            txtMaps.Text = "5";
                        }
                        break;
                    case "irc-use":
                        chkIRC.Checked = text3.ToLower() == "true";
                        break;
                    case "irc-server":
                        txtIRCServer.Text = text3;
                        break;
                    case "irc-nick":
                        txtNick.Text = text3;
                        break;
                    case "irc-channel":
                        txtChannel.Text = text3;
                        break;
                    case "irc-opchannel":
                        txtOpChannel.Text = text3;
                        break;
                    case "anti-tunnels":
                        ChkTunnels.Checked = text3.ToLower() == "true";
                        break;
                    case "max-depth":
                        txtDepth.Text = text3;
                        break;
                    case "rplimit":
                        try
                        {
                            txtRP.Text = text3;
                        }
                        catch
                        {
                            txtRP.Text = "500";
                        }
                        break;
                    case "rplimit-norm":
                        try
                        {
                            txtNormRp.Text = text3;
                        }
                        catch
                        {
                            txtNormRp.Text = "10000";
                        }
                        break;
                    case "force-cuboid":
                        chkForceCuboid.Checked = text3.ToLower() == "true";
                        break;
                    case "backup-time":
                        try
                        {
                            int num = int.Parse(text3);
                            txtBackup.Text = num.ToString();
                        }
                        catch
                        {
                            txtBackup.Text = "300";
                        }
                        break;
                    case "backup-location":
                        if (!text3.Contains("System.Windows.Forms.TextBox, Text:"))
                        {
                            txtBackupLocation.Text = text3;
                        }
                        break;
                    case "physicsrestart":
                        chkPhysicsRest.Checked = text3.ToLower() == "true";
                        break;
                    case "deathcount":
                        chkDeath.Checked = text3.ToLower() == "true";
                        break;
                    case "defaultcolor":
                        text4 = c.Parse(text3);
                        if (text4 == "")
                        {
                            text4 = c.Name(text3);
                            if (!(text4 != ""))
                            {
                                Server.s.Log("Could not find " + text3);
                                break;
                            }
                            text4 = text3;
                        }
                        cmbDefaultColour.SelectedIndex = cmbDefaultColour.Items.IndexOf(c.Name(text3));
                        break;
                    case "irc-color":
                        text4 = c.Parse(text3);
                        if (text4 == "")
                        {
                            text4 = c.Name(text3);
                            if (!(text4 != ""))
                            {
                                Server.s.Log("Could not find " + text3);
                                break;
                            }
                            text4 = text3;
                        }
                        cmbIRCColour.SelectedIndex = cmbIRCColour.Items.IndexOf(c.Name(text3));
                        break;
                    case "default-rank":
                        try
                        {
                            if (cmbDefaultRank.Items.IndexOf(text3.ToLower()) != -1)
                            {
                                cmbDefaultRank.SelectedIndex = cmbDefaultRank.Items.IndexOf(text3.ToLower());
                            }
                        }
                        catch
                        {
                            cmbDefaultRank.SelectedIndex = 1;
                        }
                        break;
                    case "old-help":
                        chkHelp.Checked = text3.ToLower() == "true";
                        break;
                    case "cheapmessage":
                        chkCheap.Checked = text3.ToLower() == "true";
                        break;
                    case "cheap-message-given":
                        txtCheap.Text = text3;
                        break;
                    case "rank-super":
                        chkrankSuper.Checked = text3.ToLower() == "true";
                        break;
                    case "custom-ban":
                        chkBanMessage.Checked = text3.ToLower() == "true";
                        break;
                    case "custom-ban-message":
                        txtBanMessage.Text = text3;
                        break;
                    case "custom-shutdown":
                        chkShutdown.Checked = text3.ToLower() == "true";
                        break;
                    case "custom-shutdown-message":
                        txtShutdown.Text = text3;
                        break;
                    case "auto-restart":
                        chkRestartTime.Checked = text3.ToLower() == "true";
                        break;
                    case "restarttime":
                        txtRestartTime.Text = text3;
                        break;
                    case "afk-minutes":
                        try
                        {
                            txtafk.Text = Convert.ToInt16(text3).ToString();
                        }
                        catch
                        {
                            txtafk.Text = "10";
                        }
                        break;
                    case "afk-kick":
                        try
                        {
                            txtAFKKick.Text = Convert.ToInt16(text3).ToString();
                        }
                        catch
                        {
                            txtAFKKick.Text = "45";
                        }
                        break;
                    case "check-updates":
                        chkUpdates.Checked = text3.ToLower() == "true";
                        break;
                    case "autoload":
                        chkAutoload.Checked = text3.ToLower() == "true";
                        break;
                    case "parse-emotes":
                        chkSmile.Checked = text3.ToLower() == "true";
                        break;
                    case "main-name":
                        txtMain.Text = text3;
                        break;
                    case "dollar-before-dollar":
                        chk17Dollar.Checked = text3.ToLower() == "true";
                        break;
                    case "money-name":
                        txtMoneys.Text = text3;
                        break;
                    case "restart-on-error":
                        chkRestart.Checked = text3.ToLower() == "true";
                        break;
                    case "repeat-messages":
                        chkRepeatMessages.Checked = text3.ToLower() == "true";
                        break;
                    case "host-state":
                        if (text3 != "")
                        {
                            txtHost.Text = text3;
                        }
                        break;
                    case "server-description":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                        {
                            descriptionBox.Text = text3;
                        }
                        else
                        {
                            descriptionBox.Text = Server.description;
                        }
                        break;
                    case "server-flag":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                        {
                            flagBox.Text = text3;
                        }
                        else
                        {
                            flagBox.Text = Server.Flag;
                        }
                        break;
                    case "auto-flag":
                        try
                        {
                            autoFlagCheck.Checked = bool.Parse(text3);
                        }
                        catch
                        {
                            autoFlagCheck.Checked = Server.autoFlag;
                        }
                        if (autoFlagCheck.Checked)
                        {
                            flagBox.Enabled = false;
                            flagBox.Text = Server.Flag;
                        }
                        break;
                }
            }
        }

        public bool ValidString(string str, string allowed)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890" + allowed;
            foreach (char value in str)
            {
                if (text.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public void Save(string givenPath)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(givenPath)))
                {
                    streamWriter.WriteLine("# Edit the settings below to modify how your server operates. This is an explanation of what each setting does.");
                    streamWriter.WriteLine("#   server-name\t=\tThe name which displays on minecraft.net");
                    streamWriter.WriteLine("#   motd\t=\tThe message which displays when a player connects");
                    streamWriter.WriteLine("#   port\t=\tThe port to operate from");
                    streamWriter.WriteLine("#   console-only\t=\tRun without a GUI (useful for Linux servers with mono)");
                    streamWriter.WriteLine("#   verify-names-security\t=\tVerify the validity of names");
                    streamWriter.WriteLine("#   public\t=\tSet to true to appear in the public server list");
                    streamWriter.WriteLine("#   max-players\t=\tThe maximum number of connections");
                    streamWriter.WriteLine("#   max-maps\t=\tThe maximum number of maps loaded at once");
                    streamWriter.WriteLine("#   world-chat\t=\tSet to true to enable world chat");
                    streamWriter.WriteLine("#   guest-goto\t=\tSet to true to give guests goto and levels commands");
                    streamWriter.WriteLine("#   irc\t=\tSet to true to enable the IRC bot");
                    streamWriter.WriteLine("#   irc-nick\t=\tThe name of the IRC bot");
                    streamWriter.WriteLine("#   irc-server\t=\tThe server to connect to");
                    streamWriter.WriteLine("#   irc-channel\t=\tThe channel to join");
                    streamWriter.WriteLine("#   irc-opchannel\t=\tThe channel to join (posts OpChat)");
                    streamWriter.WriteLine("#   irc-port\t=\tThe port to use to connect");
                    streamWriter.WriteLine(
                        "#   irc-identify\t=(true/false)\tDo you want the IRC bot to Identify itself with nickserv. Note: You will need to register it's name with nickserv manually.");
                    streamWriter.WriteLine("#   irc-password\t=\tThe password you want to use if you're identifying with nickserv");
                    streamWriter.WriteLine("#   anti-tunnels\t=\tStops people digging below max-depth");
                    streamWriter.WriteLine("#   max-depth\t=\tThe maximum allowed depth to dig down");
                    streamWriter.WriteLine("#   backup-time\t=\tThe number of seconds between automatic backups");
                    streamWriter.WriteLine("#   overload\t=\tThe higher this is, the longer the physics is allowed to lag. Default 1500");
                    streamWriter.WriteLine("#   use-whitelist\t=\tSwitch to allow use of a whitelist to override IP bans for certain players.  Default false.");
                    streamWriter.WriteLine("#   force-cuboid\t=\tRun cuboid until the limit is hit, instead of canceling the whole operation.  Default false.");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#   Host\t=\tThe host name for the database (usually 127.0.0.1)");
                    streamWriter.WriteLine("#   SQLPort\t=\tPort number to be used for MySQL.  Unless you manually changed the port, leave this alone.  Default 3306.");
                    streamWriter.WriteLine("#   Username\t=\tThe username you used to create the database (usually root)");
                    streamWriter.WriteLine("#   Password\t=\tThe password set while making the database");
                    streamWriter.WriteLine("#   DatabaseName\t=\tThe name of the database stored (Default = MCZall)");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#   defaultColor\t=\tThe color code of the default messages (Default = &e)");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# Server options");
                    streamWriter.WriteLine("server-name = " + txtName.Text);
                    streamWriter.WriteLine("motd = " + txtMOTD.Text);
                    streamWriter.WriteLine("port = " + txtPort.Text);
                    streamWriter.WriteLine("verify-names-security = " + chkVerify.Checked.ToString().ToLower());
                    streamWriter.WriteLine("public = " + chkPublic.Checked.ToString().ToLower());
                    streamWriter.WriteLine("max-players = " + txtPlayers.Text);
                    streamWriter.WriteLine("max-maps = " + txtMaps.Text);
                    streamWriter.WriteLine("world-chat = " + chkWorld.Checked.ToString().ToLower());
                    streamWriter.WriteLine("check-updates = " + chkUpdates.Checked.ToString().ToLower());
                    streamWriter.WriteLine("autoload = " + chkAutoload.Checked.ToString().ToLower());
                    streamWriter.WriteLine("auto-restart = " + chkRestartTime.Checked.ToString().ToLower());
                    streamWriter.WriteLine("restarttime = " + txtRestartTime.Text);
                    streamWriter.WriteLine("restart-on-error = " + chkRestart.Checked);
                    if (Player.ValidName(txtMain.Text))
                    {
                        streamWriter.WriteLine("main-name = " + txtMain.Text);
                    }
                    else
                    {
                        streamWriter.WriteLine("main-name = main");
                    }
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# irc bot options");
                    streamWriter.WriteLine("irc-use = " + chkIRC.Checked);
                    streamWriter.WriteLine("irc-nick = " + txtNick.Text);
                    streamWriter.WriteLine("irc-server = " + txtIRCServer.Text);
                    streamWriter.WriteLine("irc-channel = " + txtChannel.Text);
                    streamWriter.WriteLine("irc-opchannel = " + txtOpChannel.Text);
                    streamWriter.WriteLine("irc-port = " + Server.ircPort);
                    streamWriter.WriteLine("irc-identify = " + Server.ircIdentify);
                    streamWriter.WriteLine("irc-password = " + Server.ircPassword);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# other options");
                    streamWriter.WriteLine("anti-tunnels = " + ChkTunnels.Checked.ToString().ToLower());
                    streamWriter.WriteLine("max-depth = " + txtDepth.Text);
                    streamWriter.WriteLine("rplimit = " + txtRP.Text);
                    streamWriter.WriteLine("physicsrestart = " + chkPhysicsRest.Checked.ToString().ToLower());
                    streamWriter.WriteLine("old-help = " + chkHelp.Checked.ToString().ToLower());
                    streamWriter.WriteLine("deathcount = " + chkDeath.Checked.ToString().ToLower());
                    streamWriter.WriteLine("afk-minutes = " + txtafk.Text);
                    streamWriter.WriteLine("afk-kick = " + txtAFKKick.Text);
                    streamWriter.WriteLine("parse-emotes = " + chkSmile.Checked.ToString().ToLower());
                    streamWriter.WriteLine("dollar-before-dollar = " + chk17Dollar.Checked.ToString().ToLower());
                    streamWriter.WriteLine("use-whitelist = " + Server.useWhitelist.ToString().ToLower());
                    streamWriter.WriteLine("money-name = " + txtMoneys.Text);
                    streamWriter.WriteLine("opchat-perm = " +
                                           (sbyte)Group.groupList.Find(grp => grp.name == cmbOpChat.Items[cmbOpChat.SelectedIndex].ToString())
                                               .Permission);
                    streamWriter.WriteLine("force-cuboid = " + chkForceCuboid.Checked.ToString().ToLower());
                    streamWriter.WriteLine("repeat-messages = " + chkRepeatMessages.Checked);
                    streamWriter.WriteLine("host-state = " + txtHost.Text);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# backup options");
                    streamWriter.WriteLine("backup-time = " + txtBackup.Text);
                    streamWriter.WriteLine("backup-location = " + txtBackupLocation.Text);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Error logging");
                    streamWriter.WriteLine("report-back = " + Server.reportBack.ToString().ToLower());
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#MySQL information");
                    streamWriter.WriteLine("UseMySQL = " + Server.useMySQL);
                    streamWriter.WriteLine("Host = " + Server.MySQLHost);
                    streamWriter.WriteLine("SQLPort = " + Server.MySQLPort);
                    streamWriter.WriteLine("Username = " + Server.MySQLUsername);
                    streamWriter.WriteLine("Password = " + Server.MySQLPassword);
                    streamWriter.WriteLine("DatabaseName = " + Server.MySQLDatabaseName);
                    streamWriter.WriteLine("Pooling = " + Server.MySQLPooling);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Colors");
                    streamWriter.WriteLine("defaultColor = " + cmbDefaultColour.Items[cmbDefaultColour.SelectedIndex]);
                    streamWriter.WriteLine("irc-color = " + cmbIRCColour.Items[cmbIRCColour.SelectedIndex]);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Custom Messages");
                    streamWriter.WriteLine("custom-ban = " + chkBanMessage.Checked.ToString().ToLower());
                    streamWriter.WriteLine("custom-ban-message = " + txtBanMessage.Text);
                    streamWriter.WriteLine("custom-shutdown = " + chkShutdown.Checked.ToString().ToLower());
                    streamWriter.WriteLine("custom-shutdown-message = " + txtShutdown.Text);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("cheapmessage = " + chkCheap.Checked.ToString().ToLower());
                    streamWriter.WriteLine("cheap-message-given = " + txtCheap.Text);
                    streamWriter.WriteLine("rank-super = " + chkrankSuper.Checked.ToString().ToLower());
                    streamWriter.WriteLine("default-rank = " + cmbDefaultRank.Items[cmbDefaultRank.SelectedIndex]);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Lava Settings");
                    streamWriter.WriteLine("lava-state = " + Enum.GetName(typeof(LavaState), LavaSettings.All.LavaState));
                    streamWriter.WriteLine("global-time-before = " + LavaSystem.stime);
                    streamWriter.WriteLine("global-time-after = " + LavaSystem.stime2);
                    streamWriter.WriteLine("reappearing-message = " + Server.serverMessage.Replace(Environment.NewLine, "^"));
                    streamWriter.WriteLine("reappearing-message-interval = " + Server.serverMessageInterval);
                    streamWriter.WriteLine("use-heaven = " + Server.useHeaven);
                    streamWriter.WriteLine("heaven-map-name = " + Server.heavenMapName);
                    streamWriter.WriteLine("chance-calm = " + LavaSystem.chanceCalm);
                    streamWriter.WriteLine("chance-disturbed = " + LavaSystem.chanceDisturbed);
                    streamWriter.WriteLine("chance-furious = " + LavaSystem.chanceFurious);
                    streamWriter.WriteLine("chance-wild = " + LavaSystem.chanceWild);
                    streamWriter.WriteLine("game-mode = " + Enum.GetName(typeof(Mode), Server.mode));
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#WOM Settings");
                    streamWriter.WriteLine("server-description = " + descriptionBox.Text);
                    streamWriter.WriteLine("server-flag = " + flagBox.Text);
                    streamWriter.WriteLine("auto-flag = " + autoFlagCheck.Checked);
                }
            }
            catch
            {
                Server.s.Log("SAVE FAILED! " + givenPath);
            }
        }

        void cmbDefaultColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDefault.BackColor = Color.FromName(cmbDefaultColour.Items[cmbDefaultColour.SelectedIndex].ToString());
        }

        void cmbIRCColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIRC.BackColor = Color.FromName(cmbIRCColour.Items[cmbIRCColour.SelectedIndex].ToString());
        }

        void ClearIfNotNumber(TextBox foundTxt)
        {
            try
            {
                int.Parse(foundTxt.Text[foundTxt.Text.Length - 1].ToString());
            }
            catch
            {
                foundTxt.Text = "";
            }
        }

        void txtPort_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtPort);
        }

        void txtPlayers_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtPlayers);
        }

        void txtMaps_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtMaps);
        }

        void txtBackup_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtBackup);
        }

        void txtDepth_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtDepth);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            saveStuff();
            SaveChatFilterTab();
            Dispose();
        }

        void btnApply_Click(object sender, EventArgs e)
        {
            ApplyChatFilterTab();
            saveStuff();
        }

        void saveStuff()
        {
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Expected O, but got Unknown
            //IL_0056: Unknown result type (might be due to invalid IL or missing references)
            //IL_005c: Expected O, but got Unknown
            //IL_0098: Unknown result type (might be due to invalid IL or missing references)
            foreach (Control item in (ArrangedElementCollection)tabControl.Controls)
            {
                Control val = item;
                if (!(val is TabPage) || val == tabPage3 || val == tabPage5)
                {
                    continue;
                }
                foreach (Control item2 in (ArrangedElementCollection)val.Controls)
                {
                    Control val2 = item2;
                    if (val2 is TextBox && val2.Text == "" && val2 != serverMessage && val2 != vipEntry)
                    {
                        MessageBox.Show("A textbox has been left empty. It must be filled.\n" + val2.Name);
                        return;
                    }
                }
            }
            Save("properties/server.properties");
            SaveRanks();
            SaveCommands();
            SaveBlocks();
            ServerProperties.Load("properties/server.properties", skipsalt: true);
            GrpCommands.fillRanks();
        }

        void btnDiscard_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void toolTip_Popup(object sender, PopupEventArgs e) {}

        void tabPage2_Click(object sender, EventArgs e) {}

        void tabPage1_Click(object sender, EventArgs e) {}

        void chkPhysicsRest_CheckedChanged(object sender, EventArgs e) {}

        void chkGC_CheckedChanged(object sender, EventArgs e) {}

        void btnBackup_Click(object sender, EventArgs e)
        {
            //IL_0005: Unknown result type (might be due to invalid IL or missing references)
            MessageBox.Show("Currently glitchy! Just type in the location by hand.");
        }

        void listRanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!skip)
            {
                Group group = storedRanks.Find(grp => grp.trueName == listRanks.Items[listRanks.SelectedIndex].ToString().Split('=')[0].Trim());
                if (group.Permission == LevelPermission.Nobody)
                {
                    listRanks.SelectedIndex = 0;
                    return;
                }
                txtRankName.Text = group.trueName;
                TextBox obj = txtPermission;
                int permission = (int)group.Permission;
                obj.Text = permission.ToString();
                txtLimit.Text = group.maxBlocks.ToString();
                cmbColor.SelectedIndex = cmbColor.Items.IndexOf(c.Name(group.color));
                txtFileName.Text = group.fileName;
                txtPromotionPrice.Text = group.promotionPrice.ToString();
                textBoxSmallMaps.Text = group.smallMaps.ToString();
                textBoxMediumMaps.Text = group.mediumMaps.ToString();
                textBoxBigMaps.Text = group.bigMaps.ToString();
            }
        }

        void txtRankName_TextChanged(object sender, EventArgs e)
        {
            if (txtRankName.Text != "" && txtRankName.Text.ToLower() != "nobody")
            {
                storedRanks[listRanks.SelectedIndex].trueName = txtRankName.Text;
                skip = true;
                listRanks.Items[listRanks.SelectedIndex] = txtRankName.Text + "  =  " + (int)storedRanks[listRanks.SelectedIndex].Permission;
                skip = false;
            }
        }

        void txtPermission_TextChanged(object sender, EventArgs e)
        {
            if (!(txtPermission.Text != ""))
            {
                return;
            }
            int num;
            try
            {
                num = int.Parse(txtPermission.Text);
            }
            catch
            {
                if (txtPermission.Text != "-")
                {
                    txtPermission.Text = txtPermission.Text.Remove(txtPermission.Text.Length - 1);
                }
                return;
            }
            if (num < -50)
            {
                txtPermission.Text = "-50";
                return;
            }
            if (num > 119)
            {
                txtPermission.Text = "119";
                return;
            }
            storedRanks[listRanks.SelectedIndex].Permission = (LevelPermission)num;
            skip = true;
            listRanks.Items[listRanks.SelectedIndex] = storedRanks[listRanks.SelectedIndex].trueName + "  =  " + num;
            skip = false;
        }

        void txtLimit_TextChanged(object sender, EventArgs e)
        {
            if (txtLimit.Text != "")
            {
                int num;
                try
                {
                    num = int.Parse(txtLimit.Text);
                }
                catch
                {
                    txtLimit.Text = txtLimit.Text.Remove(txtLimit.Text.Length - 1);
                    return;
                }
                if (num < 1)
                {
                    txtLimit.Text = "1";
                }
                else
                {
                    storedRanks[listRanks.SelectedIndex].maxBlocks = num;
                }
            }
        }

        void txtFileName_TextChanged(object sender, EventArgs e)
        {
            if (txtFileName.Text != "")
            {
                storedRanks[listRanks.SelectedIndex].fileName = txtFileName.Text;
            }
        }

        void btnAddRank_Click(object sender, EventArgs e)
        {
            new Random();
            Group group = new Group((LevelPermission)5, 600, "CHANGEME", '1', "CHANGEME.txt", 0);
            storedRanks.Add(group);
            listRanks.Items.Add(group.trueName + "  =  " + (int)group.Permission);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (listRanks.Items.Count > 1)
            {
                storedRanks.RemoveAt(listRanks.SelectedIndex);
                skip = true;
                listRanks.Items.RemoveAt(listRanks.SelectedIndex);
                skip = false;
                listRanks.SelectedIndex = 0;
            }
        }

        void listCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            Command cmd = Command.all.Find(listCommands.SelectedItem.ToString());
            GrpCommands.rankAllowance rankAllowance = storedCommands.Find(aV => aV.commandName == cmd.name);
            if (Group.findPerm(rankAllowance.lowestRank) == null)
            {
                rankAllowance.lowestRank = cmd.defaultRank;
            }
            txtCmdLowest.Text = string.Concat((int)rankAllowance.lowestRank);
            bool flag = false;
            txtCmdDisallow.Text = "";
            foreach (LevelPermission item in rankAllowance.disallow)
            {
                flag = true;
                TextBox obj = txtCmdDisallow;
                obj.Text = obj.Text + "," + (int)item;
            }
            if (flag)
            {
                txtCmdDisallow.Text = txtCmdDisallow.Text.Remove(0, 1);
            }
            flag = false;
            txtCmdAllow.Text = "";
            foreach (LevelPermission item2 in rankAllowance.allow)
            {
                flag = true;
                TextBox obj2 = txtCmdAllow;
                obj2.Text = obj2.Text + "," + (int)item2;
            }
            if (flag)
            {
                txtCmdAllow.Text = txtCmdAllow.Text.Remove(0, 1);
            }
            textBoxCmdShortcut.Text = cmd.shortcut;
        }

        void txtCmdLowest_TextChanged(object sender, EventArgs e)
        {
            fillLowest(ref txtCmdLowest, ref storedCommands[listCommands.SelectedIndex].lowestRank);
        }

        void txtCmdDisallow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtCmdDisallow, ref storedCommands[listCommands.SelectedIndex].disallow);
        }

        void txtCmdAllow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtCmdAllow, ref storedCommands[listCommands.SelectedIndex].allow);
        }

        void listBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte b = Block.Byte(listBlocks.SelectedItem.ToString());
            Block.Blocks blocks = storedBlocks.Find(bS => bS.type == b);
            TextBox obj = txtBlLowest;
            int lowestRank = (int)blocks.lowestRank;
            obj.Text = lowestRank.ToString();
            bool flag = false;
            txtBlDisallow.Text = "";
            foreach (LevelPermission item in blocks.disallow)
            {
                flag = true;
                TextBox obj2 = txtBlDisallow;
                obj2.Text = obj2.Text + "," + (int)item;
            }
            if (flag)
            {
                txtBlDisallow.Text = txtBlDisallow.Text.Remove(0, 1);
            }
            flag = false;
            txtBlAllow.Text = "";
            foreach (LevelPermission item2 in blocks.allow)
            {
                flag = true;
                TextBox obj3 = txtBlAllow;
                obj3.Text = obj3.Text + "," + (int)item2;
            }
            if (flag)
            {
                txtBlAllow.Text = txtBlAllow.Text.Remove(0, 1);
            }
        }

        void txtBlLowest_TextChanged(object sender, EventArgs e)
        {
            fillLowest(ref txtBlLowest, ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).lowestRank);
        }

        void txtBlDisallow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtBlDisallow, ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).disallow);
        }

        void txtBlAllow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtBlAllow, ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).allow);
        }

        void fillAllowance(ref TextBox txtBox, ref List<LevelPermission> addTo)
        {
            addTo.Clear();
            if (!(txtBox.Text != ""))
            {
                return;
            }
            string[] array = txtBox.Text.Split(',');
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim().ToLower();
                int item;
                try
                {
                    item = int.Parse(array[i]);
                }
                catch
                {
                    Group group = Group.Find(array[i]);
                    if (group == null)
                    {
                        Server.s.Log("Could not find " + array[i]);
                        continue;
                    }
                    item = (int)group.Permission;
                }
                addTo.Add((LevelPermission)item);
            }
            txtBox.Text = "";
            foreach (LevelPermission item2 in addTo)
            {
                TextBox obj2 = txtBox;
                obj2.Text = obj2.Text + "," + (int)item2;
            }
            if (txtBox.Text != "")
            {
                txtBox.Text = txtBox.Text.Remove(0, 1);
            }
        }

        void fillLowest(ref TextBox txtBox, ref LevelPermission toChange)
        {
            if (!(txtBox.Text != ""))
            {
                return;
            }
            txtBox.Text = txtBox.Text.Trim().ToLower();
            int num = -100;
            try
            {
                num = int.Parse(txtBox.Text);
            }
            catch
            {
                Group group = Group.Find(txtBox.Text);
                if (group != null)
                {
                    num = (int)group.Permission;
                }
                else
                {
                    Server.s.Log("Could not find " + txtBox.Text);
                }
            }
            txtBox.Text = "";
            if (num < -99)
            {
                txtBox.Text = string.Concat((int)toChange);
            }
            else
            {
                txtBox.Text = string.Concat(num);
            }
            toChange = (LevelPermission)Convert.ToInt16(txtBox.Text);
        }

        void btnBlHelp_Click(object sender, EventArgs e)
        {
            getHelp(listBlocks.SelectedItem.ToString());
        }

        void btnCmdHelp_Click(object sender, EventArgs e)
        {
            getHelp(listCommands.SelectedItem.ToString());
        }

        void getHelp(string toHelp)
        {
            //IL_004a: Unknown result type (might be due to invalid IL or missing references)
            Player.storedHelp = "";
            Player.storeHelp = true;
            Command.all.Find("help").Use(null, toHelp);
            Player.storeHelp = false;
            string text = "Help information for " + toHelp + ":\r\n\r\n";
            text += Player.storedHelp;
            MessageBox.Show(text);
        }

        void chkIRC_CheckedChanged(object sender, EventArgs e)
        {
            Server.irc = chkIRC.Checked;
            ActiveControl = null;
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtPromotionPrice.Text != "")
            {
                int num;
                try
                {
                    num = int.Parse(txtPromotionPrice.Text);
                }
                catch
                {
                    txtLimit.Text = "Wrong number.";
                    return;
                }
                if (num < 0)
                {
                    txtLimit.Text = "0";
                }
                else
                {
                    storedRanks[listRanks.SelectedIndex].promotionPrice = num;
                }
            }
        }

        void txtMOTD_TextChanged(object sender, EventArgs e) {}

        void FlowControl_Scroll(object sender, ScrollEventArgs e)
        {
            if (FlowControl.Value < 11)
            {
                Server.updateTimer.Interval = 40 + (FlowControl.Value - 1) * 10;
            }
            if (FlowControl.Value >= 11 && FlowControl.Value < 21)
            {
                Server.updateTimer.Interval = 150 + (FlowControl.Value - 11) * 20;
            }
            if (FlowControl.Value >= 21 && FlowControl.Value < 31)
            {
                Server.updateTimer.Interval = 380 + (FlowControl.Value - 21) * 50;
            }
            if (FlowControl.Value >= 31 && FlowControl.Value < 41)
            {
                Server.updateTimer.Interval = 900 + (FlowControl.Value - 31) * 100;
            }
            if (FlowControl.Value >= 41 && FlowControl.Value < 51)
            {
                Server.updateTimer.Interval = 2000 + (FlowControl.Value - 41) * 1000;
            }
            txtPositionDelay.Text = Server.updateTimer.Interval.ToString();
        }

        void PositionDelayUpdate_Click(object sender, EventArgs e)
        {
            if (txtPositionDelay.Text != "")
            {
                try
                {
                    Server.updateTimer.Interval = int.Parse(txtPositionDelay.Text.Trim(' '));
                }
                catch
                {
                    Server.s.Log("Error: Delay time given is incorrect");
                }
            }
        }

        void vipRemove_Click(object sender, EventArgs e)
        {
            if (vipList.SelectedItem != null)
            {
                VipList.RemoveVIP(vipList.SelectedItem.ToString());
                vipList.Items.Clear();
                vipList.Items.AddRange(VipList.GetArray());
            }
        }

        void vipAdd_Click(object sender, EventArgs e)
        {
            VipList.AddVIP(vipEntry.Text);
            vipEntry.Text = "";
            vipList.Items.Clear();
            vipList.Items.AddRange(VipList.GetArray());
        }

        void chkIRC_VisibleChanged(object sender, EventArgs e) {}

        void btnSetServerMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(serverMessageInterval.Text.Trim()))
            {
                LavaSystem.StopServerMessage();
                serverMessageInterval.Text = "stopped";
                return;
            }
            try
            {
                Server.serverMessageInterval = int.Parse(serverMessageInterval.Text.Trim());
            }
            catch
            {
                serverMessageInterval.Text = "Incorrect number.";
                return;
            }
            if (ServerProperties.ValidString(serverMessage.Text, "*\"|'=%$![]&:;.,{}~-+()?_/\\ " + Environment.NewLine))
            {
                if (serverMessage.Text.Trim() == "")
                {
                    Server.serverMessage = "";
                    LavaSystem.StopServerMessage();
                    serverMessageInterval.Text = "stopped";
                }
                else
                {
                    Server.serverMessage = serverMessage.Text;
                    LavaSystem.StartServerMessage();
                }
            }
            else
            {
                serverMessage.Text = "Invalid character used.";
                LavaSystem.StopServerMessage();
                serverMessageInterval.Text = "stopped";
            }
        }

        void FlowControl_Scroll_1(object sender, ScrollEventArgs e) {}

        void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            storedRanks[listRanks.SelectedIndex].color = c.Parse(cmbColor.Items[cmbColor.SelectedIndex].ToString());
        }

        void vipAdd_Click_1(object sender, EventArgs e) {}

        void vipRemove_Click_1(object sender, EventArgs e) {}

        void txtName_TextChanged(object sender, EventArgs e) {}

        void autoFlagCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (autoFlagCheck.Checked)
            {
                flagBox.Enabled = false;
                flagBox.Text = Server.Flag;
            }
            else
            {
                flagBox.Enabled = true;
            }
        }

        void flagBox_TextChanged(object sender, EventArgs e) {}

        void txtBlLowest_TextChanged_1(object sender, EventArgs e) {}

        void serverMessage_TextChanged(object sender, EventArgs e) {}

        void generalProperties_Click(object sender, EventArgs e) {}

        void chkWorld_CheckedChanged(object sender, EventArgs e)
        {
            Server.worldChat = chkWorld.Checked;
            ActiveControl = null;
        }

        void chkVerify_CheckedChanged(object sender, EventArgs e)
        {
            Server.verify = chkVerify.Checked;
            ActiveControl = null;
        }

        void chkPublic_CheckedChanged(object sender, EventArgs e)
        {
            Server.isPublic = chkPublic.Checked;
            ActiveControl = null;
        }

        void chkAutoload_CheckedChanged(object sender, EventArgs e)
        {
            Server.AutoLoad = chkAutoload.Checked;
            ActiveControl = null;
        }

        void ChkTunnels_CheckedChanged(object sender, EventArgs e)
        {
            Server.antiTunnel = ChkTunnels.Checked;
            ActiveControl = null;
        }

        void chkRestart_CheckedChanged(object sender, EventArgs e)
        {
            Server.restartOnError = chkRestart.Checked;
            ActiveControl = null;
        }

        void chkRepeatMessages_CheckedChanged(object sender, EventArgs e)
        {
            Server.repeatMessage = chkRepeatMessages.Checked;
            ActiveControl = null;
        }

        void chk17Dollar_CheckedChanged(object sender, EventArgs e)
        {
            Server.useDollarSign = chk17Dollar.Checked;
            ActiveControl = null;
        }

        void chkSmile_CheckedChanged(object sender, EventArgs e)
        {
            Server.parseSmiley = chkSmile.Checked;
            ActiveControl = null;
        }

        void chkForceCuboid_CheckedChanged(object sender, EventArgs e)
        {
            Server.forceCuboid = chkForceCuboid.Checked;
            ActiveControl = null;
        }

        void chkBanMessage_CheckedChanged(object sender, EventArgs e)
        {
            Server.customBan = chkBanMessage.Checked;
        }

        void chkShutdown_CheckedChanged(object sender, EventArgs e)
        {
            Server.customShutdown = chkShutdown.Checked;
        }

        void chkCheap_CheckedChanged(object sender, EventArgs e)
        {
            Server.cheapMessage = chkCheap.Checked;
        }

        void chkRestartTime_CheckedChanged(object sender, EventArgs e) {}

        void chkrankSuper_CheckedChanged(object sender, EventArgs e) {}

        void chkHelp_CheckedChanged(object sender, EventArgs e)
        {
            Server.oldHelp = chkHelp.Checked;
            ActiveControl = null;
        }

        void chkDeath_CheckedChanged(object sender, EventArgs e)
        {
            Server.deathcount = chkDeath.Checked;
            ActiveControl = null;
        }

        void chkPhysicsRest_CheckedChanged_1(object sender, EventArgs e)
        {
            Server.physicsRestart = chkPhysicsRest.Checked;
            ActiveControl = null;
        }

        void tabPage8_Click(object sender, EventArgs e) {}

        public void UpdateLavaProperties()
        {
            lavaPropertyGrid.SelectedObject = LavaSettings.All;
            txtTime1.Text = LavaSystem.stime.ToString();
            txtTime2.Text = LavaSystem.stime2.ToString();
            heavenMapName.Text = Server.heavenMapName;
            useHeaven.Checked = Server.useHeaven;
        }

        void updateTime_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (txtTime1.Text != "" || txtTime1.Text.Trim(' ') != "")
            {
                try
                {
                    LavaSystem.stime = int.Parse(txtTime1.Text);
                    LavaSystem.time = int.Parse(txtTime1.Text);
                }
                catch
                {
                    flag = false;
                }
            }
            if (txtTime2.Text != "" || txtTime2.Text.Trim(' ') != "")
            {
                try
                {
                    LavaSystem.stime2 = int.Parse(txtTime2.Text);
                    LavaSystem.time2 = int.Parse(txtTime2.Text);
                }
                catch
                {
                    flag = false;
                }
            }
            if (flag)
            {
                Server.s.Log("Time settings were updated succesfully.");
            }
            else
            {
                Server.s.Log("Error: Given data is not a number.");
            }
        }

        void useHeaven_CheckedChanged(object sender, EventArgs e)
        {
            if (useHeaven.Checked)
            {
                Server.useHeaven = true;
            }
            else
            {
                Server.useHeaven = false;
            }
        }

        void setHeavenMap_Click(object sender, EventArgs e)
        {
            if (Level.ExactFind(heavenMapName.Text.ToLower()) != null)
            {
                return;
            }
            Level level = Level.Load(heavenMapName.Text.ToLower(), 3, lavaSurv: true);
            if (level != null)
            {
                if (Server.heavenMap != null)
                {
                    Command.all.Find("unload").Use(null, Server.heavenMap.name);
                }
                Server.heavenMap = level;
                Server.heavenMapName = Server.heavenMap.name;
                Server.AddLevel(Server.heavenMap);
            }
            else
            {
                heavenMapName.Text = "Map not found.";
            }
        }

        void PropertyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            LavaSettings.All.Save();
            InfectionSettings.All.Save();
        }

        void tabPage19_Click(object sender, EventArgs e) {}

        void InitChatFilterTab()
        {
            ChatFilterSettings all = ChatFilterSettings.All;
            SetAdvancedFilterVisibility(all.GuiShowAdvancedSettings);
            checkBoxRemoveCaps.Checked = all.RemoveCaps;
            checkBoxRemoveCaps1.Checked = all.RemoveCaps;
            checkBoxShortenRepetitions.Checked = all.ShortenRepetitions;
            checkBoxShortenRepetitions1.Checked = all.ShortenRepetitions;
            checkBoxRemoveBadWords.Checked = all.RemoveBadWords;
            checkBoxRemoveBadWords1.Checked = all.RemoveBadWords;
            checkBoxMessagesCooldown.Checked = all.MessagesCooldown;
            checkBoxMessagesCooldown1.Checked = all.MessagesCooldown;
            checkBoxChatFilterAdvanced.Checked = all.GuiShowAdvancedSettings;
            textBoxMaxCaps.Text = all.MaxCaps.ToString();
            textBoxMaxChars.Text = all.CharSpamMaxChars.ToString();
            textBoxMaxIllegalGroups.Text = all.CharSpamMaxIllegalGroups.ToString();
            textBoxCharSpamSubstitution.Text = all.CharSpamSubstitution;
            textBoxCharSpamWarning.Text = all.CharSpamWarning;
            switch (all.CharSpamAction)
            {
                case ChatFilter.CharacterSpamAction.DisplaySubstitution:
                    radioButtonCharSpam2.Checked = true;
                    break;
                case ChatFilter.CharacterSpamAction.SendWarning:
                    radioButtonCharSpam3.Checked = true;
                    break;
                case ChatFilter.CharacterSpamAction.DisplaySubstitution | ChatFilter.CharacterSpamAction.SendWarning:
                    radioButtonCharSpam1.Checked = true;
                    break;
                default:
                    radioButtonCharSpam1.Checked = true;
                    break;
            }
            textBoxBadLanguageSubstitution.Text = all.BadLanguageSubstitution;
            textBoxBadLanguageKickMsg.Text = all.BadLanguageKickMessage;
            textBoxBadLanguageWarning.Text = all.BadLanguageWarning;
            textBoxBadLanguageKickLimit.Text = all.BadLanguageWarningLimit.ToString();
            switch (all.BadLanguageDetectionLevel)
            {
                default:
                    comboBoxDetectionLevel.SelectedIndex = 0;
                    break;
                case ChatFilter.BadLanguageDetectionLevel.High:
                    comboBoxDetectionLevel.SelectedIndex = 1;
                    break;
            }
            switch (all.BadLanguageAction)
            {
                case ChatFilter.BadLanguageAction.DisplaySubstitution:
                    radioButtonBadLanguage2.Checked = true;
                    break;
                case ChatFilter.BadLanguageAction.SendWarning:
                    radioButtonBadLanguage3.Checked = true;
                    break;
                case ChatFilter.BadLanguageAction.DisplaySubstitution | ChatFilter.BadLanguageAction.SendWarning:
                    radioButtonBadLanguage1.Checked = true;
                    break;
                default:
                    radioButtonBadLanguage1.Checked = true;
                    break;
            }
            textBoxMaxMessages.Text = all.CooldownMaxMessages.ToString();
            textBoxMaxMessagesSeconds.Text = all.CooldownMaxMessagesSeconds.ToString();
            textBoxDuplicateMessages.Text = all.CooldownMaxSameMessages.ToString();
            textBoxDuplicateMessagesSeconds.Text = all.CooldownMaxSameMessagesSeconds.ToString();
            textBoxMaxMessagesWarning.Text = all.CooldownMaxWarning;
            textBoxDuplicateMessagesWarning.Text = all.CooldownDuplicatesWarning;
            checkBoxTempMute.Checked = all.CooldownTempMute;
        }

        void ApplyChatFilterTab()
        {
            ChatFilterSettings all = ChatFilterSettings.All;
            all.RemoveCaps = checkBoxRemoveCaps.Checked;
            all.ShortenRepetitions = checkBoxShortenRepetitions.Checked;
            all.RemoveBadWords = checkBoxRemoveBadWords.Checked;
            all.MessagesCooldown = checkBoxMessagesCooldown.Checked;
            all.GuiShowAdvancedSettings = checkBoxChatFilterAdvanced.Checked;
            int result = 3;
            if (int.TryParse(textBoxMaxCaps.Text, out result))
            {
                all.MaxCaps = result;
            }
            int result2 = 3;
            if (int.TryParse(textBoxMaxChars.Text, out result2))
            {
                all.CharSpamMaxChars = result2;
            }
            int result3 = 1;
            if (int.TryParse(textBoxMaxIllegalGroups.Text, out result3))
            {
                all.CharSpamMaxIllegalGroups = result3;
            }
            all.CharSpamSubstitution = textBoxCharSpamSubstitution.Text;
            all.CharSpamWarning = textBoxCharSpamWarning.Text;
            if (radioButtonCharSpam1.Checked)
            {
                all.CharSpamAction = ChatFilter.CharacterSpamAction.DisplaySubstitution | ChatFilter.CharacterSpamAction.SendWarning;
            }
            else if (radioButtonCharSpam2.Checked)
            {
                all.CharSpamAction = ChatFilter.CharacterSpamAction.DisplaySubstitution;
            }
            else
            {
                all.CharSpamAction = ChatFilter.CharacterSpamAction.SendWarning;
            }
            all.BadLanguageDetectionLevel =
                comboBoxDetectionLevel.SelectedIndex != 0 ? ChatFilter.BadLanguageDetectionLevel.High : ChatFilter.BadLanguageDetectionLevel.Normal;
            all.BadLanguageSubstitution = textBoxBadLanguageSubstitution.Text;
            int result4 = 3;
            if (int.TryParse(textBoxBadLanguageKickLimit.Text, out result4))
            {
                all.BadLanguageWarningLimit = result4;
            }
            all.BadLanguageWarning = textBoxBadLanguageWarning.Text;
            all.BadLanguageKickMessage = textBoxBadLanguageKickMsg.Text;
            if (radioButtonBadLanguage1.Checked)
            {
                all.BadLanguageAction = ChatFilter.BadLanguageAction.DisplaySubstitution | ChatFilter.BadLanguageAction.SendWarning;
            }
            else if (radioButtonBadLanguage2.Checked)
            {
                all.BadLanguageAction = ChatFilter.BadLanguageAction.DisplaySubstitution;
            }
            else
            {
                all.BadLanguageAction = ChatFilter.BadLanguageAction.SendWarning;
            }
            int result5 = 5;
            if (int.TryParse(textBoxMaxMessages.Text, out result5))
            {
                all.CooldownMaxMessages = result5;
            }
            int result6 = 10;
            if (int.TryParse(textBoxMaxMessagesSeconds.Text, out result6))
            {
                all.CooldownMaxMessagesSeconds = result6;
            }
            int result7 = 2;
            if (int.TryParse(textBoxDuplicateMessages.Text, out result7))
            {
                all.CooldownMaxSameMessages = result7;
            }
            int result8 = 8;
            if (int.TryParse(textBoxDuplicateMessagesSeconds.Text, out result8))
            {
                all.CooldownMaxSameMessagesSeconds = result8;
            }
            all.CooldownTempMute = checkBoxTempMute.Checked;
            all.CooldownMaxWarning = textBoxMaxMessagesWarning.Text;
            all.CooldownDuplicatesWarning = textBoxDuplicateMessagesWarning.Text;
        }

        void SaveChatFilterTab()
        {
            ApplyChatFilterTab();
            ChatFilterSettings.All.Save();
        }

        void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            SetAdvancedFilterVisibility(((CheckBox)sender).Checked);
        }

        void SetAdvancedFilterVisibility(bool visible)
        {
            if (visible)
            {
                Action<TabPage> action = delegate(TabPage t)
                {
                    if (!tabControlChat.TabPages.Contains(t))
                    {
                        tabControlChat.TabPages.Add(t);
                    }
                };
                action(tabPageChatBadWords);
                action(tabPageChatCaps);
                action(tabPageChatCharSpam);
                action(tabPageChatSpam);
            }
            else
            {
                tabControlChat.TabPages.Remove(tabPageChatCaps);
                tabControlChat.TabPages.Remove(tabPageChatCharSpam);
                tabControlChat.TabPages.Remove(tabPageChatBadWords);
                tabControlChat.TabPages.Remove(tabPageChatSpam);
            }
        }

        void CheckTheOther(object sender, CheckBox target)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            if (target.Checked != ((CheckBox)sender).Checked)
            {
                target.Checked = ((CheckBox)sender).Checked;
            }
        }

        void checkBoxRemoveCaps_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(sender, checkBoxRemoveCaps1);
            ChatFilterSettings.All.RemoveCaps = val.Checked;
        }

        void checkBoxRemoveCaps1_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxRemoveCaps);
            ChatFilterSettings.All.RemoveCaps = val.Checked;
        }

        void checkBoxShortenRepetitions_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxShortenRepetitions1);
            ChatFilterSettings.All.ShortenRepetitions = val.Checked;
        }

        void checkBoxShortenRepetitions1_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxShortenRepetitions);
            ChatFilterSettings.All.ShortenRepetitions = val.Checked;
        }

        void checkBoxRemoveBadWords_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxRemoveBadWords1);
            ChatFilterSettings.All.RemoveBadWords = val.Checked;
        }

        void checkBoxRemoveBadWords1_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxRemoveBadWords);
            ChatFilterSettings.All.RemoveBadWords = val.Checked;
        }

        void checkBoxMessagesCooldown_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxMessagesCooldown1);
            ChatFilterSettings.All.MessagesCooldown = val.Checked;
        }

        void checkBoxMessagesCooldown1_CheckedChanged(object sender, EventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            ActiveControl = null;
            CheckBox val = (CheckBox)sender;
            CheckTheOther(val, checkBoxMessagesCooldown);
            ChatFilterSettings.All.MessagesCooldown = val.Checked;
        }

        void ShowBadWordsEditor()
        {
            if (badWordsEditor == null || badWordsEditor.IsDisposed)
            {
                badWordsEditor = new BadWordsEditor();
                badWordsEditor.Show();
            }
            else if (badWordsEditor.Visible)
            {
                badWordsEditor.BringToFront();
            }
        }

        void ShowForm(Form form, Type type)
        {
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0035: Expected O, but got Unknown
            if (!typeof(Form).IsAssignableFrom(type))
            {
                throw new ArgumentException("Given type is not a type of Form.");
            }
            if (form == null || form.IsDisposed)
            {
                form = (Form)Activator.CreateInstance(type);
                form.Show();
            }
            else if (form.Visible)
            {
                form.BringToFront();
            }
        }

        void ShowForm<T>(T form) where T : Form, new()
        {
            if (form == null || form.IsDisposed)
            {
                form = new T();
                form.Show();
            }
            else if (form.Visible)
            {
                form.BringToFront();
            }
        }

        void button3_Click(object sender, EventArgs e)
        {
            ShowForm(badWordsEditor);
        }

        void button6_Click(object sender, EventArgs e)
        {
            ShowForm(badWordsEditor);
        }

        void button2_Click(object sender, EventArgs e)
        {
            ShowForm(whiteListEditor);
        }

        void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        void txtPromotionPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        void textBoxSmallMaps_TextChanged(object sender, EventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            TextBox val = (TextBox)sender;
            if (val.Text != "")
            {
                try
                {
                    storedRanks[listRanks.SelectedIndex].smallMaps = int.Parse(val.Text);
                }
                catch
                {
                    val.Text = "Incorrect value.";
                }
            }
        }

        void textBoxMediumMaps_TextChanged(object sender, EventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            TextBox val = (TextBox)sender;
            if (val.Text != "")
            {
                try
                {
                    storedRanks[listRanks.SelectedIndex].mediumMaps = int.Parse(val.Text);
                }
                catch
                {
                    val.Text = "Incorrect value.";
                }
            }
        }

        void textBoxBigMaps_TextChanged(object sender, EventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            TextBox val = (TextBox)sender;
            if (val.Text != "")
            {
                try
                {
                    storedRanks[listRanks.SelectedIndex].bigMaps = int.Parse(val.Text);
                }
                catch
                {
                    val.Text = "Incorrect value.";
                }
            }
        }

        void textBoxMaxMessages_TextChanged(object sender, EventArgs e) {}

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
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0038: Expected O, but got Unknown
            //IL_0039: Unknown result type (might be due to invalid IL or missing references)
            //IL_0043: Expected O, but got Unknown
            //IL_0044: Unknown result type (might be due to invalid IL or missing references)
            //IL_004e: Expected O, but got Unknown
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0059: Expected O, but got Unknown
            //IL_005a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0064: Expected O, but got Unknown
            //IL_0065: Unknown result type (might be due to invalid IL or missing references)
            //IL_006f: Expected O, but got Unknown
            //IL_0070: Unknown result type (might be due to invalid IL or missing references)
            //IL_007a: Expected O, but got Unknown
            //IL_007b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0085: Expected O, but got Unknown
            //IL_0086: Unknown result type (might be due to invalid IL or missing references)
            //IL_0090: Expected O, but got Unknown
            //IL_0091: Unknown result type (might be due to invalid IL or missing references)
            //IL_009b: Expected O, but got Unknown
            //IL_009c: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a6: Expected O, but got Unknown
            //IL_00a7: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b1: Expected O, but got Unknown
            //IL_00b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bc: Expected O, but got Unknown
            //IL_00bd: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c7: Expected O, but got Unknown
            //IL_00c8: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d2: Expected O, but got Unknown
            //IL_00d3: Unknown result type (might be due to invalid IL or missing references)
            //IL_00dd: Expected O, but got Unknown
            //IL_00de: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e8: Expected O, but got Unknown
            //IL_00e9: Unknown result type (might be due to invalid IL or missing references)
            //IL_00f3: Expected O, but got Unknown
            //IL_00f4: Unknown result type (might be due to invalid IL or missing references)
            //IL_00fe: Expected O, but got Unknown
            //IL_00ff: Unknown result type (might be due to invalid IL or missing references)
            //IL_0109: Expected O, but got Unknown
            //IL_010a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0114: Expected O, but got Unknown
            //IL_0115: Unknown result type (might be due to invalid IL or missing references)
            //IL_011f: Expected O, but got Unknown
            //IL_0120: Unknown result type (might be due to invalid IL or missing references)
            //IL_012a: Expected O, but got Unknown
            //IL_012b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0135: Expected O, but got Unknown
            //IL_0136: Unknown result type (might be due to invalid IL or missing references)
            //IL_0140: Expected O, but got Unknown
            //IL_0141: Unknown result type (might be due to invalid IL or missing references)
            //IL_014b: Expected O, but got Unknown
            //IL_014c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0156: Expected O, but got Unknown
            //IL_0157: Unknown result type (might be due to invalid IL or missing references)
            //IL_0161: Expected O, but got Unknown
            //IL_0162: Unknown result type (might be due to invalid IL or missing references)
            //IL_016c: Expected O, but got Unknown
            //IL_016d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0177: Expected O, but got Unknown
            //IL_0178: Unknown result type (might be due to invalid IL or missing references)
            //IL_0182: Expected O, but got Unknown
            //IL_0183: Unknown result type (might be due to invalid IL or missing references)
            //IL_018d: Expected O, but got Unknown
            //IL_018e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0198: Expected O, but got Unknown
            //IL_0199: Unknown result type (might be due to invalid IL or missing references)
            //IL_01a3: Expected O, but got Unknown
            //IL_01a4: Unknown result type (might be due to invalid IL or missing references)
            //IL_01ae: Expected O, but got Unknown
            //IL_01af: Unknown result type (might be due to invalid IL or missing references)
            //IL_01b9: Expected O, but got Unknown
            //IL_01ba: Unknown result type (might be due to invalid IL or missing references)
            //IL_01c4: Expected O, but got Unknown
            //IL_01c5: Unknown result type (might be due to invalid IL or missing references)
            //IL_01cf: Expected O, but got Unknown
            //IL_01d0: Unknown result type (might be due to invalid IL or missing references)
            //IL_01da: Expected O, but got Unknown
            //IL_01db: Unknown result type (might be due to invalid IL or missing references)
            //IL_01e5: Expected O, but got Unknown
            //IL_01e6: Unknown result type (might be due to invalid IL or missing references)
            //IL_01f0: Expected O, but got Unknown
            //IL_01f1: Unknown result type (might be due to invalid IL or missing references)
            //IL_01fb: Expected O, but got Unknown
            //IL_01fc: Unknown result type (might be due to invalid IL or missing references)
            //IL_0206: Expected O, but got Unknown
            //IL_0207: Unknown result type (might be due to invalid IL or missing references)
            //IL_0211: Expected O, but got Unknown
            //IL_0212: Unknown result type (might be due to invalid IL or missing references)
            //IL_021c: Expected O, but got Unknown
            //IL_021d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0227: Expected O, but got Unknown
            //IL_0228: Unknown result type (might be due to invalid IL or missing references)
            //IL_0232: Expected O, but got Unknown
            //IL_0233: Unknown result type (might be due to invalid IL or missing references)
            //IL_023d: Expected O, but got Unknown
            //IL_023e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0248: Expected O, but got Unknown
            //IL_0249: Unknown result type (might be due to invalid IL or missing references)
            //IL_0253: Expected O, but got Unknown
            //IL_0254: Unknown result type (might be due to invalid IL or missing references)
            //IL_025e: Expected O, but got Unknown
            //IL_025f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0269: Expected O, but got Unknown
            //IL_026a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0274: Expected O, but got Unknown
            //IL_0275: Unknown result type (might be due to invalid IL or missing references)
            //IL_027f: Expected O, but got Unknown
            //IL_0280: Unknown result type (might be due to invalid IL or missing references)
            //IL_028a: Expected O, but got Unknown
            //IL_028b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0295: Expected O, but got Unknown
            //IL_0296: Unknown result type (might be due to invalid IL or missing references)
            //IL_02a0: Expected O, but got Unknown
            //IL_02a1: Unknown result type (might be due to invalid IL or missing references)
            //IL_02ab: Expected O, but got Unknown
            //IL_02ac: Unknown result type (might be due to invalid IL or missing references)
            //IL_02b6: Expected O, but got Unknown
            //IL_02b7: Unknown result type (might be due to invalid IL or missing references)
            //IL_02c1: Expected O, but got Unknown
            //IL_02c2: Unknown result type (might be due to invalid IL or missing references)
            //IL_02cc: Expected O, but got Unknown
            //IL_02cd: Unknown result type (might be due to invalid IL or missing references)
            //IL_02d7: Expected O, but got Unknown
            //IL_02d8: Unknown result type (might be due to invalid IL or missing references)
            //IL_02e2: Expected O, but got Unknown
            //IL_02e3: Unknown result type (might be due to invalid IL or missing references)
            //IL_02ed: Expected O, but got Unknown
            //IL_02ee: Unknown result type (might be due to invalid IL or missing references)
            //IL_02f8: Expected O, but got Unknown
            //IL_02f9: Unknown result type (might be due to invalid IL or missing references)
            //IL_0303: Expected O, but got Unknown
            //IL_0304: Unknown result type (might be due to invalid IL or missing references)
            //IL_030e: Expected O, but got Unknown
            //IL_030f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0319: Expected O, but got Unknown
            //IL_031a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0324: Expected O, but got Unknown
            //IL_0325: Unknown result type (might be due to invalid IL or missing references)
            //IL_032f: Expected O, but got Unknown
            //IL_0330: Unknown result type (might be due to invalid IL or missing references)
            //IL_033a: Expected O, but got Unknown
            //IL_033b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0345: Expected O, but got Unknown
            //IL_0346: Unknown result type (might be due to invalid IL or missing references)
            //IL_0350: Expected O, but got Unknown
            //IL_0351: Unknown result type (might be due to invalid IL or missing references)
            //IL_035b: Expected O, but got Unknown
            //IL_035c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0366: Expected O, but got Unknown
            //IL_0367: Unknown result type (might be due to invalid IL or missing references)
            //IL_0371: Expected O, but got Unknown
            //IL_0372: Unknown result type (might be due to invalid IL or missing references)
            //IL_037c: Expected O, but got Unknown
            //IL_037d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0387: Expected O, but got Unknown
            //IL_0388: Unknown result type (might be due to invalid IL or missing references)
            //IL_0392: Expected O, but got Unknown
            //IL_0393: Unknown result type (might be due to invalid IL or missing references)
            //IL_039d: Expected O, but got Unknown
            //IL_039e: Unknown result type (might be due to invalid IL or missing references)
            //IL_03a8: Expected O, but got Unknown
            //IL_03a9: Unknown result type (might be due to invalid IL or missing references)
            //IL_03b3: Expected O, but got Unknown
            //IL_03b4: Unknown result type (might be due to invalid IL or missing references)
            //IL_03be: Expected O, but got Unknown
            //IL_03bf: Unknown result type (might be due to invalid IL or missing references)
            //IL_03c9: Expected O, but got Unknown
            //IL_03ca: Unknown result type (might be due to invalid IL or missing references)
            //IL_03d4: Expected O, but got Unknown
            //IL_03d5: Unknown result type (might be due to invalid IL or missing references)
            //IL_03df: Expected O, but got Unknown
            //IL_03e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_03ea: Expected O, but got Unknown
            //IL_03eb: Unknown result type (might be due to invalid IL or missing references)
            //IL_03f5: Expected O, but got Unknown
            //IL_03f6: Unknown result type (might be due to invalid IL or missing references)
            //IL_0400: Expected O, but got Unknown
            //IL_0401: Unknown result type (might be due to invalid IL or missing references)
            //IL_040b: Expected O, but got Unknown
            //IL_040c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0416: Expected O, but got Unknown
            //IL_0417: Unknown result type (might be due to invalid IL or missing references)
            //IL_0421: Expected O, but got Unknown
            //IL_0422: Unknown result type (might be due to invalid IL or missing references)
            //IL_042c: Expected O, but got Unknown
            //IL_042d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0437: Expected O, but got Unknown
            //IL_0438: Unknown result type (might be due to invalid IL or missing references)
            //IL_0442: Expected O, but got Unknown
            //IL_0443: Unknown result type (might be due to invalid IL or missing references)
            //IL_044d: Expected O, but got Unknown
            //IL_044e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0458: Expected O, but got Unknown
            //IL_0459: Unknown result type (might be due to invalid IL or missing references)
            //IL_0463: Expected O, but got Unknown
            //IL_0464: Unknown result type (might be due to invalid IL or missing references)
            //IL_046e: Expected O, but got Unknown
            //IL_046f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0479: Expected O, but got Unknown
            //IL_047a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0484: Expected O, but got Unknown
            //IL_0485: Unknown result type (might be due to invalid IL or missing references)
            //IL_048f: Expected O, but got Unknown
            //IL_0490: Unknown result type (might be due to invalid IL or missing references)
            //IL_049a: Expected O, but got Unknown
            //IL_049b: Unknown result type (might be due to invalid IL or missing references)
            //IL_04a5: Expected O, but got Unknown
            //IL_04a6: Unknown result type (might be due to invalid IL or missing references)
            //IL_04b0: Expected O, but got Unknown
            //IL_04b1: Unknown result type (might be due to invalid IL or missing references)
            //IL_04bb: Expected O, but got Unknown
            //IL_04bc: Unknown result type (might be due to invalid IL or missing references)
            //IL_04c6: Expected O, but got Unknown
            //IL_04c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_04d1: Expected O, but got Unknown
            //IL_04d2: Unknown result type (might be due to invalid IL or missing references)
            //IL_04dc: Expected O, but got Unknown
            //IL_04dd: Unknown result type (might be due to invalid IL or missing references)
            //IL_04e7: Expected O, but got Unknown
            //IL_04e8: Unknown result type (might be due to invalid IL or missing references)
            //IL_04f2: Expected O, but got Unknown
            //IL_04f3: Unknown result type (might be due to invalid IL or missing references)
            //IL_04fd: Expected O, but got Unknown
            //IL_04fe: Unknown result type (might be due to invalid IL or missing references)
            //IL_0508: Expected O, but got Unknown
            //IL_0509: Unknown result type (might be due to invalid IL or missing references)
            //IL_0513: Expected O, but got Unknown
            //IL_0514: Unknown result type (might be due to invalid IL or missing references)
            //IL_051e: Expected O, but got Unknown
            //IL_051f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0529: Expected O, but got Unknown
            //IL_052a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0534: Expected O, but got Unknown
            //IL_0535: Unknown result type (might be due to invalid IL or missing references)
            //IL_053f: Expected O, but got Unknown
            //IL_0540: Unknown result type (might be due to invalid IL or missing references)
            //IL_054a: Expected O, but got Unknown
            //IL_054b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0555: Expected O, but got Unknown
            //IL_0556: Unknown result type (might be due to invalid IL or missing references)
            //IL_0560: Expected O, but got Unknown
            //IL_0561: Unknown result type (might be due to invalid IL or missing references)
            //IL_056b: Expected O, but got Unknown
            //IL_056c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0576: Expected O, but got Unknown
            //IL_0577: Unknown result type (might be due to invalid IL or missing references)
            //IL_0581: Expected O, but got Unknown
            //IL_0582: Unknown result type (might be due to invalid IL or missing references)
            //IL_058c: Expected O, but got Unknown
            //IL_058d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0597: Expected O, but got Unknown
            //IL_0598: Unknown result type (might be due to invalid IL or missing references)
            //IL_05a2: Expected O, but got Unknown
            //IL_05a3: Unknown result type (might be due to invalid IL or missing references)
            //IL_05ad: Expected O, but got Unknown
            //IL_05ae: Unknown result type (might be due to invalid IL or missing references)
            //IL_05b8: Expected O, but got Unknown
            //IL_05b9: Unknown result type (might be due to invalid IL or missing references)
            //IL_05c3: Expected O, but got Unknown
            //IL_05c4: Unknown result type (might be due to invalid IL or missing references)
            //IL_05ce: Expected O, but got Unknown
            //IL_05cf: Unknown result type (might be due to invalid IL or missing references)
            //IL_05d9: Expected O, but got Unknown
            //IL_05da: Unknown result type (might be due to invalid IL or missing references)
            //IL_05e4: Expected O, but got Unknown
            //IL_05e5: Unknown result type (might be due to invalid IL or missing references)
            //IL_05ef: Expected O, but got Unknown
            //IL_05f0: Unknown result type (might be due to invalid IL or missing references)
            //IL_05fa: Expected O, but got Unknown
            //IL_05fb: Unknown result type (might be due to invalid IL or missing references)
            //IL_0605: Expected O, but got Unknown
            //IL_0606: Unknown result type (might be due to invalid IL or missing references)
            //IL_0610: Expected O, but got Unknown
            //IL_0611: Unknown result type (might be due to invalid IL or missing references)
            //IL_061b: Expected O, but got Unknown
            //IL_061c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0626: Expected O, but got Unknown
            //IL_0627: Unknown result type (might be due to invalid IL or missing references)
            //IL_0631: Expected O, but got Unknown
            //IL_0632: Unknown result type (might be due to invalid IL or missing references)
            //IL_063c: Expected O, but got Unknown
            //IL_063d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0647: Expected O, but got Unknown
            //IL_0648: Unknown result type (might be due to invalid IL or missing references)
            //IL_0652: Expected O, but got Unknown
            //IL_0653: Unknown result type (might be due to invalid IL or missing references)
            //IL_065d: Expected O, but got Unknown
            //IL_065e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0668: Expected O, but got Unknown
            //IL_0669: Unknown result type (might be due to invalid IL or missing references)
            //IL_0673: Expected O, but got Unknown
            //IL_0674: Unknown result type (might be due to invalid IL or missing references)
            //IL_067e: Expected O, but got Unknown
            //IL_067f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0689: Expected O, but got Unknown
            //IL_068a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0694: Expected O, but got Unknown
            //IL_0695: Unknown result type (might be due to invalid IL or missing references)
            //IL_069f: Expected O, but got Unknown
            //IL_06a0: Unknown result type (might be due to invalid IL or missing references)
            //IL_06aa: Expected O, but got Unknown
            //IL_06ab: Unknown result type (might be due to invalid IL or missing references)
            //IL_06b5: Expected O, but got Unknown
            //IL_06b6: Unknown result type (might be due to invalid IL or missing references)
            //IL_06c0: Expected O, but got Unknown
            //IL_06c1: Unknown result type (might be due to invalid IL or missing references)
            //IL_06cb: Expected O, but got Unknown
            //IL_06cc: Unknown result type (might be due to invalid IL or missing references)
            //IL_06d6: Expected O, but got Unknown
            //IL_06d7: Unknown result type (might be due to invalid IL or missing references)
            //IL_06e1: Expected O, but got Unknown
            //IL_06e2: Unknown result type (might be due to invalid IL or missing references)
            //IL_06ec: Expected O, but got Unknown
            //IL_06ed: Unknown result type (might be due to invalid IL or missing references)
            //IL_06f7: Expected O, but got Unknown
            //IL_06f8: Unknown result type (might be due to invalid IL or missing references)
            //IL_0702: Expected O, but got Unknown
            //IL_0703: Unknown result type (might be due to invalid IL or missing references)
            //IL_070d: Expected O, but got Unknown
            //IL_070e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0718: Expected O, but got Unknown
            //IL_0719: Unknown result type (might be due to invalid IL or missing references)
            //IL_0723: Expected O, but got Unknown
            //IL_0724: Unknown result type (might be due to invalid IL or missing references)
            //IL_072e: Expected O, but got Unknown
            //IL_072f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0739: Expected O, but got Unknown
            //IL_073a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0744: Expected O, but got Unknown
            //IL_0745: Unknown result type (might be due to invalid IL or missing references)
            //IL_074f: Expected O, but got Unknown
            //IL_0750: Unknown result type (might be due to invalid IL or missing references)
            //IL_075a: Expected O, but got Unknown
            //IL_075b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0765: Expected O, but got Unknown
            //IL_0766: Unknown result type (might be due to invalid IL or missing references)
            //IL_0770: Expected O, but got Unknown
            //IL_0771: Unknown result type (might be due to invalid IL or missing references)
            //IL_077b: Expected O, but got Unknown
            //IL_077c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0786: Expected O, but got Unknown
            //IL_0787: Unknown result type (might be due to invalid IL or missing references)
            //IL_0791: Expected O, but got Unknown
            //IL_0792: Unknown result type (might be due to invalid IL or missing references)
            //IL_079c: Expected O, but got Unknown
            //IL_079d: Unknown result type (might be due to invalid IL or missing references)
            //IL_07a7: Expected O, but got Unknown
            //IL_07a8: Unknown result type (might be due to invalid IL or missing references)
            //IL_07b2: Expected O, but got Unknown
            //IL_07b3: Unknown result type (might be due to invalid IL or missing references)
            //IL_07bd: Expected O, but got Unknown
            //IL_07be: Unknown result type (might be due to invalid IL or missing references)
            //IL_07c8: Expected O, but got Unknown
            //IL_07c9: Unknown result type (might be due to invalid IL or missing references)
            //IL_07d3: Expected O, but got Unknown
            //IL_07d4: Unknown result type (might be due to invalid IL or missing references)
            //IL_07de: Expected O, but got Unknown
            //IL_07df: Unknown result type (might be due to invalid IL or missing references)
            //IL_07e9: Expected O, but got Unknown
            //IL_07ea: Unknown result type (might be due to invalid IL or missing references)
            //IL_07f4: Expected O, but got Unknown
            //IL_07f5: Unknown result type (might be due to invalid IL or missing references)
            //IL_07ff: Expected O, but got Unknown
            //IL_0800: Unknown result type (might be due to invalid IL or missing references)
            //IL_080a: Expected O, but got Unknown
            //IL_080b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0815: Expected O, but got Unknown
            //IL_0816: Unknown result type (might be due to invalid IL or missing references)
            //IL_0820: Expected O, but got Unknown
            //IL_0821: Unknown result type (might be due to invalid IL or missing references)
            //IL_082b: Expected O, but got Unknown
            //IL_082c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0836: Expected O, but got Unknown
            //IL_0837: Unknown result type (might be due to invalid IL or missing references)
            //IL_0841: Expected O, but got Unknown
            //IL_0842: Unknown result type (might be due to invalid IL or missing references)
            //IL_084c: Expected O, but got Unknown
            //IL_084d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0857: Expected O, but got Unknown
            //IL_0858: Unknown result type (might be due to invalid IL or missing references)
            //IL_0862: Expected O, but got Unknown
            //IL_0863: Unknown result type (might be due to invalid IL or missing references)
            //IL_086d: Expected O, but got Unknown
            //IL_086e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0878: Expected O, but got Unknown
            //IL_0879: Unknown result type (might be due to invalid IL or missing references)
            //IL_0883: Expected O, but got Unknown
            //IL_0884: Unknown result type (might be due to invalid IL or missing references)
            //IL_088e: Expected O, but got Unknown
            //IL_088f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0899: Expected O, but got Unknown
            //IL_089a: Unknown result type (might be due to invalid IL or missing references)
            //IL_08a4: Expected O, but got Unknown
            //IL_08a5: Unknown result type (might be due to invalid IL or missing references)
            //IL_08af: Expected O, but got Unknown
            //IL_08b0: Unknown result type (might be due to invalid IL or missing references)
            //IL_08ba: Expected O, but got Unknown
            //IL_08bb: Unknown result type (might be due to invalid IL or missing references)
            //IL_08c5: Expected O, but got Unknown
            //IL_08c6: Unknown result type (might be due to invalid IL or missing references)
            //IL_08d0: Expected O, but got Unknown
            //IL_08d1: Unknown result type (might be due to invalid IL or missing references)
            //IL_08db: Expected O, but got Unknown
            //IL_08dc: Unknown result type (might be due to invalid IL or missing references)
            //IL_08e6: Expected O, but got Unknown
            //IL_08e7: Unknown result type (might be due to invalid IL or missing references)
            //IL_08f1: Expected O, but got Unknown
            //IL_08f2: Unknown result type (might be due to invalid IL or missing references)
            //IL_08fc: Expected O, but got Unknown
            //IL_08fd: Unknown result type (might be due to invalid IL or missing references)
            //IL_0907: Expected O, but got Unknown
            //IL_0908: Unknown result type (might be due to invalid IL or missing references)
            //IL_0912: Expected O, but got Unknown
            //IL_0913: Unknown result type (might be due to invalid IL or missing references)
            //IL_091d: Expected O, but got Unknown
            //IL_091e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0928: Expected O, but got Unknown
            //IL_0929: Unknown result type (might be due to invalid IL or missing references)
            //IL_0933: Expected O, but got Unknown
            //IL_0934: Unknown result type (might be due to invalid IL or missing references)
            //IL_093e: Expected O, but got Unknown
            //IL_093f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0949: Expected O, but got Unknown
            //IL_094a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0954: Expected O, but got Unknown
            //IL_0955: Unknown result type (might be due to invalid IL or missing references)
            //IL_095f: Expected O, but got Unknown
            //IL_0960: Unknown result type (might be due to invalid IL or missing references)
            //IL_096a: Expected O, but got Unknown
            //IL_096b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0975: Expected O, but got Unknown
            //IL_0976: Unknown result type (might be due to invalid IL or missing references)
            //IL_0980: Expected O, but got Unknown
            //IL_0981: Unknown result type (might be due to invalid IL or missing references)
            //IL_098b: Expected O, but got Unknown
            //IL_098c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0996: Expected O, but got Unknown
            //IL_0997: Unknown result type (might be due to invalid IL or missing references)
            //IL_09a1: Expected O, but got Unknown
            //IL_09a2: Unknown result type (might be due to invalid IL or missing references)
            //IL_09ac: Expected O, but got Unknown
            //IL_09ad: Unknown result type (might be due to invalid IL or missing references)
            //IL_09b7: Expected O, but got Unknown
            //IL_09b8: Unknown result type (might be due to invalid IL or missing references)
            //IL_09c2: Expected O, but got Unknown
            //IL_09c3: Unknown result type (might be due to invalid IL or missing references)
            //IL_09cd: Expected O, but got Unknown
            //IL_09ce: Unknown result type (might be due to invalid IL or missing references)
            //IL_09d8: Expected O, but got Unknown
            //IL_09d9: Unknown result type (might be due to invalid IL or missing references)
            //IL_09e3: Expected O, but got Unknown
            //IL_09e4: Unknown result type (might be due to invalid IL or missing references)
            //IL_09ee: Expected O, but got Unknown
            //IL_09ef: Unknown result type (might be due to invalid IL or missing references)
            //IL_09f9: Expected O, but got Unknown
            //IL_09fa: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a04: Expected O, but got Unknown
            //IL_0a05: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a0f: Expected O, but got Unknown
            //IL_0a10: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a1a: Expected O, but got Unknown
            //IL_0a1b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a25: Expected O, but got Unknown
            //IL_0a26: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a30: Expected O, but got Unknown
            //IL_0a31: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a3b: Expected O, but got Unknown
            //IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a46: Expected O, but got Unknown
            //IL_0a47: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a51: Expected O, but got Unknown
            //IL_0a52: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a5c: Expected O, but got Unknown
            //IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a67: Expected O, but got Unknown
            //IL_0a68: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a72: Expected O, but got Unknown
            //IL_0a73: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a7d: Expected O, but got Unknown
            //IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a88: Expected O, but got Unknown
            //IL_0a89: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a93: Expected O, but got Unknown
            //IL_0a94: Unknown result type (might be due to invalid IL or missing references)
            //IL_0a9e: Expected O, but got Unknown
            //IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
            //IL_0aaf: Expected O, but got Unknown
            //IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
            //IL_0aba: Expected O, but got Unknown
            //IL_0abb: Unknown result type (might be due to invalid IL or missing references)
            //IL_0ac5: Expected O, but got Unknown
            //IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
            //IL_0ad0: Expected O, but got Unknown
            //IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
            //IL_0adb: Expected O, but got Unknown
            //IL_0adc: Unknown result type (might be due to invalid IL or missing references)
            //IL_0ae6: Expected O, but got Unknown
            //IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
            //IL_0af1: Expected O, but got Unknown
            //IL_0af2: Unknown result type (might be due to invalid IL or missing references)
            //IL_0afc: Expected O, but got Unknown
            //IL_0afd: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b07: Expected O, but got Unknown
            //IL_0b08: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b12: Expected O, but got Unknown
            //IL_0b13: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b1d: Expected O, but got Unknown
            //IL_0b1e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b28: Expected O, but got Unknown
            //IL_0b29: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b33: Expected O, but got Unknown
            //IL_0b34: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b3e: Expected O, but got Unknown
            //IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b49: Expected O, but got Unknown
            //IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b54: Expected O, but got Unknown
            //IL_0b55: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b5f: Expected O, but got Unknown
            //IL_0b60: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b6a: Expected O, but got Unknown
            //IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b75: Expected O, but got Unknown
            //IL_0b76: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b80: Expected O, but got Unknown
            //IL_0b81: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b8b: Expected O, but got Unknown
            //IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0b96: Expected O, but got Unknown
            //IL_0b97: Unknown result type (might be due to invalid IL or missing references)
            //IL_0ba1: Expected O, but got Unknown
            //IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
            //IL_0bac: Expected O, but got Unknown
            //IL_0de5: Unknown result type (might be due to invalid IL or missing references)
            //IL_0def: Expected O, but got Unknown
            //IL_1165: Unknown result type (might be due to invalid IL or missing references)
            //IL_2402: Unknown result type (might be due to invalid IL or missing references)
            //IL_240c: Expected O, but got Unknown
            //IL_3377: Unknown result type (might be due to invalid IL or missing references)
            //IL_354e: Unknown result type (might be due to invalid IL or missing references)
            //IL_3558: Expected O, but got Unknown
            //IL_360e: Unknown result type (might be due to invalid IL or missing references)
            //IL_3618: Expected O, but got Unknown
            //IL_396e: Unknown result type (might be due to invalid IL or missing references)
            //IL_3978: Expected O, but got Unknown
            //IL_3a48: Unknown result type (might be due to invalid IL or missing references)
            //IL_3a52: Expected O, but got Unknown
            //IL_3bb8: Unknown result type (might be due to invalid IL or missing references)
            //IL_3c1b: Unknown result type (might be due to invalid IL or missing references)
            //IL_3c25: Expected O, but got Unknown
            //IL_4021: Unknown result type (might be due to invalid IL or missing references)
            //IL_424e: Unknown result type (might be due to invalid IL or missing references)
            //IL_4258: Expected O, but got Unknown
            //IL_42dc: Unknown result type (might be due to invalid IL or missing references)
            //IL_42e6: Expected O, but got Unknown
            //IL_4367: Unknown result type (might be due to invalid IL or missing references)
            //IL_4371: Expected O, but got Unknown
            //IL_4442: Unknown result type (might be due to invalid IL or missing references)
            //IL_444c: Expected O, but got Unknown
            //IL_44b8: Unknown result type (might be due to invalid IL or missing references)
            //IL_44c2: Expected O, but got Unknown
            //IL_452e: Unknown result type (might be due to invalid IL or missing references)
            //IL_4538: Expected O, but got Unknown
            //IL_4801: Unknown result type (might be due to invalid IL or missing references)
            //IL_480b: Expected O, but got Unknown
            //IL_4fa7: Unknown result type (might be due to invalid IL or missing references)
            //IL_552f: Unknown result type (might be due to invalid IL or missing references)
            //IL_5973: Unknown result type (might be due to invalid IL or missing references)
            //IL_5eb6: Unknown result type (might be due to invalid IL or missing references)
            //IL_64c5: Unknown result type (might be due to invalid IL or missing references)
            //IL_77d6: Unknown result type (might be due to invalid IL or missing references)
            //IL_7bda: Unknown result type (might be due to invalid IL or missing references)
            //IL_8b98: Unknown result type (might be due to invalid IL or missing references)
            //IL_8ba2: Expected O, but got Unknown
            //IL_8c32: Unknown result type (might be due to invalid IL or missing references)
            //IL_8c3c: Expected O, but got Unknown
            //IL_8ccc: Unknown result type (might be due to invalid IL or missing references)
            //IL_8cd6: Expected O, but got Unknown
            //IL_8db5: Unknown result type (might be due to invalid IL or missing references)
            //IL_8dbf: Expected O, but got Unknown
            //IL_8ef5: Unknown result type (might be due to invalid IL or missing references)
            //IL_8f98: Unknown result type (might be due to invalid IL or missing references)
            //IL_90d6: Unknown result type (might be due to invalid IL or missing references)
            //IL_97c9: Unknown result type (might be due to invalid IL or missing references)
            //IL_99c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_9b7b: Unknown result type (might be due to invalid IL or missing references)
            //IL_9b85: Expected O, but got Unknown
            //IL_9bb8: Unknown result type (might be due to invalid IL or missing references)
            //IL_9bc2: Expected O, but got Unknown
            components = new Container();
            GeneralSettings generalSettings = new GeneralSettings();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PropertyWindow));
            LavaSettings lavaSettings = new LavaSettings();
            InfectionSettings infectionSettings = new InfectionSettings();
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            autoFlagCheck = new CheckBox();
            label41 = new Label();
            flagBox = new TextBox();
            label40 = new Label();
            descriptionBox = new TextBox();
            cmbOpChat = new ComboBox();
            lblOpChat = new Label();
            cmbDefaultRank = new ComboBox();
            label29 = new Label();
            lblDefault = new Label();
            cmbDefaultColour = new ComboBox();
            chkRestart = new CheckBox();
            chkPublic = new CheckBox();
            chkAutoload = new CheckBox();
            chkWorld = new CheckBox();
            chkVerify = new CheckBox();
            label10 = new Label();
            label27 = new Label();
            label22 = new Label();
            label21 = new Label();
            label30 = new Label();
            label3 = new Label();
            txtDepth = new TextBox();
            txtMain = new TextBox();
            txtMaps = new TextBox();
            txtPlayers = new TextBox();
            txtHost = new TextBox();
            txtPort = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtMOTD = new TextBox();
            txtName = new TextBox();
            ChkTunnels = new CheckBox();
            label7 = new Label();
            tabPage4 = new TabPage();
            chkRepeatMessages = new CheckBox();
            chkForceCuboid = new CheckBox();
            txtShutdown = new TextBox();
            txtBanMessage = new TextBox();
            chkShutdown = new CheckBox();
            chkBanMessage = new CheckBox();
            chkrankSuper = new CheckBox();
            chkCheap = new CheckBox();
            chkDeath = new CheckBox();
            chk17Dollar = new CheckBox();
            chkPhysicsRest = new CheckBox();
            chkSmile = new CheckBox();
            chkHelp = new CheckBox();
            label28 = new Label();
            label24 = new Label();
            txtNormRp = new TextBox();
            txtRP = new TextBox();
            label34 = new Label();
            label26 = new Label();
            label25 = new Label();
            txtAFKKick = new TextBox();
            txtafk = new TextBox();
            label9 = new Label();
            txtBackup = new TextBox();
            label32 = new Label();
            txtBackupLocation = new TextBox();
            txtMoneys = new TextBox();
            txtCheap = new TextBox();
            txtRestartTime = new TextBox();
            chkRestartTime = new CheckBox();
            tabPage6 = new TabPage();
            label39 = new Label();
            vipAdd = new Button();
            vipRemove = new Button();
            vipEntry = new TextBox();
            vipLabel = new Label();
            vipList = new ListBox();
            label35 = new Label();
            label36 = new Label();
            serverMessageInterval = new TextBox();
            label37 = new Label();
            label38 = new Label();
            serverMessage = new TextBox();
            btnSetServerMessage = new Button();
            updatePanel = new Panel();
            updateLabel = new Label();
            FlowControl = new HScrollBar();
            PositionDelayUpdate = new Button();
            txtPositionDelay = new TextBox();
            PositionDelay = new Label();
            misc3 = new TabPage();
            generalProperties = new PropertyGrid();
            tabPage2 = new TabPage();
            label90 = new Label();
            label89 = new Label();
            label88 = new Label();
            label87 = new Label();
            label86 = new Label();
            label85 = new Label();
            textBoxBigMaps = new TextBox();
            textBoxMediumMaps = new TextBox();
            textBoxSmallMaps = new TextBox();
            label84 = new Label();
            label83 = new Label();
            label81 = new Label();
            label82 = new Label();
            label33 = new Label();
            txtPromotionPrice = new TextBox();
            cmbColor = new ComboBox();
            label16 = new Label();
            txtFileName = new TextBox();
            label14 = new Label();
            txtLimit = new TextBox();
            label13 = new Label();
            txtPermission = new TextBox();
            label12 = new Label();
            txtRankName = new TextBox();
            label11 = new Label();
            button1 = new Button();
            btnAddRank = new Button();
            listRanks = new ListBox();
            tabPage3 = new TabPage();
            tabControl3 = new TabControl();
            tabPage15 = new TabPage();
            listCommands = new ListBox();
            textBoxCmdShortcut = new TextBox();
            label8 = new Label();
            label76 = new Label();
            label15 = new Label();
            btnCmdHelp = new Button();
            label17 = new Label();
            txtCmdRanks = new TextBox();
            txtCmdDisallow = new TextBox();
            txtCmdAllow = new TextBox();
            txtCmdLowest = new TextBox();
            tabPage16 = new TabPage();
            label80 = new Label();
            textBoxCmdWarning = new TextBox();
            checkBoxCmdCooldown = new CheckBox();
            label77 = new Label();
            textBoxCmdMax = new TextBox();
            textBoxCmdMaxSeconds = new TextBox();
            label78 = new Label();
            label79 = new Label();
            tabPage5 = new TabPage();
            btnBlHelp = new Button();
            txtBlRanks = new TextBox();
            txtBlAllow = new TextBox();
            txtBlLowest = new TextBox();
            txtBlDisallow = new TextBox();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            listBlocks = new ListBox();
            tabPage12 = new TabPage();
            txtChannel = new TextBox();
            label23 = new Label();
            chkIRC = new CheckBox();
            cmbIRCColour = new ComboBox();
            lblIRC = new Label();
            label31 = new Label();
            txtNick = new TextBox();
            txtOpChannel = new TextBox();
            txtIRCServer = new TextBox();
            chkUpdates = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            label6 = new Label();
            tabPage14 = new TabPage();
            tabControlChat = new TabControl();
            tabPageChat1 = new TabPage();
            checkBoxChatFilterAdvanced = new CheckBox();
            label75 = new Label();
            label74 = new Label();
            label73 = new Label();
            label72 = new Label();
            groupBox3 = new GroupBox();
            checkBoxRemoveCaps = new CheckBox();
            checkBoxShortenRepetitions = new CheckBox();
            checkBoxRemoveBadWords = new CheckBox();
            checkBoxMessagesCooldown = new CheckBox();
            button3 = new Button();
            tabPageChatBadWords = new TabPage();
            button2 = new Button();
            radioButtonBadLanguage3 = new RadioButton();
            radioButtonBadLanguage2 = new RadioButton();
            radioButtonBadLanguage1 = new RadioButton();
            label69 = new Label();
            label68 = new Label();
            textBoxBadLanguageKickMsg = new TextBox();
            label67 = new Label();
            textBoxBadLanguageWarning = new TextBox();
            label55 = new Label();
            textBox13 = new TextBox();
            label62 = new Label();
            textBoxBadLanguageKickLimit = new TextBox();
            comboBoxDetectionLevel = new ComboBox();
            button6 = new Button();
            checkBoxRemoveBadWords1 = new CheckBox();
            label53 = new Label();
            label54 = new Label();
            textBoxBadLanguageSubstitution = new TextBox();
            tabPageChatCaps = new TabPage();
            label50 = new Label();
            textBox6 = new TextBox();
            textBoxMaxCaps = new TextBox();
            label43 = new Label();
            checkBoxRemoveCaps1 = new CheckBox();
            tabPageChatCharSpam = new TabPage();
            label66 = new Label();
            radioButtonCharSpam3 = new RadioButton();
            radioButtonCharSpam1 = new RadioButton();
            radioButtonCharSpam2 = new RadioButton();
            label65 = new Label();
            textBoxCharSpamWarning = new TextBox();
            label63 = new Label();
            textBox12 = new TextBox();
            textBoxCharSpamSubstitution = new TextBox();
            label52 = new Label();
            checkBoxShortenRepetitions1 = new CheckBox();
            label51 = new Label();
            textBoxMaxChars = new TextBox();
            textBoxMaxIllegalGroups = new TextBox();
            label47 = new Label();
            tabPageChatSpam = new TabPage();
            checkBoxTempMute = new CheckBox();
            textBoxDuplicateMessagesWarning = new TextBox();
            label71 = new Label();
            label70 = new Label();
            textBoxMaxMessagesWarning = new TextBox();
            label64 = new Label();
            textBox14 = new TextBox();
            label61 = new Label();
            textBoxDuplicateMessagesSeconds = new TextBox();
            label60 = new Label();
            checkBoxMessagesCooldown1 = new CheckBox();
            label59 = new Label();
            textBoxMaxMessages = new TextBox();
            textBoxMaxMessagesSeconds = new TextBox();
            label56 = new Label();
            label58 = new Label();
            label57 = new Label();
            textBoxDuplicateMessages = new TextBox();
            btnSave = new Button();
            btnDiscard = new Button();
            btnApply = new Button();
            toolTip = new ToolTip(components);
            tabControl1 = new TabControl();
            tabPage8 = new TabPage();
            tabPage9 = new TabPage();
            tabControl2 = new TabControl();
            tabPage11 = new TabPage();
            groupBox2 = new GroupBox();
            label44 = new Label();
            useHeaven = new CheckBox();
            label42 = new Label();
            heavenMapName = new TextBox();
            setHeavenMapButton = new Button();
            groupBox1 = new GroupBox();
            label45 = new Label();
            label46 = new Label();
            updateTimeSettingsButton = new Button();
            txtTime2 = new TextBox();
            label48 = new Label();
            label49 = new Label();
            txtTime1 = new TextBox();
            tabPage13 = new TabPage();
            lavaPropertyGrid = new PropertyGrid();
            tabPage10 = new TabPage();
            zombiePropertyGrid = new PropertyGrid();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage6.SuspendLayout();
            updatePanel.SuspendLayout();
            misc3.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabControl3.SuspendLayout();
            tabPage15.SuspendLayout();
            tabPage16.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage12.SuspendLayout();
            tabPage14.SuspendLayout();
            tabControlChat.SuspendLayout();
            tabPageChat1.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPageChatBadWords.SuspendLayout();
            tabPageChatCaps.SuspendLayout();
            tabPageChatCharSpam.SuspendLayout();
            tabPageChatSpam.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage8.SuspendLayout();
            tabPage9.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage11.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage13.SuspendLayout();
            tabPage10.SuspendLayout();
            SuspendLayout();
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage4);
            tabControl.Controls.Add(tabPage6);
            tabControl.Controls.Add(misc3);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Controls.Add(tabPage5);
            tabControl.Controls.Add(tabPage12);
            tabControl.Controls.Add(tabPage14);
            tabControl.Cursor = Cursors.Default;
            tabControl.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(6, 7);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(595, 413);
            tabControl.TabIndex = 0;
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(autoFlagCheck);
            tabPage1.Controls.Add(label41);
            tabPage1.Controls.Add(flagBox);
            tabPage1.Controls.Add(label40);
            tabPage1.Controls.Add(descriptionBox);
            tabPage1.Controls.Add(cmbOpChat);
            tabPage1.Controls.Add(lblOpChat);
            tabPage1.Controls.Add(cmbDefaultRank);
            tabPage1.Controls.Add(label29);
            tabPage1.Controls.Add(lblDefault);
            tabPage1.Controls.Add(cmbDefaultColour);
            tabPage1.Controls.Add(chkRestart);
            tabPage1.Controls.Add(chkPublic);
            tabPage1.Controls.Add(chkAutoload);
            tabPage1.Controls.Add(chkWorld);
            tabPage1.Controls.Add(chkVerify);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label27);
            tabPage1.Controls.Add(label22);
            tabPage1.Controls.Add(label21);
            tabPage1.Controls.Add(label30);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(txtDepth);
            tabPage1.Controls.Add(txtMain);
            tabPage1.Controls.Add(txtMaps);
            tabPage1.Controls.Add(txtPlayers);
            tabPage1.Controls.Add(txtHost);
            tabPage1.Controls.Add(txtPort);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(txtMOTD);
            tabPage1.Controls.Add(txtName);
            tabPage1.Controls.Add(ChkTunnels);
            tabPage1.Controls.Add(label7);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(587, 387);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Server";
            tabPage1.Click += tabPage1_Click;
            autoFlagCheck.AutoSize = true;
            autoFlagCheck.Location = new Point(244, 101);
            autoFlagCheck.Name = "autoFlagCheck";
            autoFlagCheck.Size = new Size(147, 17);
            autoFlagCheck.TabIndex = 29;
            autoFlagCheck.Text = "Flag based on game mode";
            autoFlagCheck.UseVisualStyleBackColor = true;
            autoFlagCheck.CheckedChanged += autoFlagCheck_CheckedChanged;
            label41.AutoSize = true;
            label41.Location = new Point(20, 102);
            label41.Name = "label41";
            label41.Size = new Size(29, 13);
            label41.TabIndex = 28;
            label41.Text = "Flag:";
            flagBox.Location = new Point(93, 99);
            flagBox.Name = "flagBox";
            flagBox.Size = new Size(135, 21);
            flagBox.TabIndex = 27;
            toolTip.SetToolTip(
                flagBox,
                "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any hacks at all and use \"-fly\" and whatnot to disallow other things.");
            flagBox.TextChanged += flagBox_TextChanged;
            label40.AutoSize = true;
            label40.Location = new Point(20, 76);
            label40.Name = "label40";
            label40.Size = new Size(64, 13);
            label40.TabIndex = 26;
            label40.Text = "Description:";
            descriptionBox.Location = new Point(93, 73);
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(461, 21);
            descriptionBox.TabIndex = 25;
            toolTip.SetToolTip(
                descriptionBox,
                "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any hacks at all and use \"-fly\" and whatnot to disallow other things.");
            cmbOpChat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpChat.FormattingEnabled = true;
            cmbOpChat.Location = new Point(472, 263);
            cmbOpChat.Name = "cmbOpChat";
            cmbOpChat.Size = new Size(81, 21);
            cmbOpChat.TabIndex = 23;
            toolTip.SetToolTip(cmbOpChat, "Default rank required to read op chat.");
            lblOpChat.AutoSize = true;
            lblOpChat.Location = new Point(399, 266);
            lblOpChat.Name = "lblOpChat";
            lblOpChat.Size = new Size(70, 13);
            lblOpChat.TabIndex = 22;
            lblOpChat.Text = "Op Chat rank:";
            cmbDefaultRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultRank.FormattingEnabled = true;
            cmbDefaultRank.Location = new Point(473, 290);
            cmbDefaultRank.Name = "cmbDefaultRank";
            cmbDefaultRank.Size = new Size(81, 21);
            cmbDefaultRank.TabIndex = 21;
            toolTip.SetToolTip(cmbDefaultRank, "Default rank assigned to new visitors to the server.");
            label29.AutoSize = true;
            label29.Location = new Point(399, 293);
            label29.Name = "label29";
            label29.Size = new Size(68, 13);
            label29.TabIndex = 20;
            label29.Text = "Default rank:";
            lblDefault.Location = new Point(539, 239);
            lblDefault.Name = "lblDefault";
            lblDefault.Size = new Size(15, 15);
            lblDefault.TabIndex = 10;
            cmbDefaultColour.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultColour.FormattingEnabled = true;
            cmbDefaultColour.Location = new Point(472, 236);
            cmbDefaultColour.Name = "cmbDefaultColour";
            cmbDefaultColour.Size = new Size(57, 21);
            cmbDefaultColour.TabIndex = 9;
            toolTip.SetToolTip(cmbDefaultColour, "The colour of the default chat used in the server.\nFor example, when you are asked to select two corners in a cuboid.");
            cmbDefaultColour.SelectedIndexChanged += cmbDefaultColour_SelectedIndexChanged;
            chkRestart.Appearance = Appearance.Button;
            chkRestart.AutoSize = true;
            chkRestart.Location = new Point(410, 348);
            chkRestart.Name = "chkRestart";
            chkRestart.Size = new Size(153, 23);
            chkRestart.TabIndex = 4;
            chkRestart.Text = "Restart when an error occurs";
            chkRestart.UseVisualStyleBackColor = true;
            chkRestart.CheckedChanged += chkRestart_CheckedChanged;
            chkPublic.Appearance = Appearance.Button;
            chkPublic.AutoSize = true;
            chkPublic.Location = new Point(165, 145);
            chkPublic.Name = "chkPublic";
            chkPublic.Size = new Size(46, 23);
            chkPublic.TabIndex = 4;
            chkPublic.Text = "Public";
            toolTip.SetToolTip(chkPublic, "Whether or not the server will appear on the server list.");
            chkPublic.UseVisualStyleBackColor = true;
            chkPublic.CheckedChanged += chkPublic_CheckedChanged;
            chkAutoload.Appearance = Appearance.Button;
            chkAutoload.AutoSize = true;
            chkAutoload.Location = new Point(191, 202);
            chkAutoload.Name = "chkAutoload";
            chkAutoload.Size = new Size(104, 23);
            chkAutoload.TabIndex = 4;
            chkAutoload.Text = "Load map on /goto";
            toolTip.SetToolTip(chkAutoload, "Load a map when a user wishes to go to it, and unload empty maps");
            chkAutoload.UseVisualStyleBackColor = true;
            chkAutoload.CheckedChanged += chkAutoload_CheckedChanged;
            chkWorld.Appearance = Appearance.Button;
            chkWorld.AutoSize = true;
            chkWorld.Location = new Point(472, 203);
            chkWorld.Name = "chkWorld";
            chkWorld.Size = new Size(69, 23);
            chkWorld.TabIndex = 4;
            chkWorld.Text = "World chat";
            toolTip.SetToolTip(chkWorld, "If disabled, every map has isolated chat.\nIf enabled, every map is able to communicate without special letters.");
            chkWorld.UseVisualStyleBackColor = true;
            chkWorld.CheckedChanged += chkWorld_CheckedChanged;
            chkVerify.AutoSize = true;
            chkVerify.Location = new Point(19, 278);
            chkVerify.Name = "chkVerify";
            chkVerify.Size = new Size(171, 17);
            chkVerify.TabIndex = 4;
            chkVerify.Text = "Verify Names - keep it checked!";
            toolTip.SetToolTip(chkVerify, "Make sure the user is who they claim to be.");
            chkVerify.UseVisualStyleBackColor = true;
            chkVerify.CheckedChanged += chkVerify_CheckedChanged;
            label10.AutoSize = true;
            label10.Location = new Point(399, 239);
            label10.Name = "label10";
            label10.Size = new Size(71, 13);
            label10.TabIndex = 3;
            label10.Text = "Default color:";
            label27.AutoSize = true;
            label27.Location = new Point(16, 234);
            label27.Name = "label27";
            label27.Size = new Size(86, 13);
            label27.TabIndex = 3;
            label27.Text = "Main map name:";
            label22.AutoSize = true;
            label22.Location = new Point(45, 207);
            label22.Name = "label22";
            label22.Size = new Size(58, 13);
            label22.TabIndex = 3;
            label22.Text = "Max Maps:";
            label21.AutoSize = true;
            label21.Location = new Point(432, 102);
            label21.Name = "label21";
            label21.Size = new Size(67, 13);
            label21.TabIndex = 3;
            label21.Text = "Max Players:";
            label30.AutoSize = true;
            label30.Location = new Point(375, 150);
            label30.Name = "label30";
            label30.Size = new Size(95, 13);
            label30.TabIndex = 3;
            label30.Text = "Default host state:";
            label3.AutoSize = true;
            label3.Location = new Point(22, 148);
            label3.Name = "label3";
            label3.Size = new Size(62, 13);
            label3.TabIndex = 3;
            label3.Text = "Server port:";
            txtDepth.Location = new Point(159, 333);
            txtDepth.Name = "txtDepth";
            txtDepth.Size = new Size(41, 21);
            txtDepth.TabIndex = 2;
            toolTip.SetToolTip(txtDepth, "Depth which guests can dig.\nDefault = 4");
            txtDepth.TextChanged += txtDepth_TextChanged;
            txtMain.Location = new Point(109, 231);
            txtMain.Name = "txtMain";
            txtMain.Size = new Size(60, 21);
            txtMain.TabIndex = 2;
            txtMain.TextChanged += txtMaps_TextChanged;
            txtMaps.Location = new Point(109, 204);
            txtMaps.Name = "txtMaps";
            txtMaps.Size = new Size(60, 21);
            txtMaps.TabIndex = 2;
            toolTip.SetToolTip(txtMaps, "The total number of maps which can be loaded at once.\nDefault = 5");
            txtMaps.TextChanged += txtMaps_TextChanged;
            txtPlayers.Location = new Point(507, 99);
            txtPlayers.Name = "txtPlayers";
            txtPlayers.Size = new Size(46, 21);
            txtPlayers.TabIndex = 2;
            toolTip.SetToolTip(txtPlayers, "The total number of players which can login.\nDefault = 12");
            txtPlayers.TextChanged += txtPlayers_TextChanged;
            txtHost.Location = new Point(476, 147);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(75, 21);
            txtHost.TabIndex = 2;
            txtHost.TextChanged += txtPort_TextChanged;
            txtPort.Location = new Point(90, 145);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(60, 21);
            txtPort.TabIndex = 2;
            toolTip.SetToolTip(txtPort, "The port that the server will output on.\nDefault = 25565\n\nChanging will reset your ExternalURL.");
            txtPort.TextChanged += txtPort_TextChanged;
            label2.AutoSize = true;
            label2.Location = new Point(20, 50);
            label2.Name = "label2";
            label2.Size = new Size(38, 13);
            label2.TabIndex = 1;
            label2.Text = "MOTD:";
            label1.AutoSize = true;
            label1.Location = new Point(20, 24);
            label1.Name = "label1";
            label1.Size = new Size(38, 13);
            label1.TabIndex = 1;
            label1.Text = "Name:";
            txtMOTD.Location = new Point(64, 47);
            txtMOTD.Name = "txtMOTD";
            txtMOTD.Size = new Size(490, 21);
            txtMOTD.TabIndex = 0;
            toolTip.SetToolTip(
                txtMOTD,
                "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any hacks at all and use \"-fly\" and whatnot to disallow other things.");
            txtMOTD.TextChanged += txtMOTD_TextChanged;
            txtName.Location = new Point(64, 21);
            txtName.Name = "txtName";
            txtName.Size = new Size(490, 21);
            txtName.TabIndex = 0;
            toolTip.SetToolTip(txtName, "The name of the server.\nPick something good!");
            txtName.TextChanged += txtName_TextChanged;
            ChkTunnels.Appearance = Appearance.Button;
            ChkTunnels.AutoSize = true;
            ChkTunnels.Location = new Point(25, 334);
            ChkTunnels.Name = "ChkTunnels";
            ChkTunnels.Size = new Size(82, 23);
            ChkTunnels.TabIndex = 4;
            ChkTunnels.Text = "Anti tunneling";
            toolTip.SetToolTip(ChkTunnels, "Should guests be limited to digging a certain depth?");
            ChkTunnels.UseVisualStyleBackColor = true;
            ChkTunnels.CheckedChanged += ChkTunnels_CheckedChanged;
            label7.AutoSize = true;
            label7.Location = new Point(114, 339);
            label7.Name = "label7";
            label7.Size = new Size(39, 13);
            label7.TabIndex = 3;
            label7.Text = "Depth:";
            tabPage4.BackColor = SystemColors.Control;
            tabPage4.Controls.Add(chkRepeatMessages);
            tabPage4.Controls.Add(chkForceCuboid);
            tabPage4.Controls.Add(txtShutdown);
            tabPage4.Controls.Add(txtBanMessage);
            tabPage4.Controls.Add(chkShutdown);
            tabPage4.Controls.Add(chkBanMessage);
            tabPage4.Controls.Add(chkrankSuper);
            tabPage4.Controls.Add(chkCheap);
            tabPage4.Controls.Add(chkDeath);
            tabPage4.Controls.Add(chk17Dollar);
            tabPage4.Controls.Add(chkPhysicsRest);
            tabPage4.Controls.Add(chkSmile);
            tabPage4.Controls.Add(chkHelp);
            tabPage4.Controls.Add(label28);
            tabPage4.Controls.Add(label24);
            tabPage4.Controls.Add(txtNormRp);
            tabPage4.Controls.Add(txtRP);
            tabPage4.Controls.Add(label34);
            tabPage4.Controls.Add(label26);
            tabPage4.Controls.Add(label25);
            tabPage4.Controls.Add(txtAFKKick);
            tabPage4.Controls.Add(txtafk);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(txtBackup);
            tabPage4.Controls.Add(label32);
            tabPage4.Controls.Add(txtBackupLocation);
            tabPage4.Controls.Add(txtMoneys);
            tabPage4.Controls.Add(txtCheap);
            tabPage4.Controls.Add(txtRestartTime);
            tabPage4.Controls.Add(chkRestartTime);
            tabPage4.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabPage4.Location = new Point(4, 22);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(587, 387);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Misc";
            chkRepeatMessages.Appearance = Appearance.Button;
            chkRepeatMessages.AutoSize = true;
            chkRepeatMessages.Location = new Point(435, 93);
            chkRepeatMessages.Name = "chkRepeatMessages";
            chkRepeatMessages.Size = new Size(127, 23);
            chkRepeatMessages.TabIndex = 29;
            chkRepeatMessages.Text = "Repeat message blocks";
            chkRepeatMessages.UseVisualStyleBackColor = true;
            chkRepeatMessages.CheckedChanged += chkRepeatMessages_CheckedChanged;
            chkForceCuboid.Appearance = Appearance.Button;
            chkForceCuboid.AutoSize = true;
            chkForceCuboid.Location = new Point(13, 261);
            chkForceCuboid.Name = "chkForceCuboid";
            chkForceCuboid.Size = new Size(78, 23);
            chkForceCuboid.TabIndex = 29;
            chkForceCuboid.Text = "Force Cuboid";
            toolTip.SetToolTip(chkForceCuboid, "When true, runs an attempted cuboid despite cuboid limits, until it hits the group limit for that user.");
            chkForceCuboid.UseVisualStyleBackColor = true;
            chkForceCuboid.CheckedChanged += chkForceCuboid_CheckedChanged;
            txtShutdown.Location = new Point(176, 226);
            txtShutdown.MaxLength = 128;
            txtShutdown.Name = "txtShutdown";
            txtShutdown.Size = new Size(387, 21);
            txtShutdown.TabIndex = 28;
            txtBanMessage.Location = new Point(149, 199);
            txtBanMessage.MaxLength = 128;
            txtBanMessage.Name = "txtBanMessage";
            txtBanMessage.Size = new Size(414, 21);
            txtBanMessage.TabIndex = 27;
            chkShutdown.AutoSize = true;
            chkShutdown.Location = new Point(12, 229);
            chkShutdown.Name = "chkShutdown";
            chkShutdown.Size = new Size(158, 17);
            chkShutdown.TabIndex = 26;
            chkShutdown.Text = "Custom shutdown message:";
            chkShutdown.UseVisualStyleBackColor = true;
            chkShutdown.CheckedChanged += chkShutdown_CheckedChanged;
            chkBanMessage.AutoSize = true;
            chkBanMessage.Location = new Point(12, 202);
            chkBanMessage.Name = "chkBanMessage";
            chkBanMessage.Size = new Size(129, 17);
            chkBanMessage.TabIndex = 25;
            chkBanMessage.Text = "Custom ban message:";
            chkBanMessage.UseVisualStyleBackColor = true;
            chkBanMessage.CheckedChanged += chkBanMessage_CheckedChanged;
            chkrankSuper.Appearance = Appearance.Button;
            chkrankSuper.AutoSize = true;
            chkrankSuper.Location = new Point(368, 347);
            chkrankSuper.Name = "chkrankSuper";
            chkrankSuper.Size = new Size(195, 23);
            chkrankSuper.TabIndex = 24;
            chkrankSuper.Text = "SuperOPs can appoint other SuperOPs";
            toolTip.SetToolTip(chkrankSuper, "Does what it says on the tin");
            chkrankSuper.UseVisualStyleBackColor = true;
            chkrankSuper.CheckedChanged += chkrankSuper_CheckedChanged;
            chkCheap.AutoSize = true;
            chkCheap.Location = new Point(12, 174);
            chkCheap.Name = "chkCheap";
            chkCheap.Size = new Size(103, 17);
            chkCheap.TabIndex = 23;
            chkCheap.Text = "Cheap message:";
            toolTip.SetToolTip(chkCheap, "Is immortality cheap and unfair?");
            chkCheap.UseVisualStyleBackColor = true;
            chkCheap.CheckedChanged += chkCheap_CheckedChanged;
            chkDeath.Appearance = Appearance.Button;
            chkDeath.AutoSize = true;
            chkDeath.Location = new Point(13, 318);
            chkDeath.Name = "chkDeath";
            chkDeath.Size = new Size(75, 23);
            chkDeath.TabIndex = 21;
            chkDeath.Text = "Death count";
            toolTip.SetToolTip(chkDeath, "\"Bob has died 10 times.\"");
            chkDeath.UseVisualStyleBackColor = true;
            chkDeath.CheckedChanged += chkDeath_CheckedChanged;
            chk17Dollar.Appearance = Appearance.Button;
            chk17Dollar.AutoSize = true;
            chk17Dollar.Location = new Point(472, 318);
            chk17Dollar.Name = "chk17Dollar";
            chk17Dollar.Size = new Size(91, 23);
            chk17Dollar.TabIndex = 22;
            chk17Dollar.Text = "$ before $name";
            chk17Dollar.UseVisualStyleBackColor = true;
            chk17Dollar.CheckedChanged += chk17Dollar_CheckedChanged;
            chkPhysicsRest.Appearance = Appearance.Button;
            chkPhysicsRest.AutoSize = true;
            chkPhysicsRest.Location = new Point(13, 289);
            chkPhysicsRest.Name = "chkPhysicsRest";
            chkPhysicsRest.Size = new Size(89, 23);
            chkPhysicsRest.TabIndex = 22;
            chkPhysicsRest.Text = "Restart physics";
            toolTip.SetToolTip(chkPhysicsRest, "Restart physics on shutdown, clearing all physics blocks.");
            chkPhysicsRest.UseVisualStyleBackColor = true;
            chkPhysicsRest.CheckedChanged += chkPhysicsRest_CheckedChanged_1;
            chkSmile.Appearance = Appearance.Button;
            chkSmile.AutoSize = true;
            chkSmile.Location = new Point(481, 289);
            chkSmile.Name = "chkSmile";
            chkSmile.Size = new Size(82, 23);
            chkSmile.TabIndex = 19;
            chkSmile.Text = "Parse emotes";
            chkSmile.UseVisualStyleBackColor = true;
            chkSmile.CheckedChanged += chkSmile_CheckedChanged;
            chkHelp.Appearance = Appearance.Button;
            chkHelp.AutoSize = true;
            chkHelp.Location = new Point(13, 347);
            chkHelp.Name = "chkHelp";
            chkHelp.Size = new Size(56, 23);
            chkHelp.TabIndex = 20;
            chkHelp.Text = "Old help";
            toolTip.SetToolTip(chkHelp, "Should the old, cluttered help menu be used?");
            chkHelp.UseVisualStyleBackColor = true;
            chkHelp.CheckedChanged += chkHelp_CheckedChanged;
            label28.AutoSize = true;
            label28.Location = new Point(451, 72);
            label28.Name = "label28";
            label28.Size = new Size(61, 13);
            label28.TabIndex = 16;
            label28.Text = "Normal /rp:";
            label24.AutoSize = true;
            label24.Location = new Point(464, 46);
            label24.Name = "label24";
            label24.Size = new Size(48, 13);
            label24.TabIndex = 15;
            label24.Text = "/rp limit:";
            toolTip.SetToolTip(label24, "Limit for custom physics set by /rp");
            txtNormRp.Location = new Point(521, 69);
            txtNormRp.Name = "txtNormRp";
            txtNormRp.Size = new Size(41, 21);
            txtNormRp.TabIndex = 13;
            txtRP.Location = new Point(521, 43);
            txtRP.Name = "txtRP";
            txtRP.Size = new Size(41, 21);
            txtRP.TabIndex = 14;
            label34.AutoSize = true;
            label34.Location = new Point(403, 265);
            label34.Name = "label34";
            label34.Size = new Size(71, 13);
            label34.TabIndex = 11;
            label34.Text = "Money name:";
            label26.AutoSize = true;
            label26.Location = new Point(29, 93);
            label26.Name = "label26";
            label26.Size = new Size(48, 13);
            label26.TabIndex = 11;
            label26.Text = "AFK Kick:";
            label25.AutoSize = true;
            label25.Location = new Point(23, 67);
            label25.Name = "label25";
            label25.Size = new Size(54, 13);
            label25.TabIndex = 12;
            label25.Text = "AFK timer:";
            txtAFKKick.Location = new Point(83, 91);
            txtAFKKick.Name = "txtAFKKick";
            txtAFKKick.Size = new Size(41, 21);
            txtAFKKick.TabIndex = 9;
            toolTip.SetToolTip(txtAFKKick, "Kick the user after they have been afk for this many minutes (0 = No kick)");
            txtafk.Location = new Point(83, 64);
            txtafk.Name = "txtafk";
            txtafk.Size = new Size(41, 21);
            txtafk.TabIndex = 10;
            toolTip.SetToolTip(txtafk, "How long the server should wait before declaring someone ask afk. (0 = No timer at all)");
            label9.AutoSize = true;
            label9.Location = new Point(10, 42);
            label9.Name = "label9";
            label9.Size = new Size(67, 13);
            label9.TabIndex = 7;
            label9.Text = "Backup time:";
            txtBackup.Location = new Point(83, 37);
            txtBackup.Name = "txtBackup";
            txtBackup.Size = new Size(41, 21);
            txtBackup.TabIndex = 5;
            toolTip.SetToolTip(txtBackup, "How often should backups be taken, in seconds.\nDefault = 300");
            label32.AutoSize = true;
            label32.Location = new Point(10, 15);
            label32.Name = "label32";
            label32.Size = new Size(44, 13);
            label32.TabIndex = 3;
            label32.Text = "Backup:";
            txtBackupLocation.Location = new Point(60, 12);
            txtBackupLocation.Name = "txtBackupLocation";
            txtBackupLocation.Size = new Size(502, 21);
            txtBackupLocation.TabIndex = 2;
            txtMoneys.Location = new Point(480, 262);
            txtMoneys.Name = "txtMoneys";
            txtMoneys.Size = new Size(82, 21);
            txtMoneys.TabIndex = 1;
            txtCheap.Location = new Point(122, 172);
            txtCheap.Name = "txtCheap";
            txtCheap.Size = new Size(441, 21);
            txtCheap.TabIndex = 1;
            txtRestartTime.Location = new Point(149, 145);
            txtRestartTime.Name = "txtRestartTime";
            txtRestartTime.Size = new Size(84, 21);
            txtRestartTime.TabIndex = 1;
            txtRestartTime.Text = "HH: mm: ss";
            chkRestartTime.AutoSize = true;
            chkRestartTime.Location = new Point(12, 147);
            chkRestartTime.Name = "chkRestartTime";
            chkRestartTime.Size = new Size(131, 17);
            chkRestartTime.TabIndex = 0;
            chkRestartTime.Text = "Restart server at time:";
            chkRestartTime.UseVisualStyleBackColor = true;
            chkRestartTime.CheckedChanged += chkRestartTime_CheckedChanged;
            tabPage6.BackColor = Color.Transparent;
            tabPage6.Controls.Add(label39);
            tabPage6.Controls.Add(vipAdd);
            tabPage6.Controls.Add(vipRemove);
            tabPage6.Controls.Add(vipEntry);
            tabPage6.Controls.Add(vipLabel);
            tabPage6.Controls.Add(vipList);
            tabPage6.Controls.Add(label35);
            tabPage6.Controls.Add(label36);
            tabPage6.Controls.Add(serverMessageInterval);
            tabPage6.Controls.Add(label37);
            tabPage6.Controls.Add(label38);
            tabPage6.Controls.Add(serverMessage);
            tabPage6.Controls.Add(btnSetServerMessage);
            tabPage6.Controls.Add(updatePanel);
            tabPage6.Location = new Point(4, 22);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(587, 387);
            tabPage6.TabIndex = 6;
            tabPage6.Text = "Misc 2";
            label39.Location = new Point(349, 334);
            label39.Name = "label39";
            label39.Size = new Size(133, 32);
            label39.TabIndex = 31;
            label39.Text = "People on VIP list can join server even if server is full.";
            vipAdd.Location = new Point(498, 82);
            vipAdd.Name = "vipAdd";
            vipAdd.Size = new Size(75, 23);
            vipAdd.TabIndex = 26;
            vipAdd.Text = "Add";
            vipAdd.Click += vipAdd_Click;
            vipRemove.Location = new Point(498, 119);
            vipRemove.Name = "vipRemove";
            vipRemove.Size = new Size(75, 23);
            vipRemove.TabIndex = 29;
            vipRemove.Text = "Remove";
            vipRemove.Click += vipRemove_Click;
            vipEntry.Location = new Point(352, 82);
            vipEntry.Name = "vipEntry";
            vipEntry.Size = new Size(130, 21);
            vipEntry.TabIndex = 30;
            vipLabel.Font = new Font("Calibri", 12f);
            vipLabel.Location = new Point(429, 31);
            vipLabel.Name = "vipLabel";
            vipLabel.Size = new Size(72, 18);
            vipLabel.TabIndex = 27;
            vipLabel.Text = "VIP List";
            vipList.Location = new Point(352, 119);
            vipList.Name = "vipList";
            vipList.Size = new Size(130, 212);
            vipList.TabIndex = 28;
            label35.Font = new Font("Calibri", 12f);
            label35.Location = new Point(89, 210);
            label35.Name = "label35";
            label35.Size = new Size(158, 24);
            label35.TabIndex = 25;
            label35.Text = "Reappearing message";
            label36.Location = new Point(175, 323);
            label36.Name = "label36";
            label36.Size = new Size(32, 22);
            label36.TabIndex = 24;
            label36.Text = "min.";
            serverMessageInterval.Location = new Point(93, 320);
            serverMessageInterval.Name = "serverMessageInterval";
            serverMessageInterval.Size = new Size(78, 21);
            serverMessageInterval.TabIndex = 23;
            label37.Location = new Point(12, 323);
            label37.Name = "label37";
            label37.Size = new Size(75, 22);
            label37.TabIndex = 22;
            label37.Text = "Display every:";
            label38.Location = new Point(9, 251);
            label38.Name = "label38";
            label38.Size = new Size(78, 22);
            label38.TabIndex = 21;
            label38.Text = "Your Message:";
            serverMessage.Location = new Point(93, 248);
            serverMessage.Multiline = true;
            serverMessage.Name = "serverMessage";
            serverMessage.Size = new Size(234, 55);
            serverMessage.TabIndex = 20;
            serverMessage.TextChanged += serverMessage_TextChanged;
            btnSetServerMessage.Location = new Point(236, 320);
            btnSetServerMessage.Name = "btnSetServerMessage";
            btnSetServerMessage.Size = new Size(42, 23);
            btnSetServerMessage.TabIndex = 19;
            btnSetServerMessage.Text = "Set";
            btnSetServerMessage.Click += btnSetServerMessage_Click;
            updatePanel.Controls.Add(updateLabel);
            updatePanel.Controls.Add(FlowControl);
            updatePanel.Controls.Add(PositionDelayUpdate);
            updatePanel.Controls.Add(txtPositionDelay);
            updatePanel.Controls.Add(PositionDelay);
            updatePanel.Location = new Point(15, 15);
            updatePanel.Name = "updatePanel";
            updatePanel.Size = new Size(312, 171);
            updatePanel.TabIndex = 6;
            updateLabel.Font = new Font("Calibri", 12f);
            updateLabel.Location = new Point(56, 16);
            updateLabel.Name = "updateLabel";
            updateLabel.Size = new Size(214, 23);
            updateLabel.TabIndex = 0;
            updateLabel.Text = "Players position refreshing rate";
            FlowControl.Location = new Point(15, 108);
            FlowControl.Maximum = 59;
            FlowControl.Minimum = 1;
            FlowControl.Name = "FlowControl";
            FlowControl.Size = new Size(280, 18);
            FlowControl.TabIndex = 0;
            FlowControl.Value = 7;
            FlowControl.Scroll += FlowControl_Scroll;
            PositionDelayUpdate.Location = new Point(221, 58);
            PositionDelayUpdate.Name = "PositionDelayUpdate";
            PositionDelayUpdate.Size = new Size(75, 23);
            PositionDelayUpdate.TabIndex = 0;
            PositionDelayUpdate.Text = "OK";
            PositionDelayUpdate.Click += PositionDelayUpdate_Click;
            txtPositionDelay.Location = new Point(105, 59);
            txtPositionDelay.Name = "txtPositionDelay";
            txtPositionDelay.Size = new Size(100, 21);
            txtPositionDelay.TabIndex = 0;
            txtPositionDelay.Text = "100";
            PositionDelay.Location = new Point(17, 48);
            PositionDelay.Name = "PositionDelay";
            PositionDelay.Size = new Size(84, 44);
            PositionDelay.TabIndex = 0;
            PositionDelay.Text = "Player position showing delay: (default=100)";
            misc3.BackColor = Color.Transparent;
            misc3.Controls.Add(generalProperties);
            misc3.Location = new Point(4, 22);
            misc3.Name = "misc3";
            misc3.Padding = new Padding(3);
            misc3.Size = new Size(587, 387);
            misc3.TabIndex = 8;
            misc3.Text = "Misc 3";
            generalProperties.Dock = DockStyle.Fill;
            generalProperties.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 238);
            generalProperties.Location = new Point(3, 3);
            generalProperties.Name = "generalProperties";
            generalSettings.AllowAndListOnClassiCube = true;
            generalSettings.AvgStop = 50;
            generalSettings.ChatFontFamily = "Calibri";
            generalSettings.ChatFontSize = 12f;
            generalSettings.ChatSpecialCharacters = false;
            generalSettings.CheckPortOnStart = true;
            generalSettings.CooldownCmdMax = 4;
            generalSettings.CooldownCmdMaxSeconds = 8;
            generalSettings.CooldownCmdUse = true;
            generalSettings.CooldownCmdWarning = "%cWARNING: Slow down! You are using way too many commands per second.";
            generalSettings.CustomConsoleName = "%cLord of the Server:%f";
            generalSettings.CustomConsoleNameDelimiter = ":%f";
            generalSettings.DeathCooldown = 20;
            generalSettings.DevMessagePermission = 80;
            generalSettings.ExperimentalMessages = true;
            generalSettings.HomeMapDepth = 64;
            generalSettings.HomeMapHeight = 64;
            generalSettings.HomeMapWidth = 64;
            generalSettings.IntelliSys = false;
            generalSettings.KickSlug = false;
            generalSettings.KickWomUsers = false;
            generalSettings.MinPermissionForReview = 80;
            generalSettings.PillarMaxHeight = -1;
            generalSettings.PlusMarkerForClassiCubeAccount = true;
            generalSettings.ShowServerLag = false;
            generalSettings.Threshold1 = 60;
            generalSettings.Threshold2 = 10;
            generalSettings.UseChat = false;
            generalSettings.UseCustomName = false;
            generalSettings.VerifyNameForLocalIPs = false;
            generalProperties.SelectedObject = generalSettings;
            generalProperties.Size = new Size(581, 381);
            generalProperties.TabIndex = 0;
            generalProperties.Click += generalProperties_Click;
            tabPage2.BackColor = Color.Transparent;
            tabPage2.Controls.Add(label90);
            tabPage2.Controls.Add(label89);
            tabPage2.Controls.Add(label88);
            tabPage2.Controls.Add(label87);
            tabPage2.Controls.Add(label86);
            tabPage2.Controls.Add(label85);
            tabPage2.Controls.Add(textBoxBigMaps);
            tabPage2.Controls.Add(textBoxMediumMaps);
            tabPage2.Controls.Add(textBoxSmallMaps);
            tabPage2.Controls.Add(label84);
            tabPage2.Controls.Add(label83);
            tabPage2.Controls.Add(label81);
            tabPage2.Controls.Add(label82);
            tabPage2.Controls.Add(label33);
            tabPage2.Controls.Add(txtPromotionPrice);
            tabPage2.Controls.Add(cmbColor);
            tabPage2.Controls.Add(label16);
            tabPage2.Controls.Add(txtFileName);
            tabPage2.Controls.Add(label14);
            tabPage2.Controls.Add(txtLimit);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(txtPermission);
            tabPage2.Controls.Add(label12);
            tabPage2.Controls.Add(txtRankName);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(btnAddRank);
            tabPage2.Controls.Add(listRanks);
            tabPage2.Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(587, 387);
            tabPage2.TabIndex = 4;
            tabPage2.Text = "Ranks";
            label90.AutoSize = true;
            label90.Cursor = Cursors.Help;
            label90.Location = new Point(205, 346);
            label90.Name = "label90";
            label90.Size = new Size(18, 13);
            label90.TabIndex = 29;
            label90.Text = "(?)";
            label90.TextAlign = ContentAlignment.MiddleRight;
            toolTip.SetToolTip(label90, "The number of big maps that can be created by a player with this rank with a command '/mymap'.");
            label89.AutoSize = true;
            label89.Cursor = Cursors.Help;
            label89.Location = new Point(205, 319);
            label89.Name = "label89";
            label89.Size = new Size(18, 13);
            label89.TabIndex = 28;
            label89.Text = "(?)";
            label89.TextAlign = ContentAlignment.MiddleRight;
            toolTip.SetToolTip(label89, "The number of medium maps that can be created by a player with this rank with a command '/mymap'.");
            label88.AutoSize = true;
            label88.Cursor = Cursors.Help;
            label88.Location = new Point(205, 292);
            label88.Name = "label88";
            label88.Size = new Size(18, 13);
            label88.TabIndex = 27;
            label88.Text = "(?)";
            label88.TextAlign = ContentAlignment.MiddleRight;
            toolTip.SetToolTip(label88, "The number of small maps that can be created by a player with this rank with a command '/mymap'.");
            label87.AutoSize = true;
            label87.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            label87.Location = new Point(88, 206);
            label87.Name = "label87";
            label87.Size = new Size(34, 14);
            label87.TabIndex = 26;
            label87.Text = "Shop";
            label87.TextAlign = ContentAlignment.MiddleRight;
            label86.AutoSize = true;
            label86.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            label86.Location = new Point(87, 18);
            label86.Name = "label86";
            label86.Size = new Size(36, 14);
            label86.TabIndex = 25;
            label86.Text = "Basic";
            label86.TextAlign = ContentAlignment.MiddleRight;
            label85.AutoSize = true;
            label85.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            label85.Location = new Point(83, 268);
            label85.Name = "label85";
            label85.Size = new Size(55, 14);
            label85.TabIndex = 24;
            label85.Text = "My Maps";
            label85.TextAlign = ContentAlignment.MiddleRight;
            textBoxBigMaps.Location = new Point(99, 343);
            textBoxBigMaps.Name = "textBoxBigMaps";
            textBoxBigMaps.Size = new Size(100, 21);
            textBoxBigMaps.TabIndex = 23;
            textBoxBigMaps.TextChanged += textBoxBigMaps_TextChanged;
            textBoxBigMaps.KeyPress += textBox3_KeyPress;
            textBoxMediumMaps.Location = new Point(99, 316);
            textBoxMediumMaps.Name = "textBoxMediumMaps";
            textBoxMediumMaps.Size = new Size(100, 21);
            textBoxMediumMaps.TabIndex = 22;
            textBoxMediumMaps.TextChanged += textBoxMediumMaps_TextChanged;
            textBoxMediumMaps.KeyPress += textBox2_KeyPress;
            textBoxSmallMaps.Location = new Point(99, 289);
            textBoxSmallMaps.Name = "textBoxSmallMaps";
            textBoxSmallMaps.Size = new Size(100, 21);
            textBoxSmallMaps.TabIndex = 21;
            textBoxSmallMaps.TextChanged += textBoxSmallMaps_TextChanged;
            textBoxSmallMaps.KeyPress += textBox1_KeyPress;
            label84.AutoSize = true;
            label84.Location = new Point(39, 346);
            label84.Name = "label84";
            label84.Size = new Size(52, 13);
            label84.TabIndex = 20;
            label84.Text = "Big Maps:";
            label84.TextAlign = ContentAlignment.MiddleRight;
            label83.AutoSize = true;
            label83.Location = new Point(15, 319);
            label83.Name = "label83";
            label83.Size = new Size(77, 13);
            label83.TabIndex = 19;
            label83.Text = "Medium Maps:";
            label83.TextAlign = ContentAlignment.MiddleRight;
            label81.AutoSize = true;
            label81.Location = new Point(28, 292);
            label81.Name = "label81";
            label81.Size = new Size(64, 13);
            label81.TabIndex = 18;
            label81.Text = "Small Maps:";
            label81.TextAlign = ContentAlignment.MiddleRight;
            label82.AutoSize = true;
            label82.Cursor = Cursors.Help;
            label82.Location = new Point(205, 230);
            label82.Name = "label82";
            label82.Size = new Size(18, 13);
            label82.TabIndex = 17;
            label82.Text = "(?)";
            label82.TextAlign = ContentAlignment.MiddleRight;
            toolTip.SetToolTip(
                label82, "This field defines the price for of the promotion that is available in '/shop'. If the value is 0 then the rank won't be buyable.");
            label33.AutoSize = true;
            label33.Location = new Point(6, 230);
            label33.Name = "label33";
            label33.Size = new Size(85, 13);
            label33.TabIndex = 15;
            label33.Text = "Promotion Price:";
            label33.TextAlign = ContentAlignment.MiddleRight;
            txtPromotionPrice.Location = new Point(99, 227);
            txtPromotionPrice.Name = "txtPromotionPrice";
            txtPromotionPrice.Size = new Size(100, 21);
            txtPromotionPrice.TabIndex = 14;
            txtPromotionPrice.TextChanged += textBox1_TextChanged;
            txtPromotionPrice.KeyPress += txtPromotionPrice_KeyPress;
            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColor.FormattingEnabled = true;
            cmbColor.Location = new Point(99, 121);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(100, 21);
            cmbColor.TabIndex = 12;
            cmbColor.SelectedIndexChanged += cmbColor_SelectedIndexChanged;
            label16.AutoSize = true;
            label16.Location = new Point(58, 124);
            label16.Name = "label16";
            label16.Size = new Size(35, 13);
            label16.TabIndex = 11;
            label16.Text = "Color:";
            txtFileName.Location = new Point(99, 170);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(100, 21);
            txtFileName.TabIndex = 4;
            txtFileName.TextChanged += txtFileName_TextChanged;
            label14.AutoSize = true;
            label14.Location = new Point(39, 173);
            label14.Name = "label14";
            label14.Size = new Size(54, 13);
            label14.TabIndex = 3;
            label14.Text = "Filename:";
            label14.TextAlign = ContentAlignment.MiddleRight;
            txtLimit.Location = new Point(99, 95);
            txtLimit.Name = "txtLimit";
            txtLimit.Size = new Size(100, 21);
            txtLimit.TabIndex = 4;
            txtLimit.TextChanged += txtLimit_TextChanged;
            label13.AutoSize = true;
            label13.Location = new Point(34, 98);
            label13.Name = "label13";
            label13.Size = new Size(59, 13);
            label13.TabIndex = 3;
            label13.Text = "Block limit:";
            label13.TextAlign = ContentAlignment.MiddleRight;
            txtPermission.Location = new Point(99, 68);
            txtPermission.Name = "txtPermission";
            txtPermission.Size = new Size(100, 21);
            txtPermission.TabIndex = 4;
            txtPermission.TextChanged += txtPermission_TextChanged;
            label12.AutoSize = true;
            label12.Location = new Point(30, 71);
            label12.Name = "label12";
            label12.Size = new Size(63, 13);
            label12.TabIndex = 3;
            label12.Text = "Permission:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            txtRankName.Location = new Point(99, 41);
            txtRankName.Name = "txtRankName";
            txtRankName.Size = new Size(100, 21);
            txtRankName.TabIndex = 4;
            txtRankName.TextChanged += txtRankName_TextChanged;
            label11.AutoSize = true;
            label11.Location = new Point(55, 44);
            label11.Name = "label11";
            label11.Size = new Size(38, 13);
            label11.TabIndex = 3;
            label11.Text = "Name:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            button1.Location = new Point(311, 6);
            button1.Name = "button1";
            button1.Size = new Size(57, 23);
            button1.TabIndex = 2;
            button1.Text = "Del";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            btnAddRank.Location = new Point(248, 6);
            btnAddRank.Name = "btnAddRank";
            btnAddRank.Size = new Size(57, 23);
            btnAddRank.TabIndex = 1;
            btnAddRank.Text = "Add";
            btnAddRank.UseVisualStyleBackColor = true;
            btnAddRank.Click += btnAddRank_Click;
            listRanks.FormattingEnabled = true;
            listRanks.Location = new Point(235, 35);
            listRanks.Name = "listRanks";
            listRanks.Size = new Size(151, 329);
            listRanks.TabIndex = 0;
            listRanks.SelectedIndexChanged += listRanks_SelectedIndexChanged;
            tabPage3.AutoScroll = true;
            tabPage3.Controls.Add(tabControl3);
            tabPage3.Location = new Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(587, 387);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Commands";
            toolTip.SetToolTip(tabPage3, "Which ranks can use which commands.");
            tabControl3.Controls.Add(tabPage15);
            tabControl3.Controls.Add(tabPage16);
            tabControl3.Location = new Point(3, 16);
            tabControl3.Name = "tabControl3";
            tabControl3.SelectedIndex = 0;
            tabControl3.Size = new Size(581, 368);
            tabControl3.TabIndex = 28;
            tabPage15.BackColor = Color.Transparent;
            tabPage15.Controls.Add(listCommands);
            tabPage15.Controls.Add(textBoxCmdShortcut);
            tabPage15.Controls.Add(label8);
            tabPage15.Controls.Add(label76);
            tabPage15.Controls.Add(label15);
            tabPage15.Controls.Add(btnCmdHelp);
            tabPage15.Controls.Add(label17);
            tabPage15.Controls.Add(txtCmdRanks);
            tabPage15.Controls.Add(txtCmdDisallow);
            tabPage15.Controls.Add(txtCmdAllow);
            tabPage15.Controls.Add(txtCmdLowest);
            tabPage15.Location = new Point(4, 22);
            tabPage15.Name = "tabPage15";
            tabPage15.Padding = new Padding(3);
            tabPage15.Size = new Size(573, 342);
            tabPage15.TabIndex = 0;
            tabPage15.Text = "Manager";
            listCommands.FormattingEnabled = true;
            listCommands.Location = new Point(319, 47);
            listCommands.Name = "listCommands";
            listCommands.Size = new Size(151, 277);
            listCommands.TabIndex = 0;
            listCommands.SelectedIndexChanged += listCommands_SelectedIndexChanged;
            textBoxCmdShortcut.Location = new Point(208, 53);
            textBoxCmdShortcut.Name = "textBoxCmdShortcut";
            textBoxCmdShortcut.ReadOnly = true;
            textBoxCmdShortcut.Size = new Size(92, 21);
            textBoxCmdShortcut.TabIndex = 27;
            label8.AutoSize = true;
            label8.Location = new Point(103, 83);
            label8.Name = "label8";
            label8.Size = new Size(105, 13);
            label8.TabIndex = 1;
            label8.Text = "Lowest rank needed:";
            label76.AutoSize = true;
            label76.Location = new Point(158, 56);
            label76.Name = "label76";
            label76.Size = new Size(50, 13);
            label76.TabIndex = 26;
            label76.Text = "Shortcut:";
            label15.AutoSize = true;
            label15.Location = new Point(128, 111);
            label15.Name = "label15";
            label15.Size = new Size(80, 13);
            label15.TabIndex = 2;
            label15.Text = "But don't allow:";
            btnCmdHelp.Location = new Point(335, 18);
            btnCmdHelp.Name = "btnCmdHelp";
            btnCmdHelp.Size = new Size(120, 23);
            btnCmdHelp.TabIndex = 25;
            btnCmdHelp.Text = "Help information";
            btnCmdHelp.UseVisualStyleBackColor = true;
            btnCmdHelp.Click += btnCmdHelp_Click;
            label17.AutoSize = true;
            label17.Location = new Point(152, 138);
            label17.Name = "label17";
            label17.Size = new Size(56, 13);
            label17.TabIndex = 3;
            label17.Text = "And allow:";
            txtCmdRanks.Location = new Point(106, 161);
            txtCmdRanks.Multiline = true;
            txtCmdRanks.Name = "txtCmdRanks";
            txtCmdRanks.ReadOnly = true;
            txtCmdRanks.ScrollBars = ScrollBars.Vertical;
            txtCmdRanks.Size = new Size(194, 163);
            txtCmdRanks.TabIndex = 15;
            txtCmdDisallow.Location = new Point(208, 107);
            txtCmdDisallow.Name = "txtCmdDisallow";
            txtCmdDisallow.Size = new Size(92, 21);
            txtCmdDisallow.TabIndex = 14;
            txtCmdDisallow.LostFocus += txtCmdDisallow_TextChanged;
            txtCmdAllow.Location = new Point(208, 134);
            txtCmdAllow.Name = "txtCmdAllow";
            txtCmdAllow.Size = new Size(92, 21);
            txtCmdAllow.TabIndex = 14;
            txtCmdAllow.LostFocus += txtCmdAllow_TextChanged;
            txtCmdLowest.Location = new Point(208, 80);
            txtCmdLowest.Name = "txtCmdLowest";
            txtCmdLowest.Size = new Size(92, 21);
            txtCmdLowest.TabIndex = 14;
            txtCmdLowest.LostFocus += txtCmdLowest_TextChanged;
            tabPage16.BackColor = Color.Transparent;
            tabPage16.Controls.Add(label80);
            tabPage16.Controls.Add(textBoxCmdWarning);
            tabPage16.Controls.Add(checkBoxCmdCooldown);
            tabPage16.Controls.Add(label77);
            tabPage16.Controls.Add(textBoxCmdMax);
            tabPage16.Controls.Add(textBoxCmdMaxSeconds);
            tabPage16.Controls.Add(label78);
            tabPage16.Controls.Add(label79);
            tabPage16.Location = new Point(4, 22);
            tabPage16.Name = "tabPage16";
            tabPage16.Padding = new Padding(3);
            tabPage16.Size = new Size(573, 342);
            tabPage16.TabIndex = 1;
            tabPage16.Text = "Spam Prevention";
            label80.AutoSize = true;
            label80.Location = new Point(21, 116);
            label80.Name = "label80";
            label80.Size = new Size(148, 13);
            label80.TabIndex = 31;
            label80.Text = "Too many commands warning:";
            textBoxCmdWarning.Location = new Point(176, 113);
            textBoxCmdWarning.Name = "textBoxCmdWarning";
            textBoxCmdWarning.Size = new Size(360, 21);
            textBoxCmdWarning.TabIndex = 30;
            checkBoxCmdCooldown.Appearance = Appearance.Button;
            checkBoxCmdCooldown.AutoSize = true;
            checkBoxCmdCooldown.Location = new Point(40, 48);
            checkBoxCmdCooldown.MinimumSize = new Size(120, 0);
            checkBoxCmdCooldown.Name = "checkBoxCmdCooldown";
            checkBoxCmdCooldown.Size = new Size(120, 23);
            checkBoxCmdCooldown.TabIndex = 23;
            checkBoxCmdCooldown.Text = "Commands Cooldown";
            checkBoxCmdCooldown.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxCmdCooldown.UseVisualStyleBackColor = true;
            label77.AutoSize = true;
            label77.Location = new Point(393, 53);
            label77.Name = "label77";
            label77.Size = new Size(49, 13);
            label77.TabIndex = 28;
            label77.Text = "seconds.";
            textBoxCmdMax.Location = new Point(287, 50);
            textBoxCmdMax.Name = "textBoxCmdMax";
            textBoxCmdMax.Size = new Size(32, 21);
            textBoxCmdMax.TabIndex = 24;
            textBoxCmdMaxSeconds.Location = new Point(355, 50);
            textBoxCmdMaxSeconds.Name = "textBoxCmdMaxSeconds";
            textBoxCmdMaxSeconds.Size = new Size(32, 21);
            textBoxCmdMaxSeconds.TabIndex = 27;
            label78.AutoSize = true;
            label78.Location = new Point(198, 53);
            label78.Name = "label78";
            label78.Size = new Size(84, 13);
            label78.TabIndex = 25;
            label78.Text = "Max commands:";
            label79.AutoSize = true;
            label79.Location = new Point(327, 53);
            label79.Name = "label79";
            label79.Size = new Size(23, 13);
            label79.TabIndex = 26;
            label79.Text = "per";
            tabPage5.BackColor = Color.Transparent;
            tabPage5.Controls.Add(btnBlHelp);
            tabPage5.Controls.Add(txtBlRanks);
            tabPage5.Controls.Add(txtBlAllow);
            tabPage5.Controls.Add(txtBlLowest);
            tabPage5.Controls.Add(txtBlDisallow);
            tabPage5.Controls.Add(label18);
            tabPage5.Controls.Add(label19);
            tabPage5.Controls.Add(label20);
            tabPage5.Controls.Add(listBlocks);
            tabPage5.Location = new Point(4, 22);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(587, 387);
            tabPage5.TabIndex = 5;
            tabPage5.Text = "Blocks";
            btnBlHelp.Location = new Point(342, 14);
            btnBlHelp.Name = "btnBlHelp";
            btnBlHelp.Size = new Size(120, 23);
            btnBlHelp.TabIndex = 23;
            btnBlHelp.Text = "Help information";
            btnBlHelp.UseVisualStyleBackColor = true;
            btnBlHelp.Click += btnBlHelp_Click;
            txtBlRanks.Location = new Point(113, 130);
            txtBlRanks.Multiline = true;
            txtBlRanks.Name = "txtBlRanks";
            txtBlRanks.ReadOnly = true;
            txtBlRanks.Size = new Size(194, 243);
            txtBlRanks.TabIndex = 22;
            txtBlAllow.Location = new Point(215, 103);
            txtBlAllow.Name = "txtBlAllow";
            txtBlAllow.Size = new Size(92, 21);
            txtBlAllow.TabIndex = 20;
            txtBlAllow.LostFocus += txtBlAllow_TextChanged;
            txtBlLowest.Location = new Point(215, 49);
            txtBlLowest.Name = "txtBlLowest";
            txtBlLowest.Size = new Size(92, 21);
            txtBlLowest.TabIndex = 21;
            txtBlLowest.TextChanged += txtBlLowest_TextChanged_1;
            txtBlLowest.LostFocus += txtBlLowest_TextChanged;
            txtBlDisallow.Location = new Point(215, 76);
            txtBlDisallow.Name = "txtBlDisallow";
            txtBlDisallow.Size = new Size(92, 21);
            txtBlDisallow.TabIndex = 21;
            txtBlDisallow.LostFocus += txtBlDisallow_TextChanged;
            label18.AutoSize = true;
            label18.Location = new Point(159, 107);
            label18.Name = "label18";
            label18.Size = new Size(56, 13);
            label18.TabIndex = 18;
            label18.Text = "And allow:";
            label19.AutoSize = true;
            label19.Location = new Point(135, 80);
            label19.Name = "label19";
            label19.Size = new Size(80, 13);
            label19.TabIndex = 17;
            label19.Text = "But don't allow:";
            label20.AutoSize = true;
            label20.Location = new Point(110, 52);
            label20.Name = "label20";
            label20.Size = new Size(105, 13);
            label20.TabIndex = 16;
            label20.Text = "Lowest rank needed:";
            listBlocks.FormattingEnabled = true;
            listBlocks.Location = new Point(326, 43);
            listBlocks.Name = "listBlocks";
            listBlocks.Size = new Size(151, 329);
            listBlocks.Sorted = true;
            listBlocks.TabIndex = 15;
            listBlocks.SelectedIndexChanged += listBlocks_SelectedIndexChanged;
            tabPage12.Controls.Add(txtChannel);
            tabPage12.Controls.Add(label23);
            tabPage12.Controls.Add(chkIRC);
            tabPage12.Controls.Add(cmbIRCColour);
            tabPage12.Controls.Add(lblIRC);
            tabPage12.Controls.Add(label31);
            tabPage12.Controls.Add(txtNick);
            tabPage12.Controls.Add(txtOpChannel);
            tabPage12.Controls.Add(txtIRCServer);
            tabPage12.Controls.Add(chkUpdates);
            tabPage12.Controls.Add(label5);
            tabPage12.Controls.Add(label4);
            tabPage12.Controls.Add(label6);
            tabPage12.Location = new Point(4, 22);
            tabPage12.Name = "tabPage12";
            tabPage12.Padding = new Padding(3);
            tabPage12.Size = new Size(587, 387);
            tabPage12.TabIndex = 10;
            tabPage12.Text = "IRC";
            tabPage12.UseVisualStyleBackColor = true;
            txtChannel.Location = new Point(213, 88);
            txtChannel.Name = "txtChannel";
            txtChannel.Size = new Size(107, 21);
            txtChannel.TabIndex = 0;
            toolTip.SetToolTip(txtChannel, "The IRC channel to be used.");
            label23.AutoSize = true;
            label23.Location = new Point(156, 168);
            label23.Name = "label23";
            label23.Size = new Size(51, 13);
            label23.TabIndex = 3;
            label23.Text = "IRC color:";
            chkIRC.Appearance = Appearance.Button;
            chkIRC.AutoSize = true;
            chkIRC.Location = new Point(106, 59);
            chkIRC.Name = "chkIRC";
            chkIRC.Size = new Size(52, 23);
            chkIRC.TabIndex = 4;
            chkIRC.Text = "Use IRC";
            toolTip.SetToolTip(
                chkIRC, "Whether to use the IRC bot or not.\nIRC stands for Internet Relay Chat and allows for communication with the server while outside Minecraft.");
            chkIRC.UseVisualStyleBackColor = true;
            chkIRC.CheckedChanged += chkIRC_CheckedChanged;
            cmbIRCColour.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIRCColour.FormattingEnabled = true;
            cmbIRCColour.Location = new Point(213, 165);
            cmbIRCColour.Name = "cmbIRCColour";
            cmbIRCColour.Size = new Size(57, 21);
            cmbIRCColour.TabIndex = 9;
            toolTip.SetToolTip(cmbIRCColour, "The colour of the IRC nicks used in the IRC.");
            cmbIRCColour.SelectedIndexChanged += cmbIRCColour_SelectedIndexChanged;
            lblIRC.Location = new Point(280, 169);
            lblIRC.Name = "lblIRC";
            lblIRC.Size = new Size(15, 15);
            lblIRC.TabIndex = 10;
            label31.AutoSize = true;
            label31.Location = new Point(143, 118);
            label31.Name = "label31";
            label31.Size = new Size(64, 13);
            label31.TabIndex = 13;
            label31.Text = "Op Channel:";
            txtNick.Location = new Point(380, 88);
            txtNick.Name = "txtNick";
            txtNick.Size = new Size(94, 21);
            txtNick.TabIndex = 0;
            toolTip.SetToolTip(txtNick, "The Nick that the IRC bot will try and use.");
            txtOpChannel.Location = new Point(213, 115);
            txtOpChannel.Name = "txtOpChannel";
            txtOpChannel.Size = new Size(107, 21);
            txtOpChannel.TabIndex = 14;
            toolTip.SetToolTip(txtOpChannel, "The IRC channel to be used.");
            txtIRCServer.Location = new Point(213, 61);
            txtIRCServer.Name = "txtIRCServer";
            txtIRCServer.Size = new Size(261, 21);
            txtIRCServer.TabIndex = 0;
            toolTip.SetToolTip(txtIRCServer, "The IRC server to be used.\nDefault = irc.esper.net\nBetter choice = irc.foonetic.net");
            chkUpdates.Appearance = Appearance.Button;
            chkUpdates.AutoSize = true;
            chkUpdates.Location = new Point(379, 118);
            chkUpdates.Name = "chkUpdates";
            chkUpdates.Size = new Size(101, 23);
            chkUpdates.TabIndex = 4;
            chkUpdates.Text = "Check for updates";
            chkUpdates.UseVisualStyleBackColor = true;
            chkUpdates.Visible = false;
            label5.AutoSize = true;
            label5.Location = new Point(158, 91);
            label5.Name = "label5";
            label5.Size = new Size(49, 13);
            label5.TabIndex = 1;
            label5.Text = "Channel:";
            label4.AutoSize = true;
            label4.Location = new Point(344, 91);
            label4.Name = "label4";
            label4.Size = new Size(30, 13);
            label4.TabIndex = 1;
            label4.Text = "Nick:";
            label6.AutoSize = true;
            label6.Location = new Point(167, 64);
            label6.Name = "label6";
            label6.Size = new Size(40, 13);
            label6.TabIndex = 1;
            label6.Text = "Server:";
            tabPage14.BackColor = SystemColors.Control;
            tabPage14.Controls.Add(tabControlChat);
            tabPage14.Location = new Point(4, 22);
            tabPage14.Name = "tabPage14";
            tabPage14.Padding = new Padding(3);
            tabPage14.Size = new Size(587, 387);
            tabPage14.TabIndex = 11;
            tabPage14.Text = "Chat Filter";
            tabControlChat.Controls.Add(tabPageChat1);
            tabControlChat.Controls.Add(tabPageChatBadWords);
            tabControlChat.Controls.Add(tabPageChatCaps);
            tabControlChat.Controls.Add(tabPageChatCharSpam);
            tabControlChat.Controls.Add(tabPageChatSpam);
            tabControlChat.Location = new Point(7, 17);
            tabControlChat.Multiline = true;
            tabControlChat.Name = "tabControlChat";
            tabControlChat.SelectedIndex = 0;
            tabControlChat.Size = new Size(574, 364);
            tabControlChat.TabIndex = 13;
            tabPageChat1.BackColor = SystemColors.Control;
            tabPageChat1.Controls.Add(checkBoxChatFilterAdvanced);
            tabPageChat1.Controls.Add(label75);
            tabPageChat1.Controls.Add(label74);
            tabPageChat1.Controls.Add(label73);
            tabPageChat1.Controls.Add(label72);
            tabPageChat1.Controls.Add(groupBox3);
            tabPageChat1.Controls.Add(button3);
            tabPageChat1.Location = new Point(4, 22);
            tabPageChat1.Name = "tabPageChat1";
            tabPageChat1.Size = new Size(566, 338);
            tabPageChat1.TabIndex = 4;
            tabPageChat1.Text = "Basic Settings";
            tabPageChat1.Click += tabPage19_Click;
            checkBoxChatFilterAdvanced.AutoSize = true;
            checkBoxChatFilterAdvanced.Location = new Point(32, 238);
            checkBoxChatFilterAdvanced.Name = "checkBoxChatFilterAdvanced";
            checkBoxChatFilterAdvanced.Size = new Size(137, 17);
            checkBoxChatFilterAdvanced.TabIndex = 23;
            checkBoxChatFilterAdvanced.Text = "Show advanced settings";
            checkBoxChatFilterAdvanced.UseVisualStyleBackColor = true;
            checkBoxChatFilterAdvanced.CheckedChanged += checkBox9_CheckedChanged;
            label75.AutoSize = true;
            label75.Location = new Point(203, 172);
            label75.Name = "label75";
            label75.Size = new Size(135, 13);
            label75.TabIndex = 22;
            label75.Text = "- message spam prevention";
            label74.AutoSize = true;
            label74.Location = new Point(203, 54);
            label74.Name = "label74";
            label74.Size = new Size(100, 13);
            label74.TabIndex = 21;
            label74.Text = "- bad language filter";
            label73.AutoSize = true;
            label73.Location = new Point(203, 132);
            label73.Name = "label73";
            label73.Size = new Size(111, 13);
            label73.TabIndex = 20;
            label73.Text = "- character spam filter";
            label72.AutoSize = true;
            label72.Location = new Point(203, 93);
            label72.Name = "label72";
            label72.Size = new Size(87, 13);
            label72.TabIndex = 19;
            label72.Text = "- caps prevention";
            groupBox3.BackColor = SystemColors.ControlLight;
            groupBox3.Controls.Add(checkBoxRemoveCaps);
            groupBox3.Controls.Add(checkBoxShortenRepetitions);
            groupBox3.Controls.Add(checkBoxRemoveBadWords);
            groupBox3.Controls.Add(checkBoxMessagesCooldown);
            groupBox3.Location = new Point(17, 21);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(155, 179);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Main switches";
            checkBoxRemoveCaps.Appearance = Appearance.Button;
            checkBoxRemoveCaps.AutoSize = true;
            checkBoxRemoveCaps.Location = new Point(15, 67);
            checkBoxRemoveCaps.MinimumSize = new Size(120, 0);
            checkBoxRemoveCaps.Name = "checkBoxRemoveCaps";
            checkBoxRemoveCaps.Size = new Size(120, 23);
            checkBoxRemoveCaps.TabIndex = 2;
            checkBoxRemoveCaps.Text = "Remove Caps";
            checkBoxRemoveCaps.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxRemoveCaps.UseVisualStyleBackColor = true;
            checkBoxRemoveCaps.CheckedChanged += checkBoxRemoveCaps_CheckedChanged;
            checkBoxShortenRepetitions.Appearance = Appearance.Button;
            checkBoxShortenRepetitions.AutoSize = true;
            checkBoxShortenRepetitions.Location = new Point(15, 106);
            checkBoxShortenRepetitions.MinimumSize = new Size(120, 0);
            checkBoxShortenRepetitions.Name = "checkBoxShortenRepetitions";
            checkBoxShortenRepetitions.Size = new Size(120, 23);
            checkBoxShortenRepetitions.TabIndex = 3;
            checkBoxShortenRepetitions.Text = "Shorten Repetitions";
            checkBoxShortenRepetitions.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxShortenRepetitions.UseVisualStyleBackColor = true;
            checkBoxShortenRepetitions.CheckedChanged += checkBoxShortenRepetitions_CheckedChanged;
            checkBoxRemoveBadWords.Appearance = Appearance.Button;
            checkBoxRemoveBadWords.AutoSize = true;
            checkBoxRemoveBadWords.Location = new Point(15, 28);
            checkBoxRemoveBadWords.MinimumSize = new Size(120, 0);
            checkBoxRemoveBadWords.Name = "checkBoxRemoveBadWords";
            checkBoxRemoveBadWords.Size = new Size(120, 23);
            checkBoxRemoveBadWords.TabIndex = 4;
            checkBoxRemoveBadWords.Text = "Remove Bad Words";
            checkBoxRemoveBadWords.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxRemoveBadWords.UseVisualStyleBackColor = true;
            checkBoxRemoveBadWords.CheckedChanged += checkBoxRemoveBadWords_CheckedChanged;
            checkBoxMessagesCooldown.Appearance = Appearance.Button;
            checkBoxMessagesCooldown.AutoSize = true;
            checkBoxMessagesCooldown.Location = new Point(15, 146);
            checkBoxMessagesCooldown.MinimumSize = new Size(120, 0);
            checkBoxMessagesCooldown.Name = "checkBoxMessagesCooldown";
            checkBoxMessagesCooldown.Size = new Size(120, 23);
            checkBoxMessagesCooldown.TabIndex = 5;
            checkBoxMessagesCooldown.Text = "Messages Cooldown";
            checkBoxMessagesCooldown.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxMessagesCooldown.UseVisualStyleBackColor = true;
            checkBoxMessagesCooldown.CheckedChanged += checkBoxMessagesCooldown_CheckedChanged;
            button3.Location = new Point(341, 49);
            button3.Name = "button3";
            button3.Size = new Size(92, 23);
            button3.TabIndex = 16;
            button3.Text = "Edit Bad Words";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            tabPageChatBadWords.BackColor = SystemColors.Control;
            tabPageChatBadWords.Controls.Add(button2);
            tabPageChatBadWords.Controls.Add(radioButtonBadLanguage3);
            tabPageChatBadWords.Controls.Add(radioButtonBadLanguage2);
            tabPageChatBadWords.Controls.Add(radioButtonBadLanguage1);
            tabPageChatBadWords.Controls.Add(label69);
            tabPageChatBadWords.Controls.Add(label68);
            tabPageChatBadWords.Controls.Add(textBoxBadLanguageKickMsg);
            tabPageChatBadWords.Controls.Add(label67);
            tabPageChatBadWords.Controls.Add(textBoxBadLanguageWarning);
            tabPageChatBadWords.Controls.Add(label55);
            tabPageChatBadWords.Controls.Add(textBox13);
            tabPageChatBadWords.Controls.Add(label62);
            tabPageChatBadWords.Controls.Add(textBoxBadLanguageKickLimit);
            tabPageChatBadWords.Controls.Add(comboBoxDetectionLevel);
            tabPageChatBadWords.Controls.Add(button6);
            tabPageChatBadWords.Controls.Add(checkBoxRemoveBadWords1);
            tabPageChatBadWords.Controls.Add(label53);
            tabPageChatBadWords.Controls.Add(label54);
            tabPageChatBadWords.Controls.Add(textBoxBadLanguageSubstitution);
            tabPageChatBadWords.Location = new Point(4, 22);
            tabPageChatBadWords.Name = "tabPageChatBadWords";
            tabPageChatBadWords.Size = new Size(566, 338);
            tabPageChatBadWords.TabIndex = 2;
            tabPageChatBadWords.Text = "Bad Words Filter";
            button2.Location = new Point(459, 22);
            button2.Name = "button2";
            button2.Size = new Size(90, 23);
            button2.TabIndex = 28;
            button2.Text = "Edit White List";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            radioButtonBadLanguage3.AutoSize = true;
            radioButtonBadLanguage3.Location = new Point(52, 212);
            radioButtonBadLanguage3.Name = "radioButtonBadLanguage3";
            radioButtonBadLanguage3.Size = new Size(254, 17);
            radioButtonBadLanguage3.TabIndex = 27;
            radioButtonBadLanguage3.TabStop = true;
            radioButtonBadLanguage3.Text = "Don't display the message and send the warning.";
            radioButtonBadLanguage3.UseVisualStyleBackColor = true;
            radioButtonBadLanguage2.AutoSize = true;
            radioButtonBadLanguage2.Location = new Point(52, 189);
            radioButtonBadLanguage2.Name = "radioButtonBadLanguage2";
            radioButtonBadLanguage2.Size = new Size(138, 17);
            radioButtonBadLanguage2.TabIndex = 26;
            radioButtonBadLanguage2.TabStop = true;
            radioButtonBadLanguage2.Text = "Substitute the message.";
            radioButtonBadLanguage2.UseVisualStyleBackColor = true;
            radioButtonBadLanguage1.AutoSize = true;
            radioButtonBadLanguage1.Location = new Point(52, 166);
            radioButtonBadLanguage1.Name = "radioButtonBadLanguage1";
            radioButtonBadLanguage1.Size = new Size(241, 17);
            radioButtonBadLanguage1.TabIndex = 25;
            radioButtonBadLanguage1.TabStop = true;
            radioButtonBadLanguage1.Text = "Substitute the message and send the warning.";
            radioButtonBadLanguage1.UseVisualStyleBackColor = true;
            label69.AutoSize = true;
            label69.Location = new Point(30, 149);
            label69.Name = "label69";
            label69.Size = new Size(117, 13);
            label69.TabIndex = 24;
            label69.Text = "If bad word is detected:";
            label68.AutoSize = true;
            label68.Location = new Point(72, 85);
            label68.Name = "label68";
            label68.Size = new Size(50, 13);
            label68.TabIndex = 23;
            label68.Text = "Warning:";
            textBoxBadLanguageKickMsg.Location = new Point(128, 110);
            textBoxBadLanguageKickMsg.Name = "textBoxBadLanguageKickMsg";
            textBoxBadLanguageKickMsg.Size = new Size(421, 21);
            textBoxBadLanguageKickMsg.TabIndex = 22;
            label67.AutoSize = true;
            label67.Location = new Point(49, 110);
            label67.Name = "label67";
            label67.Size = new Size(73, 13);
            label67.TabIndex = 21;
            label67.Text = "Kick message:";
            textBoxBadLanguageWarning.Location = new Point(128, 82);
            textBoxBadLanguageWarning.Name = "textBoxBadLanguageWarning";
            textBoxBadLanguageWarning.Size = new Size(421, 21);
            textBoxBadLanguageWarning.TabIndex = 20;
            label55.AutoSize = true;
            label55.Location = new Point(30, 235);
            label55.Name = "label55";
            label55.Size = new Size(29, 13);
            label55.TabIndex = 19;
            label55.Text = "Help";
            textBox13.BackColor = SystemColors.ControlLight;
            textBox13.Location = new Point(30, 252);
            textBox13.Multiline = true;
            textBox13.Name = "textBox13";
            textBox13.ReadOnly = true;
            textBox13.Size = new Size(513, 65);
            textBox13.TabIndex = 18;
            textBox13.Text = componentResourceManager.GetString("textBox13.Text");
            label62.AutoSize = true;
            label62.Location = new Point(358, 58);
            label62.Name = "label62";
            label62.Size = new Size(123, 13);
            label62.TabIndex = 17;
            label62.Text = "Kick if warnings exceeds:";
            textBoxBadLanguageKickLimit.Location = new Point(486, 55);
            textBoxBadLanguageKickLimit.Name = "textBoxBadLanguageKickLimit";
            textBoxBadLanguageKickLimit.Size = new Size(63, 21);
            textBoxBadLanguageKickLimit.TabIndex = 16;
            comboBoxDetectionLevel.FormattingEnabled = true;
            comboBoxDetectionLevel.Items.AddRange(new object[2]
            {
                "Normal", "High"
            });
            comboBoxDetectionLevel.Location = new Point(267, 22);
            comboBoxDetectionLevel.Name = "comboBoxDetectionLevel";
            comboBoxDetectionLevel.Size = new Size(85, 21);
            comboBoxDetectionLevel.TabIndex = 12;
            button6.Location = new Point(361, 22);
            button6.Name = "button6";
            button6.Size = new Size(92, 23);
            button6.TabIndex = 15;
            button6.Text = "Edit Bad Words";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            checkBoxRemoveBadWords1.Appearance = Appearance.Button;
            checkBoxRemoveBadWords1.AutoSize = true;
            checkBoxRemoveBadWords1.Location = new Point(30, 22);
            checkBoxRemoveBadWords1.MinimumSize = new Size(120, 0);
            checkBoxRemoveBadWords1.Name = "checkBoxRemoveBadWords1";
            checkBoxRemoveBadWords1.Size = new Size(120, 23);
            checkBoxRemoveBadWords1.TabIndex = 3;
            checkBoxRemoveBadWords1.Text = "Remove Bad Words";
            checkBoxRemoveBadWords1.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxRemoveBadWords1.UseVisualStyleBackColor = true;
            checkBoxRemoveBadWords1.CheckedChanged += checkBoxRemoveBadWords1_CheckedChanged;
            label53.AutoSize = true;
            label53.Location = new Point(193, 58);
            label53.Name = "label53";
            label53.Size = new Size(66, 13);
            label53.TabIndex = 10;
            label53.Text = "Substitution:";
            label54.AutoSize = true;
            label54.Location = new Point(179, 25);
            label54.Name = "label54";
            label54.Size = new Size(81, 13);
            label54.TabIndex = 13;
            label54.Text = "Detection level:";
            textBoxBadLanguageSubstitution.Location = new Point(267, 55);
            textBoxBadLanguageSubstitution.Name = "textBoxBadLanguageSubstitution";
            textBoxBadLanguageSubstitution.Size = new Size(85, 21);
            textBoxBadLanguageSubstitution.TabIndex = 11;
            tabPageChatCaps.BackColor = SystemColors.Control;
            tabPageChatCaps.Controls.Add(label50);
            tabPageChatCaps.Controls.Add(textBox6);
            tabPageChatCaps.Controls.Add(textBoxMaxCaps);
            tabPageChatCaps.Controls.Add(label43);
            tabPageChatCaps.Controls.Add(checkBoxRemoveCaps1);
            tabPageChatCaps.Location = new Point(4, 22);
            tabPageChatCaps.Name = "tabPageChatCaps";
            tabPageChatCaps.Padding = new Padding(3);
            tabPageChatCaps.Size = new Size(566, 338);
            tabPageChatCaps.TabIndex = 0;
            tabPageChatCaps.Text = "Caps Prevention";
            label50.AutoSize = true;
            label50.Location = new Point(17, 247);
            label50.Name = "label50";
            label50.Size = new Size(29, 13);
            label50.TabIndex = 10;
            label50.Text = "Help";
            textBox6.BackColor = SystemColors.ControlLight;
            textBox6.Location = new Point(20, 263);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(513, 53);
            textBox6.TabIndex = 9;
            textBox6.Text = "Removes all uppercase characters if there's more of them than \"Max Caps\" value.";
            textBoxMaxCaps.Location = new Point(232, 24);
            textBoxMaxCaps.Name = "textBoxMaxCaps";
            textBoxMaxCaps.Size = new Size(41, 21);
            textBoxMaxCaps.TabIndex = 6;
            label43.AutoSize = true;
            label43.Location = new Point(172, 27);
            label43.Name = "label43";
            label43.Size = new Size(55, 13);
            label43.TabIndex = 7;
            label43.Text = "Max Caps:";
            checkBoxRemoveCaps1.Appearance = Appearance.Button;
            checkBoxRemoveCaps1.AutoSize = true;
            checkBoxRemoveCaps1.Location = new Point(17, 22);
            checkBoxRemoveCaps1.MinimumSize = new Size(120, 0);
            checkBoxRemoveCaps1.Name = "checkBoxRemoveCaps1";
            checkBoxRemoveCaps1.Size = new Size(120, 23);
            checkBoxRemoveCaps1.TabIndex = 1;
            checkBoxRemoveCaps1.Text = "Remove Caps";
            checkBoxRemoveCaps1.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxRemoveCaps1.UseVisualStyleBackColor = true;
            checkBoxRemoveCaps1.CheckedChanged += checkBoxRemoveCaps1_CheckedChanged;
            tabPageChatCharSpam.BackColor = SystemColors.Control;
            tabPageChatCharSpam.Controls.Add(label66);
            tabPageChatCharSpam.Controls.Add(radioButtonCharSpam3);
            tabPageChatCharSpam.Controls.Add(radioButtonCharSpam1);
            tabPageChatCharSpam.Controls.Add(radioButtonCharSpam2);
            tabPageChatCharSpam.Controls.Add(label65);
            tabPageChatCharSpam.Controls.Add(textBoxCharSpamWarning);
            tabPageChatCharSpam.Controls.Add(label63);
            tabPageChatCharSpam.Controls.Add(textBox12);
            tabPageChatCharSpam.Controls.Add(textBoxCharSpamSubstitution);
            tabPageChatCharSpam.Controls.Add(label52);
            tabPageChatCharSpam.Controls.Add(checkBoxShortenRepetitions1);
            tabPageChatCharSpam.Controls.Add(label51);
            tabPageChatCharSpam.Controls.Add(textBoxMaxChars);
            tabPageChatCharSpam.Controls.Add(textBoxMaxIllegalGroups);
            tabPageChatCharSpam.Controls.Add(label47);
            tabPageChatCharSpam.Location = new Point(4, 22);
            tabPageChatCharSpam.Name = "tabPageChatCharSpam";
            tabPageChatCharSpam.Padding = new Padding(3);
            tabPageChatCharSpam.Size = new Size(566, 338);
            tabPageChatCharSpam.TabIndex = 1;
            tabPageChatCharSpam.Text = "Character Repetition Filter";
            label66.AutoSize = true;
            label66.Location = new Point(16, 122);
            label66.Name = "label66";
            label66.Size = new Size(218, 13);
            label66.TabIndex = 19;
            label66.Text = "In case of exeeding 'Max illegal groups' value:";
            radioButtonCharSpam3.AutoSize = true;
            radioButtonCharSpam3.Location = new Point(44, 189);
            radioButtonCharSpam3.Name = "radioButtonCharSpam3";
            radioButtonCharSpam3.Size = new Size(254, 17);
            radioButtonCharSpam3.TabIndex = 18;
            radioButtonCharSpam3.TabStop = true;
            radioButtonCharSpam3.Text = "Don't display the message and send the warning.";
            radioButtonCharSpam3.UseVisualStyleBackColor = true;
            radioButtonCharSpam1.AutoSize = true;
            radioButtonCharSpam1.Location = new Point(44, 143);
            radioButtonCharSpam1.Name = "radioButtonCharSpam1";
            radioButtonCharSpam1.Size = new Size(245, 17);
            radioButtonCharSpam1.TabIndex = 17;
            radioButtonCharSpam1.TabStop = true;
            radioButtonCharSpam1.Text = "Display substitution text and send the warning.";
            radioButtonCharSpam1.UseVisualStyleBackColor = true;
            radioButtonCharSpam2.AutoSize = true;
            radioButtonCharSpam2.Location = new Point(44, 166);
            radioButtonCharSpam2.Name = "radioButtonCharSpam2";
            radioButtonCharSpam2.Size = new Size(142, 17);
            radioButtonCharSpam2.TabIndex = 16;
            radioButtonCharSpam2.TabStop = true;
            radioButtonCharSpam2.Text = "Display substitution text.";
            radioButtonCharSpam2.UseVisualStyleBackColor = true;
            label65.AutoSize = true;
            label65.Location = new Point(16, 81);
            label65.Name = "label65";
            label65.Size = new Size(50, 13);
            label65.TabIndex = 15;
            label65.Text = "Warning:";
            textBoxCharSpamWarning.Location = new Point(72, 78);
            textBoxCharSpamWarning.Name = "textBoxCharSpamWarning";
            textBoxCharSpamWarning.Size = new Size(468, 21);
            textBoxCharSpamWarning.TabIndex = 14;
            label63.AutoSize = true;
            label63.Location = new Point(24, 211);
            label63.Name = "label63";
            label63.Size = new Size(29, 13);
            label63.TabIndex = 13;
            label63.Text = "Help";
            textBox12.BackColor = SystemColors.ControlLight;
            textBox12.Location = new Point(27, 227);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.ReadOnly = true;
            textBox12.Size = new Size(513, 99);
            textBox12.TabIndex = 12;
            textBox12.Text = componentResourceManager.GetString("textBox12.Text");
            textBoxCharSpamSubstitution.Location = new Point(401, 51);
            textBoxCharSpamSubstitution.Name = "textBoxCharSpamSubstitution";
            textBoxCharSpamSubstitution.Size = new Size(64, 21);
            textBoxCharSpamSubstitution.TabIndex = 10;
            label52.AutoSize = true;
            label52.Location = new Point(327, 54);
            label52.Name = "label52";
            label52.Size = new Size(66, 13);
            label52.TabIndex = 9;
            label52.Text = "Substitution:";
            checkBoxShortenRepetitions1.Appearance = Appearance.Button;
            checkBoxShortenRepetitions1.AutoSize = true;
            checkBoxShortenRepetitions1.Location = new Point(19, 22);
            checkBoxShortenRepetitions1.MinimumSize = new Size(120, 0);
            checkBoxShortenRepetitions1.Name = "checkBoxShortenRepetitions1";
            checkBoxShortenRepetitions1.Size = new Size(120, 23);
            checkBoxShortenRepetitions1.TabIndex = 2;
            checkBoxShortenRepetitions1.Text = "Shorten Repetitions";
            checkBoxShortenRepetitions1.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxShortenRepetitions1.UseVisualStyleBackColor = true;
            checkBoxShortenRepetitions1.CheckedChanged += checkBoxShortenRepetitions1_CheckedChanged;
            label51.AutoSize = true;
            label51.Location = new Point(301, 27);
            label51.Name = "label51";
            label51.Size = new Size(95, 13);
            label51.TabIndex = 8;
            label51.Text = "Max illegal groups:";
            textBoxMaxChars.Location = new Point(230, 24);
            textBoxMaxChars.Name = "textBoxMaxChars";
            textBoxMaxChars.Size = new Size(61, 21);
            textBoxMaxChars.TabIndex = 3;
            textBoxMaxIllegalGroups.Location = new Point(401, 24);
            textBoxMaxIllegalGroups.Name = "textBoxMaxIllegalGroups";
            textBoxMaxIllegalGroups.Size = new Size(64, 21);
            textBoxMaxIllegalGroups.TabIndex = 7;
            label47.AutoSize = true;
            label47.Location = new Point(166, 27);
            label47.Name = "label47";
            label47.Size = new Size(59, 13);
            label47.TabIndex = 4;
            label47.Text = "Max Chars:";
            tabPageChatSpam.BackColor = SystemColors.Control;
            tabPageChatSpam.Controls.Add(checkBoxTempMute);
            tabPageChatSpam.Controls.Add(textBoxDuplicateMessagesWarning);
            tabPageChatSpam.Controls.Add(label71);
            tabPageChatSpam.Controls.Add(label70);
            tabPageChatSpam.Controls.Add(textBoxMaxMessagesWarning);
            tabPageChatSpam.Controls.Add(label64);
            tabPageChatSpam.Controls.Add(textBox14);
            tabPageChatSpam.Controls.Add(label61);
            tabPageChatSpam.Controls.Add(textBoxDuplicateMessagesSeconds);
            tabPageChatSpam.Controls.Add(label60);
            tabPageChatSpam.Controls.Add(checkBoxMessagesCooldown1);
            tabPageChatSpam.Controls.Add(label59);
            tabPageChatSpam.Controls.Add(textBoxMaxMessages);
            tabPageChatSpam.Controls.Add(textBoxMaxMessagesSeconds);
            tabPageChatSpam.Controls.Add(label56);
            tabPageChatSpam.Controls.Add(label58);
            tabPageChatSpam.Controls.Add(label57);
            tabPageChatSpam.Controls.Add(textBoxDuplicateMessages);
            tabPageChatSpam.Location = new Point(4, 22);
            tabPageChatSpam.Name = "tabPageChatSpam";
            tabPageChatSpam.Size = new Size(566, 338);
            tabPageChatSpam.TabIndex = 3;
            tabPageChatSpam.Text = "Message Spam Prevention";
            checkBoxTempMute.AutoSize = true;
            checkBoxTempMute.Location = new Point(455, 24);
            checkBoxTempMute.Name = "checkBoxTempMute";
            checkBoxTempMute.Size = new Size(78, 17);
            checkBoxTempMute.TabIndex = 32;
            checkBoxTempMute.Text = "Temp mute";
            checkBoxTempMute.UseVisualStyleBackColor = true;
            textBoxDuplicateMessagesWarning.Location = new Point(175, 129);
            textBoxDuplicateMessagesWarning.Name = "textBoxDuplicateMessagesWarning";
            textBoxDuplicateMessagesWarning.Size = new Size(360, 21);
            textBoxDuplicateMessagesWarning.TabIndex = 31;
            textBoxDuplicateMessagesWarning.Visible = false;
            label71.AutoSize = true;
            label71.Location = new Point(25, 132);
            label71.Name = "label71";
            label71.Size = new Size(145, 13);
            label71.TabIndex = 30;
            label71.Text = "Duplicate messages warning:";
            label71.Visible = false;
            label70.AutoSize = true;
            label70.Location = new Point(25, 98);
            label70.Name = "label70";
            label70.Size = new Size(143, 13);
            label70.TabIndex = 29;
            label70.Text = "Too many messages warning:";
            textBoxMaxMessagesWarning.Location = new Point(175, 95);
            textBoxMaxMessagesWarning.Name = "textBoxMaxMessagesWarning";
            textBoxMaxMessagesWarning.Size = new Size(360, 21);
            textBoxMaxMessagesWarning.TabIndex = 28;
            label64.AutoSize = true;
            label64.Location = new Point(22, 205);
            label64.Name = "label64";
            label64.Size = new Size(29, 13);
            label64.TabIndex = 27;
            label64.Text = "Help";
            textBox14.BackColor = SystemColors.ControlLight;
            textBox14.Location = new Point(22, 224);
            textBox14.Multiline = true;
            textBox14.Name = "textBox14";
            textBox14.ReadOnly = true;
            textBox14.Size = new Size(513, 65);
            textBox14.TabIndex = 26;
            textBox14.Text =
                "Message spam prevention checks the number of messages sent by a player within a certain amount of time and it takes action if the set limit is exceeded.";
            label61.AutoSize = true;
            label61.Location = new Point(385, 53);
            label61.Name = "label61";
            label61.Size = new Size(49, 13);
            label61.TabIndex = 25;
            label61.Text = "seconds.";
            label61.Visible = false;
            textBoxDuplicateMessagesSeconds.Location = new Point(347, 50);
            textBoxDuplicateMessagesSeconds.Name = "textBoxDuplicateMessagesSeconds";
            textBoxDuplicateMessagesSeconds.Size = new Size(32, 21);
            textBoxDuplicateMessagesSeconds.TabIndex = 24;
            textBoxDuplicateMessagesSeconds.Visible = false;
            label60.AutoSize = true;
            label60.Location = new Point(319, 53);
            label60.Name = "label60";
            label60.Size = new Size(16, 13);
            label60.TabIndex = 23;
            label60.Text = "in";
            label60.Visible = false;
            checkBoxMessagesCooldown1.Appearance = Appearance.Button;
            checkBoxMessagesCooldown1.AutoSize = true;
            checkBoxMessagesCooldown1.Location = new Point(22, 24);
            checkBoxMessagesCooldown1.MinimumSize = new Size(120, 0);
            checkBoxMessagesCooldown1.Name = "checkBoxMessagesCooldown1";
            checkBoxMessagesCooldown1.Size = new Size(120, 23);
            checkBoxMessagesCooldown1.TabIndex = 4;
            checkBoxMessagesCooldown1.Text = "Messages Cooldown";
            checkBoxMessagesCooldown1.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxMessagesCooldown1.UseVisualStyleBackColor = true;
            checkBoxMessagesCooldown1.CheckedChanged += checkBoxMessagesCooldown1_CheckedChanged;
            label59.AutoSize = true;
            label59.Location = new Point(385, 26);
            label59.Name = "label59";
            label59.Size = new Size(49, 13);
            label59.TabIndex = 22;
            label59.Text = "seconds.";
            textBoxMaxMessages.Location = new Point(279, 23);
            textBoxMaxMessages.Name = "textBoxMaxMessages";
            textBoxMaxMessages.Size = new Size(32, 21);
            textBoxMaxMessages.TabIndex = 16;
            textBoxMaxMessages.TextChanged += textBoxMaxMessages_TextChanged;
            textBoxMaxMessagesSeconds.Location = new Point(347, 23);
            textBoxMaxMessagesSeconds.Name = "textBoxMaxMessagesSeconds";
            textBoxMaxMessagesSeconds.Size = new Size(32, 21);
            textBoxMaxMessagesSeconds.TabIndex = 21;
            label56.AutoSize = true;
            label56.Location = new Point(195, 26);
            label56.Name = "label56";
            label56.Size = new Size(79, 13);
            label56.TabIndex = 17;
            label56.Text = "Max messages:";
            label58.AutoSize = true;
            label58.Location = new Point(319, 26);
            label58.Name = "label58";
            label58.Size = new Size(23, 13);
            label58.TabIndex = 20;
            label58.Text = "per";
            label57.AutoSize = true;
            label57.Location = new Point(128, 52);
            label57.Name = "label57";
            label57.Size = new Size(146, 13);
            label57.TabIndex = 18;
            label57.Text = "Max same messages in a row:";
            label57.Visible = false;
            textBoxDuplicateMessages.Location = new Point(279, 49);
            textBoxDuplicateMessages.Name = "textBoxDuplicateMessages";
            textBoxDuplicateMessages.Size = new Size(32, 21);
            textBoxDuplicateMessages.TabIndex = 19;
            textBoxDuplicateMessages.Visible = false;
            btnSave.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(443, 426);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            btnDiscard.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDiscard.Location = new Point(522, 426);
            btnDiscard.Name = "btnDiscard";
            btnDiscard.Size = new Size(75, 23);
            btnDiscard.TabIndex = 1;
            btnDiscard.Text = "Discard";
            btnDiscard.UseVisualStyleBackColor = true;
            btnDiscard.Click += btnDiscard_Click;
            btnApply.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnApply.Location = new Point(362, 426);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(75, 23);
            btnApply.TabIndex = 1;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            toolTip.AutoPopDelay = 8000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "Information";
            toolTip.Popup += toolTip_Popup;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Controls.Add(tabPage10);
            tabControl1.Cursor = Cursors.Default;
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(615, 489);
            tabControl1.TabIndex = 2;
            tabPage8.Controls.Add(tabControl);
            tabPage8.Controls.Add(btnApply);
            tabPage8.Controls.Add(btnSave);
            tabPage8.Controls.Add(btnDiscard);
            tabPage8.Location = new Point(4, 25);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(607, 460);
            tabPage8.TabIndex = 0;
            tabPage8.Text = "General";
            tabPage8.UseVisualStyleBackColor = true;
            tabPage8.Click += tabPage8_Click;
            tabPage9.Controls.Add(tabControl2);
            tabPage9.Location = new Point(4, 25);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(607, 460);
            tabPage9.TabIndex = 1;
            tabPage9.Text = "Lava";
            tabPage9.UseVisualStyleBackColor = true;
            tabControl2.Controls.Add(tabPage11);
            tabControl2.Controls.Add(tabPage13);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(3, 3);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(601, 454);
            tabControl2.TabIndex = 2;
            tabPage11.BackColor = Color.Transparent;
            tabPage11.Controls.Add(groupBox2);
            tabPage11.Controls.Add(groupBox1);
            tabPage11.Location = new Point(4, 22);
            tabPage11.Name = "tabPage11";
            tabPage11.Padding = new Padding(3);
            tabPage11.Size = new Size(593, 428);
            tabPage11.TabIndex = 0;
            tabPage11.Text = "Settings I";
            groupBox2.Controls.Add(label44);
            groupBox2.Controls.Add(useHeaven);
            groupBox2.Controls.Add(label42);
            groupBox2.Controls.Add(heavenMapName);
            groupBox2.Controls.Add(setHeavenMapButton);
            groupBox2.Location = new Point(38, 217);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(309, 184);
            groupBox2.TabIndex = 36;
            groupBox2.TabStop = false;
            groupBox2.Text = "Heaven map";
            label44.Location = new Point(6, 71);
            label44.Name = "label44";
            label44.Size = new Size(103, 22);
            label44.TabIndex = 31;
            label44.Text = "Heaven map name:";
            useHeaven.Location = new Point(9, 36);
            useHeaven.Name = "useHeaven";
            useHeaven.Size = new Size(120, 24);
            useHeaven.TabIndex = 29;
            useHeaven.Text = "Heaven for ghosts";
            useHeaven.CheckedChanged += useHeaven_CheckedChanged;
            label42.AutoSize = true;
            label42.Location = new Point(49, 103);
            label42.MaximumSize = new Size(200, 0);
            label42.Name = "label42";
            label42.Size = new Size(191, 39);
            label42.TabIndex = 34;
            label42.Text = "The heaven map has to be located in lava/maps directory and it must not be used as lava map.";
            heavenMapName.Location = new Point(115, 68);
            heavenMapName.Name = "heavenMapName";
            heavenMapName.Size = new Size(97, 21);
            heavenMapName.TabIndex = 30;
            setHeavenMapButton.Location = new Point(226, 66);
            setHeavenMapButton.Name = "setHeavenMapButton";
            setHeavenMapButton.Size = new Size(42, 23);
            setHeavenMapButton.TabIndex = 32;
            setHeavenMapButton.Text = "Set";
            setHeavenMapButton.Click += setHeavenMap_Click;
            groupBox1.Controls.Add(label45);
            groupBox1.Controls.Add(label46);
            groupBox1.Controls.Add(updateTimeSettingsButton);
            groupBox1.Controls.Add(txtTime2);
            groupBox1.Controls.Add(label48);
            groupBox1.Controls.Add(label49);
            groupBox1.Controls.Add(txtTime1);
            groupBox1.Location = new Point(38, 45);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(309, 155);
            groupBox1.TabIndex = 35;
            groupBox1.TabStop = false;
            groupBox1.Text = "Global round time";
            label45.AutoSize = true;
            label45.Location = new Point(217, 79);
            label45.Name = "label45";
            label45.Size = new Size(54, 13);
            label45.TabIndex = 5;
            label45.Text = "( Phase II )";
            label46.AutoSize = true;
            label46.Location = new Point(218, 44);
            label46.Name = "label46";
            label46.Size = new Size(51, 13);
            label46.TabIndex = 4;
            label46.Text = "( Phase I )";
            updateTimeSettingsButton.Location = new Point(51, 120);
            updateTimeSettingsButton.Name = "updateTimeSettingsButton";
            updateTimeSettingsButton.Size = new Size(132, 23);
            updateTimeSettingsButton.TabIndex = 0;
            updateTimeSettingsButton.Text = "Update Time Settings";
            updateTimeSettingsButton.Click += updateTime_Click;
            txtTime2.Location = new Point(97, 76);
            txtTime2.Name = "txtTime2";
            txtTime2.Size = new Size(100, 21);
            txtTime2.TabIndex = 2;
            txtTime2.Text = "8";
            label48.AutoSize = true;
            label48.Location = new Point(25, 43);
            label48.Name = "label48";
            label48.Size = new Size(66, 13);
            label48.TabIndex = 3;
            label48.Text = "Time before:";
            label49.AutoSize = true;
            label49.Location = new Point(25, 79);
            label49.Name = "label49";
            label49.Size = new Size(58, 13);
            label49.TabIndex = 3;
            label49.Text = "Time after:";
            txtTime1.Location = new Point(97, 41);
            txtTime1.Name = "txtTime1";
            txtTime1.Size = new Size(100, 21);
            txtTime1.TabIndex = 3;
            txtTime1.Text = "14";
            tabPage13.BackColor = Color.Transparent;
            tabPage13.Controls.Add(lavaPropertyGrid);
            tabPage13.Location = new Point(4, 22);
            tabPage13.Name = "tabPage13";
            tabPage13.Padding = new Padding(3);
            tabPage13.Size = new Size(593, 428);
            tabPage13.TabIndex = 1;
            tabPage13.Text = "Settings II";
            tabPage13.Visible = false;
            lavaPropertyGrid.Dock = DockStyle.Fill;
            lavaPropertyGrid.Location = new Point(3, 3);
            lavaPropertyGrid.Name = "lavaPropertyGrid";
            lavaSettings.AllowCuboidOnLavaMaps = false;
            lavaSettings.AllowGoldRockOnLavaMaps = false;
            lavaSettings.AllowInGameVariables = true;
            lavaSettings.AmountOfMoneyInTreasure = 5;
            lavaSettings.Antigrief = AntigriefType.BasedOnPlayersLevel;
            lavaSettings.AutoNameColoring = true;
            lavaSettings.AutoServerLock = false;
            lavaSettings.ConnectionSpeedTest = false;
            lavaSettings.DisallowBuildingNearLavaSpawn = true;
            lavaSettings.DisallowHacksUseOnLavaMap = false;
            lavaSettings.DisallowSpleefing = false;
            lavaSettings.DisallowSpongesNearLavaSpawn = true;
            lavaSettings.HacksUseOnLavaMapPermission = 80;
            lavaSettings.HeadlessGhosts = true;
            lavaSettings.HideDeathMessagesAmount = 20;
            lavaSettings.IsAfkDuringVoteAllowed = true;
            lavaSettings.LavaMapPlayerLimit = 0;
            lavaSettings.LavaMovementDelay = 4;
            lavaSettings.LavaState = LavaState.Disturbed;
            lavaSettings.LavaWorldChat = false;
            lavaSettings.LivesAtStart = 3;
            lavaSettings.OpsBypassSpleefPrevention = false;
            lavaSettings.OverloadProtection = false;
            lavaSettings.PreventScoreAbuse = true;
            lavaSettings.RandomLavaState = true;
            lavaSettings.RequiredDistanceFromSpawn = 5;
            lavaSettings.RequireRegistrationForPromotion = false;
            lavaSettings.RewardAboveSeaLevel = 30;
            lavaSettings.RewardBelowSeaLevel = 25;
            lavaSettings.ScoreMode = ScoreSystem.BasedOnAir;
            lavaSettings.ScoreRewardFixed = 500;
            lavaSettings.ShowDistanceOffsetMessage = true;
            lavaSettings.ShowMapAuthor = false;
            lavaSettings.ShowMapRating = true;
            lavaSettings.SpawnOnDeath = true;
            lavaSettings.StarSystem = true;
            lavaSettings.UpperLevelOfBoplAntigrief = 8;
            lavaSettings.VotingSystem = true;
            lavaPropertyGrid.SelectedObject = lavaSettings;
            lavaPropertyGrid.Size = new Size(587, 422);
            lavaPropertyGrid.TabIndex = 2;
            tabPage10.Controls.Add(zombiePropertyGrid);
            tabPage10.Location = new Point(4, 25);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new Padding(3);
            tabPage10.Size = new Size(607, 460);
            tabPage10.TabIndex = 2;
            tabPage10.Text = "Zombie";
            tabPage10.UseVisualStyleBackColor = true;
            zombiePropertyGrid.Dock = DockStyle.Fill;
            zombiePropertyGrid.Location = new Point(3, 3);
            zombiePropertyGrid.Name = "zombiePropertyGrid";
            infectionSettings.BlockGlitchPrevention = true;
            infectionSettings.BrokenNeckZombies = true;
            infectionSettings.DisallowHacksUseOnInfectionMap = true;
            infectionSettings.DisallowSpleefing = true;
            infectionSettings.HacksOnInfectionMapPermission = 100;
            infectionSettings.HumanTag = "%e[human] ";
            infectionSettings.IsAfkDuringVoteAllowed = true;
            infectionSettings.MapVoteDurationSeconds = 20;
            infectionSettings.MinimumPlayers = 1;
            infectionSettings.OpsBypassSpleefPrevention = false;
            infectionSettings.RefreeTag = "&f[refree] ";
            infectionSettings.RewardForHumansFixed = 30;
            infectionSettings.RewardForHumansMultipiler = 2;
            infectionSettings.RewardForZombiesFixed = 5;
            infectionSettings.RewardForZombiesMultipiler = 4;
            infectionSettings.RoundTime = 6;
            infectionSettings.ShowMapAuthor = false;
            infectionSettings.ShowMapRating = true;
            infectionSettings.SpeedHackDetectionThreshold = 250;
            infectionSettings.UsePlayerLevels = true;
            infectionSettings.VotingSystem = true;
            infectionSettings.ZombieAlias = "zombie";
            infectionSettings.ZombieTag = "&c[zombie] ";
            zombiePropertyGrid.SelectedObject = infectionSettings;
            zombiePropertyGrid.Size = new Size(601, 454);
            zombiePropertyGrid.TabIndex = 0;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(638, 503);
            Controls.Add(tabControl1);
            Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PropertyWindow";
            Text = "Properties";
            FormClosing += PropertyWindow_FormClosing;
            Load += PropertyWindow_Load;
            Disposed += PropertyWindow_Unload;
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            updatePanel.ResumeLayout(false);
            updatePanel.PerformLayout();
            misc3.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabControl3.ResumeLayout(false);
            tabPage15.ResumeLayout(false);
            tabPage15.PerformLayout();
            tabPage16.ResumeLayout(false);
            tabPage16.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage12.ResumeLayout(false);
            tabPage12.PerformLayout();
            tabPage14.ResumeLayout(false);
            tabControlChat.ResumeLayout(false);
            tabPageChat1.ResumeLayout(false);
            tabPageChat1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabPageChatBadWords.ResumeLayout(false);
            tabPageChatBadWords.PerformLayout();
            tabPageChatCaps.ResumeLayout(false);
            tabPageChatCaps.PerformLayout();
            tabPageChatCharSpam.ResumeLayout(false);
            tabPageChatCharSpam.PerformLayout();
            tabPageChatSpam.ResumeLayout(false);
            tabPageChatSpam.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage8.ResumeLayout(false);
            tabPage9.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage11.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage13.ResumeLayout(false);
            tabPage10.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}