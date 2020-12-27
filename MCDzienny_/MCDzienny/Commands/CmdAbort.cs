namespace MCDzienny
{
    // Token: 0x0200024D RID: 589
    public class CmdAbort : Command
    {
        // Token: 0x17000619 RID: 1561
        // (get) Token: 0x06001114 RID: 4372 RVA: 0x0005D534 File Offset: 0x0005B734
        public override string name
        {
            get { return "abort"; }
        }

        // Token: 0x1700061A RID: 1562
        // (get) Token: 0x06001115 RID: 4373 RVA: 0x0005D53C File Offset: 0x0005B73C
        public override string shortcut
        {
            get { return "a"; }
        }

        // Token: 0x1700061B RID: 1563
        // (get) Token: 0x06001116 RID: 4374 RVA: 0x0005D544 File Offset: 0x0005B744
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700061C RID: 1564
        // (get) Token: 0x06001117 RID: 4375 RVA: 0x0005D54C File Offset: 0x0005B74C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700061D RID: 1565
        // (get) Token: 0x06001118 RID: 4376 RVA: 0x0005D550 File Offset: 0x0005B750
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700061E RID: 1566
        // (get) Token: 0x06001119 RID: 4377 RVA: 0x0005D554 File Offset: 0x0005B754
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x1700061F RID: 1567
        // (get) Token: 0x0600111A RID: 4378 RVA: 0x0005D558 File Offset: 0x0005B758
        public override string CustomName
        {
            get { return Lang.Command.CmdAbortName; }
        }

        // Token: 0x0600111B RID: 4379 RVA: 0x0005D560 File Offset: 0x0005B760
        public override void Use(Player p, string message)
        {
            p.ClearBlockchange();
            p.painting = false;
            p.BlockAction = 0;
            p.megaBoid = false;
            p.cmdTimer = false;
            p.staticCommands = false;
            p.deleteMode = false;
            p.ZoneCheck = false;
            p.modeType = 0;
            p.aiming = false;
            p.onTrain = false;
            Player.SendMessage(p, Lang.Command.CmdAbortMessage);
        }

        // Token: 0x0600111C RID: 4380 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.CmdAbortHelp);
        }
    }
}