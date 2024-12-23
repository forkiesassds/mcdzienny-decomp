using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class CmdJoker : Command
    {
        public override string name { get { return "joker"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            bool flag = false;
            string text = "text/joker.txt";
            if (File.Exists(text))
            {
                StreamReader streamReader = new FileInfo(text).OpenText();
                var list = new List<string>();
                while (streamReader.Peek() != -1)
                {
                    list.Add(streamReader.ReadLine());
                }
                if (list.Count == 0)
                {
                    Player.SendMessage(p, "The file 'joker.txt' is empty. It has to be filled with (funny) texts.");
                    return;
                }
                if (message[0] == '#')
                {
                    message = message.Remove(0, 1).Trim();
                    flag = true;
                    Server.s.Log("Stealth joker attempted");
                }
                Player player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Could not find player.");
                }
                else if (!player.joker)
                {
                    player.joker = true;
                    if (flag)
                    {
                        Player.GlobalMessageOps(string.Format("{0} is now STEALTH joker'd. ", player.color + player.name + Server.DefaultColor));
                    }
                    else
                    {
                        Player.GlobalChat(null,
                                          string.Format("{0} is now a &aJ&bo&ck&5e&9r{1}.", player.color + player.PublicName + Server.DefaultColor, Server.DefaultColor),
                                          showname: false);
                    }
                }
                else
                {
                    player.joker = false;
                    if (flag)
                    {
                        Player.GlobalMessageOps(string.Format("{0} is now STEALTH Unjoker'd. ", player.color + player.name + Server.DefaultColor));
                    }
                    else
                    {
                        Player.GlobalChat(null,
                                          string.Format("{0} is no longer a &aJ&bo&ck&5e&9r{1}.", player.color + player.PublicName + Server.DefaultColor,
                                                        Server.DefaultColor),
                                          showname: false);
                    }
                }
            }
            else
            {
                File.Create(text);
                Player.SendMessage(p, "The file 'joker.txt' is empty. It has to be filled with (funny) texts.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/joker <name> - Causes a player to become a joker!");
            Player.SendMessage(p, "/joker # <name> - Makes the player a joker silently");
        }
    }
}