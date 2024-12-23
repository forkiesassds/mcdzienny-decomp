using System.Collections.Generic;
using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    [XmlRoot("settings")]
    public class LevelInfoRaw
    {

        public LevelInfoRaw()
        {
            Items = new List<LevelInfoRawItem>();
        }

        [XmlArrayItem("i")]
        [XmlArray("items")]
        public List<LevelInfoRawItem> Items { get; set; }
    }
}