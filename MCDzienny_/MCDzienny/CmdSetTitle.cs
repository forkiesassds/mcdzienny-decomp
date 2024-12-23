using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdSetTitle : Command
    {
        public override string name { get { return "settitle"; } }

        public override string shortcut { get { return "st"; } }

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
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }
            string text = "";
            int num = message.IndexOf(' ');
            if (message.Split(' ').Length > 1)
            {
                text = message.Substring(num + 1);
                if (text.Length > 17)
                {
                    Player.SendMessage(p, "Title must be under 17 letters.");
                    return;
                }
                if ((p == null || !Server.devs.Contains(p.name)) && (Server.devs.Contains(player.name) || text.ToLower() == "dev"))
                {
                    Player.SendMessage(p, "Can't let you do that, starfox.");
                    return;
                }
                if (text != "")
                {
                    Player.GlobalChat(null, string.Format("{0} was given the title of &b[{1}]", player.color + player.PublicName + Server.DefaultColor, text), showname: false);
                }
                else
                {
                    Player.GlobalChat(null, string.Format("{0} had their title removed.", player.color + player.prefix + player.PublicName + Server.DefaultColor), showname: false);
                }
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@Title", text);
                dictionary.Add("@Name", player.name);
                string queryString = "UPDATE Players SET Title = @Title WHERE Name = @Name";
                DBInterface.ExecuteQuery(queryString, dictionary);
                player.title = text;
                player.SetPrefix();
            }
            else
            {
                player.title = "";
                player.SetPrefix();
                Player.GlobalChat(null, string.Format("{0} had their title removed.", player.color + player.PublicName + Server.DefaultColor), showname: false);
                string queryString = "UPDATE Players SET Title = '' WHERE Name = '" + player.name + "'";
                DBInterface.ExecuteQuery(queryString);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title <player> [title] - Gives <player> the [title].");
            Player.SendMessage(p, "If no [title] is given, the player's title is removed.");
        }
    }
}