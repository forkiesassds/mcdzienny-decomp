using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    [XmlRoot("i")]
    public class LevelInfoRawItem
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}