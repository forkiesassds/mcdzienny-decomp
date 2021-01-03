using System;
using System.IO;
using System.Threading;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x02000078 RID: 120
    public class CmdReport : Command
    {
        // Token: 0x040001BD RID: 445
        public bool pending;

        // Token: 0x170000D9 RID: 217
        // (get) Token: 0x06000317 RID: 791 RVA: 0x0001188C File Offset: 0x0000FA8C
        public override string name
        {
            get { return "report"; }
        }

        // Token: 0x170000DA RID: 218
        // (get) Token: 0x06000318 RID: 792 RVA: 0x00011894 File Offset: 0x0000FA94
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000DB RID: 219
        // (get) Token: 0x06000319 RID: 793 RVA: 0x0001189C File Offset: 0x0000FA9C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170000DC RID: 220
        // (get) Token: 0x0600031A RID: 794 RVA: 0x000118A4 File Offset: 0x0000FAA4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000DD RID: 221
        // (get) Token: 0x0600031B RID: 795 RVA: 0x000118A8 File Offset: 0x0000FAA8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x0600031D RID: 797 RVA: 0x000118B4 File Offset: 0x0000FAB4
        public override void Use(Player p, string message)
        {
            string level = null;
            var x = 0;
            var y = 0;
            var z = 0;
            var message2 = new Message(message);
            if (message2.Count > 0 && message2.Count < 3)
            {
                Help(p);
                return;
            }

            message2.ReadString();
            var player = Player.Find(message2.ReadString());
            var text = message2.ReadToEnd();
            if (text == "check")
            {
                pending = false;
                Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " has checked the crime scene out.");
                return;
            }

            if (message.Split(' ').Length >= 3)
            {
                if (!File.Exists("extra/report.txt"))
                {
                    File.CreateText("extra/report.txt");
                }
                else
                {
                    Player.GlobalMessage(string.Concat(p.color, p.name, Server.DefaultColor, " has reported ",
                        player.color, player.name));
                    Player.GlobalMessage(Server.DefaultColor + "Reason: " + text);
                    File.AppendAllText("extra/report.txt",
                        string.Concat(Environment.NewLine, player.name, " was reported by ", p.name, ". Reason: ",
                            text));
                    pending = true;
                    x = p.pos[0] / 32;
                    y = p.pos[1] / 32;
                    z = p.pos[2] / 32;
                    level = p.level.name;
                }

                while (pending)
                {
                    Player.players.ForEach(delegate(Player who)
                    {
                        if (who.group.Permission >= LevelPermission.Operator)
                        {
                            Player.SendMessage(who,
                                string.Concat("There is a report stationed at ", x.ToString(), " ", y.ToString(), " ",
                                    z.ToString(), "."));
                            Player.SendMessage(who, "It is in the map " + level + ".");
                        }
                    });
                    Thread.Sleep(5000);
                }
            }
        }

        // Token: 0x0600031E RID: 798 RVA: 0x00011ACC File Offset: 0x0000FCCC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/report [type] [name] [reason]");
            Player.SendMessage(p, "[type] can be check or make");
        }
    }
}