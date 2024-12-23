namespace MCDzienny
{
    public class CmdJail : Command
    {
        public override string name { get { return "jail"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if ((message.ToLower() == "create" || message.ToLower() == "") && p != null)
            {
                p.level.jailx = p.pos[0];
                p.level.jaily = p.pos[1];
                p.level.jailz = p.pos[2];
                p.level.jailrotx = p.rot[0];
                p.level.jailroty = p.rot[1];
                Player.SendMessage(p, "Set Jail point.");
                return;
            }
            Player player = Player.Find(message);
            if (player != null)
            {
                if (!player.jailed)
                {
                    if (p != null)
                    {
                        if (player.group.Permission >= p.group.Permission)
                        {
                            Player.SendMessage(p, "Cannot jail someone of equal or greater rank.");
                            return;
                        }
                        if (player.level != p.level)
                        {
                            all.Find("goto").Use(player, p.level.name);
                        }
                        Player.GlobalDie(player, self: false);
                        Player.GlobalSpawn(player, p.level.jailx, p.level.jaily, p.level.jailz, p.level.jailrotx, p.level.jailroty, self: true);
                    }
                    else
                    {
                        Player.GlobalDie(player, self: false);
                        Player.GlobalSpawn(player, player.level.jailx, player.level.jaily, player.level.jailz, player.level.jailrotx, player.level.jailroty, self: true);
                    }
                    player.jailed = true;
                    Player.GlobalChat(null, string.Format("{0} was &8jailed", player.color + player.PublicName + Server.DefaultColor), showname: false);
                }
                else
                {
                    player.jailed = false;
                    Player.GlobalChat(null, string.Format("{0} was &afreed{1} from jail", player.color + player.PublicName + Server.DefaultColor, Server.DefaultColor), showname: false);
                }
            }
            else
            {
                Player.SendMessage(p, "Could not find specified player.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/jail [user] - Places [user] in jail unable to use commands.");
            Player.SendMessage(p, "/jail [create] - Creates the jail point for the map.");
        }
    }
}