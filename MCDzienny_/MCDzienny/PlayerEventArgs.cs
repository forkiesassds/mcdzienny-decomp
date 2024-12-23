using System;

namespace MCDzienny
{
    public class PlayerEventArgs : EventArgs
    {

        public PlayerEventArgs(Player player)
        {
            Player = player;
        }
        public Player Player { get; set; }
    }
}