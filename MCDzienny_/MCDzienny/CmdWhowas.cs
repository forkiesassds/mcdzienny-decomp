using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdWhowas : Command
    {

        public override string name { get { return "whowas"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.FindExact(message);
            if (player != null && !player.hidden)
            {
                Player.SendMessage(p, string.Format("{0} is online, using /whois instead.", player.color + player.PublicName + Server.DefaultColor));
                all.Find("whois").Use(p, message);
                return;
            }
            if (message.IndexOf("'") != -1)
            {
                Player.SendMessage(p, "Cannot parse request.");
                return;
            }
            string text = Group.findPlayer(message.ToLower());
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Name", message);
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM Players WHERE Name = @Name", dictionary))
            {
                if (dataTable.Rows.Count == 0)
                {
                    Player.SendMessage(p, string.Format("{0} has the rank of {1}", Group.Find(text).color + message + Server.DefaultColor, Group.Find(text).color + text));
                    return;
                }
                Player.SendMessage(p, string.Concat(Group.Find(text).color, dataTable.Rows[0]["Title"], " ", message, Server.DefaultColor, " has :"));
                Player.SendMessage(p, string.Format("> > the rank of \"{0}\"", Group.Find(text).color + text));
                try
                {
                    if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") &&
                        !Group.Find("Nobody").commands.Contains("take"))
                    {
                        Player.SendMessage(p, string.Concat("> > &a", dataTable.Rows[0]["Money"], Server.DefaultColor, " ", Server.moneys));
                    }
                }
                catch {}
                Player.SendMessage(p, string.Format("> > &cdied &a{0} times", string.Concat(dataTable.Rows[0]["TotalDeaths"], Server.DefaultColor)));
                Player.SendMessage(p, string.Format("> > &bmodified &a{0} blocks.", string.Concat(dataTable.Rows[0]["totalBlocks"], Server.DefaultColor)));
                Player.SendMessage(p, string.Format("> > was last seen on &a{0}", dataTable.Rows[0]["LastLogin"]));
                Player.SendMessage(p, string.Format("> > first logged into the server on &a{0}", dataTable.Rows[0]["FirstLogin"]));
                Player.SendMessage(
                    p,
                    string.Format("> > logged in &a{0} times, &c{1} of which ended in a kick.", string.Concat(dataTable.Rows[0]["totalLogin"], Server.DefaultColor),
                                  string.Concat(dataTable.Rows[0]["totalKicked"], Server.DefaultColor)));
                int result = 0;
                int.TryParse(dataTable.Rows[0]["totalMinutesPlayed"].ToString(), out result);
                Player.SendMessage(p, string.Format("> > Total time played: &a{0}", result / 60 > 0 ? string.Format("{0} hours {1} minutes", result / 60, result % 60) : string
                                                        .Format("{0} minutes", result)));
                if (p == null || p.group.Permission >= LevelPermission.Admin)
                {
                    if (Server.bannedIP.Contains(dataTable.Rows[0]["IP"].ToString()))
                    {
                        dataTable.Rows[0]["IP"] = string.Format("&8{0}, which is banned", dataTable.Rows[0]["IP"]);
                    }
                    Player.SendMessage(p, string.Format("> > the IP of {0}", dataTable.Rows[0]["IP"]));
                    if (Server.useWhitelist && Server.whiteList != null && Server.whiteList.Contains(message.ToLower()))
                    {
                        Player.SendMessage(p, "> > Player is &fWhitelisted");
                    }
                    if (Server.devs.Contains(message.ToLower()))
                    {
                        Player.SendMessage(p, Server.DefaultColor + "> > Player is a &9Developer");
                    }
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }

        enum BugPlaceType
        {
            None,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Eleven,
            Twelve
        }
    }
}