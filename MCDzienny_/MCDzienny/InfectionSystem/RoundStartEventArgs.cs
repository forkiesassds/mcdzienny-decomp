using System;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x020000A7 RID: 167
    public class RoundStartEventArgs : EventArgs
    {
        // Token: 0x17000141 RID: 321
        // (get) Token: 0x06000479 RID: 1145 RVA: 0x00019F70 File Offset: 0x00018170
        // (set) Token: 0x0600047A RID: 1146 RVA: 0x00019F78 File Offset: 0x00018178
        public Level CurrentInfectionLevel { get; set; }
    }
}