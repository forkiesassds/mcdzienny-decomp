using System;

namespace MCDzienny.Notification
{
    // Token: 0x020001D7 RID: 471
    public class Item
    {
        // Token: 0x040006D8 RID: 1752

        // Token: 0x040006D4 RID: 1748

        // Token: 0x040006D5 RID: 1749

        // Token: 0x040006D3 RID: 1747

        // Token: 0x040006D6 RID: 1750

        // Token: 0x040006D7 RID: 1751

        // Token: 0x06000D30 RID: 3376 RVA: 0x0004B9A0 File Offset: 0x00049BA0
        public Item(string id, string content, Priority priority) : this(id, content, priority, default(DateTime))
        {
        }

        // Token: 0x06000D31 RID: 3377 RVA: 0x0004B9C0 File Offset: 0x00049BC0
        public Item(string id, string content, Priority priority, DateTime expiration)
        {
            ID = id;
            Content = content;
            Priority = priority;
            Expiration = expiration;
        }

        // Token: 0x06000D32 RID: 3378 RVA: 0x0004B9E8 File Offset: 0x00049BE8
        public Item(string id, string content, Priority priority, string author, DateTime pubDate)
        {
            ID = id;
            Content = content;
            Priority = priority;
            PubDate = pubDate;
            Author = author;
        }

        // Token: 0x170004F5 RID: 1269
        // (get) Token: 0x06000D2A RID: 3370 RVA: 0x0004B970 File Offset: 0x00049B70
        public string ID { get; private set; }

        // Token: 0x170004F6 RID: 1270
        // (get) Token: 0x06000D2B RID: 3371 RVA: 0x0004B978 File Offset: 0x00049B78
        public string Content { get; private set; }

        // Token: 0x170004F7 RID: 1271
        // (get) Token: 0x06000D2C RID: 3372 RVA: 0x0004B980 File Offset: 0x00049B80
        public DateTime Expiration { get; private set; }

        // Token: 0x170004F8 RID: 1272
        // (get) Token: 0x06000D2D RID: 3373 RVA: 0x0004B988 File Offset: 0x00049B88
        public Priority Priority { get; private set; }

        // Token: 0x170004F9 RID: 1273
        // (get) Token: 0x06000D2E RID: 3374 RVA: 0x0004B990 File Offset: 0x00049B90
        public DateTime PubDate { get; private set; }

        // Token: 0x170004FA RID: 1274
        // (get) Token: 0x06000D2F RID: 3375 RVA: 0x0004B998 File Offset: 0x00049B98
        public string Author { get; private set; }
    }
}