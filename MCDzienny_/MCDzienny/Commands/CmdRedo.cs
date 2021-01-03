using System;

namespace MCDzienny
{
    // Token: 0x02000288 RID: 648
    public class CmdRedo : Command
    {
        // Token: 0x170006EB RID: 1771
        // (get) Token: 0x060012A0 RID: 4768 RVA: 0x000671BC File Offset: 0x000653BC
        public override string name
        {
            get { return "redo"; }
        }

        // Token: 0x170006EC RID: 1772
        // (get) Token: 0x060012A1 RID: 4769 RVA: 0x000671C4 File Offset: 0x000653C4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006ED RID: 1773
        // (get) Token: 0x060012A2 RID: 4770 RVA: 0x000671CC File Offset: 0x000653CC
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006EE RID: 1774
        // (get) Token: 0x060012A3 RID: 4771 RVA: 0x000671D4 File Offset: 0x000653D4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006EF RID: 1775
        // (get) Token: 0x060012A4 RID: 4772 RVA: 0x000671D8 File Offset: 0x000653D8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170006F0 RID: 1776
        // (get) Token: 0x060012A5 RID: 4773 RVA: 0x000671DC File Offset: 0x000653DC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060012A7 RID: 4775 RVA: 0x000671E8 File Offset: 0x000653E8
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
                var level = Level.FindExact(Pos.mapName);
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

        // Token: 0x060012A8 RID: 4776 RVA: 0x00067248 File Offset: 0x00065448
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/redo - Redoes the Undo you just performed.");
        }
    }
}