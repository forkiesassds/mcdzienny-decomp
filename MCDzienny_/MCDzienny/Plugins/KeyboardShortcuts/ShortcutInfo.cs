using System.Xml.Serialization;

namespace MCDzienny.Plugins.KeyboardShortcuts
{
    // Token: 0x02000049 RID: 73
    public class ShortcutInfo
    {
        // Token: 0x1700006D RID: 109
        // (get) Token: 0x060001AA RID: 426 RVA: 0x00009A08 File Offset: 0x00007C08
        // (set) Token: 0x060001AB RID: 427 RVA: 0x00009A10 File Offset: 0x00007C10
        [XmlAttribute] public string Command { get; set; }

        // Token: 0x1700006E RID: 110
        // (get) Token: 0x060001AC RID: 428 RVA: 0x00009A1C File Offset: 0x00007C1C
        // (set) Token: 0x060001AD RID: 429 RVA: 0x00009A24 File Offset: 0x00007C24
        [XmlAttribute] public string Shortcut { get; set; }
    }
}