using System.Timers;

namespace MCDzienny
{
    // Token: 0x020002C3 RID: 707
    public class CmdTimer : Command
    {
        // Token: 0x170007B0 RID: 1968
        // (get) Token: 0x06001415 RID: 5141 RVA: 0x0006F844 File Offset: 0x0006DA44
        public override string name
        {
            get { return "timer"; }
        }

        // Token: 0x170007B1 RID: 1969
        // (get) Token: 0x06001416 RID: 5142 RVA: 0x0006F84C File Offset: 0x0006DA4C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007B2 RID: 1970
        // (get) Token: 0x06001417 RID: 5143 RVA: 0x0006F854 File Offset: 0x0006DA54
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170007B3 RID: 1971
        // (get) Token: 0x06001418 RID: 5144 RVA: 0x0006F85C File Offset: 0x0006DA5C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007B4 RID: 1972
        // (get) Token: 0x06001419 RID: 5145 RVA: 0x0006F860 File Offset: 0x0006DA60
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600141B RID: 5147 RVA: 0x0006F86C File Offset: 0x0006DA6C
        public override void Use(Player p, string message)
        {
            if (p.cmdTimer)
            {
                Player.SendMessage(p, "Can only have one timer at a time. Use /abort to cancel your previous timer.");
                return;
            }

            var messageTimer = new Timer(5000.0);
            if (message == "")
            {
                Help(p);
                return;
            }

            var TotalTime = 0;
            try
            {
                TotalTime = int.Parse(message.Split(' ')[0]);
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            catch
            {
                TotalTime = 60;
            }

            if (TotalTime > 300)
            {
                Player.SendMessage(p, "Cannot have more than 5 minutes in a timer");
                return;
            }

            Player.GlobalChatLevel(p,
                string.Concat(Server.DefaultColor, "Timer lasting for ", TotalTime, " seconds has started."), false);
            TotalTime /= 5;
            Player.GlobalChatLevel(p, Server.DefaultColor + message, false);
            p.cmdTimer = true;
            messageTimer.Elapsed += delegate
            {
                TotalTime--;
                if (TotalTime < 1 || !p.cmdTimer)
                {
                    Player.SendMessage(p, "Timer ended.");
                    messageTimer.Stop();
                    return;
                }

                Player.GlobalChatLevel(p, Server.DefaultColor + message, false);
                Player.GlobalChatLevel(p, "Timer has " + TotalTime * 5 + " seconds remaining.", false);
            };
            messageTimer.Start();
        }

        // Token: 0x0600141C RID: 5148 RVA: 0x0006F9FC File Offset: 0x0006DBFC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/timer [time] [message] - Starts a timer which repeats [message] every 5 seconds.");
            Player.SendMessage(p, "Repeats constantly until [time] has passed");
        }
    }
}