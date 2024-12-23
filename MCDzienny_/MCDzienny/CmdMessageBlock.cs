using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdMessageBlock : Command
    {

        public override string name { get { return "mb"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.message = "";
            try
            {
                switch (message.Split(' ')[0])
                {
                    case "air":
                        catchPos.type = 132;
                        break;
                    case "water":
                        catchPos.type = 133;
                        break;
                    case "lava":
                        catchPos.type = 134;
                        break;
                    case "black":
                        catchPos.type = 131;
                        break;
                    case "white":
                        catchPos.type = 130;
                        break;
                    case "show":
                        showMBs(p);
                        return;
                    default:
                        catchPos.type = 130;
                        catchPos.message = message;
                        break;
                }
            }
            catch
            {
                catchPos.type = 130;
                catchPos.message = message;
            }
            if (catchPos.message == "")
            {
                catchPos.message = message.Substring(message.IndexOf(' ') + 1);
            }
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place where you wish the message block to go.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mb [block] [message] - Places a message in your next block.");
            Player.SendMessage(p, "Valid blocks: white, black, air, water, lava");
            Player.SendMessage(p, "/mb show shows or hides MBs");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Message", catchPos.message);
            Dictionary<string, object> parameters = dictionary;
            if (p.level.mapType == MapType.MyMap)
            {
                using (DataTable dataTable = DBInterface.fillData(string.Format("SELECT * FROM Messages WHERE Map={0} AND X={1} AND Y={2} AND Z={3};", p.level.MapDbId, x,
                                                                                y, z)))
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        DBInterface.ExecuteQuery(
                            "INSERT INTO `Messages` (Map, X, Y, Z, Message) VALUES (" + p.level.MapDbId + ", " + x + ", " + y + ", " + z + ", @Message)", parameters);
                    }
                    else
                    {
                        DBInterface.ExecuteQuery("UPDATE `Messages` SET Message=@Message WHERE Map=" + p.level.MapDbId + "AND X=" + x + " AND Y=" + y + " AND Z=" + z,
                                                 parameters);
                    }
                }
            }
            else
            {
                using (DataTable dataTable2 =
                       DBInterface.fillData("SELECT * FROM `Messages" + p.level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z))
                {
                    if (dataTable2.Rows.Count == 0)
                    {
                        DBInterface.ExecuteQuery(
                            "INSERT INTO `Messages" + p.level.name + "` (X, Y, Z, Message) VALUES (" + (int)x + ", " + (int)y + ", " + (int)z + ", @Message)", parameters);
                    }
                    else
                    {
                        DBInterface.ExecuteQuery("UPDATE `Messages" + p.level.name + "` SET Message=@Message WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z,
                                                 parameters);
                    }
                }
            }
            p.level.Blockchange(p, x, y, z, catchPos.type);
            Player.SendMessage(p, "Message block placed.");
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        public void showMBs(Player p)
        {
            p.showMBs = !p.showMBs;
            DataTable dataTable = null;
            dataTable = p.level.mapType != MapType.MyMap ? DBInterface.fillData("SELECT * FROM `Messages" + p.level.name + "`")
                : DBInterface.fillData("SELECT * FROM Messages WHERE Map=" + p.level.MapDbId);
            if (p.showMBs)
            {
                int i;
                for (i = 0; i < dataTable.Rows.Count; i++)
                {
                    p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["X"].ToString()), ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                                      ushort.Parse(dataTable.Rows[i]["Z"].ToString()), 130);
                }
                Player.SendMessage(p, string.Format("Now showing &a{0} MBs.", i + Server.DefaultColor));
            }
            else
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    p.SendBlockchange(ushort.Parse(dataTable.Rows[i]["X"].ToString()), ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                                      ushort.Parse(dataTable.Rows[i]["Z"].ToString()),
                                      p.level.GetTile(ushort.Parse(dataTable.Rows[i]["X"].ToString()), ushort.Parse(dataTable.Rows[i]["Y"].ToString()),
                                                      ushort.Parse(dataTable.Rows[i]["Z"].ToString())));
                }
                Player.SendMessage(p, "Now hiding MBs.");
            }
            dataTable.Dispose();
        }

        struct CatchPos
        {
            public string message;

            public byte type;
        }
    }
}