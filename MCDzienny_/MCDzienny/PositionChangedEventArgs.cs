using System;

namespace MCDzienny
{
    public class PositionChangedEventArgs : EventArgs
    {

        public PositionChangedEventArgs(int x, int y, int z, byte pitch, byte jaw)
        {
            X = x;
            Y = y;
            Z = z;
            Pitch = pitch;
            Jaw = jaw;
        }
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public byte Pitch { get; set; }

        public byte Jaw { get; set; }
    }
}