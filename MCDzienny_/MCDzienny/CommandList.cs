using System.Collections;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002B2 RID: 690
    public sealed class CommandList : IEnumerable<Command>, IEnumerable
    {
        // Token: 0x0400096A RID: 2410
        public List<Command> commands = new List<Command>();

        // Token: 0x1700078E RID: 1934
        // (get) Token: 0x060013C0 RID: 5056 RVA: 0x0006CEB0 File Offset: 0x0006B0B0
        public int Count
        {
            get { return commands.Count; }
        }

        // Token: 0x060013C1 RID: 5057 RVA: 0x0006CEC0 File Offset: 0x0006B0C0
        IEnumerator IEnumerable.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        // Token: 0x060013C2 RID: 5058 RVA: 0x0006CED4 File Offset: 0x0006B0D4
        IEnumerator<Command> IEnumerable<Command>.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        // Token: 0x060013C3 RID: 5059 RVA: 0x0006CEE8 File Offset: 0x0006B0E8
        public void Sort()
        {
            commands.Sort(SortByName);
        }

        // Token: 0x060013C4 RID: 5060 RVA: 0x0006CF04 File Offset: 0x0006B104
        public static int SortByName(Command x, Command y)
        {
            return string.Compare(x.name, y.name);
        }

        // Token: 0x060013C5 RID: 5061 RVA: 0x0006CF18 File Offset: 0x0006B118
        public void Add(Command cmd)
        {
            commands.Add(cmd);
        }

        // Token: 0x060013C6 RID: 5062 RVA: 0x0006CF28 File Offset: 0x0006B128
        public void AddRange(List<Command> listCommands)
        {
            listCommands.ForEach(delegate(Command cmd) { commands.Add(cmd); });
        }

        // Token: 0x060013C7 RID: 5063 RVA: 0x0006CF3C File Offset: 0x0006B13C
        public List<string> commandNames()
        {
            var tempList = new List<string>();
            commands.ForEach(delegate(Command cmd) { tempList.Add(cmd.name); });
            return tempList;
        }

        // Token: 0x060013C8 RID: 5064 RVA: 0x0006CF78 File Offset: 0x0006B178
        public bool Remove(Command cmd)
        {
            return commands.Remove(cmd);
        }

        // Token: 0x060013C9 RID: 5065 RVA: 0x0006CF88 File Offset: 0x0006B188
        public bool Contains(Command cmd)
        {
            return commands.Contains(cmd);
        }

        // Token: 0x060013CA RID: 5066 RVA: 0x0006CF98 File Offset: 0x0006B198
        public bool Contains(string name)
        {
            name = name.ToLower();
            foreach (var command in commands)
                if (command.name == name.ToLower())
                    return true;
            return false;
        }

        // Token: 0x060013CB RID: 5067 RVA: 0x0006D008 File Offset: 0x0006B208
        public Command Find(string name)
        {
            name = name.ToLower();
            foreach (var command in commands)
                if (command.name == name.ToLower() || command.shortcut == name.ToLower())
                    return command;
            return null;
        }

        // Token: 0x060013CC RID: 5068 RVA: 0x0006D08C File Offset: 0x0006B28C
        public string FindShort(string shortcut)
        {
            if (shortcut == "") return "";
            shortcut = shortcut.ToLower();
            foreach (var command in commands)
                if (command.shortcut == shortcut)
                    return command.name;
            return "";
        }

        // Token: 0x060013CD RID: 5069 RVA: 0x0006D114 File Offset: 0x0006B314
        public List<Command> All()
        {
            return new List<Command>(commands);
        }
    }
}