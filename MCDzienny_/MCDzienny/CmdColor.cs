using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdColor : Command
    {
        public override string name { get { return "color"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (p != null && !p.boughtColor)
            {
                Player.SendMessage(p, "You have to buy the color first. Check /store.");
                return;
            }
            if (message == "")
            {
                Help(p);
                return;
            }
            string text = c.Parse(message);
            if (text == "")
            {
                Player.SendMessage(p, string.Format("There is no color \"{0}\".", message));
                return;
            }
            if (text == p.color)
            {
                Player.SendMessage(p, "You already have this color.");
                return;
            }
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Color", c.Name(text));
            dictionary.Add("@Name", p.name);
            DBInterface.ExecuteQuery("UPDATE Players SET color = @Color WHERE name = @Name", dictionary);
            Player.GlobalChat(p, string.Format("{0}*{1} color changed to {2}.", p.color, Name(p.PublicName), text + c.Name(text) + Server.DefaultColor), showname: false);
            p.color = text;
            Player.GlobalDie(p, self: false);
            Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
            p.SetPrefix();
            p.boughtColor = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/color <color> - changes the nick color.");
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