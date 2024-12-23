using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdWhoip : Command
    {
        public override string name { get { return "whoip"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message.IndexOf("'") != -1)
            {
                Player.SendMessage(p, "Cannot parse request.");
                return;
            }
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@IP", message);
            using (DataTable dataTable = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP", dictionary))
            {
                if (dataTable.Rows.Count == 0)
                {
                    Player.SendMessage(p, "Could not find anyone with this IP");
                    return;
                }
                string text = "Players with this IP: ";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    text = string.Concat(text, dataTable.Rows[i]["Name"], ", ");
                }
                text = text.Remove(text.Length - 2);
                Player.SendMessage(p, text);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whoip <ip address> - Displays players associated with a given IP address.");
        }
    }
}