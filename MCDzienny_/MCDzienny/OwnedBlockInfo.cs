namespace MCDzienny
{
    public class OwnedBlockInfo
    {

        public string clanName;

        public int permissionLevel;

        public string playersColor;
        public string playersName;

        public int tier;

        public OwnedBlockInfo(string playersName, string playersColor, string clanName, int permissionLevel, int tier)
        {
            this.playersName = playersName;
            this.playersColor = playersColor;
            this.clanName = clanName;
            this.permissionLevel = permissionLevel;
            this.tier = tier;
        }

        public string ColoredName { get { return playersColor + playersName; } }
    }
}