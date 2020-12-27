using System;
using System.Collections.Generic;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x020000A6 RID: 166
    public class AnnounceWinnersEventArgs : EventArgs
    {
        // Token: 0x1700013E RID: 318
        // (get) Token: 0x06000472 RID: 1138 RVA: 0x00019F2C File Offset: 0x0001812C
        // (set) Token: 0x06000473 RID: 1139 RVA: 0x00019F34 File Offset: 0x00018134
        public List<Player> NotInfected { get; set; }

        // Token: 0x1700013F RID: 319
        // (get) Token: 0x06000474 RID: 1140 RVA: 0x00019F40 File Offset: 0x00018140
        // (set) Token: 0x06000475 RID: 1141 RVA: 0x00019F48 File Offset: 0x00018148
        public List<Player> Infected { get; set; }

        // Token: 0x17000140 RID: 320
        // (get) Token: 0x06000476 RID: 1142 RVA: 0x00019F54 File Offset: 0x00018154
        // (set) Token: 0x06000477 RID: 1143 RVA: 0x00019F5C File Offset: 0x0001815C
        public Level CurrentInfectionLevel { get; set; }
    }
}