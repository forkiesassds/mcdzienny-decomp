using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    public class EnvironmentHandler
    {
        public static readonly Environment Night;

        public static readonly Environment Day;

        public static readonly Environment Cloudless;

        public static readonly Environment Darkness;

        public static readonly Environment Vanilla;

        public static readonly Environment Pinky;

        public static readonly Environment Stormy;

        static EnvironmentHandler()
        {
            Night = new Environment();
            EnvironmentItem item = new EnvironmentItem
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

        public Environment Parse(string value)
        {
            if (value == null)
            {
                throw new NullReferenceException("value");
            }
            switch (value.ToLower())
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
                default:
                    return null;
            }
        }

        public void SendToPlayer(Player player, Environment env)
        {
            if (player == null)
            {
                throw new NullReferenceException("player");
            }
            if (env == null)
            {
                throw new NullReferenceException("env");
            }
            if (player.Cpe.EnvColors != 1)
            {
                return;
            }
            foreach (EnvironmentItem item in env.Items)
            {
                V1.EnvSetColor(player, (byte)item.Type, item.Color.Red, item.Color.Green, item.Color.Blue);
            }
        }
    }
}