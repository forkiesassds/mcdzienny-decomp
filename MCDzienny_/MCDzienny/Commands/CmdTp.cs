using System.Threading;

namespace MCDzienny
{
    // Token: 0x020002A3 RID: 675
    public class CmdTp : Command
    {
        // Token: 0x17000755 RID: 1877
        // (get) Token: 0x06001359 RID: 4953 RVA: 0x0006A4C8 File Offset: 0x000686C8
        public override string name
        {
            get { return "tp"; }
        }

        // Token: 0x17000756 RID: 1878
        // (get) Token: 0x0600135A RID: 4954 RVA: 0x0006A4D0 File Offset: 0x000686D0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000757 RID: 1879
        // (get) Token: 0x0600135B RID: 4955 RVA: 0x0006A4D8 File Offset: 0x000686D8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000758 RID: 1880
        // (get) Token: 0x0600135C RID: 4956 RVA: 0x0006A4E0 File Offset: 0x000686E0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000759 RID: 1881
        // (get) Token: 0x0600135D RID: 4957 RVA: 0x0006A4E4 File Offset: 0x000686E4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x1700075A RID: 1882
        // (get) Token: 0x0600135E RID: 4958 RVA: 0x0006A4E8 File Offset: 0x000686E8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001360 RID: 4960 RVA: 0x0006A4F4 File Offset: 0x000686F4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                all.Find("spawn");
                return;
            }

            if (p.group.Permission < LevelPermission.Operator &&
                (p.level.mapType == MapType.Lava || p.level.mapType == MapType.Zombie) && !p.hasTeleport)
            {
                Player.SendMessage(p, "You don't have teleport. You have to buy it in /store first");
                return;
            }

            var player = Player.Find(message);
            if (player == null || player.hidden && p.@group.Permission < LevelPermission.Admin)
            {
                Player.SendMessage(p, string.Format("There is no player \"{0}\"!", message));
                return;
            }

            if (player == p)
            {
                Player.SendMessage(p, "Congratulations, you teleported to yourself.");
                return;
            }

            if (p.level != player.level)
            {
                if (player.level.name.Contains("cMuseum"))
                {
                    Player.SendMessage(p, string.Format("Player \"{0}\" is in a museum!", message));
                    return;
                }

                all.Find("goto").Use(p, player.level.name);
            }

            if (p.level == player.level)
            {
                if (player.Loading)
                {
                    Player.SendMessage(p,
                        string.Format("Waiting for {0} to spawn...",
                            player.color + player.PublicName + Server.DefaultColor));
                    while (player.Loading) Thread.Sleep(1000);
                }

                while (p.Loading) Thread.Sleep(1000);
                p.SendPos(byte.MaxValue, player.pos[0], player.pos[1], player.pos[2], player.rot[0], 0);
            }

            p.hasTeleport = false;
        }

        // Token: 0x06001361 RID: 4961 RVA: 0x0006A698 File Offset: 0x00068898
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tp <player> - Teleports yourself to a player.");
            Player.SendMessage(p, "If <player> is blank, /spawn is used.");
        }
    }
}