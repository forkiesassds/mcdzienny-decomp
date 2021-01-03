namespace MCDzienny
{
    // Token: 0x0200029F RID: 671
    public class CmdStore : Command
    {
        // Token: 0x1700073C RID: 1852
        // (get) Token: 0x06001334 RID: 4916 RVA: 0x00069F8C File Offset: 0x0006818C
        public override string name
        {
            get { return "store"; }
        }

        // Token: 0x1700073D RID: 1853
        // (get) Token: 0x06001335 RID: 4917 RVA: 0x00069F94 File Offset: 0x00068194
        public override string shortcut
        {
            get { return "shop"; }
        }

        // Token: 0x1700073E RID: 1854
        // (get) Token: 0x06001336 RID: 4918 RVA: 0x00069F9C File Offset: 0x0006819C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700073F RID: 1855
        // (get) Token: 0x06001337 RID: 4919 RVA: 0x00069FA4 File Offset: 0x000681A4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000740 RID: 1856
        // (get) Token: 0x06001338 RID: 4920 RVA: 0x00069FA8 File Offset: 0x000681A8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000741 RID: 1857
        // (get) Token: 0x06001339 RID: 4921 RVA: 0x00069FAC File Offset: 0x000681AC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000742 RID: 1858
        // (get) Token: 0x0600133A RID: 4922 RVA: 0x00069FB0 File Offset: 0x000681B0
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x0600133B RID: 4923 RVA: 0x00069FB4 File Offset: 0x000681B4
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Zombie)
            {
                StoreSystem.Store.ZombieStore.DisplayItems(p, 0);
                return;
            }

            Player.SendMessage(p, "You can buy:");
            if (message.ToLower() == "more")
            {
                Store.PrintListMore(p);
                return;
            }

            Store.PrintList(p);
        }

        // Token: 0x0600133C RID: 4924 RVA: 0x0006A008 File Offset: 0x00068208
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/store - shows you the list of the stuff available.");
        }
    }
}