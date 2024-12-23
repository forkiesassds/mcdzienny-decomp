using System.Collections.Generic;

namespace MCDzienny
{
    public class CommandBlock
    {

        internal List<ActionElement> actionElements = new List<ActionElement>();

        internal string blockType;

        internal ValueExplicitPair<ChangeAction> changeAction;

        internal List<CommandElement> commandElements = new List<CommandElement>();
        internal int x;

        internal int y;

        internal int z;

        public CommandBlock() {}

        public CommandBlock(int x, int y, int z, string blockType, ValueExplicitPair<ChangeAction> changeAction, List<CommandElement> commandElements,
                            List<ActionElement> actionElements)
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