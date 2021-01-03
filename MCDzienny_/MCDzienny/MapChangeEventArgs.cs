using System;

namespace MCDzienny
{
    // Token: 0x0200003F RID: 63
    public class MapChangeEventArgs : EventArgs
    {
        // Token: 0x06000160 RID: 352 RVA: 0x000089DC File Offset: 0x00006BDC
        public MapChangeEventArgs(Player player, Level from, Level to)
        {
            Player = player;
            From = from;
            To = to;
        }

        // Token: 0x17000057 RID: 87
        // (get) Token: 0x06000158 RID: 344 RVA: 0x0000898C File Offset: 0x00006B8C
        // (set) Token: 0x06000159 RID: 345 RVA: 0x00008994 File Offset: 0x00006B94
        public Player Player { get; private set; }

        // Token: 0x17000058 RID: 88
        // (get) Token: 0x0600015A RID: 346 RVA: 0x000089A0 File Offset: 0x00006BA0
        // (set) Token: 0x0600015B RID: 347 RVA: 0x000089A8 File Offset: 0x00006BA8
        public Level From { get; private set; }

        // Token: 0x17000059 RID: 89
        // (get) Token: 0x0600015C RID: 348 RVA: 0x000089B4 File Offset: 0x00006BB4
        // (set) Token: 0x0600015D RID: 349 RVA: 0x000089BC File Offset: 0x00006BBC
        public Level To { get; private set; }

        // Token: 0x1700005A RID: 90
        // (get) Token: 0x0600015E RID: 350 RVA: 0x000089C8 File Offset: 0x00006BC8
        // (set) Token: 0x0600015F RID: 351 RVA: 0x000089D0 File Offset: 0x00006BD0
        public bool Handled { get; set; }
    }
}