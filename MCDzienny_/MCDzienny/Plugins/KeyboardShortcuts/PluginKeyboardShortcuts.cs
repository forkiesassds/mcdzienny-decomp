using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;
using MCDzienny.Cpe;
using MCDzienny.Plugins.KeyboardShortcuts;

namespace MCDzienny.Plugins
{
    class PluginKeyboardShortcuts : Plugin
    {
        public static readonly string ShortcutsPath = "extra/shortcuts.xml";

        readonly PluginKeyboardShortcutsGui gui = new PluginKeyboardShortcutsGui();

        readonly PluginKeyboardShortcutsGui2 mainControl;

        public PluginKeyboardShortcuts()
        {
            //IL_0034: Unknown result type (might be due to invalid IL or missing references)
            //IL_003e: Expected O, but got Unknown
            mainControl = gui.keyboardShortcuts;
            mainControl.applyButton.Click += new RoutedEventHandler(applyButton_Click);
            LoadShorcutsFromXml();
        }

        public override string Description
        {
            get
            {
                return "A list of keybord shortcuts that work for CPE clients. The shortcuts are sent to players when they join, or when the \"Apply\" button is pressed.";
            }
        }

        public override string Author { get { return "Dzienny"; } }

        public override string Name { get { return "Keyboard Shortcuts"; } }

        public override System.Windows.Forms.UserControl MainInterface { get { return gui; } }

        public override string Version { get { return "1.0"; } }

        public override int VersionNumber { get { return 1; } }

        public override void Initialize() {}

        public IEnumerable<ShortcutInfo> GetShortcuts()
        {
            return mainControl.keyboardShortcuts.Items.Cast<ShortcutInfo>();
        }

        public void SetShortcuts(IEnumerable<ShortcutInfo> shortcuts)
        {
            IList items = mainControl.keyboardShortcuts.Items;
            items.Clear();
            foreach (ShortcutInfo shortcut in shortcuts)
            {
                items.Add(shortcut);
            }
        }

        public override void Terminate()
        {
            mainControl.Dispatcher.Invoke((Action)delegate
            {
                mainControl.RemoveEmptyShortcuts();
                SaveShortcutsToXml();
            });
        }

        void applyButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.RemoveEmptyShortcuts();
            SaveShortcutsToXml();
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.Cpe.TextHotKey == 1)
                {
                    V1.SetTextHotKey(p, "", "", 0, 0);
                }
            });
            foreach (ShortcutInfo si in mainControl.keyboardShortcuts.Items)
            {
                if (si.Command.Length > 64 || !ServerProperties.ValidString(si.Command, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
                {
                    continue;
                }
                CpeHotKeyInfo key = WpfToLwjglKeyMap.ToCpeHotKey(si.Shortcut);
                Player.players.ForEachSync(delegate(Player p)
                {
                    if (p.Cpe.TextHotKey == 1)
                    {
                        V1.SetTextHotKey(p, "", si.Command, key.KeyCode, key.KeyMod);
                    }
                });
            }
        }

        void LoadShorcutsFromXml()
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Expected O, but got Unknown
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_001a: Expected O, but got Unknown
            try
            {
                XmlSerializer val = new XmlSerializer(typeof(List<ShortcutInfo>), new XmlRootAttribute("Shortcuts"));
                XmlReader val2 = XmlReader.Create(ShortcutsPath);
                List<ShortcutInfo> list;
                try
                {
                    list = (List<ShortcutInfo>)val.Deserialize(val2);
                }
                finally
                {
                    ((IDisposable)val2).Dispose();
                }
                if (list == null)
                {
                    return;
                }
                foreach (ShortcutInfo item in list)
                {
                    mainControl.keyboardShortcuts.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void SaveShortcutsToXml()
        {
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            //IL_0032: Expected O, but got Unknown
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0033: Expected O, but got Unknown
            //IL_0038: Unknown result type (might be due to invalid IL or missing references)
            //IL_003f: Expected O, but got Unknown
            try
            {
                ItemCollection items = mainControl.keyboardShortcuts.Items;
                var list = items.Cast<ShortcutInfo>().ToList();
                XmlSerializer val = new XmlSerializer(list.GetType(), new XmlRootAttribute("Shortcuts"));
                string shortcutsPath = ShortcutsPath;
                XmlWriterSettings val2 = new XmlWriterSettings();
                val2.Encoding = Encoding.UTF8;
                val2.Indent = true;
                XmlWriter val3 = XmlWriter.Create(shortcutsPath, val2);
                try
                {
                    val.Serialize(val3, list);
                }
                finally
                {
                    ((IDisposable)val3).Dispose();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }
    }
}