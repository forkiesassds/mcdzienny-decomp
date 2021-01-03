using System;

namespace MCDzienny
{
    // Token: 0x02000279 RID: 633
    public class CmdMove : Command
    {
        // Token: 0x170006B1 RID: 1713
        // (get) Token: 0x0600122B RID: 4651 RVA: 0x000648BC File Offset: 0x00062ABC
        public override string name
        {
            get { return "move"; }
        }

        // Token: 0x170006B2 RID: 1714
        // (get) Token: 0x0600122C RID: 4652 RVA: 0x000648C4 File Offset: 0x00062AC4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006B3 RID: 1715
        // (get) Token: 0x0600122D RID: 4653 RVA: 0x000648CC File Offset: 0x00062ACC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170006B4 RID: 1716
        // (get) Token: 0x0600122E RID: 4654 RVA: 0x000648D4 File Offset: 0x00062AD4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006B5 RID: 1717
        // (get) Token: 0x0600122F RID: 4655 RVA: 0x000648D8 File Offset: 0x00062AD8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x06001231 RID: 4657 RVA: 0x000648E4 File Offset: 0x00062AE4
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2 || message.Split(' ').Length > 4)
            {
                Help(p);
                return;
            }

            if (message.Split(' ').Length != 2)
            {
                Player player;
                if (message.Split(' ').Length == 4)
                {
                    player = Player.Find(message.Split(' ')[0]);
                    if (player == null)
                    {
                        Player.SendMessage(p, "Could not find player specified");
                        return;
                    }

                    if (p != null && player.group.Permission > p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot move someone of greater rank");
                        return;
                    }

                    message = message.Substring(message.IndexOf(' ') + 1);
                }
                else
                {
                    player = p;
                }

                try
                {
                    var num = Convert.ToUInt16(message.Split(' ')[0]);
                    var num2 = Convert.ToUInt16(message.Split(' ')[1]);
                    var num3 = Convert.ToUInt16(message.Split(' ')[2]);
                    num *= 32;
                    num += 16;
                    num2 *= 32;
                    num2 += 32;
                    num3 *= 32;
                    num3 += 16;
                    player.SendPos(byte.MaxValue, num, num2, num3, p.rot[0], p.rot[1]);
                    if (p != player)
                        Player.SendMessage(p, string.Format("Moved {0}", player.color + player.PublicName));
                }
                catch
                {
                    Player.SendMessage(p, "Invalid co-ordinates");
                }

                return;
            }

            var player2 = Player.Find(message.Split(' ')[0]);
            var level = Level.Find(message.Split(' ')[1]);
            if (player2 == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }

            if (level == null)
            {
                Player.SendMessage(p, "Could not find level specified");
                return;
            }

            if (p != null && player2.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot move someone of greater rank");
                return;
            }

            all.Find("goto").Use(player2, level.name);
            if (player2.level == level)
            {
                Player.SendMessage(p,
                    string.Format("Sent {0} to {1}", player2.color + player2.PublicName + Server.DefaultColor,
                        level.name));
                return;
            }

            Player.SendMessage(p, string.Format("{0} is not loaded", level.name));
        }

        // Token: 0x06001232 RID: 4658 RVA: 0x00064BA8 File Offset: 0x00062DA8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/move <player> <map> <x> <y> <z> - Move <player>");
            Player.SendMessage(p, "<map> must be blank if x, y or z is used and vice versa");
        }
    }
}