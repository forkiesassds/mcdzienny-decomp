using System;

namespace MCDzienny.InfectionSystem
{
    public class RoundStartEventArgs : EventArgs
    {
        public Level CurrentInfectionLevel { get; set; }
    }
}