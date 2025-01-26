using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCDzienny.Plugins;

namespace MCDzienny
{
    public class PluginControlRemovePlugin : UserControl
    {

        Button buttonAddPlugin;

        Container components;
        Label label1;

        ListBox listBoxPlugins;

        public PluginControlRemovePlugin()
        {
            InitializeComponent();
            LoadPluginsList();
            Server.Plugins.AvailablePlugins.CollectionChanged += AvailablePlugins_CollectionChanged;
        }

        void AvailablePlugins_CollectionChanged(object sender, EventArgs e)
        {
            LoadPluginsList();
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
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_000b: Expected O, but got Unknown
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Expected O, but got Unknown
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Expected O, but got Unknown
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

        void ctlMain_Load(object sender, EventArgs e) {}

        void LoadPluginsList()
        {
            listBoxPlugins.Items.Clear();
            Server.Plugins.AvailablePlugins.ForEach(delegate(AvailablePlugin p)
            {
                if (!p.IsCore)
                {
                    listBoxPlugins.Items.Add(p.Instance.Name);
                }
            });
        }

        void buttonRemovePlugin_Click(object sender, EventArgs e)
        {
            object selectedItem = listBoxPlugins.SelectedItem;
            if (selectedItem != null)
            {
                Server.Plugins.RemovePluginByName(selectedItem.ToString());
            }
        }
    }
}