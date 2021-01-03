using System.Text;

namespace MCDzienny
{
    // Token: 0x02000062 RID: 98
    public class CmdHumans : Command
    {
        // Token: 0x170000A8 RID: 168
        // (get) Token: 0x06000273 RID: 627 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
        public override string name
        {
            get { return "humans"; }
        }

        // Token: 0x170000A9 RID: 169
        // (get) Token: 0x06000274 RID: 628 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
        public override string shortcut
        {
            get { return "human"; }
        }

        // Token: 0x170000AA RID: 170
        // (get) Token: 0x06000275 RID: 629 RVA: 0x0000D900 File Offset: 0x0000BB00
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000AB RID: 171
        // (get) Token: 0x06000276 RID: 630 RVA: 0x0000D908 File Offset: 0x0000BB08
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000AC RID: 172
        // (get) Token: 0x06000277 RID: 631 RVA: 0x0000D90C File Offset: 0x0000BB0C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170000AD RID: 173
        // (get) Token: 0x06000278 RID: 632 RVA: 0x0000D910 File Offset: 0x0000BB10
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x06000279 RID: 633 RVA: 0x0000D914 File Offset: 0x0000BB14
        public override void Use(Player p, string message)
        {
            var sb = new StringBuilder();
            InfectionSystem.InfectionSystem.notInfected.ForEach(delegate(Player pl)
            {
                sb.Append(pl.PublicName).Append(", ");
            });
            if (sb.Length > 0) sb.Remove(sb.Length - 2, 2);
            Player.SendMessage(p, "%aHumans:");
            Player.SendMessage(p, sb.ToString());
        }

        // Token: 0x0600027A RID: 634 RVA: 0x0000D98C File Offset: 0x0000BB8C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/humans - displays list of humans.");
        }
    }
}