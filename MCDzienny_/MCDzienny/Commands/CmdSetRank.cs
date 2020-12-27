namespace MCDzienny
{
    // Token: 0x02000297 RID: 663
    public class CmdSetRank : Command
    {
        // Token: 0x17000720 RID: 1824
        // (get) Token: 0x060012FF RID: 4863 RVA: 0x00068B00 File Offset: 0x00066D00
        public override string name
        {
            get { return "setrank"; }
        }

        // Token: 0x17000721 RID: 1825
        // (get) Token: 0x06001300 RID: 4864 RVA: 0x00068B08 File Offset: 0x00066D08
        public override string shortcut
        {
            get { return "rank"; }
        }

        // Token: 0x17000722 RID: 1826
        // (get) Token: 0x06001301 RID: 4865 RVA: 0x00068B10 File Offset: 0x00066D10
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000723 RID: 1827
        // (get) Token: 0x06001302 RID: 4866 RVA: 0x00068B18 File Offset: 0x00066D18
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000724 RID: 1828
        // (get) Token: 0x06001303 RID: 4867 RVA: 0x00068B1C File Offset: 0x00066D1C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001304 RID: 4868 RVA: 0x00068B20 File Offset: 0x00066D20
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
            var group = Group.Find(message.Split(' ')[1]);
            string str;
            if (message.Split(' ').Length > 2)
                str = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1));
            else
                str = "Congratulations!";
            if (group == null)
            {
                Player.SendMessage(p, "Could not find specified rank.");
                return;
            }

            var group2 = Group.findPerm(LevelPermission.Banned);
            if (player == null)
            {
                var text = message.Split(' ')[0];
                if (Group.findPlayerGroup(text) == group2 || group == group2)
                {
                    Player.SendMessage(p, string.Format("Cannot change the rank to or from \"{0}\".", group2.name));
                    return;
                }

                if (p != null && (Group.findPlayerGroup(text).Permission >= p.group.Permission ||
                                  group.Permission >= p.group.Permission))
                {
                    Player.SendMessage(p, "Cannot change the rank of someone equal or higher than you");
                    return;
                }

                var group3 = Group.findPlayerGroup(text);
                group3.playerList.Remove(text);
                group3.playerList.Save();
                group.playerList.Add(text);
                group.playerList.Save();
                Player.GlobalMessage(string.Format("{0} &f(offline){1}'s rank was set to {2}", text,
                    Server.DefaultColor, group.color + group.name));
            }
            else
            {
                if (player == p)
                {
                    Player.SendMessage(p, "Cannot change your own rank.");
                    return;
                }

                if (p != null && (player.group.Permission >= p.group.Permission ||
                                  group.Permission >= p.group.Permission))
                {
                    Player.SendMessage(p, "Cannot change the rank of someone equal or higher to yourself.");
                    return;
                }

                if (player.group == group2 || group == group2 || group.Permission >= LevelPermission.Nobody)
                {
                    Player.SendMessage(p, string.Format("Cannot change the rank to or from \"{0}\".", group2.name));
                    return;
                }

                player.group.playerList.Remove(player.name);
                player.group.playerList.Save();
                group.playerList.Add(player.name);
                group.playerList.Save();
                Player.GlobalChat(player,
                    string.Format("{0}'s rank was set to {1}", player.color + player.PublicName + Server.DefaultColor,
                        group.color + group.name), false);
                Player.GlobalChat(null, "&6" + str, false);
                player.group = group;
                player.color = player.group.color;
                Player.GlobalDie(player, false);
                player.SendMessage(string.Format("You are now ranked {0}, type /help for your new set of commands.",
                    group.color + group.name + Server.DefaultColor));
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1],
                    false);
            }
        }

        // Token: 0x06001305 RID: 4869 RVA: 0x00068E44 File Offset: 0x00067044
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setrank <player> <rank> <yay> - Sets or returns a players rank.");
            Player.SendMessage(p, "You may use /rank as a shortcut");
            Player.SendMessage(p, "Valid Ranks are: " + Group.concatList(true, true));
            Player.SendMessage(p, "<yay> is a celebratory message");
        }
    }
}