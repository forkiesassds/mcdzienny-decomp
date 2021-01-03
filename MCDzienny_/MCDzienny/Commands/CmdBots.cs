namespace MCDzienny
{
    // Token: 0x0200024C RID: 588
    public class CmdBots : Command
    {
        // Token: 0x17000613 RID: 1555
        // (get) Token: 0x0600110B RID: 4363 RVA: 0x0005D32C File Offset: 0x0005B52C
        public override string name
        {
            get { return "bots"; }
        }

        // Token: 0x17000614 RID: 1556
        // (get) Token: 0x0600110C RID: 4364 RVA: 0x0005D334 File Offset: 0x0005B534
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000615 RID: 1557
        // (get) Token: 0x0600110D RID: 4365 RVA: 0x0005D33C File Offset: 0x0005B53C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000616 RID: 1558
        // (get) Token: 0x0600110E RID: 4366 RVA: 0x0005D344 File Offset: 0x0005B544
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000617 RID: 1559
        // (get) Token: 0x0600110F RID: 4367 RVA: 0x0005D348 File Offset: 0x0005B548
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000618 RID: 1560
        // (get) Token: 0x06001110 RID: 4368 RVA: 0x0005D34C File Offset: 0x0005B54C
        public override string CustomName
        {
            get { return Lang.Command.BotsName; }
        }

        // Token: 0x06001112 RID: 4370 RVA: 0x0005D35C File Offset: 0x0005B55C
        public override void Use(Player p, string message)
        {
            message = "";
            foreach (var playerBot in PlayerBot.playerbots)
            {
                if (playerBot.AIName != "")
                {
                    var text = message;
                    message = string.Concat(text, ", ", playerBot.name, "(", playerBot.level.name, ")[",
                        playerBot.AIName, "]");
                }
                else if (playerBot.hunt)
                {
                    var text2 = message;
                    message = string.Concat(text2, ", ", playerBot.name, "(", playerBot.level.name, ")[Hunt]");
                }
                else
                {
                    var text3 = message;
                    message = string.Concat(text3, ", ", playerBot.name, "(", playerBot.level.name, ")");
                }

                if (playerBot.kill) message += "-kill";
            }

            if (message != "")
            {
                Player.SendMessage(p,
                    string.Format(Lang.Command.BotsMessage, Server.DefaultColor + message.Remove(0, 2)));
                return;
            }

            Player.SendMessage(p, Lang.Command.BotsMessage1);
        }

        // Token: 0x06001113 RID: 4371 RVA: 0x0005D524 File Offset: 0x0005B724
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotsHelp);
        }
    }
}