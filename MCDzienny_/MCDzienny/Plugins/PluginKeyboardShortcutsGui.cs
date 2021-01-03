using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MCDzienny.Plugins
{
    // Token: 0x02000046 RID: 70
    public class PluginKeyboardShortcutsGui : UserControl
    {
        // Token: 0x040000EA RID: 234
        private IContainer components;

        // Token: 0x040000EB RID: 235
        private ElementHost elementHost1;

        // Token: 0x040000E9 RID: 233
        public PluginKeyboardShortcutsGui2 keyboardShortcuts;

        // Token: 0x040000ED RID: 237
        private PluginKeyboardShortcutsGui2 pluginKeyboardShortcutsGui21;

        // Token: 0x040000EC RID: 236
        private ElementHost wpfHost;

        // Token: 0x06000189 RID: 393 RVA: 0x00008F6C File Offset: 0x0000716C
        public PluginKeyboardShortcutsGui()
        {
            InitializeComponent();
            var elementHost = wpfHost;
            keyboardShortcuts = (PluginKeyboardShortcutsGui2) elementHost.Child;
        }

        // Token: 0x0600018A RID: 394 RVA: 0x00008FA0 File Offset: 0x000071A0
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x0600018B RID: 395 RVA: 0x00008FC0 File Offset: 0x000071C0
        private void InitializeComponent()
        {
            elementHost1 = new ElementHost();
            wpfHost = new ElementHost();
            pluginKeyboardShortcutsGui21 = new PluginKeyboardShortcutsGui2();
            SuspendLayout();
            elementHost1.Location = new Point(14, 16);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new Size(514, 286);
            elementHost1.TabIndex = 0;
            elementHost1.Text = "elementHost1";
            elementHost1.Child = null;
            wpfHost.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wpfHost.Location = new Point(14, 16);
            wpfHost.Name = "wpfHost";
            wpfHost.Size = new Size(514, 286);
            wpfHost.TabIndex = 1;
            wpfHost.Text = "wpfHost";
            wpfHost.Child = pluginKeyboardShortcutsGui21;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(wpfHost);
            Controls.Add(elementHost1);
            Name = "PluginKeyboardShortcutsGui";
            Size = new Size(540, 319);
            ResumeLayout(false);
        }
    }
}