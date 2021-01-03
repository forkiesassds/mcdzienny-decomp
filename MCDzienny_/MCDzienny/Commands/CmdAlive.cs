using System.Text;

namespace MCDzienny
{
    // Token: 0x020000B4 RID: 180
    public class CmdAlive : Command
    {
        // Token: 0x1700029C RID: 668
        // (get) Token: 0x0600061F RID: 1567 RVA: 0x00020CB0 File Offset: 0x0001EEB0
        public override string name
        {
            get { return "alive"; }
        }

        // Token: 0x1700029D RID: 669
        // (get) Token: 0x06000620 RID: 1568 RVA: 0x00020CB8 File Offset: 0x0001EEB8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700029E RID: 670
        // (get) Token: 0x06000621 RID: 1569 RVA: 0x00020CC0 File Offset: 0x0001EEC0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700029F RID: 671
        // (get) Token: 0x06000622 RID: 1570 RVA: 0x00020CC8 File Offset: 0x0001EEC8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002A0 RID: 672
        // (get) Token: 0x06000623 RID: 1571 RVA: 0x00020CCC File Offset: 0x0001EECC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170002A1 RID: 673
        // (get) Token: 0x06000624 RID: 1572 RVA: 0x00020CD0 File Offset: 0x0001EED0
        public override string CustomName
        {
            get { return Lang.Command.AliveName; }
        }

        // Token: 0x170002A2 RID: 674
        // (get) Token: 0x06000625 RID: 1573 RVA: 0x00020CD8 File Offset: 0x0001EED8
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x06000627 RID: 1575 RVA: 0x00020CE4 File Offset: 0x0001EEE4
        public override void Use(Player p, string message)
        {
            if (p != null && p.level.mapType == MapType.Zombie)
            {
                all.Find("humans").Use(p, "");
                return;
            }

            var alivePlayers = new StringBuilder();
            Player.SendMessage(p, Lang.Command.AliveMessage);
            Player.players.ForEachSync(delegate(Player who)
            {
                if (who.lives > 0 && !who.hidden && !who.invincible) alivePlayers.Append("%c" + who.PublicName + ", ");
            });
            Player.SendMessage(p, alivePlayers.ToString().TrimEnd().TrimEnd(','));
        }

        // Token: 0x06000628 RID: 1576 RVA: 0x00020D80 File Offset: 0x0001EF80
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AliveHelp);
        }
    }
}