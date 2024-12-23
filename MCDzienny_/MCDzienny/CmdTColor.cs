namespace MCDzienny
{
    public class CmdTColor : Command
    {
        public override string name { get { return "tcolor"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            message = message.Replace("'", "");
            string[] array = message.Split(' ');
            Player player = Player.Find(array[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }
            if (array.Length == 1)
            {
                player.titlecolor = "";
                Player.GlobalChat(player, string.Format("{0} had their title color removed.", player.color + player.PublicName + Server.DefaultColor), showname: false);
                DBInterface.ExecuteQuery("UPDATE Players SET title_color = '' WHERE Name = '" + player.name + "'");
                player.SetPrefix();
                return;
            }
            string text = c.Parse(array[1]);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", array[1]));
                return;
            }
            if (text == player.titlecolor)
            {
                Player.SendMessage(p, player.PublicName + " already has that title color.");
                return;
            }
            DBInterface.ExecuteQuery("UPDATE Players SET title_color = '" + c.Name(text) + "' WHERE Name = '" + player.name + "'");
            Player.GlobalChat(
                player,
                string.Format("{0} had their title color changed to {1}.", player.color + player.PublicName + Server.DefaultColor,
                              text + c.Name(text) + Server.DefaultColor),
                showname: false);
            player.titlecolor = text;
            player.SetPrefix();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tcolor <player> [color] - Gives <player> the title color of [color].");
            Player.SendMessage(p, "If no [color] is specified, title color is removed.");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }
    }
}