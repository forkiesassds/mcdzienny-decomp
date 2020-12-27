namespace MCDzienny
{
    // Token: 0x020001A7 RID: 423
    public class ExtendedDrawArgs : BasicDrawArgs
    {
        // Token: 0x06000C32 RID: 3122 RVA: 0x00047410 File Offset: 0x00045610
        public ExtendedDrawArgs(byte type1, int integer, params int[] integers) : base(type1, integer)
        {
            Integers = integers;
        }

        // Token: 0x06000C33 RID: 3123 RVA: 0x00047424 File Offset: 0x00045624
        public ExtendedDrawArgs(byte type1, byte type2, int integer, params int[] integers) : base(type1, type2,
            integer)
        {
            Integers = integers;
        }

        // Token: 0x170004CB RID: 1227
        // (get) Token: 0x06000C30 RID: 3120 RVA: 0x000473FC File Offset: 0x000455FC
        // (set) Token: 0x06000C31 RID: 3121 RVA: 0x00047404 File Offset: 0x00045604
        public int[] Integers { get; set; }
    }
}