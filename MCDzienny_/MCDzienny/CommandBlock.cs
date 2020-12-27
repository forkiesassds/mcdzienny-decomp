using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020001A4 RID: 420
    public class CommandBlock
    {
        // Token: 0x04000654 RID: 1620
        internal List<ActionElement> actionElements = new List<ActionElement>();

        // Token: 0x04000652 RID: 1618
        internal string blockType;

        // Token: 0x04000655 RID: 1621
        internal ValueExplicitPair<ChangeAction> changeAction;

        // Token: 0x04000653 RID: 1619
        internal List<CommandElement> commandElements = new List<CommandElement>();

        // Token: 0x0400064F RID: 1615
        internal int x;

        // Token: 0x04000650 RID: 1616
        internal int y;

        // Token: 0x04000651 RID: 1617
        internal int z;

        // Token: 0x06000C2B RID: 3115 RVA: 0x00047328 File Offset: 0x00045528
        public CommandBlock()
        {
        }

        // Token: 0x06000C2C RID: 3116 RVA: 0x00047348 File Offset: 0x00045548
        public CommandBlock(int x, int y, int z, string blockType, ValueExplicitPair<ChangeAction> changeAction,
            List<CommandElement> commandElements, List<ActionElement> actionElements)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.blockType = blockType;
            this.changeAction = changeAction;
            this.commandElements = commandElements;
            this.actionElements = actionElements;
        }
    }
}