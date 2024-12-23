using System;

namespace MCDzienny
{
    public class CmdWrite : Command
    {

        public override string name { get { return "write"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.givenMessage = message.ToUpper();
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine direction.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/write [message] - Writes [message] in blocks");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            type = p.bindings[type];
            p.ClearBlockchange();
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "You are not allowed to place this block type.");
                return;
            }
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            if (x == catchPos.x && z == catchPos.z)
            {
                Player.SendMessage(p, "No direction was selected");
                return;
            }
            if (Math.Abs(catchPos.x - x) > Math.Abs(catchPos.z - z))
            {
                ushort x2 = catchPos.x;
                if (x > catchPos.x)
                {
                    string givenMessage = catchPos.givenMessage;
                    foreach (char c2 in givenMessage)
                    {
                        x2 = FindReference.writeLetter(p, c2, x2, catchPos.y, catchPos.z, type, 0);
                    }
                }
                else
                {
                    string givenMessage2 = catchPos.givenMessage;
                    foreach (char c3 in givenMessage2)
                    {
                        x2 = FindReference.writeLetter(p, c3, x2, catchPos.y, catchPos.z, type, 1);
                    }
                }
            }
            else
            {
                ushort x2 = catchPos.z;
                if (z > catchPos.z)
                {
                    string givenMessage3 = catchPos.givenMessage;
                    foreach (char c4 in givenMessage3)
                    {
                        x2 = FindReference.writeLetter(p, c4, catchPos.x, catchPos.y, x2, type, 2);
                    }
                }
                else
                {
                    string givenMessage4 = catchPos.givenMessage;
                    foreach (char c5 in givenMessage4)
                    {
                        x2 = FindReference.writeLetter(p, c5, catchPos.x, catchPos.y, x2, type, 3);
                    }
                }
            }
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

            public string givenMessage;
        }
    }
}