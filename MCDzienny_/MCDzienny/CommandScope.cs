using System;

namespace MCDzienny
{
    // Token: 0x020002B1 RID: 689
    [Flags]
    public enum CommandScope
    {
        // Token: 0x04000963 RID: 2403
        Freebuild = 1,

        // Token: 0x04000964 RID: 2404
        Lava = 2,

        // Token: 0x04000965 RID: 2405
        Zombie = 4,

        // Token: 0x04000966 RID: 2406
        Home = 8,

        // Token: 0x04000967 RID: 2407
        TntWars = 16,

        // Token: 0x04000968 RID: 2408
        MyMap = 32,

        // Token: 0x04000969 RID: 2409
        All = 63
    }
}