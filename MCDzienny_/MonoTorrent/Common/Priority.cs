namespace MonoTorrent.Common
{
    // Token: 0x02000381 RID: 897
    public enum Priority
    {
        // Token: 0x04000E0B RID: 3595
        DoNotDownload,

        // Token: 0x04000E0C RID: 3596
        Lowest,

        // Token: 0x04000E0D RID: 3597
        Low,

        // Token: 0x04000E0E RID: 3598
        Normal = 4,

        // Token: 0x04000E0F RID: 3599
        High = 8,

        // Token: 0x04000E10 RID: 3600
        Highest = 16,

        // Token: 0x04000E11 RID: 3601
        Immediate = 32
    }
}