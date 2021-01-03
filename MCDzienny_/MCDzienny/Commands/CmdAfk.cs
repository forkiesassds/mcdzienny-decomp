using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000251 RID: 593
    internal class CmdAfk : Command
    {
        // Token: 0x1700062C RID: 1580
        // (get) Token: 0x06001133 RID: 4403 RVA: 0x0005DC88 File Offset: 0x0005BE88
        public override string name
        {
            get { return "afk"; }
        }

        // Token: 0x1700062D RID: 1581
        // (get) Token: 0x06001134 RID: 4404 RVA: 0x0005DC90 File Offset: 0x0005BE90
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700062E RID: 1582
        // (get) Token: 0x06001135 RID: 4405 RVA: 0x0005DC98 File Offset: 0x0005BE98
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700062F RID: 1583
        // (get) Token: 0x06001136 RID: 4406 RVA: 0x0005DCA0 File Offset: 0x0005BEA0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000630 RID: 1584
        // (get) Token: 0x06001137 RID: 4407 RVA: 0x0005DCA4 File Offset: 0x0005BEA4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000631 RID: 1585
        // (get) Token: 0x06001138 RID: 4408 RVA: 0x0005DCA8 File Offset: 0x0005BEA8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000632 RID: 1586
        // (get) Token: 0x06001139 RID: 4409 RVA: 0x0005DCAC File Offset: 0x0005BEAC
        public override string CustomName
        {
            get { return Lang.Command.AfkName; }
        }

        // Token: 0x0600113A RID: 4410 RVA: 0x0005DCB4 File Offset: 0x0005BEB4
        public override void Use(Player p, string message)
        {
            if (!(message != "list"))
            {
                foreach (var message2 in Server.afkset) Player.SendMessage(p, message2);
                return;
            }

            if (p.joker) message = "";
            if (Server.voteMode && (p.level.mapType == MapType.Lava && !LavaSettings.All.IsAfkDuringVoteAllowed ||
                                    p.level.mapType == MapType.Zombie && !InfectionSettings.All.IsAfkDuringVoteAllowed))
            {
                Player.SendMessage(p, "You can't use this command during a map vote.");
                return;
            }

            if (Server.afkset.Contains(p.name))
            {
                Server.afkset.Remove(p.name);
                if (!Server.voteMode)
                    Player.GlobalMessage(string.Format(Lang.Command.AfkMessage2,
                        p.color + p.PublicName + Server.DefaultColor));
                Player.IRCSay(string.Format(Lang.Command.AfkMessage3, p.PublicName));
                return;
            }

            Server.afkset.Add(p.name);
            if (p.muted || p.IsTempMuted)
            {
                Player.SendMessage(p, "You are afk now.");
                return;
            }

            var flag = false;
            Player.OnPlayerChatEvent(p, ref message, ref flag);
            if (flag) return;
            Player.GlobalMessage(string.Format(Lang.Command.AfkMessage, p.color + p.PublicName + Server.DefaultColor,
                message));
            Player.IRCSay(string.Format(Lang.Command.AfkMessage1, p.PublicName, message));
        }

        // Token: 0x0600113B RID: 4411 RVA: 0x0005DE50 File Offset: 0x0005C050
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AfkHelp);
        }
    }
}