namespace MCDzienny
{
    // Token: 0x02000065 RID: 101
    public class CmdIronman : Command
    {
        // Token: 0x170000AE RID: 174
        // (get) Token: 0x0600027E RID: 638 RVA: 0x0000D9CC File Offset: 0x0000BBCC
        public override string name
        {
            get { return "ironman"; }
        }

        // Token: 0x170000AF RID: 175
        // (get) Token: 0x0600027F RID: 639 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
        public override string shortcut
        {
            get { return "im"; }
        }

        // Token: 0x170000B0 RID: 176
        // (get) Token: 0x06000280 RID: 640 RVA: 0x0000D9DC File Offset: 0x0000BBDC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000B1 RID: 177
        // (get) Token: 0x06000281 RID: 641 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000B2 RID: 178
        // (get) Token: 0x06000282 RID: 642 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000B3 RID: 179
        // (get) Token: 0x06000283 RID: 643 RVA: 0x0000D9EC File Offset: 0x0000BBEC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170000B4 RID: 180
        // (get) Token: 0x06000284 RID: 644 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000286 RID: 646 RVA: 0x0000D9FC File Offset: 0x0000BBFC
        public override void Use(Player p, string message)
        {
            if (p.IronChallenge != IronChallengeType.None)
            {
                Player.SendMessage(p, "You already accepted Iron Man challenge!");
                return;
            }

            if (LavaSystem.time > 0)
            {
                p.IronChallenge = IronChallengeType.IronMan;
                Player.GlobalChatLevel(p, p.color + p.PublicName + " %atakes on Iron Man challenge!", false);
                Player.SendMessage(p, "Good luck!");
                return;
            }

            Player.SendMessage(p, "Sorry, you can take up Iron Man challenge only before the flood starts.");
        }

        // Token: 0x06000287 RID: 647 RVA: 0x0000DA60 File Offset: 0x0000BC60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ironman - accepts the challenge that you will not die once");
            Player.SendMessage(p, "during this round.");
            Player.SendMessage(p, "If you meet this condition your award in " + Server.moneys);
            Player.SendMessage(p, "will be twice as high as normally. But if you fail you will");
            Player.SendMessage(p, "lose three times what you could gain.");
        }
    }
}