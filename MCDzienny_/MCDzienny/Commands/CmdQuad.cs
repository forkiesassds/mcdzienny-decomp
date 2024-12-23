using System.Collections.Generic;
using System.Linq;

namespace MCDzienny.Commands
{
    public class CmdQuad : Command
    {
        public override string name { get { return "quad"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            byte b = byte.MaxValue;
            b = byte.MaxValue;
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
            Player.SendMessage(p, "Place 4 blocks to determine vertices of the quad.");
            BlockCatch.CaptureMultipleBlocks(p, 4, DrawQuad, new BasicDrawArgs(b));
        }

        void DrawQuad(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            var list = changes.Select(c => new BlockPoints(new Vector3(c.X, c.Z, c.Y), c.Type)).ToList();
            if (da.Type1 != byte.MaxValue)
            {
                list[3].blockType = da.Type1;
            }
            if (!Block.canPlace(p, list[3].blockType))
            {
                Player.SendMessage(p, "Cannot place this type of block.");
                return;
            }
            new Core().DrawQuad(p, list);
            if (p.staticCommands)
            {
                Player.SendMessage(p, "Place 4 blocks to determine vertices of the quad.");
                BlockCatch.CaptureMultipleBlocks(p, 4, DrawQuad, da);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/quad - draws a quad.");
        }
    }
}