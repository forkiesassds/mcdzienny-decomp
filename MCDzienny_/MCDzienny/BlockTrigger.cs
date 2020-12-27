using System;

namespace MCDzienny
{
    // Token: 0x020001A0 RID: 416
    [Flags]
    public enum BlockTrigger
    {
        // Token: 0x0400063F RID: 1599
        None = 0,

        // Token: 0x04000640 RID: 1600
        Hit = 1,

        // Token: 0x04000641 RID: 1601
        Build = 2,

        // Token: 0x04000642 RID: 1602
        Walk = 4
    }
}