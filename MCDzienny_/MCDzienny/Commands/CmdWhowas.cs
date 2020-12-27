using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002AF RID: 687
    public class CmdWhowas : Command
    {
        // Token: 0x17000789 RID: 1929
        // (get) Token: 0x060013B7 RID: 5047 RVA: 0x0006C9A4 File Offset: 0x0006ABA4
        public override string name
        {
            get { return "whowas"; }
        }

        // Token: 0x1700078A RID: 1930
        // (get) Token: 0x060013B8 RID: 5048 RVA: 0x0006C9AC File Offset: 0x0006ABAC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700078B RID: 1931
        // (get) Token: 0x060013B9 RID: 5049 RVA: 0x0006C9B4 File Offset: 0x0006ABB4
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700078C RID: 1932
        // (get) Token: 0x060013BA RID: 5050 RVA: 0x0006C9BC File Offset: 0x0006ABBC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700078D RID: 1933
        // (get) Token: 0x060013BB RID: 5051 RVA: 0x0006C9C0 File Offset: 0x0006ABC0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060013BD RID: 5053 RVA: 0x0006C9CC File Offset: 0x0006ABCC
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.FindExact(message);
            if (player != null && !player.hidden)
            {
                Player.SendMessage(p,
                    string.Format("{0} is online, using /whois instead.",
                        player.color + player.PublicName + Server.DefaultColor));
                all.Find("whois").Use(p, message);
                return;
            }

            if (message.IndexOf("'") != -1)
            {
                Player.SendMessage(p, "Cannot parse request.");
                return;
            }

            var text = Group.findPlayer(message.ToLower());
            using (var dataTable = DBInterface.fillData("SELECT * FROM Players WHERE Name = @Name",
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
                    Player.SendMessage(p,
                        string.Format("{0} has the rank of {1}", Group.Find(text).color + message + Server.DefaultColor,
                            Group.Find(text).color + text));
                }
                else
                {
                    Player.SendMessage(p,
                        string.Concat(Group.Find(text).color, dataTable.Rows[0]["Title"], " ", message,
                            Server.DefaultColor, " has :"));
                    Player.SendMessage(p, string.Format("> > the rank of \"{0}\"", Group.Find(text).color + text));
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("pay") &&
                            !Group.Find("Nobody").commands.Contains("give") &&
                            !Group.Find("Nobody").commands.Contains("take"))
                            Player.SendMessage(p,
                                string.Concat("> > &a", dataTable.Rows[0]["Money"], Server.DefaultColor, " ",
                                    Server.moneys));
                    }
                    catch
                    {
                    }

                    Player.SendMessage(p,
                        string.Format("> > &cdied &a{0} times",
                            dataTable.Rows[0]["TotalDeaths"] + Server.DefaultColor));
                    Player.SendMessage(p,
                        string.Format("> > &bmodified &a{0} blocks.",
                            dataTable.Rows[0]["totalBlocks"] + Server.DefaultColor));
                    Player.SendMessage(p, string.Format("> > was last seen on &a{0}", dataTable.Rows[0]["LastLogin"]));
                    Player.SendMessage(p,
                        string.Format("> > first logged into the server on &a{0}", dataTable.Rows[0]["FirstLogin"]));
                    Player.SendMessage(p,
                        string.Format("> > logged in &a{0} times, &c{1} of which ended in a kick.",
                            dataTable.Rows[0]["totalLogin"] + Server.DefaultColor,
                            dataTable.Rows[0]["totalKicked"] + Server.DefaultColor));
                    var num = 0;
                    int.TryParse(dataTable.Rows[0]["totalMinutesPlayed"].ToString(), out num);
                    Player.SendMessage(p,
                        string.Format("> > Total time played: &a{0}",
                            num / 60 > 0
                                ? string.Format("{0} hours {1} minutes", num / 60, num % 60)
                                : string.Format("{0} minutes", num)));
                    if (p == null || p.group.Permission >= LevelPermission.Admin)
                    {
                        if (Server.bannedIP.Contains(dataTable.Rows[0]["IP"].ToString()))
                            dataTable.Rows[0]["IP"] = string.Format("&8{0}, which is banned", dataTable.Rows[0]["IP"]);
                        Player.SendMessage(p, string.Format("> > the IP of {0}", dataTable.Rows[0]["IP"]));
                        if (Server.useWhitelist && Server.whiteList != null &&
                            Server.whiteList.Contains(message.ToLower()))
                            Player.SendMessage(p, "> > Player is &fWhitelisted");
                        if (Server.devs.Contains(message.ToLower()))
                            Player.SendMessage(p, Server.DefaultColor + "> > Player is a &9Developer");
                    }
                }
            }
        }

        // Token: 0x060013BE RID: 5054 RVA: 0x0006CE8C File Offset: 0x0006B08C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }

        // Token: 0x020002B0 RID: 688
        private enum BugPlaceType
        {
            // Token: 0x04000955 RID: 2389
            None,

            // Token: 0x04000956 RID: 2390
            One,

            // Token: 0x04000957 RID: 2391
            Two,

            // Token: 0x04000958 RID: 2392
            Three,

            // Token: 0x04000959 RID: 2393
            Four,

            // Token: 0x0400095A RID: 2394
            Five,

            // Token: 0x0400095B RID: 2395
            Six,

            // Token: 0x0400095C RID: 2396
            Seven,

            // Token: 0x0400095D RID: 2397
            Eight,

            // Token: 0x0400095E RID: 2398
            Nine,

            // Token: 0x0400095F RID: 2399
            Ten,

            // Token: 0x04000960 RID: 2400
            Eleven,

            // Token: 0x04000961 RID: 2401
            Twelve
        }
    }
}