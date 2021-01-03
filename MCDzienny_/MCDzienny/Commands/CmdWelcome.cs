using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200012C RID: 300
    public class CmdWelcome : Command
    {
        // Token: 0x1700041B RID: 1051
        // (get) Token: 0x060008EF RID: 2287 RVA: 0x0002CAF4 File Offset: 0x0002ACF4
        public override string name
        {
            get { return "welcome"; }
        }

        // Token: 0x1700041C RID: 1052
        // (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002CAFC File Offset: 0x0002ACFC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700041D RID: 1053
        // (get) Token: 0x060008F1 RID: 2289 RVA: 0x0002CB04 File Offset: 0x0002AD04
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700041E RID: 1054
        // (get) Token: 0x060008F2 RID: 2290 RVA: 0x0002CB0C File Offset: 0x0002AD0C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700041F RID: 1055
        // (get) Token: 0x060008F3 RID: 2291 RVA: 0x0002CB10 File Offset: 0x0002AD10
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000420 RID: 1056
        // (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002CB14 File Offset: 0x0002AD14
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000421 RID: 1057
        // (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002CB18 File Offset: 0x0002AD18
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x060008F7 RID: 2295 RVA: 0x0002CB24 File Offset: 0x0002AD24
        public override void Use(Player p, string message)
        {
            if (!p.boughtWelcome)
            {
                Player.SendMessage(p, "In order to use /welcome you have to buy it in shop first.");
                return;
            }

            string queryString;
            if (message == "")
            {
                p.welcomeMessage = "";
                Player.GlobalChat(null,
                    p.color + p.PublicName + Server.DefaultColor + " had his welcome message removed.", false);
                queryString = "UPDATE Players SET welcomeMessage = '' WHERE Name = '" + p.name + "'";
                DBInterface.ExecuteQuery(queryString);
                return;
            }

            if (message.Length > 37)
            {
                Player.SendMessage(p, "Welcome message must be under 37 letters.");
                return;
            }

            Player.GlobalChat(null,
                string.Format("{0} was given the welcome message: {1}.", p.color + p.PublicName + Server.DefaultColor,
                    message), false);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@Welcome", message);
            dictionary.Add("@Name", p.name);
            queryString = "UPDATE Players SET welcomeMessage = @Welcome WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, dictionary);
            p.welcomeMessage = message;
            p.boughtWelcome = false;
        }

        // Token: 0x060008F8 RID: 2296 RVA: 0x0002CC1C File Offset: 0x0002AE1C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/welcome [message] - Gives you the custom message on joining server.");
            Player.SendMessage(p, "If no [message] is given, your welcome message is set to default.");
        }
    }
}