namespace MCDzienny
{
    // Token: 0x020000BD RID: 189
    public class CmdCountdown : Command
    {
        // Token: 0x170002CC RID: 716
        // (get) Token: 0x06000669 RID: 1641 RVA: 0x00021628 File Offset: 0x0001F828
        public override string name
        {
            get { return "countdown"; }
        }

        // Token: 0x170002CD RID: 717
        // (get) Token: 0x0600066A RID: 1642 RVA: 0x00021630 File Offset: 0x0001F830
        public override string shortcut
        {
            get { return "cdown"; }
        }

        // Token: 0x170002CE RID: 718
        // (get) Token: 0x0600066B RID: 1643 RVA: 0x00021638 File Offset: 0x0001F838
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002CF RID: 719
        // (get) Token: 0x0600066C RID: 1644 RVA: 0x00021640 File Offset: 0x0001F840
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002D0 RID: 720
        // (get) Token: 0x0600066D RID: 1645 RVA: 0x00021644 File Offset: 0x0001F844
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600066F RID: 1647 RVA: 0x00021650 File Offset: 0x0001F850
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

            LavaSystem.time = time;
            LavaSystem.phase1holder = true;
            LavaSystem.UpdateTimeStatus();
        }

        // Token: 0x06000670 RID: 1648 RVA: 0x000216A0 File Offset: 0x0001F8A0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/countdown [time] - Counts down time to the lava flood. [time] in minutes");
            Player.SendMessage(p, "/countdown stop - Stops the countdown");
            Player.SendMessage(p, "/cdown - shortcut");
        }
    }
}