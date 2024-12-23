namespace MCDzienny.Levels.Effects
{
    public class Texture
    {

        public Texture(string url, sbyte sideBlock, sbyte edgeBlock, short sideLevel)
        {
            Url = url;
            SideBlock = sideBlock;
            EdgeBlock = edgeBlock;
            SideLevel = sideLevel;
        }
        public string Url { get; set; }

        public sbyte SideBlock { get; set; }

        public sbyte EdgeBlock { get; set; }

        public short SideLevel { get; set; }
    }
}