using System.Collections;
using System.Collections.Generic;

namespace MCDzienny
{
    public sealed class CommandList : IEnumerable<Command>, IEnumerable
    {
        public List<Command> commands = new List<Command>();

        public int Count { get { return commands.Count; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        IEnumerator<Command> IEnumerable<Command>.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        public void Sort()
        {
            commands.Sort(SortByName);
        }

        public static int SortByName(Command x, Command y)
        {
            return string.Compare(x.name, y.name);
        }

        public void Add(Command cmd)
        {
            commands.Add(cmd);
        }

        public void AddRange(List<Command> listCommands)
        {
            listCommands.ForEach(delegate(Command cmd) { commands.Add(cmd); });
        }

        public List<string> commandNames()
        {
            var tempList = new List<string>();
            commands.ForEach(delegate(Command cmd) { tempList.Add(cmd.name); });
            return tempList;
        }

        public bool Remove(Command cmd)
        {
            return commands.Remove(cmd);
        }

        public bool Contains(Command cmd)
        {
            return commands.Contains(cmd);
        }

        public bool Contains(string name)
        {
            name = name.ToLower();
            foreach (Command command in commands)
            {
                if (command.name == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public Command Find(string name)
        {
            name = name.ToLower();
            foreach (Command command in commands)
            {
                if (command.name == name.ToLower() || command.shortcut == name.ToLower())
                {
                    return command;
                }
            }
            return null;
        }

        public string FindShort(string shortcut)
        {
            if (shortcut == "")
            {
                return "";
            }
            shortcut = shortcut.ToLower();
            foreach (Command command in commands)
            {
                if (command.shortcut == shortcut)
                {
                    return command.name;
                }
            }
            return "";
        }

        public List<Command> All()
        {
            return new List<Command>(commands);
        }
    }
}