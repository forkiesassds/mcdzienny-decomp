namespace MCDzienny.Commands
{
    // Token: 0x0200011F RID: 287
    public class CmdVoteAbort : Command
    {
        // Token: 0x170003EC RID: 1004
        // (get) Token: 0x06000897 RID: 2199 RVA: 0x0002BC2C File Offset: 0x00029E2C
        public override string name
        {
            get { return "voteabort"; }
        }

        // Token: 0x170003ED RID: 1005
        // (get) Token: 0x06000898 RID: 2200 RVA: 0x0002BC34 File Offset: 0x00029E34
        public override string shortcut
        {
            get { return "vabort"; }
        }

        // Token: 0x170003EE RID: 1006
        // (get) Token: 0x06000899 RID: 2201 RVA: 0x0002BC3C File Offset: 0x00029E3C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003EF RID: 1007
        // (get) Token: 0x0600089A RID: 2202 RVA: 0x0002BC44 File Offset: 0x00029E44
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003F0 RID: 1008
        // (get) Token: 0x0600089B RID: 2203 RVA: 0x0002BC48 File Offset: 0x00029E48
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600089D RID: 2205 RVA: 0x0002BC54 File Offset: 0x00029E54
        public override void Use(Player p, string message)
        {
            if (VotingSystem.votingInProgress)
            {
                VotingSystem.EndVote();
                Player.SendMessage(p, "@The voting was aborted.");
                Player.GlobalChatWorld(p, "%4The voting was aborted.", false);
                return;
            }

            Player.SendMessage(p, "@There's no voting at the moment.");
        }

        // Token: 0x0600089E RID: 2206 RVA: 0x0002BC88 File Offset: 0x00029E88
        public override void Help(Player p)
        {
            Player.SendMessage(p, "");
        }
    }
}