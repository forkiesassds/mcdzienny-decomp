using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCDzienny.Plugins;

namespace MCDzienny
{
    // Token: 0x020001EC RID: 492
    public class PluginControlRemovePlugin : UserControl
    {
        // Token: 0x0400072B RID: 1835
        private Button buttonAddPlugin;

        // Token: 0x0400072D RID: 1837
        private Container components;

        // Token: 0x0400072A RID: 1834
        private Label label1;

        // Token: 0x0400072C RID: 1836
        private ListBox listBoxPlugins;

        // Token: 0x06000D9C RID: 3484 RVA: 0x0004D374 File Offset: 0x0004B574
        public PluginControlRemovePlugin()
        {
            InitializeComponent();
            LoadPluginsList();
            Server.Plugins.AvailablePlugins.CollectionChanged += AvailablePlugins_CollectionChanged;
        }

        // Token: 0x06000D9D RID: 3485 RVA: 0x0004D3A4 File Offset: 0x0004B5A4
        private void AvailablePlugins_CollectionChanged(object sender, EventArgs e)
        {
            LoadPluginsList();
        }

        // Token: 0x06000D9E RID: 3486 RVA: 0x0004D3AC File Offset: 0x0004B5AC
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x06000D9F RID: 3487 RVA: 0x0004D3CC File Offset: 0x0004B5CC
        private void InitializeComponent()
        {
            label1 = new Label();
            buttonAddPlugin = new Button();
            listBoxPlugins = new ListBox();
            SuspendLayout();
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(165, 12);
            label1.Name = "label1";
            label1.Size = new Size(153, 13);
            label1.TabIndex = 3;
            label1.Text = "Select a plugin for the removal:";
            buttonAddPlugin.Anchor = AnchorStyles.Bottom;
            buttonAddPlugin.Location = new Point(166, 342);
            buttonAddPlugin.Name = "buttonAddPlugin";
            buttonAddPlugin.Size = new Size(150, 23);
            buttonAddPlugin.TabIndex = 4;
            buttonAddPlugin.Text = "Remove Plugin";
            buttonAddPlugin.UseVisualStyleBackColor = true;
            buttonAddPlugin.Click += buttonRemovePlugin_Click;
            listBoxPlugins.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            listBoxPlugins.FormattingEnabled = true;
            listBoxPlugins.Location = new Point(166, 33);
            listBoxPlugins.Name = "listBoxPlugins";
            listBoxPlugins.Size = new Size(150, 303);
            listBoxPlugins.TabIndex = 6;
            BackColor = Color.White;
            Controls.Add(listBoxPlugins);
            Controls.Add(buttonAddPlugin);
            Controls.Add(label1);
            Name = "PluginControlRemovePlugin";
            Size = new Size(488, 376);
            Load += ctlMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        // Token: 0x06000DA0 RID: 3488 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
        private void ctlMain_Load(object sender, EventArgs e)
        {
        }

        // Token: 0x06000DA1 RID: 3489 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
        private void LoadPluginsList()
        {
            listBoxPlugins.Items.Clear();
            Server.Plugins.AvailablePlugins.ForEach(delegate(AvailablePlugin p)
            {
                if (!p.IsCore) listBoxPlugins.Items.Add(p.Instance.Name);
            });
        }

        // Token: 0x06000DA2 RID: 3490 RVA: 0x0004D614 File Offset: 0x0004B814
        private void buttonRemovePlugin_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxPlugins.SelectedItem;
            if (selectedItem != null) Server.Plugins.RemovePluginByName(selectedItem.ToString());
        }
    }
}