namespace MCDzienny
{
    // Token: 0x020000CB RID: 203
    public class CmdItems : Command
    {
        // Token: 0x170002ED RID: 749
        // (get) Token: 0x060006B5 RID: 1717 RVA: 0x00022CE0 File Offset: 0x00020EE0
        public override string name
        {
            get { return "items"; }
        }

        // Token: 0x170002EE RID: 750
        // (get) Token: 0x060006B6 RID: 1718 RVA: 0x00022CE8 File Offset: 0x00020EE8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002EF RID: 751
        // (get) Token: 0x060006B7 RID: 1719 RVA: 0x00022CF0 File Offset: 0x00020EF0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002F0 RID: 752
        // (get) Token: 0x060006B8 RID: 1720 RVA: 0x00022CF8 File Offset: 0x00020EF8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002F1 RID: 753
        // (get) Token: 0x060006B9 RID: 1721 RVA: 0x00022CFC File Offset: 0x00020EFC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170002F2 RID: 754
        // (get) Token: 0x060006BA RID: 1722 RVA: 0x00022D00 File Offset: 0x00020F00
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170002F3 RID: 755
        // (get) Token: 0x060006BB RID: 1723 RVA: 0x00022D04 File Offset: 0x00020F04
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x060006BD RID: 1725 RVA: 0x00022D10 File Offset: 0x00020F10
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "You have:");
            Player.SendMessage(p, string.Format("water blocks: {0},", p.waterBlocks));
            Player.SendMessage(p, string.Format("sponges: {0},", p.spongeBlocks));
            Player.SendMessage(p, string.Format("doors: {0},", p.doorBlocks));
            Player.SendMessage(p, string.Format("hammer: {0},", p.hammer));
            Player.SendMessage(p, string.Format("teleport: {0}.", p.hasTeleport ? 1 : 0));
        }

        // Token: 0x060006BE RID: 1726 RVA: 0x00022DB8 File Offset: 0x00020FB8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/items - shows your possesions.");
        }
    }
}