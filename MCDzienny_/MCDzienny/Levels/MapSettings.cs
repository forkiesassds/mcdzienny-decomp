using System.Collections.Generic;
using System.Xml.Serialization;

namespace MCDzienny.Levels
{
    [XmlRoot("MapSettings", Namespace = "", IsNullable = false)]
    public class MapSettings
    {
        [XmlArray("CommandBlocks", IsNullable = true)]
        [XmlArrayItem(typeof(CommandBlock))]
        public List<CommandBlock> commandBlocks;
    }
}