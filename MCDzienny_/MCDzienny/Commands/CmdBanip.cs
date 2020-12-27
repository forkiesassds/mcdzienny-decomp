using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    // Token: 0x02000253 RID: 595
    public class CmdBanip : Command
    {
        // Token: 0x17000639 RID: 1593
        // (get) Token: 0x06001146 RID: 4422 RVA: 0x0005E204 File Offset: 0x0005C404
        public override string name
        {
            get { return "banip"; }
        }

        // Token: 0x1700063A RID: 1594
        // (get) Token: 0x06001147 RID: 4423 RVA: 0x0005E20C File Offset: 0x0005C40C
        public override string shortcut
        {
            get { return "bi"; }
        }

        // Token: 0x1700063B RID: 1595
        // (get) Token: 0x06001148 RID: 4424 RVA: 0x0005E214 File Offset: 0x0005C414
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700063C RID: 1596
        // (get) Token: 0x06001149 RID: 4425 RVA: 0x0005E21C File Offset: 0x0005C41C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700063D RID: 1597
        // (get) Token: 0x0600114A RID: 4426 RVA: 0x0005E220 File Offset: 0x0005C420
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x1700063E RID: 1598
        // (get) Token: 0x0600114B RID: 4427 RVA: 0x0005E224 File Offset: 0x0005C424
        public override string CustomName
        {
            get { return Lang.Command.BanIPName; }
        }

        // Token: 0x0600114D RID: 4429 RVA: 0x0005E234 File Offset: 0x0005C434
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
                var player = Player.Find(message);
                if (player == null)
                {
                    var num = 0;
                    DataTable dataTable;
                    try
                    {
                        dataTable = DBInterface.fillData("SELECT IP FROM Players WHERE Name = @Name",
                            new Dictionary<string, object>
                            {
                                {
                                    "@Name",
                                    message
                                }
                            });
                    }
                    catch (Exception ex)
                    {
                        if (num < 10) num++;
                        Server.ErrorLog(ex);
                        return;
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        message = dataTable.Rows[0]["IP"].ToString();
                        dataTable.Dispose();
                        goto IL_E6;
                    }

                    Player.SendMessage(p, Lang.Command.BanIPMessage);
                    return;
                }

                message = player.ip;
            }
            else
            {
                var player2 = Player.Find(message);
                if (player2 != null) message = player2.ip;
            }

            IL_E6:
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
                Player.IRCSay(string.Format(Lang.Command.BanIPMessage6, message.ToLower(), p.name));
            else
                Player.IRCSay(string.Format(Lang.Command.BanIPMessage7, message.ToLower()));
            Server.bannedIP.Add(message);
            Server.bannedIP.Save("banned-ip.txt", false);
            Server.s.Log("IP-BANNED: " + message.ToLower());
        }

        // Token: 0x0600114E RID: 4430 RVA: 0x0005E448 File Offset: 0x0005C648
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BanIPHelp);
        }
    }
}