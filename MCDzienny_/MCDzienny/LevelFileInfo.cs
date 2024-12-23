namespace MCDzienny
{
    public class LevelFileInfo
    {

        public int Depth;

        public int Height;

        public PlayerPosition Spawn;
        public int Width;

        public int BlockCount { get { return Width * Height * Depth; } }
    }
}