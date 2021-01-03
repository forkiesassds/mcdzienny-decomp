namespace MCDzienny.StoreSystem
{
    // Token: 0x02000235 RID: 565
    internal interface ICommand
    {
        // Token: 0x170005E8 RID: 1512
        // (get) Token: 0x0600107C RID: 4220
        string CmdName { get; }

        // Token: 0x170005E9 RID: 1513
        // (get) Token: 0x0600107D RID: 4221
        string CmdShortcut { get; }

        // Token: 0x170005EA RID: 1514
        // (get) Token: 0x0600107E RID: 4222
        CommandScope CmdScope { get; }

        // Token: 0x0600107F RID: 4223
        void Use(Player p, string message);

        // Token: 0x06001080 RID: 4224
        void Help(Player p);
    }
}