namespace MCDzienny
{
    public class CmdTitleColor : Command
    {
        public override string name { get { return "titlecolor"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            message = message.Replace("'", "");
            if (p == null)
            {
                Player.SendMessage(p, "You can't use this command from console.");
                return;
            }
            if (!p.boughtTColor)
            {
                Player.SendMessage(p, "You have to buy /titlecolor before you can use it.");
                return;
            }
            string text = c.Parse(message);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                return;
            }
            if (text == p.titlecolor)
            {
                Player.SendMessage(p, "You already have that title color.");
                return;
            }
            DBInterface.ExecuteQuery("UPDATE Players SET title_color = '" + c.Name(text) + "' WHERE Name = '" + p.name + "'");
            Player.GlobalChat(p,
                              string.Format("{0} had their title color changed to {1}.", p.color + p.PublicName + Server.DefaultColor,
                                            text + c.Name(text) + Server.DefaultColor),
                              showname: false);
            p.titlecolor = text;
            p.SetPrefix();
            p.boughtTColor = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/titlecolor [color] - gives you the title color of [color].");
            Player.SendMessage(p, "&0black &1navy &2green &3teal &4maroon &5purple &6gold &7silver");
            Player.SendMessage(p, "&8gray &9blue &alime &baqua &cred &dpink &eyellow &fwhite");
        }
    }
}