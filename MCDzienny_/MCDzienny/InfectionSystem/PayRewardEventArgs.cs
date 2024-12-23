using System;
using System.Collections.Generic;

namespace MCDzienny.InfectionSystem
{
    public class PayRewardEventArgs : EventArgs
    {
        public List<Player> NotInfected { get; set; }

        public List<Player> Infected { get; set; }

        public Level CurrentInfectionLevel { get; set; }
    }
}