using System;

namespace MCDzienny
{
    public class CmdRedo : Command
    {
        public override string name { get { return "redo"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            byte b;
            p.RedoBuffer.ForEach(delegate(Player.UndoPos Pos)
            {
                Level level = Level.FindExact(Pos.mapName);
                if (level != null)
                {
                    b = level.GetTile(Pos.x, Pos.y, Pos.z);
                    level.Blockchange(Pos.x, Pos.y, Pos.z, Pos.type);
                    Pos.newtype = Pos.type;
                    Pos.type = b;
                    Pos.timePlaced = DateTime.Now;
                    p.UndoBuffer.Add(Pos);
                }
            });
            Player.SendMessage(p, "Redo performed.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/redo - Redoes the Undo you just performed.");
        }
    }
}