using System;

namespace MCDzienny
{
    // Token: 0x020001A6 RID: 422
    public class CommandElement
    {
        // Token: 0x04000656 RID: 1622
        internal ValueExplicitPair<BlockTrigger> blockTrigger;

        // Token: 0x04000659 RID: 1625
        internal string commandString;

        // Token: 0x04000657 RID: 1623
        internal ValueExplicitPair<bool> consoleUse;

        // Token: 0x04000658 RID: 1624
        internal ValueExplicitPair<float> cooldown;

        // Token: 0x0400065A RID: 1626
        internal DateTime lastUsed = new DateTime(0L);

        // Token: 0x06000C2E RID: 3118 RVA: 0x000473B0 File Offset: 0x000455B0
        internal CommandElement()
        {
        }

        // Token: 0x06000C2F RID: 3119 RVA: 0x000473C8 File Offset: 0x000455C8
        internal CommandElement(ValueExplicitPair<BlockTrigger> blockTrigger, ValueExplicitPair<bool> consoleUse,
            ValueExplicitPair<float> cooldown, string commandString)
        {
            this.blockTrigger = blockTrigger;
            this.consoleUse = consoleUse;
            this.cooldown = cooldown;
            this.commandString = commandString;
        }
    }
}