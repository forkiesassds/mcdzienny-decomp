using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000293 RID: 659
    internal class CmdRules : Command
    {
        // Token: 0x1700070C RID: 1804
        // (get) Token: 0x060012DF RID: 4831 RVA: 0x000684CC File Offset: 0x000666CC
        public override string name
        {
            get { return "rules"; }
        }

        // Token: 0x1700070D RID: 1805
        // (get) Token: 0x060012E0 RID: 4832 RVA: 0x000684D4 File Offset: 0x000666D4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700070E RID: 1806
        // (get) Token: 0x060012E1 RID: 4833 RVA: 0x000684DC File Offset: 0x000666DC
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700070F RID: 1807
        // (get) Token: 0x060012E2 RID: 4834 RVA: 0x000684E4 File Offset: 0x000666E4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000710 RID: 1808
        // (get) Token: 0x060012E3 RID: 4835 RVA: 0x000684E8 File Offset: 0x000666E8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060012E4 RID: 4836 RVA: 0x000684EC File Offset: 0x000666EC
        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message != "")
            {
                if (p.group.Permission <= LevelPermission.Admin)
                {
                    Player.SendMessage(p, "You are not allowed to send /rules to another player!");
                    return;
                }

                player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Couldn't find a player named " + message);
                    return;
                }
            }
            else
            {
                player = p;
            }

            if (!File.Exists("text/rules.txt")) File.WriteAllText("text/rules.txt", "No rules entered yet!");
            var list = new List<string>();
            using (var streamReader = File.OpenText("text/rules.txt"))
            {
                while (!streamReader.EndOfStream) list.Add(streamReader.ReadLine());
            }

            Player.SendMessage(player, "Rules:");
            foreach (var message2 in list) Player.SendMessage(player, message2);
        }

        // Token: 0x060012E5 RID: 4837 RVA: 0x000685E8 File Offset: 0x000667E8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rules - displays server rules.");
            Player.SendMessage(p, "/rules [player] - sends rules to a player.");
        }
    }
}