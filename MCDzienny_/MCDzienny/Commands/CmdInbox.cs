using System;

namespace MCDzienny
{
    // Token: 0x020002C7 RID: 711
    public class CmdInbox : Command
    {
        // Token: 0x170007C1 RID: 1985
        // (get) Token: 0x06001431 RID: 5169 RVA: 0x0006FD1C File Offset: 0x0006DF1C
        public override string name
        {
            get { return "inbox"; }
        }

        // Token: 0x170007C2 RID: 1986
        // (get) Token: 0x06001432 RID: 5170 RVA: 0x0006FD24 File Offset: 0x0006DF24
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007C3 RID: 1987
        // (get) Token: 0x06001433 RID: 5171 RVA: 0x0006FD2C File Offset: 0x0006DF2C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x170007C4 RID: 1988
        // (get) Token: 0x06001434 RID: 5172 RVA: 0x0006FD34 File Offset: 0x0006DF34
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007C5 RID: 1989
        // (get) Token: 0x06001435 RID: 5173 RVA: 0x0006FD38 File Offset: 0x0006DF38
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170007C6 RID: 1990
        // (get) Token: 0x06001436 RID: 5174 RVA: 0x0006FD3C File Offset: 0x0006DF3C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001437 RID: 5175 RVA: 0x0006FD40 File Offset: 0x0006DF40
        public override void Use(Player p, string message)
        {
            try
            {
                DBInterface.ExecuteQuery(string.Format(
                    "CREATE TABLE if not exists `Inbox{0}` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));",
                    p.name));
                if (message == "")
                    using (var dataTable =
                        DBInterface.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent"))
                    {
                        if (dataTable.Rows.Count == 0)
                        {
                            Player.SendMessage(p, "No messages found.");
                            return;
                        }

                        for (var i = 0; i < dataTable.Rows.Count; i++)
                            Player.SendMessage(p,
                                string.Format("{0}: From &5{1} at &a{2}", i,
                                    dataTable.Rows[i]["PlayerFrom"] + Server.DefaultColor,
                                    dataTable.Rows[i]["TimeSent"]));
                        goto IL_3B9;
                    }

                if (message.Split(' ')[0].ToLower() == "del" || message.Split(' ')[0].ToLower() == "delete")
                {
                    var num = -1;
                    if (message.Split(' ')[1].ToLower() != "all")
                    {
                        try
                        {
                            num = int.Parse(message.Split(' ')[1]);
                        }
                        catch
                        {
                            Player.SendMessage(p, "Incorrect number given.");
                            return;
                        }

                        if (num < 0)
                        {
                            Player.SendMessage(p, "Cannot delete records below 0");
                            return;
                        }
                    }

                    using (var dataTable2 =
                        DBInterface.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent"))
                    {
                        if (dataTable2.Rows.Count - 1 < num || dataTable2.Rows.Count == 0)
                        {
                            Player.SendMessage(p, string.Format("\"{0}\" does not exist.", num));
                            dataTable2.Dispose();
                            return;
                        }

                        string queryString;
                        if (num == -1)
                        {
                            if (Server.useMySQL)
                                queryString = "TRUNCATE TABLE `Inbox" + p.name + "`";
                            else
                                queryString = "DELETE FROM `Inbox" + p.name + "`";
                        }
                        else
                        {
                            queryString = string.Format(
                                "DELETE FROM `Inbox{0}` WHERE PlayerFrom='{1}' AND TimeSent='{2}'", p.name,
                                dataTable2.Rows[num]["PlayerFrom"],
                                Convert.ToDateTime(dataTable2.Rows[num]["TimeSent"]).ToString("yyyy-MM-dd HH:mm:ss"));
                        }

                        DBInterface.ExecuteQuery(queryString);
                        if (num == -1)
                            Player.SendMessage(p, "Deleted all messages.");
                        else
                            Player.SendMessage(p, "Deleted message.");
                        goto IL_3B9;
                    }
                }

                int num2;
                try
                {
                    num2 = int.Parse(message);
                }
                catch
                {
                    Player.SendMessage(p, "Incorrect number given.");
                    return;
                }

                if (num2 < 0)
                    Player.SendMessage(p, "Cannot read records below 0");
                else
                    using (var dataTable3 = DBInterface.fillData("SELECT * FROM Inbox" + p.name + " ORDER BY TimeSent"))
                    {
                        if (dataTable3.Rows.Count - 1 < num2 || dataTable3.Rows.Count == 0)
                        {
                            Player.SendMessage(p, string.Format("\"{0}\" does not exist.", num2));
                        }
                        else
                        {
                            Player.SendMessage(p,
                                string.Format("Message from &5{0} sent at &a{1}:",
                                    dataTable3.Rows[num2]["PlayerFrom"] + Server.DefaultColor,
                                    dataTable3.Rows[num2]["TimeSent"]));
                            Player.SendMessage(p, dataTable3.Rows[num2]["Contents"].ToString());
                        }
                    }

                IL_3B9: ;
            }
            catch (Exception)
            {
                Player.SendMessage(p, "Error accessing inbox. You may have no mail, try again.");
                throw;
            }
        }

        // Token: 0x06001438 RID: 5176 RVA: 0x000701AC File Offset: 0x0006E3AC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/inbox - Displays all your messages.");
            Player.SendMessage(p, "/inbox [num] - Displays the message at [num]");
            Player.SendMessage(p,
                "/inbox <del> [\"all\"/num] - Deletes the message at Num or All if \"all\" is given.");
        }
    }
}