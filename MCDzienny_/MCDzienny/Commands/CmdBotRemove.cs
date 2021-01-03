using System;

namespace MCDzienny
{
    // Token: 0x02000248 RID: 584
    public class CmdBotRemove : Command
    {
        // Token: 0x040008D2 RID: 2258
        public string[,] botlist;

        // Token: 0x170005FA RID: 1530
        // (get) Token: 0x060010E5 RID: 4325 RVA: 0x0005C1D8 File Offset: 0x0005A3D8
        public override string name
        {
            get { return "botremove"; }
        }

        // Token: 0x170005FB RID: 1531
        // (get) Token: 0x060010E6 RID: 4326 RVA: 0x0005C1E0 File Offset: 0x0005A3E0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170005FC RID: 1532
        // (get) Token: 0x060010E7 RID: 4327 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170005FD RID: 1533
        // (get) Token: 0x060010E8 RID: 4328 RVA: 0x0005C1F0 File Offset: 0x0005A3F0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170005FE RID: 1534
        // (get) Token: 0x060010E9 RID: 4329 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x170005FF RID: 1535
        // (get) Token: 0x060010EA RID: 4330 RVA: 0x0005C1F8 File Offset: 0x0005A3F8
        public override string CustomName
        {
            get { return Lang.Command.BotRemoveName; }
        }

        // Token: 0x060010EC RID: 4332 RVA: 0x0005C208 File Offset: 0x0005A408
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            try
            {
                if (message.ToLower() == Lang.Command.BotRemoveParameter)
                {
                    for (var i = 0; i < PlayerBot.playerbots.Count; i++)
                        if (PlayerBot.playerbots[i].level == p.level)
                        {
                            var playerBot = PlayerBot.playerbots[i];
                            playerBot.removeBot();
                            i--;
                        }
                }
                else
                {
                    var playerBot2 = PlayerBot.Find(message);
                    if (playerBot2 == null)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BotRemoveMessage, playerBot2));
                    }
                    else if (p.level != playerBot2.level)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BotRemoveMessage1, playerBot2.name));
                    }
                    else
                    {
                        playerBot2.removeBot();
                        Player.SendMessage(p, Lang.Command.BotRemoveMessage2);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060010ED RID: 4333 RVA: 0x0005C2F0 File Offset: 0x0005A4F0
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotRemoveHelp);
        }
    }
}