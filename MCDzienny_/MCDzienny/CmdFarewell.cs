using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdFarewell : Command
    {
        public override string name { get { return "farewell"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (!p.boughtFarewell)
            {
                Player.SendMessage(p, "In order to use /farewell you have to buy it in shop first.");
            }
            else if (message == "")
            {
                p.farewellMessage = "";
                Player.GlobalChat(null, string.Format("{0} had his farewell message removed.", p.color + p.PublicName + Server.DefaultColor), showname: false);
                string queryString = "UPDATE Players SET farewellMessage = '' WHERE Name = @Name";
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@Name", p.name);
                DBInterface.ExecuteQuery(queryString, dictionary);
            }
            else if (message.Length > 37)
            {
                Player.SendMessage(p, "Welcome message must be under 37 letters.");
            }
            else
            {
                Player.GlobalChat(null, string.Format("{0} was given the farewell message: {1}.", p.color + p.PublicName + Server.DefaultColor, message), showname: false);
                string queryString = "UPDATE Players SET farewellMessage = @Farewell WHERE Name = @Name";
                var dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("@Farewell", message);
                dictionary2.Add("@Name", p.name);
                DBInterface.ExecuteQuery(queryString, dictionary2);
                p.farewellMessage = message;
                p.boughtFarewell = false;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/farewell [message] - Gives you the custom message on disconnecting.");
            Player.SendMessage(p, "If no [message] is given, your farewell message is set to default.");
        }
    }
}