namespace MCDzienny
{
    public class CmdSetRank : Command
    {
        public override string name { get { return "setrank"; } }

        public override string shortcut { get { return "rank"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            Group group = Group.Find(message.Split(' ')[1]);
            string text = message.Split(' ').Length <= 2 ? "Congratulations!" : message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1));
            if (group == null)
            {
                Player.SendMessage(p, "Could not find specified rank.");
                return;
            }
            Group group2 = Group.findPerm(LevelPermission.Banned);
            if (player == null)
            {
                string text2 = message.Split(' ')[0];
                if (Group.findPlayerGroup(text2) == group2 || group == group2)
                {
                    Player.SendMessage(p, string.Format("Cannot change the rank to or from \"{0}\".", group2.name));
                    return;
                }
                if (p != null && (Group.findPlayerGroup(text2).Permission >= p.group.Permission || group.Permission >= p.group.Permission))
                {
                    Player.SendMessage(p, "Cannot change the rank of someone equal or higher than you");
                    return;
                }
                Group group3 = Group.findPlayerGroup(text2);
                group3.playerList.Remove(text2);
                group3.playerList.Save();
                group.playerList.Add(text2);
                group.playerList.Save();
                Player.GlobalMessage(string.Format("{0} &f(offline){1}'s rank was set to {2}", text2, Server.DefaultColor, group.color + group.name));
            }
            else if (player == p)
            {
                Player.SendMessage(p, "Cannot change your own rank.");
            }
            else if (p != null && (player.group.Permission >= p.group.Permission || group.Permission >= p.group.Permission))
            {
                Player.SendMessage(p, "Cannot change the rank of someone equal or higher to yourself.");
            }
            else if (player.group == group2 || group == group2 || group.Permission >= LevelPermission.Nobody)
            {
                Player.SendMessage(p, string.Format("Cannot change the rank to or from \"{0}\".", group2.name));
            }
            else
            {
                player.group.playerList.Remove(player.name);
                player.group.playerList.Save();
                group.playerList.Add(player.name);
                group.playerList.Save();
                Player.GlobalChat(player, string.Format("{0}'s rank was set to {1}", player.color + player.PublicName + Server.DefaultColor, group.color + group.name), showname: false);
                Player.GlobalChat(null, "&6" + text, showname: false);
                player.group = group;
                player.color = player.group.color;
                Player.GlobalDie(player, self: false);
                player.SendMessage(string.Format("You are now ranked {0}, type /help for your new set of commands.", group.color + group.name + Server.DefaultColor));
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setrank <player> <rank> <yay> - Sets or returns a players rank.");
            Player.SendMessage(p, "You may use /rank as a shortcut");
            Player.SendMessage(p, "Valid Ranks are: " + Group.concatList(includeColor: true, skipExtra: true));
            Player.SendMessage(p, "<yay> is a celebratory message");
        }
    }
}