using System;

namespace MCDzienny
{
    // Token: 0x020001A9 RID: 425
    public class LevelEventArgs : EventArgs
    {
        // Token: 0x0400065E RID: 1630
        public Level level;

        // Token: 0x06000C44 RID: 3140 RVA: 0x000475C0 File Offset: 0x000457C0
        public LevelEventArgs(Level level)
        {
            this.level = level;
        }
    }
}