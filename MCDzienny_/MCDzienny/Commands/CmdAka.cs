using System;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000059 RID: 89
    internal class CmdAka : Command
    {
        // Token: 0x17000077 RID: 119
        // (get) Token: 0x06000213 RID: 531 RVA: 0x0000C54C File Offset: 0x0000A74C
        public override string name
        {
            get { return "aka"; }
        }

        // Token: 0x17000078 RID: 120
        // (get) Token: 0x06000214 RID: 532 RVA: 0x0000C554 File Offset: 0x0000A754
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000079 RID: 121
        // (get) Token: 0x06000215 RID: 533 RVA: 0x0000C55C File Offset: 0x0000A75C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700007A RID: 122
        // (get) Token: 0x06000216 RID: 534 RVA: 0x0000C564 File Offset: 0x0000A764
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700007B RID: 123
        // (get) Token: 0x06000217 RID: 535 RVA: 0x0000C568 File Offset: 0x0000A768
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700007C RID: 124
        // (get) Token: 0x06000218 RID: 536 RVA: 0x0000C56C File Offset: 0x0000A76C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x1700007D RID: 125
        // (get) Token: 0x06000219 RID: 537 RVA: 0x0000C570 File Offset: 0x0000A770
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x0600021A RID: 538 RVA: 0x0000C574 File Offset: 0x0000A774
        public override void Use(Player p, string message)
        {
            Func<Player, string> getName = null;
            if (p.ShowAlias)
            {
                getName = GetTrueName;
                p.ShowAlias = false;
            }
            else
            {
                getName = GetMaskedName;
                p.ShowAlias = true;
            }

            p.DespawnAll();
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (p.level == pl.level && p != pl && !pl.hidden) p.SendSpawn(pl, getName(pl));
            });
        }

        // Token: 0x0600021B RID: 539 RVA: 0x0000C604 File Offset: 0x0000A804
        private string GetTrueName(Player p)
        {
            return (p.isZombie ? "&c" : "") + p.PublicName;
        }

        // Token: 0x0600021C RID: 540 RVA: 0x0000C628 File Offset: 0x0000A828
        private string GetMaskedName(Player p)
        {
            if (!p.isZombie) return p.color + (p.IsRefree ? "[REF]" : "") + p.PublicName;
            return InfectionSettings.All.ZombieAlias;
        }

        // Token: 0x0600021D RID: 541 RVA: 0x0000C664 File Offset: 0x0000A864
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/aka - switches between players' real name and temporary aliases.");
        }
    }
}