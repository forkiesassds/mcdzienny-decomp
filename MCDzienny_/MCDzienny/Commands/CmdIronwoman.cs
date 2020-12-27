namespace MCDzienny
{
    // Token: 0x0200008E RID: 142
    public class CmdIronwoman : Command
    {
        // Token: 0x17000125 RID: 293
        // (get) Token: 0x060003C7 RID: 967 RVA: 0x00014220 File Offset: 0x00012420
        public override string name
        {
            get { return "ironwoman"; }
        }

        // Token: 0x17000126 RID: 294
        // (get) Token: 0x060003C8 RID: 968 RVA: 0x00014228 File Offset: 0x00012428
        public override string shortcut
        {
            get { return "iw"; }
        }

        // Token: 0x17000127 RID: 295
        // (get) Token: 0x060003C9 RID: 969 RVA: 0x00014230 File Offset: 0x00012430
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000128 RID: 296
        // (get) Token: 0x060003CA RID: 970 RVA: 0x00014238 File Offset: 0x00012438
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000129 RID: 297
        // (get) Token: 0x060003CB RID: 971 RVA: 0x0001423C File Offset: 0x0001243C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700012A RID: 298
        // (get) Token: 0x060003CC RID: 972 RVA: 0x00014240 File Offset: 0x00012440
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x1700012B RID: 299
        // (get) Token: 0x060003CD RID: 973 RVA: 0x00014244 File Offset: 0x00012444
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x060003CF RID: 975 RVA: 0x00014250 File Offset: 0x00012450
        public override void Use(Player p, string message)
        {
            if (p.IronChallenge != IronChallengeType.None)
            {
                Player.SendMessage(p, "You already accepted Iron challenge!");
                return;
            }

            if (LavaSystem.time > 0)
            {
                p.IronChallenge = IronChallengeType.IronWoman;
                Player.GlobalChatLevel(p, p.color + p.PublicName + " %atakes on Iron Woman challenge!", false);
                Player.SendMessage(p, "Good luck!");
                return;
            }

            Player.SendMessage(p, "Sorry, you can take up Iron Woman challenge only before the flood starts.");
        }

        // Token: 0x060003D0 RID: 976 RVA: 0x000142B4 File Offset: 0x000124B4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ironwoman - accepts the challenge that you will not die once");
            Player.SendMessage(p, "during this round.");
            Player.SendMessage(p, "If you meet this condition your award in " + Server.moneys);
            Player.SendMessage(p, "will be twice as high as normally. But if you fail you will");
            Player.SendMessage(p, "lose three times what you could gain.");
        }
    }
}