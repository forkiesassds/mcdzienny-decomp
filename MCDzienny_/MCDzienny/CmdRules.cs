using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    class CmdRules : Command
    {
        public override string name { get { return "rules"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message != "")
            {
                if (p.group.Permission <= LevelPermission.Admin)
                {
                    Player.SendMessage(p, "You are not allowed to send /rules to another player!");
                    return;
                }
                player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Couldn't find a player named " + message);
                    return;
                }
            }
            else
            {
                player = p;
            }
            if (!File.Exists("text/rules.txt"))
            {
                File.WriteAllText("text/rules.txt", "No rules entered yet!");
            }
            var list = new List<string>();
            using (StreamReader streamReader = File.OpenText("text/rules.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine());
                }
            }
            Player.SendMessage(player, "Rules:");
            foreach (string item in list)
            {
                Player.SendMessage(player, item);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rules - displays server rules.");
            Player.SendMessage(p, "/rules [player] - sends rules to a player.");
        }
    }
}