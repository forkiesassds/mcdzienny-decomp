namespace MCDzienny.StoreSystem
{
    interface ICommand
    {
        string CmdName { get; }

        string CmdShortcut { get; }

        CommandScope CmdScope { get; }

        void Use(Player p, string message);

        void Help(Player p);
    }
}