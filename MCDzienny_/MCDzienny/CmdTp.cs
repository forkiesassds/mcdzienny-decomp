using System.Threading;

namespace MCDzienny
{
    public class CmdTp : Command
    {
        public override string name { get { return "tp"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                all.Find("spawn");
                return;
            }
            if (p.group.Permission < LevelPermission.Operator && (p.level.mapType == MapType.Lava || p.level.mapType == MapType.Zombie) && !p.hasTeleport)
            {
                Player.SendMessage(p, "You don't have teleport. You have to buy it in /store first");
                return;
            }
            Player player = Player.Find(message);
            if (player == null || player.hidden && p.group.Permission < LevelPermission.Admin)
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
                    Player.SendMessage(p, string.Format("Waiting for {0} to spawn...", player.color + player.PublicName + Server.DefaultColor));
                    while (player.Loading)
                    {
                        Thread.Sleep(1000);
                    }
                }
                while (p.Loading)
                {
                    Thread.Sleep(1000);
                }
                p.SendPos(byte.MaxValue, player.pos[0], player.pos[1], player.pos[2], player.rot[0], 0);
            }
            p.hasTeleport = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tp <player> - Teleports yourself to a player.");
            Player.SendMessage(p, "If <player> is blank, /spawn is used.");
        }
    }
}