using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using MCDzienny.Cpe;
using MCDzienny.Plugins.KeyboardShortcuts;

namespace MCDzienny.Plugins
{
    // Token: 0x02000043 RID: 67
    internal class PluginKeyboardShortcuts : Plugin
    {
        // Token: 0x040000E2 RID: 226
        public static readonly string ShortcutsPath = "extra/shortcuts.xml";

        // Token: 0x040000E3 RID: 227
        private readonly PluginKeyboardShortcutsGui gui = new PluginKeyboardShortcutsGui();

        // Token: 0x040000E4 RID: 228
        private readonly PluginKeyboardShortcutsGui2 mainControl;

        // Token: 0x0600017B RID: 379 RVA: 0x00008B5C File Offset: 0x00006D5C
        public PluginKeyboardShortcuts()
        {
            mainControl = gui.keyboardShortcuts;
            mainControl.applyButton.Click += applyButton_Click;
            LoadShorcutsFromXml();
        }

        // Token: 0x17000065 RID: 101
        // (get) Token: 0x06000175 RID: 373 RVA: 0x00008B30 File Offset: 0x00006D30
        public override string Description
        {
            get
            {
                return
                    "A list of keybord shortcuts that work for CPE clients. The shortcuts are sent to players when they join, or when the \"Apply\" button is pressed.";
            }
        }

        // Token: 0x17000066 RID: 102
        // (get) Token: 0x06000176 RID: 374 RVA: 0x00008B38 File Offset: 0x00006D38
        public override string Author
        {
            get { return "Dzienny"; }
        }

        // Token: 0x17000067 RID: 103
        // (get) Token: 0x06000177 RID: 375 RVA: 0x00008B40 File Offset: 0x00006D40
        public override string Name
        {
            get { return "Keyboard Shortcuts"; }
        }

        // Token: 0x17000068 RID: 104
        // (get) Token: 0x06000178 RID: 376 RVA: 0x00008B48 File Offset: 0x00006D48
        public override UserControl MainInterface
        {
            get { return gui; }
        }

        // Token: 0x17000069 RID: 105
        // (get) Token: 0x06000179 RID: 377 RVA: 0x00008B50 File Offset: 0x00006D50
        public override string Version
        {
            get { return "1.0"; }
        }

        // Token: 0x1700006A RID: 106
        // (get) Token: 0x0600017A RID: 378 RVA: 0x00008B58 File Offset: 0x00006D58
        public override int VersionNumber
        {
            get { return 1; }
        }

        // Token: 0x0600017C RID: 380 RVA: 0x00008BB0 File Offset: 0x00006DB0
        public override void Initialize()
        {
        }

        // Token: 0x0600017D RID: 381 RVA: 0x00008BB4 File Offset: 0x00006DB4
        public IEnumerable<ShortcutInfo> GetShortcuts()
        {
            return mainControl.keyboardShortcuts.Items.Cast<ShortcutInfo>();
        }

        // Token: 0x0600017E RID: 382 RVA: 0x00008BCC File Offset: 0x00006DCC
        public void SetShortcuts(IEnumerable<ShortcutInfo> shortcuts)
        {
            var items = mainControl.keyboardShortcuts.Items;
            items.Clear();
            foreach (var newItem in shortcuts) items.Add(newItem);
        }

        // Token: 0x0600017F RID: 383 RVA: 0x00008C2C File Offset: 0x00006E2C
        public override void Terminate()
        {
            mainControl.Dispatcher.Invoke(new Action(delegate
            {
                mainControl.RemoveEmptyShortcuts();
                SaveShortcutsToXml();
            }));
        }

        // Token: 0x06000180 RID: 384 RVA: 0x00008C54 File Offset: 0x00006E54
        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.RemoveEmptyShortcuts();
            SaveShortcutsToXml();
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.Cpe.TextHotKey == 1) V1.SetTextHotKey(p, "", "", 0, 0);
            });
            foreach (ShortcutInfo si in mainControl.keyboardShortcuts.Items)
            {
                if (si.Command.Length > 64 ||
                    !ServerProperties.ValidString(si.Command, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& ")) continue;
                var key = WpfToLwjglKeyMap.ToCpeHotKey(si.Shortcut);
                Player.players.ForEachSync(delegate(Player p)
                {
                    if (p.Cpe.TextHotKey == 1) V1.SetTextHotKey(p, "", si.Command, key.KeyCode, key.KeyMod);
                });
            }
        }

        // Token: 0x06000181 RID: 385 RVA: 0x00008D5C File Offset: 0x00006F5C
        private void LoadShorcutsFromXml()
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ShortcutInfo>), new XmlRootAttribute("Shortcuts"));
                List<ShortcutInfo> list;
                using (var xmlReader = XmlReader.Create(ShortcutsPath))
                {
                    list = (List<ShortcutInfo>) xmlSerializer.Deserialize(xmlReader);
                }

                if (list != null)
                    foreach (var newItem in list)
                        mainControl.keyboardShortcuts.Items.Add(newItem);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000182 RID: 386 RVA: 0x00008E24 File Offset: 0x00007024
        private void SaveShortcutsToXml()
        {
            try
            {
                var items = mainControl.keyboardShortcuts.Items;
                var list = items.Cast<ShortcutInfo>().ToList();
                var xmlSerializer = new XmlSerializer(list.GetType(), new XmlRootAttribute("Shortcuts"));
                using (var xmlWriter = XmlWriter.Create(ShortcutsPath, new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true
                }))
                {
                    xmlSerializer.Serialize(xmlWriter, list);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }
    }
}