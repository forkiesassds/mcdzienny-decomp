using System;

namespace MCDzienny
{
    // Token: 0x020002A4 RID: 676
    public class CmdTree : Command
    {
        // Token: 0x1700075B RID: 1883
        // (get) Token: 0x06001362 RID: 4962 RVA: 0x0006A6B0 File Offset: 0x000688B0
        public override string name
        {
            get { return "tree"; }
        }

        // Token: 0x1700075C RID: 1884
        // (get) Token: 0x06001363 RID: 4963 RVA: 0x0006A6B8 File Offset: 0x000688B8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700075D RID: 1885
        // (get) Token: 0x06001364 RID: 4964 RVA: 0x0006A6C0 File Offset: 0x000688C0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700075E RID: 1886
        // (get) Token: 0x06001365 RID: 4965 RVA: 0x0006A6C8 File Offset: 0x000688C8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700075F RID: 1887
        // (get) Token: 0x06001366 RID: 4966 RVA: 0x0006A6CC File Offset: 0x000688CC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000760 RID: 1888
        // (get) Token: 0x06001367 RID: 4967 RVA: 0x0006A6D0 File Offset: 0x000688D0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001369 RID: 4969 RVA: 0x0006A6DC File Offset: 0x000688DC
        public override void Use(Player p, string message)
        {
            p.ClearBlockchange();
            string a;
            if ((a = message.ToLower()) != null)
            {
                if (a == "1")
                {
                    p.Blockchange += AddTreeB;
                    goto IL_73;
                }

                if (a == "2" || a == "cactus")
                {
                    p.Blockchange += AddCactus;
                    goto IL_73;
                }
            }

            p.Blockchange += AddTree;
            IL_73:
            Player.SendMessage(p, "Select where you wish your tree to grow");
            p.painting = false;
        }

        // Token: 0x0600136A RID: 4970 RVA: 0x0006A770 File Offset: 0x00068970
        private void AddTreeB(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var random = new Random();
            var b = (byte) random.Next(8, 11);
            var num = (short) (b - random.Next(6, 8));
            for (ushort num2 = 0; num2 < b; num2 = (ushort) (num2 + 1))
                p.level.Blockchange(p, x, (ushort) (y + num2), z, 17);
            for (var num3 = (short) -num; num3 <= num; num3 = (short) (num3 + 1))
            for (var num4 = (short) -num; num4 <= num; num4 = (short) (num4 + 1))
            for (var num5 = (short) -num; num5 <= num; num5 = (short) (num5 + 1))
            {
                var num6 = (short) Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                if (num6 < num + 1 && random.Next(num6) < 2)
                    try
                    {
                        p.level.Blockchange(p, (ushort) (x + num3), (ushort) (y + num4 + b), (ushort) (z + num5), 18);
                    }
                    catch
                    {
                    }
            }

            if (!p.staticCommands) p.ClearBlockchange();
        }

        // Token: 0x0600136B RID: 4971 RVA: 0x0006A868 File Offset: 0x00068A68
        private void AddTree(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var random = new Random();
            var b = (byte) random.Next(5, 8);
            for (ushort num = 0; num < b; num = (ushort) (num + 1))
                p.level.Blockchange(p, x, (ushort) (y + num), z, 17);
            var num2 = (short) (b - random.Next(2, 4));
            for (var num3 = (short) -num2; num3 <= num2; num3 = (short) (num3 + 1))
            for (var num4 = (short) -num2; num4 <= num2; num4 = (short) (num4 + 1))
            for (var num5 = (short) -num2; num5 <= num2; num5 = (short) (num5 + 1))
            {
                var num6 = (short) Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                if (num6 < num2 + 1 && random.Next(num6) < 2)
                    try
                    {
                        p.level.Blockchange(p, (ushort) (x + num3), (ushort) (y + num4 + b), (ushort) (z + num5), 18);
                    }
                    catch
                    {
                    }
            }

            if (!p.staticCommands) p.ClearBlockchange();
        }

        // Token: 0x0600136C RID: 4972 RVA: 0x0006A960 File Offset: 0x00068B60
        private void AddCactus(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var random = new Random();
            var b = (byte) random.Next(3, 6);
            for (ushort num = 0; num <= b; num = (ushort) (num + 1))
                p.level.Blockchange(p, x, (ushort) (y + num), z, 25);
            var num2 = 0;
            var num3 = 0;
            switch (random.Next(1, 3))
            {
                case 1:
                    num2 = -1;
                    break;
                default:
                    num3 = -1;
                    break;
            }

            for (ushort num = b; num <= random.Next(b + 2, b + 5); num = (ushort) (num + 1))
                p.level.Blockchange(p, (ushort) (x + num2), (ushort) (y + num), (ushort) (z + num3), 25);
            for (ushort num = b; num <= random.Next(b + 2, b + 5); num = (ushort) (num + 1))
                p.level.Blockchange(p, (ushort) (x - num2), (ushort) (y + num), (ushort) (z - num3), 25);
            if (!p.staticCommands) p.ClearBlockchange();
        }

        // Token: 0x0600136D RID: 4973 RVA: 0x0006AA40 File Offset: 0x00068C40
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tree [type] - Turns tree mode on or off.");
            Player.SendMessage(p, "Types - (Fern | 1), (Cactus | 2)");
        }
    }
}