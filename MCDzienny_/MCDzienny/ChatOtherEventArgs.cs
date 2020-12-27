using System;

namespace MCDzienny
{
    // Token: 0x02000168 RID: 360
    public class ChatOtherEventArgs : EventArgs
    {
        // Token: 0x06000A32 RID: 2610 RVA: 0x00035964 File Offset: 0x00033B64
        public ChatOtherEventArgs(string message, Player from, Player to, ChatType chatType)
        {
            Message = message;
            From = from;
            To = to;
            ChatType = chatType;
        }

        // Token: 0x1700047F RID: 1151
        // (get) Token: 0x06000A28 RID: 2600 RVA: 0x00035900 File Offset: 0x00033B00
        // (set) Token: 0x06000A29 RID: 2601 RVA: 0x00035908 File Offset: 0x00033B08
        public Player From { get; set; }

        // Token: 0x17000480 RID: 1152
        // (get) Token: 0x06000A2A RID: 2602 RVA: 0x00035914 File Offset: 0x00033B14
        // (set) Token: 0x06000A2B RID: 2603 RVA: 0x0003591C File Offset: 0x00033B1C
        public Player To { get; set; }

        // Token: 0x17000481 RID: 1153
        // (get) Token: 0x06000A2C RID: 2604 RVA: 0x00035928 File Offset: 0x00033B28
        // (set) Token: 0x06000A2D RID: 2605 RVA: 0x00035930 File Offset: 0x00033B30
        public bool Handled { get; set; }

        // Token: 0x17000482 RID: 1154
        // (get) Token: 0x06000A2E RID: 2606 RVA: 0x0003593C File Offset: 0x00033B3C
        // (set) Token: 0x06000A2F RID: 2607 RVA: 0x00035944 File Offset: 0x00033B44
        public ChatType ChatType { get; set; }

        // Token: 0x17000483 RID: 1155
        // (get) Token: 0x06000A30 RID: 2608 RVA: 0x00035950 File Offset: 0x00033B50
        // (set) Token: 0x06000A31 RID: 2609 RVA: 0x00035958 File Offset: 0x00033B58
        public string Message { get; set; }
    }
}