using System.Text;

namespace MCDzienny
{
    // Token: 0x02000082 RID: 130
    public class CmdZombies : Command
    {
        // Token: 0x17000107 RID: 263
        // (get) Token: 0x0600036F RID: 879 RVA: 0x000127B0 File Offset: 0x000109B0
        public override string name
        {
            get { return "zombies"; }
        }

        // Token: 0x17000108 RID: 264
        // (get) Token: 0x06000370 RID: 880 RVA: 0x000127B8 File Offset: 0x000109B8
        public override string shortcut
        {
            get { return "zombie"; }
        }

        // Token: 0x17000109 RID: 265
        // (get) Token: 0x06000371 RID: 881 RVA: 0x000127C0 File Offset: 0x000109C0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700010A RID: 266
        // (get) Token: 0x06000372 RID: 882 RVA: 0x000127C8 File Offset: 0x000109C8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700010B RID: 267
        // (get) Token: 0x06000373 RID: 883 RVA: 0x000127CC File Offset: 0x000109CC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700010C RID: 268
        // (get) Token: 0x06000374 RID: 884 RVA: 0x000127D0 File Offset: 0x000109D0
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x06000375 RID: 885 RVA: 0x000127D4 File Offset: 0x000109D4
        public override void Use(Player p, string message)
        {
            var sb = new StringBuilder();
            sb.Append("%c");
            InfectionSystem.InfectionSystem.infected.ForEach(delegate(Player pl)
            {
                sb.Append(pl.PublicName).Append(", ");
            });
            if (sb.Length > 0) sb.Remove(sb.Length - 2, 2);
            Player.SendMessage(p, "%cZombies:");
            Player.SendMessage(p, sb.ToString());
        }

        // Token: 0x06000376 RID: 886 RVA: 0x00012860 File Offset: 0x00010A60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zombies - displays list of zombies.");
        }
    }
}