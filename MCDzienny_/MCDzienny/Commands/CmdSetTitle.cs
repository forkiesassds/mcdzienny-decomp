using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002CA RID: 714
    public class CmdSetTitle : Command
    {
        // Token: 0x170007CE RID: 1998
        // (get) Token: 0x06001448 RID: 5192 RVA: 0x00070508 File Offset: 0x0006E708
        public override string name
        {
            get { return "settitle"; }
        }

        // Token: 0x170007CF RID: 1999
        // (get) Token: 0x06001449 RID: 5193 RVA: 0x00070510 File Offset: 0x0006E710
        public override string shortcut
        {
            get { return "st"; }
        }

        // Token: 0x170007D0 RID: 2000
        // (get) Token: 0x0600144A RID: 5194 RVA: 0x00070518 File Offset: 0x0006E718
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170007D1 RID: 2001
        // (get) Token: 0x0600144B RID: 5195 RVA: 0x00070520 File Offset: 0x0006E720
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007D2 RID: 2002
        // (get) Token: 0x0600144C RID: 5196 RVA: 0x00070524 File Offset: 0x0006E724
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600144D RID: 5197 RVA: 0x00070528 File Offset: 0x0006E728
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }

            var num = message.IndexOf(' ');
            string queryString;
            if (message.Split(' ').Length <= 1)
            {
                player.title = "";
                player.SetPrefix();
                Player.GlobalChat(null,
                    string.Format("{0} had their title removed.",
                        player.color + player.PublicName + Server.DefaultColor), false);
                queryString = "UPDATE Players SET Title = '' WHERE Name = '" + player.name + "'";
                DBInterface.ExecuteQuery(queryString);
                return;
            }

            var text = message.Substring(num + 1);
            if (text.Length > 17)
            {
                Player.SendMessage(p, "Title must be under 17 letters.");
                return;
            }

            if ((p == null || !Server.devs.Contains(p.name)) &&
                (Server.devs.Contains(player.name) || text.ToLower() == "dev"))
            {
                Player.SendMessage(p, "Can't let you do that, starfox.");
                return;
            }

            if (text != "")
                Player.GlobalChat(null,
                    string.Format("{0} was given the title of &b[{1}]",
                        player.color + player.PublicName + Server.DefaultColor, text), false);
            else
                Player.GlobalChat(null,
                    string.Format("{0} had their title removed.",
                        player.color + player.prefix + player.PublicName + Server.DefaultColor), false);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Title", text);
            dictionary.Add("@Name", player.name);
            queryString = "UPDATE Players SET Title = @Title WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, dictionary);
            player.title = text;
            player.SetPrefix();
        }

        // Token: 0x0600144E RID: 5198 RVA: 0x00070704 File Offset: 0x0006E904
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title <player> [title] - Gives <player> the [title].");
            Player.SendMessage(p, "If no [title] is given, the player's title is removed.");
        }
    }
}