using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200012D RID: 301
    public class CmdFarewell : Command
    {
        // Token: 0x17000422 RID: 1058
        // (get) Token: 0x060008F9 RID: 2297 RVA: 0x0002CC34 File Offset: 0x0002AE34
        public override string name
        {
            get { return "farewell"; }
        }

        // Token: 0x17000423 RID: 1059
        // (get) Token: 0x060008FA RID: 2298 RVA: 0x0002CC3C File Offset: 0x0002AE3C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000424 RID: 1060
        // (get) Token: 0x060008FB RID: 2299 RVA: 0x0002CC44 File Offset: 0x0002AE44
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000425 RID: 1061
        // (get) Token: 0x060008FC RID: 2300 RVA: 0x0002CC4C File Offset: 0x0002AE4C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000426 RID: 1062
        // (get) Token: 0x060008FD RID: 2301 RVA: 0x0002CC50 File Offset: 0x0002AE50
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000427 RID: 1063
        // (get) Token: 0x060008FE RID: 2302 RVA: 0x0002CC54 File Offset: 0x0002AE54
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000428 RID: 1064
        // (get) Token: 0x060008FF RID: 2303 RVA: 0x0002CC58 File Offset: 0x0002AE58
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x06000900 RID: 2304 RVA: 0x0002CC5C File Offset: 0x0002AE5C
        public override void Use(Player p, string message)
        {
            if (!p.boughtFarewell)
            {
                Player.SendMessage(p, "In order to use /farewell you have to buy it in shop first.");
                return;
            }

            string queryString;
            if (message == "")
            {
                p.farewellMessage = "";
                Player.GlobalChat(null,
                    string.Format("{0} had his farewell message removed.",
                        p.color + p.PublicName + Server.DefaultColor), false);
                queryString = "UPDATE Players SET farewellMessage = '' WHERE Name = @Name";
                DBInterface.ExecuteQuery(queryString, new Dictionary<string, object>
                {
                    {
                        "@Name",
                        p.name
                    }
                });
                return;
            }

            if (message.Length > 37)
            {
                Player.SendMessage(p, "Welcome message must be under 37 letters.");
                return;
            }

            Player.GlobalChat(null,
                string.Format("{0} was given the farewell message: {1}.", p.color + p.PublicName + Server.DefaultColor,
                    message), false);
            queryString = "UPDATE Players SET farewellMessage = @Farewell WHERE Name = @Name";
            DBInterface.ExecuteQuery(queryString, new Dictionary<string, object>
            {
                {
                    "@Farewell",
                    message
                },
                {
                    "@Name",
                    p.name
                }
            });
            p.farewellMessage = message;
            p.boughtFarewell = false;
        }

        // Token: 0x06000901 RID: 2305 RVA: 0x0002CD60 File Offset: 0x0002AF60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/farewell [message] - Gives you the custom message on disconnecting.");
            Player.SendMessage(p, "If no [message] is given, your farewell message is set to default.");
        }
    }
}