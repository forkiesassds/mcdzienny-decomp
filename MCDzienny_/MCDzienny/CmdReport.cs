using System;
using System.IO;
using System.Threading;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdReport : Command
    {
        public bool pending;

        public override string name { get { return "report"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            string level = null;
            int x = 0;
            int y = 0;
            int z = 0;
            Message message2 = new Message(message);
            if (message2.Count > 0 && message2.Count < 3)
            {
                Help(p);
                return;
            }
            message2.ReadString();
            Player player = Player.Find(message2.ReadString());
            string text = message2.ReadToEnd();
            if (text == "check")
            {
                pending = false;
                Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " has checked the crime scene out.");
            }
            else
            {
                if (message.Split(' ').Length < 3)
                {
                    return;
                }
                if (!File.Exists("extra/report.txt"))
                {
                    File.CreateText("extra/report.txt");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " has reported " + player.color + player.name);
                    Player.GlobalMessage(Server.DefaultColor + "Reason: " + text);
                    File.AppendAllText("extra/report.txt", Environment.NewLine + player.name + " was reported by " + p.name + ". Reason: " + text);
                    pending = true;
                    x = p.pos[0] / 32;
                    y = p.pos[1] / 32;
                    z = p.pos[2] / 32;
                    level = p.level.name;
                }
                while (pending)
                {
                    Player.players.ForEach(delegate(Player who)
                    {
                        if (who.group.Permission >= LevelPermission.Operator)
                        {
                            Player.SendMessage(who, "There is a report stationed at " + x + " " + y + " " + z + ".");
                            Player.SendMessage(who, "It is in the map " + level + ".");
                        }
                    });
                    Thread.Sleep(5000);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/report [type] [name] [reason]");
            Player.SendMessage(p, "[type] can be check or make");
        }
    }
}