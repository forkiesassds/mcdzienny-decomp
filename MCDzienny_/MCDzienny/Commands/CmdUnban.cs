namespace MCDzienny
{
    // Token: 0x020002A5 RID: 677
    public class CmdUnban : Command
    {
        // Token: 0x17000761 RID: 1889
        // (get) Token: 0x0600136E RID: 4974 RVA: 0x0006AA58 File Offset: 0x00068C58
        public override string name
        {
            get { return "unban"; }
        }

        // Token: 0x17000762 RID: 1890
        // (get) Token: 0x0600136F RID: 4975 RVA: 0x0006AA60 File Offset: 0x00068C60
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000763 RID: 1891
        // (get) Token: 0x06001370 RID: 4976 RVA: 0x0006AA68 File Offset: 0x00068C68
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000764 RID: 1892
        // (get) Token: 0x06001371 RID: 4977 RVA: 0x0006AA70 File Offset: 0x00068C70
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000765 RID: 1893
        // (get) Token: 0x06001372 RID: 4978 RVA: 0x0006AA74 File Offset: 0x00068C74
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001374 RID: 4980 RVA: 0x0006AA80 File Offset: 0x00068C80
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var flag = false;
            if (message[0] == '@')
            {
                flag = true;
                message = message.Remove(0, 1).Trim();
            }

            var player = Player.Find(message);
            if (player == null)
            {
                if (Group.findPlayerGroup(message) == Group.findPerm(LevelPermission.Banned))
                {
                    Player.GlobalMessage(string.Format("{0} &8(banned){1} is now {2}!", message, Server.DefaultColor,
                        Group.standard.color + Group.standard.name + Server.DefaultColor));
                    Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
                    goto IL_2A2;
                }

                foreach (var item in Server.tempBans)
                    if (item.name.ToLower() == message.ToLower())
                    {
                        Server.tempBans.Remove(item);
                        Player.GlobalMessage(string.Format("{0} has had their temporary ban lifted.", message));
                        return;
                    }

                Player.SendMessage(p, "Player is not banned.");
            }
            else
            {
                if (Group.findPlayerGroup(message) != Group.findPerm(LevelPermission.Banned))
                {
                    foreach (var item2 in Server.tempBans)
                        if (item2.name == player.name)
                        {
                            Server.tempBans.Remove(item2);
                            Player.GlobalMessage(string.Concat(player.color, player.prefix, player.PublicName,
                                Server.DefaultColor, "has had their temporary ban lifted."));
                            return;
                        }

                    Player.SendMessage(p, "Player is not banned.");
                    return;
                }

                Player.GlobalChat(player,
                    string.Concat(player.color, player.prefix, player.PublicName, Server.DefaultColor, " is now ",
                        Group.standard.color, Group.standard.name, Server.DefaultColor, "!"), false);
                player.group = Group.standard;
                player.color = player.group.color;
                Player.GlobalDie(player, false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1],
                    false);
                Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
                goto IL_2A2;
            }

            return;
            IL_2A2:
            Group.findPerm(LevelPermission.Banned).playerList.Save();
            if (flag) all.Find("unbanip").Use(p, "@" + message);
        }

        // Token: 0x06001375 RID: 4981 RVA: 0x0006AD80 File Offset: 0x00068F80
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unban <player> - Unbans a player.  This includes temporary bans.");
        }
    }
}