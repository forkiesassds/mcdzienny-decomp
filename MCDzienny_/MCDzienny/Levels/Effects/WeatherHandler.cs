using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    public class WeatherHandler
    {
        public static readonly int Unknown = -1;

        public static readonly int Normal = 0;

        public static readonly int Raining = 1;

        public static readonly int Snowing = 2;

        public int Parse(string value)
        {
            if (value == null)
            {
                throw new NullReferenceException("value");
            }
            switch (value.ToLower())
            {
                case "normal":
                case "sunny":
                    return Normal;
                case "rain":
                case "raining":
                    return Raining;
                case "snow":
                case "snowing":
                    return Snowing;
                default:
                    return Unknown;
            }
        }

        public void SendToPlayer(Player player, int weather)
        {
            if (player == null)
            {
                throw new NullReferenceException("player");
            }
            if (weather != Unknown && player.Cpe.EnvWeatherType == 1)
            {
                V1.EnvSetWeatherType(player, (byte)weather);
            }
        }
    }
}