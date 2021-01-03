using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000037 RID: 55
    public class WeatherHandler
    {
        // Token: 0x040000CA RID: 202
        public static readonly int Unknown = -1;

        // Token: 0x040000CB RID: 203
        public static readonly int Normal = 0;

        // Token: 0x040000CC RID: 204
        public static readonly int Raining = 1;

        // Token: 0x040000CD RID: 205
        public static readonly int Snowing = 2;

        // Token: 0x0600013A RID: 314 RVA: 0x00008494 File Offset: 0x00006694
        public int Parse(string value)
        {
            if (value == null) throw new NullReferenceException("value");
            string key;
            switch (key = value.ToLower())
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
            }

            return Unknown;
        }

        // Token: 0x0600013B RID: 315 RVA: 0x00008564 File Offset: 0x00006764
        public void SendToPlayer(Player player, int weather)
        {
            if (player == null) throw new NullReferenceException("player");
            if (weather == Unknown) return;
            if (player.Cpe.EnvWeatherType == 1) V1.EnvSetWeatherType(player, (byte) weather);
        }
    }
}