namespace MCDzienny.ActionScripts
{
    // Token: 0x02000002 RID: 2
    public abstract class Action
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000002 RID: 2
        // (set) Token: 0x06000003 RID: 3
        public abstract string Name { get; set; }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000004 RID: 4
        // (set) Token: 0x06000005 RID: 5
        public abstract string Description { get; set; }

        // Token: 0x06000006 RID: 6
        public abstract void DoAction(Player p, string arguments, int blockX, int blockY, int blockZ);
    }
}