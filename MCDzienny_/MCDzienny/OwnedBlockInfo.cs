namespace MCDzienny
{
    // Token: 0x020001AD RID: 429
    public class OwnedBlockInfo
    {
        // Token: 0x0400066C RID: 1644
        public string clanName;

        // Token: 0x0400066D RID: 1645
        public int permissionLevel;

        // Token: 0x0400066F RID: 1647
        public string playersColor;

        // Token: 0x0400066B RID: 1643
        public string playersName;

        // Token: 0x0400066E RID: 1646
        public int tier;

        // Token: 0x06000C4D RID: 3149 RVA: 0x00047EDC File Offset: 0x000460DC
        public OwnedBlockInfo(string playersName, string playersColor, string clanName, int permissionLevel, int tier)
        {
            this.playersName = playersName;
            this.playersColor = playersColor;
            this.clanName = clanName;
            this.permissionLevel = permissionLevel;
            this.tier = tier;
        }

        // Token: 0x170004CC RID: 1228
        // (get) Token: 0x06000C4C RID: 3148 RVA: 0x00047EC8 File Offset: 0x000460C8
        public string ColoredName
        {
            get { return playersColor + playersName; }
        }
    }
}