using System.Xml.Serialization;

namespace MCDzienny.Plugins.KeyboardShortcuts
{
    public class ShortcutInfo
    {
        [XmlAttribute]
        public string Command { get; set; }

        [XmlAttribute]
        public string Shortcut { get; set; }
    }
}