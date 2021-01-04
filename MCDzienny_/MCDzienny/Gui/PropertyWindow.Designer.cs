namespace MCDzienny.Gui
{
	// Token: 0x02000304 RID: 772
	public partial class PropertyWindow : global::System.Windows.Forms.Form
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x0007C258 File Offset: 0x0007A458
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0007C278 File Offset: 0x0007A478
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            MCDzienny.Settings.GeneralSettings generalSettings2 = new MCDzienny.Settings.GeneralSettings();
            MCDzienny.Settings.LavaSettings lavaSettings2 = new MCDzienny.Settings.LavaSettings();
            MCDzienny.Settings.InfectionSettings infectionSettings2 = new MCDzienny.Settings.InfectionSettings();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.autoFlagCheck = new System.Windows.Forms.CheckBox();
            this.label41 = new System.Windows.Forms.Label();
            this.flagBox = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.cmbOpChat = new System.Windows.Forms.ComboBox();
            this.lblOpChat = new System.Windows.Forms.Label();
            this.cmbDefaultRank = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.lblDefault = new System.Windows.Forms.Label();
            this.cmbDefaultColour = new System.Windows.Forms.ComboBox();
            this.chkRestart = new System.Windows.Forms.CheckBox();
            this.chkPublic = new System.Windows.Forms.CheckBox();
            this.chkAutoload = new System.Windows.Forms.CheckBox();
            this.chkWorld = new System.Windows.Forms.CheckBox();
            this.chkVerify = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.txtMaps = new System.Windows.Forms.TextBox();
            this.txtPlayers = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMOTD = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ChkTunnels = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkRepeatMessages = new System.Windows.Forms.CheckBox();
            this.chkForceCuboid = new System.Windows.Forms.CheckBox();
            this.txtShutdown = new System.Windows.Forms.TextBox();
            this.txtBanMessage = new System.Windows.Forms.TextBox();
            this.chkShutdown = new System.Windows.Forms.CheckBox();
            this.chkBanMessage = new System.Windows.Forms.CheckBox();
            this.chkrankSuper = new System.Windows.Forms.CheckBox();
            this.chkCheap = new System.Windows.Forms.CheckBox();
            this.chkDeath = new System.Windows.Forms.CheckBox();
            this.chk17Dollar = new System.Windows.Forms.CheckBox();
            this.chkPhysicsRest = new System.Windows.Forms.CheckBox();
            this.chkSmile = new System.Windows.Forms.CheckBox();
            this.chkHelp = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtNormRp = new System.Windows.Forms.TextBox();
            this.txtRP = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtAFKKick = new System.Windows.Forms.TextBox();
            this.txtafk = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtBackupLocation = new System.Windows.Forms.TextBox();
            this.txtMoneys = new System.Windows.Forms.TextBox();
            this.txtCheap = new System.Windows.Forms.TextBox();
            this.txtRestartTime = new System.Windows.Forms.TextBox();
            this.chkRestartTime = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label39 = new System.Windows.Forms.Label();
            this.vipAdd = new System.Windows.Forms.Button();
            this.vipRemove = new System.Windows.Forms.Button();
            this.vipEntry = new System.Windows.Forms.TextBox();
            this.vipLabel = new System.Windows.Forms.Label();
            this.vipList = new System.Windows.Forms.ListBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.serverMessageInterval = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.serverMessage = new System.Windows.Forms.TextBox();
            this.btnSetServerMessage = new System.Windows.Forms.Button();
            this.updatePanel = new System.Windows.Forms.Panel();
            this.updateLabel = new System.Windows.Forms.Label();
            this.FlowControl = new System.Windows.Forms.HScrollBar();
            this.PositionDelayUpdate = new System.Windows.Forms.Button();
            this.txtPositionDelay = new System.Windows.Forms.TextBox();
            this.PositionDelay = new System.Windows.Forms.Label();
            this.misc3 = new System.Windows.Forms.TabPage();
            this.generalProperties = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label90 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.textBoxBigMaps = new System.Windows.Forms.TextBox();
            this.textBoxMediumMaps = new System.Windows.Forms.TextBox();
            this.textBoxSmallMaps = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtPromotionPrice = new System.Windows.Forms.TextBox();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPermission = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRankName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddRank = new System.Windows.Forms.Button();
            this.listRanks = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.listCommands = new System.Windows.Forms.ListBox();
            this.textBoxCmdShortcut = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCmdHelp = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCmdRanks = new System.Windows.Forms.TextBox();
            this.txtCmdDisallow = new System.Windows.Forms.TextBox();
            this.txtCmdAllow = new System.Windows.Forms.TextBox();
            this.txtCmdLowest = new System.Windows.Forms.TextBox();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.label80 = new System.Windows.Forms.Label();
            this.textBoxCmdWarning = new System.Windows.Forms.TextBox();
            this.checkBoxCmdCooldown = new System.Windows.Forms.CheckBox();
            this.label77 = new System.Windows.Forms.Label();
            this.textBoxCmdMax = new System.Windows.Forms.TextBox();
            this.textBoxCmdMaxSeconds = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnBlHelp = new System.Windows.Forms.Button();
            this.txtBlRanks = new System.Windows.Forms.TextBox();
            this.txtBlAllow = new System.Windows.Forms.TextBox();
            this.txtBlLowest = new System.Windows.Forms.TextBox();
            this.txtBlDisallow = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.listBlocks = new System.Windows.Forms.ListBox();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.chkIRC = new System.Windows.Forms.CheckBox();
            this.cmbIRCColour = new System.Windows.Forms.ComboBox();
            this.lblIRC = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtNick = new System.Windows.Forms.TextBox();
            this.txtOpChannel = new System.Windows.Forms.TextBox();
            this.txtIRCServer = new System.Windows.Forms.TextBox();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.tabControlChat = new System.Windows.Forms.TabControl();
            this.tabPageChat1 = new System.Windows.Forms.TabPage();
            this.checkBoxChatFilterAdvanced = new System.Windows.Forms.CheckBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxRemoveCaps = new System.Windows.Forms.CheckBox();
            this.checkBoxShortenRepetitions = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoveBadWords = new System.Windows.Forms.CheckBox();
            this.checkBoxMessagesCooldown = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPageChatBadWords = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButtonBadLanguage3 = new System.Windows.Forms.RadioButton();
            this.radioButtonBadLanguage2 = new System.Windows.Forms.RadioButton();
            this.radioButtonBadLanguage1 = new System.Windows.Forms.RadioButton();
            this.label69 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.textBoxBadLanguageKickMsg = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.textBoxBadLanguageWarning = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.textBoxBadLanguageKickLimit = new System.Windows.Forms.TextBox();
            this.comboBoxDetectionLevel = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBoxRemoveBadWords1 = new System.Windows.Forms.CheckBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.textBoxBadLanguageSubstitution = new System.Windows.Forms.TextBox();
            this.tabPageChatCaps = new System.Windows.Forms.TabPage();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBoxMaxCaps = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.checkBoxRemoveCaps1 = new System.Windows.Forms.CheckBox();
            this.tabPageChatCharSpam = new System.Windows.Forms.TabPage();
            this.label66 = new System.Windows.Forms.Label();
            this.radioButtonCharSpam3 = new System.Windows.Forms.RadioButton();
            this.radioButtonCharSpam1 = new System.Windows.Forms.RadioButton();
            this.radioButtonCharSpam2 = new System.Windows.Forms.RadioButton();
            this.label65 = new System.Windows.Forms.Label();
            this.textBoxCharSpamWarning = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBoxCharSpamSubstitution = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.checkBoxShortenRepetitions1 = new System.Windows.Forms.CheckBox();
            this.label51 = new System.Windows.Forms.Label();
            this.textBoxMaxChars = new System.Windows.Forms.TextBox();
            this.textBoxMaxIllegalGroups = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.tabPageChatSpam = new System.Windows.Forms.TabPage();
            this.checkBoxTempMute = new System.Windows.Forms.CheckBox();
            this.textBoxDuplicateMessagesWarning = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.textBoxMaxMessagesWarning = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.textBoxDuplicateMessagesSeconds = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.checkBoxMessagesCooldown1 = new System.Windows.Forms.CheckBox();
            this.label59 = new System.Windows.Forms.Label();
            this.textBoxMaxMessages = new System.Windows.Forms.TextBox();
            this.textBoxMaxMessagesSeconds = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.textBoxDuplicateMessages = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.useHeaven = new System.Windows.Forms.CheckBox();
            this.label42 = new System.Windows.Forms.Label();
            this.heavenMapName = new System.Windows.Forms.TextBox();
            this.setHeavenMapButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.updateTimeSettingsButton = new System.Windows.Forms.Button();
            this.txtTime2 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtTime1 = new System.Windows.Forms.TextBox();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.lavaPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.zombiePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.updatePanel.SuspendLayout();
            this.misc3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabControlChat.SuspendLayout();
            this.tabPageChat1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageChatBadWords.SuspendLayout();
            this.tabPageChatCaps.SuspendLayout();
            this.tabPageChatCharSpam.SuspendLayout();
            this.tabPageChatSpam.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Controls.Add(this.misc3);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage12);
            this.tabControl.Controls.Add(this.tabPage14);
            this.tabControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(6, 7);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(595, 413);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.autoFlagCheck);
            this.tabPage1.Controls.Add(this.label41);
            this.tabPage1.Controls.Add(this.flagBox);
            this.tabPage1.Controls.Add(this.label40);
            this.tabPage1.Controls.Add(this.descriptionBox);
            this.tabPage1.Controls.Add(this.cmbOpChat);
            this.tabPage1.Controls.Add(this.lblOpChat);
            this.tabPage1.Controls.Add(this.cmbDefaultRank);
            this.tabPage1.Controls.Add(this.label29);
            this.tabPage1.Controls.Add(this.lblDefault);
            this.tabPage1.Controls.Add(this.cmbDefaultColour);
            this.tabPage1.Controls.Add(this.chkRestart);
            this.tabPage1.Controls.Add(this.chkPublic);
            this.tabPage1.Controls.Add(this.chkAutoload);
            this.tabPage1.Controls.Add(this.chkWorld);
            this.tabPage1.Controls.Add(this.chkVerify);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label27);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtDepth);
            this.tabPage1.Controls.Add(this.txtMain);
            this.tabPage1.Controls.Add(this.txtMaps);
            this.tabPage1.Controls.Add(this.txtPlayers);
            this.tabPage1.Controls.Add(this.txtHost);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMOTD);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.ChkTunnels);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(587, 387);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // autoFlagCheck
            // 
            this.autoFlagCheck.AutoSize = true;
            this.autoFlagCheck.Location = new System.Drawing.Point(234, 102);
            this.autoFlagCheck.Name = "autoFlagCheck";
            this.autoFlagCheck.Size = new System.Drawing.Size(147, 17);
            this.autoFlagCheck.TabIndex = 29;
            this.autoFlagCheck.Text = "Flag based on game mode";
            this.autoFlagCheck.UseVisualStyleBackColor = true;
            this.autoFlagCheck.CheckedChanged += new System.EventHandler(this.autoFlagCheck_CheckedChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(20, 102);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(29, 13);
            this.label41.TabIndex = 28;
            this.label41.Text = "Flag:";
            // 
            // flagBox
            // 
            this.flagBox.Location = new System.Drawing.Point(93, 99);
            this.flagBox.Name = "flagBox";
            this.flagBox.Size = new System.Drawing.Size(135, 21);
            this.flagBox.TabIndex = 27;
            this.toolTip.SetToolTip(this.flagBox, "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any " +
        "hacks at all and use \"-fly\" and whatnot to disallow other things.");
            this.flagBox.TextChanged += new System.EventHandler(this.flagBox_TextChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(20, 76);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(64, 13);
            this.label40.TabIndex = 26;
            this.label40.Text = "Description:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(93, 73);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(461, 21);
            this.descriptionBox.TabIndex = 25;
            this.toolTip.SetToolTip(this.descriptionBox, "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any " +
        "hacks at all and use \"-fly\" and whatnot to disallow other things.");
            // 
            // cmbOpChat
            // 
            this.cmbOpChat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpChat.FormattingEnabled = true;
            this.cmbOpChat.Location = new System.Drawing.Point(472, 263);
            this.cmbOpChat.Name = "cmbOpChat";
            this.cmbOpChat.Size = new System.Drawing.Size(81, 21);
            this.cmbOpChat.TabIndex = 23;
            this.toolTip.SetToolTip(this.cmbOpChat, "Default rank required to read op chat.");
            // 
            // lblOpChat
            // 
            this.lblOpChat.AutoSize = true;
            this.lblOpChat.Location = new System.Drawing.Point(399, 266);
            this.lblOpChat.Name = "lblOpChat";
            this.lblOpChat.Size = new System.Drawing.Size(70, 13);
            this.lblOpChat.TabIndex = 22;
            this.lblOpChat.Text = "Op Chat rank:";
            // 
            // cmbDefaultRank
            // 
            this.cmbDefaultRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultRank.FormattingEnabled = true;
            this.cmbDefaultRank.Location = new System.Drawing.Point(473, 290);
            this.cmbDefaultRank.Name = "cmbDefaultRank";
            this.cmbDefaultRank.Size = new System.Drawing.Size(81, 21);
            this.cmbDefaultRank.TabIndex = 21;
            this.toolTip.SetToolTip(this.cmbDefaultRank, "Default rank assigned to new visitors to the server.");
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(399, 293);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(68, 13);
            this.label29.TabIndex = 20;
            this.label29.Text = "Default rank:";
            // 
            // lblDefault
            // 
            this.lblDefault.Location = new System.Drawing.Point(539, 239);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(15, 15);
            this.lblDefault.TabIndex = 10;
            // 
            // cmbDefaultColour
            // 
            this.cmbDefaultColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultColour.FormattingEnabled = true;
            this.cmbDefaultColour.Location = new System.Drawing.Point(472, 236);
            this.cmbDefaultColour.Name = "cmbDefaultColour";
            this.cmbDefaultColour.Size = new System.Drawing.Size(57, 21);
            this.cmbDefaultColour.TabIndex = 9;
            this.toolTip.SetToolTip(this.cmbDefaultColour, "The colour of the default chat used in the server.\nFor example, when you are aske" +
        "d to select two corners in a cuboid.");
            this.cmbDefaultColour.SelectedIndexChanged += new System.EventHandler(this.cmbDefaultColour_SelectedIndexChanged);
            // 
            // chkRestart
            // 
            this.chkRestart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRestart.AutoSize = true;
            this.chkRestart.Location = new System.Drawing.Point(410, 348);
            this.chkRestart.Name = "chkRestart";
            this.chkRestart.Size = new System.Drawing.Size(153, 23);
            this.chkRestart.TabIndex = 4;
            this.chkRestart.Text = "Restart when an error occurs";
            this.chkRestart.UseVisualStyleBackColor = true;
            this.chkRestart.CheckedChanged += new System.EventHandler(this.chkRestart_CheckedChanged);
            // 
            // chkPublic
            // 
            this.chkPublic.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPublic.AutoSize = true;
            this.chkPublic.Location = new System.Drawing.Point(165, 145);
            this.chkPublic.Name = "chkPublic";
            this.chkPublic.Size = new System.Drawing.Size(46, 23);
            this.chkPublic.TabIndex = 4;
            this.chkPublic.Text = "Public";
            this.toolTip.SetToolTip(this.chkPublic, "Whether or not the server will appear on the server list.");
            this.chkPublic.UseVisualStyleBackColor = true;
            this.chkPublic.CheckedChanged += new System.EventHandler(this.chkPublic_CheckedChanged);
            // 
            // chkAutoload
            // 
            this.chkAutoload.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoload.AutoSize = true;
            this.chkAutoload.Location = new System.Drawing.Point(191, 202);
            this.chkAutoload.Name = "chkAutoload";
            this.chkAutoload.Size = new System.Drawing.Size(104, 23);
            this.chkAutoload.TabIndex = 4;
            this.chkAutoload.Text = "Load map on /goto";
            this.toolTip.SetToolTip(this.chkAutoload, "Load a map when a user wishes to go to it, and unload empty maps");
            this.chkAutoload.UseVisualStyleBackColor = true;
            this.chkAutoload.CheckedChanged += new System.EventHandler(this.chkAutoload_CheckedChanged);
            // 
            // chkWorld
            // 
            this.chkWorld.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkWorld.AutoSize = true;
            this.chkWorld.Location = new System.Drawing.Point(472, 203);
            this.chkWorld.Name = "chkWorld";
            this.chkWorld.Size = new System.Drawing.Size(69, 23);
            this.chkWorld.TabIndex = 4;
            this.chkWorld.Text = "World chat";
            this.toolTip.SetToolTip(this.chkWorld, "If disabled, every map has isolated chat.\nIf enabled, every map is able to commun" +
        "icate without special letters.");
            this.chkWorld.UseVisualStyleBackColor = true;
            this.chkWorld.CheckedChanged += new System.EventHandler(this.chkWorld_CheckedChanged);
            // 
            // chkVerify
            // 
            this.chkVerify.AutoSize = true;
            this.chkVerify.Location = new System.Drawing.Point(19, 278);
            this.chkVerify.Name = "chkVerify";
            this.chkVerify.Size = new System.Drawing.Size(171, 17);
            this.chkVerify.TabIndex = 4;
            this.chkVerify.Text = "Verify Names - keep it checked!";
            this.toolTip.SetToolTip(this.chkVerify, "Make sure the user is who they claim to be.");
            this.chkVerify.UseVisualStyleBackColor = true;
            this.chkVerify.CheckedChanged += new System.EventHandler(this.chkVerify_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(399, 239);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Default color:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(16, 234);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(86, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "Main map name:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(45, 207);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Max Maps:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(432, 102);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Max Players:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(375, 150);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(95, 13);
            this.label30.TabIndex = 3;
            this.label30.Text = "Default host state:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server port:";
            // 
            // txtDepth
            // 
            this.txtDepth.Location = new System.Drawing.Point(159, 333);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(41, 21);
            this.txtDepth.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtDepth, "Depth which guests can dig.\nDefault = 4");
            this.txtDepth.TextChanged += new System.EventHandler(this.txtDepth_TextChanged);
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(109, 231);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(60, 21);
            this.txtMain.TabIndex = 2;
            this.txtMain.TextChanged += new System.EventHandler(this.txtMaps_TextChanged);
            // 
            // txtMaps
            // 
            this.txtMaps.Location = new System.Drawing.Point(109, 204);
            this.txtMaps.Name = "txtMaps";
            this.txtMaps.Size = new System.Drawing.Size(60, 21);
            this.txtMaps.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtMaps, "The total number of maps which can be loaded at once.\nDefault = 5");
            this.txtMaps.TextChanged += new System.EventHandler(this.txtMaps_TextChanged);
            // 
            // txtPlayers
            // 
            this.txtPlayers.Location = new System.Drawing.Point(507, 99);
            this.txtPlayers.Name = "txtPlayers";
            this.txtPlayers.Size = new System.Drawing.Size(46, 21);
            this.txtPlayers.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtPlayers, "The total number of players which can login.\nDefault = 12");
            this.txtPlayers.TextChanged += new System.EventHandler(this.txtPlayers_TextChanged);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(476, 147);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(75, 21);
            this.txtHost.TabIndex = 2;
            this.txtHost.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(90, 145);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 21);
            this.txtPort.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtPort, "The port that the server will output on.\nDefault = 25565\n\nChanging will reset you" +
        "r ExternalURL.");
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "MOTD:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // txtMOTD
            // 
            this.txtMOTD.Location = new System.Drawing.Point(64, 47);
            this.txtMOTD.Name = "txtMOTD";
            this.txtMOTD.Size = new System.Drawing.Size(490, 21);
            this.txtMOTD.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtMOTD, "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any " +
        "hacks at all and use \"-fly\" and whatnot to disallow other things.");
            this.txtMOTD.TextChanged += new System.EventHandler(this.txtMOTD_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(64, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(490, 21);
            this.txtName.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtName, "The name of the server.\nPick something good!");
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // ChkTunnels
            // 
            this.ChkTunnels.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkTunnels.AutoSize = true;
            this.ChkTunnels.Location = new System.Drawing.Point(25, 334);
            this.ChkTunnels.Name = "ChkTunnels";
            this.ChkTunnels.Size = new System.Drawing.Size(82, 23);
            this.ChkTunnels.TabIndex = 4;
            this.ChkTunnels.Text = "Anti tunneling";
            this.toolTip.SetToolTip(this.ChkTunnels, "Should guests be limited to digging a certain depth?");
            this.ChkTunnels.UseVisualStyleBackColor = true;
            this.ChkTunnels.CheckedChanged += new System.EventHandler(this.ChkTunnels_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Depth:";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.chkRepeatMessages);
            this.tabPage4.Controls.Add(this.chkForceCuboid);
            this.tabPage4.Controls.Add(this.txtShutdown);
            this.tabPage4.Controls.Add(this.txtBanMessage);
            this.tabPage4.Controls.Add(this.chkShutdown);
            this.tabPage4.Controls.Add(this.chkBanMessage);
            this.tabPage4.Controls.Add(this.chkrankSuper);
            this.tabPage4.Controls.Add(this.chkCheap);
            this.tabPage4.Controls.Add(this.chkDeath);
            this.tabPage4.Controls.Add(this.chk17Dollar);
            this.tabPage4.Controls.Add(this.chkPhysicsRest);
            this.tabPage4.Controls.Add(this.chkSmile);
            this.tabPage4.Controls.Add(this.chkHelp);
            this.tabPage4.Controls.Add(this.label28);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.txtNormRp);
            this.tabPage4.Controls.Add(this.txtRP);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.txtAFKKick);
            this.tabPage4.Controls.Add(this.txtafk);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.txtBackup);
            this.tabPage4.Controls.Add(this.label32);
            this.tabPage4.Controls.Add(this.txtBackupLocation);
            this.tabPage4.Controls.Add(this.txtMoneys);
            this.tabPage4.Controls.Add(this.txtCheap);
            this.tabPage4.Controls.Add(this.txtRestartTime);
            this.tabPage4.Controls.Add(this.chkRestartTime);
            this.tabPage4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(587, 387);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Misc";
            // 
            // chkRepeatMessages
            // 
            this.chkRepeatMessages.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRepeatMessages.AutoSize = true;
            this.chkRepeatMessages.Location = new System.Drawing.Point(435, 93);
            this.chkRepeatMessages.Name = "chkRepeatMessages";
            this.chkRepeatMessages.Size = new System.Drawing.Size(127, 23);
            this.chkRepeatMessages.TabIndex = 29;
            this.chkRepeatMessages.Text = "Repeat message blocks";
            this.chkRepeatMessages.UseVisualStyleBackColor = true;
            this.chkRepeatMessages.CheckedChanged += new System.EventHandler(this.chkRepeatMessages_CheckedChanged);
            // 
            // chkForceCuboid
            // 
            this.chkForceCuboid.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkForceCuboid.AutoSize = true;
            this.chkForceCuboid.Location = new System.Drawing.Point(13, 261);
            this.chkForceCuboid.Name = "chkForceCuboid";
            this.chkForceCuboid.Size = new System.Drawing.Size(78, 23);
            this.chkForceCuboid.TabIndex = 29;
            this.chkForceCuboid.Text = "Force Cuboid";
            this.toolTip.SetToolTip(this.chkForceCuboid, "When true, runs an attempted cuboid despite cuboid limits, until it hits the grou" +
        "p limit for that user.");
            this.chkForceCuboid.UseVisualStyleBackColor = true;
            this.chkForceCuboid.CheckedChanged += new System.EventHandler(this.chkForceCuboid_CheckedChanged);
            // 
            // txtShutdown
            // 
            this.txtShutdown.Location = new System.Drawing.Point(176, 226);
            this.txtShutdown.MaxLength = 128;
            this.txtShutdown.Name = "txtShutdown";
            this.txtShutdown.Size = new System.Drawing.Size(387, 21);
            this.txtShutdown.TabIndex = 28;
            // 
            // txtBanMessage
            // 
            this.txtBanMessage.Location = new System.Drawing.Point(149, 199);
            this.txtBanMessage.MaxLength = 128;
            this.txtBanMessage.Name = "txtBanMessage";
            this.txtBanMessage.Size = new System.Drawing.Size(414, 21);
            this.txtBanMessage.TabIndex = 27;
            // 
            // chkShutdown
            // 
            this.chkShutdown.AutoSize = true;
            this.chkShutdown.Location = new System.Drawing.Point(12, 229);
            this.chkShutdown.Name = "chkShutdown";
            this.chkShutdown.Size = new System.Drawing.Size(158, 17);
            this.chkShutdown.TabIndex = 26;
            this.chkShutdown.Text = "Custom shutdown message:";
            this.chkShutdown.UseVisualStyleBackColor = true;
            this.chkShutdown.CheckedChanged += new System.EventHandler(this.chkShutdown_CheckedChanged);
            // 
            // chkBanMessage
            // 
            this.chkBanMessage.AutoSize = true;
            this.chkBanMessage.Location = new System.Drawing.Point(12, 202);
            this.chkBanMessage.Name = "chkBanMessage";
            this.chkBanMessage.Size = new System.Drawing.Size(129, 17);
            this.chkBanMessage.TabIndex = 25;
            this.chkBanMessage.Text = "Custom ban message:";
            this.chkBanMessage.UseVisualStyleBackColor = true;
            this.chkBanMessage.CheckedChanged += new System.EventHandler(this.chkBanMessage_CheckedChanged);
            // 
            // chkrankSuper
            // 
            this.chkrankSuper.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkrankSuper.AutoSize = true;
            this.chkrankSuper.Location = new System.Drawing.Point(368, 347);
            this.chkrankSuper.Name = "chkrankSuper";
            this.chkrankSuper.Size = new System.Drawing.Size(195, 23);
            this.chkrankSuper.TabIndex = 24;
            this.chkrankSuper.Text = "SuperOPs can appoint other SuperOPs";
            this.toolTip.SetToolTip(this.chkrankSuper, "Does what it says on the tin");
            this.chkrankSuper.UseVisualStyleBackColor = true;
            this.chkrankSuper.CheckedChanged += new System.EventHandler(this.chkrankSuper_CheckedChanged);
            // 
            // chkCheap
            // 
            this.chkCheap.AutoSize = true;
            this.chkCheap.Location = new System.Drawing.Point(12, 174);
            this.chkCheap.Name = "chkCheap";
            this.chkCheap.Size = new System.Drawing.Size(103, 17);
            this.chkCheap.TabIndex = 23;
            this.chkCheap.Text = "Cheap message:";
            this.toolTip.SetToolTip(this.chkCheap, "Is immortality cheap and unfair?");
            this.chkCheap.UseVisualStyleBackColor = true;
            this.chkCheap.CheckedChanged += new System.EventHandler(this.chkCheap_CheckedChanged);
            // 
            // chkDeath
            // 
            this.chkDeath.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDeath.AutoSize = true;
            this.chkDeath.Location = new System.Drawing.Point(13, 318);
            this.chkDeath.Name = "chkDeath";
            this.chkDeath.Size = new System.Drawing.Size(75, 23);
            this.chkDeath.TabIndex = 21;
            this.chkDeath.Text = "Death count";
            this.toolTip.SetToolTip(this.chkDeath, "\"Bob has died 10 times.\"");
            this.chkDeath.UseVisualStyleBackColor = true;
            this.chkDeath.CheckedChanged += new System.EventHandler(this.chkDeath_CheckedChanged);
            // 
            // chk17Dollar
            // 
            this.chk17Dollar.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk17Dollar.AutoSize = true;
            this.chk17Dollar.Location = new System.Drawing.Point(472, 318);
            this.chk17Dollar.Name = "chk17Dollar";
            this.chk17Dollar.Size = new System.Drawing.Size(91, 23);
            this.chk17Dollar.TabIndex = 22;
            this.chk17Dollar.Text = "$ before $name";
            this.chk17Dollar.UseVisualStyleBackColor = true;
            this.chk17Dollar.CheckedChanged += new System.EventHandler(this.chk17Dollar_CheckedChanged);
            // 
            // chkPhysicsRest
            // 
            this.chkPhysicsRest.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPhysicsRest.AutoSize = true;
            this.chkPhysicsRest.Location = new System.Drawing.Point(13, 289);
            this.chkPhysicsRest.Name = "chkPhysicsRest";
            this.chkPhysicsRest.Size = new System.Drawing.Size(89, 23);
            this.chkPhysicsRest.TabIndex = 22;
            this.chkPhysicsRest.Text = "Restart physics";
            this.toolTip.SetToolTip(this.chkPhysicsRest, "Restart physics on shutdown, clearing all physics blocks.");
            this.chkPhysicsRest.UseVisualStyleBackColor = true;
            this.chkPhysicsRest.CheckedChanged += new System.EventHandler(this.chkPhysicsRest_CheckedChanged_1);
            // 
            // chkSmile
            // 
            this.chkSmile.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSmile.AutoSize = true;
            this.chkSmile.Location = new System.Drawing.Point(481, 289);
            this.chkSmile.Name = "chkSmile";
            this.chkSmile.Size = new System.Drawing.Size(82, 23);
            this.chkSmile.TabIndex = 19;
            this.chkSmile.Text = "Parse emotes";
            this.chkSmile.UseVisualStyleBackColor = true;
            this.chkSmile.CheckedChanged += new System.EventHandler(this.chkSmile_CheckedChanged);
            // 
            // chkHelp
            // 
            this.chkHelp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkHelp.AutoSize = true;
            this.chkHelp.Location = new System.Drawing.Point(13, 347);
            this.chkHelp.Name = "chkHelp";
            this.chkHelp.Size = new System.Drawing.Size(56, 23);
            this.chkHelp.TabIndex = 20;
            this.chkHelp.Text = "Old help";
            this.toolTip.SetToolTip(this.chkHelp, "Should the old, cluttered help menu be used?");
            this.chkHelp.UseVisualStyleBackColor = true;
            this.chkHelp.CheckedChanged += new System.EventHandler(this.chkHelp_CheckedChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(451, 72);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(61, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "Normal /rp:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(464, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 15;
            this.label24.Text = "/rp limit:";
            this.toolTip.SetToolTip(this.label24, "Limit for custom physics set by /rp");
            // 
            // txtNormRp
            // 
            this.txtNormRp.Location = new System.Drawing.Point(521, 69);
            this.txtNormRp.Name = "txtNormRp";
            this.txtNormRp.Size = new System.Drawing.Size(41, 21);
            this.txtNormRp.TabIndex = 13;
            // 
            // txtRP
            // 
            this.txtRP.Location = new System.Drawing.Point(521, 43);
            this.txtRP.Name = "txtRP";
            this.txtRP.Size = new System.Drawing.Size(41, 21);
            this.txtRP.TabIndex = 14;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(403, 265);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(71, 13);
            this.label34.TabIndex = 11;
            this.label34.Text = "Money name:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(29, 93);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 11;
            this.label26.Text = "AFK Kick:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(23, 67);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "AFK timer:";
            // 
            // txtAFKKick
            // 
            this.txtAFKKick.Location = new System.Drawing.Point(83, 91);
            this.txtAFKKick.Name = "txtAFKKick";
            this.txtAFKKick.Size = new System.Drawing.Size(41, 21);
            this.txtAFKKick.TabIndex = 9;
            this.toolTip.SetToolTip(this.txtAFKKick, "Kick the user after they have been afk for this many minutes (0 = No kick)");
            // 
            // txtafk
            // 
            this.txtafk.Location = new System.Drawing.Point(83, 64);
            this.txtafk.Name = "txtafk";
            this.txtafk.Size = new System.Drawing.Size(41, 21);
            this.txtafk.TabIndex = 10;
            this.toolTip.SetToolTip(this.txtafk, "How long the server should wait before declaring someone ask afk. (0 = No timer a" +
        "t all)");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Backup time:";
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(83, 37);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.Size = new System.Drawing.Size(41, 21);
            this.txtBackup.TabIndex = 5;
            this.toolTip.SetToolTip(this.txtBackup, "How often should backups be taken, in seconds.\nDefault = 300");
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(44, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "Backup:";
            // 
            // txtBackupLocation
            // 
            this.txtBackupLocation.Location = new System.Drawing.Point(60, 12);
            this.txtBackupLocation.Name = "txtBackupLocation";
            this.txtBackupLocation.Size = new System.Drawing.Size(502, 21);
            this.txtBackupLocation.TabIndex = 2;
            // 
            // txtMoneys
            // 
            this.txtMoneys.Location = new System.Drawing.Point(480, 262);
            this.txtMoneys.Name = "txtMoneys";
            this.txtMoneys.Size = new System.Drawing.Size(82, 21);
            this.txtMoneys.TabIndex = 1;
            // 
            // txtCheap
            // 
            this.txtCheap.Location = new System.Drawing.Point(122, 172);
            this.txtCheap.Name = "txtCheap";
            this.txtCheap.Size = new System.Drawing.Size(441, 21);
            this.txtCheap.TabIndex = 1;
            // 
            // txtRestartTime
            // 
            this.txtRestartTime.Location = new System.Drawing.Point(149, 145);
            this.txtRestartTime.Name = "txtRestartTime";
            this.txtRestartTime.Size = new System.Drawing.Size(84, 21);
            this.txtRestartTime.TabIndex = 1;
            this.txtRestartTime.Text = "HH: mm: ss";
            // 
            // chkRestartTime
            // 
            this.chkRestartTime.AutoSize = true;
            this.chkRestartTime.Location = new System.Drawing.Point(12, 147);
            this.chkRestartTime.Name = "chkRestartTime";
            this.chkRestartTime.Size = new System.Drawing.Size(131, 17);
            this.chkRestartTime.TabIndex = 0;
            this.chkRestartTime.Text = "Restart server at time:";
            this.chkRestartTime.UseVisualStyleBackColor = true;
            this.chkRestartTime.CheckedChanged += new System.EventHandler(this.chkRestartTime_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.Transparent;
            this.tabPage6.Controls.Add(this.label39);
            this.tabPage6.Controls.Add(this.vipAdd);
            this.tabPage6.Controls.Add(this.vipRemove);
            this.tabPage6.Controls.Add(this.vipEntry);
            this.tabPage6.Controls.Add(this.vipLabel);
            this.tabPage6.Controls.Add(this.vipList);
            this.tabPage6.Controls.Add(this.label35);
            this.tabPage6.Controls.Add(this.label36);
            this.tabPage6.Controls.Add(this.serverMessageInterval);
            this.tabPage6.Controls.Add(this.label37);
            this.tabPage6.Controls.Add(this.label38);
            this.tabPage6.Controls.Add(this.serverMessage);
            this.tabPage6.Controls.Add(this.btnSetServerMessage);
            this.tabPage6.Controls.Add(this.updatePanel);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(587, 387);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Misc 2";
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(349, 334);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(133, 32);
            this.label39.TabIndex = 31;
            this.label39.Text = "People on VIP list can join server even if server is full.";
            // 
            // vipAdd
            // 
            this.vipAdd.Location = new System.Drawing.Point(498, 82);
            this.vipAdd.Name = "vipAdd";
            this.vipAdd.Size = new System.Drawing.Size(75, 23);
            this.vipAdd.TabIndex = 26;
            this.vipAdd.Text = "Add";
            this.vipAdd.Click += new System.EventHandler(this.vipAdd_Click);
            // 
            // vipRemove
            // 
            this.vipRemove.Location = new System.Drawing.Point(498, 119);
            this.vipRemove.Name = "vipRemove";
            this.vipRemove.Size = new System.Drawing.Size(75, 23);
            this.vipRemove.TabIndex = 29;
            this.vipRemove.Text = "Remove";
            this.vipRemove.Click += new System.EventHandler(this.vipRemove_Click);
            // 
            // vipEntry
            // 
            this.vipEntry.Location = new System.Drawing.Point(352, 82);
            this.vipEntry.Name = "vipEntry";
            this.vipEntry.Size = new System.Drawing.Size(130, 21);
            this.vipEntry.TabIndex = 30;
            // 
            // vipLabel
            // 
            this.vipLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.vipLabel.Location = new System.Drawing.Point(429, 31);
            this.vipLabel.Name = "vipLabel";
            this.vipLabel.Size = new System.Drawing.Size(72, 18);
            this.vipLabel.TabIndex = 27;
            this.vipLabel.Text = "VIP List";
            // 
            // vipList
            // 
            this.vipList.Location = new System.Drawing.Point(352, 119);
            this.vipList.Name = "vipList";
            this.vipList.Size = new System.Drawing.Size(130, 212);
            this.vipList.TabIndex = 28;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Calibri", 12F);
            this.label35.Location = new System.Drawing.Point(89, 210);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(158, 24);
            this.label35.TabIndex = 25;
            this.label35.Text = "Reappearing message";
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(175, 323);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(32, 22);
            this.label36.TabIndex = 24;
            this.label36.Text = "min.";
            // 
            // serverMessageInterval
            // 
            this.serverMessageInterval.Location = new System.Drawing.Point(93, 320);
            this.serverMessageInterval.Name = "serverMessageInterval";
            this.serverMessageInterval.Size = new System.Drawing.Size(78, 21);
            this.serverMessageInterval.TabIndex = 23;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(12, 323);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(75, 22);
            this.label37.TabIndex = 22;
            this.label37.Text = "Display every:";
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(9, 251);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(78, 22);
            this.label38.TabIndex = 21;
            this.label38.Text = "Your Message:";
            // 
            // serverMessage
            // 
            this.serverMessage.Location = new System.Drawing.Point(93, 248);
            this.serverMessage.Multiline = true;
            this.serverMessage.Name = "serverMessage";
            this.serverMessage.Size = new System.Drawing.Size(234, 55);
            this.serverMessage.TabIndex = 20;
            this.serverMessage.TextChanged += new System.EventHandler(this.serverMessage_TextChanged);
            // 
            // btnSetServerMessage
            // 
            this.btnSetServerMessage.Location = new System.Drawing.Point(236, 320);
            this.btnSetServerMessage.Name = "btnSetServerMessage";
            this.btnSetServerMessage.Size = new System.Drawing.Size(42, 23);
            this.btnSetServerMessage.TabIndex = 19;
            this.btnSetServerMessage.Text = "Set";
            this.btnSetServerMessage.Click += new System.EventHandler(this.btnSetServerMessage_Click);
            // 
            // updatePanel
            // 
            this.updatePanel.Controls.Add(this.updateLabel);
            this.updatePanel.Controls.Add(this.FlowControl);
            this.updatePanel.Controls.Add(this.PositionDelayUpdate);
            this.updatePanel.Controls.Add(this.txtPositionDelay);
            this.updatePanel.Controls.Add(this.PositionDelay);
            this.updatePanel.Location = new System.Drawing.Point(15, 15);
            this.updatePanel.Name = "updatePanel";
            this.updatePanel.Size = new System.Drawing.Size(312, 171);
            this.updatePanel.TabIndex = 6;
            // 
            // updateLabel
            // 
            this.updateLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.updateLabel.Location = new System.Drawing.Point(56, 16);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(214, 23);
            this.updateLabel.TabIndex = 0;
            this.updateLabel.Text = "Players position refreshing rate";
            // 
            // FlowControl
            // 
            this.FlowControl.Location = new System.Drawing.Point(15, 108);
            this.FlowControl.Maximum = 59;
            this.FlowControl.Minimum = 1;
            this.FlowControl.Name = "FlowControl";
            this.FlowControl.Size = new System.Drawing.Size(280, 18);
            this.FlowControl.TabIndex = 0;
            this.FlowControl.Value = 7;
            this.FlowControl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FlowControl_Scroll);
            // 
            // PositionDelayUpdate
            // 
            this.PositionDelayUpdate.Location = new System.Drawing.Point(221, 58);
            this.PositionDelayUpdate.Name = "PositionDelayUpdate";
            this.PositionDelayUpdate.Size = new System.Drawing.Size(75, 23);
            this.PositionDelayUpdate.TabIndex = 0;
            this.PositionDelayUpdate.Text = "OK";
            this.PositionDelayUpdate.Click += new System.EventHandler(this.PositionDelayUpdate_Click);
            // 
            // txtPositionDelay
            // 
            this.txtPositionDelay.Location = new System.Drawing.Point(105, 59);
            this.txtPositionDelay.Name = "txtPositionDelay";
            this.txtPositionDelay.Size = new System.Drawing.Size(100, 21);
            this.txtPositionDelay.TabIndex = 0;
            this.txtPositionDelay.Text = "100";
            // 
            // PositionDelay
            // 
            this.PositionDelay.Location = new System.Drawing.Point(17, 48);
            this.PositionDelay.Name = "PositionDelay";
            this.PositionDelay.Size = new System.Drawing.Size(84, 44);
            this.PositionDelay.TabIndex = 0;
            this.PositionDelay.Text = "Player position showing delay: (default=100)";
            // 
            // misc3
            // 
            this.misc3.BackColor = System.Drawing.Color.Transparent;
            this.misc3.Controls.Add(this.generalProperties);
            this.misc3.Location = new System.Drawing.Point(4, 22);
            this.misc3.Name = "misc3";
            this.misc3.Padding = new System.Windows.Forms.Padding(3);
            this.misc3.Size = new System.Drawing.Size(587, 387);
            this.misc3.TabIndex = 8;
            this.misc3.Text = "Misc 3";
            // 
            // generalProperties
            // 
            this.generalProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalProperties.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.generalProperties.Location = new System.Drawing.Point(3, 3);
            this.generalProperties.Name = "generalProperties";
            generalSettings2.AllowAndListOnClassiCube = true;
            generalSettings2.AvgStop = 50;
            generalSettings2.ChatFontFamily = "Calibri";
            generalSettings2.ChatFontSize = 12F;
            generalSettings2.ChatSpecialCharacters = false;
            generalSettings2.CheckPortOnStart = true;
            generalSettings2.CooldownCmdMax = 4;
            generalSettings2.CooldownCmdMaxSeconds = 8;
            generalSettings2.CooldownCmdUse = true;
            generalSettings2.CooldownCmdWarning = "%cWARNING: Slow down! You are using way too many commands per second.";
            generalSettings2.CustomConsoleName = "%cLord of the Server:%f";
            generalSettings2.CustomConsoleNameDelimiter = ":%f";
            generalSettings2.DeathCooldown = 20;
            generalSettings2.DevMessagePermission = 80;
            generalSettings2.ExperimentalMessages = true;
            generalSettings2.HomeMapDepth = 64;
            generalSettings2.HomeMapHeight = 64;
            generalSettings2.HomeMapWidth = 64;
            generalSettings2.IntelliSys = false;
            generalSettings2.KickSlug = false;
            generalSettings2.KickWomUsers = false;
            generalSettings2.MinPermissionForReview = 80;
            generalSettings2.PillarMaxHeight = -1;
            generalSettings2.PlusMarkerForClassiCubeAccount = true;
            generalSettings2.ShowServerLag = false;
            generalSettings2.Threshold1 = 60;
            generalSettings2.Threshold2 = 10;
            generalSettings2.UseChat = false;
            generalSettings2.UseCustomName = false;
            generalSettings2.VerifyNameForLocalIPs = false;
            this.generalProperties.SelectedObject = generalSettings2;
            this.generalProperties.Size = new System.Drawing.Size(581, 381);
            this.generalProperties.TabIndex = 0;
            this.generalProperties.Click += new System.EventHandler(this.generalProperties_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.label90);
            this.tabPage2.Controls.Add(this.label89);
            this.tabPage2.Controls.Add(this.label88);
            this.tabPage2.Controls.Add(this.label87);
            this.tabPage2.Controls.Add(this.label86);
            this.tabPage2.Controls.Add(this.label85);
            this.tabPage2.Controls.Add(this.textBoxBigMaps);
            this.tabPage2.Controls.Add(this.textBoxMediumMaps);
            this.tabPage2.Controls.Add(this.textBoxSmallMaps);
            this.tabPage2.Controls.Add(this.label84);
            this.tabPage2.Controls.Add(this.label83);
            this.tabPage2.Controls.Add(this.label81);
            this.tabPage2.Controls.Add(this.label82);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.txtPromotionPrice);
            this.tabPage2.Controls.Add(this.cmbColor);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.txtFileName);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtLimit);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.txtPermission);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtRankName);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnAddRank);
            this.tabPage2.Controls.Add(this.listRanks);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(587, 387);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Ranks";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Cursor = System.Windows.Forms.Cursors.Help;
            this.label90.Location = new System.Drawing.Point(205, 346);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(18, 13);
            this.label90.TabIndex = 29;
            this.label90.Text = "(?)";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label90, "The number of big maps that can be created by a player with this rank with a comm" +
        "and \'/mymap\'.");
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Cursor = System.Windows.Forms.Cursors.Help;
            this.label89.Location = new System.Drawing.Point(205, 319);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(18, 13);
            this.label89.TabIndex = 28;
            this.label89.Text = "(?)";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label89, "The number of medium maps that can be created by a player with this rank with a c" +
        "ommand \'/mymap\'.");
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Cursor = System.Windows.Forms.Cursors.Help;
            this.label88.Location = new System.Drawing.Point(205, 292);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(18, 13);
            this.label88.TabIndex = 27;
            this.label88.Text = "(?)";
            this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label88, "The number of small maps that can be created by a player with this rank with a co" +
        "mmand \'/mymap\'.");
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(88, 206);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(34, 14);
            this.label87.TabIndex = 26;
            this.label87.Text = "Shop";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(87, 18);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(36, 14);
            this.label86.TabIndex = 25;
            this.label86.Text = "Basic";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(83, 268);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(55, 14);
            this.label85.TabIndex = 24;
            this.label85.Text = "My Maps";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxBigMaps
            // 
            this.textBoxBigMaps.Location = new System.Drawing.Point(99, 343);
            this.textBoxBigMaps.Name = "textBoxBigMaps";
            this.textBoxBigMaps.Size = new System.Drawing.Size(100, 21);
            this.textBoxBigMaps.TabIndex = 23;
            this.textBoxBigMaps.TextChanged += new System.EventHandler(this.textBoxBigMaps_TextChanged);
            this.textBoxBigMaps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // textBoxMediumMaps
            // 
            this.textBoxMediumMaps.Location = new System.Drawing.Point(99, 316);
            this.textBoxMediumMaps.Name = "textBoxMediumMaps";
            this.textBoxMediumMaps.Size = new System.Drawing.Size(100, 21);
            this.textBoxMediumMaps.TabIndex = 22;
            this.textBoxMediumMaps.TextChanged += new System.EventHandler(this.textBoxMediumMaps_TextChanged);
            this.textBoxMediumMaps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBoxSmallMaps
            // 
            this.textBoxSmallMaps.Location = new System.Drawing.Point(99, 289);
            this.textBoxSmallMaps.Name = "textBoxSmallMaps";
            this.textBoxSmallMaps.Size = new System.Drawing.Size(100, 21);
            this.textBoxSmallMaps.TabIndex = 21;
            this.textBoxSmallMaps.TextChanged += new System.EventHandler(this.textBoxSmallMaps_TextChanged);
            this.textBoxSmallMaps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(39, 346);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(52, 13);
            this.label84.TabIndex = 20;
            this.label84.Text = "Big Maps:";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(15, 319);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(77, 13);
            this.label83.TabIndex = 19;
            this.label83.Text = "Medium Maps:";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(28, 292);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(64, 13);
            this.label81.TabIndex = 18;
            this.label81.Text = "Small Maps:";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Cursor = System.Windows.Forms.Cursors.Help;
            this.label82.Location = new System.Drawing.Point(205, 230);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(18, 13);
            this.label82.TabIndex = 17;
            this.label82.Text = "(?)";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label82, "This field defines the price for of the promotion that is available in \'/shop\'. I" +
        "f the value is 0 then the rank won\'t be buyable.");
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 230);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(85, 13);
            this.label33.TabIndex = 15;
            this.label33.Text = "Promotion Price:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPromotionPrice
            // 
            this.txtPromotionPrice.Location = new System.Drawing.Point(99, 227);
            this.txtPromotionPrice.Name = "txtPromotionPrice";
            this.txtPromotionPrice.Size = new System.Drawing.Size(100, 21);
            this.txtPromotionPrice.TabIndex = 14;
            this.txtPromotionPrice.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtPromotionPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPromotionPrice_KeyPress);
            // 
            // cmbColor
            // 
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(99, 121);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(100, 21);
            this.cmbColor.TabIndex = 12;
            this.cmbColor.SelectedIndexChanged += new System.EventHandler(this.cmbColor_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(58, 124);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Color:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(99, 170);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(100, 21);
            this.txtFileName.TabIndex = 4;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 173);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Filename:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(99, 95);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(100, 21);
            this.txtLimit.TabIndex = 4;
            this.txtLimit.TextChanged += new System.EventHandler(this.txtLimit_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Block limit:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPermission
            // 
            this.txtPermission.Location = new System.Drawing.Point(99, 68);
            this.txtPermission.Name = "txtPermission";
            this.txtPermission.Size = new System.Drawing.Size(100, 21);
            this.txtPermission.TabIndex = 4;
            this.txtPermission.TextChanged += new System.EventHandler(this.txtPermission_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Permission:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRankName
            // 
            this.txtRankName.Location = new System.Drawing.Point(99, 41);
            this.txtRankName.Name = "txtRankName";
            this.txtRankName.Size = new System.Drawing.Size(100, 21);
            this.txtRankName.TabIndex = 4;
            this.txtRankName.TextChanged += new System.EventHandler(this.txtRankName_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(55, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Del";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddRank
            // 
            this.btnAddRank.Location = new System.Drawing.Point(248, 6);
            this.btnAddRank.Name = "btnAddRank";
            this.btnAddRank.Size = new System.Drawing.Size(57, 23);
            this.btnAddRank.TabIndex = 1;
            this.btnAddRank.Text = "Add";
            this.btnAddRank.UseVisualStyleBackColor = true;
            this.btnAddRank.Click += new System.EventHandler(this.btnAddRank_Click);
            // 
            // listRanks
            // 
            this.listRanks.FormattingEnabled = true;
            this.listRanks.Location = new System.Drawing.Point(235, 35);
            this.listRanks.Name = "listRanks";
            this.listRanks.Size = new System.Drawing.Size(151, 329);
            this.listRanks.TabIndex = 0;
            this.listRanks.SelectedIndexChanged += new System.EventHandler(this.listRanks_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.tabControl3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(587, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Commands";
            this.toolTip.SetToolTip(this.tabPage3, "Which ranks can use which commands.");
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage15);
            this.tabControl3.Controls.Add(this.tabPage16);
            this.tabControl3.Location = new System.Drawing.Point(3, 16);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(581, 368);
            this.tabControl3.TabIndex = 28;
            // 
            // tabPage15
            // 
            this.tabPage15.BackColor = System.Drawing.Color.Transparent;
            this.tabPage15.Controls.Add(this.listCommands);
            this.tabPage15.Controls.Add(this.textBoxCmdShortcut);
            this.tabPage15.Controls.Add(this.label8);
            this.tabPage15.Controls.Add(this.label76);
            this.tabPage15.Controls.Add(this.label15);
            this.tabPage15.Controls.Add(this.btnCmdHelp);
            this.tabPage15.Controls.Add(this.label17);
            this.tabPage15.Controls.Add(this.txtCmdRanks);
            this.tabPage15.Controls.Add(this.txtCmdDisallow);
            this.tabPage15.Controls.Add(this.txtCmdAllow);
            this.tabPage15.Controls.Add(this.txtCmdLowest);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(573, 342);
            this.tabPage15.TabIndex = 0;
            this.tabPage15.Text = "Manager";
            // 
            // listCommands
            // 
            this.listCommands.FormattingEnabled = true;
            this.listCommands.Location = new System.Drawing.Point(319, 47);
            this.listCommands.Name = "listCommands";
            this.listCommands.Size = new System.Drawing.Size(151, 277);
            this.listCommands.TabIndex = 0;
            this.listCommands.SelectedIndexChanged += new System.EventHandler(this.listCommands_SelectedIndexChanged);
            // 
            // textBoxCmdShortcut
            // 
            this.textBoxCmdShortcut.Location = new System.Drawing.Point(208, 53);
            this.textBoxCmdShortcut.Name = "textBoxCmdShortcut";
            this.textBoxCmdShortcut.ReadOnly = true;
            this.textBoxCmdShortcut.Size = new System.Drawing.Size(92, 21);
            this.textBoxCmdShortcut.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(103, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Lowest rank needed:";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(158, 56);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(50, 13);
            this.label76.TabIndex = 26;
            this.label76.Text = "Shortcut:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(128, 111);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "But don\'t allow:";
            // 
            // btnCmdHelp
            // 
            this.btnCmdHelp.Location = new System.Drawing.Point(335, 18);
            this.btnCmdHelp.Name = "btnCmdHelp";
            this.btnCmdHelp.Size = new System.Drawing.Size(120, 23);
            this.btnCmdHelp.TabIndex = 25;
            this.btnCmdHelp.Text = "Help information";
            this.btnCmdHelp.UseVisualStyleBackColor = true;
            this.btnCmdHelp.Click += new System.EventHandler(this.btnCmdHelp_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(152, 138);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "And allow:";
            // 
            // txtCmdRanks
            // 
            this.txtCmdRanks.Location = new System.Drawing.Point(106, 161);
            this.txtCmdRanks.Multiline = true;
            this.txtCmdRanks.Name = "txtCmdRanks";
            this.txtCmdRanks.ReadOnly = true;
            this.txtCmdRanks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCmdRanks.Size = new System.Drawing.Size(194, 163);
            this.txtCmdRanks.TabIndex = 15;
            // 
            // txtCmdDisallow
            // 
            this.txtCmdDisallow.Location = new System.Drawing.Point(208, 107);
            this.txtCmdDisallow.Name = "txtCmdDisallow";
            this.txtCmdDisallow.Size = new System.Drawing.Size(92, 21);
            this.txtCmdDisallow.TabIndex = 14;
            this.txtCmdDisallow.LostFocus += new System.EventHandler(this.txtCmdDisallow_TextChanged);
            // 
            // txtCmdAllow
            // 
            this.txtCmdAllow.Location = new System.Drawing.Point(208, 134);
            this.txtCmdAllow.Name = "txtCmdAllow";
            this.txtCmdAllow.Size = new System.Drawing.Size(92, 21);
            this.txtCmdAllow.TabIndex = 14;
            this.txtCmdAllow.LostFocus += new System.EventHandler(this.txtCmdAllow_TextChanged);
            // 
            // txtCmdLowest
            // 
            this.txtCmdLowest.Location = new System.Drawing.Point(208, 80);
            this.txtCmdLowest.Name = "txtCmdLowest";
            this.txtCmdLowest.Size = new System.Drawing.Size(92, 21);
            this.txtCmdLowest.TabIndex = 14;
            this.txtCmdLowest.LostFocus += new System.EventHandler(this.txtCmdLowest_TextChanged);
            // 
            // tabPage16
            // 
            this.tabPage16.BackColor = System.Drawing.Color.Transparent;
            this.tabPage16.Controls.Add(this.label80);
            this.tabPage16.Controls.Add(this.textBoxCmdWarning);
            this.tabPage16.Controls.Add(this.checkBoxCmdCooldown);
            this.tabPage16.Controls.Add(this.label77);
            this.tabPage16.Controls.Add(this.textBoxCmdMax);
            this.tabPage16.Controls.Add(this.textBoxCmdMaxSeconds);
            this.tabPage16.Controls.Add(this.label78);
            this.tabPage16.Controls.Add(this.label79);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(573, 342);
            this.tabPage16.TabIndex = 1;
            this.tabPage16.Text = "Spam Prevention";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(21, 116);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(148, 13);
            this.label80.TabIndex = 31;
            this.label80.Text = "Too many commands warning:";
            // 
            // textBoxCmdWarning
            // 
            this.textBoxCmdWarning.Location = new System.Drawing.Point(176, 113);
            this.textBoxCmdWarning.Name = "textBoxCmdWarning";
            this.textBoxCmdWarning.Size = new System.Drawing.Size(360, 21);
            this.textBoxCmdWarning.TabIndex = 30;
            // 
            // checkBoxCmdCooldown
            // 
            this.checkBoxCmdCooldown.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCmdCooldown.AutoSize = true;
            this.checkBoxCmdCooldown.Location = new System.Drawing.Point(40, 48);
            this.checkBoxCmdCooldown.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxCmdCooldown.Name = "checkBoxCmdCooldown";
            this.checkBoxCmdCooldown.Size = new System.Drawing.Size(120, 23);
            this.checkBoxCmdCooldown.TabIndex = 23;
            this.checkBoxCmdCooldown.Text = "Commands Cooldown";
            this.checkBoxCmdCooldown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxCmdCooldown.UseVisualStyleBackColor = true;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(393, 53);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(49, 13);
            this.label77.TabIndex = 28;
            this.label77.Text = "seconds.";
            // 
            // textBoxCmdMax
            // 
            this.textBoxCmdMax.Location = new System.Drawing.Point(287, 50);
            this.textBoxCmdMax.Name = "textBoxCmdMax";
            this.textBoxCmdMax.Size = new System.Drawing.Size(32, 21);
            this.textBoxCmdMax.TabIndex = 24;
            // 
            // textBoxCmdMaxSeconds
            // 
            this.textBoxCmdMaxSeconds.Location = new System.Drawing.Point(355, 50);
            this.textBoxCmdMaxSeconds.Name = "textBoxCmdMaxSeconds";
            this.textBoxCmdMaxSeconds.Size = new System.Drawing.Size(32, 21);
            this.textBoxCmdMaxSeconds.TabIndex = 27;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(198, 53);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(84, 13);
            this.label78.TabIndex = 25;
            this.label78.Text = "Max commands:";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(327, 53);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(23, 13);
            this.label79.TabIndex = 26;
            this.label79.Text = "per";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Transparent;
            this.tabPage5.Controls.Add(this.btnBlHelp);
            this.tabPage5.Controls.Add(this.txtBlRanks);
            this.tabPage5.Controls.Add(this.txtBlAllow);
            this.tabPage5.Controls.Add(this.txtBlLowest);
            this.tabPage5.Controls.Add(this.txtBlDisallow);
            this.tabPage5.Controls.Add(this.label18);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.listBlocks);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(587, 387);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Blocks";
            // 
            // btnBlHelp
            // 
            this.btnBlHelp.Location = new System.Drawing.Point(342, 14);
            this.btnBlHelp.Name = "btnBlHelp";
            this.btnBlHelp.Size = new System.Drawing.Size(120, 23);
            this.btnBlHelp.TabIndex = 23;
            this.btnBlHelp.Text = "Help information";
            this.btnBlHelp.UseVisualStyleBackColor = true;
            this.btnBlHelp.Click += new System.EventHandler(this.btnBlHelp_Click);
            // 
            // txtBlRanks
            // 
            this.txtBlRanks.Location = new System.Drawing.Point(113, 130);
            this.txtBlRanks.Multiline = true;
            this.txtBlRanks.Name = "txtBlRanks";
            this.txtBlRanks.ReadOnly = true;
            this.txtBlRanks.Size = new System.Drawing.Size(194, 243);
            this.txtBlRanks.TabIndex = 22;
            // 
            // txtBlAllow
            // 
            this.txtBlAllow.Location = new System.Drawing.Point(215, 103);
            this.txtBlAllow.Name = "txtBlAllow";
            this.txtBlAllow.Size = new System.Drawing.Size(92, 21);
            this.txtBlAllow.TabIndex = 20;
            this.txtBlAllow.LostFocus += new System.EventHandler(this.txtBlAllow_TextChanged);
            // 
            // txtBlLowest
            // 
            this.txtBlLowest.Location = new System.Drawing.Point(215, 49);
            this.txtBlLowest.Name = "txtBlLowest";
            this.txtBlLowest.Size = new System.Drawing.Size(92, 21);
            this.txtBlLowest.TabIndex = 21;
            this.txtBlLowest.TextChanged += new System.EventHandler(this.txtBlLowest_TextChanged_1);
            this.txtBlLowest.LostFocus += new System.EventHandler(this.txtBlLowest_TextChanged);
            // 
            // txtBlDisallow
            // 
            this.txtBlDisallow.Location = new System.Drawing.Point(215, 76);
            this.txtBlDisallow.Name = "txtBlDisallow";
            this.txtBlDisallow.Size = new System.Drawing.Size(92, 21);
            this.txtBlDisallow.TabIndex = 21;
            this.txtBlDisallow.LostFocus += new System.EventHandler(this.txtBlDisallow_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(159, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "And allow:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(135, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "But don\'t allow:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(110, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Lowest rank needed:";
            // 
            // listBlocks
            // 
            this.listBlocks.FormattingEnabled = true;
            this.listBlocks.Location = new System.Drawing.Point(326, 43);
            this.listBlocks.Name = "listBlocks";
            this.listBlocks.Size = new System.Drawing.Size(151, 329);
            this.listBlocks.Sorted = true;
            this.listBlocks.TabIndex = 15;
            this.listBlocks.SelectedIndexChanged += new System.EventHandler(this.listBlocks_SelectedIndexChanged);
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.txtChannel);
            this.tabPage12.Controls.Add(this.label23);
            this.tabPage12.Controls.Add(this.chkIRC);
            this.tabPage12.Controls.Add(this.cmbIRCColour);
            this.tabPage12.Controls.Add(this.lblIRC);
            this.tabPage12.Controls.Add(this.label31);
            this.tabPage12.Controls.Add(this.txtNick);
            this.tabPage12.Controls.Add(this.txtOpChannel);
            this.tabPage12.Controls.Add(this.txtIRCServer);
            this.tabPage12.Controls.Add(this.chkUpdates);
            this.tabPage12.Controls.Add(this.label5);
            this.tabPage12.Controls.Add(this.label4);
            this.tabPage12.Controls.Add(this.label6);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(587, 387);
            this.tabPage12.TabIndex = 10;
            this.tabPage12.Text = "IRC";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(213, 88);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(107, 21);
            this.txtChannel.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtChannel, "The IRC channel to be used.");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(156, 168);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "IRC color:";
            // 
            // chkIRC
            // 
            this.chkIRC.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIRC.AutoSize = true;
            this.chkIRC.Location = new System.Drawing.Point(106, 59);
            this.chkIRC.Name = "chkIRC";
            this.chkIRC.Size = new System.Drawing.Size(52, 23);
            this.chkIRC.TabIndex = 4;
            this.chkIRC.Text = "Use IRC";
            this.toolTip.SetToolTip(this.chkIRC, "Whether to use the IRC bot or not.\nIRC stands for Internet Relay Chat and allows " +
        "for communication with the server while outside Minecraft.");
            this.chkIRC.UseVisualStyleBackColor = true;
            this.chkIRC.CheckedChanged += new System.EventHandler(this.chkIRC_CheckedChanged);
            // 
            // cmbIRCColour
            // 
            this.cmbIRCColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIRCColour.FormattingEnabled = true;
            this.cmbIRCColour.Location = new System.Drawing.Point(213, 165);
            this.cmbIRCColour.Name = "cmbIRCColour";
            this.cmbIRCColour.Size = new System.Drawing.Size(57, 21);
            this.cmbIRCColour.TabIndex = 9;
            this.toolTip.SetToolTip(this.cmbIRCColour, "The colour of the IRC nicks used in the IRC.");
            this.cmbIRCColour.SelectedIndexChanged += new System.EventHandler(this.cmbIRCColour_SelectedIndexChanged);
            // 
            // lblIRC
            // 
            this.lblIRC.Location = new System.Drawing.Point(280, 169);
            this.lblIRC.Name = "lblIRC";
            this.lblIRC.Size = new System.Drawing.Size(15, 15);
            this.lblIRC.TabIndex = 10;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(143, 118);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(64, 13);
            this.label31.TabIndex = 13;
            this.label31.Text = "Op Channel:";
            // 
            // txtNick
            // 
            this.txtNick.Location = new System.Drawing.Point(380, 88);
            this.txtNick.Name = "txtNick";
            this.txtNick.Size = new System.Drawing.Size(94, 21);
            this.txtNick.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtNick, "The Nick that the IRC bot will try and use.");
            // 
            // txtOpChannel
            // 
            this.txtOpChannel.Location = new System.Drawing.Point(213, 115);
            this.txtOpChannel.Name = "txtOpChannel";
            this.txtOpChannel.Size = new System.Drawing.Size(107, 21);
            this.txtOpChannel.TabIndex = 14;
            this.toolTip.SetToolTip(this.txtOpChannel, "The IRC channel to be used.");
            // 
            // txtIRCServer
            // 
            this.txtIRCServer.Location = new System.Drawing.Point(213, 61);
            this.txtIRCServer.Name = "txtIRCServer";
            this.txtIRCServer.Size = new System.Drawing.Size(261, 21);
            this.txtIRCServer.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtIRCServer, "The IRC server to be used.\nDefault = irc.esper.net\nBetter choice = irc.foonetic.n" +
        "et");
            // 
            // chkUpdates
            // 
            this.chkUpdates.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.Location = new System.Drawing.Point(379, 118);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(101, 23);
            this.chkUpdates.TabIndex = 4;
            this.chkUpdates.Text = "Check for updates";
            this.chkUpdates.UseVisualStyleBackColor = true;
            this.chkUpdates.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Channel:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Nick:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Server:";
            // 
            // tabPage14
            // 
            this.tabPage14.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage14.Controls.Add(this.tabControlChat);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(587, 387);
            this.tabPage14.TabIndex = 11;
            this.tabPage14.Text = "Chat Filter";
            // 
            // tabControlChat
            // 
            this.tabControlChat.Controls.Add(this.tabPageChat1);
            this.tabControlChat.Controls.Add(this.tabPageChatBadWords);
            this.tabControlChat.Controls.Add(this.tabPageChatCaps);
            this.tabControlChat.Controls.Add(this.tabPageChatCharSpam);
            this.tabControlChat.Controls.Add(this.tabPageChatSpam);
            this.tabControlChat.Location = new System.Drawing.Point(7, 17);
            this.tabControlChat.Multiline = true;
            this.tabControlChat.Name = "tabControlChat";
            this.tabControlChat.SelectedIndex = 0;
            this.tabControlChat.Size = new System.Drawing.Size(574, 364);
            this.tabControlChat.TabIndex = 13;
            // 
            // tabPageChat1
            // 
            this.tabPageChat1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageChat1.Controls.Add(this.checkBoxChatFilterAdvanced);
            this.tabPageChat1.Controls.Add(this.label75);
            this.tabPageChat1.Controls.Add(this.label74);
            this.tabPageChat1.Controls.Add(this.label73);
            this.tabPageChat1.Controls.Add(this.label72);
            this.tabPageChat1.Controls.Add(this.groupBox3);
            this.tabPageChat1.Controls.Add(this.button3);
            this.tabPageChat1.Location = new System.Drawing.Point(4, 22);
            this.tabPageChat1.Name = "tabPageChat1";
            this.tabPageChat1.Size = new System.Drawing.Size(566, 338);
            this.tabPageChat1.TabIndex = 4;
            this.tabPageChat1.Text = "Basic Settings";
            this.tabPageChat1.Click += new System.EventHandler(this.tabPage19_Click);
            // 
            // checkBoxChatFilterAdvanced
            // 
            this.checkBoxChatFilterAdvanced.AutoSize = true;
            this.checkBoxChatFilterAdvanced.Location = new System.Drawing.Point(32, 238);
            this.checkBoxChatFilterAdvanced.Name = "checkBoxChatFilterAdvanced";
            this.checkBoxChatFilterAdvanced.Size = new System.Drawing.Size(137, 17);
            this.checkBoxChatFilterAdvanced.TabIndex = 23;
            this.checkBoxChatFilterAdvanced.Text = "Show advanced settings";
            this.checkBoxChatFilterAdvanced.UseVisualStyleBackColor = true;
            this.checkBoxChatFilterAdvanced.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(203, 172);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(135, 13);
            this.label75.TabIndex = 22;
            this.label75.Text = "- message spam prevention";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(203, 54);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(100, 13);
            this.label74.TabIndex = 21;
            this.label74.Text = "- bad language filter";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(203, 132);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(111, 13);
            this.label73.TabIndex = 20;
            this.label73.Text = "- character spam filter";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(203, 93);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(87, 13);
            this.label72.TabIndex = 19;
            this.label72.Text = "- caps prevention";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.checkBoxRemoveCaps);
            this.groupBox3.Controls.Add(this.checkBoxShortenRepetitions);
            this.groupBox3.Controls.Add(this.checkBoxRemoveBadWords);
            this.groupBox3.Controls.Add(this.checkBoxMessagesCooldown);
            this.groupBox3.Location = new System.Drawing.Point(17, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 179);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Main switches";
            // 
            // checkBoxRemoveCaps
            // 
            this.checkBoxRemoveCaps.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRemoveCaps.AutoSize = true;
            this.checkBoxRemoveCaps.Location = new System.Drawing.Point(15, 67);
            this.checkBoxRemoveCaps.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxRemoveCaps.Name = "checkBoxRemoveCaps";
            this.checkBoxRemoveCaps.Size = new System.Drawing.Size(120, 23);
            this.checkBoxRemoveCaps.TabIndex = 2;
            this.checkBoxRemoveCaps.Text = "Remove Caps";
            this.checkBoxRemoveCaps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRemoveCaps.UseVisualStyleBackColor = true;
            this.checkBoxRemoveCaps.CheckedChanged += new System.EventHandler(this.checkBoxRemoveCaps_CheckedChanged);
            // 
            // checkBoxShortenRepetitions
            // 
            this.checkBoxShortenRepetitions.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxShortenRepetitions.AutoSize = true;
            this.checkBoxShortenRepetitions.Location = new System.Drawing.Point(15, 106);
            this.checkBoxShortenRepetitions.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxShortenRepetitions.Name = "checkBoxShortenRepetitions";
            this.checkBoxShortenRepetitions.Size = new System.Drawing.Size(120, 23);
            this.checkBoxShortenRepetitions.TabIndex = 3;
            this.checkBoxShortenRepetitions.Text = "Shorten Repetitions";
            this.checkBoxShortenRepetitions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxShortenRepetitions.UseVisualStyleBackColor = true;
            this.checkBoxShortenRepetitions.CheckedChanged += new System.EventHandler(this.checkBoxShortenRepetitions_CheckedChanged);
            // 
            // checkBoxRemoveBadWords
            // 
            this.checkBoxRemoveBadWords.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRemoveBadWords.AutoSize = true;
            this.checkBoxRemoveBadWords.Location = new System.Drawing.Point(15, 28);
            this.checkBoxRemoveBadWords.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxRemoveBadWords.Name = "checkBoxRemoveBadWords";
            this.checkBoxRemoveBadWords.Size = new System.Drawing.Size(120, 23);
            this.checkBoxRemoveBadWords.TabIndex = 4;
            this.checkBoxRemoveBadWords.Text = "Remove Bad Words";
            this.checkBoxRemoveBadWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRemoveBadWords.UseVisualStyleBackColor = true;
            this.checkBoxRemoveBadWords.CheckedChanged += new System.EventHandler(this.checkBoxRemoveBadWords_CheckedChanged);
            // 
            // checkBoxMessagesCooldown
            // 
            this.checkBoxMessagesCooldown.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMessagesCooldown.AutoSize = true;
            this.checkBoxMessagesCooldown.Location = new System.Drawing.Point(15, 146);
            this.checkBoxMessagesCooldown.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxMessagesCooldown.Name = "checkBoxMessagesCooldown";
            this.checkBoxMessagesCooldown.Size = new System.Drawing.Size(120, 23);
            this.checkBoxMessagesCooldown.TabIndex = 5;
            this.checkBoxMessagesCooldown.Text = "Messages Cooldown";
            this.checkBoxMessagesCooldown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxMessagesCooldown.UseVisualStyleBackColor = true;
            this.checkBoxMessagesCooldown.CheckedChanged += new System.EventHandler(this.checkBoxMessagesCooldown_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(341, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Edit Bad Words";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPageChatBadWords
            // 
            this.tabPageChatBadWords.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageChatBadWords.Controls.Add(this.button2);
            this.tabPageChatBadWords.Controls.Add(this.radioButtonBadLanguage3);
            this.tabPageChatBadWords.Controls.Add(this.radioButtonBadLanguage2);
            this.tabPageChatBadWords.Controls.Add(this.radioButtonBadLanguage1);
            this.tabPageChatBadWords.Controls.Add(this.label69);
            this.tabPageChatBadWords.Controls.Add(this.label68);
            this.tabPageChatBadWords.Controls.Add(this.textBoxBadLanguageKickMsg);
            this.tabPageChatBadWords.Controls.Add(this.label67);
            this.tabPageChatBadWords.Controls.Add(this.textBoxBadLanguageWarning);
            this.tabPageChatBadWords.Controls.Add(this.label55);
            this.tabPageChatBadWords.Controls.Add(this.textBox13);
            this.tabPageChatBadWords.Controls.Add(this.label62);
            this.tabPageChatBadWords.Controls.Add(this.textBoxBadLanguageKickLimit);
            this.tabPageChatBadWords.Controls.Add(this.comboBoxDetectionLevel);
            this.tabPageChatBadWords.Controls.Add(this.button6);
            this.tabPageChatBadWords.Controls.Add(this.checkBoxRemoveBadWords1);
            this.tabPageChatBadWords.Controls.Add(this.label53);
            this.tabPageChatBadWords.Controls.Add(this.label54);
            this.tabPageChatBadWords.Controls.Add(this.textBoxBadLanguageSubstitution);
            this.tabPageChatBadWords.Location = new System.Drawing.Point(4, 22);
            this.tabPageChatBadWords.Name = "tabPageChatBadWords";
            this.tabPageChatBadWords.Size = new System.Drawing.Size(566, 338);
            this.tabPageChatBadWords.TabIndex = 2;
            this.tabPageChatBadWords.Text = "Bad Words Filter";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(459, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "Edit White List";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButtonBadLanguage3
            // 
            this.radioButtonBadLanguage3.AutoSize = true;
            this.radioButtonBadLanguage3.Location = new System.Drawing.Point(52, 212);
            this.radioButtonBadLanguage3.Name = "radioButtonBadLanguage3";
            this.radioButtonBadLanguage3.Size = new System.Drawing.Size(254, 17);
            this.radioButtonBadLanguage3.TabIndex = 27;
            this.radioButtonBadLanguage3.TabStop = true;
            this.radioButtonBadLanguage3.Text = "Don\'t display the message and send the warning.";
            this.radioButtonBadLanguage3.UseVisualStyleBackColor = true;
            // 
            // radioButtonBadLanguage2
            // 
            this.radioButtonBadLanguage2.AutoSize = true;
            this.radioButtonBadLanguage2.Location = new System.Drawing.Point(52, 189);
            this.radioButtonBadLanguage2.Name = "radioButtonBadLanguage2";
            this.radioButtonBadLanguage2.Size = new System.Drawing.Size(138, 17);
            this.radioButtonBadLanguage2.TabIndex = 26;
            this.radioButtonBadLanguage2.TabStop = true;
            this.radioButtonBadLanguage2.Text = "Substitute the message.";
            this.radioButtonBadLanguage2.UseVisualStyleBackColor = true;
            // 
            // radioButtonBadLanguage1
            // 
            this.radioButtonBadLanguage1.AutoSize = true;
            this.radioButtonBadLanguage1.Location = new System.Drawing.Point(52, 166);
            this.radioButtonBadLanguage1.Name = "radioButtonBadLanguage1";
            this.radioButtonBadLanguage1.Size = new System.Drawing.Size(241, 17);
            this.radioButtonBadLanguage1.TabIndex = 25;
            this.radioButtonBadLanguage1.TabStop = true;
            this.radioButtonBadLanguage1.Text = "Substitute the message and send the warning.";
            this.radioButtonBadLanguage1.UseVisualStyleBackColor = true;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(30, 149);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(117, 13);
            this.label69.TabIndex = 24;
            this.label69.Text = "If bad word is detected:";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(72, 85);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(50, 13);
            this.label68.TabIndex = 23;
            this.label68.Text = "Warning:";
            // 
            // textBoxBadLanguageKickMsg
            // 
            this.textBoxBadLanguageKickMsg.Location = new System.Drawing.Point(128, 110);
            this.textBoxBadLanguageKickMsg.Name = "textBoxBadLanguageKickMsg";
            this.textBoxBadLanguageKickMsg.Size = new System.Drawing.Size(421, 21);
            this.textBoxBadLanguageKickMsg.TabIndex = 22;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(49, 110);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(73, 13);
            this.label67.TabIndex = 21;
            this.label67.Text = "Kick message:";
            // 
            // textBoxBadLanguageWarning
            // 
            this.textBoxBadLanguageWarning.Location = new System.Drawing.Point(128, 82);
            this.textBoxBadLanguageWarning.Name = "textBoxBadLanguageWarning";
            this.textBoxBadLanguageWarning.Size = new System.Drawing.Size(421, 21);
            this.textBoxBadLanguageWarning.TabIndex = 20;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(30, 235);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(29, 13);
            this.label55.TabIndex = 19;
            this.label55.Text = "Help";
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox13.Location = new System.Drawing.Point(30, 252);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(513, 65);
            this.textBox13.TabIndex = 18;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(358, 58);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(123, 13);
            this.label62.TabIndex = 17;
            this.label62.Text = "Kick if warnings exceeds:";
            // 
            // textBoxBadLanguageKickLimit
            // 
            this.textBoxBadLanguageKickLimit.Location = new System.Drawing.Point(486, 55);
            this.textBoxBadLanguageKickLimit.Name = "textBoxBadLanguageKickLimit";
            this.textBoxBadLanguageKickLimit.Size = new System.Drawing.Size(63, 21);
            this.textBoxBadLanguageKickLimit.TabIndex = 16;
            // 
            // comboBoxDetectionLevel
            // 
            this.comboBoxDetectionLevel.FormattingEnabled = true;
            this.comboBoxDetectionLevel.Items.AddRange(new object[] {
            "Normal",
            "High"});
            this.comboBoxDetectionLevel.Location = new System.Drawing.Point(267, 22);
            this.comboBoxDetectionLevel.Name = "comboBoxDetectionLevel";
            this.comboBoxDetectionLevel.Size = new System.Drawing.Size(85, 21);
            this.comboBoxDetectionLevel.TabIndex = 12;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(361, 22);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 23);
            this.button6.TabIndex = 15;
            this.button6.Text = "Edit Bad Words";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBoxRemoveBadWords1
            // 
            this.checkBoxRemoveBadWords1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRemoveBadWords1.AutoSize = true;
            this.checkBoxRemoveBadWords1.Location = new System.Drawing.Point(30, 22);
            this.checkBoxRemoveBadWords1.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxRemoveBadWords1.Name = "checkBoxRemoveBadWords1";
            this.checkBoxRemoveBadWords1.Size = new System.Drawing.Size(120, 23);
            this.checkBoxRemoveBadWords1.TabIndex = 3;
            this.checkBoxRemoveBadWords1.Text = "Remove Bad Words";
            this.checkBoxRemoveBadWords1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRemoveBadWords1.UseVisualStyleBackColor = true;
            this.checkBoxRemoveBadWords1.CheckedChanged += new System.EventHandler(this.checkBoxRemoveBadWords1_CheckedChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(193, 58);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(66, 13);
            this.label53.TabIndex = 10;
            this.label53.Text = "Substitution:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(179, 25);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(81, 13);
            this.label54.TabIndex = 13;
            this.label54.Text = "Detection level:";
            // 
            // textBoxBadLanguageSubstitution
            // 
            this.textBoxBadLanguageSubstitution.Location = new System.Drawing.Point(267, 55);
            this.textBoxBadLanguageSubstitution.Name = "textBoxBadLanguageSubstitution";
            this.textBoxBadLanguageSubstitution.Size = new System.Drawing.Size(85, 21);
            this.textBoxBadLanguageSubstitution.TabIndex = 11;
            // 
            // tabPageChatCaps
            // 
            this.tabPageChatCaps.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageChatCaps.Controls.Add(this.label50);
            this.tabPageChatCaps.Controls.Add(this.textBox6);
            this.tabPageChatCaps.Controls.Add(this.textBoxMaxCaps);
            this.tabPageChatCaps.Controls.Add(this.label43);
            this.tabPageChatCaps.Controls.Add(this.checkBoxRemoveCaps1);
            this.tabPageChatCaps.Location = new System.Drawing.Point(4, 22);
            this.tabPageChatCaps.Name = "tabPageChatCaps";
            this.tabPageChatCaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChatCaps.Size = new System.Drawing.Size(566, 338);
            this.tabPageChatCaps.TabIndex = 0;
            this.tabPageChatCaps.Text = "Caps Prevention";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(17, 247);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(29, 13);
            this.label50.TabIndex = 10;
            this.label50.Text = "Help";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox6.Location = new System.Drawing.Point(20, 263);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(513, 53);
            this.textBox6.TabIndex = 9;
            this.textBox6.Text = "Removes all uppercase characters if there\'s more of them than \"Max Caps\" value.";
            // 
            // textBoxMaxCaps
            // 
            this.textBoxMaxCaps.Location = new System.Drawing.Point(232, 24);
            this.textBoxMaxCaps.Name = "textBoxMaxCaps";
            this.textBoxMaxCaps.Size = new System.Drawing.Size(41, 21);
            this.textBoxMaxCaps.TabIndex = 6;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(172, 27);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(55, 13);
            this.label43.TabIndex = 7;
            this.label43.Text = "Max Caps:";
            // 
            // checkBoxRemoveCaps1
            // 
            this.checkBoxRemoveCaps1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRemoveCaps1.AutoSize = true;
            this.checkBoxRemoveCaps1.Location = new System.Drawing.Point(17, 22);
            this.checkBoxRemoveCaps1.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxRemoveCaps1.Name = "checkBoxRemoveCaps1";
            this.checkBoxRemoveCaps1.Size = new System.Drawing.Size(120, 23);
            this.checkBoxRemoveCaps1.TabIndex = 1;
            this.checkBoxRemoveCaps1.Text = "Remove Caps";
            this.checkBoxRemoveCaps1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRemoveCaps1.UseVisualStyleBackColor = true;
            this.checkBoxRemoveCaps1.CheckedChanged += new System.EventHandler(this.checkBoxRemoveCaps1_CheckedChanged);
            // 
            // tabPageChatCharSpam
            // 
            this.tabPageChatCharSpam.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageChatCharSpam.Controls.Add(this.label66);
            this.tabPageChatCharSpam.Controls.Add(this.radioButtonCharSpam3);
            this.tabPageChatCharSpam.Controls.Add(this.radioButtonCharSpam1);
            this.tabPageChatCharSpam.Controls.Add(this.radioButtonCharSpam2);
            this.tabPageChatCharSpam.Controls.Add(this.label65);
            this.tabPageChatCharSpam.Controls.Add(this.textBoxCharSpamWarning);
            this.tabPageChatCharSpam.Controls.Add(this.label63);
            this.tabPageChatCharSpam.Controls.Add(this.textBox12);
            this.tabPageChatCharSpam.Controls.Add(this.textBoxCharSpamSubstitution);
            this.tabPageChatCharSpam.Controls.Add(this.label52);
            this.tabPageChatCharSpam.Controls.Add(this.checkBoxShortenRepetitions1);
            this.tabPageChatCharSpam.Controls.Add(this.label51);
            this.tabPageChatCharSpam.Controls.Add(this.textBoxMaxChars);
            this.tabPageChatCharSpam.Controls.Add(this.textBoxMaxIllegalGroups);
            this.tabPageChatCharSpam.Controls.Add(this.label47);
            this.tabPageChatCharSpam.Location = new System.Drawing.Point(4, 22);
            this.tabPageChatCharSpam.Name = "tabPageChatCharSpam";
            this.tabPageChatCharSpam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChatCharSpam.Size = new System.Drawing.Size(566, 338);
            this.tabPageChatCharSpam.TabIndex = 1;
            this.tabPageChatCharSpam.Text = "Character Repetition Filter";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(16, 122);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(218, 13);
            this.label66.TabIndex = 19;
            this.label66.Text = "In case of exeeding \'Max illegal groups\' value:";
            // 
            // radioButtonCharSpam3
            // 
            this.radioButtonCharSpam3.AutoSize = true;
            this.radioButtonCharSpam3.Location = new System.Drawing.Point(44, 189);
            this.radioButtonCharSpam3.Name = "radioButtonCharSpam3";
            this.radioButtonCharSpam3.Size = new System.Drawing.Size(254, 17);
            this.radioButtonCharSpam3.TabIndex = 18;
            this.radioButtonCharSpam3.TabStop = true;
            this.radioButtonCharSpam3.Text = "Don\'t display the message and send the warning.";
            this.radioButtonCharSpam3.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharSpam1
            // 
            this.radioButtonCharSpam1.AutoSize = true;
            this.radioButtonCharSpam1.Location = new System.Drawing.Point(44, 143);
            this.radioButtonCharSpam1.Name = "radioButtonCharSpam1";
            this.radioButtonCharSpam1.Size = new System.Drawing.Size(245, 17);
            this.radioButtonCharSpam1.TabIndex = 17;
            this.radioButtonCharSpam1.TabStop = true;
            this.radioButtonCharSpam1.Text = "Display substitution text and send the warning.";
            this.radioButtonCharSpam1.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharSpam2
            // 
            this.radioButtonCharSpam2.AutoSize = true;
            this.radioButtonCharSpam2.Location = new System.Drawing.Point(44, 166);
            this.radioButtonCharSpam2.Name = "radioButtonCharSpam2";
            this.radioButtonCharSpam2.Size = new System.Drawing.Size(142, 17);
            this.radioButtonCharSpam2.TabIndex = 16;
            this.radioButtonCharSpam2.TabStop = true;
            this.radioButtonCharSpam2.Text = "Display substitution text.";
            this.radioButtonCharSpam2.UseVisualStyleBackColor = true;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(16, 81);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(50, 13);
            this.label65.TabIndex = 15;
            this.label65.Text = "Warning:";
            // 
            // textBoxCharSpamWarning
            // 
            this.textBoxCharSpamWarning.Location = new System.Drawing.Point(72, 78);
            this.textBoxCharSpamWarning.Name = "textBoxCharSpamWarning";
            this.textBoxCharSpamWarning.Size = new System.Drawing.Size(468, 21);
            this.textBoxCharSpamWarning.TabIndex = 14;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(24, 211);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(29, 13);
            this.label63.TabIndex = 13;
            this.label63.Text = "Help";
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox12.Location = new System.Drawing.Point(27, 227);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(513, 99);
            this.textBox12.TabIndex = 12;
            // 
            // textBoxCharSpamSubstitution
            // 
            this.textBoxCharSpamSubstitution.Location = new System.Drawing.Point(401, 51);
            this.textBoxCharSpamSubstitution.Name = "textBoxCharSpamSubstitution";
            this.textBoxCharSpamSubstitution.Size = new System.Drawing.Size(64, 21);
            this.textBoxCharSpamSubstitution.TabIndex = 10;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(327, 54);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(66, 13);
            this.label52.TabIndex = 9;
            this.label52.Text = "Substitution:";
            // 
            // checkBoxShortenRepetitions1
            // 
            this.checkBoxShortenRepetitions1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxShortenRepetitions1.AutoSize = true;
            this.checkBoxShortenRepetitions1.Location = new System.Drawing.Point(19, 22);
            this.checkBoxShortenRepetitions1.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxShortenRepetitions1.Name = "checkBoxShortenRepetitions1";
            this.checkBoxShortenRepetitions1.Size = new System.Drawing.Size(120, 23);
            this.checkBoxShortenRepetitions1.TabIndex = 2;
            this.checkBoxShortenRepetitions1.Text = "Shorten Repetitions";
            this.checkBoxShortenRepetitions1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxShortenRepetitions1.UseVisualStyleBackColor = true;
            this.checkBoxShortenRepetitions1.CheckedChanged += new System.EventHandler(this.checkBoxShortenRepetitions1_CheckedChanged);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(301, 27);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(95, 13);
            this.label51.TabIndex = 8;
            this.label51.Text = "Max illegal groups:";
            // 
            // textBoxMaxChars
            // 
            this.textBoxMaxChars.Location = new System.Drawing.Point(230, 24);
            this.textBoxMaxChars.Name = "textBoxMaxChars";
            this.textBoxMaxChars.Size = new System.Drawing.Size(61, 21);
            this.textBoxMaxChars.TabIndex = 3;
            // 
            // textBoxMaxIllegalGroups
            // 
            this.textBoxMaxIllegalGroups.Location = new System.Drawing.Point(401, 24);
            this.textBoxMaxIllegalGroups.Name = "textBoxMaxIllegalGroups";
            this.textBoxMaxIllegalGroups.Size = new System.Drawing.Size(64, 21);
            this.textBoxMaxIllegalGroups.TabIndex = 7;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(166, 27);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(59, 13);
            this.label47.TabIndex = 4;
            this.label47.Text = "Max Chars:";
            // 
            // tabPageChatSpam
            // 
            this.tabPageChatSpam.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageChatSpam.Controls.Add(this.checkBoxTempMute);
            this.tabPageChatSpam.Controls.Add(this.textBoxDuplicateMessagesWarning);
            this.tabPageChatSpam.Controls.Add(this.label71);
            this.tabPageChatSpam.Controls.Add(this.label70);
            this.tabPageChatSpam.Controls.Add(this.textBoxMaxMessagesWarning);
            this.tabPageChatSpam.Controls.Add(this.label64);
            this.tabPageChatSpam.Controls.Add(this.textBox14);
            this.tabPageChatSpam.Controls.Add(this.label61);
            this.tabPageChatSpam.Controls.Add(this.textBoxDuplicateMessagesSeconds);
            this.tabPageChatSpam.Controls.Add(this.label60);
            this.tabPageChatSpam.Controls.Add(this.checkBoxMessagesCooldown1);
            this.tabPageChatSpam.Controls.Add(this.label59);
            this.tabPageChatSpam.Controls.Add(this.textBoxMaxMessages);
            this.tabPageChatSpam.Controls.Add(this.textBoxMaxMessagesSeconds);
            this.tabPageChatSpam.Controls.Add(this.label56);
            this.tabPageChatSpam.Controls.Add(this.label58);
            this.tabPageChatSpam.Controls.Add(this.label57);
            this.tabPageChatSpam.Controls.Add(this.textBoxDuplicateMessages);
            this.tabPageChatSpam.Location = new System.Drawing.Point(4, 22);
            this.tabPageChatSpam.Name = "tabPageChatSpam";
            this.tabPageChatSpam.Size = new System.Drawing.Size(566, 338);
            this.tabPageChatSpam.TabIndex = 3;
            this.tabPageChatSpam.Text = "Message Spam Prevention";
            // 
            // checkBoxTempMute
            // 
            this.checkBoxTempMute.AutoSize = true;
            this.checkBoxTempMute.Location = new System.Drawing.Point(455, 24);
            this.checkBoxTempMute.Name = "checkBoxTempMute";
            this.checkBoxTempMute.Size = new System.Drawing.Size(78, 17);
            this.checkBoxTempMute.TabIndex = 32;
            this.checkBoxTempMute.Text = "Temp mute";
            this.checkBoxTempMute.UseVisualStyleBackColor = true;
            // 
            // textBoxDuplicateMessagesWarning
            // 
            this.textBoxDuplicateMessagesWarning.Location = new System.Drawing.Point(175, 129);
            this.textBoxDuplicateMessagesWarning.Name = "textBoxDuplicateMessagesWarning";
            this.textBoxDuplicateMessagesWarning.Size = new System.Drawing.Size(360, 21);
            this.textBoxDuplicateMessagesWarning.TabIndex = 31;
            this.textBoxDuplicateMessagesWarning.Visible = false;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(25, 132);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(145, 13);
            this.label71.TabIndex = 30;
            this.label71.Text = "Duplicate messages warning:";
            this.label71.Visible = false;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(25, 98);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(143, 13);
            this.label70.TabIndex = 29;
            this.label70.Text = "Too many messages warning:";
            // 
            // textBoxMaxMessagesWarning
            // 
            this.textBoxMaxMessagesWarning.Location = new System.Drawing.Point(175, 95);
            this.textBoxMaxMessagesWarning.Name = "textBoxMaxMessagesWarning";
            this.textBoxMaxMessagesWarning.Size = new System.Drawing.Size(360, 21);
            this.textBoxMaxMessagesWarning.TabIndex = 28;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(22, 205);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(29, 13);
            this.label64.TabIndex = 27;
            this.label64.Text = "Help";
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox14.Location = new System.Drawing.Point(22, 224);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(513, 65);
            this.textBox14.TabIndex = 26;
            this.textBox14.Text = "Message spam prevention checks the number of messages sent by a player within a c" +
    "ertain amount of time and it takes action if the set limit is exceeded.";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(385, 53);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(49, 13);
            this.label61.TabIndex = 25;
            this.label61.Text = "seconds.";
            this.label61.Visible = false;
            // 
            // textBoxDuplicateMessagesSeconds
            // 
            this.textBoxDuplicateMessagesSeconds.Location = new System.Drawing.Point(347, 50);
            this.textBoxDuplicateMessagesSeconds.Name = "textBoxDuplicateMessagesSeconds";
            this.textBoxDuplicateMessagesSeconds.Size = new System.Drawing.Size(32, 21);
            this.textBoxDuplicateMessagesSeconds.TabIndex = 24;
            this.textBoxDuplicateMessagesSeconds.Visible = false;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(319, 53);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(16, 13);
            this.label60.TabIndex = 23;
            this.label60.Text = "in";
            this.label60.Visible = false;
            // 
            // checkBoxMessagesCooldown1
            // 
            this.checkBoxMessagesCooldown1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMessagesCooldown1.AutoSize = true;
            this.checkBoxMessagesCooldown1.Location = new System.Drawing.Point(22, 24);
            this.checkBoxMessagesCooldown1.MinimumSize = new System.Drawing.Size(120, 0);
            this.checkBoxMessagesCooldown1.Name = "checkBoxMessagesCooldown1";
            this.checkBoxMessagesCooldown1.Size = new System.Drawing.Size(120, 23);
            this.checkBoxMessagesCooldown1.TabIndex = 4;
            this.checkBoxMessagesCooldown1.Text = "Messages Cooldown";
            this.checkBoxMessagesCooldown1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxMessagesCooldown1.UseVisualStyleBackColor = true;
            this.checkBoxMessagesCooldown1.CheckedChanged += new System.EventHandler(this.checkBoxMessagesCooldown1_CheckedChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(385, 26);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(49, 13);
            this.label59.TabIndex = 22;
            this.label59.Text = "seconds.";
            // 
            // textBoxMaxMessages
            // 
            this.textBoxMaxMessages.Location = new System.Drawing.Point(279, 23);
            this.textBoxMaxMessages.Name = "textBoxMaxMessages";
            this.textBoxMaxMessages.Size = new System.Drawing.Size(32, 21);
            this.textBoxMaxMessages.TabIndex = 16;
            this.textBoxMaxMessages.TextChanged += new System.EventHandler(this.textBoxMaxMessages_TextChanged);
            // 
            // textBoxMaxMessagesSeconds
            // 
            this.textBoxMaxMessagesSeconds.Location = new System.Drawing.Point(347, 23);
            this.textBoxMaxMessagesSeconds.Name = "textBoxMaxMessagesSeconds";
            this.textBoxMaxMessagesSeconds.Size = new System.Drawing.Size(32, 21);
            this.textBoxMaxMessagesSeconds.TabIndex = 21;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(195, 26);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(79, 13);
            this.label56.TabIndex = 17;
            this.label56.Text = "Max messages:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(319, 26);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(23, 13);
            this.label58.TabIndex = 20;
            this.label58.Text = "per";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(128, 52);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(146, 13);
            this.label57.TabIndex = 18;
            this.label57.Text = "Max same messages in a row:";
            this.label57.Visible = false;
            // 
            // textBoxDuplicateMessages
            // 
            this.textBoxDuplicateMessages.Location = new System.Drawing.Point(279, 49);
            this.textBoxDuplicateMessages.Name = "textBoxDuplicateMessages";
            this.textBoxDuplicateMessages.Size = new System.Drawing.Size(32, 21);
            this.textBoxDuplicateMessages.TabIndex = 19;
            this.textBoxDuplicateMessages.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(443, 426);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscard.Location = new System.Drawing.Point(522, 426);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(75, 23);
            this.btnDiscard.TabIndex = 1;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(362, 426);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Information";
            this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip_Popup);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(615, 489);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.tabControl);
            this.tabPage8.Controls.Add(this.btnApply);
            this.tabPage8.Controls.Add(this.btnSave);
            this.tabPage8.Controls.Add(this.btnDiscard);
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(607, 460);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "General";
            this.tabPage8.UseVisualStyleBackColor = true;
            this.tabPage8.Click += new System.EventHandler(this.tabPage8_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.tabControl2);
            this.tabPage9.Location = new System.Drawing.Point(4, 25);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(607, 460);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "Lava";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Controls.Add(this.tabPage13);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(601, 454);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.Transparent;
            this.tabPage11.Controls.Add(this.groupBox2);
            this.tabPage11.Controls.Add(this.groupBox1);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(593, 428);
            this.tabPage11.TabIndex = 0;
            this.tabPage11.Text = "Settings I";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label44);
            this.groupBox2.Controls.Add(this.useHeaven);
            this.groupBox2.Controls.Add(this.label42);
            this.groupBox2.Controls.Add(this.heavenMapName);
            this.groupBox2.Controls.Add(this.setHeavenMapButton);
            this.groupBox2.Location = new System.Drawing.Point(38, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 184);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Heaven map";
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(6, 71);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(103, 22);
            this.label44.TabIndex = 31;
            this.label44.Text = "Heaven map name:";
            // 
            // useHeaven
            // 
            this.useHeaven.Location = new System.Drawing.Point(9, 36);
            this.useHeaven.Name = "useHeaven";
            this.useHeaven.Size = new System.Drawing.Size(120, 24);
            this.useHeaven.TabIndex = 29;
            this.useHeaven.Text = "Heaven for ghosts";
            this.useHeaven.CheckedChanged += new System.EventHandler(this.useHeaven_CheckedChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(49, 103);
            this.label42.MaximumSize = new System.Drawing.Size(200, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(191, 39);
            this.label42.TabIndex = 34;
            this.label42.Text = "The heaven map has to be located in lava/maps directory and it must not be used a" +
    "s lava map.";
            // 
            // heavenMapName
            // 
            this.heavenMapName.Location = new System.Drawing.Point(115, 68);
            this.heavenMapName.Name = "heavenMapName";
            this.heavenMapName.Size = new System.Drawing.Size(97, 21);
            this.heavenMapName.TabIndex = 30;
            // 
            // setHeavenMapButton
            // 
            this.setHeavenMapButton.Location = new System.Drawing.Point(226, 66);
            this.setHeavenMapButton.Name = "setHeavenMapButton";
            this.setHeavenMapButton.Size = new System.Drawing.Size(42, 23);
            this.setHeavenMapButton.TabIndex = 32;
            this.setHeavenMapButton.Text = "Set";
            this.setHeavenMapButton.Click += new System.EventHandler(this.setHeavenMap_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.updateTimeSettingsButton);
            this.groupBox1.Controls.Add(this.txtTime2);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.txtTime1);
            this.groupBox1.Location = new System.Drawing.Point(38, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 155);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global round time";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(217, 79);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(54, 13);
            this.label45.TabIndex = 5;
            this.label45.Text = "( Phase II )";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(218, 44);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(51, 13);
            this.label46.TabIndex = 4;
            this.label46.Text = "( Phase I )";
            // 
            // updateTimeSettingsButton
            // 
            this.updateTimeSettingsButton.Location = new System.Drawing.Point(51, 120);
            this.updateTimeSettingsButton.Name = "updateTimeSettingsButton";
            this.updateTimeSettingsButton.Size = new System.Drawing.Size(132, 23);
            this.updateTimeSettingsButton.TabIndex = 0;
            this.updateTimeSettingsButton.Text = "Update Time Settings";
            this.updateTimeSettingsButton.Click += new System.EventHandler(this.updateTime_Click);
            // 
            // txtTime2
            // 
            this.txtTime2.Location = new System.Drawing.Point(97, 76);
            this.txtTime2.Name = "txtTime2";
            this.txtTime2.Size = new System.Drawing.Size(100, 21);
            this.txtTime2.TabIndex = 2;
            this.txtTime2.Text = "8";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(25, 43);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(66, 13);
            this.label48.TabIndex = 3;
            this.label48.Text = "Time before:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(25, 79);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 13);
            this.label49.TabIndex = 3;
            this.label49.Text = "Time after:";
            // 
            // txtTime1
            // 
            this.txtTime1.Location = new System.Drawing.Point(97, 41);
            this.txtTime1.Name = "txtTime1";
            this.txtTime1.Size = new System.Drawing.Size(100, 21);
            this.txtTime1.TabIndex = 3;
            this.txtTime1.Text = "14";
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.Transparent;
            this.tabPage13.Controls.Add(this.lavaPropertyGrid);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(593, 428);
            this.tabPage13.TabIndex = 1;
            this.tabPage13.Text = "Settings II";
            this.tabPage13.Visible = false;
            // 
            // lavaPropertyGrid
            // 
            this.lavaPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lavaPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.lavaPropertyGrid.Name = "lavaPropertyGrid";
            lavaSettings2.AllowCuboidOnLavaMaps = false;
            lavaSettings2.AllowGoldRockOnLavaMaps = false;
            lavaSettings2.AllowInGameVariables = true;
            lavaSettings2.AmountOfMoneyInTreasure = 5;
            lavaSettings2.Antigrief = MCDzienny.AntigriefType.BasedOnPlayersLevel;
            lavaSettings2.AutoNameColoring = true;
            lavaSettings2.AutoServerLock = false;
            lavaSettings2.ConnectionSpeedTest = false;
            lavaSettings2.DisallowBuildingNearLavaSpawn = true;
            lavaSettings2.DisallowHacksUseOnLavaMap = false;
            lavaSettings2.DisallowSpleefing = false;
            lavaSettings2.DisallowSpongesNearLavaSpawn = true;
            lavaSettings2.HacksUseOnLavaMapPermission = 80;
            lavaSettings2.HeadlessGhosts = true;
            lavaSettings2.HideDeathMessagesAmount = 20;
            lavaSettings2.IsAfkDuringVoteAllowed = true;
            lavaSettings2.LavaMapPlayerLimit = 0;
            lavaSettings2.LavaMovementDelay = 4;
            lavaSettings2.LavaState = MCDzienny.LavaState.Disturbed;
            lavaSettings2.LavaWorldChat = false;
            lavaSettings2.LivesAtStart = ((byte)(3));
            lavaSettings2.OpsBypassSpleefPrevention = false;
            lavaSettings2.OverloadProtection = false;
            lavaSettings2.PreventScoreAbuse = true;
            lavaSettings2.RandomLavaState = true;
            lavaSettings2.RequiredDistanceFromSpawn = 5;
            lavaSettings2.RequireRegistrationForPromotion = false;
            lavaSettings2.RewardAboveSeaLevel = 30;
            lavaSettings2.RewardBelowSeaLevel = 25;
            lavaSettings2.ScoreMode = MCDzienny.ScoreSystem.BasedOnAir;
            lavaSettings2.ScoreRewardFixed = 500;
            lavaSettings2.ShowDistanceOffsetMessage = true;
            lavaSettings2.ShowMapAuthor = false;
            lavaSettings2.ShowMapRating = true;
            lavaSettings2.SpawnOnDeath = true;
            lavaSettings2.StarSystem = true;
            lavaSettings2.UpperLevelOfBoplAntigrief = 8;
            lavaSettings2.VotingSystem = true;
            this.lavaPropertyGrid.SelectedObject = lavaSettings2;
            this.lavaPropertyGrid.Size = new System.Drawing.Size(587, 422);
            this.lavaPropertyGrid.TabIndex = 2;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.zombiePropertyGrid);
            this.tabPage10.Location = new System.Drawing.Point(4, 25);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(607, 460);
            this.tabPage10.TabIndex = 2;
            this.tabPage10.Text = "Zombie";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // zombiePropertyGrid
            // 
            this.zombiePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zombiePropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.zombiePropertyGrid.Name = "zombiePropertyGrid";
            infectionSettings2.BlockGlitchPrevention = true;
            infectionSettings2.BrokenNeckZombies = true;
            infectionSettings2.DisallowHacksUseOnInfectionMap = true;
            infectionSettings2.DisallowSpleefing = true;
            infectionSettings2.HacksOnInfectionMapPermission = 100;
            infectionSettings2.HumanTag = "%e[human] ";
            infectionSettings2.IsAfkDuringVoteAllowed = true;
            infectionSettings2.MapVoteDurationSeconds = 20;
            infectionSettings2.MinimumPlayers = 1;
            infectionSettings2.OpsBypassSpleefPrevention = false;
            infectionSettings2.RefreeTag = "&f[refree] ";
            infectionSettings2.RewardForHumansFixed = 30;
            infectionSettings2.RewardForHumansMultipiler = 2;
            infectionSettings2.RewardForZombiesFixed = 5;
            infectionSettings2.RewardForZombiesMultipiler = 4;
            infectionSettings2.RoundTime = 6;
            infectionSettings2.ShowMapAuthor = false;
            infectionSettings2.ShowMapRating = true;
            infectionSettings2.SpeedHackDetectionThreshold = 250;
            infectionSettings2.UsePlayerLevels = true;
            infectionSettings2.VotingSystem = true;
            infectionSettings2.ZombieAlias = "zombie";
            infectionSettings2.ZombieTag = "&c[zombie] ";
            this.zombiePropertyGrid.SelectedObject = infectionSettings2;
            this.zombiePropertyGrid.Size = new System.Drawing.Size(601, 454);
            this.zombiePropertyGrid.TabIndex = 0;
            // 
            // PropertyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 503);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyWindow";
            this.Text = "Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertyWindow_FormClosing);
            this.Load += new System.EventHandler(this.PropertyWindow_Load);
            this.Disposed += new System.EventHandler(this.PropertyWindow_Unload);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.updatePanel.ResumeLayout(false);
            this.updatePanel.PerformLayout();
            this.misc3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.tabPage16.ResumeLayout(false);
            this.tabPage16.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tabControlChat.ResumeLayout(false);
            this.tabPageChat1.ResumeLayout(false);
            this.tabPageChat1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageChatBadWords.ResumeLayout(false);
            this.tabPageChatBadWords.PerformLayout();
            this.tabPageChatCaps.ResumeLayout(false);
            this.tabPageChatCaps.PerformLayout();
            this.tabPageChatCharSpam.ResumeLayout(false);
            this.tabPageChatCharSpam.PerformLayout();
            this.tabPageChatSpam.ResumeLayout(false);
            this.tabPageChatSpam.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		// Token: 0x040009D3 RID: 2515
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040009D4 RID: 2516
		private global::System.Windows.Forms.TabControl tabControl;

		// Token: 0x040009D5 RID: 2517
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x040009D6 RID: 2518
		private global::System.Windows.Forms.TabPage tabPage3;

		// Token: 0x040009D7 RID: 2519
		private global::System.Windows.Forms.CheckBox chkPublic;

		// Token: 0x040009D8 RID: 2520
		private global::System.Windows.Forms.CheckBox chkWorld;

		// Token: 0x040009D9 RID: 2521
		private global::System.Windows.Forms.CheckBox chkVerify;

		// Token: 0x040009DA RID: 2522
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040009DB RID: 2523
		private global::System.Windows.Forms.TextBox txtPort;

		// Token: 0x040009DC RID: 2524
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040009DD RID: 2525
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040009DE RID: 2526
		private global::System.Windows.Forms.TextBox txtMOTD;

		// Token: 0x040009DF RID: 2527
		private global::System.Windows.Forms.TextBox txtName;

		// Token: 0x040009E0 RID: 2528
		private global::System.Windows.Forms.CheckBox chkIRC;

		// Token: 0x040009E1 RID: 2529
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040009E2 RID: 2530
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040009E3 RID: 2531
		private global::System.Windows.Forms.TextBox txtChannel;

		// Token: 0x040009E4 RID: 2532
		private global::System.Windows.Forms.TextBox txtNick;

		// Token: 0x040009E5 RID: 2533
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040009E6 RID: 2534
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040009E7 RID: 2535
		private global::System.Windows.Forms.TextBox txtIRCServer;

		// Token: 0x040009E8 RID: 2536
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x040009E9 RID: 2537
		private global::System.Windows.Forms.Button btnDiscard;

		// Token: 0x040009EA RID: 2538
		private global::System.Windows.Forms.Label label22;

		// Token: 0x040009EB RID: 2539
		private global::System.Windows.Forms.Label label21;

		// Token: 0x040009EC RID: 2540
		private global::System.Windows.Forms.TextBox txtMaps;

		// Token: 0x040009ED RID: 2541
		private global::System.Windows.Forms.TextBox txtPlayers;

		// Token: 0x040009EE RID: 2542
		private global::System.Windows.Forms.ComboBox cmbDefaultColour;

		// Token: 0x040009EF RID: 2543
		private global::System.Windows.Forms.Label lblDefault;

		// Token: 0x040009F0 RID: 2544
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x040009F1 RID: 2545
		private global::System.Windows.Forms.Label lblIRC;

		// Token: 0x040009F2 RID: 2546
		private global::System.Windows.Forms.ComboBox cmbIRCColour;

		// Token: 0x040009F3 RID: 2547
		private global::System.Windows.Forms.Label label23;

		// Token: 0x040009F4 RID: 2548
		private global::System.Windows.Forms.ToolTip toolTip;

		// Token: 0x040009F5 RID: 2549
		private global::System.Windows.Forms.CheckBox chkUpdates;

		// Token: 0x040009F6 RID: 2550
		private global::System.Windows.Forms.CheckBox chkAutoload;

		// Token: 0x040009F7 RID: 2551
		private global::System.Windows.Forms.Label label27;

		// Token: 0x040009F8 RID: 2552
		private global::System.Windows.Forms.TextBox txtMain;

		// Token: 0x040009F9 RID: 2553
		private global::System.Windows.Forms.TextBox txtOpChannel;

		// Token: 0x040009FA RID: 2554
		private global::System.Windows.Forms.Label label31;

		// Token: 0x040009FB RID: 2555
		private global::System.Windows.Forms.TabPage tabPage4;

		// Token: 0x040009FC RID: 2556
		private global::System.Windows.Forms.TextBox txtRestartTime;

		// Token: 0x040009FD RID: 2557
		private global::System.Windows.Forms.CheckBox chkRestartTime;

		// Token: 0x040009FE RID: 2558
		private global::System.Windows.Forms.Label label32;

		// Token: 0x040009FF RID: 2559
		private global::System.Windows.Forms.TextBox txtBackupLocation;

		// Token: 0x04000A00 RID: 2560
		private global::System.Windows.Forms.Label label28;

		// Token: 0x04000A01 RID: 2561
		private global::System.Windows.Forms.Label label24;

		// Token: 0x04000A02 RID: 2562
		private global::System.Windows.Forms.TextBox txtNormRp;

		// Token: 0x04000A03 RID: 2563
		private global::System.Windows.Forms.TextBox txtRP;

		// Token: 0x04000A04 RID: 2564
		private global::System.Windows.Forms.Label label26;

		// Token: 0x04000A05 RID: 2565
		private global::System.Windows.Forms.Label label25;

		// Token: 0x04000A06 RID: 2566
		private global::System.Windows.Forms.TextBox txtAFKKick;

		// Token: 0x04000A07 RID: 2567
		private global::System.Windows.Forms.TextBox txtafk;

		// Token: 0x04000A08 RID: 2568
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000A09 RID: 2569
		private global::System.Windows.Forms.TextBox txtBackup;

		// Token: 0x04000A0A RID: 2570
		private global::System.Windows.Forms.CheckBox chkrankSuper;

		// Token: 0x04000A0B RID: 2571
		private global::System.Windows.Forms.CheckBox chkCheap;

		// Token: 0x04000A0C RID: 2572
		private global::System.Windows.Forms.CheckBox chkDeath;

		// Token: 0x04000A0D RID: 2573
		private global::System.Windows.Forms.CheckBox chk17Dollar;

		// Token: 0x04000A0E RID: 2574
		private global::System.Windows.Forms.CheckBox chkPhysicsRest;

		// Token: 0x04000A0F RID: 2575
		private global::System.Windows.Forms.CheckBox chkSmile;

		// Token: 0x04000A10 RID: 2576
		private global::System.Windows.Forms.CheckBox chkHelp;

		// Token: 0x04000A11 RID: 2577
		private global::System.Windows.Forms.TextBox txtCheap;

		// Token: 0x04000A12 RID: 2578
		private global::System.Windows.Forms.Label label34;

		// Token: 0x04000A13 RID: 2579
		private global::System.Windows.Forms.TextBox txtMoneys;

		// Token: 0x04000A14 RID: 2580
		private global::System.Windows.Forms.TextBox txtShutdown;

		// Token: 0x04000A15 RID: 2581
		private global::System.Windows.Forms.TextBox txtBanMessage;

		// Token: 0x04000A16 RID: 2582
		private global::System.Windows.Forms.CheckBox chkShutdown;

		// Token: 0x04000A17 RID: 2583
		private global::System.Windows.Forms.CheckBox chkBanMessage;

		// Token: 0x04000A18 RID: 2584
		private global::System.Windows.Forms.ComboBox cmbDefaultRank;

		// Token: 0x04000A19 RID: 2585
		private global::System.Windows.Forms.Label label29;

		// Token: 0x04000A1A RID: 2586
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x04000A1B RID: 2587
		private global::System.Windows.Forms.ComboBox cmbColor;

		// Token: 0x04000A1C RID: 2588
		private global::System.Windows.Forms.Label label16;

		// Token: 0x04000A1D RID: 2589
		private global::System.Windows.Forms.TextBox txtFileName;

		// Token: 0x04000A1E RID: 2590
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000A1F RID: 2591
		private global::System.Windows.Forms.TextBox txtLimit;

		// Token: 0x04000A20 RID: 2592
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000A21 RID: 2593
		private global::System.Windows.Forms.TextBox txtPermission;

		// Token: 0x04000A22 RID: 2594
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000A23 RID: 2595
		private global::System.Windows.Forms.TextBox txtRankName;

		// Token: 0x04000A24 RID: 2596
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000A25 RID: 2597
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000A26 RID: 2598
		private global::System.Windows.Forms.Button btnAddRank;

		// Token: 0x04000A27 RID: 2599
		private global::System.Windows.Forms.ListBox listRanks;

		// Token: 0x04000A28 RID: 2600
		private global::System.Windows.Forms.CheckBox chkRestart;

		// Token: 0x04000A29 RID: 2601
		private global::System.Windows.Forms.TextBox txtDepth;

		// Token: 0x04000A2A RID: 2602
		private global::System.Windows.Forms.CheckBox ChkTunnels;

		// Token: 0x04000A2B RID: 2603
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000A2C RID: 2604
		private global::System.Windows.Forms.Label lblOpChat;

		// Token: 0x04000A2D RID: 2605
		private global::System.Windows.Forms.ComboBox cmbOpChat;

		// Token: 0x04000A2E RID: 2606
		private global::System.Windows.Forms.TextBox txtCmdAllow;

		// Token: 0x04000A2F RID: 2607
		private global::System.Windows.Forms.TextBox txtCmdDisallow;

		// Token: 0x04000A30 RID: 2608
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000A31 RID: 2609
		private global::System.Windows.Forms.Label label15;

		// Token: 0x04000A32 RID: 2610
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000A33 RID: 2611
		private global::System.Windows.Forms.ListBox listCommands;

		// Token: 0x04000A34 RID: 2612
		private global::System.Windows.Forms.TabPage tabPage5;

		// Token: 0x04000A35 RID: 2613
		private global::System.Windows.Forms.TextBox txtBlAllow;

		// Token: 0x04000A36 RID: 2614
		private global::System.Windows.Forms.TextBox txtBlDisallow;

		// Token: 0x04000A37 RID: 2615
		private global::System.Windows.Forms.Label label18;

		// Token: 0x04000A38 RID: 2616
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000A39 RID: 2617
		private global::System.Windows.Forms.Label label20;

		// Token: 0x04000A3A RID: 2618
		private global::System.Windows.Forms.ListBox listBlocks;

		// Token: 0x04000A3B RID: 2619
		private global::System.Windows.Forms.TextBox txtCmdLowest;

		// Token: 0x04000A3C RID: 2620
		private global::System.Windows.Forms.TextBox txtBlLowest;

		// Token: 0x04000A3D RID: 2621
		private global::System.Windows.Forms.TextBox txtCmdRanks;

		// Token: 0x04000A3E RID: 2622
		private global::System.Windows.Forms.TextBox txtBlRanks;

		// Token: 0x04000A3F RID: 2623
		private global::System.Windows.Forms.Button btnBlHelp;

		// Token: 0x04000A40 RID: 2624
		private global::System.Windows.Forms.Button btnCmdHelp;

		// Token: 0x04000A41 RID: 2625
		private global::System.Windows.Forms.CheckBox chkForceCuboid;

		// Token: 0x04000A42 RID: 2626
		private global::System.Windows.Forms.Label label30;

		// Token: 0x04000A43 RID: 2627
		private global::System.Windows.Forms.TextBox txtHost;

		// Token: 0x04000A44 RID: 2628
		private global::System.Windows.Forms.CheckBox chkRepeatMessages;

		// Token: 0x04000A45 RID: 2629
		private global::System.Windows.Forms.Label label33;

		// Token: 0x04000A46 RID: 2630
		private global::System.Windows.Forms.TextBox txtPromotionPrice;

		// Token: 0x04000A47 RID: 2631
		private global::System.Windows.Forms.TabPage tabPage6;

		// Token: 0x04000A48 RID: 2632
		private global::System.Windows.Forms.Panel updatePanel;

		// Token: 0x04000A49 RID: 2633
		private global::System.Windows.Forms.Label updateLabel;

		// Token: 0x04000A4A RID: 2634
		private global::System.Windows.Forms.HScrollBar FlowControl;

		// Token: 0x04000A4B RID: 2635
		private global::System.Windows.Forms.Button PositionDelayUpdate;

		// Token: 0x04000A4C RID: 2636
		private global::System.Windows.Forms.TextBox txtPositionDelay;

		// Token: 0x04000A4D RID: 2637
		private global::System.Windows.Forms.Label PositionDelay;

		// Token: 0x04000A4E RID: 2638
		private global::System.Windows.Forms.Label label35;

		// Token: 0x04000A4F RID: 2639
		private global::System.Windows.Forms.Label label36;

		// Token: 0x04000A50 RID: 2640
		private global::System.Windows.Forms.TextBox serverMessageInterval;

		// Token: 0x04000A51 RID: 2641
		private global::System.Windows.Forms.Label label37;

		// Token: 0x04000A52 RID: 2642
		private global::System.Windows.Forms.Label label38;

		// Token: 0x04000A53 RID: 2643
		private global::System.Windows.Forms.TextBox serverMessage;

		// Token: 0x04000A54 RID: 2644
		private global::System.Windows.Forms.Button btnSetServerMessage;

		// Token: 0x04000A55 RID: 2645
		private global::System.Windows.Forms.TabPage misc3;

		// Token: 0x04000A56 RID: 2646
		private global::System.Windows.Forms.PropertyGrid generalProperties;

		// Token: 0x04000A57 RID: 2647
		private global::System.Windows.Forms.CheckBox autoFlagCheck;

		// Token: 0x04000A58 RID: 2648
		private global::System.Windows.Forms.Label label41;

		// Token: 0x04000A59 RID: 2649
		private global::System.Windows.Forms.TextBox flagBox;

		// Token: 0x04000A5A RID: 2650
		private global::System.Windows.Forms.Label label40;

		// Token: 0x04000A5B RID: 2651
		private global::System.Windows.Forms.TextBox descriptionBox;

		// Token: 0x04000A5C RID: 2652
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x04000A5D RID: 2653
		private global::System.Windows.Forms.TabPage tabPage8;

		// Token: 0x04000A5E RID: 2654
		private global::System.Windows.Forms.TabPage tabPage9;

		// Token: 0x04000A5F RID: 2655
		private global::System.Windows.Forms.TabPage tabPage10;

		// Token: 0x04000A60 RID: 2656
		private global::System.Windows.Forms.TabPage tabPage12;

		// Token: 0x04000A61 RID: 2657
		private global::System.Windows.Forms.TabControl tabControl2;

		// Token: 0x04000A62 RID: 2658
		private global::System.Windows.Forms.TabPage tabPage11;

		// Token: 0x04000A63 RID: 2659
		private global::System.Windows.Forms.Label label42;

		// Token: 0x04000A64 RID: 2660
		private global::System.Windows.Forms.Button setHeavenMapButton;

		// Token: 0x04000A65 RID: 2661
		private global::System.Windows.Forms.Label label44;

		// Token: 0x04000A66 RID: 2662
		private global::System.Windows.Forms.TextBox heavenMapName;

		// Token: 0x04000A67 RID: 2663
		private global::System.Windows.Forms.CheckBox useHeaven;

		// Token: 0x04000A68 RID: 2664
		private global::System.Windows.Forms.Label label45;

		// Token: 0x04000A69 RID: 2665
		private global::System.Windows.Forms.Label label46;

		// Token: 0x04000A6A RID: 2666
		private global::System.Windows.Forms.Label label48;

		// Token: 0x04000A6B RID: 2667
		private global::System.Windows.Forms.TextBox txtTime1;

		// Token: 0x04000A6C RID: 2668
		private global::System.Windows.Forms.Label label49;

		// Token: 0x04000A6D RID: 2669
		private global::System.Windows.Forms.TextBox txtTime2;

		// Token: 0x04000A6E RID: 2670
		private global::System.Windows.Forms.Button updateTimeSettingsButton;

		// Token: 0x04000A6F RID: 2671
		private global::System.Windows.Forms.TabPage tabPage13;

		// Token: 0x04000A70 RID: 2672
		private global::System.Windows.Forms.PropertyGrid lavaPropertyGrid;

		// Token: 0x04000A71 RID: 2673
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000A72 RID: 2674
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000A73 RID: 2675
		private global::System.Windows.Forms.PropertyGrid zombiePropertyGrid;

		// Token: 0x04000A74 RID: 2676
		private global::System.Windows.Forms.TabPage tabPage14;

		// Token: 0x04000A75 RID: 2677
		private global::System.Windows.Forms.CheckBox checkBoxMessagesCooldown1;

		// Token: 0x04000A76 RID: 2678
		private global::System.Windows.Forms.CheckBox checkBoxRemoveBadWords1;

		// Token: 0x04000A77 RID: 2679
		private global::System.Windows.Forms.CheckBox checkBoxShortenRepetitions1;

		// Token: 0x04000A78 RID: 2680
		private global::System.Windows.Forms.CheckBox checkBoxRemoveCaps1;

		// Token: 0x04000A79 RID: 2681
		private global::System.Windows.Forms.TabControl tabControlChat;

		// Token: 0x04000A7A RID: 2682
		private global::System.Windows.Forms.TabPage tabPageChatCaps;

		// Token: 0x04000A7B RID: 2683
		private global::System.Windows.Forms.Label label50;

		// Token: 0x04000A7C RID: 2684
		private global::System.Windows.Forms.TextBox textBox6;

		// Token: 0x04000A7D RID: 2685
		private global::System.Windows.Forms.TextBox textBoxMaxCaps;

		// Token: 0x04000A7E RID: 2686
		private global::System.Windows.Forms.Label label43;

		// Token: 0x04000A7F RID: 2687
		private global::System.Windows.Forms.TabPage tabPageChatCharSpam;

		// Token: 0x04000A80 RID: 2688
		private global::System.Windows.Forms.Label label66;

		// Token: 0x04000A81 RID: 2689
		private global::System.Windows.Forms.RadioButton radioButtonCharSpam3;

		// Token: 0x04000A82 RID: 2690
		private global::System.Windows.Forms.RadioButton radioButtonCharSpam1;

		// Token: 0x04000A83 RID: 2691
		private global::System.Windows.Forms.RadioButton radioButtonCharSpam2;

		// Token: 0x04000A84 RID: 2692
		private global::System.Windows.Forms.Label label65;

		// Token: 0x04000A85 RID: 2693
		private global::System.Windows.Forms.TextBox textBoxCharSpamWarning;

		// Token: 0x04000A86 RID: 2694
		private global::System.Windows.Forms.Label label63;

		// Token: 0x04000A87 RID: 2695
		private global::System.Windows.Forms.TextBox textBox12;

		// Token: 0x04000A88 RID: 2696
		private global::System.Windows.Forms.TextBox textBoxCharSpamSubstitution;

		// Token: 0x04000A89 RID: 2697
		private global::System.Windows.Forms.Label label52;

		// Token: 0x04000A8A RID: 2698
		private global::System.Windows.Forms.Label label51;

		// Token: 0x04000A8B RID: 2699
		private global::System.Windows.Forms.TextBox textBoxMaxChars;

		// Token: 0x04000A8C RID: 2700
		private global::System.Windows.Forms.TextBox textBoxMaxIllegalGroups;

		// Token: 0x04000A8D RID: 2701
		private global::System.Windows.Forms.Label label47;

		// Token: 0x04000A8E RID: 2702
		private global::System.Windows.Forms.TabPage tabPageChatBadWords;

		// Token: 0x04000A8F RID: 2703
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000A90 RID: 2704
		private global::System.Windows.Forms.RadioButton radioButtonBadLanguage3;

		// Token: 0x04000A91 RID: 2705
		private global::System.Windows.Forms.RadioButton radioButtonBadLanguage2;

		// Token: 0x04000A92 RID: 2706
		private global::System.Windows.Forms.RadioButton radioButtonBadLanguage1;

		// Token: 0x04000A93 RID: 2707
		private global::System.Windows.Forms.Label label69;

		// Token: 0x04000A94 RID: 2708
		private global::System.Windows.Forms.Label label68;

		// Token: 0x04000A95 RID: 2709
		private global::System.Windows.Forms.TextBox textBoxBadLanguageKickMsg;

		// Token: 0x04000A96 RID: 2710
		private global::System.Windows.Forms.Label label67;

		// Token: 0x04000A97 RID: 2711
		private global::System.Windows.Forms.TextBox textBoxBadLanguageWarning;

		// Token: 0x04000A98 RID: 2712
		private global::System.Windows.Forms.Label label55;

		// Token: 0x04000A99 RID: 2713
		private global::System.Windows.Forms.TextBox textBox13;

		// Token: 0x04000A9A RID: 2714
		private global::System.Windows.Forms.Label label62;

		// Token: 0x04000A9B RID: 2715
		private global::System.Windows.Forms.TextBox textBoxBadLanguageKickLimit;

		// Token: 0x04000A9C RID: 2716
		private global::System.Windows.Forms.ComboBox comboBoxDetectionLevel;

		// Token: 0x04000A9D RID: 2717
		private global::System.Windows.Forms.Button button6;

		// Token: 0x04000A9E RID: 2718
		private global::System.Windows.Forms.Label label53;

		// Token: 0x04000A9F RID: 2719
		private global::System.Windows.Forms.Label label54;

		// Token: 0x04000AA0 RID: 2720
		private global::System.Windows.Forms.TextBox textBoxBadLanguageSubstitution;

		// Token: 0x04000AA1 RID: 2721
		private global::System.Windows.Forms.TabPage tabPageChatSpam;

		// Token: 0x04000AA2 RID: 2722
		private global::System.Windows.Forms.TextBox textBoxDuplicateMessagesWarning;

		// Token: 0x04000AA3 RID: 2723
		private global::System.Windows.Forms.Label label71;

		// Token: 0x04000AA4 RID: 2724
		private global::System.Windows.Forms.Label label70;

		// Token: 0x04000AA5 RID: 2725
		private global::System.Windows.Forms.TextBox textBoxMaxMessagesWarning;

		// Token: 0x04000AA6 RID: 2726
		private global::System.Windows.Forms.Label label64;

		// Token: 0x04000AA7 RID: 2727
		private global::System.Windows.Forms.TextBox textBox14;

		// Token: 0x04000AA8 RID: 2728
		private global::System.Windows.Forms.Label label61;

		// Token: 0x04000AA9 RID: 2729
		private global::System.Windows.Forms.TextBox textBoxDuplicateMessagesSeconds;

		// Token: 0x04000AAA RID: 2730
		private global::System.Windows.Forms.Label label60;

		// Token: 0x04000AAB RID: 2731
		private global::System.Windows.Forms.Label label59;

		// Token: 0x04000AAC RID: 2732
		private global::System.Windows.Forms.TextBox textBoxMaxMessages;

		// Token: 0x04000AAD RID: 2733
		private global::System.Windows.Forms.TextBox textBoxMaxMessagesSeconds;

		// Token: 0x04000AAE RID: 2734
		private global::System.Windows.Forms.Label label56;

		// Token: 0x04000AAF RID: 2735
		private global::System.Windows.Forms.Label label58;

		// Token: 0x04000AB0 RID: 2736
		private global::System.Windows.Forms.Label label57;

		// Token: 0x04000AB1 RID: 2737
		private global::System.Windows.Forms.TextBox textBoxDuplicateMessages;

		// Token: 0x04000AB2 RID: 2738
		private global::System.Windows.Forms.TabPage tabPageChat1;

		// Token: 0x04000AB3 RID: 2739
		private global::System.Windows.Forms.CheckBox checkBoxChatFilterAdvanced;

		// Token: 0x04000AB4 RID: 2740
		private global::System.Windows.Forms.Label label75;

		// Token: 0x04000AB5 RID: 2741
		private global::System.Windows.Forms.Label label74;

		// Token: 0x04000AB6 RID: 2742
		private global::System.Windows.Forms.Label label73;

		// Token: 0x04000AB7 RID: 2743
		private global::System.Windows.Forms.Label label72;

		// Token: 0x04000AB8 RID: 2744
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x04000AB9 RID: 2745
		private global::System.Windows.Forms.CheckBox checkBoxRemoveCaps;

		// Token: 0x04000ABA RID: 2746
		private global::System.Windows.Forms.CheckBox checkBoxShortenRepetitions;

		// Token: 0x04000ABB RID: 2747
		private global::System.Windows.Forms.CheckBox checkBoxRemoveBadWords;

		// Token: 0x04000ABC RID: 2748
		private global::System.Windows.Forms.CheckBox checkBoxMessagesCooldown;

		// Token: 0x04000ABD RID: 2749
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000ABE RID: 2750
		private global::System.Windows.Forms.CheckBox checkBoxTempMute;

		// Token: 0x04000ABF RID: 2751
		private global::System.Windows.Forms.TextBox textBoxCmdShortcut;

		// Token: 0x04000AC0 RID: 2752
		private global::System.Windows.Forms.Label label76;

		// Token: 0x04000AC1 RID: 2753
		private global::System.Windows.Forms.TabControl tabControl3;

		// Token: 0x04000AC2 RID: 2754
		private global::System.Windows.Forms.TabPage tabPage15;

		// Token: 0x04000AC3 RID: 2755
		private global::System.Windows.Forms.TabPage tabPage16;

		// Token: 0x04000AC4 RID: 2756
		private global::System.Windows.Forms.Label label39;

		// Token: 0x04000AC5 RID: 2757
		private global::System.Windows.Forms.Button vipAdd;

		// Token: 0x04000AC6 RID: 2758
		private global::System.Windows.Forms.Button vipRemove;

		// Token: 0x04000AC7 RID: 2759
		private global::System.Windows.Forms.TextBox vipEntry;

		// Token: 0x04000AC8 RID: 2760
		private global::System.Windows.Forms.Label vipLabel;

		// Token: 0x04000AC9 RID: 2761
		private global::System.Windows.Forms.ListBox vipList;

		// Token: 0x04000ACA RID: 2762
		private global::System.Windows.Forms.CheckBox checkBoxCmdCooldown;

		// Token: 0x04000ACB RID: 2763
		private global::System.Windows.Forms.Label label77;

		// Token: 0x04000ACC RID: 2764
		private global::System.Windows.Forms.TextBox textBoxCmdMax;

		// Token: 0x04000ACD RID: 2765
		private global::System.Windows.Forms.TextBox textBoxCmdMaxSeconds;

		// Token: 0x04000ACE RID: 2766
		private global::System.Windows.Forms.Label label78;

		// Token: 0x04000ACF RID: 2767
		private global::System.Windows.Forms.Label label79;

		// Token: 0x04000AD0 RID: 2768
		private global::System.Windows.Forms.Label label80;

		// Token: 0x04000AD1 RID: 2769
		private global::System.Windows.Forms.TextBox textBoxCmdWarning;

		// Token: 0x04000AD2 RID: 2770
		private global::System.Windows.Forms.Label label90;

		// Token: 0x04000AD3 RID: 2771
		private global::System.Windows.Forms.Label label89;

		// Token: 0x04000AD4 RID: 2772
		private global::System.Windows.Forms.Label label88;

		// Token: 0x04000AD5 RID: 2773
		private global::System.Windows.Forms.Label label87;

		// Token: 0x04000AD6 RID: 2774
		private global::System.Windows.Forms.Label label86;

		// Token: 0x04000AD7 RID: 2775
		private global::System.Windows.Forms.Label label85;

		// Token: 0x04000AD8 RID: 2776
		private global::System.Windows.Forms.TextBox textBoxBigMaps;

		// Token: 0x04000AD9 RID: 2777
		private global::System.Windows.Forms.TextBox textBoxMediumMaps;

		// Token: 0x04000ADA RID: 2778
		private global::System.Windows.Forms.TextBox textBoxSmallMaps;

		// Token: 0x04000ADB RID: 2779
		private global::System.Windows.Forms.Label label84;

		// Token: 0x04000ADC RID: 2780
		private global::System.Windows.Forms.Label label83;

		// Token: 0x04000ADD RID: 2781
		private global::System.Windows.Forms.Label label81;

		// Token: 0x04000ADE RID: 2782
		private global::System.Windows.Forms.Label label82;
	}
}
