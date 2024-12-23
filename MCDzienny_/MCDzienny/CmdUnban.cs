namespace MCDzienny
{
    public class CmdUnban : Command
    {
        public override string name { get { return "unban"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            bool flag = false;
            if (message[0] == '@')
            {
                flag = true;
                message = message.Remove(0, 1).Trim();
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                if (Group.findPlayerGroup(message) != Group.findPerm(LevelPermission.Banned))
                {
                    foreach (Server.TempBan tempBan in Server.tempBans)
                    {
                        if (tempBan.name.ToLower() == message.ToLower())
                        {
                            Server.tempBans.Remove(tempBan);
                            Player.GlobalMessage(string.Format("{0} has had their temporary ban lifted.", message));
                            return;
                        }
                    }
                    Player.SendMessage(p, "Player is not banned.");
                    return;
                }
                Player.GlobalMessage(string.Format("{0} &8(banned){1} is now {2}!", message, Server.DefaultColor,
                                                   Group.standard.color + Group.standard.name + Server.DefaultColor));
                Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
            }
            else
            {
                if (Group.findPlayerGroup(message) != Group.findPerm(LevelPermission.Banned))
                {
                    foreach (Server.TempBan tempBan2 in Server.tempBans)
                    {
                        if (tempBan2.name == player.name)
                        {
                            Server.tempBans.Remove(tempBan2);
                            Player.GlobalMessage(player.color + player.prefix + player.PublicName + Server.DefaultColor + "has had their temporary ban lifted.");
                            return;
                        }
                    }
                    Player.SendMessage(p, "Player is not banned.");
                    return;
                }
                Player.GlobalChat(
                    player,
                    player.color + player.prefix + player.PublicName + Server.DefaultColor + " is now " + Group.standard.color + Group.standard.name +
                    Server.DefaultColor + "!", showname: false);
                player.group = Group.standard;
                player.color = player.group.color;
                Player.GlobalDie(player, self: false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
                Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
            }
            Group.findPerm(LevelPermission.Banned).playerList.Save();
            if (flag)
            {
                all.Find("unbanip").Use(p, "@" + message);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unban <player> - Unbans a player.  This includes temporary bans.");
        }
    }
}