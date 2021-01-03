using System;

namespace MCDzienny
{
    // Token: 0x0200033C RID: 828
    public class PlayerJoinedEventArgs : EventArgs
    {
        // Token: 0x06001803 RID: 6147 RVA: 0x000A0C38 File Offset: 0x0009EE38
        public PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }

        // Token: 0x170008E9 RID: 2281
        // (get) Token: 0x06001801 RID: 6145 RVA: 0x000A0C24 File Offset: 0x0009EE24
        // (set) Token: 0x06001802 RID: 6146 RVA: 0x000A0C2C File Offset: 0x0009EE2C
        public Player Player { get; set; }
    }
}