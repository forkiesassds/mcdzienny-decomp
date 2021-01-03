using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000031 RID: 49
    public class EnvironmentHandler
    {
        // Token: 0x040000B4 RID: 180
        public static readonly Environment Night = new Environment();

        // Token: 0x040000B5 RID: 181
        public static readonly Environment Day;

        // Token: 0x040000B6 RID: 182
        public static readonly Environment Cloudless;

        // Token: 0x040000B7 RID: 183
        public static readonly Environment Darkness;

        // Token: 0x040000B8 RID: 184
        public static readonly Environment Vanilla;

        // Token: 0x040000B9 RID: 185
        public static readonly Environment Pinky;

        // Token: 0x040000BA RID: 186
        public static readonly Environment Stormy;

        // Token: 0x0600011E RID: 286 RVA: 0x000077F8 File Offset: 0x000059F8
        static EnvironmentHandler()
        {
            var item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(0, 0, 0)
            };
            Night.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(0, 0, 0)
            };
            Night.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(0, 0, 0)
            };
            Night.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(137, 137, 137)
            };
            Night.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(153, 153, 153)
            };
            Night.Items.Add(item);
            Day = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Day.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(153, 204, byte.MaxValue)
            };
            Day.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Day.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(153, 153, 153)
            };
            Day.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(252, 252, 252)
            };
            Day.Items.Add(item);
            Cloudless = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(153, 204, byte.MaxValue)
            };
            Cloudless.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(153, 204, byte.MaxValue)
            };
            Cloudless.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Cloudless.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(153, 153, 153)
            };
            Cloudless.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(252, 252, 252)
            };
            Cloudless.Items.Add(item);
            Darkness = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(0, 0, 0)
            };
            Darkness.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(0, 0, 0)
            };
            Darkness.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(128, 128, 128)
            };
            Darkness.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(30, 30, 30)
            };
            Darkness.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(50, 50, 50)
            };
            Darkness.Items.Add(item);
            Vanilla = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(136, 136, 136)
            };
            Vanilla.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(242, 241, 179)
            };
            Vanilla.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Vanilla.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(153, 153, 153)
            };
            Vanilla.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(252, 252, 252)
            };
            Vanilla.Items.Add(item);
            Stormy = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(51, 51, 51)
            };
            Stormy.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(61, 81, 102)
            };
            Stormy.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(136, 136, 136)
            };
            Stormy.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(153, 153, 153)
            };
            Stormy.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(187, 187, 187)
            };
            Stormy.Items.Add(item);
            Pinky = new Environment();
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Cloud,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Pinky.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sky,
                Color = new RgbColor(253, 204, byte.MaxValue)
            };
            Pinky.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Fog,
                Color = new RgbColor(byte.MaxValue, byte.MaxValue, byte.MaxValue)
            };
            Pinky.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Shadow,
                Color = new RgbColor(153, 153, 153)
            };
            Pinky.Items.Add(item);
            item = new EnvironmentItem
            {
                Type = EnvironmentType.Sunlight,
                Color = new RgbColor(252, 252, 252)
            };
            Pinky.Items.Add(item);
        }

        // Token: 0x0600011F RID: 287 RVA: 0x00008038 File Offset: 0x00006238
        public Environment Parse(string value)
        {
            if (value == null) throw new NullReferenceException("value");
            string key;
            switch (key = value.ToLower())
            {
                case "night":
                    return Night;
                case "day":
                    return Day;
                case "cloudless":
                    return Cloudless;
                case "darkness":
                    return Darkness;
                case "vanilla":
                    return Vanilla;
                case "stormy":
                    return Stormy;
                case "pinky":
                    return Pinky;
            }

            return null;
        }

        // Token: 0x06000120 RID: 288 RVA: 0x0000812C File Offset: 0x0000632C
        public void SendToPlayer(Player player, Environment env)
        {
            if (player == null) throw new NullReferenceException("player");
            if (env == null) throw new NullReferenceException("env");
            if (player.Cpe.EnvColors == 1)
                foreach (var environmentItem in env.Items)
                    V1.EnvSetColor(player, (byte) environmentItem.Type, environmentItem.Color.Red,
                        environmentItem.Color.Green, environmentItem.Color.Blue);
        }
    }
}