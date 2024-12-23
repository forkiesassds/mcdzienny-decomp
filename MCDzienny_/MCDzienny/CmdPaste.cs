using System;

namespace MCDzienny
{
    public class CmdPaste : Command
    {

        public string loadname;

        public override string name { get { return "paste"; } }

        public override string shortcut { get { return "v"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place a block in the corner of where you want to paste.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paste - Pastes the stored copy.");
            Player.SendMessage(p, string.Format("&4BEWARE: {0}The blocks will always be pasted in a set direction", Server.DefaultColor));
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            Player.UndoPos Pos1 = default(Player.UndoPos);
            p.CopyBuffer.ForEach(delegate(Player.CopyPos pos)
            {
                Pos1.x = (ushort)(Math.Abs(pos.x) + x);
                Pos1.y = (ushort)(Math.Abs(pos.y) + y);
                Pos1.z = (ushort)(Math.Abs(pos.z) + z);
                if ((pos.type != 0 || p.copyAir) && p.level.GetTile(Pos1.x, Pos1.y, Pos1.z) != byte.MaxValue)
                {
                    p.level.Blockchange(p, (ushort)(Pos1.x + p.copyoffset[0]), (ushort)(Pos1.y + p.copyoffset[1]), (ushort)(Pos1.z + p.copyoffset[2]), pos.type);
                }
            });
            Player.SendMessage(p, string.Format("Pasted {0} blocks.", p.CopyBuffer.Count));
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}