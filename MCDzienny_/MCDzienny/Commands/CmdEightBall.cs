using System;
using System.Threading;

namespace MCDzienny.Commands
{
    public class CmdEightBall : Command
    {
        public override string name { get { return "8ball"; } }

        public override string shortcut { get { return "8b"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message == "#")
            {
                Help(p);
                return;
            }
            if (p != null && (p.muted || p.IsTempMuted))
            {
                Player.SendMessage(p, "You can't use this command while muted.");
                return;
            }
            bool stopIt = false;
            Player.OnPlayerChatEvent(p, ref message, ref stopIt);
            if (stopIt)
            {
                return;
            }
            int num = 0;
            string text = "";
            string arg = p == null ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1) : p.color + p.PublicName;
            if (message[message.Length - 1] != '?')
            {
                message += '?';
            }
            if (message.Trim()[0] == '#')
            {
                message = message.Substring(1, message.Length - 1).Trim();
                text = new Random().Next(0, 2) != 0 ? "No" : "Yes";
                Player.GlobalMessage(string.Format("%9# {0}%9 asks Magic 8-ball: {1}", arg, Server.DefaultColor + message));
                Thread.Sleep(2000);
                Player.GlobalMessage(string.Format("%9# Magic 8-ball says: {0}", Server.DefaultColor + text));
                return;
            }
            switch (new Random().Next(0, 20))
            {
                case 0:
                    text = "It is certain";
                    break;
                case 1:
                    text = "It is decidedly so";
                    break;
                case 2:
                    text = "Without a doubt";
                    break;
                case 3:
                    text = "Yes – definitely";
                    break;
                case 4:
                    text = "You may rely on it";
                    break;
                case 5:
                    text = "As I see it, yes";
                    break;
                case 6:
                    text = "Most likely";
                    break;
                case 7:
                    text = "Outlook good";
                    break;
                case 8:
                    text = "Signs point to yes";
                    break;
                case 9:
                    text = "Yes";
                    break;
                case 10:
                    text = "Reply hazy, try again";
                    break;
                case 11:
                    text = "Ask again later";
                    break;
                case 12:
                    text = "Better not tell you now";
                    break;
                case 13:
                    text = "Cannot predict now";
                    break;
                case 14:
                    text = "Concentrate and ask again";
                    break;
                case 15:
                    text = "Don't count on it";
                    break;
                case 16:
                    text = "My reply is no";
                    break;
                case 17:
                    text = "My sources say no";
                    break;
                case 18:
                    text = "Outlook not so good";
                    break;
                case 19:
                    text = "Very doubtful";
                    break;
            }
            Player.GlobalMessage(string.Format("%9* {0}%9 asks Magic 8-ball: {1}", arg, Server.DefaultColor + message));
            Thread.Sleep(2000);
            Player.GlobalMessage(string.Format("%9* Magic 8-ball says: {0}", Server.DefaultColor + text));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/8ball [question] - ask for advice.");
            Player.SendMessage(p, "/8ball #[question] - gives only yes or no answer.");
        }
    }
}