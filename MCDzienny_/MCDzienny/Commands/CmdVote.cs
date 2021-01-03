namespace MCDzienny
{
    // Token: 0x0200012F RID: 303
    public class CmdVote : Command
    {
        // Token: 0x1700042E RID: 1070
        // (get) Token: 0x0600090B RID: 2315 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
        public override string name
        {
            get { return "vote"; }
        }

        // Token: 0x1700042F RID: 1071
        // (get) Token: 0x0600090C RID: 2316 RVA: 0x0002D0CC File Offset: 0x0002B2CC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000430 RID: 1072
        // (get) Token: 0x0600090D RID: 2317 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000431 RID: 1073
        // (get) Token: 0x0600090E RID: 2318 RVA: 0x0002D0DC File Offset: 0x0002B2DC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000432 RID: 1074
        // (get) Token: 0x0600090F RID: 2319 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x06000911 RID: 2321 RVA: 0x0002D0EC File Offset: 0x0002B2EC
        public override void Use(Player p, string message)
        {
            if (message == "")
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
            if (VotingSystem.votingInProgress)
            {
                Player.SendMessage(p, "Please wait till the end of the current voting!");
                return;
            }

            Player.GlobalMessage("---------------- Vote ----------------");
            Player.GlobalMessage(string.Format(message));
            Player.GlobalMessage(string.Format("Type &cY {0}or &cN {1}to vote.", Server.DefaultColor,
                Server.DefaultColor));
            VotingSystem.StartVote(null, message, VotingSystem.TypeOfVote.Vote, 30000);
        }

        // Token: 0x06000912 RID: 2322 RVA: 0x0002D194 File Offset: 0x0002B394
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/vote <message> - Calls a 30sec vote.");
        }
    }
}