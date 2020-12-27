using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    // Token: 0x02000304 RID: 772
    public partial class PropertyWindow : Form
    {
        // Token: 0x040009CD RID: 2509
        private BadWordsEditor badWordsEditor;

        // Token: 0x040009D2 RID: 2514
        private bool skip;

        // Token: 0x040009D1 RID: 2513
        private readonly List<Block.Blocks> storedBlocks = new List<Block.Blocks>();

        // Token: 0x040009D0 RID: 2512
        private readonly List<GrpCommands.rankAllowance> storedCommands = new List<GrpCommands.rankAllowance>();

        // Token: 0x040009CF RID: 2511
        private readonly List<Group> storedRanks = new List<Group>();

        // Token: 0x040009CE RID: 2510
        private WhiteListEditor whiteListEditor;

        // Token: 0x060015D4 RID: 5588 RVA: 0x0007805C File Offset: 0x0007625C
        public PropertyWindow()
        {
            InitializeComponent();
            UpdateProperties();
            UpdateLavaProperties();
            UpdateZombieProperties();
            InitChatFilterTab();
        }

        // Token: 0x060015D5 RID: 5589 RVA: 0x000780B0 File Offset: 0x000762B0
        private void UpdateZombieProperties()
        {
            zombiePropertyGrid.SelectedObject = InfectionSettings.All;
        }

        // Token: 0x060015D6 RID: 5590 RVA: 0x000780C4 File Offset: 0x000762C4
        public void ShowAt(Point location)
        {
            StartPosition = FormStartPosition.Manual;
            var x = location.X - Width / 2;
            var y = location.Y - Height / 2;
            Location = new Point(x, y);
            base.Show();
        }

        // Token: 0x060015D7 RID: 5591 RVA: 0x00078110 File Offset: 0x00076310
        private void PropertyWindow_Load(object sender, EventArgs e)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
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
            cmbDefaultColour.Items.AddRange(items);
            cmbIRCColour.Items.AddRange(items);
            cmbColor.Items.AddRange(items);
            var text = "";
            foreach (var group in Group.groupList)
            {
                cmbDefaultRank.Items.Add(group.name);
                cmbOpChat.Items.Add(group.name);
                if (group.Permission == Server.opchatperm) text = group.name;
            }

            cmbDefaultRank.SelectedIndex = 1;
            cmbOpChat.SelectedIndex = text != "" ? cmbOpChat.Items.IndexOf(text) : 1;
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

        // Token: 0x060015D8 RID: 5592 RVA: 0x00078300 File Offset: 0x00076500
        private void PropertyWindow_Unload(object sender, EventArgs e)
        {
            Window.prevLoaded = false;
            GeneralSettings.All.Save();
        }

        // Token: 0x060015D9 RID: 5593 RVA: 0x00078314 File Offset: 0x00076514
        public void LoadRanks()
        {
            txtCmdRanks.Text = "The following ranks are available: \r\n\r\n";
            listRanks.Items.Clear();
            storedRanks.Clear();
            storedRanks.AddRange(Group.groupList);
            foreach (var group in storedRanks)
            {
                var textBox = txtCmdRanks;
                object text = textBox.Text;
                textBox.Text = string.Concat(text, "\t", group.name, " (", (int) group.Permission, ")\r\n");
                listRanks.Items.Add(group.trueName + "  =  " + (int) group.Permission);
            }

            txtBlRanks.Text = txtCmdRanks.Text;
            listRanks.SelectedIndex = 0;
        }

        // Token: 0x060015DA RID: 5594 RVA: 0x00078444 File Offset: 0x00076644
        public void SaveRanks()
        {
            Group.saveGroups(storedRanks);
            Group.InitAll();
            LoadRanks();
        }

        // Token: 0x060015DB RID: 5595 RVA: 0x0007845C File Offset: 0x0007665C
        public void LoadCommands()
        {
            listCommands.Items.Clear();
            storedCommands.Clear();
            foreach (var rankAllowance in GrpCommands.allowedCommands)
            {
                storedCommands.Add(rankAllowance);
                listCommands.Items.Add(rankAllowance.commandName);
            }

            if (listCommands.SelectedIndex == -1) listCommands.SelectedIndex = 0;
        }

        // Token: 0x060015DC RID: 5596 RVA: 0x00078500 File Offset: 0x00076700
        public void SaveCommands()
        {
            GeneralSettings.All.CooldownCmdUse = checkBoxCmdCooldown.Checked;
            try
            {
                GeneralSettings.All.CooldownCmdMax = int.Parse(textBoxCmdMax.Text);
            }
            catch
            {
            }

            try
            {
                GeneralSettings.All.CooldownCmdMaxSeconds = int.Parse(textBoxCmdMaxSeconds.Text);
            }
            catch
            {
            }

            GeneralSettings.All.CooldownCmdWarning = textBoxCmdWarning.Text;
            GrpCommands.Save(storedCommands);
            GrpCommands.fillRanks();
            LoadCommands();
        }

        // Token: 0x060015DD RID: 5597 RVA: 0x000785A8 File Offset: 0x000767A8
        public void LoadBlocks()
        {
            listBlocks.Items.Clear();
            storedBlocks.Clear();
            storedBlocks.AddRange(Block.BlockList);
            foreach (var blocks in storedBlocks)
                if (Block.Name(blocks.type) != "unknown")
                    listBlocks.Items.Add(Block.Name(blocks.type));
            if (listBlocks.SelectedIndex == -1) listBlocks.SelectedIndex = 0;
        }

        // Token: 0x060015DE RID: 5598 RVA: 0x0007866C File Offset: 0x0007686C
        public void SaveBlocks()
        {
            Block.SaveBlocks(storedBlocks);
            Block.SetBlocks();
            LoadBlocks();
        }

        // Token: 0x060015DF RID: 5599 RVA: 0x00078684 File Offset: 0x00076884
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

        // Token: 0x060015E0 RID: 5600 RVA: 0x00078740 File Offset: 0x00076940
        public void LoadProp(string givenPath)
        {
            if (!File.Exists(givenPath)) return;
            var array = File.ReadAllLines(givenPath);
            var array2 = array;
            foreach (var text in array2)
            {
                if (text == "" || text[0] == '#') continue;
                var text2 = text.Split('=')[0].Trim();
                var text3 = text.Substring(text.IndexOf('=') + 1).Trim();
                var text4 = "";
                switch (text2.ToLower())
                {
                    case "server-name":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                            txtName.Text = text3;
                        else
                            txtName.Text = "MCDzienny Default Server Name";
                        break;
                    case "motd":
                        if (ValidString(text3, "![]&:.,{}~-+()?_/\\= "))
                            txtMOTD.Text = text3;
                        else
                            txtMOTD.Text = "Welcome to my server!";
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
                        chkVerify.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "public":
                        chkPublic.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "world-chat":
                        chkWorld.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "max-players":
                        try
                        {
                            if (Convert.ToByte(text3) > byte.MaxValue)
                                text3 = "255";
                            else if (Convert.ToByte(text3) < 0) text3 = "0";
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
                                text3 = "100";
                            else if (Convert.ToByte(text3) < 1) text3 = "1";
                            txtMaps.Text = text3;
                        }
                        catch
                        {
                            Server.s.Log("max-maps invalid! setting to default.");
                            txtMaps.Text = "5";
                        }

                        break;
                    case "irc-use":
                        chkIRC.Checked = text3.ToLower() == "true" ? true : false;
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
                        ChkTunnels.Checked = text3.ToLower() == "true" ? true : false;
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
                        chkForceCuboid.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "backup-time":
                        try
                        {
                            var num = int.Parse(text3);
                            txtBackup.Text = num.ToString();
                        }
                        catch
                        {
                            txtBackup.Text = "300";
                        }

                        break;
                    case "backup-location":
                        if (!text3.Contains("System.Windows.Forms.TextBox, Text:")) txtBackupLocation.Text = text3;
                        break;
                    case "physicsrestart":
                        chkPhysicsRest.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "deathcount":
                        chkDeath.Checked = text3.ToLower() == "true" ? true : false;
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
                                cmbDefaultRank.SelectedIndex = cmbDefaultRank.Items.IndexOf(text3.ToLower());
                        }
                        catch
                        {
                            cmbDefaultRank.SelectedIndex = 1;
                        }

                        break;
                    case "old-help":
                        chkHelp.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "cheapmessage":
                        chkCheap.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "cheap-message-given":
                        txtCheap.Text = text3;
                        break;
                    case "rank-super":
                        chkrankSuper.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "custom-ban":
                        chkBanMessage.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "custom-ban-message":
                        txtBanMessage.Text = text3;
                        break;
                    case "custom-shutdown":
                        chkShutdown.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "custom-shutdown-message":
                        txtShutdown.Text = text3;
                        break;
                    case "auto-restart":
                        chkRestartTime.Checked = text3.ToLower() == "true" ? true : false;
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
                        chkUpdates.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "autoload":
                        chkAutoload.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "parse-emotes":
                        chkSmile.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "main-name":
                        txtMain.Text = text3;
                        break;
                    case "dollar-before-dollar":
                        chk17Dollar.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "money-name":
                        txtMoneys.Text = text3;
                        break;
                    case "restart-on-error":
                        chkRestart.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "repeat-messages":
                        chkRepeatMessages.Checked = text3.ToLower() == "true" ? true : false;
                        break;
                    case "host-state":
                        if (text3 != "") txtHost.Text = text3;
                        break;
                    case "server-description":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                            descriptionBox.Text = text3;
                        else
                            descriptionBox.Text = Server.description;
                        break;
                    case "server-flag":
                        if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                            flagBox.Text = text3;
                        else
                            flagBox.Text = Server.Flag;
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

        // Token: 0x060015E1 RID: 5601 RVA: 0x000793EC File Offset: 0x000775EC
        public bool ValidString(string str, string allowed)
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890" + allowed;
            foreach (var value in str)
                if (text.IndexOf(value) == -1)
                    return false;
            return true;
        }

        // Token: 0x060015E2 RID: 5602 RVA: 0x00079438 File Offset: 0x00077638
        public void Save(string givenPath)
        {
            try
            {
                using (var streamWriter = new StreamWriter(File.Create(givenPath)))
                {
                    streamWriter.WriteLine(
                        "# Edit the settings below to modify how your server operates. This is an explanation of what each setting does.");
                    streamWriter.WriteLine("#   server-name\t=\tThe name which displays on minecraft.net");
                    streamWriter.WriteLine("#   motd\t=\tThe message which displays when a player connects");
                    streamWriter.WriteLine("#   port\t=\tThe port to operate from");
                    streamWriter.WriteLine(
                        "#   console-only\t=\tRun without a GUI (useful for Linux servers with mono)");
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
                    streamWriter.WriteLine(
                        "#   irc-password\t=\tThe password you want to use if you're identifying with nickserv");
                    streamWriter.WriteLine("#   anti-tunnels\t=\tStops people digging below max-depth");
                    streamWriter.WriteLine("#   max-depth\t=\tThe maximum allowed depth to dig down");
                    streamWriter.WriteLine("#   backup-time\t=\tThe number of seconds between automatic backups");
                    streamWriter.WriteLine(
                        "#   overload\t=\tThe higher this is, the longer the physics is allowed to lag. Default 1500");
                    streamWriter.WriteLine(
                        "#   use-whitelist\t=\tSwitch to allow use of a whitelist to override IP bans for certain players.  Default false.");
                    streamWriter.WriteLine(
                        "#   force-cuboid\t=\tRun cuboid until the limit is hit, instead of canceling the whole operation.  Default false.");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#   Host\t=\tThe host name for the database (usually 127.0.0.1)");
                    streamWriter.WriteLine(
                        "#   SQLPort\t=\tPort number to be used for MySQL.  Unless you manually changed the port, leave this alone.  Default 3306.");
                    streamWriter.WriteLine(
                        "#   Username\t=\tThe username you used to create the database (usually root)");
                    streamWriter.WriteLine("#   Password\t=\tThe password set while making the database");
                    streamWriter.WriteLine("#   DatabaseName\t=\tThe name of the database stored (Default = MCZall)");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(
                        "#   defaultColor\t=\tThe color code of the default messages (Default = &e)");
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
                        streamWriter.WriteLine("main-name = " + txtMain.Text);
                    else
                        streamWriter.WriteLine("main-name = main");
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
                    streamWriter.WriteLine("opchat-perm = " + (sbyte) Group.groupList
                        .Find(grp => grp.name == cmbOpChat.Items[cmbOpChat.SelectedIndex].ToString()).Permission);
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
                    streamWriter.WriteLine(
                        "lava-state = " + Enum.GetName(typeof(LavaState), LavaSettings.All.LavaState));
                    streamWriter.WriteLine("global-time-before = " + LavaSystem.stime);
                    streamWriter.WriteLine("global-time-after = " + LavaSystem.stime2);
                    streamWriter.WriteLine("reappearing-message = " +
                                           Server.serverMessage.Replace(Environment.NewLine, "^"));
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

        // Token: 0x060015E3 RID: 5603 RVA: 0x0007A000 File Offset: 0x00078200
        private void cmbDefaultColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDefault.BackColor = Color.FromName(cmbDefaultColour.Items[cmbDefaultColour.SelectedIndex].ToString());
        }

        // Token: 0x060015E4 RID: 5604 RVA: 0x0007A034 File Offset: 0x00078234
        private void cmbIRCColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIRC.BackColor = Color.FromName(cmbIRCColour.Items[cmbIRCColour.SelectedIndex].ToString());
        }

        // Token: 0x060015E5 RID: 5605 RVA: 0x0007A068 File Offset: 0x00078268
        private void ClearIfNotNumber(TextBox foundTxt)
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

        // Token: 0x060015E6 RID: 5606 RVA: 0x0007A0BC File Offset: 0x000782BC
        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtPort);
        }

        // Token: 0x060015E7 RID: 5607 RVA: 0x0007A0CC File Offset: 0x000782CC
        private void txtPlayers_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtPlayers);
        }

        // Token: 0x060015E8 RID: 5608 RVA: 0x0007A0DC File Offset: 0x000782DC
        private void txtMaps_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtMaps);
        }

        // Token: 0x060015E9 RID: 5609 RVA: 0x0007A0EC File Offset: 0x000782EC
        private void txtBackup_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtBackup);
        }

        // Token: 0x060015EA RID: 5610 RVA: 0x0007A0FC File Offset: 0x000782FC
        private void txtDepth_TextChanged(object sender, EventArgs e)
        {
            ClearIfNotNumber(txtDepth);
        }

        // Token: 0x060015EB RID: 5611 RVA: 0x0007A10C File Offset: 0x0007830C
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveStuff();
            SaveChatFilterTab();
            base.Dispose();
        }

        // Token: 0x060015EC RID: 5612 RVA: 0x0007A120 File Offset: 0x00078320
        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyChatFilterTab();
            saveStuff();
        }

        // Token: 0x060015ED RID: 5613 RVA: 0x0007A130 File Offset: 0x00078330
        private void saveStuff()
        {
            foreach (var obj in tabControl.Controls)
            {
                var control = (Control) obj;
                if (control is TabPage && control != tabPage3 && control != tabPage5)
                    foreach (var obj2 in control.Controls)
                    {
                        var control2 = (Control) obj2;
                        if (control2 is TextBox && control2.Text == "" && control2 != serverMessage &&
                            control2 != vipEntry)
                        {
                            MessageBox.Show("A textbox has been left empty. It must be filled.\n" + control2.Name);
                            return;
                        }
                    }
            }

            Save("properties/server.properties");
            SaveRanks();
            SaveCommands();
            SaveBlocks();
            ServerProperties.Load("properties/server.properties", true);
            GrpCommands.fillRanks();
        }

        // Token: 0x060015EE RID: 5614 RVA: 0x0007A268 File Offset: 0x00078468
        private void btnDiscard_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        // Token: 0x060015EF RID: 5615 RVA: 0x0007A270 File Offset: 0x00078470
        private void toolTip_Popup(object sender, PopupEventArgs e)
        {
        }

        // Token: 0x060015F0 RID: 5616 RVA: 0x0007A274 File Offset: 0x00078474
        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x060015F1 RID: 5617 RVA: 0x0007A278 File Offset: 0x00078478
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x060015F2 RID: 5618 RVA: 0x0007A27C File Offset: 0x0007847C
        private void chkPhysicsRest_CheckedChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x060015F3 RID: 5619 RVA: 0x0007A280 File Offset: 0x00078480
        private void chkGC_CheckedChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x060015F4 RID: 5620 RVA: 0x0007A284 File Offset: 0x00078484
        private void btnBackup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently glitchy! Just type in the location by hand.");
        }

        // Token: 0x060015F5 RID: 5621 RVA: 0x0007A294 File Offset: 0x00078494
        private void listRanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skip) return;
            var group = storedRanks.Find(grp =>
                grp.trueName == listRanks.Items[listRanks.SelectedIndex].ToString().Split('=')[0].Trim());
            if (group.Permission == LevelPermission.Nobody)
            {
                listRanks.SelectedIndex = 0;
                return;
            }

            txtRankName.Text = group.trueName;
            Control control = txtPermission;
            var permission = (int) group.Permission;
            control.Text = permission.ToString();
            txtLimit.Text = group.maxBlocks.ToString();
            cmbColor.SelectedIndex = cmbColor.Items.IndexOf(c.Name(group.color));
            txtFileName.Text = group.fileName;
            txtPromotionPrice.Text = group.promotionPrice.ToString();
            textBoxSmallMaps.Text = group.smallMaps.ToString();
            textBoxMediumMaps.Text = group.mediumMaps.ToString();
            textBoxBigMaps.Text = group.bigMaps.ToString();
        }

        // Token: 0x060015F6 RID: 5622 RVA: 0x0007A3A8 File Offset: 0x000785A8
        private void txtRankName_TextChanged(object sender, EventArgs e)
        {
            if (txtRankName.Text != "" && txtRankName.Text.ToLower() != "nobody")
            {
                storedRanks[listRanks.SelectedIndex].trueName = txtRankName.Text;
                skip = true;
                listRanks.Items[listRanks.SelectedIndex] = txtRankName.Text + "  =  " +
                                                           (int) storedRanks[listRanks.SelectedIndex].Permission;
                skip = false;
            }
        }

        // Token: 0x060015F7 RID: 5623 RVA: 0x0007A474 File Offset: 0x00078674
        private void txtPermission_TextChanged(object sender, EventArgs e)
        {
            if (txtPermission.Text != "")
            {
                int num;
                try
                {
                    num = int.Parse(txtPermission.Text);
                }
                catch
                {
                    if (txtPermission.Text != "-")
                        txtPermission.Text = txtPermission.Text.Remove(txtPermission.Text.Length - 1);
                    return;
                }

                if (num < -50)
                {
                    txtPermission.Text = "-50";
                }
                else
                {
                    if (num > 119)
                    {
                        txtPermission.Text = "119";
                        return;
                    }

                    storedRanks[listRanks.SelectedIndex].Permission = (LevelPermission) num;
                    skip = true;
                    listRanks.Items[listRanks.SelectedIndex] =
                        storedRanks[listRanks.SelectedIndex].trueName + "  =  " + num;
                    skip = false;
                }
            }
        }

        // Token: 0x060015F8 RID: 5624 RVA: 0x0007A5A4 File Offset: 0x000787A4
        private void txtLimit_TextChanged(object sender, EventArgs e)
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

                if (num >= 1)
                {
                    storedRanks[listRanks.SelectedIndex].maxBlocks = num;
                    return;
                }

                txtLimit.Text = "1";
            }
        }

        // Token: 0x060015F9 RID: 5625 RVA: 0x0007A64C File Offset: 0x0007884C
        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            if (txtFileName.Text != "") storedRanks[listRanks.SelectedIndex].fileName = txtFileName.Text;
        }

        // Token: 0x060015FA RID: 5626 RVA: 0x0007A68C File Offset: 0x0007888C
        private void btnAddRank_Click(object sender, EventArgs e)
        {
            new Random();
            var group = new Group((LevelPermission) 5, 600, "CHANGEME", '1', "CHANGEME.txt", 0);
            storedRanks.Add(group);
            listRanks.Items.Add(group.trueName + "  =  " + (int) group.Permission);
        }

        // Token: 0x060015FB RID: 5627 RVA: 0x0007A6F0 File Offset: 0x000788F0
        private void button1_Click(object sender, EventArgs e)
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

        // Token: 0x060015FC RID: 5628 RVA: 0x0007A75C File Offset: 0x0007895C
        private void listCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmd = Command.all.Find(listCommands.SelectedItem.ToString());
            var rankAllowance = storedCommands.Find(aV => aV.commandName == cmd.name);
            if (Group.findPerm(rankAllowance.lowestRank) == null) rankAllowance.lowestRank = cmd.defaultRank;
            txtCmdLowest.Text = string.Concat((int) rankAllowance.lowestRank);
            var flag = false;
            txtCmdDisallow.Text = "";
            foreach (var levelPermission in rankAllowance.disallow)
            {
                flag = true;
                var textBox = txtCmdDisallow;
                textBox.Text = textBox.Text + "," + (int) levelPermission;
            }

            if (flag) txtCmdDisallow.Text = txtCmdDisallow.Text.Remove(0, 1);
            flag = false;
            txtCmdAllow.Text = "";
            foreach (var levelPermission2 in rankAllowance.allow)
            {
                flag = true;
                var textBox2 = txtCmdAllow;
                textBox2.Text = textBox2.Text + "," + (int) levelPermission2;
            }

            if (flag) txtCmdAllow.Text = txtCmdAllow.Text.Remove(0, 1);
            textBoxCmdShortcut.Text = cmd.shortcut;
        }

        // Token: 0x060015FD RID: 5629 RVA: 0x0007A924 File Offset: 0x00078B24
        private void txtCmdLowest_TextChanged(object sender, EventArgs e)
        {
            fillLowest(ref txtCmdLowest, ref storedCommands[listCommands.SelectedIndex].lowestRank);
        }

        // Token: 0x060015FE RID: 5630 RVA: 0x0007A950 File Offset: 0x00078B50
        private void txtCmdDisallow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtCmdDisallow, ref storedCommands[listCommands.SelectedIndex].disallow);
        }

        // Token: 0x060015FF RID: 5631 RVA: 0x0007A97C File Offset: 0x00078B7C
        private void txtCmdAllow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtCmdAllow, ref storedCommands[listCommands.SelectedIndex].allow);
        }

        // Token: 0x06001600 RID: 5632 RVA: 0x0007A9A8 File Offset: 0x00078BA8
        private void listBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            var b = Block.Byte(listBlocks.SelectedItem.ToString());
            var blocks = storedBlocks.Find(bS => bS.type == b);
            Control control = txtBlLowest;
            var lowestRank = (int) blocks.lowestRank;
            control.Text = lowestRank.ToString();
            var flag = false;
            txtBlDisallow.Text = "";
            foreach (var levelPermission in blocks.disallow)
            {
                flag = true;
                var textBox = txtBlDisallow;
                textBox.Text = textBox.Text + "," + (int) levelPermission;
            }

            if (flag) txtBlDisallow.Text = txtBlDisallow.Text.Remove(0, 1);
            flag = false;
            txtBlAllow.Text = "";
            foreach (var levelPermission2 in blocks.allow)
            {
                flag = true;
                var textBox2 = txtBlAllow;
                textBox2.Text = textBox2.Text + "," + (int) levelPermission2;
            }

            if (flag) txtBlAllow.Text = txtBlAllow.Text.Remove(0, 1);
        }

        // Token: 0x06001601 RID: 5633 RVA: 0x0007AB34 File Offset: 0x00078D34
        private void txtBlLowest_TextChanged(object sender, EventArgs e)
        {
            fillLowest(ref txtBlLowest,
                ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).lowestRank);
        }

        // Token: 0x06001602 RID: 5634 RVA: 0x0007AB60 File Offset: 0x00078D60
        private void txtBlDisallow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtBlDisallow,
                ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).disallow);
        }

        // Token: 0x06001603 RID: 5635 RVA: 0x0007AB8C File Offset: 0x00078D8C
        private void txtBlAllow_TextChanged(object sender, EventArgs e)
        {
            fillAllowance(ref txtBlAllow,
                ref storedBlocks.Find(b => b.type == Block.Byte(listBlocks.SelectedItem.ToString())).allow);
        }

        // Token: 0x06001604 RID: 5636 RVA: 0x0007ABB8 File Offset: 0x00078DB8
        private void fillAllowance(ref TextBox txtBox, ref List<LevelPermission> addTo)
        {
            addTo.Clear();
            if (txtBox.Text != "")
            {
                var array = txtBox.Text.Split(',');
                var i = 0;
                while (i < array.Length)
                {
                    array[i] = array[i].Trim().ToLower();
                    int item;
                    try
                    {
                        item = int.Parse(array[i]);
                    }
                    catch
                    {
                        var group = Group.Find(array[i]);
                        if (group == null)
                        {
                            Server.s.Log("Could not find " + array[i]);
                            goto IL_93;
                        }

                        item = (int) group.Permission;
                    }

                    goto IL_8B;
                    IL_93:
                    i++;
                    continue;
                    IL_8B:
                    addTo.Add((LevelPermission) item);
                    goto IL_93;
                }

                txtBox.Text = "";
                foreach (var levelPermission in addTo)
                {
                    var textBox = txtBox;
                    textBox.Text = textBox.Text + "," + (int) levelPermission;
                }

                if (txtBox.Text != "") txtBox.Text = txtBox.Text.Remove(0, 1);
            }
        }

        // Token: 0x06001605 RID: 5637 RVA: 0x0007AD00 File Offset: 0x00078F00
        private void fillLowest(ref TextBox txtBox, ref LevelPermission toChange)
        {
            if (txtBox.Text != "")
            {
                txtBox.Text = txtBox.Text.Trim().ToLower();
                var num = -100;
                try
                {
                    num = int.Parse(txtBox.Text);
                }
                catch
                {
                    var group = Group.Find(txtBox.Text);
                    if (group != null)
                        num = (int) group.Permission;
                    else
                        Server.s.Log("Could not find " + txtBox.Text);
                }

                txtBox.Text = "";
                if (num < -99)
                    txtBox.Text = string.Concat((int) toChange);
                else
                    txtBox.Text = string.Concat(num);
                toChange = (LevelPermission) Convert.ToInt16(txtBox.Text);
            }
        }

        // Token: 0x06001606 RID: 5638 RVA: 0x0007ADDC File Offset: 0x00078FDC
        private void btnBlHelp_Click(object sender, EventArgs e)
        {
            getHelp(listBlocks.SelectedItem.ToString());
        }

        // Token: 0x06001607 RID: 5639 RVA: 0x0007ADF4 File Offset: 0x00078FF4
        private void btnCmdHelp_Click(object sender, EventArgs e)
        {
            getHelp(listCommands.SelectedItem.ToString());
        }

        // Token: 0x06001608 RID: 5640 RVA: 0x0007AE0C File Offset: 0x0007900C
        private void getHelp(string toHelp)
        {
            Player.storedHelp = "";
            Player.storeHelp = true;
            Command.all.Find("help").Use(null, toHelp);
            Player.storeHelp = false;
            var text = "Help information for " + toHelp + ":\r\n\r\n";
            text += Player.storedHelp;
            MessageBox.Show(text);
        }

        // Token: 0x06001609 RID: 5641 RVA: 0x0007AE6C File Offset: 0x0007906C
        private void chkIRC_CheckedChanged(object sender, EventArgs e)
        {
            Server.irc = chkIRC.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600160A RID: 5642 RVA: 0x0007AE88 File Offset: 0x00079088
        private void textBox1_TextChanged(object sender, EventArgs e)
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

                if (num >= 0)
                {
                    storedRanks[listRanks.SelectedIndex].promotionPrice = num;
                    return;
                }

                txtLimit.Text = "0";
            }
        }

        // Token: 0x0600160B RID: 5643 RVA: 0x0007AF14 File Offset: 0x00079114
        private void txtMOTD_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600160C RID: 5644 RVA: 0x0007AF18 File Offset: 0x00079118
        private void FlowControl_Scroll(object sender, ScrollEventArgs e)
        {
            if (FlowControl.Value < 11) Server.updateTimer.Interval = 40 + (FlowControl.Value - 1) * 10;
            if (FlowControl.Value >= 11 && FlowControl.Value < 21)
                Server.updateTimer.Interval = 150 + (FlowControl.Value - 11) * 20;
            if (FlowControl.Value >= 21 && FlowControl.Value < 31)
                Server.updateTimer.Interval = 380 + (FlowControl.Value - 21) * 50;
            if (FlowControl.Value >= 31 && FlowControl.Value < 41)
                Server.updateTimer.Interval = 900 + (FlowControl.Value - 31) * 100;
            if (FlowControl.Value >= 41 && FlowControl.Value < 51)
                Server.updateTimer.Interval = 2000 + (FlowControl.Value - 41) * 1000;
            txtPositionDelay.Text = Server.updateTimer.Interval.ToString();
        }

        // Token: 0x0600160D RID: 5645 RVA: 0x0007B074 File Offset: 0x00079274
        private void PositionDelayUpdate_Click(object sender, EventArgs e)
        {
            if (txtPositionDelay.Text != "")
                try
                {
                    Server.updateTimer.Interval = int.Parse(txtPositionDelay.Text.Trim(' '));
                }
                catch
                {
                    Server.s.Log("Error: Delay time given is incorrect");
                }
        }

        // Token: 0x0600160E RID: 5646 RVA: 0x0007B0EC File Offset: 0x000792EC
        private void vipRemove_Click(object sender, EventArgs e)
        {
            if (vipList.SelectedItem != null)
            {
                VipList.RemoveVIP(vipList.SelectedItem.ToString());
                vipList.Items.Clear();
                vipList.Items.AddRange(VipList.GetArray());
            }
        }

        // Token: 0x0600160F RID: 5647 RVA: 0x0007B140 File Offset: 0x00079340
        private void vipAdd_Click(object sender, EventArgs e)
        {
            VipList.AddVIP(vipEntry.Text);
            vipEntry.Text = "";
            vipList.Items.Clear();
            vipList.Items.AddRange(VipList.GetArray());
        }

        // Token: 0x06001610 RID: 5648 RVA: 0x0007B194 File Offset: 0x00079394
        private void chkIRC_VisibleChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001611 RID: 5649 RVA: 0x0007B198 File Offset: 0x00079398
        private void btnSetServerMessage_Click(object sender, EventArgs e)
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
                    return;
                }

                Server.serverMessage = serverMessage.Text;
                LavaSystem.StartServerMessage();
            }
            else
            {
                serverMessage.Text = "Invalid character used.";
                LavaSystem.StopServerMessage();
                serverMessageInterval.Text = "stopped";
            }
        }

        // Token: 0x06001612 RID: 5650 RVA: 0x0007B2AC File Offset: 0x000794AC
        private void FlowControl_Scroll_1(object sender, ScrollEventArgs e)
        {
        }

        // Token: 0x06001613 RID: 5651 RVA: 0x0007B2B0 File Offset: 0x000794B0
        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            storedRanks[listRanks.SelectedIndex].color = c.Parse(cmbColor.Items[cmbColor.SelectedIndex].ToString());
        }

        // Token: 0x06001614 RID: 5652 RVA: 0x0007B300 File Offset: 0x00079500
        private void vipAdd_Click_1(object sender, EventArgs e)
        {
        }

        // Token: 0x06001615 RID: 5653 RVA: 0x0007B304 File Offset: 0x00079504
        private void vipRemove_Click_1(object sender, EventArgs e)
        {
        }

        // Token: 0x06001616 RID: 5654 RVA: 0x0007B308 File Offset: 0x00079508
        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001617 RID: 5655 RVA: 0x0007B30C File Offset: 0x0007950C
        private void autoFlagCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (autoFlagCheck.Checked)
            {
                flagBox.Enabled = false;
                flagBox.Text = Server.Flag;
                return;
            }

            flagBox.Enabled = true;
        }

        // Token: 0x06001618 RID: 5656 RVA: 0x0007B344 File Offset: 0x00079544
        private void flagBox_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x06001619 RID: 5657 RVA: 0x0007B348 File Offset: 0x00079548
        private void txtBlLowest_TextChanged_1(object sender, EventArgs e)
        {
        }

        // Token: 0x0600161A RID: 5658 RVA: 0x0007B34C File Offset: 0x0007954C
        private void serverMessage_TextChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600161B RID: 5659 RVA: 0x0007B350 File Offset: 0x00079550
        private void generalProperties_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x0600161C RID: 5660 RVA: 0x0007B354 File Offset: 0x00079554
        private void chkWorld_CheckedChanged(object sender, EventArgs e)
        {
            Server.worldChat = chkWorld.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600161D RID: 5661 RVA: 0x0007B370 File Offset: 0x00079570
        private void chkVerify_CheckedChanged(object sender, EventArgs e)
        {
            Server.verify = chkVerify.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600161E RID: 5662 RVA: 0x0007B38C File Offset: 0x0007958C
        private void chkPublic_CheckedChanged(object sender, EventArgs e)
        {
            Server.isPublic = chkPublic.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600161F RID: 5663 RVA: 0x0007B3A8 File Offset: 0x000795A8
        private void chkAutoload_CheckedChanged(object sender, EventArgs e)
        {
            Server.AutoLoad = chkAutoload.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001620 RID: 5664 RVA: 0x0007B3C4 File Offset: 0x000795C4
        private void ChkTunnels_CheckedChanged(object sender, EventArgs e)
        {
            Server.antiTunnel = ChkTunnels.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001621 RID: 5665 RVA: 0x0007B3E0 File Offset: 0x000795E0
        private void chkRestart_CheckedChanged(object sender, EventArgs e)
        {
            Server.restartOnError = chkRestart.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001622 RID: 5666 RVA: 0x0007B3FC File Offset: 0x000795FC
        private void chkRepeatMessages_CheckedChanged(object sender, EventArgs e)
        {
            Server.repeatMessage = chkRepeatMessages.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001623 RID: 5667 RVA: 0x0007B418 File Offset: 0x00079618
        private void chk17Dollar_CheckedChanged(object sender, EventArgs e)
        {
            Server.useDollarSign = chk17Dollar.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001624 RID: 5668 RVA: 0x0007B434 File Offset: 0x00079634
        private void chkSmile_CheckedChanged(object sender, EventArgs e)
        {
            Server.parseSmiley = chkSmile.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001625 RID: 5669 RVA: 0x0007B450 File Offset: 0x00079650
        private void chkForceCuboid_CheckedChanged(object sender, EventArgs e)
        {
            Server.forceCuboid = chkForceCuboid.Checked;
            ActiveControl = null;
        }

        // Token: 0x06001626 RID: 5670 RVA: 0x0007B46C File Offset: 0x0007966C
        private void chkBanMessage_CheckedChanged(object sender, EventArgs e)
        {
            Server.customBan = chkBanMessage.Checked;
        }

        // Token: 0x06001627 RID: 5671 RVA: 0x0007B480 File Offset: 0x00079680
        private void chkShutdown_CheckedChanged(object sender, EventArgs e)
        {
            Server.customShutdown = chkShutdown.Checked;
        }

        // Token: 0x06001628 RID: 5672 RVA: 0x0007B494 File Offset: 0x00079694
        private void chkCheap_CheckedChanged(object sender, EventArgs e)
        {
            Server.cheapMessage = chkCheap.Checked;
        }

        // Token: 0x06001629 RID: 5673 RVA: 0x0007B4A8 File Offset: 0x000796A8
        private void chkRestartTime_CheckedChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600162A RID: 5674 RVA: 0x0007B4AC File Offset: 0x000796AC
        private void chkrankSuper_CheckedChanged(object sender, EventArgs e)
        {
        }

        // Token: 0x0600162B RID: 5675 RVA: 0x0007B4B0 File Offset: 0x000796B0
        private void chkHelp_CheckedChanged(object sender, EventArgs e)
        {
            Server.oldHelp = chkHelp.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600162C RID: 5676 RVA: 0x0007B4CC File Offset: 0x000796CC
        private void chkDeath_CheckedChanged(object sender, EventArgs e)
        {
            Server.deathcount = chkDeath.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600162D RID: 5677 RVA: 0x0007B4E8 File Offset: 0x000796E8
        private void chkPhysicsRest_CheckedChanged_1(object sender, EventArgs e)
        {
            Server.physicsRestart = chkPhysicsRest.Checked;
            ActiveControl = null;
        }

        // Token: 0x0600162E RID: 5678 RVA: 0x0007B504 File Offset: 0x00079704
        private void tabPage8_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x0600162F RID: 5679 RVA: 0x0007B508 File Offset: 0x00079708
        public void UpdateLavaProperties()
        {
            lavaPropertyGrid.SelectedObject = LavaSettings.All;
            txtTime1.Text = LavaSystem.stime.ToString();
            txtTime2.Text = LavaSystem.stime2.ToString();
            heavenMapName.Text = Server.heavenMapName;
            useHeaven.Checked = Server.useHeaven;
        }

        // Token: 0x06001630 RID: 5680 RVA: 0x0007B570 File Offset: 0x00079770
        private void updateTime_Click(object sender, EventArgs e)
        {
            var flag = true;
            if (!(txtTime1.Text != ""))
                if (!(txtTime1.Text.Trim(' ') != ""))
                    goto IL_73;
            try
            {
                LavaSystem.stime = int.Parse(txtTime1.Text);
                LavaSystem.time = int.Parse(txtTime1.Text);
            }
            catch
            {
                flag = false;
            }

            IL_73:
            if (!(txtTime2.Text != ""))
                if (!(txtTime2.Text.Trim(' ') != ""))
                    goto IL_E4;
            try
            {
                LavaSystem.stime2 = int.Parse(txtTime2.Text);
                LavaSystem.time2 = int.Parse(txtTime2.Text);
            }
            catch
            {
                flag = false;
            }

            IL_E4:
            if (flag)
            {
                Server.s.Log("Time settings were updated succesfully.");
                return;
            }

            Server.s.Log("Error: Given data is not a number.");
        }

        // Token: 0x06001631 RID: 5681 RVA: 0x0007B6A4 File Offset: 0x000798A4
        private void useHeaven_CheckedChanged(object sender, EventArgs e)
        {
            if (useHeaven.Checked)
            {
                Server.useHeaven = true;
                return;
            }

            Server.useHeaven = false;
        }

        // Token: 0x06001632 RID: 5682 RVA: 0x0007B6C0 File Offset: 0x000798C0
        private void setHeavenMap_Click(object sender, EventArgs e)
        {
            if (Level.ExactFind(heavenMapName.Text.ToLower()) != null) return;
            var level = Level.Load(heavenMapName.Text.ToLower(), 3, true);
            if (level != null)
            {
                if (Server.heavenMap != null) Command.all.Find("unload").Use(null, Server.heavenMap.name);
                Server.heavenMap = level;
                Server.heavenMapName = Server.heavenMap.name;
                Server.AddLevel(Server.heavenMap);
                return;
            }

            heavenMapName.Text = "Map not found.";
        }

        // Token: 0x06001633 RID: 5683 RVA: 0x0007B758 File Offset: 0x00079958
        private void PropertyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            LavaSettings.All.Save();
            InfectionSettings.All.Save();
        }

        // Token: 0x06001634 RID: 5684 RVA: 0x0007B770 File Offset: 0x00079970
        private void tabPage19_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x06001635 RID: 5685 RVA: 0x0007B774 File Offset: 0x00079974
        private void InitChatFilterTab()
        {
            var all = ChatFilterSettings.All;
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

        // Token: 0x06001636 RID: 5686 RVA: 0x0007BA5C File Offset: 0x00079C5C
        private void ApplyChatFilterTab()
        {
            var all = ChatFilterSettings.All;
            all.RemoveCaps = checkBoxRemoveCaps.Checked;
            all.ShortenRepetitions = checkBoxShortenRepetitions.Checked;
            all.RemoveBadWords = checkBoxRemoveBadWords.Checked;
            all.MessagesCooldown = checkBoxMessagesCooldown.Checked;
            all.GuiShowAdvancedSettings = checkBoxChatFilterAdvanced.Checked;
            var maxCaps = 3;
            if (int.TryParse(textBoxMaxCaps.Text, out maxCaps)) all.MaxCaps = maxCaps;
            var charSpamMaxChars = 3;
            if (int.TryParse(textBoxMaxChars.Text, out charSpamMaxChars)) all.CharSpamMaxChars = charSpamMaxChars;
            var charSpamMaxIllegalGroups = 1;
            if (int.TryParse(textBoxMaxIllegalGroups.Text, out charSpamMaxIllegalGroups))
                all.CharSpamMaxIllegalGroups = charSpamMaxIllegalGroups;
            all.CharSpamSubstitution = textBoxCharSpamSubstitution.Text;
            all.CharSpamWarning = textBoxCharSpamWarning.Text;
            if (radioButtonCharSpam1.Checked)
                all.CharSpamAction = ChatFilter.CharacterSpamAction.DisplaySubstitution |
                                     ChatFilter.CharacterSpamAction.SendWarning;
            else if (radioButtonCharSpam2.Checked)
                all.CharSpamAction = ChatFilter.CharacterSpamAction.DisplaySubstitution;
            else
                all.CharSpamAction = ChatFilter.CharacterSpamAction.SendWarning;
            all.BadLanguageDetectionLevel = comboBoxDetectionLevel.SelectedIndex == 0
                ? ChatFilter.BadLanguageDetectionLevel.Normal
                : ChatFilter.BadLanguageDetectionLevel.High;
            all.BadLanguageSubstitution = textBoxBadLanguageSubstitution.Text;
            var badLanguageWarningLimit = 3;
            if (int.TryParse(textBoxBadLanguageKickLimit.Text, out badLanguageWarningLimit))
                all.BadLanguageWarningLimit = badLanguageWarningLimit;
            all.BadLanguageWarning = textBoxBadLanguageWarning.Text;
            all.BadLanguageKickMessage = textBoxBadLanguageKickMsg.Text;
            if (radioButtonBadLanguage1.Checked)
                all.BadLanguageAction = ChatFilter.BadLanguageAction.DisplaySubstitution |
                                        ChatFilter.BadLanguageAction.SendWarning;
            else if (radioButtonBadLanguage2.Checked)
                all.BadLanguageAction = ChatFilter.BadLanguageAction.DisplaySubstitution;
            else
                all.BadLanguageAction = ChatFilter.BadLanguageAction.SendWarning;
            var cooldownMaxMessages = 5;
            if (int.TryParse(textBoxMaxMessages.Text, out cooldownMaxMessages))
                all.CooldownMaxMessages = cooldownMaxMessages;
            var cooldownMaxMessagesSeconds = 10;
            if (int.TryParse(textBoxMaxMessagesSeconds.Text, out cooldownMaxMessagesSeconds))
                all.CooldownMaxMessagesSeconds = cooldownMaxMessagesSeconds;
            var cooldownMaxSameMessages = 2;
            if (int.TryParse(textBoxDuplicateMessages.Text, out cooldownMaxSameMessages))
                all.CooldownMaxSameMessages = cooldownMaxSameMessages;
            var cooldownMaxSameMessagesSeconds = 8;
            if (int.TryParse(textBoxDuplicateMessagesSeconds.Text, out cooldownMaxSameMessagesSeconds))
                all.CooldownMaxSameMessagesSeconds = cooldownMaxSameMessagesSeconds;
            all.CooldownTempMute = checkBoxTempMute.Checked;
            all.CooldownMaxWarning = textBoxMaxMessagesWarning.Text;
            all.CooldownDuplicatesWarning = textBoxDuplicateMessagesWarning.Text;
        }

        // Token: 0x06001637 RID: 5687 RVA: 0x0007BCBC File Offset: 0x00079EBC
        private void SaveChatFilterTab()
        {
            ApplyChatFilterTab();
            ChatFilterSettings.All.Save();
        }

        // Token: 0x06001638 RID: 5688 RVA: 0x0007BCD0 File Offset: 0x00079ED0
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            SetAdvancedFilterVisibility(((CheckBox) sender).Checked);
        }

        // Token: 0x06001639 RID: 5689 RVA: 0x0007BCE4 File Offset: 0x00079EE4
        private void SetAdvancedFilterVisibility(bool visible)
        {
            if (visible)
            {
                Action<TabPage> action = delegate(TabPage t)
                {
                    if (!tabControlChat.TabPages.Contains(t)) tabControlChat.TabPages.Add(t);
                };
                action(tabPageChatBadWords);
                action(tabPageChatCaps);
                action(tabPageChatCharSpam);
                action(tabPageChatSpam);
                return;
            }

            tabControlChat.TabPages.Remove(tabPageChatCaps);
            tabControlChat.TabPages.Remove(tabPageChatCharSpam);
            tabControlChat.TabPages.Remove(tabPageChatBadWords);
            tabControlChat.TabPages.Remove(tabPageChatSpam);
        }

        // Token: 0x0600163A RID: 5690 RVA: 0x0007BD94 File Offset: 0x00079F94
        private void CheckTheOther(object sender, CheckBox target)
        {
            if (target.Checked != ((CheckBox) sender).Checked) target.Checked = ((CheckBox) sender).Checked;
        }

        // Token: 0x0600163B RID: 5691 RVA: 0x0007BDBC File Offset: 0x00079FBC
        private void checkBoxRemoveCaps_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(sender, checkBoxRemoveCaps1);
            ChatFilterSettings.All.RemoveCaps = checkBox.Checked;
        }

        // Token: 0x0600163C RID: 5692 RVA: 0x0007BDF4 File Offset: 0x00079FF4
        private void checkBoxRemoveCaps1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxRemoveCaps);
            ChatFilterSettings.All.RemoveCaps = checkBox.Checked;
        }

        // Token: 0x0600163D RID: 5693 RVA: 0x0007BE2C File Offset: 0x0007A02C
        private void checkBoxShortenRepetitions_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxShortenRepetitions1);
            ChatFilterSettings.All.ShortenRepetitions = checkBox.Checked;
        }

        // Token: 0x0600163E RID: 5694 RVA: 0x0007BE64 File Offset: 0x0007A064
        private void checkBoxShortenRepetitions1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxShortenRepetitions);
            ChatFilterSettings.All.ShortenRepetitions = checkBox.Checked;
        }

        // Token: 0x0600163F RID: 5695 RVA: 0x0007BE9C File Offset: 0x0007A09C
        private void checkBoxRemoveBadWords_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxRemoveBadWords1);
            ChatFilterSettings.All.RemoveBadWords = checkBox.Checked;
        }

        // Token: 0x06001640 RID: 5696 RVA: 0x0007BED4 File Offset: 0x0007A0D4
        private void checkBoxRemoveBadWords1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxRemoveBadWords);
            ChatFilterSettings.All.RemoveBadWords = checkBox.Checked;
        }

        // Token: 0x06001641 RID: 5697 RVA: 0x0007BF0C File Offset: 0x0007A10C
        private void checkBoxMessagesCooldown_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxMessagesCooldown1);
            ChatFilterSettings.All.MessagesCooldown = checkBox.Checked;
        }

        // Token: 0x06001642 RID: 5698 RVA: 0x0007BF44 File Offset: 0x0007A144
        private void checkBoxMessagesCooldown1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveControl = null;
            var checkBox = (CheckBox) sender;
            CheckTheOther(checkBox, checkBoxMessagesCooldown);
            ChatFilterSettings.All.MessagesCooldown = checkBox.Checked;
        }

        // Token: 0x06001643 RID: 5699 RVA: 0x0007BF7C File Offset: 0x0007A17C
        private void ShowBadWordsEditor()
        {
            if (badWordsEditor == null || badWordsEditor.IsDisposed)
            {
                badWordsEditor = new BadWordsEditor();
                badWordsEditor.Show();
                return;
            }

            if (badWordsEditor.Visible) badWordsEditor.BringToFront();
        }

        // Token: 0x06001644 RID: 5700 RVA: 0x0007BFD0 File Offset: 0x0007A1D0
        private void ShowForm(Form form, Type type)
        {
            if (!typeof(Form).IsAssignableFrom(type)) throw new ArgumentException("Given type is not a type of Form.");
            if (form == null || form.IsDisposed)
            {
                form = (Form) Activator.CreateInstance(type);
                form.Show();
                return;
            }

            if (form.Visible) form.BringToFront();
        }

        // Token: 0x06001645 RID: 5701 RVA: 0x0007C028 File Offset: 0x0007A228
        private void ShowForm<T>(T form) where T : Form, new()
        {
            if (form == null || form.IsDisposed)
            {
                form = Activator.CreateInstance<T>();
                form.Show();
                return;
            }

            if (form.Visible) form.BringToFront();
        }

        // Token: 0x06001646 RID: 5702 RVA: 0x0007C080 File Offset: 0x0007A280
        private void button3_Click(object sender, EventArgs e)
        {
            ShowForm(badWordsEditor);
        }

        // Token: 0x06001647 RID: 5703 RVA: 0x0007C090 File Offset: 0x0007A290
        private void button6_Click(object sender, EventArgs e)
        {
            ShowForm(badWordsEditor);
        }

        // Token: 0x06001648 RID: 5704 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
        private void button2_Click(object sender, EventArgs e)
        {
            ShowForm(whiteListEditor);
        }

        // Token: 0x06001649 RID: 5705 RVA: 0x0007C0B0 File Offset: 0x0007A2B0
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        // Token: 0x0600164A RID: 5706 RVA: 0x0007C0C8 File Offset: 0x0007A2C8
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        // Token: 0x0600164B RID: 5707 RVA: 0x0007C0E0 File Offset: 0x0007A2E0
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        // Token: 0x0600164C RID: 5708 RVA: 0x0007C0F8 File Offset: 0x0007A2F8
        private void txtPromotionPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        // Token: 0x0600164D RID: 5709 RVA: 0x0007C110 File Offset: 0x0007A310
        private void textBoxSmallMaps_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text != "")
                try
                {
                    storedRanks[listRanks.SelectedIndex].smallMaps = int.Parse(textBox.Text);
                }
                catch
                {
                    textBox.Text = "Incorrect value.";
                }
        }

        // Token: 0x0600164E RID: 5710 RVA: 0x0007C17C File Offset: 0x0007A37C
        private void textBoxMediumMaps_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text != "")
                try
                {
                    storedRanks[listRanks.SelectedIndex].mediumMaps = int.Parse(textBox.Text);
                }
                catch
                {
                    textBox.Text = "Incorrect value.";
                }
        }

        // Token: 0x0600164F RID: 5711 RVA: 0x0007C1E8 File Offset: 0x0007A3E8
        private void textBoxBigMaps_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text != "")
                try
                {
                    storedRanks[listRanks.SelectedIndex].bigMaps = int.Parse(textBox.Text);
                }
                catch
                {
                    textBox.Text = "Incorrect value.";
                }
        }

        // Token: 0x06001650 RID: 5712 RVA: 0x0007C254 File Offset: 0x0007A454
        private void textBoxMaxMessages_TextChanged(object sender, EventArgs e)
        {
        }
    }
}