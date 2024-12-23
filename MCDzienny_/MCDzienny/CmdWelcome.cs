using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdWelcome : Command
    {
        public override string name { get { return "welcome"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (!p.boughtWelcome)
            {
                Player.SendMessage(p, "In order to use /welcome you have to buy it in shop first.");
                return;
            }
            string queryString;
            if (message == "")
            {
                p.welcomeMessage = "";
                Player.GlobalChat(null, p.color + p.PublicName + Server.DefaultColor + " had his welcome message removed.", showname: false);
                queryString = "UPDATE Players SET welcomeMessage = '' WHERE Name = '" + p.name + "'";
                DBInterface.ExecuteQuery(queryString);
                return;
            }
            if (message.Length > 37)
            {
                Player.SendMessage(p, "Welcome message must be under 37 letters.");
                return;
            }
            Player.GlobalChat(null, string.Format("{0} was given the welcome message: {1}.", p.color + p.PublicName + Server.DefaultColor, message), showname: false);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Welcome", message);
            dictionary.Add("@Name", p.name);
            queryString = "UPDATE Players SET welcomeMessage = @Welcome WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, dictionary);
            p.welcomeMessage = message;
            p.boughtWelcome = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/welcome [message] - Gives you the custom message on joining server.");
            Player.SendMessage(p, "If no [message] is given, your welcome message is set to default.");
        }
    }
}