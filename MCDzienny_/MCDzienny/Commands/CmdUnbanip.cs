using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace MCDzienny
{
    // Token: 0x020002A6 RID: 678
    public class CmdUnbanip : Command
    {
        // Token: 0x04000950 RID: 2384
        private readonly Regex regex =
            new Regex(
                "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

        // Token: 0x17000766 RID: 1894
        // (get) Token: 0x06001376 RID: 4982 RVA: 0x0006AD90 File Offset: 0x00068F90
        public override string name
        {
            get { return "unbanip"; }
        }

        // Token: 0x17000767 RID: 1895
        // (get) Token: 0x06001377 RID: 4983 RVA: 0x0006AD98 File Offset: 0x00068F98
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000768 RID: 1896
        // (get) Token: 0x06001378 RID: 4984 RVA: 0x0006ADA0 File Offset: 0x00068FA0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000769 RID: 1897
        // (get) Token: 0x06001379 RID: 4985 RVA: 0x0006ADA8 File Offset: 0x00068FA8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700076A RID: 1898
        // (get) Token: 0x0600137A RID: 4986 RVA: 0x0006ADAC File Offset: 0x00068FAC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600137C RID: 4988 RVA: 0x0006ADC8 File Offset: 0x00068FC8
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
                var player = Player.Find(message);
                if (player == null)
                {
                    var num = 0;
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
                            if (num < 10) continue;
                            Server.ErrorLog(ex);
                            Player.SendMessage(p,
                                "There was a database error fetching the IP address.  It has been logged.");
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
            Server.bannedIP.Save("banned-ip.txt", false);
            Server.s.Log("IP-UNBANNED: " + message.ToLower());
        }

        // Token: 0x0600137D RID: 4989 RVA: 0x0006AF54 File Offset: 0x00069154
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/unbanip <ip> - Un-bans an ip.  Also accepts a player name when you use @ before the name.");
        }
    }
}