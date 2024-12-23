using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace MCDzienny
{
    public class CmdUnbanip : Command
    {
        readonly Regex regex = new Regex("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

        public override string name { get { return "unbanip"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message[0] == '@')
            {
                message = message.Remove(0, 1).Trim();
                Player player = Player.Find(message);
                if (player == null)
                {
                    int num = 0;
                    DataTable dataTable;
                    while (true)
                    {
                        try
                        {
                            var dictionary = new Dictionary<string, object>();
                            dictionary.Add("@Name", message);
                            dataTable = DBInterface.fillData("SELECT IP FROM Players WHERE Name = @Name", dictionary);
                        }
                        catch (Exception ex)
                        {
                            num++;
                            if (num < 10)
                            {
                                continue;
                            }
                            Server.ErrorLog(ex);
                            Player.SendMessage(p, "There was a database error fetching the IP address.  It has been logged.");
                            return;
                        }
                        break;
                    }
                    if (dataTable.Rows.Count <= 0)
                    {
                        Player.SendMessage(p, "Unable to find an IP address for that user.");
                        return;
                    }
                    message = dataTable.Rows[0]["IP"].ToString();
                    dataTable.Dispose();
                }
                else
                {
                    message = player.ip;
                }
            }
            if (!regex.IsMatch(message))
            {
                Player.SendMessage(p, "Not a valid ip!");
                return;
            }
            if (p != null && p.ip == message)
            {
                Player.SendMessage(p, "You shouldn't be able to use this command...");
                return;
            }
            if (!Server.bannedIP.Contains(message))
            {
                Player.SendMessage(p, string.Format("{0} doesn't seem to be banned...", message));
                return;
            }
            Player.GlobalMessage(string.Format("{0} got &8unip-banned{1}!", message, Server.DefaultColor));
            Server.bannedIP.Remove(message);
            Server.bannedIP.Save("banned-ip.txt", console: false);
            Server.s.Log("IP-UNBANNED: " + message.ToLower());
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unbanip <ip> - Un-bans an ip.  Also accepts a player name when you use @ before the name.");
        }
    }
}