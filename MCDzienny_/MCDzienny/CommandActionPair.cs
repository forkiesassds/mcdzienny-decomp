using System.Collections.Generic;

namespace MCDzienny
{
    public class CommandActionPair
    {

        internal List<ActionElement> blockActions;

        internal List<CommandElement> blockCommands;
        internal ChangeAction changeAction;

        public CommandActionPair() {}

        public CommandActionPair(ChangeAction changeAction, List<CommandElement> blockCommands, List<ActionElement> blockActions)
        {
            this.blockCommands = blockCommands;
            this.blockActions = blockActions;
            this.changeAction = changeAction;
        }
    }
}