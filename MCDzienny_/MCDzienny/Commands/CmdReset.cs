namespace MCDzienny
{
    // Token: 0x02000133 RID: 307
    public class CmdReset : Command
    {
        // Token: 0x17000445 RID: 1093
        // (get) Token: 0x0600092E RID: 2350 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
        public override string name
        {
            get { return "reset"; }
        }

        // Token: 0x17000446 RID: 1094
        // (get) Token: 0x0600092F RID: 2351 RVA: 0x0002DABC File Offset: 0x0002BCBC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000447 RID: 1095
        // (get) Token: 0x06000930 RID: 2352 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000448 RID: 1096
        // (get) Token: 0x06000931 RID: 2353 RVA: 0x0002DACC File Offset: 0x0002BCCC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000449 RID: 1097
        // (get) Token: 0x06000932 RID: 2354 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x06000933 RID: 2355 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            message = message.ToLower();
            string a;
            if ((a = message) != null)
            {
                if (a == "xp")
                {
                    Player.SendMessage(p, "This command will reset experience for all players.");
                    Player.SendMessage(p, "If you are sure you want to do this write '/reset xp yes.'");
                    return;
                }

                if (!(a == "xp yes")) return;
                DBInterface.ExecuteQuery("UPDATE `players` SET `totalScore` = 0");
                DBInterface.ExecuteQuery("UPDATE `players` SET `bestScore` = 0");
                Player.SendMessage(p, "Players experience has been reset.");
            }
        }

        // Token: 0x06000934 RID: 2356 RVA: 0x0002DB54 File Offset: 0x0002BD54
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reset xp - resets experience for all players.");
        }
    }
}