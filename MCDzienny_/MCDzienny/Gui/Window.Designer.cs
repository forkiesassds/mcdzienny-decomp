namespace MCDzienny.Gui
{
	// Token: 0x0200035F RID: 863
	public partial class Window : global::System.Windows.Forms.Form
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000AE0F0 File Offset: 0x000AC2F0
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mapsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finiteModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animalAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeWaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.growingGrassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.survivalDeathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killerBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whoisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zombieSurvivalTab = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.zombieSettings = new System.Windows.Forms.Button();
            this.infectionMapsGrid = new MCDzienny.Misc.DataGridViewEnumerated();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            this.iconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.hideConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownServer = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.mode = new System.Windows.Forms.ComboBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.gBChat = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.gBCommands = new System.Windows.Forms.GroupBox();
            this.txtCommandsUsed = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewPlayers = new MCDzienny.CustomListView();
            this.PlayersColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayersColumnMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayersColumnAfk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.pCount = new System.Windows.Forms.Label();
            this.listViewMaps = new MCDzienny.CustomListView();
            this.mapColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mapColumnPhysics = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mapColumnPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mapColumnWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chatTab = new System.Windows.Forms.TabPage();
            this.chatWarningLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chatPlayerList = new System.Windows.Forms.ListBox();
            this.chatMainBox = new System.Windows.Forms.RichTextBox();
            this.chatOnOff_btn = new System.Windows.Forms.Button();
            this.chatInputBox = new System.Windows.Forms.TextBox();
            this.cBlack = new System.Windows.Forms.Label();
            this.cWhite = new System.Windows.Forms.Label();
            this.cDarkBlue = new System.Windows.Forms.Label();
            this.cYellow = new System.Windows.Forms.Label();
            this.cDarkGreen = new System.Windows.Forms.Label();
            this.cPink = new System.Windows.Forms.Label();
            this.cTeal = new System.Windows.Forms.Label();
            this.cRed = new System.Windows.Forms.Label();
            this.cDarkRed = new System.Windows.Forms.Label();
            this.cAqua = new System.Windows.Forms.Label();
            this.cPurple = new System.Windows.Forms.Label();
            this.cBrightGreen = new System.Windows.Forms.Label();
            this.cGold = new System.Windows.Forms.Label();
            this.cBlue = new System.Windows.Forms.Label();
            this.cGray = new System.Windows.Forms.Label();
            this.cDarkGray = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tabPagePlugins = new System.Windows.Forms.TabPage();
            this.pnlPlugin = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblPluginDesc = new System.Windows.Forms.Label();
            this.lblPluginAuthor = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.playersTab = new System.Windows.Forms.TabPage();
            this.playerColorCombo = new System.Windows.Forms.ComboBox();
            this.targetMapCombo = new System.Windows.Forms.ComboBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.banCheck = new System.Windows.Forms.CheckBox();
            this.kickCheck = new System.Windows.Forms.CheckBox();
            this.playersListView = new System.Windows.Forms.ListView();
            this.PlName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleText = new System.Windows.Forms.TextBox();
            this.banText = new System.Windows.Forms.TextBox();
            this.kickText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.playersGrid = new System.Windows.Forms.PropertyGrid();
            this.button4 = new System.Windows.Forms.Button();
            this.xbanCheck = new System.Windows.Forms.CheckBox();
            this.xbanText = new System.Windows.Forms.TextBox();
            this.mapsTab = new System.Windows.Forms.TabPage();
            this.mapsList = new System.Windows.Forms.ListBox();
            this.button13 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.unloadedMapsList = new System.Windows.Forms.ListBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btnCreateMap = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.allMapsGrid = new System.Windows.Forms.PropertyGrid();
            this.lavaTab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.mapsGrid = new MCDzienny.Misc.DataGridViewEnumerated();
            this.mapName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeOfLava = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changelogTab = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSystem = new System.Windows.Forms.TextBox();
            this.errorsTab = new System.Windows.Forms.TabPage();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button5 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelUptime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRoundTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLagometer = new System.Windows.Forms.ToolStripStatusLabel();
            this.mapsStrip.SuspendLayout();
            this.playerStrip.SuspendLayout();
            this.zombieSurvivalTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infectionMapsGrid)).BeginInit();
            this.iconContext.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.mainTabs.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.gBChat.SuspendLayout();
            this.gBCommands.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.chatTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPagePlugins.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.playersTab.SuspendLayout();
            this.mapsTab.SuspendLayout();
            this.lavaTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapsGrid)).BeginInit();
            this.changelogTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.errorsTab.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapsStrip
            // 
            this.mapsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.physicsToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mapsStrip.Name = "mapsStrip";
            this.mapsStrip.Size = new System.Drawing.Size(117, 92);
            this.mapsStrip.Opening += new System.ComponentModel.CancelEventHandler(this.mapsStrip_Opening);
            // 
            // physicsToolStripMenuItem
            // 
            this.physicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.physicsToolStripMenuItem.Name = "physicsToolStripMenuItem";
            this.physicsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.physicsToolStripMenuItem.Text = "Physics";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem2.Text = "off";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem3.Text = "1";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem4.Text = "2";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem5.Text = "3";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem6.Text = "door";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.unloadToolStripMenuItem.Text = "Unload";
            this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finiteModeToolStripMenuItem,
            this.animalAIToolStripMenuItem,
            this.edgeWaterToolStripMenuItem,
            this.growingGrassToolStripMenuItem,
            this.survivalDeathToolStripMenuItem,
            this.killerBlocksToolStripMenuItem,
            this.rPChatToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // finiteModeToolStripMenuItem
            // 
            this.finiteModeToolStripMenuItem.Name = "finiteModeToolStripMenuItem";
            this.finiteModeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.finiteModeToolStripMenuItem.Text = "Finite Mode";
            this.finiteModeToolStripMenuItem.Click += new System.EventHandler(this.finiteModeToolStripMenuItem_Click);
            // 
            // animalAIToolStripMenuItem
            // 
            this.animalAIToolStripMenuItem.Name = "animalAIToolStripMenuItem";
            this.animalAIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.animalAIToolStripMenuItem.Text = "Animal AI";
            this.animalAIToolStripMenuItem.Click += new System.EventHandler(this.animalAIToolStripMenuItem_Click);
            // 
            // edgeWaterToolStripMenuItem
            // 
            this.edgeWaterToolStripMenuItem.Name = "edgeWaterToolStripMenuItem";
            this.edgeWaterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.edgeWaterToolStripMenuItem.Text = "Edge Water";
            this.edgeWaterToolStripMenuItem.Click += new System.EventHandler(this.edgeWaterToolStripMenuItem_Click);
            // 
            // growingGrassToolStripMenuItem
            // 
            this.growingGrassToolStripMenuItem.Name = "growingGrassToolStripMenuItem";
            this.growingGrassToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.growingGrassToolStripMenuItem.Text = "Grass Growing";
            this.growingGrassToolStripMenuItem.Click += new System.EventHandler(this.growingGrassToolStripMenuItem_Click);
            // 
            // survivalDeathToolStripMenuItem
            // 
            this.survivalDeathToolStripMenuItem.Name = "survivalDeathToolStripMenuItem";
            this.survivalDeathToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.survivalDeathToolStripMenuItem.Text = "Survival Death";
            this.survivalDeathToolStripMenuItem.Click += new System.EventHandler(this.survivalDeathToolStripMenuItem_Click);
            // 
            // killerBlocksToolStripMenuItem
            // 
            this.killerBlocksToolStripMenuItem.Name = "killerBlocksToolStripMenuItem";
            this.killerBlocksToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.killerBlocksToolStripMenuItem.Text = "Killer Blocks";
            this.killerBlocksToolStripMenuItem.Click += new System.EventHandler(this.killerBlocksToolStripMenuItem_Click);
            // 
            // rPChatToolStripMenuItem
            // 
            this.rPChatToolStripMenuItem.Name = "rPChatToolStripMenuItem";
            this.rPChatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rPChatToolStripMenuItem.Text = "RP Chat";
            this.rPChatToolStripMenuItem.Click += new System.EventHandler(this.rPChatToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // playerStrip
            // 
            this.playerStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whoisToolStripMenuItem,
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.voiceToolStripMenuItem});
            this.playerStrip.Name = "playerStrip";
            this.playerStrip.Size = new System.Drawing.Size(106, 92);
            this.playerStrip.Opening += new System.ComponentModel.CancelEventHandler(this.playerStrip_Opening);
            // 
            // whoisToolStripMenuItem
            // 
            this.whoisToolStripMenuItem.Name = "whoisToolStripMenuItem";
            this.whoisToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.whoisToolStripMenuItem.Text = "whois";
            this.whoisToolStripMenuItem.Click += new System.EventHandler(this.whoisToolStripMenuItem_Click);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.kickToolStripMenuItem.Text = "kick";
            this.kickToolStripMenuItem.Click += new System.EventHandler(this.kickToolStripMenuItem_Click);
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.banToolStripMenuItem.Text = "ban";
            this.banToolStripMenuItem.Click += new System.EventHandler(this.banToolStripMenuItem_Click);
            // 
            // voiceToolStripMenuItem
            // 
            this.voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            this.voiceToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.voiceToolStripMenuItem.Text = "voice";
            this.voiceToolStripMenuItem.Click += new System.EventHandler(this.voiceToolStripMenuItem_Click);
            // 
            // zombieSurvivalTab
            // 
            this.zombieSurvivalTab.BackColor = System.Drawing.Color.Transparent;
            this.zombieSurvivalTab.Controls.Add(this.label5);
            this.zombieSurvivalTab.Controls.Add(this.zombieSettings);
            this.zombieSurvivalTab.Controls.Add(this.infectionMapsGrid);
            this.zombieSurvivalTab.Location = new System.Drawing.Point(4, 22);
            this.zombieSurvivalTab.Name = "zombieSurvivalTab";
            this.zombieSurvivalTab.Padding = new System.Windows.Forms.Padding(3);
            this.zombieSurvivalTab.Size = new System.Drawing.Size(702, 488);
            this.zombieSurvivalTab.TabIndex = 7;
            this.zombieSurvivalTab.Text = "Zombie Survival";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(42, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Infection maps:";
            // 
            // zombieSettings
            // 
            this.zombieSettings.Location = new System.Drawing.Point(523, 12);
            this.zombieSettings.Name = "zombieSettings";
            this.zombieSettings.Size = new System.Drawing.Size(106, 23);
            this.zombieSettings.TabIndex = 0;
            this.zombieSettings.Text = "Zombie Settings";
            this.zombieSettings.UseVisualStyleBackColor = true;
            this.zombieSettings.Click += new System.EventHandler(this.zombieSettings_Click);
            // 
            // infectionMapsGrid
            // 
            this.infectionMapsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.infectionMapsGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.infectionMapsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.infectionMapsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.infectionMapsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.infectionMapsGrid.Location = new System.Drawing.Point(43, 49);
            this.infectionMapsGrid.Name = "infectionMapsGrid";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.infectionMapsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.infectionMapsGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.infectionMapsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.infectionMapsGrid.Size = new System.Drawing.Size(616, 406);
            this.infectionMapsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 135.3597F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Map Name";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 88.54781F;
            this.dataGridViewTextBoxColumn2.HeaderText = "x";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 15;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 88.54781F;
            this.dataGridViewTextBoxColumn3.HeaderText = "y";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 15;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 88.54781F;
            this.dataGridViewTextBoxColumn4.HeaderText = "z";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 15;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 103.8501F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Phase I";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 30;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 106.599F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Phase II";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 30;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 88.54781F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Block";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 30;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // tmrRestart
            // 
            this.tmrRestart.Enabled = true;
            this.tmrRestart.Interval = 1000;
            // 
            // iconContext
            // 
            this.iconContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConsole,
            this.hideConsole,
            this.shutdownServer});
            this.iconContext.Name = "iconContext";
            this.iconContext.Size = new System.Drawing.Size(164, 70);
            // 
            // openConsole
            // 
            this.openConsole.Name = "openConsole";
            this.openConsole.Size = new System.Drawing.Size(163, 22);
            this.openConsole.Text = "Open Console";
            this.openConsole.Click += new System.EventHandler(this.openConsole_Click);
            // 
            // hideConsole
            // 
            this.hideConsole.Name = "hideConsole";
            this.hideConsole.Size = new System.Drawing.Size(163, 22);
            this.hideConsole.Text = "Hide Console";
            this.hideConsole.Click += new System.EventHandler(this.hideConsole_Click);
            // 
            // shutdownServer
            // 
            this.shutdownServer.Name = "shutdownServer";
            this.shutdownServer.Size = new System.Drawing.Size(163, 22);
            this.shutdownServer.Text = "Shutdown Server";
            this.shutdownServer.Click += new System.EventHandler(this.shutdownServer_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(709, 514);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(709, 514);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(709, 514);
            this.toolStripContainer1.TabIndex = 52;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Padding = new System.Windows.Forms.Padding(0, 0, 25, 25);
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // mainTabs
            // 
            this.mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabs.Controls.Add(this.mainTab);
            this.mainTabs.Controls.Add(this.chatTab);
            this.mainTabs.Controls.Add(this.tabPagePlugins);
            this.mainTabs.Controls.Add(this.playersTab);
            this.mainTabs.Controls.Add(this.mapsTab);
            this.mainTabs.Controls.Add(this.lavaTab);
            this.mainTabs.Controls.Add(this.changelogTab);
            this.mainTabs.Controls.Add(this.errorsTab);
            this.mainTabs.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainTabs.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.mainTabs.Location = new System.Drawing.Point(2, 13);
            this.mainTabs.MinimumSize = new System.Drawing.Size(710, 512);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(728, 512);
            this.mainTabs.TabIndex = 2;
            this.mainTabs.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // mainTab
            // 
            this.mainTab.BackColor = System.Drawing.Color.Transparent;
            this.mainTab.Controls.Add(this.splitContainer5);
            this.mainTab.Controls.Add(this.label17);
            this.mainTab.Controls.Add(this.mode);
            this.mainTab.Controls.Add(this.txtUrl);
            this.mainTab.Controls.Add(this.splitContainer3);
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(720, 486);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Main";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.Location = new System.Drawing.Point(13, 442);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.txtInput);
            this.splitContainer5.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.txtCommands);
            this.splitContainer5.Panel2.Controls.Add(this.label2);
            this.splitContainer5.Size = new System.Drawing.Size(688, 34);
            this.splitContainer5.SplitterDistance = 451;
            this.splitContainer5.TabIndex = 47;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(53, 7);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(395, 21);
            this.txtInput.TabIndex = 27;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 14);
            this.label1.TabIndex = 26;
            this.label1.Text = "Chat:";
            // 
            // txtCommands
            // 
            this.txtCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(72, 8);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(158, 21);
            this.txtCommands.TabIndex = 28;
            this.txtCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 29;
            this.label2.Text = "Command:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(481, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 18);
            this.label17.TabIndex = 40;
            this.label17.Text = "Game mode:";
            // 
            // mode
            // 
            this.mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mode.FormattingEnabled = true;
            this.mode.Items.AddRange(new object[] {
            "Lava Survival",
            "Lava/Freebuild",
            "Freebuild",
            "Zombie(Beta)"});
            this.mode.Location = new System.Drawing.Point(565, 10);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(136, 21);
            this.mode.TabIndex = 39;
            this.mode.SelectedIndexChanged += new System.EventHandler(this.mode_SelectedIndexChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtUrl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(13, 7);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(446, 21);
            this.txtUrl.TabIndex = 25;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(13, 34);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(688, 406);
            this.splitContainer3.SplitterDistance = 406;
            this.splitContainer3.TabIndex = 46;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(3, 6);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.gBChat);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gBCommands);
            this.splitContainer4.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer4_Panel2_Paint);
            this.splitContainer4.Size = new System.Drawing.Size(395, 396);
            this.splitContainer4.SplitterDistance = 237;
            this.splitContainer4.TabIndex = 35;
            // 
            // gBChat
            // 
            this.gBChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBChat.Controls.Add(this.txtLog);
            this.gBChat.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gBChat.Location = new System.Drawing.Point(3, 8);
            this.gBChat.Name = "gBChat";
            this.gBChat.Size = new System.Drawing.Size(389, 219);
            this.gBChat.TabIndex = 32;
            this.gBChat.TabStop = false;
            this.gBChat.Text = "Chat";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(383, 200);
            this.txtLog.TabIndex = 1;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // gBCommands
            // 
            this.gBCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBCommands.Controls.Add(this.txtCommandsUsed);
            this.gBCommands.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gBCommands.Location = new System.Drawing.Point(3, 3);
            this.gBCommands.Name = "gBCommands";
            this.gBCommands.Size = new System.Drawing.Size(389, 149);
            this.gBCommands.TabIndex = 34;
            this.gBCommands.TabStop = false;
            this.gBCommands.Text = "Commands";
            this.gBCommands.Enter += new System.EventHandler(this.gBCommands_Enter);
            // 
            // txtCommandsUsed
            // 
            this.txtCommandsUsed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandsUsed.BackColor = System.Drawing.Color.White;
            this.txtCommandsUsed.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCommandsUsed.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandsUsed.Location = new System.Drawing.Point(9, 21);
            this.txtCommandsUsed.Multiline = true;
            this.txtCommandsUsed.Name = "txtCommandsUsed";
            this.txtCommandsUsed.ReadOnly = true;
            this.txtCommandsUsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandsUsed.Size = new System.Drawing.Size(380, 125);
            this.txtCommandsUsed.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewPlayers);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.pCount);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewMaps);
            this.splitContainer2.Panel2.Controls.Add(this.mCount);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(272, 399);
            this.splitContainer2.SplitterDistance = 213;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 45;
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlayersColumnName,
            this.PlayersColumnMap,
            this.PlayersColumnAfk});
            this.listViewPlayers.ContextMenuStrip = this.playerStrip;
            this.listViewPlayers.FullRowSelect = true;
            this.listViewPlayers.Location = new System.Drawing.Point(7, 23);
            this.listViewPlayers.MultiSelect = false;
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(262, 176);
            this.listViewPlayers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewPlayers.TabIndex = 44;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            this.listViewPlayers.SelectedIndexChanged += new System.EventHandler(this.listViewPlayers_SelectedIndexChanged);
            // 
            // PlayersColumnName
            // 
            this.PlayersColumnName.Text = "Name";
            this.PlayersColumnName.Width = 101;
            // 
            // PlayersColumnMap
            // 
            this.PlayersColumnMap.Text = "Map";
            this.PlayersColumnMap.Width = 92;
            // 
            // PlayersColumnAfk
            // 
            this.PlayersColumnAfk.Text = "Afk";
            this.PlayersColumnAfk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PlayersColumnAfk.Width = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 14);
            this.label3.TabIndex = 41;
            this.label3.Text = "Players";
            // 
            // pCount
            // 
            this.pCount.AutoSize = true;
            this.pCount.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pCount.Location = new System.Drawing.Point(107, 6);
            this.pCount.Name = "pCount";
            this.pCount.Size = new System.Drawing.Size(13, 14);
            this.pCount.TabIndex = 43;
            this.pCount.Text = "0";
            // 
            // listViewMaps
            // 
            this.listViewMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mapColumnName,
            this.mapColumnPhysics,
            this.mapColumnPlayers,
            this.mapColumnWeight});
            this.listViewMaps.ContextMenuStrip = this.mapsStrip;
            this.listViewMaps.FullRowSelect = true;
            this.listViewMaps.Location = new System.Drawing.Point(7, 18);
            this.listViewMaps.MultiSelect = false;
            this.listViewMaps.Name = "listViewMaps";
            this.listViewMaps.Size = new System.Drawing.Size(262, 140);
            this.listViewMaps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMaps.TabIndex = 45;
            this.listViewMaps.UseCompatibleStateImageBehavior = false;
            this.listViewMaps.View = System.Windows.Forms.View.Details;
            // 
            // mapColumnName
            // 
            this.mapColumnName.Text = "Name";
            this.mapColumnName.Width = 84;
            // 
            // mapColumnPhysics
            // 
            this.mapColumnPhysics.Text = "Physics";
            this.mapColumnPhysics.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mapColumnPhysics.Width = 47;
            // 
            // mapColumnPlayers
            // 
            this.mapColumnPlayers.Text = "Players";
            this.mapColumnPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapColumnPlayers.Width = 47;
            // 
            // mapColumnWeight
            // 
            this.mapColumnWeight.Text = "Weight";
            this.mapColumnWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapColumnWeight.Width = 67;
            // 
            // mCount
            // 
            this.mCount.AutoSize = true;
            this.mCount.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mCount.Location = new System.Drawing.Point(107, 2);
            this.mCount.Name = "mCount";
            this.mCount.Size = new System.Drawing.Size(13, 14);
            this.mCount.TabIndex = 44;
            this.mCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 42;
            this.label4.Text = "Maps";
            // 
            // chatTab
            // 
            this.chatTab.BackColor = System.Drawing.Color.Transparent;
            this.chatTab.Controls.Add(this.chatWarningLabel);
            this.chatTab.Controls.Add(this.pictureBox1);
            this.chatTab.Controls.Add(this.chatPlayerList);
            this.chatTab.Controls.Add(this.chatMainBox);
            this.chatTab.Controls.Add(this.chatOnOff_btn);
            this.chatTab.Controls.Add(this.chatInputBox);
            this.chatTab.Controls.Add(this.cBlack);
            this.chatTab.Controls.Add(this.cWhite);
            this.chatTab.Controls.Add(this.cDarkBlue);
            this.chatTab.Controls.Add(this.cYellow);
            this.chatTab.Controls.Add(this.cDarkGreen);
            this.chatTab.Controls.Add(this.cPink);
            this.chatTab.Controls.Add(this.cTeal);
            this.chatTab.Controls.Add(this.cRed);
            this.chatTab.Controls.Add(this.cDarkRed);
            this.chatTab.Controls.Add(this.cAqua);
            this.chatTab.Controls.Add(this.cPurple);
            this.chatTab.Controls.Add(this.cBrightGreen);
            this.chatTab.Controls.Add(this.cGold);
            this.chatTab.Controls.Add(this.cBlue);
            this.chatTab.Controls.Add(this.cGray);
            this.chatTab.Controls.Add(this.cDarkGray);
            this.chatTab.Controls.Add(this.label25);
            this.chatTab.Location = new System.Drawing.Point(4, 22);
            this.chatTab.Name = "chatTab";
            this.chatTab.Padding = new System.Windows.Forms.Padding(3);
            this.chatTab.Size = new System.Drawing.Size(720, 486);
            this.chatTab.TabIndex = 7;
            this.chatTab.Text = "Chat";
            this.chatTab.Click += new System.EventHandler(this.chatTab_Click_1);
            // 
            // chatWarningLabel
            // 
            this.chatWarningLabel.AutoSize = true;
            this.chatWarningLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.chatWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.chatWarningLabel.Location = new System.Drawing.Point(123, 16);
            this.chatWarningLabel.Name = "chatWarningLabel";
            this.chatWarningLabel.Size = new System.Drawing.Size(573, 17);
            this.chatWarningLabel.TabIndex = 64;
            this.chatWarningLabel.Text = "You have to update MCDzienny.exe file manually in order to make the color chat wo" +
    "rk properly!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(147, 354);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 63;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // chatPlayerList
            // 
            this.chatPlayerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatPlayerList.FormattingEnabled = true;
            this.chatPlayerList.Location = new System.Drawing.Point(581, 43);
            this.chatPlayerList.Name = "chatPlayerList";
            this.chatPlayerList.Size = new System.Drawing.Size(117, 420);
            this.chatPlayerList.Sorted = true;
            this.chatPlayerList.TabIndex = 42;
            // 
            // chatMainBox
            // 
            this.chatMainBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatMainBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.chatMainBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatMainBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatMainBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chatMainBox.Location = new System.Drawing.Point(24, 45);
            this.chatMainBox.MaxLength = 10000;
            this.chatMainBox.Name = "chatMainBox";
            this.chatMainBox.ReadOnly = true;
            this.chatMainBox.Size = new System.Drawing.Size(551, 296);
            this.chatMainBox.TabIndex = 40;
            this.chatMainBox.Text = "";
            // 
            // chatOnOff_btn
            // 
            this.chatOnOff_btn.BackColor = System.Drawing.Color.Transparent;
            this.chatOnOff_btn.Location = new System.Drawing.Point(24, 16);
            this.chatOnOff_btn.Name = "chatOnOff_btn";
            this.chatOnOff_btn.Size = new System.Drawing.Size(75, 23);
            this.chatOnOff_btn.TabIndex = 62;
            this.chatOnOff_btn.Text = "Deactivated";
            this.chatOnOff_btn.UseVisualStyleBackColor = false;
            this.chatOnOff_btn.Click += new System.EventHandler(this.chatOnOff_btn_Click);
            // 
            // chatInputBox
            // 
            this.chatInputBox.AcceptsReturn = true;
            this.chatInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatInputBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chatInputBox.Location = new System.Drawing.Point(147, 374);
            this.chatInputBox.Multiline = true;
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatInputBox.Size = new System.Drawing.Size(426, 98);
            this.chatInputBox.TabIndex = 41;
            this.chatInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInputBox_KeyDown);
            // 
            // cBlack
            // 
            this.cBlack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBlack.AutoSize = true;
            this.cBlack.BackColor = System.Drawing.Color.Black;
            this.cBlack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cBlack.ForeColor = System.Drawing.Color.Black;
            this.cBlack.Location = new System.Drawing.Point(31, 362);
            this.cBlack.MinimumSize = new System.Drawing.Size(20, 20);
            this.cBlack.Name = "cBlack";
            this.cBlack.Size = new System.Drawing.Size(20, 20);
            this.cBlack.TabIndex = 45;
            this.cBlack.Tag = "%0";
            this.toolTip1.SetToolTip(this.cBlack, "%0");
            this.cBlack.Click += new System.EventHandler(this.Color_Click);
            // 
            // cWhite
            // 
            this.cWhite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cWhite.AutoSize = true;
            this.cWhite.BackColor = System.Drawing.Color.White;
            this.cWhite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cWhite.ForeColor = System.Drawing.Color.DarkBlue;
            this.cWhite.Location = new System.Drawing.Point(109, 362);
            this.cWhite.MinimumSize = new System.Drawing.Size(20, 20);
            this.cWhite.Name = "cWhite";
            this.cWhite.Size = new System.Drawing.Size(20, 20);
            this.cWhite.TabIndex = 60;
            this.cWhite.Tag = "%f";
            this.toolTip1.SetToolTip(this.cWhite, "%f");
            this.cWhite.Click += new System.EventHandler(this.Color_Click);
            // 
            // cDarkBlue
            // 
            this.cDarkBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cDarkBlue.AutoSize = true;
            this.cDarkBlue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(170)))));
            this.cDarkBlue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cDarkBlue.ForeColor = System.Drawing.Color.DarkBlue;
            this.cDarkBlue.Location = new System.Drawing.Point(31, 388);
            this.cDarkBlue.MinimumSize = new System.Drawing.Size(20, 20);
            this.cDarkBlue.Name = "cDarkBlue";
            this.cDarkBlue.Size = new System.Drawing.Size(20, 20);
            this.cDarkBlue.TabIndex = 46;
            this.cDarkBlue.Tag = "%1";
            this.toolTip1.SetToolTip(this.cDarkBlue, "%1");
            this.cDarkBlue.Click += new System.EventHandler(this.Color_Click);
            // 
            // cYellow
            // 
            this.cYellow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cYellow.AutoSize = true;
            this.cYellow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(85)))));
            this.cYellow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cYellow.ForeColor = System.Drawing.Color.DarkBlue;
            this.cYellow.Location = new System.Drawing.Point(109, 414);
            this.cYellow.MinimumSize = new System.Drawing.Size(20, 20);
            this.cYellow.Name = "cYellow";
            this.cYellow.Size = new System.Drawing.Size(20, 20);
            this.cYellow.TabIndex = 59;
            this.cYellow.Tag = "%e";
            this.toolTip1.SetToolTip(this.cYellow, "%e");
            this.cYellow.Click += new System.EventHandler(this.Color_Click);
            // 
            // cDarkGreen
            // 
            this.cDarkGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cDarkGreen.AutoSize = true;
            this.cDarkGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(0)))));
            this.cDarkGreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cDarkGreen.ForeColor = System.Drawing.Color.DarkBlue;
            this.cDarkGreen.Location = new System.Drawing.Point(31, 414);
            this.cDarkGreen.MinimumSize = new System.Drawing.Size(20, 20);
            this.cDarkGreen.Name = "cDarkGreen";
            this.cDarkGreen.Size = new System.Drawing.Size(20, 20);
            this.cDarkGreen.TabIndex = 47;
            this.cDarkGreen.Tag = "%2";
            this.toolTip1.SetToolTip(this.cDarkGreen, "%2");
            this.cDarkGreen.Click += new System.EventHandler(this.Color_Click);
            // 
            // cPink
            // 
            this.cPink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cPink.AutoSize = true;
            this.cPink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.cPink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cPink.ForeColor = System.Drawing.Color.DarkBlue;
            this.cPink.Location = new System.Drawing.Point(109, 440);
            this.cPink.MinimumSize = new System.Drawing.Size(20, 20);
            this.cPink.Name = "cPink";
            this.cPink.Size = new System.Drawing.Size(20, 20);
            this.cPink.TabIndex = 58;
            this.cPink.Tag = "%d";
            this.toolTip1.SetToolTip(this.cPink, "%d");
            this.cPink.Click += new System.EventHandler(this.Color_Click);
            // 
            // cTeal
            // 
            this.cTeal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cTeal.AutoSize = true;
            this.cTeal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cTeal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cTeal.ForeColor = System.Drawing.Color.DarkBlue;
            this.cTeal.Location = new System.Drawing.Point(83, 388);
            this.cTeal.MinimumSize = new System.Drawing.Size(20, 20);
            this.cTeal.Name = "cTeal";
            this.cTeal.Size = new System.Drawing.Size(20, 20);
            this.cTeal.TabIndex = 48;
            this.cTeal.Tag = "%3";
            this.toolTip1.SetToolTip(this.cTeal, "%3");
            this.cTeal.Click += new System.EventHandler(this.Color_Click);
            // 
            // cRed
            // 
            this.cRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cRed.AutoSize = true;
            this.cRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.cRed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cRed.ForeColor = System.Drawing.Color.DarkBlue;
            this.cRed.Location = new System.Drawing.Point(57, 440);
            this.cRed.MinimumSize = new System.Drawing.Size(20, 20);
            this.cRed.Name = "cRed";
            this.cRed.Size = new System.Drawing.Size(20, 20);
            this.cRed.TabIndex = 57;
            this.cRed.Tag = "%c";
            this.toolTip1.SetToolTip(this.cRed, "%c");
            this.cRed.Click += new System.EventHandler(this.Color_Click);
            // 
            // cDarkRed
            // 
            this.cDarkRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cDarkRed.AutoSize = true;
            this.cDarkRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cDarkRed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cDarkRed.ForeColor = System.Drawing.Color.DarkBlue;
            this.cDarkRed.Location = new System.Drawing.Point(31, 440);
            this.cDarkRed.MinimumSize = new System.Drawing.Size(20, 20);
            this.cDarkRed.Name = "cDarkRed";
            this.cDarkRed.Size = new System.Drawing.Size(20, 20);
            this.cDarkRed.TabIndex = 49;
            this.cDarkRed.Tag = "%4";
            this.toolTip1.SetToolTip(this.cDarkRed, "%4");
            this.cDarkRed.Click += new System.EventHandler(this.Color_Click);
            // 
            // cAqua
            // 
            this.cAqua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cAqua.AutoSize = true;
            this.cAqua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cAqua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cAqua.ForeColor = System.Drawing.Color.DarkBlue;
            this.cAqua.Location = new System.Drawing.Point(109, 388);
            this.cAqua.MinimumSize = new System.Drawing.Size(20, 20);
            this.cAqua.Name = "cAqua";
            this.cAqua.Size = new System.Drawing.Size(20, 20);
            this.cAqua.TabIndex = 56;
            this.cAqua.Tag = "%b";
            this.toolTip1.SetToolTip(this.cAqua, "%b");
            this.cAqua.Click += new System.EventHandler(this.Color_Click);
            // 
            // cPurple
            // 
            this.cPurple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cPurple.AutoSize = true;
            this.cPurple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(0)))), ((int)(((byte)(170)))));
            this.cPurple.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cPurple.ForeColor = System.Drawing.Color.DarkBlue;
            this.cPurple.Location = new System.Drawing.Point(83, 440);
            this.cPurple.MinimumSize = new System.Drawing.Size(20, 20);
            this.cPurple.Name = "cPurple";
            this.cPurple.Size = new System.Drawing.Size(20, 20);
            this.cPurple.TabIndex = 50;
            this.cPurple.Tag = "%5";
            this.toolTip1.SetToolTip(this.cPurple, "%5");
            this.cPurple.Click += new System.EventHandler(this.Color_Click);
            // 
            // cBrightGreen
            // 
            this.cBrightGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBrightGreen.AutoSize = true;
            this.cBrightGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(255)))), ((int)(((byte)(85)))));
            this.cBrightGreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cBrightGreen.ForeColor = System.Drawing.Color.DarkBlue;
            this.cBrightGreen.Location = new System.Drawing.Point(57, 414);
            this.cBrightGreen.MinimumSize = new System.Drawing.Size(20, 20);
            this.cBrightGreen.Name = "cBrightGreen";
            this.cBrightGreen.Size = new System.Drawing.Size(20, 20);
            this.cBrightGreen.TabIndex = 55;
            this.cBrightGreen.Tag = "%a";
            this.toolTip1.SetToolTip(this.cBrightGreen, "%a");
            this.cBrightGreen.Click += new System.EventHandler(this.Color_Click);
            // 
            // cGold
            // 
            this.cGold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cGold.AutoSize = true;
            this.cGold.BackColor = System.Drawing.Color.Gold;
            this.cGold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cGold.ForeColor = System.Drawing.Color.DarkBlue;
            this.cGold.Location = new System.Drawing.Point(83, 414);
            this.cGold.MinimumSize = new System.Drawing.Size(20, 20);
            this.cGold.Name = "cGold";
            this.cGold.Size = new System.Drawing.Size(20, 20);
            this.cGold.TabIndex = 51;
            this.cGold.Tag = "%6";
            this.toolTip1.SetToolTip(this.cGold, "%6");
            this.cGold.Click += new System.EventHandler(this.Color_Click);
            // 
            // cBlue
            // 
            this.cBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBlue.AutoSize = true;
            this.cBlue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.cBlue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cBlue.ForeColor = System.Drawing.Color.DarkBlue;
            this.cBlue.Location = new System.Drawing.Point(57, 388);
            this.cBlue.MinimumSize = new System.Drawing.Size(20, 20);
            this.cBlue.Name = "cBlue";
            this.cBlue.Size = new System.Drawing.Size(20, 20);
            this.cBlue.TabIndex = 54;
            this.cBlue.Tag = "%9";
            this.toolTip1.SetToolTip(this.cBlue, "%9");
            this.cBlue.Click += new System.EventHandler(this.Color_Click);
            // 
            // cGray
            // 
            this.cGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cGray.AutoSize = true;
            this.cGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cGray.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cGray.ForeColor = System.Drawing.Color.DarkBlue;
            this.cGray.Location = new System.Drawing.Point(83, 362);
            this.cGray.MinimumSize = new System.Drawing.Size(20, 20);
            this.cGray.Name = "cGray";
            this.cGray.Size = new System.Drawing.Size(20, 20);
            this.cGray.TabIndex = 52;
            this.cGray.Tag = "%7";
            this.toolTip1.SetToolTip(this.cGray, "%7");
            this.cGray.Click += new System.EventHandler(this.Color_Click);
            // 
            // cDarkGray
            // 
            this.cDarkGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cDarkGray.AutoSize = true;
            this.cDarkGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.cDarkGray.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cDarkGray.ForeColor = System.Drawing.Color.DarkBlue;
            this.cDarkGray.Location = new System.Drawing.Point(57, 362);
            this.cDarkGray.MinimumSize = new System.Drawing.Size(20, 20);
            this.cDarkGray.Name = "cDarkGray";
            this.cDarkGray.Size = new System.Drawing.Size(20, 20);
            this.cDarkGray.TabIndex = 53;
            this.cDarkGray.Tag = "%8";
            this.toolTip1.SetToolTip(this.cDarkGray, "%8");
            this.cDarkGray.Click += new System.EventHandler(this.Color_Click);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Gray;
            this.label25.ForeColor = System.Drawing.Color.DarkBlue;
            this.label25.Location = new System.Drawing.Point(21, 350);
            this.label25.MinimumSize = new System.Drawing.Size(120, 120);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(120, 120);
            this.label25.TabIndex = 61;
            // 
            // tabPagePlugins
            // 
            this.tabPagePlugins.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePlugins.Controls.Add(this.pnlPlugin);
            this.tabPagePlugins.Controls.Add(this.groupBox4);
            this.tabPagePlugins.Controls.Add(this.treeView1);
            this.tabPagePlugins.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlugins.Name = "tabPagePlugins";
            this.tabPagePlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlugins.Size = new System.Drawing.Size(720, 486);
            this.tabPagePlugins.TabIndex = 10;
            this.tabPagePlugins.Text = "Plugins";
            // 
            // pnlPlugin
            // 
            this.pnlPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlugin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlugin.Location = new System.Drawing.Point(201, 6);
            this.pnlPlugin.Name = "pnlPlugin";
            this.pnlPlugin.Size = new System.Drawing.Size(513, 474);
            this.pnlPlugin.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.lblPluginDesc);
            this.groupBox4.Controls.Add(this.lblPluginAuthor);
            this.groupBox4.Controls.Add(this.lblPluginVersion);
            this.groupBox4.Controls.Add(this.lblPluginName);
            this.groupBox4.Location = new System.Drawing.Point(7, 336);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 144);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Plugin Information:";
            // 
            // lblPluginDesc
            // 
            this.lblPluginDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginDesc.Location = new System.Drawing.Point(6, 65);
            this.lblPluginDesc.Name = "lblPluginDesc";
            this.lblPluginDesc.Size = new System.Drawing.Size(176, 64);
            this.lblPluginDesc.TabIndex = 4;
            this.lblPluginDesc.Text = "   Plugin Description Goes Here... Test One Two Three, This is a Test...";
            // 
            // lblPluginAuthor
            // 
            this.lblPluginAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginAuthor.Location = new System.Drawing.Point(6, 49);
            this.lblPluginAuthor.Name = "lblPluginAuthor";
            this.lblPluginAuthor.Size = new System.Drawing.Size(176, 16);
            this.lblPluginAuthor.TabIndex = 3;
            this.lblPluginAuthor.Text = "By: <Author\'s Name>";
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginVersion.Location = new System.Drawing.Point(6, 33);
            this.lblPluginVersion.Name = "lblPluginVersion";
            this.lblPluginVersion.Size = new System.Drawing.Size(176, 16);
            this.lblPluginVersion.TabIndex = 2;
            this.lblPluginVersion.Text = "(<Version>)";
            // 
            // lblPluginName
            // 
            this.lblPluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginName.Location = new System.Drawing.Point(6, 17);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new System.Drawing.Size(176, 16);
            this.lblPluginName.TabIndex = 1;
            this.lblPluginName.Text = "<Plugin Name Here>";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(6, 6);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(189, 323);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // playersTab
            // 
            this.playersTab.BackColor = System.Drawing.Color.Transparent;
            this.playersTab.Controls.Add(this.playerColorCombo);
            this.playersTab.Controls.Add(this.targetMapCombo);
            this.playersTab.Controls.Add(this.button15);
            this.playersTab.Controls.Add(this.button14);
            this.playersTab.Controls.Add(this.banCheck);
            this.playersTab.Controls.Add(this.kickCheck);
            this.playersTab.Controls.Add(this.playersListView);
            this.playersTab.Controls.Add(this.titleText);
            this.playersTab.Controls.Add(this.banText);
            this.playersTab.Controls.Add(this.kickText);
            this.playersTab.Controls.Add(this.label12);
            this.playersTab.Controls.Add(this.button8);
            this.playersTab.Controls.Add(this.button7);
            this.playersTab.Controls.Add(this.button6);
            this.playersTab.Controls.Add(this.btnMute);
            this.playersTab.Controls.Add(this.button3);
            this.playersTab.Controls.Add(this.button2);
            this.playersTab.Controls.Add(this.label10);
            this.playersTab.Controls.Add(this.label7);
            this.playersTab.Controls.Add(this.playersGrid);
            this.playersTab.Controls.Add(this.button4);
            this.playersTab.Controls.Add(this.xbanCheck);
            this.playersTab.Controls.Add(this.xbanText);
            this.playersTab.Location = new System.Drawing.Point(4, 22);
            this.playersTab.Name = "playersTab";
            this.playersTab.Padding = new System.Windows.Forms.Padding(3);
            this.playersTab.Size = new System.Drawing.Size(720, 486);
            this.playersTab.TabIndex = 8;
            this.playersTab.Text = "Players";
            // 
            // playerColorCombo
            // 
            this.playerColorCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playerColorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerColorCombo.FormattingEnabled = true;
            this.playerColorCombo.Location = new System.Drawing.Point(500, 395);
            this.playerColorCombo.Name = "playerColorCombo";
            this.playerColorCombo.Size = new System.Drawing.Size(102, 21);
            this.playerColorCombo.TabIndex = 66;
            // 
            // targetMapCombo
            // 
            this.targetMapCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.targetMapCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetMapCombo.FormattingEnabled = true;
            this.targetMapCombo.Location = new System.Drawing.Point(501, 422);
            this.targetMapCombo.Name = "targetMapCombo";
            this.targetMapCombo.Size = new System.Drawing.Size(101, 21);
            this.targetMapCombo.TabIndex = 65;
            this.targetMapCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button15.Location = new System.Drawing.Point(338, 364);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 64;
            this.button15.Text = "Undo All";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button14.Location = new System.Drawing.Point(419, 422);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 63;
            this.button14.Text = "Move";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // banCheck
            // 
            this.banCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.banCheck.AutoSize = true;
            this.banCheck.Location = new System.Drawing.Point(95, 426);
            this.banCheck.Name = "banCheck";
            this.banCheck.Size = new System.Drawing.Size(15, 14);
            this.banCheck.TabIndex = 61;
            this.banCheck.UseVisualStyleBackColor = true;
            this.banCheck.Visible = false;
            this.banCheck.CheckedChanged += new System.EventHandler(this.banCheck_CheckedChanged);
            // 
            // kickCheck
            // 
            this.kickCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kickCheck.AutoSize = true;
            this.kickCheck.Location = new System.Drawing.Point(95, 370);
            this.kickCheck.Name = "kickCheck";
            this.kickCheck.Size = new System.Drawing.Size(15, 14);
            this.kickCheck.TabIndex = 60;
            this.kickCheck.UseVisualStyleBackColor = true;
            this.kickCheck.CheckedChanged += new System.EventHandler(this.kickCheck_CheckedChanged);
            // 
            // playersListView
            // 
            this.playersListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.playersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlName,
            this.PlRank,
            this.PlMap});
            this.playersListView.FullRowSelect = true;
            this.playersListView.HideSelection = false;
            this.playersListView.Location = new System.Drawing.Point(18, 34);
            this.playersListView.MultiSelect = false;
            this.playersListView.Name = "playersListView";
            this.playersListView.Size = new System.Drawing.Size(304, 284);
            this.playersListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.playersListView.TabIndex = 59;
            this.playersListView.UseCompatibleStateImageBehavior = false;
            this.playersListView.View = System.Windows.Forms.View.Details;
            this.playersListView.SelectedIndexChanged += new System.EventHandler(this.playersListView_SelectedIndexChanged);
            // 
            // PlName
            // 
            this.PlName.Text = "Name";
            this.PlName.Width = 100;
            // 
            // PlRank
            // 
            this.PlRank.Text = "Rank";
            this.PlRank.Width = 100;
            // 
            // PlMap
            // 
            this.PlMap.Text = "Map";
            this.PlMap.Width = 100;
            // 
            // titleText
            // 
            this.titleText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.titleText.Location = new System.Drawing.Point(500, 366);
            this.titleText.MaxLength = 17;
            this.titleText.Name = "titleText";
            this.titleText.Size = new System.Drawing.Size(102, 21);
            this.titleText.TabIndex = 57;
            // 
            // banText
            // 
            this.banText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.banText.Location = new System.Drawing.Point(114, 424);
            this.banText.Name = "banText";
            this.banText.ReadOnly = true;
            this.banText.Size = new System.Drawing.Size(207, 21);
            this.banText.TabIndex = 55;
            this.banText.Visible = false;
            // 
            // kickText
            // 
            this.kickText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kickText.Location = new System.Drawing.Point(114, 366);
            this.kickText.Name = "kickText";
            this.kickText.ReadOnly = true;
            this.kickText.Size = new System.Drawing.Size(207, 21);
            this.kickText.TabIndex = 54;
            this.kickText.TextChanged += new System.EventHandler(this.kickText_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(15, 344);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 14);
            this.label12.TabIndex = 53;
            this.label12.Text = "Commands";
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.Location = new System.Drawing.Point(338, 393);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 51;
            this.button8.Text = "Kill";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.Location = new System.Drawing.Point(419, 393);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 47;
            this.button7.Text = "Color";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.Location = new System.Drawing.Point(419, 364);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 50;
            this.button6.Text = "Title";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnMute
            // 
            this.btnMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMute.Location = new System.Drawing.Point(338, 422);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(75, 23);
            this.btnMute.TabIndex = 49;
            this.btnMute.Text = "Mute";
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(18, 364);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 47;
            this.button3.Text = "Kick";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(18, 422);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 46;
            this.button2.Text = "Ban";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(367, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 14);
            this.label10.TabIndex = 45;
            this.label10.Text = "Properties";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(15, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 14);
            this.label7.TabIndex = 42;
            this.label7.Text = "Players";
            // 
            // playersGrid
            // 
            this.playersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playersGrid.Location = new System.Drawing.Point(370, 34);
            this.playersGrid.Name = "playersGrid";
            this.playersGrid.Size = new System.Drawing.Size(300, 284);
            this.playersGrid.TabIndex = 1;
            this.playersGrid.ToolbarVisible = false;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(18, 393);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 48;
            this.button4.Text = "XBan";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // xbanCheck
            // 
            this.xbanCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xbanCheck.AutoSize = true;
            this.xbanCheck.Location = new System.Drawing.Point(95, 399);
            this.xbanCheck.Name = "xbanCheck";
            this.xbanCheck.Size = new System.Drawing.Size(15, 14);
            this.xbanCheck.TabIndex = 62;
            this.xbanCheck.UseVisualStyleBackColor = true;
            this.xbanCheck.CheckedChanged += new System.EventHandler(this.xbanCheck_CheckedChanged);
            // 
            // xbanText
            // 
            this.xbanText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xbanText.Location = new System.Drawing.Point(114, 395);
            this.xbanText.Name = "xbanText";
            this.xbanText.ReadOnly = true;
            this.xbanText.Size = new System.Drawing.Size(207, 21);
            this.xbanText.TabIndex = 56;
            // 
            // mapsTab
            // 
            this.mapsTab.BackColor = System.Drawing.Color.Transparent;
            this.mapsTab.Controls.Add(this.mapsList);
            this.mapsTab.Controls.Add(this.button13);
            this.mapsTab.Controls.Add(this.label11);
            this.mapsTab.Controls.Add(this.unloadedMapsList);
            this.mapsTab.Controls.Add(this.button12);
            this.mapsTab.Controls.Add(this.button11);
            this.mapsTab.Controls.Add(this.button10);
            this.mapsTab.Controls.Add(this.btnCreateMap);
            this.mapsTab.Controls.Add(this.label9);
            this.mapsTab.Controls.Add(this.label8);
            this.mapsTab.Controls.Add(this.allMapsGrid);
            this.mapsTab.Location = new System.Drawing.Point(4, 22);
            this.mapsTab.Name = "mapsTab";
            this.mapsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mapsTab.Size = new System.Drawing.Size(720, 486);
            this.mapsTab.TabIndex = 9;
            this.mapsTab.Text = "Maps";
            // 
            // mapsList
            // 
            this.mapsList.FormattingEnabled = true;
            this.mapsList.Location = new System.Drawing.Point(33, 38);
            this.mapsList.Name = "mapsList";
            this.mapsList.Size = new System.Drawing.Size(132, 199);
            this.mapsList.TabIndex = 0;
            this.mapsList.SelectedIndexChanged += new System.EventHandler(this.mapsList_SelectedIndexChanged);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(191, 38);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 53;
            this.button13.Text = "Unload";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(30, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 14);
            this.label11.TabIndex = 52;
            this.label11.Text = "Unloaded maps";
            // 
            // unloadedMapsList
            // 
            this.unloadedMapsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.unloadedMapsList.FormattingEnabled = true;
            this.unloadedMapsList.Location = new System.Drawing.Point(33, 275);
            this.unloadedMapsList.Name = "unloadedMapsList";
            this.unloadedMapsList.Size = new System.Drawing.Size(132, 173);
            this.unloadedMapsList.TabIndex = 51;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(191, 96);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 50;
            this.button12.Text = "Delete";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(191, 67);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 49;
            this.button11.Text = "Rename";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.Location = new System.Drawing.Point(191, 436);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 48;
            this.button10.Text = "Load";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // btnCreateMap
            // 
            this.btnCreateMap.Location = new System.Drawing.Point(191, 214);
            this.btnCreateMap.Name = "btnCreateMap";
            this.btnCreateMap.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMap.TabIndex = 47;
            this.btnCreateMap.Text = "Create";
            this.btnCreateMap.UseVisualStyleBackColor = true;
            this.btnCreateMap.Click += new System.EventHandler(this.button9_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(395, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "Properties";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(30, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 14);
            this.label8.TabIndex = 43;
            this.label8.Text = "Maps";
            // 
            // allMapsGrid
            // 
            this.allMapsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allMapsGrid.Location = new System.Drawing.Point(398, 38);
            this.allMapsGrid.Name = "allMapsGrid";
            this.allMapsGrid.Size = new System.Drawing.Size(278, 409);
            this.allMapsGrid.TabIndex = 1;
            // 
            // lavaTab
            // 
            this.lavaTab.BackColor = System.Drawing.Color.Transparent;
            this.lavaTab.Controls.Add(this.label6);
            this.lavaTab.Controls.Add(this.mapsGrid);
            this.lavaTab.Location = new System.Drawing.Point(4, 22);
            this.lavaTab.Name = "lavaTab";
            this.lavaTab.Padding = new System.Windows.Forms.Padding(3);
            this.lavaTab.Size = new System.Drawing.Size(720, 486);
            this.lavaTab.TabIndex = 6;
            this.lavaTab.Text = "Lava Survival";
            this.lavaTab.Click += new System.EventHandler(this.lavaTab_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(42, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "Lava maps:";
            // 
            // mapsGrid
            // 
            this.mapsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mapsGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.mapsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mapName,
            this.sourceX,
            this.sourceY,
            this.sourceZ,
            this.phase1,
            this.phase2,
            this.typeOfLava});
            this.mapsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mapsGrid.Location = new System.Drawing.Point(43, 49);
            this.mapsGrid.Name = "mapsGrid";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mapsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mapsGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.mapsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.mapsGrid.Size = new System.Drawing.Size(616, 404);
            this.mapsGrid.TabIndex = 0;
            this.mapsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mapsGrid_CellContentClick);
            // 
            // mapName
            // 
            this.mapName.FillWeight = 135.3597F;
            this.mapName.HeaderText = "Map Name";
            this.mapName.MinimumWidth = 40;
            this.mapName.Name = "mapName";
            // 
            // sourceX
            // 
            this.sourceX.FillWeight = 88.54781F;
            this.sourceX.HeaderText = "x";
            this.sourceX.MinimumWidth = 15;
            this.sourceX.Name = "sourceX";
            // 
            // sourceY
            // 
            this.sourceY.FillWeight = 88.54781F;
            this.sourceY.HeaderText = "y";
            this.sourceY.MinimumWidth = 15;
            this.sourceY.Name = "sourceY";
            // 
            // sourceZ
            // 
            this.sourceZ.FillWeight = 88.54781F;
            this.sourceZ.HeaderText = "z";
            this.sourceZ.MinimumWidth = 15;
            this.sourceZ.Name = "sourceZ";
            // 
            // phase1
            // 
            this.phase1.FillWeight = 103.8501F;
            this.phase1.HeaderText = "Phase I";
            this.phase1.MinimumWidth = 30;
            this.phase1.Name = "phase1";
            // 
            // phase2
            // 
            this.phase2.FillWeight = 106.599F;
            this.phase2.HeaderText = "Phase II";
            this.phase2.MinimumWidth = 30;
            this.phase2.Name = "phase2";
            // 
            // typeOfLava
            // 
            this.typeOfLava.FillWeight = 88.54781F;
            this.typeOfLava.HeaderText = "Block";
            this.typeOfLava.MinimumWidth = 30;
            this.typeOfLava.Name = "typeOfLava";
            // 
            // changelogTab
            // 
            this.changelogTab.BackColor = System.Drawing.Color.Transparent;
            this.changelogTab.Controls.Add(this.tabControl1);
            this.changelogTab.Location = new System.Drawing.Point(4, 22);
            this.changelogTab.Name = "changelogTab";
            this.changelogTab.Padding = new System.Windows.Forms.Padding(7, 6, 20, 11);
            this.changelogTab.Size = new System.Drawing.Size(720, 486);
            this.changelogTab.TabIndex = 2;
            this.changelogTab.Text = "Info";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(7, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(693, 469);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.txtChangelog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(685, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Changelog";
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChangelog.Location = new System.Drawing.Point(3, 3);
            this.txtChangelog.Margin = new System.Windows.Forms.Padding(10, 10, 30, 30);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChangelog.Size = new System.Drawing.Size(679, 437);
            this.txtChangelog.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.txtSystem);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 443);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "System Log";
            // 
            // txtSystem
            // 
            this.txtSystem.BackColor = System.Drawing.Color.White;
            this.txtSystem.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystem.Location = new System.Drawing.Point(3, 3);
            this.txtSystem.Multiline = true;
            this.txtSystem.Name = "txtSystem";
            this.txtSystem.ReadOnly = true;
            this.txtSystem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystem.Size = new System.Drawing.Size(679, 437);
            this.txtSystem.TabIndex = 2;
            // 
            // errorsTab
            // 
            this.errorsTab.BackColor = System.Drawing.Color.Transparent;
            this.errorsTab.Controls.Add(this.txtErrors);
            this.errorsTab.Location = new System.Drawing.Point(4, 22);
            this.errorsTab.Name = "errorsTab";
            this.errorsTab.Padding = new System.Windows.Forms.Padding(7, 6, 20, 11);
            this.errorsTab.Size = new System.Drawing.Size(720, 486);
            this.errorsTab.TabIndex = 3;
            this.errorsTab.Text = "Errors";
            // 
            // txtErrors
            // 
            this.txtErrors.BackColor = System.Drawing.Color.White;
            this.txtErrors.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrors.Location = new System.Drawing.Point(7, 6);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ReadOnly = true;
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrors.Size = new System.Drawing.Size(693, 469);
            this.txtErrors.TabIndex = 1;
            this.txtErrors.TextChanged += new System.EventHandler(this.txtErrors_TextChanged);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.Location = new System.Drawing.Point(659, 5);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(64, 23);
            this.minimizeButton.TabIndex = 36;
            this.minimizeButton.Text = "Minimize";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProperties.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(585, 5);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(70, 23);
            this.btnProperties.TabIndex = 34;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(535, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(46, 23);
            this.button5.TabIndex = 37;
            this.button5.Text = "Tools";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelUptime,
            this.toolStripStatusLabelRoundTime,
            this.toolStripStatusLabelLagometer});
            this.statusStrip1.Location = new System.Drawing.Point(2, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(727, 23);
            this.statusStrip1.TabIndex = 38;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelUptime
            // 
            this.toolStripStatusLabelUptime.AutoSize = false;
            this.toolStripStatusLabelUptime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelUptime.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.toolStripStatusLabelUptime.Name = "toolStripStatusLabelUptime";
            this.toolStripStatusLabelUptime.Size = new System.Drawing.Size(128, 18);
            this.toolStripStatusLabelUptime.Text = "Uptime : 0min";
            this.toolStripStatusLabelUptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelRoundTime
            // 
            this.toolStripStatusLabelRoundTime.AutoSize = false;
            this.toolStripStatusLabelRoundTime.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelRoundTime.Name = "toolStripStatusLabelRoundTime";
            this.toolStripStatusLabelRoundTime.Size = new System.Drawing.Size(260, 18);
            this.toolStripStatusLabelRoundTime.Text = "Flood starts in : 1h 59min    Round ends in : 1h 59min";
            this.toolStripStatusLabelRoundTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelLagometer
            // 
            this.toolStripStatusLabelLagometer.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelLagometer.Name = "toolStripStatusLabelLagometer";
            this.toolStripStatusLabelLagometer.Size = new System.Drawing.Size(304, 18);
            this.toolStripStatusLabelLagometer.Spring = true;
            this.toolStripStatusLabelLagometer.Text = "Lag (avg.) : ";
            this.toolStripStatusLabelLagometer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 549);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.mainTabs);
            this.MinimumSize = new System.Drawing.Size(740, 580);
            this.Name = "Window";
            this.Padding = new System.Windows.Forms.Padding(2, 13, 3, 1);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.mapsStrip.ResumeLayout(false);
            this.playerStrip.ResumeLayout(false);
            this.zombieSurvivalTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infectionMapsGrid)).EndInit();
            this.iconContext.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.mainTabs.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.gBChat.ResumeLayout(false);
            this.gBChat.PerformLayout();
            this.gBCommands.ResumeLayout(false);
            this.gBCommands.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.chatTab.ResumeLayout(false);
            this.chatTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPagePlugins.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.playersTab.ResumeLayout(false);
            this.playersTab.PerformLayout();
            this.mapsTab.ResumeLayout(false);
            this.mapsTab.PerformLayout();
            this.lavaTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapsGrid)).EndInit();
            this.changelogTab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.errorsTab.ResumeLayout(false);
            this.errorsTab.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000D0E RID: 3342
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000D0F RID: 3343
		private global::System.Windows.Forms.Timer tmrRestart;

		// Token: 0x04000D10 RID: 3344
		private global::System.Windows.Forms.ContextMenuStrip iconContext;

		// Token: 0x04000D11 RID: 3345
		private global::System.Windows.Forms.ToolStripMenuItem openConsole;

		// Token: 0x04000D12 RID: 3346
		private global::System.Windows.Forms.ToolStripMenuItem shutdownServer;

		// Token: 0x04000D13 RID: 3347
		private global::System.Windows.Forms.ToolStripMenuItem hideConsole;

		// Token: 0x04000D14 RID: 3348
		private global::System.Windows.Forms.ContextMenuStrip playerStrip;

		// Token: 0x04000D15 RID: 3349
		private global::System.Windows.Forms.ToolStripMenuItem whoisToolStripMenuItem;

		// Token: 0x04000D16 RID: 3350
		private global::System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;

		// Token: 0x04000D17 RID: 3351
		private global::System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;

		// Token: 0x04000D18 RID: 3352
		private global::System.Windows.Forms.ToolStripMenuItem voiceToolStripMenuItem;

		// Token: 0x04000D19 RID: 3353
		private global::System.Windows.Forms.ContextMenuStrip mapsStrip;

		// Token: 0x04000D1A RID: 3354
		private global::System.Windows.Forms.ToolStripMenuItem physicsToolStripMenuItem;

		// Token: 0x04000D1B RID: 3355
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x04000D1C RID: 3356
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x04000D1D RID: 3357
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x04000D1E RID: 3358
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

		// Token: 0x04000D1F RID: 3359
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

		// Token: 0x04000D20 RID: 3360
		private global::System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem;

		// Token: 0x04000D21 RID: 3361
		private global::System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;

		// Token: 0x04000D22 RID: 3362
		private global::System.Windows.Forms.ToolStripMenuItem finiteModeToolStripMenuItem;

		// Token: 0x04000D23 RID: 3363
		private global::System.Windows.Forms.ToolStripMenuItem animalAIToolStripMenuItem;

		// Token: 0x04000D24 RID: 3364
		private global::System.Windows.Forms.ToolStripMenuItem edgeWaterToolStripMenuItem;

		// Token: 0x04000D25 RID: 3365
		private global::System.Windows.Forms.ToolStripMenuItem growingGrassToolStripMenuItem;

		// Token: 0x04000D26 RID: 3366
		private global::System.Windows.Forms.ToolStripMenuItem survivalDeathToolStripMenuItem;

		// Token: 0x04000D27 RID: 3367
		private global::System.Windows.Forms.ToolStripMenuItem killerBlocksToolStripMenuItem;

		// Token: 0x04000D28 RID: 3368
		private global::System.Windows.Forms.ToolStripMenuItem rPChatToolStripMenuItem;

		// Token: 0x04000D29 RID: 3369
		private global::System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

		// Token: 0x04000D2A RID: 3370
		private global::System.Windows.Forms.TabPage zombieSurvivalTab;

		// Token: 0x04000D2B RID: 3371
		private global::System.Windows.Forms.Button zombieSettings;

		// Token: 0x04000D2C RID: 3372
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000D2D RID: 3373
		private global::MCDzienny.Misc.DataGridViewEnumerated infectionMapsGrid;

		// Token: 0x04000D2E RID: 3374
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x04000D2F RID: 3375
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000D30 RID: 3376
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x04000D31 RID: 3377
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		// Token: 0x04000D32 RID: 3378
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		// Token: 0x04000D33 RID: 3379
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		// Token: 0x04000D34 RID: 3380
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		// Token: 0x04000D35 RID: 3381
		private global::System.Windows.Forms.ToolStripPanel BottomToolStripPanel;

		// Token: 0x04000D36 RID: 3382
		private global::System.Windows.Forms.ToolStripPanel TopToolStripPanel;

		// Token: 0x04000D37 RID: 3383
		private global::System.Windows.Forms.ToolStripPanel RightToolStripPanel;

		// Token: 0x04000D38 RID: 3384
		private global::System.Windows.Forms.ToolStripPanel LeftToolStripPanel;

		// Token: 0x04000D39 RID: 3385
		private global::System.Windows.Forms.ToolStripContentPanel ContentPanel;

		// Token: 0x04000D3A RID: 3386
		private global::System.Windows.Forms.ToolStripContainer toolStripContainer1;

		// Token: 0x04000D3B RID: 3387
		private global::System.Windows.Forms.TabControl mainTabs;

		// Token: 0x04000D3C RID: 3388
		private global::System.Windows.Forms.TabPage mainTab;

		// Token: 0x04000D3D RID: 3389
		private global::System.Windows.Forms.Label mCount;

		// Token: 0x04000D3E RID: 3390
		private global::System.Windows.Forms.Label pCount;

		// Token: 0x04000D3F RID: 3391
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000D40 RID: 3392
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000D41 RID: 3393
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000D42 RID: 3394
		private global::System.Windows.Forms.GroupBox gBCommands;

		// Token: 0x04000D43 RID: 3395
		private global::System.Windows.Forms.TextBox txtCommandsUsed;

		// Token: 0x04000D44 RID: 3396
		private global::System.Windows.Forms.ComboBox mode;

		// Token: 0x04000D45 RID: 3397
		private global::System.Windows.Forms.GroupBox gBChat;

		// Token: 0x04000D46 RID: 3398
		private global::System.Windows.Forms.TextBox txtLog;

		// Token: 0x04000D47 RID: 3399
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000D48 RID: 3400
		private global::System.Windows.Forms.TextBox txtCommands;

		// Token: 0x04000D49 RID: 3401
		private global::System.Windows.Forms.TextBox txtInput;

		// Token: 0x04000D4A RID: 3402
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000D4B RID: 3403
		private global::System.Windows.Forms.TextBox txtUrl;

		// Token: 0x04000D4C RID: 3404
		private global::System.Windows.Forms.TabPage chatTab;

		// Token: 0x04000D4D RID: 3405
		private global::System.Windows.Forms.TabPage playersTab;

		// Token: 0x04000D4E RID: 3406
		private global::System.Windows.Forms.Button button8;

		// Token: 0x04000D4F RID: 3407
		private global::System.Windows.Forms.Button button7;

		// Token: 0x04000D50 RID: 3408
		private global::System.Windows.Forms.Button button6;

		// Token: 0x04000D51 RID: 3409
		private global::System.Windows.Forms.Button btnMute;

		// Token: 0x04000D52 RID: 3410
		private global::System.Windows.Forms.Button button4;

		// Token: 0x04000D53 RID: 3411
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000D54 RID: 3412
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000D55 RID: 3413
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000D56 RID: 3414
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000D57 RID: 3415
		private global::System.Windows.Forms.PropertyGrid playersGrid;

		// Token: 0x04000D58 RID: 3416
		private global::System.Windows.Forms.TabPage mapsTab;

		// Token: 0x04000D59 RID: 3417
		private global::System.Windows.Forms.Button button13;

		// Token: 0x04000D5A RID: 3418
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000D5B RID: 3419
		private global::System.Windows.Forms.ListBox unloadedMapsList;

		// Token: 0x04000D5C RID: 3420
		private global::System.Windows.Forms.Button button12;

		// Token: 0x04000D5D RID: 3421
		private global::System.Windows.Forms.Button button11;

		// Token: 0x04000D5E RID: 3422
		private global::System.Windows.Forms.Button button10;

		// Token: 0x04000D5F RID: 3423
		private global::System.Windows.Forms.Button btnCreateMap;

		// Token: 0x04000D60 RID: 3424
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000D61 RID: 3425
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000D62 RID: 3426
		private global::System.Windows.Forms.PropertyGrid allMapsGrid;

		// Token: 0x04000D63 RID: 3427
		private global::System.Windows.Forms.ListBox mapsList;

		// Token: 0x04000D64 RID: 3428
		private global::System.Windows.Forms.TabPage lavaTab;

		// Token: 0x04000D65 RID: 3429
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000D66 RID: 3430
		private global::MCDzienny.Misc.DataGridViewEnumerated mapsGrid;

		// Token: 0x04000D67 RID: 3431
		private global::System.Windows.Forms.DataGridViewTextBoxColumn mapName;

		// Token: 0x04000D68 RID: 3432
		private global::System.Windows.Forms.DataGridViewTextBoxColumn sourceX;

		// Token: 0x04000D69 RID: 3433
		private global::System.Windows.Forms.DataGridViewTextBoxColumn sourceY;

		// Token: 0x04000D6A RID: 3434
		private global::System.Windows.Forms.DataGridViewTextBoxColumn sourceZ;

		// Token: 0x04000D6B RID: 3435
		private global::System.Windows.Forms.DataGridViewTextBoxColumn phase1;

		// Token: 0x04000D6C RID: 3436
		private global::System.Windows.Forms.DataGridViewTextBoxColumn phase2;

		// Token: 0x04000D6D RID: 3437
		private global::System.Windows.Forms.DataGridViewTextBoxColumn typeOfLava;

        // Token: 0x04000D6E RID: 3438

		// Token: 0x04000D6F RID: 3439
		private global::System.Windows.Forms.TabPage changelogTab;

		// Token: 0x04000D70 RID: 3440
		private global::System.Windows.Forms.TextBox txtChangelog;

		// Token: 0x04000D71 RID: 3441
		private global::System.Windows.Forms.TabPage errorsTab;

		// Token: 0x04000D72 RID: 3442
		private global::System.Windows.Forms.TextBox txtErrors;

		// Token: 0x04000D73 RID: 3443
		private global::System.Windows.Forms.Button minimizeButton;

		// Token: 0x04000D74 RID: 3444
		private global::System.Windows.Forms.Button btnProperties;

		// Token: 0x04000D75 RID: 3445
		private global::System.Windows.Forms.FontDialog fontDialog1;

		// Token: 0x04000D76 RID: 3446
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000D77 RID: 3447
		private global::System.Windows.Forms.TextBox titleText;

		// Token: 0x04000D78 RID: 3448
		private global::System.Windows.Forms.TextBox xbanText;

		// Token: 0x04000D79 RID: 3449
		private global::System.Windows.Forms.TextBox banText;

		// Token: 0x04000D7A RID: 3450
		private global::System.Windows.Forms.TextBox kickText;

		// Token: 0x04000D7B RID: 3451
		private global::System.Windows.Forms.ListView playersListView;

		// Token: 0x04000D7C RID: 3452
		private global::System.Windows.Forms.ColumnHeader PlName;

		// Token: 0x04000D7D RID: 3453
		private global::System.Windows.Forms.ColumnHeader PlRank;

		// Token: 0x04000D7E RID: 3454
		private global::System.Windows.Forms.ColumnHeader PlMap;

		// Token: 0x04000D7F RID: 3455
		private global::System.Windows.Forms.ComboBox playerColorCombo;

		// Token: 0x04000D80 RID: 3456
		private global::System.Windows.Forms.ComboBox targetMapCombo;

		// Token: 0x04000D81 RID: 3457
		private global::System.Windows.Forms.Button button15;

		// Token: 0x04000D82 RID: 3458
		private global::System.Windows.Forms.Button button14;

		// Token: 0x04000D83 RID: 3459
		private global::System.Windows.Forms.CheckBox xbanCheck;

		// Token: 0x04000D84 RID: 3460
		private global::System.Windows.Forms.CheckBox banCheck;

		// Token: 0x04000D85 RID: 3461
		private global::System.Windows.Forms.CheckBox kickCheck;

		// Token: 0x04000D86 RID: 3462
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x04000D87 RID: 3463
		private global::System.Windows.Forms.SplitContainer splitContainer2;

		// Token: 0x04000D88 RID: 3464
		private global::System.Windows.Forms.SplitContainer splitContainer3;

		// Token: 0x04000D89 RID: 3465
		private global::MCDzienny.CustomListView listViewPlayers;

		// Token: 0x04000D8A RID: 3466
		private global::System.Windows.Forms.ColumnHeader PlayersColumnName;

		// Token: 0x04000D8B RID: 3467
		private global::System.Windows.Forms.ColumnHeader PlayersColumnMap;

		// Token: 0x04000D8C RID: 3468
		private global::System.Windows.Forms.ColumnHeader PlayersColumnAfk;

		// Token: 0x04000D8D RID: 3469
		private global::MCDzienny.CustomListView listViewMaps;

		// Token: 0x04000D8E RID: 3470
		private global::System.Windows.Forms.ColumnHeader mapColumnName;

		// Token: 0x04000D8F RID: 3471
		private global::System.Windows.Forms.ColumnHeader mapColumnPhysics;

		// Token: 0x04000D90 RID: 3472
		private global::System.Windows.Forms.ColumnHeader mapColumnPlayers;

		// Token: 0x04000D91 RID: 3473
		private global::System.Windows.Forms.ColumnHeader mapColumnWeight;

		// Token: 0x04000D92 RID: 3474
		private global::System.Windows.Forms.SplitContainer splitContainer5;

		// Token: 0x04000D93 RID: 3475
		private global::System.Windows.Forms.SplitContainer splitContainer4;

		// Token: 0x04000D94 RID: 3476
		private global::System.Windows.Forms.Button button5;

		// Token: 0x04000D95 RID: 3477
		private global::System.Windows.Forms.StatusStrip statusStrip1;

		// Token: 0x04000D96 RID: 3478
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUptime;

		// Token: 0x04000D97 RID: 3479
		internal global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLagometer;

		// Token: 0x04000D98 RID: 3480
		internal global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRoundTime;

		// Token: 0x04000D99 RID: 3481
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x04000D9A RID: 3482
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x04000D9B RID: 3483
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x04000D9C RID: 3484
		private global::System.Windows.Forms.TextBox txtSystem;

        // Token: 0x04000D9D RID: 3485

        // Token: 0x04000D9E RID: 3486

        // Token: 0x04000D9F RID: 3487

        // Token: 0x04000DA0 RID: 3488

        // Token: 0x04000DA1 RID: 3489

        // Token: 0x04000DA2 RID: 3490

        // Token: 0x04000DA3 RID: 3491

        // Token: 0x04000DA4 RID: 3492

        // Token: 0x04000DA5 RID: 3493

        // Token: 0x04000DA6 RID: 3494

        // Token: 0x04000DA7 RID: 3495

        // Token: 0x04000DA8 RID: 3496

        // Token: 0x04000DA9 RID: 3497

        // Token: 0x04000DAA RID: 3498

        // Token: 0x04000DAB RID: 3499

        // Token: 0x04000DAC RID: 3500

        // Token: 0x04000DAD RID: 3501

		// Token: 0x04000DAE RID: 3502
		private global::System.Windows.Forms.ListBox chatPlayerList;

		// Token: 0x04000DAF RID: 3503
		private global::System.Windows.Forms.RichTextBox chatMainBox;

		// Token: 0x04000DB0 RID: 3504
		private global::System.Windows.Forms.Button chatOnOff_btn;

		// Token: 0x04000DB1 RID: 3505
		private global::System.Windows.Forms.TextBox chatInputBox;

		// Token: 0x04000DB2 RID: 3506
		private global::System.Windows.Forms.Label cBlack;

		// Token: 0x04000DB3 RID: 3507
		private global::System.Windows.Forms.Label cWhite;

		// Token: 0x04000DB4 RID: 3508
		private global::System.Windows.Forms.Label cDarkBlue;

		// Token: 0x04000DB5 RID: 3509
		private global::System.Windows.Forms.Label cYellow;

		// Token: 0x04000DB6 RID: 3510
		private global::System.Windows.Forms.Label cDarkGreen;

		// Token: 0x04000DB7 RID: 3511
		private global::System.Windows.Forms.Label cPink;

		// Token: 0x04000DB8 RID: 3512
		private global::System.Windows.Forms.Label cTeal;

		// Token: 0x04000DB9 RID: 3513
		private global::System.Windows.Forms.Label cRed;

		// Token: 0x04000DBA RID: 3514
		private global::System.Windows.Forms.Label cDarkRed;

		// Token: 0x04000DBB RID: 3515
		private global::System.Windows.Forms.Label cAqua;

		// Token: 0x04000DBC RID: 3516
		private global::System.Windows.Forms.Label cPurple;

		// Token: 0x04000DBD RID: 3517
		private global::System.Windows.Forms.Label cBrightGreen;

		// Token: 0x04000DBE RID: 3518
		private global::System.Windows.Forms.Label cGold;

		// Token: 0x04000DBF RID: 3519
		private global::System.Windows.Forms.Label cBlue;

		// Token: 0x04000DC0 RID: 3520
		private global::System.Windows.Forms.Label cGray;

		// Token: 0x04000DC1 RID: 3521
		private global::System.Windows.Forms.Label cDarkGray;

		// Token: 0x04000DC2 RID: 3522
		private global::System.Windows.Forms.Label label25;

		// Token: 0x04000DC3 RID: 3523
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000DC4 RID: 3524
		private global::System.Windows.Forms.Label chatWarningLabel;

		// Token: 0x04000DC5 RID: 3525
		private global::System.Windows.Forms.TabPage tabPagePlugins;

		// Token: 0x04000DC6 RID: 3526
		private global::System.Windows.Forms.Panel pnlPlugin;

		// Token: 0x04000DC7 RID: 3527
		private global::System.Windows.Forms.GroupBox groupBox4;

		// Token: 0x04000DC8 RID: 3528
		private global::System.Windows.Forms.Label lblPluginDesc;

		// Token: 0x04000DC9 RID: 3529
		private global::System.Windows.Forms.Label lblPluginAuthor;

		// Token: 0x04000DCA RID: 3530
		private global::System.Windows.Forms.Label lblPluginVersion;

		// Token: 0x04000DCB RID: 3531
		private global::System.Windows.Forms.Label lblPluginName;

		// Token: 0x04000DCC RID: 3532
		private global::System.Windows.Forms.TreeView treeView1;
	}
}
