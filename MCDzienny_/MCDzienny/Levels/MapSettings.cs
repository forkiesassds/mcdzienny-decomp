using System.Collections.Generic;
using System.Xml.Serialization;

namespace MCDzienny.Levels
{
    // Token: 0x020001AA RID: 426
    [XmlRoot("MapSettings", Namespace = "", IsNullable = false)]
    public class MapSettings
    {
        // Token: 0x0400065F RID: 1631
        [XmlArray("CommandBlocks", IsNullable = true)] [XmlArrayItem(typeof(CommandBlock))]
        public List<CommandBlock> commandBlocks;
    }
}