using System.Collections.Generic;
using System.Linq;

namespace MCDzienny.Commands
{
    public class CmdTriangle : Command
    {
        public override string name { get { return "triangle"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            byte b = byte.MaxValue;
            if (message != "")
            {
                b = Block.Byte(message.ToLower().Trim());
                if (b == byte.MaxValue)
                {
                    Player.SendMessage(p, "Unknown type of block.");
                    return;
                }
                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
            }
            Player.SendMessage(p, "Place 3 blocks to determine vertices of a triangle.");
            BlockCatch.CaptureMultipleBlocks(p, 3, DrawTriangle, new BasicDrawArgs(b));
        }

        void DrawTriangle(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            var list = changes.Select(c => new BlockPoints(new Vector3(c.X, c.Z, c.Y), c.Type)).ToList();
            if (da.Type1 != byte.MaxValue)
            {
                list[2].blockType = da.Type1;
            }
            if (!Block.canPlace(p, list[2].blockType))
            {
                Player.SendMessage(p, "You are not allowed to place this block type.");
                return;
            }
            new Core().DrawTriangle(p, list);
            if (p.staticCommands)
            {
                Player.SendMessage(p, "Place 3 blocks to determine vertices of the triangle.");
                BlockCatch.CaptureMultipleBlocks(p, 3, DrawTriangle, da);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/triangle - place three blocks to determine vertices of triangle.");
            Player.SendMessage(p, "/triangle [block] - draws triangle that consists of [block].");
        }
    }
}