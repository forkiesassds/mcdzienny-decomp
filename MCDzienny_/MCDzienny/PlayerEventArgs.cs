using System;

namespace MCDzienny
{
    // Token: 0x020001DD RID: 477
    public class PlayerEventArgs : EventArgs
    {
        // Token: 0x06000D57 RID: 3415 RVA: 0x0004C4F0 File Offset: 0x0004A6F0
        public PlayerEventArgs(Player player)
        {
            Player = player;
        }

        // Token: 0x170004FD RID: 1277
        // (get) Token: 0x06000D55 RID: 3413 RVA: 0x0004C4DC File Offset: 0x0004A6DC
        // (set) Token: 0x06000D56 RID: 3414 RVA: 0x0004C4E4 File Offset: 0x0004A6E4
        public Player Player { get; set; }
    }
}