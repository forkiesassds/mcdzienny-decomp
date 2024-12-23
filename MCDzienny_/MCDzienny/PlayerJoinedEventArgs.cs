using System;

namespace MCDzienny
{
    public class PlayerJoinedEventArgs : EventArgs
    {

        public PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }
        public Player Player { get; set; }
    }
}