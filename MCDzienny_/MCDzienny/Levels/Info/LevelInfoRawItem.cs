using System.Xml.Serialization;

namespace MCDzienny.Levels.Info
{
    // Token: 0x0200003B RID: 59
    [XmlRoot("i")]
    public class LevelInfoRawItem
    {
        // Token: 0x17000054 RID: 84
        // (get) Token: 0x0600014B RID: 331 RVA: 0x00008798 File Offset: 0x00006998
        // (set) Token: 0x0600014C RID: 332 RVA: 0x000087A0 File Offset: 0x000069A0
        [XmlAttribute("key")] public string Key { get; set; }

        // Token: 0x17000055 RID: 85
        // (get) Token: 0x0600014D RID: 333 RVA: 0x000087AC File Offset: 0x000069AC
        // (set) Token: 0x0600014E RID: 334 RVA: 0x000087B4 File Offset: 0x000069B4
        [XmlAttribute("value")] public string Value { get; set; }
    }
}