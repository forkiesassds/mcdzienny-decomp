using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    // Token: 0x02000276 RID: 630
    public class CmdMessageBlock : Command
    {
        // Token: 0x170006A6 RID: 1702
        // (get) Token: 0x06001218 RID: 4632 RVA: 0x0006408C File Offset: 0x0006228C
        public override string name
        {
            get { return "mb"; }
        }

        // Token: 0x170006A7 RID: 1703
        // (get) Token: 0x06001219 RID: 4633 RVA: 0x00064094 File Offset: 0x00062294
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006A8 RID: 1704
        // (get) Token: 0x0600121A RID: 4634 RVA: 0x0006409C File Offset: 0x0006229C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006A9 RID: 1705
        // (get) Token: 0x0600121B RID: 4635 RVA: 0x000640A4 File Offset: 0x000622A4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006AA RID: 1706
        // (get) Token: 0x0600121C RID: 4636 RVA: 0x000640A8 File Offset: 0x000622A8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170006AB RID: 1707
        // (get) Token: 0x0600121D RID: 4637 RVA: 0x000640AC File Offset: 0x000622AC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600121E RID: 4638 RVA: 0x000640B0 File Offset: 0x000622B0
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            CatchPos catchPos;
            catchPos.message = "";
            try
            {
                string key;
                switch (key = message.Split(' ')[0])
                {
                    case "air":
                        catchPos.type = 132;
                        goto IL_132;
                    case "water":
                        catchPos.type = 133;
                        goto IL_132;
                    case "lava":
                        catchPos.type = 134;
                        goto IL_132;
                    case "black":
                        catchPos.type = 131;
                        goto IL_132;
                    case "white":
                        catchPos.type = 130;
                        goto IL_132;
                    case "show":
                        showMBs(p);
                        return;
                }

                catchPos.type = 130;
                catchPos.message = message;
                IL_132: ;
            }
            catch
            {
                catchPos.type = 130;
                catchPos.message = message;
            }

            if (catchPos.message == "") catchPos.message = message.Substring(message.IndexOf(' ') + 1);
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place where you wish the message block to go.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x0600121F RID: 4639 RVA: 0x00064280 File Offset: 0x00062480
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mb [block] [message] - Places a message in your next block.");
            Player.SendMessage(p, "Valid blocks: white, black, air, water, lava");
            Player.SendMessage(p, "/mb show shows or hides MBs");
        }

        // Token: 0x06001220 RID: 4640 RVA: 0x000642A4 File Offset: 0x000624A4
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var catchPos = (CatchPos) p.blockchangeObject;
            var parameters = new Dictionary<string, object>
            {
                {
                    "@Message",
                    catchPos.message
                }
            };
            if (p.level.mapType == MapType.MyMap)
                using (var dataTable = DBInterface.fillData(string.Format(
                    "SELECT * FROM Messages WHERE Map={0} AND X={1} AND Y={2} AND Z={3};", p.level.MapDbId, x, y, z)))
                {
                    if (dataTable.Rows.Count == 0)
                        DBInterface.ExecuteQuery(
                            string.Concat("INSERT INTO `Messages` (Map, X, Y, Z, Message) VALUES (", p.level.MapDbId,
                                ", ", x, ", ", y, ", ", z, ", @Message)"), parameters);
                    else
                        DBInterface.ExecuteQuery(
                            string.Concat("UPDATE `Messages` SET Message=@Message WHERE Map=", p.level.MapDbId,
                                "AND X=", x, " AND Y=", y, " AND Z=", z), parameters);
                    goto IL_2E7;
                }

            using (var dataTable2 = DBInterface.fillData(string.Concat("SELECT * FROM `Messages", p.level.name,
                "` WHERE X=", (int) x, " AND Y=", (int) y, " AND Z=", (int) z)))
            {
                if (dataTable2.Rows.Count == 0)
                    DBInterface.ExecuteQuery(
                        string.Concat("INSERT INTO `Messages", p.level.name, "` (X, Y, Z, Message) VALUES (", (int) x,
                            ", ", (int) y, ", ", (int) z, ", @Message)"), parameters);
                else
                    DBInterface.ExecuteQuery(
                        string.Concat("UPDATE `Messages", p.level.name, "` SET Message=@Message WHERE X=", (int) x,
                            " AND Y=", (int) y, " AND Z=", (int) z), parameters);
            }

            IL_2E7:
            p.level.Blockchange(p, x, y, z, catchPos.type);
            Player.SendMessage(p, "Message block placed.");
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x06001221 RID: 4641 RVA: 0x000645F0 File Offset: 0x000627F0
        public void showMBs(Player p)
        {
            p.showMBs = !p.showMBs;
            DataTable dataTable;
            if (p.level.mapType == MapType.MyMap)
                dataTable = DBInterface.fillData("SELECT * FROM Messages WHERE Map=" + p.level.MapDbId);
            else
                dataTable = DBInterface.fillData("SELECT * FROM `Messages" + p.level.name + "`");
            if (p.showMBs)
            {
                int i;
                for (i = 0; i < dataTable.Rows.Count; i++)
                    p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["X"].ToString()),
                        ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                        ushort.Parse(dataTable.Rows[i]["Z"].ToString()), 130);
                Player.SendMessage(p, string.Format("Now showing &a{0} MBs.", i + Server.DefaultColor));
            }
            else
            {
                for (var i = 0; i < dataTable.Rows.Count; i++)
                    p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["X"].ToString()),
                        ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                        ushort.Parse(dataTable.Rows[i]["Z"].ToString()),
                        p.level.GetTile(ushort.Parse(dataTable.Rows[i]["X"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                            ushort.Parse(dataTable.Rows[i]["Z"].ToString())));
                Player.SendMessage(p, "Now hiding MBs.");
            }

            dataTable.Dispose();
        }

        // Token: 0x02000277 RID: 631
        private struct CatchPos
        {
            // Token: 0x0400090E RID: 2318
            public string message;

            // Token: 0x0400090F RID: 2319
            public byte type;
        }
    }
}