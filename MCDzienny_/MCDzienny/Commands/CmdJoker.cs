using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200026D RID: 621
    public class CmdJoker : Command
    {
        // Token: 0x17000682 RID: 1666
        // (get) Token: 0x060011D4 RID: 4564 RVA: 0x00062B98 File Offset: 0x00060D98
        public override string name
        {
            get { return "joker"; }
        }

        // Token: 0x17000683 RID: 1667
        // (get) Token: 0x060011D5 RID: 4565 RVA: 0x00062BA0 File Offset: 0x00060DA0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000684 RID: 1668
        // (get) Token: 0x060011D6 RID: 4566 RVA: 0x00062BA8 File Offset: 0x00060DA8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000685 RID: 1669
        // (get) Token: 0x060011D7 RID: 4567 RVA: 0x00062BB0 File Offset: 0x00060DB0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000686 RID: 1670
        // (get) Token: 0x060011D8 RID: 4568 RVA: 0x00062BB4 File Offset: 0x00060DB4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060011DA RID: 4570 RVA: 0x00062BC0 File Offset: 0x00060DC0
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var flag = false;
            var text = "text/joker.txt";
            if (!File.Exists(text))
            {
                File.Create(text);
                Player.SendMessage(p, "The file 'joker.txt' is empty. It has to be filled with (funny) texts.");
                return;
            }

            var streamReader = new FileInfo(text).OpenText();
            var list = new List<string>();
            while (streamReader.Peek() != -1) list.Add(streamReader.ReadLine());
            if (list.Count == 0)
            {
                Player.SendMessage(p, "The file 'joker.txt' is empty. It has to be filled with (funny) texts.");
                return;
            }

            if (message[0] == '#')
            {
                message = message.Remove(0, 1).Trim();
                flag = true;
                Server.s.Log("Stealth joker attempted");
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }

            if (!player.joker)
            {
                player.joker = true;
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("{0} is now STEALTH joker'd. ",
                        player.color + player.name + Server.DefaultColor));
                    return;
                }

                Player.GlobalChat(null,
                    string.Format("{0} is now a &aJ&bo&ck&5e&9r{1}.",
                        player.color + player.PublicName + Server.DefaultColor, Server.DefaultColor), false);
            }
            else
            {
                player.joker = false;
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("{0} is now STEALTH Unjoker'd. ",
                        player.color + player.name + Server.DefaultColor));
                    return;
                }

                Player.GlobalChat(null,
                    string.Format("{0} is no longer a &aJ&bo&ck&5e&9r{1}.",
                        player.color + player.PublicName + Server.DefaultColor, Server.DefaultColor), false);
            }
        }

        // Token: 0x060011DB RID: 4571 RVA: 0x00062D54 File Offset: 0x00060F54
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/joker <name> - Causes a player to become a joker!");
            Player.SendMessage(p, "/joker # <name> - Makes the player a joker silently");
        }
    }
}