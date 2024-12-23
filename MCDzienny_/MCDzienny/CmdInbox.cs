using System;
using System.Data;

namespace MCDzienny
{
    public class CmdInbox : Command
    {
        public override string name { get { return "inbox"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            try
            {
                DBInterface.ExecuteQuery(string.Format("CREATE TABLE if not exists `Inbox{0}` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));", p.name));
                if (message == "")
                {
                    using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent"))
                    {
                        if (dataTable.Rows.Count == 0)
                        {
                            Player.SendMessage(p, "No messages found.");
                        }
                        else
                        {
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                Player.SendMessage(
                                    p, string.Format("{0}: From &5{1} at &a{2}", i, dataTable.Rows[i]["PlayerFrom"] + Server.DefaultColor, dataTable.Rows[i]["TimeSent"]));
                            }
                        }
                        return;
                    }
                }
                if (message.Split(' ')[0].ToLower() == "del" || message.Split(' ')[0].ToLower() == "delete")
                {
                    int num = -1;
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
                    using (DataTable dataTable2 = DBInterface.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent"))
                    {
                        if (dataTable2.Rows.Count - 1 < num || dataTable2.Rows.Count == 0)
                        {
                            Player.SendMessage(p, string.Format("\"{0}\" does not exist.", num));
                            dataTable2.Dispose();
                            return;
                        }
                        string queryString = num != -1
                            ? string.Format("DELETE FROM `Inbox{0}` WHERE PlayerFrom='{1}' AND TimeSent='{2}'", p.name, dataTable2.Rows[num]["PlayerFrom"],
                                            Convert.ToDateTime(dataTable2.Rows[num]["TimeSent"]).ToString("yyyy-MM-dd HH:mm:ss"))
                            : !Server.useMySQL
                                ? "DELETE FROM `Inbox" + p.name + "`"
                                : "TRUNCATE TABLE `Inbox" + p.name + "`";
                        DBInterface.ExecuteQuery(queryString);
                        if (num == -1)
                        {
                            Player.SendMessage(p, "Deleted all messages.");
                        }
                        else
                        {
                            Player.SendMessage(p, "Deleted message.");
                        }
                        return;
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
                {
                    Player.SendMessage(p, "Cannot read records below 0");
                    return;
                }
                using (DataTable dataTable3 = DBInterface.fillData("SELECT * FROM Inbox" + p.name + " ORDER BY TimeSent"))
                {
                    if (dataTable3.Rows.Count - 1 < num2 || dataTable3.Rows.Count == 0)
                    {
                        Player.SendMessage(p, string.Format("\"{0}\" does not exist.", num2));
                        return;
                    }
                    Player.SendMessage(
                        p,
                        string.Format("Message from &5{0} sent at &a{1}:", string.Concat(dataTable3.Rows[num2]["PlayerFrom"], Server.DefaultColor),
                                      dataTable3.Rows[num2]["TimeSent"]));
                    Player.SendMessage(p, dataTable3.Rows[num2]["Contents"].ToString());
                }
            }
            catch (Exception)
            {
                Player.SendMessage(p, "Error accessing inbox. You may have no mail, try again.");
                throw;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/inbox - Displays all your messages.");
            Player.SendMessage(p, "/inbox [num] - Displays the message at [num]");
            Player.SendMessage(p, "/inbox <del> [\"all\"/num] - Deletes the message at Num or All if \"all\" is given.");
        }
    }
}