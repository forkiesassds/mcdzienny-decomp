using System;

namespace MCDzienny
{
    public class MapChangeEventArgs : EventArgs
    {

        public MapChangeEventArgs(Player player, Level from, Level to)
        {
            Player = player;
            From = from;
            To = to;
        }
        public Player Player { get; private set; }

        public Level From { get; private set; }

        public Level To { get; private set; }

        public bool Handled { get; set; }
    }
}