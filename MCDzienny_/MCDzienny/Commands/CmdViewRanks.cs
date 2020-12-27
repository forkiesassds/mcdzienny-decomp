namespace MCDzienny
{
    // Token: 0x02000250 RID: 592
    public class CmdViewRanks : Command
    {
        // Token: 0x17000627 RID: 1575
        // (get) Token: 0x0600112B RID: 4395 RVA: 0x0005DB48 File Offset: 0x0005BD48
        public override string name
        {
            get { return "viewranks"; }
        }

        // Token: 0x17000628 RID: 1576
        // (get) Token: 0x0600112C RID: 4396 RVA: 0x0005DB50 File Offset: 0x0005BD50
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000629 RID: 1577
        // (get) Token: 0x0600112D RID: 4397 RVA: 0x0005DB58 File Offset: 0x0005BD58
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700062A RID: 1578
        // (get) Token: 0x0600112E RID: 4398 RVA: 0x0005DB60 File Offset: 0x0005BD60
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700062B RID: 1579
        // (get) Token: 0x0600112F RID: 4399 RVA: 0x0005DB64 File Offset: 0x0005BD64
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x06001131 RID: 4401 RVA: 0x0005DB70 File Offset: 0x0005BD70
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var group = Group.Find(message);
            if (group == null)
            {
                Player.SendMessage(p, "Could not find group");
                return;
            }

            var text = "";
            foreach (var str in group.playerList.All()) text = text + ", " + str;
            if (text == "")
            {
                Player.SendMessage(p, string.Format("No one has the rank of {0}", group.color + group.name));
                return;
            }

            Player.SendMessage(p, string.Format("People with the rank of {0}:", group.color + group.name));
            Player.SendMessage(p, text.Remove(0, 2));
        }

        // Token: 0x06001132 RID: 4402 RVA: 0x0005DC60 File Offset: 0x0005BE60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/viewranks [rank] - Shows all users who have [rank]");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}