namespace MCDzienny
{
    // Token: 0x020000B0 RID: 176
    public class BasicDrawArgs : DrawArgs
    {
        // Token: 0x060005E9 RID: 1513 RVA: 0x0001C114 File Offset: 0x0001A314
        public BasicDrawArgs(byte type1) : this(type1, byte.MaxValue, 0)
        {
        }

        // Token: 0x060005EA RID: 1514 RVA: 0x0001C124 File Offset: 0x0001A324
        public BasicDrawArgs(byte type1, byte type2) : this(type1, type2, 0)
        {
        }

        // Token: 0x060005EB RID: 1515 RVA: 0x0001C130 File Offset: 0x0001A330
        public BasicDrawArgs(byte type1, int number1) : this(type1, byte.MaxValue, number1)
        {
        }

        // Token: 0x060005EC RID: 1516 RVA: 0x0001C140 File Offset: 0x0001A340
        public BasicDrawArgs(byte type1, byte type2, int number1)
        {
            Type1 = type1;
            Type2 = type2;
            Integer = number1;
        }

        // Token: 0x17000299 RID: 665
        // (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
        // (set) Token: 0x060005E4 RID: 1508 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
        public byte Type1 { get; set; }

        // Token: 0x1700029A RID: 666
        // (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001C0EC File Offset: 0x0001A2EC
        // (set) Token: 0x060005E6 RID: 1510 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
        public byte Type2 { get; set; }

        // Token: 0x1700029B RID: 667
        // (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001C100 File Offset: 0x0001A300
        // (set) Token: 0x060005E8 RID: 1512 RVA: 0x0001C108 File Offset: 0x0001A308
        public int Integer { get; set; }
    }
}