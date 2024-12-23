using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class CmdBanip : Command
    {
        public override string name { get { return "banip"; } }

        public override string shortcut { get { return "bi"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override string CustomName { get { return Lang.Command.BanIPName; } }

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
                message = message.Replace("'", "");
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
                            return;
                        }
                        break;
                    }
                    if (dataTable.Rows.Count <= 0)
                    {
                        Player.SendMessage(p, Lang.Command.BanIPMessage);
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
            else
            {
                Player player2 = Player.Find(message);
                if (player2 != null)
                {
                    message = player2.ip;
                }
            }
            if (message.Equals("127.0.0.1"))
            {
                Player.SendMessage(p, Lang.Command.BanIPMessage1);
                return;
            }
            if (message.IndexOf('.') == -1)
            {
                Player.SendMessage(p, Lang.Command.BanIPMessage2);
                return;
            }
            if (message.Split('.').Length != 4)
            {
                Player.SendMessage(p, Lang.Command.BanIPMessage2);
                return;
            }
            if (p != null && p.ip == message)
            {
                Player.SendMessage(p, Lang.Command.BanIPMessage3);
                return;
            }
            if (Server.bannedIP.Contains(message))
            {
                Player.SendMessage(p, string.Format(Lang.Command.BanIPMessage4, message));
                return;
            }
            Player.GlobalMessage(string.Format(Lang.Command.BanIPMessage5, message));
            if (p != null)
            {
                Player.IRCSay(string.Format(Lang.Command.BanIPMessage6, message.ToLower(), p.name));
            }
            else
            {
                Player.IRCSay(string.Format(Lang.Command.BanIPMessage7, message.ToLower()));
            }
            Server.bannedIP.Add(message);
            Server.bannedIP.Save("banned-ip.txt", console: false);
            Server.s.Log("IP-BANNED: " + message.ToLower());
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BanIPHelp);
        }
    }
}