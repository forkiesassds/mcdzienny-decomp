using System.Collections.Generic;
using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    // Token: 0x02000039 RID: 57
    [XmlRoot("settings")]
    public class LevelInfoRaw
    {
        // Token: 0x06000147 RID: 327 RVA: 0x00008610 File Offset: 0x00006810
        public LevelInfoRaw()
        {
            Items = new List<LevelInfoRawItem>();
        }

        // Token: 0x17000053 RID: 83
        // (get) Token: 0x06000145 RID: 325 RVA: 0x000085FC File Offset: 0x000067FC
        // (set) Token: 0x06000146 RID: 326 RVA: 0x00008604 File Offset: 0x00006804
        [XmlArrayItem("i")]
        [XmlArray("items")]
        public List<LevelInfoRawItem> Items { get; set; }
    }
}