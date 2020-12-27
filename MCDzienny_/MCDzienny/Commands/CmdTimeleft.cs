namespace MCDzienny
{
    // Token: 0x02000104 RID: 260
    public class CmdTimeleft : Command
    {
        // Token: 0x17000391 RID: 913
        // (get) Token: 0x060007F3 RID: 2035 RVA: 0x00028490 File Offset: 0x00026690
        public override string name
        {
            get { return "timeleft"; }
        }

        // Token: 0x17000392 RID: 914
        // (get) Token: 0x060007F4 RID: 2036 RVA: 0x00028498 File Offset: 0x00026698
        public override string shortcut
        {
            get { return "tleft"; }
        }

        // Token: 0x17000393 RID: 915
        // (get) Token: 0x060007F5 RID: 2037 RVA: 0x000284A0 File Offset: 0x000266A0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000394 RID: 916
        // (get) Token: 0x060007F6 RID: 2038 RVA: 0x000284A8 File Offset: 0x000266A8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000395 RID: 917
        // (get) Token: 0x060007F7 RID: 2039 RVA: 0x000284AC File Offset: 0x000266AC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060007F9 RID: 2041 RVA: 0x000284B8 File Offset: 0x000266B8
        public override void Use(Player p, string message)
        {
            if (message == "stop")
            {
                LavaSystem.phase1holder = false;
                LavaSystem.phase2holder = false;
                return;
            }

            int time;
            if (!int.TryParse(message, out time))
            {
                Help(p);
                return;
            }

            LavaSystem.time2 = time;
            LavaSystem.phase2holder = true;
            LavaSystem.UpdateTimeStatus();
        }

        // Token: 0x060007FA RID: 2042 RVA: 0x00028508 File Offset: 0x00026708
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/timeleft [time] - Counts down time to the end lava flood round. [time] in minutes");
            Player.SendMessage(p, "/timeleft stop - Stops the countdown");
            Player.SendMessage(p, "/tleft - shortcut");
        }
    }
}