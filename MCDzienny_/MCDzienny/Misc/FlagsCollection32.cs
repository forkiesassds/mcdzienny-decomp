namespace MCDzienny.Misc
{
    // Token: 0x020001BA RID: 442
    public class FlagsCollection32
    {
        // Token: 0x0400067E RID: 1662

        // Token: 0x06000C89 RID: 3209 RVA: 0x00048C14 File Offset: 0x00046E14
        public FlagsCollection32()
        {
        }

        // Token: 0x06000C8A RID: 3210 RVA: 0x00048C1C File Offset: 0x00046E1C
        public FlagsCollection32(int flagContainer)
        {
            this.FlagContainer = flagContainer;
        }

        // Token: 0x170004E0 RID: 1248
        // (get) Token: 0x06000C87 RID: 3207 RVA: 0x00048C00 File Offset: 0x00046E00
        // (set) Token: 0x06000C88 RID: 3208 RVA: 0x00048C08 File Offset: 0x00046E08
        public int FlagContainer { get; set; }

        // Token: 0x06000C8B RID: 3211 RVA: 0x00048C2C File Offset: 0x00046E2C
        public bool GetFlag(int pos)
        {
            return (FlagContainer & (1 << pos)) == 1 << pos;
        }

        // Token: 0x06000C8C RID: 3212 RVA: 0x00048C44 File Offset: 0x00046E44
        public void SetFlag(int pos, bool flag)
        {
            if (flag)
            {
                FlagContainer |= 1 << pos;
                return;
            }

            FlagContainer &= ~(1 << pos);
        }
    }
}