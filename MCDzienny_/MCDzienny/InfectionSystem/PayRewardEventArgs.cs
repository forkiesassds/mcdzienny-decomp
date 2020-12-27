using System;
using System.Collections.Generic;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x020000A5 RID: 165
    public class PayRewardEventArgs : EventArgs
    {
        // Token: 0x1700013B RID: 315
        // (get) Token: 0x0600046B RID: 1131 RVA: 0x00019EE8 File Offset: 0x000180E8
        // (set) Token: 0x0600046C RID: 1132 RVA: 0x00019EF0 File Offset: 0x000180F0
        public List<Player> NotInfected { get; set; }

        // Token: 0x1700013C RID: 316
        // (get) Token: 0x0600046D RID: 1133 RVA: 0x00019EFC File Offset: 0x000180FC
        // (set) Token: 0x0600046E RID: 1134 RVA: 0x00019F04 File Offset: 0x00018104
        public List<Player> Infected { get; set; }

        // Token: 0x1700013D RID: 317
        // (get) Token: 0x0600046F RID: 1135 RVA: 0x00019F10 File Offset: 0x00018110
        // (set) Token: 0x06000470 RID: 1136 RVA: 0x00019F18 File Offset: 0x00018118
        public Level CurrentInfectionLevel { get; set; }
    }
}