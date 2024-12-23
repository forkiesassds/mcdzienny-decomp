using System;

namespace MCDzienny
{
    public class CmdTree : Command
    {
        public override string name { get { return "tree"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            p.ClearBlockchange();
            switch (message.ToLower())
            {
                case "1":
                    p.Blockchange += AddTreeB;
                    break;
                case "2":
                case "cactus":
                    p.Blockchange += AddCactus;
                    break;
                default:
                    p.Blockchange += AddTree;
                    break;
            }
            Player.SendMessage(p, "Select where you wish your tree to grow");
            p.painting = false;
        }

        void AddTreeB(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Random random = new Random();
            byte b = (byte)random.Next(8, 11);
            short num = (short)(b - random.Next(6, 8));
            for (ushort num2 = 0; num2 < b; num2++)
            {
                p.level.Blockchange(p, x, (ushort)(y + num2), z, 17);
            }
            for (short num3 = (short)-num; num3 <= num; num3++)
            {
                for (short num4 = (short)-num; num4 <= num; num4++)
                {
                    for (short num5 = (short)-num; num5 <= num; num5++)
                    {
                        short num6 = (short)Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                        if (num6 < num + 1 && random.Next(num6) < 2)
                        {
                            try
                            {
                                p.level.Blockchange(p, (ushort)(x + num3), (ushort)(y + num4 + b), (ushort)(z + num5), 18);
                            }
                            catch {}
                        }
                    }
                }
            }
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
        }

        void AddTree(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Random random = new Random();
            byte b = (byte)random.Next(5, 8);
            for (ushort num = 0; num < b; num++)
            {
                p.level.Blockchange(p, x, (ushort)(y + num), z, 17);
            }
            short num2 = (short)(b - random.Next(2, 4));
            for (short num3 = (short)-num2; num3 <= num2; num3++)
            {
                for (short num4 = (short)-num2; num4 <= num2; num4++)
                {
                    for (short num5 = (short)-num2; num5 <= num2; num5++)
                    {
                        short num6 = (short)Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                        if (num6 < num2 + 1 && random.Next(num6) < 2)
                        {
                            try
                            {
                                p.level.Blockchange(p, (ushort)(x + num3), (ushort)(y + num4 + b), (ushort)(z + num5), 18);
                            }
                            catch {}
                        }
                    }
                }
            }
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
        }

        void AddCactus(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Random random = new Random();
            byte b = (byte)random.Next(3, 6);
            for (ushort num = 0; num <= b; num++)
            {
                p.level.Blockchange(p, x, (ushort)(y + num), z, 25);
            }
            int num2 = 0;
            int num3 = 0;
            switch (random.Next(1, 3))
            {
                case 1:
                    num2 = -1;
                    break;
                default:
                    num3 = -1;
                    break;
            }
            for (ushort num = b; num <= random.Next(b + 2, b + 5); num++)
            {
                p.level.Blockchange(p, (ushort)(x + num2), (ushort)(y + num), (ushort)(z + num3), 25);
            }
            for (ushort num = b; num <= random.Next(b + 2, b + 5); num++)
            {
                p.level.Blockchange(p, (ushort)(x - num2), (ushort)(y + num), (ushort)(z - num3), 25);
            }
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tree [type] - Turns tree mode on or off.");
            Player.SendMessage(p, "Types - (Fern | 1), (Cactus | 2)");
        }
    }
}