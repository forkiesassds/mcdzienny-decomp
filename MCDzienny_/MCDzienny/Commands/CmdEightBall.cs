using System;
using System.Threading;

namespace MCDzienny.Commands
{
    // Token: 0x020000BE RID: 190
    public class CmdEightBall : Command
    {
        // Token: 0x170002D1 RID: 721
        // (get) Token: 0x06000671 RID: 1649 RVA: 0x000216C4 File Offset: 0x0001F8C4
        public override string name
        {
            get { return "8ball"; }
        }

        // Token: 0x170002D2 RID: 722
        // (get) Token: 0x06000672 RID: 1650 RVA: 0x000216CC File Offset: 0x0001F8CC
        public override string shortcut
        {
            get { return "8b"; }
        }

        // Token: 0x170002D3 RID: 723
        // (get) Token: 0x06000673 RID: 1651 RVA: 0x000216D4 File Offset: 0x0001F8D4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002D4 RID: 724
        // (get) Token: 0x06000674 RID: 1652 RVA: 0x000216DC File Offset: 0x0001F8DC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002D5 RID: 725
        // (get) Token: 0x06000675 RID: 1653 RVA: 0x000216E0 File Offset: 0x0001F8E0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x06000677 RID: 1655 RVA: 0x000216EC File Offset: 0x0001F8EC
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

            var flag = false;
            Player.OnPlayerChatEvent(p, ref message, ref flag);
            if (flag) return;
            var str = "";
            var arg = p == null
                ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1)
                : p.color + p.PublicName;
            if (message[message.Length - 1] != '?') message += '?';
            if (message.Trim()[0] == '#')
            {
                message = message.Substring(1, message.Length - 1).Trim();
                if (new Random().Next(0, 2) == 0)
                    str = "Yes";
                else
                    str = "No";
                Player.GlobalMessage(string.Format("%9# {0}%9 asks Magic 8-ball: {1}", arg,
                    Server.DefaultColor + message));
                Thread.Sleep(2000);
                Player.GlobalMessage(string.Format("%9# Magic 8-ball says: {0}", Server.DefaultColor + str));
                return;
            }

            switch (new Random().Next(0, 20))
            {
                case 0:
                    str = "It is certain";
                    break;
                case 1:
                    str = "It is decidedly so";
                    break;
                case 2:
                    str = "Without a doubt";
                    break;
                case 3:
                    str = "Yes – definitely";
                    break;
                case 4:
                    str = "You may rely on it";
                    break;
                case 5:
                    str = "As I see it, yes";
                    break;
                case 6:
                    str = "Most likely";
                    break;
                case 7:
                    str = "Outlook good";
                    break;
                case 8:
                    str = "Signs point to yes";
                    break;
                case 9:
                    str = "Yes";
                    break;
                case 10:
                    str = "Reply hazy, try again";
                    break;
                case 11:
                    str = "Ask again later";
                    break;
                case 12:
                    str = "Better not tell you now";
                    break;
                case 13:
                    str = "Cannot predict now";
                    break;
                case 14:
                    str = "Concentrate and ask again";
                    break;
                case 15:
                    str = "Don't count on it";
                    break;
                case 16:
                    str = "My reply is no";
                    break;
                case 17:
                    str = "My sources say no";
                    break;
                case 18:
                    str = "Outlook not so good";
                    break;
                case 19:
                    str = "Very doubtful";
                    break;
            }

            Player.GlobalMessage(string.Format("%9* {0}%9 asks Magic 8-ball: {1}", arg, Server.DefaultColor + message));
            Thread.Sleep(2000);
            Player.GlobalMessage(string.Format("%9* Magic 8-ball says: {0}", Server.DefaultColor + str));
        }

        // Token: 0x06000678 RID: 1656 RVA: 0x00021978 File Offset: 0x0001FB78
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/8ball [question] - ask for advice.");
            Player.SendMessage(p, "/8ball #[question] - gives only yes or no answer.");
        }
    }
}