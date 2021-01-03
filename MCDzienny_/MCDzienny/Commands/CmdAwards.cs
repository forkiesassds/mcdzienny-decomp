using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200011D RID: 285
    public class CmdAwards : Command
    {
        // Token: 0x170003E1 RID: 993
        // (get) Token: 0x06000886 RID: 2182 RVA: 0x0002B728 File Offset: 0x00029928
        public override string name
        {
            get { return "awards"; }
        }

        // Token: 0x170003E2 RID: 994
        // (get) Token: 0x06000887 RID: 2183 RVA: 0x0002B730 File Offset: 0x00029930
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003E3 RID: 995
        // (get) Token: 0x06000888 RID: 2184 RVA: 0x0002B738 File Offset: 0x00029938
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003E4 RID: 996
        // (get) Token: 0x06000889 RID: 2185 RVA: 0x0002B740 File Offset: 0x00029940
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003E5 RID: 997
        // (get) Token: 0x0600088A RID: 2186 RVA: 0x0002B744 File Offset: 0x00029944
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x0600088B RID: 2187 RVA: 0x0002B748 File Offset: 0x00029948
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }

            var num = 0;
            var text = "";
            if (message != "")
            {
                if (message.Split(' ').Length == 2)
                {
                    text = message.Split(' ')[0];
                    var player = Player.Find(text);
                    if (player != null) text = player.name;
                    try
                    {
                        num = int.Parse(message.Split(' ')[1]);
                        goto IL_E8;
                    }
                    catch
                    {
                        Help(p);
                        return;
                    }
                }

                if (message.Length <= 3)
                    try
                    {
                        num = int.Parse(message);
                        goto IL_E8;
                    }
                    catch
                    {
                        text = message;
                        var player2 = Player.Find(text);
                        if (player2 != null) text = player2.name;
                        goto IL_E8;
                    }

                text = message;
                var player3 = Player.Find(text);
                if (player3 != null) text = player3.name;
            }

            IL_E8:
            if (num < 0)
            {
                Player.SendMessage(p, "Cannot display pages less than 0");
            }
            else
            {
                var list = new List<Awards.awardData>();
                if (text == "")
                    list = Awards.allAwards;
                else
                    foreach (var awardName in Awards.getPlayersAwards(text))
                        list.Add(new Awards.awardData
                        {
                            awardName = awardName,
                            description = Awards.getDescription(awardName)
                        });
                if (list.Count == 0)
                {
                    if (text != "")
                    {
                        Player.SendMessage(p, "The player has no awards!");
                        return;
                    }

                    Player.SendMessage(p, "There are no awards in this server yet");
                }
                else
                {
                    var num2 = num * 5;
                    var num3 = (num - 1) * 5;
                    if (num3 > list.Count)
                    {
                        Player.SendMessage(p, "There aren't that many awards. Enter a smaller number");
                        return;
                    }

                    if (num2 > list.Count) num2 = list.Count;
                    if (text != "")
                        Player.SendMessage(p,
                            string.Format("{0} has the following awards:",
                                Server.FindColor(text) + text + Server.DefaultColor));
                    else
                        Player.SendMessage(p, "Awards available: ");
                    if (num == 0)
                    {
                        foreach (var awardData in list)
                            Player.SendMessage(p,
                                string.Format("&6{0}:%7 {1}", awardData.awardName, awardData.description));
                        if (list.Count > 8)
                        {
                            Player.SendMessage(p,
                                string.Format("&5Use &b/awards {0} 1/2/3/... &5for a more ordered list", message));
                            return;
                        }
                    }
                    else
                    {
                        for (var i = num3; i < num2; i++)
                        {
                            var awardData2 = list[i];
                            Player.SendMessage(p, "&6" + awardData2.awardName + ": &7" + awardData2.description);
                        }
                    }
                }
            }
        }

        // Token: 0x0600088C RID: 2188 RVA: 0x0002BA40 File Offset: 0x00029C40
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/awards [player] - Gives a full list of awards");
            Player.SendMessage(p, "If [player] is specified, shows awards for that player");
            Player.SendMessage(p, "Use 1/2/3/... to get an ordered list");
        }
    }
}