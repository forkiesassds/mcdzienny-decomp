namespace MCDzienny.Levels.Effects
{
    public class RgbColor
    {

        public RgbColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }
    }
}