namespace MCDzienny
{
    // Token: 0x02000070 RID: 112
    public class CmdReferee : Command
    {
        // Token: 0x170000C1 RID: 193
        // (get) Token: 0x060002DB RID: 731 RVA: 0x00010088 File Offset: 0x0000E288
        public override string name
        {
            get { return "referee"; }
        }

        // Token: 0x170000C2 RID: 194
        // (get) Token: 0x060002DC RID: 732 RVA: 0x00010090 File Offset: 0x0000E290
        public override string shortcut
        {
            get { return "ref"; }
        }

        // Token: 0x170000C3 RID: 195
        // (get) Token: 0x060002DD RID: 733 RVA: 0x00010098 File Offset: 0x0000E298
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000C4 RID: 196
        // (get) Token: 0x060002DE RID: 734 RVA: 0x000100A0 File Offset: 0x0000E2A0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000C5 RID: 197
        // (get) Token: 0x060002DF RID: 735 RVA: 0x000100A4 File Offset: 0x0000E2A4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x170000C6 RID: 198
        // (get) Token: 0x060002E0 RID: 736 RVA: 0x000100A8 File Offset: 0x0000E2A8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170000C7 RID: 199
        // (get) Token: 0x060002E1 RID: 737 RVA: 0x000100AC File Offset: 0x0000E2AC
        public override CommandScope Scope
        {
            get { return CommandScope.Zombie; }
        }

        // Token: 0x060002E3 RID: 739 RVA: 0x000100B8 File Offset: 0x0000E2B8
        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message != "") player = Player.Find(message);
            if (player == null) player = p;
            if (!player.IsRefree)
            {
                player.IsRefree = true;
                if (InfectionSystem.InfectionSystem.infected.Contains(player))
                    InfectionSystem.InfectionSystem.infected.Remove(player);
                if (InfectionSystem.InfectionSystem.notInfected.Contains(player))
                    InfectionSystem.InfectionSystem.notInfected.Remove(player);
                InfectionSystem.InfectionSystem.RemoveZombieDataAndSkin(player);
                if (!player.hidden) all.Find("hide").Use(p, "s");
                Player.SendMessage(player, "You are a referee now.");
                Player.GlobalMessageLevel(player.level,
                    player.color + player.PublicName + Server.DefaultColor + " referees this round.");
                return;
            }

            Player.GlobalDie(player, false);
            player.IsRefree = false;
            if (InfectionSystem.InfectionSystem.RoundTime > 0.0) Player.GlobalSpawn(player);
            if (player.hidden) all.Find("hide").Use(p, "s");
            Player.SendMessage(player, "You aren't a referee anymore.");
            Player.GlobalMessageLevel(player.level,
                player.color + player.PublicName + Server.DefaultColor + " stopped being a referee.");
        }

        // Token: 0x060002E4 RID: 740 RVA: 0x000101F8 File Offset: 0x0000E3F8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/referee - toggles a refree status.");
            Player.SendMessage(p, "/referee [player] - toggles a refree status for a [player].");
        }
    }
}