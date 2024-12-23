using System;

namespace MCDzienny
{
    [Flags]
    public enum BlockTrigger
    {
        None = 0,
        Hit = 1,
        Build = 2,
        Walk = 4
    }
}