namespace MCDzienny
{
    public class CmdSetColor : Command
    {
        public override string name { get { return "setcolor"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }
            int num = message.IndexOf(' ');
            message = message.Replace("'", "");
            if (num != -1)
            {
                Player player = Player.Find(message.Substring(0, num));
                if (player == null)
                {
                    Player.SendMessage(p, string.Format("There is no player \"{0}\"!", message.Substring(0, num)));
                    return;
                }
                if (message.Substring(num + 1) == "del")
                {
                    DBInterface.ExecuteQuery("UPDATE Players SET color = '' WHERE name = '" + player.name + "'");
                    Player.GlobalChat(
                        player,
                        string.Format("{0}*{1} color reverted to {2}their group's default{3}.", player.color, Name(player.PublicName), player.group.color,
                                      Server.DefaultColor),
                        showname: false);
                    player.color = player.group.color;
                    Player.GlobalDie(player, self: false);
                    Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
                    player.SetPrefix();
                    return;
                }
                string text = c.Parse(message.Substring(num + 1));
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
                DBInterface.ExecuteQuery("UPDATE Players SET color = '" + c.Name(text) + "' WHERE name = '" + player.name + "'");
                Player.GlobalChat(player, string.Format("{0}*{1} color changed to {2}.", player.color, Name(player.name), text + c.Name(text) + Server.DefaultColor), showname: false);
                player.color = text;
                Player.GlobalDie(player, self: false);
                Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
                player.SetPrefix();
            }
            else if (message == "del")
            {
                DBInterface.ExecuteQuery("UPDATE Players SET color = '' WHERE name = '" + p.name + "'");
                Player.GlobalChat(p,
                                  string.Format("{0}*{1} color reverted to {2}their group's default{3}.", p.color, Name(p.PublicName), p.group.color, Server.DefaultColor), showname: false);
                p.color = p.group.color;
                Player.GlobalDie(p, self: false);
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
                p.SetPrefix();
            }
            else
            {
                string text2 = c.Parse(message);
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
                DBInterface.ExecuteQuery("UPDATE Players SET color = '" + c.Name(text2) + "' WHERE name = '" + p.name + "'");
                Player.GlobalChat(p, string.Format("{0}*{1} color changed to {2}.", p.color, Name(p.PublicName), text2 + c.Name(text2) + Server.DefaultColor), showname: false);
                p.color = text2;
                Player.GlobalDie(p, self: false);
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
                p.SetPrefix();
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/color [player] <color/del>- Changes the nick color.  Using 'del' removes color.");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }

        static string Name(string name)
        {
            string text = name[name.Length - 1].ToString().ToLower();
            if (text == "s" || text == "x")
            {
                return name + Server.DefaultColor + "'";
            }
            return name + Server.DefaultColor + "'s";
        }
    }
}