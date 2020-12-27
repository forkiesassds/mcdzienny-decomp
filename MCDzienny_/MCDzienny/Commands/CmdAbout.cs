using System;
using System.Data;

namespace MCDzienny
{
    // Token: 0x0200024E RID: 590
    public class CmdAbout : Command
    {
        // Token: 0x17000620 RID: 1568
        // (get) Token: 0x0600111E RID: 4382 RVA: 0x0005D5DC File Offset: 0x0005B7DC
        public override string name
        {
            get { return "about"; }
        }

        // Token: 0x17000621 RID: 1569
        // (get) Token: 0x0600111F RID: 4383 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
        public override string shortcut
        {
            get { return "b"; }
        }

        // Token: 0x17000622 RID: 1570
        // (get) Token: 0x06001120 RID: 4384 RVA: 0x0005D5EC File Offset: 0x0005B7EC
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000623 RID: 1571
        // (get) Token: 0x06001121 RID: 4385 RVA: 0x0005D5F4 File Offset: 0x0005B7F4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000624 RID: 1572
        // (get) Token: 0x06001122 RID: 4386 RVA: 0x0005D5F8 File Offset: 0x0005B7F8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000625 RID: 1573
        // (get) Token: 0x06001123 RID: 4387 RVA: 0x0005D5FC File Offset: 0x0005B7FC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000626 RID: 1574
        // (get) Token: 0x06001124 RID: 4388 RVA: 0x0005D600 File Offset: 0x0005B800
        public override string CustomName
        {
            get { return Lang.Command.AboutName; }
        }

        // Token: 0x06001125 RID: 4389 RVA: 0x0005D608 File Offset: 0x0005B808
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, Lang.Command.CmdAboutBreakForInfo);
            p.ClearBlockchange();
            p.Blockchange += AboutBlockchange;
        }

        // Token: 0x06001126 RID: 4390 RVA: 0x0005D630 File Offset: 0x0005B830
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.CmdAboutHelp);
        }

        // Token: 0x06001127 RID: 4391 RVA: 0x0005D640 File Offset: 0x0005B840
        public void AboutBlockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            if (tile == 255)
            {
                Player.SendMessage(p, string.Format(Lang.Command.CmdAboutInvalidBlock, x, y, z));
                return;
            }

            p.SendBlockchange(x, y, z, tile);
            var text = string.Format(Lang.Command.CmdAboutBlock, x, y, z);
            object obj = text;
            text = string.Concat(obj, "&f", tile, " = ", Block.Name(tile));
            Player.SendMessage(p, text + Server.DefaultColor + ".");
            text = p.level.foundInfo(x, y, z);
            if (text != "") Player.SendMessage(p, string.Format(Lang.Command.CmdAboutPhysics, text));
            DataTable dataTable;
            if (p.level.mapType == MapType.MyMap)
                dataTable = DBInterface.fillData(string.Format(
                    "SELECT * FROM Blocks WHERE Map = {0} AND X= {1} AND Y={2} AND Z={3};", p.level.MapDbId, x, y, z));
            else
                dataTable = DBInterface.fillData(string.Concat("SELECT * FROM `Block", p.level.name, "` WHERE X=",
                    (int) x, " AND Y=", (int) y, " AND Z=", (int) z));
            var flag = false;
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                flag = true;
                var text2 = dataTable.Rows[i]["Username"].ToString();
                var arg = DateTime.Parse(dataTable.Rows[i]["TimePerformed"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                var arg2 = Block.Name(byte.Parse(dataTable.Rows[i]["Type"].ToString()));
                if (!(dataTable.Rows[i]["Deleted"].ToString() == "1"))
                    Player.SendMessage(p,
                        string.Format(Lang.Command.CmdAboutCreatedBy,
                            Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                else
                    Player.SendMessage(p,
                        string.Format(Lang.Command.CmdAboutDestroyedBy,
                            Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                Player.SendMessage(p, string.Format(Lang.Command.AboutDateOfModification, arg));
            }

            var list = p.level.blockCache.FindAll(bP => bP.x == x && bP.y == y && bP.z == z);
            for (var j = 0; j < list.Count; j++)
            {
                flag = true;
                var deleted = list[j].deleted;
                var text2 = list[j].name;
                var timePerformed = list[j].TimePerformed;
                var arg = timePerformed.ToString("yyyy-MM-dd HH:mm:ss");
                var arg2 = Block.Name(list[j].type);
                if (!deleted)
                    Player.SendMessage(p,
                        string.Format(Lang.Command.CmdAboutCreatedBy,
                            Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                else
                    Player.SendMessage(p,
                        string.Format(Lang.Command.CmdAboutDestroyedBy,
                            Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                Player.SendMessage(p, string.Format(Lang.Command.AboutDateOfModification, arg));
            }

            if (!flag) Player.SendMessage(p, Lang.Command.AboutNoModification);
            dataTable.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}