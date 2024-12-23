using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdTitle : Command
    {
        public override string name { get { return "title"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            message = message.Replace("'", "''");
            if (!p.boughtTitle)
            {
                Player.SendMessage(p, "In order to use /title you have to buy it in the shop first.");
                return;
            }
            bool stopIt = false;
            Player.OnPlayerChatEvent(p, ref message, ref stopIt);
            if (stopIt)
            {
                return;
            }
            string text = message;
            string queryString;
            if (text == "")
            {
                p.title = "";
                p.SetPrefix();
                Player.GlobalChat(null, string.Format("{0} had their title removed.", p.color + p.PublicName + Server.DefaultColor), showname: false);
                queryString = "UPDATE Players SET Title = '' WHERE Name = '" + p.name + "'";
                DBInterface.ExecuteQuery(queryString);
                return;
            }
            text += p.color;
            if (text.Length > 17)
            {
                Player.SendMessage(p, "Title must be under 17 letters.");
                return;
            }
            if (!Server.devs.Contains(p.name) && (Server.devs.Contains(p.name) || text.ToLower() == "dev"))
            {
                Player.SendMessage(p, "Can't let you do that, starfox.");
                return;
            }
            if (text != "")
            {
                Player.GlobalChat(null, string.Format("{0} was given the title of &b[{1}&b]", p.color + p.PublicName + Server.DefaultColor, text), showname: false);
            }
            else
            {
                Player.GlobalChat(null, string.Format("{0} had their title removed.", p.color + p.prefix + p.PublicName + Server.DefaultColor), showname: false);
            }
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Title", text);
            dictionary.Add("@Name", p.name);
            queryString = "UPDATE Players SET Title = @Title WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, dictionary);
            p.title = text;
            p.SetPrefix();
            p.boughtTitle = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title [title] - Gives you the [title].");
            Player.SendMessage(p, "If no [title] is given, your title is removed.");
        }
    }
}