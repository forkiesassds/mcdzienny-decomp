using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020001A3 RID: 419
    public class CommandActionPair
    {
        // Token: 0x0400064E RID: 1614
        internal List<ActionElement> blockActions;

        // Token: 0x0400064D RID: 1613
        internal List<CommandElement> blockCommands;

        // Token: 0x0400064C RID: 1612
        internal ChangeAction changeAction;

        // Token: 0x06000C29 RID: 3113 RVA: 0x00047300 File Offset: 0x00045500
        public CommandActionPair()
        {
        }

        // Token: 0x06000C2A RID: 3114 RVA: 0x00047308 File Offset: 0x00045508
        public CommandActionPair(ChangeAction changeAction, List<CommandElement> blockCommands,
            List<ActionElement> blockActions)
        {
            this.blockCommands = blockCommands;
            this.blockActions = blockActions;
            this.changeAction = changeAction;
        }
    }
}