using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002ED RID: 749
    public class CmdWhoip : Command
    {
        // Token: 0x17000857 RID: 2135
        // (get) Token: 0x06001538 RID: 5432 RVA: 0x00074FA4 File Offset: 0x000731A4
        public override string name
        {
            get { return "whoip"; }
        }

        // Token: 0x17000858 RID: 2136
        // (get) Token: 0x06001539 RID: 5433 RVA: 0x00074FAC File Offset: 0x000731AC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000859 RID: 2137
        // (get) Token: 0x0600153A RID: 5434 RVA: 0x00074FB4 File Offset: 0x000731B4
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700085A RID: 2138
        // (get) Token: 0x0600153B RID: 5435 RVA: 0x00074FBC File Offset: 0x000731BC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700085B RID: 2139
        // (get) Token: 0x0600153C RID: 5436 RVA: 0x00074FC0 File Offset: 0x000731C0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600153E RID: 5438 RVA: 0x00074FCC File Offset: 0x000731CC
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

            using (var dataTable = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP",
                new Dictionary<string, object>
                {
                    {
                        "@IP",
                        message
                    }
                }))
            {
                if (dataTable.Rows.Count == 0)
                {
                    Player.SendMessage(p, "Could not find anyone with this IP");
                }
                else
                {
                    var text = "Players with this IP: ";
                    for (var i = 0; i < dataTable.Rows.Count; i++) text = text + dataTable.Rows[i]["Name"] + ", ";
                    text = text.Remove(text.Length - 2);
                    Player.SendMessage(p, text);
                }
            }
        }

        // Token: 0x0600153F RID: 5439 RVA: 0x000750B0 File Offset: 0x000732B0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whoip <ip address> - Displays players associated with a given IP address.");
        }
    }
}