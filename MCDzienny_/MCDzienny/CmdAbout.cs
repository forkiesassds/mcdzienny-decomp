using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdAbout : Command
    {
        public override string name { get { return "about"; } }

        public override string shortcut { get { return "b"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return Lang.Command.AboutName; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, Lang.Command.CmdAboutBreakForInfo);
            p.ClearBlockchange();
            p.Blockchange += AboutBlockchange;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.CmdAboutHelp);
        }

        public void AboutBlockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
            byte tile = p.level.GetTile(x, y, z);
            if (tile == byte.MaxValue)
            {
                Player.SendMessage(p, string.Format(Lang.Command.CmdAboutInvalidBlock, x, y, z));
                return;
            }
            p.SendBlockchange(x, y, z, tile);
            string text = string.Format(Lang.Command.CmdAboutBlock, x, y, z);
            object obj = text;
            text = string.Concat(obj, "&f", tile, " = ", Block.Name(tile));
            Player.SendMessage(p, text + Server.DefaultColor + ".");
            text = p.level.foundInfo(x, y, z);
            if (text != "")
            {
                Player.SendMessage(p, string.Format(Lang.Command.CmdAboutPhysics, text));
            }
            DataTable dataTable = null;
            dataTable = p.level.mapType != MapType.MyMap
                ? DBInterface.fillData("SELECT * FROM `Block" + p.level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z)
                : DBInterface.fillData(string.Format("SELECT * FROM Blocks WHERE Map = {0} AND X= {1} AND Y={2} AND Z={3};", p.level.MapDbId, x, y, z));
            bool flag = false;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                flag = true;
                string text2 = dataTable.Rows[i]["Username"].ToString();
                string arg = DateTime.Parse(dataTable.Rows[i]["TimePerformed"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string arg2 = Block.Name(byte.Parse(dataTable.Rows[i]["Type"].ToString()));
                if (!(dataTable.Rows[i]["Deleted"].ToString() == "1") && 0 == 0)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.CmdAboutCreatedBy, Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                }
                else
                {
                    Player.SendMessage(p, string.Format(Lang.Command.CmdAboutDestroyedBy, Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                }
                Player.SendMessage(p, string.Format(Lang.Command.AboutDateOfModification, arg));
            }
            List<Level.BlockPos> list = p.level.blockCache.FindAll(bP => bP.x == x && bP.y == y && bP.z == z);
            for (int j = 0; j < list.Count; j++)
            {
                flag = true;
                bool deleted = list[j].deleted;
                string text2 = list[j].name;
                DateTime timePerformed = list[j].TimePerformed;
                string arg = timePerformed.ToString("yyyy-MM-dd HH:mm:ss");
                string arg2 = Block.Name(list[j].type);
                if (!deleted)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.CmdAboutCreatedBy, Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                }
                else
                {
                    Player.SendMessage(p, string.Format(Lang.Command.CmdAboutDestroyedBy, Server.FindColor(text2.Trim()) + text2.Trim() + Server.DefaultColor, arg2));
                }
                Player.SendMessage(p, string.Format(Lang.Command.AboutDateOfModification, arg));
            }
            if (!flag)
            {
                Player.SendMessage(p, Lang.Command.AboutNoModification);
            }
            dataTable.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}