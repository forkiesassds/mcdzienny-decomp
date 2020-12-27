namespace MCDzienny
{
    // Token: 0x02000255 RID: 597
    public class CmdSetColor : Command
    {
        // Token: 0x17000645 RID: 1605
        // (get) Token: 0x06001158 RID: 4440 RVA: 0x0005E694 File Offset: 0x0005C894
        public override string name
        {
            get { return "setcolor"; }
        }

        // Token: 0x17000646 RID: 1606
        // (get) Token: 0x06001159 RID: 4441 RVA: 0x0005E69C File Offset: 0x0005C89C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000647 RID: 1607
        // (get) Token: 0x0600115A RID: 4442 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000648 RID: 1608
        // (get) Token: 0x0600115B RID: 4443 RVA: 0x0005E6AC File Offset: 0x0005C8AC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000649 RID: 1609
        // (get) Token: 0x0600115C RID: 4444 RVA: 0x0005E6B0 File Offset: 0x0005C8B0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600115E RID: 4446 RVA: 0x0005E6BC File Offset: 0x0005C8BC
        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }

            var num = message.IndexOf(' ');
            message = message.Replace("'", "");
            if (num != -1)
            {
                var player = Player.Find(message.Substring(0, num));
                if (player == null)
                {
                    Player.SendMessage(p, string.Format("There is no player \"{0}\"!", message.Substring(0, num)));
                    return;
                }

                if (message.Substring(num + 1) == "del")
                {
                    DBInterface.ExecuteQuery("UPDATE Players SET color = '' WHERE name = '" + player.name + "'");
                    Player.GlobalChat(player,
                        string.Format("{0}*{1} color reverted to {2}their group's default{3}.", player.color,
                            Name(player.PublicName), player.group.color, Server.DefaultColor), false);
                    player.color = player.group.color;
                    Player.GlobalDie(player, false);
                    Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0],
                        player.rot[1], false);
                    player.SetPrefix();
                    return;
                }

                var text = c.Parse(message.Substring(num + 1));
                if (text == "")
                {
                    Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                    return;
                }

                if (text == player.color)
                {
                    Player.SendMessage(p, string.Format("{0} already has that color.", player.PublicName));
                    return;
                }

                DBInterface.ExecuteQuery(string.Concat("UPDATE Players SET color = '", c.Name(text), "' WHERE name = '",
                    player.name, "'"));
                Player.GlobalChat(player,
                    string.Format("{0}*{1} color changed to {2}.", player.color, Name(player.name),
                        text + c.Name(text) + Server.DefaultColor), false);
                player.color = text;
                Player.GlobalDie(player, false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1],
                    false);
                player.SetPrefix();
            }
            else
            {
                if (message == "del")
                {
                    DBInterface.ExecuteQuery("UPDATE Players SET color = '' WHERE name = '" + p.name + "'");
                    Player.GlobalChat(p,
                        string.Format("{0}*{1} color reverted to {2}their group's default{3}.", p.color,
                            Name(p.PublicName), p.group.color, Server.DefaultColor), false);
                    p.color = p.group.color;
                    Player.GlobalDie(p, false);
                    Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                    p.SetPrefix();
                    return;
                }

                var text2 = c.Parse(message);
                if (text2 == "")
                {
                    Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                    return;
                }

                if (text2 == p.color)
                {
                    Player.SendMessage(p, "You already have that color.");
                    return;
                }

                DBInterface.ExecuteQuery(string.Concat("UPDATE Players SET color = '", c.Name(text2),
                    "' WHERE name = '", p.name, "'"));
                Player.GlobalChat(p,
                    string.Format("{0}*{1} color changed to {2}.", p.color, Name(p.PublicName),
                        text2 + c.Name(text2) + Server.DefaultColor), false);
                p.color = text2;
                Player.GlobalDie(p, false);
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                p.SetPrefix();
            }
        }

        // Token: 0x0600115F RID: 4447 RVA: 0x0005EAF4 File Offset: 0x0005CCF4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/color [player] <color/del>- Changes the nick color.  Using 'del' removes color.");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }

        // Token: 0x06001160 RID: 4448 RVA: 0x0005EB18 File Offset: 0x0005CD18
        private static string Name(string name)
        {
            var a = name[name.Length - 1].ToString().ToLower();
            if (a == "s" || a == "x") return name + Server.DefaultColor + "'";
            return name + Server.DefaultColor + "'s";
        }
    }
}