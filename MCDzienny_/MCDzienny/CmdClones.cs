using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    class CmdClones : Command
    {
        public override string name { get { return "clones"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }
                message = p.name;
            }
            string arg = message.ToLower();
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player. Searching Player DB.");
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@Name", message);
                using (DataTable dataTable = DBInterface.fillData("SELECT IP FROM Players WHERE Name = @Name", dictionary))
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        Player.SendMessage(p, "Could not find any player by the name entered.");
                        return;
                    }
                    message = dataTable.Rows[0]["IP"].ToString();
                }
            }
            else
            {
                message = player.ip;
            }
            var list = new List<string>();
            var dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("@IP", message);
            using (DataTable dataTable2 = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP", dictionary2))
            {
                if (dataTable2.Rows.Count == 0)
                {
                    Player.SendMessage(p, "Could not find any record of the player entered.");
                    return;
                }
                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    if (!list.Contains(dataTable2.Rows[i]["Name"].ToString().ToLower()))
                    {
                        list.Add(dataTable2.Rows[i]["Name"].ToString().ToLower());
                    }
                }
            }
            if (list.Count <= 1)
            {
                Player.SendMessage(p, string.Format("{0} has no clones.", arg));
                return;
            }
            Player.SendMessage(p, "These people have the same IP address:");
            Player.SendMessage(p, string.Join(", ", list.ToArray()));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clones <name> - Finds everyone with the same IP has <name>");
        }
    }
}