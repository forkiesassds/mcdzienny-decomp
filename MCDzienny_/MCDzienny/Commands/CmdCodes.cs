namespace MCDzienny
{
    // Token: 0x020000BB RID: 187
    public class CmdCodes : Command
    {
        // Token: 0x170002C2 RID: 706
        // (get) Token: 0x06000659 RID: 1625 RVA: 0x00021528 File Offset: 0x0001F728
        public override string name
        {
            get { return "codes"; }
        }

        // Token: 0x170002C3 RID: 707
        // (get) Token: 0x0600065A RID: 1626 RVA: 0x00021530 File Offset: 0x0001F730
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002C4 RID: 708
        // (get) Token: 0x0600065B RID: 1627 RVA: 0x00021538 File Offset: 0x0001F738
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002C5 RID: 709
        // (get) Token: 0x0600065C RID: 1628 RVA: 0x00021540 File Offset: 0x0001F740
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170002C6 RID: 710
        // (get) Token: 0x0600065D RID: 1629 RVA: 0x00021544 File Offset: 0x0001F744
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x0600065E RID: 1630 RVA: 0x00021548 File Offset: 0x0001F748
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "Color Codes - put % in front of the number or letter.");
            Player.SendMessage(p,
                "%0Black = 0, %1Dark Blue = 1, %2Dark Green = 2,            %3Dark Teal = 3, %4Dark Red = 4, %5Purple = 5,       %6Gold = 6, %7Gray = 7, %8Dark Gray = 8, %9Blue = 9, %aBright Green = a, %bTeal = b, %cRed = c, %dPink = d, %eYellow = e, %fWhite = f");
        }

        // Token: 0x0600065F RID: 1631 RVA: 0x00021560 File Offset: 0x0001F760
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/codes - color codes, put % in front of the number or letter.  Example command. Made By: PlatinumKiller");
        }
    }
}