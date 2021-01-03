using System;

namespace MCDzienny
{
    // Token: 0x0200027D RID: 637
    public class CmdPaste : Command
    {
        // Token: 0x04000910 RID: 2320
        public string loadname;

        // Token: 0x170006C6 RID: 1734
        // (get) Token: 0x0600124E RID: 4686 RVA: 0x00065110 File Offset: 0x00063310
        public override string name
        {
            get { return "paste"; }
        }

        // Token: 0x170006C7 RID: 1735
        // (get) Token: 0x0600124F RID: 4687 RVA: 0x00065118 File Offset: 0x00063318
        public override string shortcut
        {
            get { return "v"; }
        }

        // Token: 0x170006C8 RID: 1736
        // (get) Token: 0x06001250 RID: 4688 RVA: 0x00065120 File Offset: 0x00063320
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006C9 RID: 1737
        // (get) Token: 0x06001251 RID: 4689 RVA: 0x00065128 File Offset: 0x00063328
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006CA RID: 1738
        // (get) Token: 0x06001252 RID: 4690 RVA: 0x0006512C File Offset: 0x0006332C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170006CB RID: 1739
        // (get) Token: 0x06001253 RID: 4691 RVA: 0x00065130 File Offset: 0x00063330
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001255 RID: 4693 RVA: 0x0006513C File Offset: 0x0006333C
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            CatchPos catchPos;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place a block in the corner of where you want to paste.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06001256 RID: 4694 RVA: 0x000651A8 File Offset: 0x000633A8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paste - Pastes the stored copy.");
            Player.SendMessage(p,
                string.Format("&4BEWARE: {0}The blocks will always be pasted in a set direction", Server.DefaultColor));
        }

        // Token: 0x06001257 RID: 4695 RVA: 0x000651CC File Offset: 0x000633CC
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            Player.UndoPos Pos1;
            p.CopyBuffer.ForEach(delegate(Player.CopyPos pos)
            {
                Pos1.x = (ushort) (Math.Abs(pos.x) + x);
                Pos1.y = (ushort) (Math.Abs(pos.y) + y);
                Pos1.z = (ushort) (Math.Abs(pos.z) + z);
                if ((pos.type != 0 || p.copyAir) && p.level.GetTile(Pos1.x, Pos1.y, Pos1.z) != 255)
                    p.level.Blockchange(p, (ushort) (Pos1.x + p.copyoffset[0]), (ushort) (Pos1.y + p.copyoffset[1]),
                        (ushort) (Pos1.z + p.copyoffset[2]), pos.type);
            });
            Player.SendMessage(p, string.Format("Pasted {0} blocks.", p.CopyBuffer.Count));
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x0200027E RID: 638
        private struct CatchPos
        {
            // Token: 0x04000911 RID: 2321
            public ushort x;

            // Token: 0x04000912 RID: 2322
            public ushort y;

            // Token: 0x04000913 RID: 2323
            public ushort z;
        }
    }
}