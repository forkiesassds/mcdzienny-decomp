using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration; 

namespace MCDzienny.Plugins
{
    public class PluginKeyboardShortcutsGui : UserControl
    {

        IContainer components;

        ElementHost elementHost1;
        public PluginKeyboardShortcutsGui2 keyboardShortcuts;

        PluginKeyboardShortcutsGui2 pluginKeyboardShortcutsGui21;

        ElementHost wpfHost;

        public PluginKeyboardShortcutsGui()
        {
            InitializeComponent();
            ElementHost val = wpfHost;
            keyboardShortcuts = (PluginKeyboardShortcutsGui2)val.Child;
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
            elementHost1 = new ElementHost();
            wpfHost = new ElementHost();
            pluginKeyboardShortcutsGui21 = new PluginKeyboardShortcutsGui2();
            pluginKeyboardShortcutsGui21.InitializeComponent();
            SuspendLayout();
            elementHost1.Location = new System.Drawing.Point(14, 16);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(514, 286);
            elementHost1.TabIndex = 0;
            elementHost1.Text = "elementHost1";
            elementHost1.Child = null;
            wpfHost.Anchor = (AnchorStyles)15;
            wpfHost.Location = new System.Drawing.Point(14, 16);
            wpfHost.Name = "wpfHost";
            wpfHost.Size = new System.Drawing.Size(514, 286);
            wpfHost.TabIndex = 1;
            wpfHost.Text = "wpfHost";
            wpfHost.Child = (UIElement)(object)pluginKeyboardShortcutsGui21;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            Controls.Add((Control)(object)wpfHost);
            Controls.Add((Control)(object)elementHost1);
            Name = "PluginKeyboardShortcutsGui";
            Size = new System.Drawing.Size(540, 319);
            ResumeLayout(false);
        }
    }
}