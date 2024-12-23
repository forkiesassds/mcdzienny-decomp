using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdSimpleCuboidTest : Command
    {
        public override string name { get { return "cube"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            byte b = byte.MaxValue;
            if (message != "")
            {
                b = Block.Parse(message);
                if (b == byte.MaxValue)
                {
                    Player.SendMessage(p, "Unknown block type.");
                    return;
                }
                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "You can't place this block type.");
                    return;
                }
            }
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            BlockCatch.CaptureMultipleBlocks(p, 2, DrawCuboid, new BasicDrawArgs(b));
        }

        void DrawCuboid(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            byte type = changes[1].Type;
            if (da.Type1 != byte.MaxValue)
            {
                type = da.Type1;
            }
            int num = Math.Min(changes[0].X, changes[1].X);
            int num2 = Math.Max(changes[0].X, changes[1].X);
            int num3 = Math.Min(changes[0].Y, changes[1].Y);
            int num4 = Math.Max(changes[0].Y, changes[1].Y);
            int num5 = Math.Min(changes[0].Z, changes[1].Z);
            int num6 = Math.Max(changes[0].Z, changes[1].Z);
            for (int i = num; i <= num2; i++)
            {
                for (int j = num3; j <= num4; j++)
                {
                    for (int k = num5; k <= num6; k++)
                    {
                        p.BlockChanges.Add(i, j, k, type);
                    }
                }
            }
            if (p.group.maxBlocks < p.BlockChanges.Count)
            {
                Player.SendMessage(p, string.Format("You can't place {0} blocks. Your limit is {1}.", p.BlockChanges.Count, p.group.maxBlocks));
                p.BlockChanges.Abort();
                return;
            }
            Player.SendMessage(p, string.Format("You've built a simple cuboid that consists of {0} blocks.", p.BlockChanges.Count));
            p.BlockChanges.Commit();
            if (p.staticCommands)
            {
                BlockCatch.CaptureMultipleBlocks(p, 2, DrawCuboid, da);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cube <block> - draws a cube.");
            Player.SendMessage(p, "block - a block type e.g. water, it's optional.");
        }
    }
}