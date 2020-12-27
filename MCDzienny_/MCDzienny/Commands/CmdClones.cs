using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002D5 RID: 725
    internal class CmdClones : Command
    {
        // Token: 0x170007EA RID: 2026
        // (get) Token: 0x06001484 RID: 5252 RVA: 0x00071754 File Offset: 0x0006F954
        public override string name
        {
            get { return "clones"; }
        }

        // Token: 0x170007EB RID: 2027
        // (get) Token: 0x06001485 RID: 5253 RVA: 0x0007175C File Offset: 0x0006F95C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007EC RID: 2028
        // (get) Token: 0x06001486 RID: 5254 RVA: 0x00071764 File Offset: 0x0006F964
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x170007ED RID: 2029
        // (get) Token: 0x06001487 RID: 5255 RVA: 0x0007176C File Offset: 0x0006F96C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007EE RID: 2030
        // (get) Token: 0x06001488 RID: 5256 RVA: 0x00071770 File Offset: 0x0006F970
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x06001489 RID: 5257 RVA: 0x00071774 File Offset: 0x0006F974
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

            var arg = message.ToLower();
            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player. Searching Player DB.");
                using (var dataTable = DBInterface.fillData("SELECT IP FROM Players WHERE Name = @Name",
                    new Dictionary<string, object>
                    {
                        {
                            "@Name",
                            message
                        }
                    }))
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        Player.SendMessage(p, "Could not find any player by the name entered.");
                        return;
                    }

                    message = dataTable.Rows[0]["IP"].ToString();
                    goto IL_AA;
                }
            }

            message = player.ip;
            IL_AA:
            var list = new List<string>();
            using (var dataTable2 = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP",
                new Dictionary<string, object>
                {
                    {
                        "@IP",
                        message
                    }
                }))
            {
                if (dataTable2.Rows.Count == 0)
                {
                    Player.SendMessage(p, "Could not find any record of the player entered.");
                    return;
                }

                for (var i = 0; i < dataTable2.Rows.Count; i++)
                    if (!list.Contains(dataTable2.Rows[i]["Name"].ToString().ToLower()))
                        list.Add(dataTable2.Rows[i]["Name"].ToString().ToLower());
            }

            if (list.Count > 1)
            {
                Player.SendMessage(p, "These people have the same IP address:");
                Player.SendMessage(p, string.Join(", ", list.ToArray()));
                return;
            }

            Player.SendMessage(p, string.Format("{0} has no clones.", arg));
        }

        // Token: 0x0600148A RID: 5258 RVA: 0x0007194C File Offset: 0x0006FB4C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clones <name> - Finds everyone with the same IP has <name>");
        }
    }
}