using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000106 RID: 262
    public class CmdTitle : Command
    {
        // Token: 0x1700039C RID: 924
        // (get) Token: 0x06000804 RID: 2052 RVA: 0x00028684 File Offset: 0x00026884
        public override string name
        {
            get { return "title"; }
        }

        // Token: 0x1700039D RID: 925
        // (get) Token: 0x06000805 RID: 2053 RVA: 0x0002868C File Offset: 0x0002688C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700039E RID: 926
        // (get) Token: 0x06000806 RID: 2054 RVA: 0x00028694 File Offset: 0x00026894
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700039F RID: 927
        // (get) Token: 0x06000807 RID: 2055 RVA: 0x0002869C File Offset: 0x0002689C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003A0 RID: 928
        // (get) Token: 0x06000808 RID: 2056 RVA: 0x000286A0 File Offset: 0x000268A0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170003A1 RID: 929
        // (get) Token: 0x06000809 RID: 2057 RVA: 0x000286A4 File Offset: 0x000268A4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170003A2 RID: 930
        // (get) Token: 0x0600080A RID: 2058 RVA: 0x000286A8 File Offset: 0x000268A8
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x0600080C RID: 2060 RVA: 0x000286B4 File Offset: 0x000268B4
        public override void Use(Player p, string message)
        {
            message = message.Replace("'", "''");
            if (!p.boughtTitle)
            {
                Player.SendMessage(p, "In order to use /title you have to buy it in the shop first.");
                return;
            }

            var flag = false;
            Player.OnPlayerChatEvent(p, ref message, ref flag);
            if (flag) return;
            var text = message;
            string queryString;
            if (text == "")
            {
                p.title = "";
                p.SetPrefix();
                Player.GlobalChat(null,
                    string.Format("{0} had their title removed.", p.color + p.PublicName + Server.DefaultColor), false);
                queryString = "UPDATE Players SET Title = '' WHERE Name = '" + p.name + "'";
                DBInterface.ExecuteQuery(queryString);
                return;
            }

            text += p.color;
            if (text.Length > 17)
            {
                Player.SendMessage(p, "Title must be under 17 letters.");
                return;
            }

            if (!Server.devs.Contains(p.name) && (Server.devs.Contains(p.name) || text.ToLower() == "dev"))
            {
                Player.SendMessage(p, "Can't let you do that, starfox.");
                return;
            }

            if (text != "")
                Player.GlobalChat(null,
                    string.Format("{0} was given the title of &b[{1}&b]", p.color + p.PublicName + Server.DefaultColor,
                        text), false);
            else
                Player.GlobalChat(null,
                    string.Format("{0} had their title removed.",
                        p.color + p.prefix + p.PublicName + Server.DefaultColor), false);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Title", text);
            dictionary.Add("@Name", p.name);
            queryString = "UPDATE Players SET Title = @Title WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, dictionary);
            p.title = text;
            p.SetPrefix();
            p.boughtTitle = false;
        }

        // Token: 0x0600080D RID: 2061 RVA: 0x00028870 File Offset: 0x00026A70
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title [title] - Gives you the [title].");
            Player.SendMessage(p, "If no [title] is given, your title is removed.");
        }
    }
}