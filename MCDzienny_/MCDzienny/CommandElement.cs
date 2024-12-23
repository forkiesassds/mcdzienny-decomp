using System;

namespace MCDzienny
{
    public class CommandElement
    {
        internal ValueExplicitPair<BlockTrigger> blockTrigger;

        internal string commandString;

        internal ValueExplicitPair<bool> consoleUse;

        internal ValueExplicitPair<float> cooldown;

        internal DateTime lastUsed = new DateTime(0L);

        internal CommandElement() {}

        internal CommandElement(ValueExplicitPair<BlockTrigger> blockTrigger, ValueExplicitPair<bool> consoleUse, ValueExplicitPair<float> cooldown, string commandString)
        {
            this.blockTrigger = blockTrigger;
            this.consoleUse = consoleUse;
            this.cooldown = cooldown;
            this.commandString = commandString;
        }
    }
}