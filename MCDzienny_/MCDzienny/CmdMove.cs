using System;

namespace MCDzienny
{
    public class CmdMove : Command
    {
        public override string name { get { return "move"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2 || message.Split(' ').Length > 4)
            {
                Help(p);
                return;
            }
            if (message.Split(' ').Length == 2)
            {
                Player player = Player.Find(message.Split(' ')[0]);
                Level level = Level.Find(message.Split(' ')[1]);
                if (player == null)
                {
                    Player.SendMessage(p, "Could not find player specified");
                    return;
                }
                if (level == null)
                {
                    Player.SendMessage(p, "Could not find level specified");
                    return;
                }
                if (p != null && player.group.Permission > p.group.Permission)
                {
                    Player.SendMessage(p, "Cannot move someone of greater rank");
                    return;
                }
                all.Find("goto").Use(player, level.name);
                if (player.level == level)
                {
                    Player.SendMessage(p, string.Format("Sent {0} to {1}", player.color + player.PublicName + Server.DefaultColor, level.name));
                }
                else
                {
                    Player.SendMessage(p, string.Format("{0} is not loaded", level.name));
                }
                return;
            }
            Player player2;
            if (message.Split(' ').Length == 4)
            {
                player2 = Player.Find(message.Split(' ')[0]);
                if (player2 == null)
                {
                    Player.SendMessage(p, "Could not find player specified");
                    return;
                }
                if (p != null && player2.group.Permission > p.group.Permission)
                {
                    Player.SendMessage(p, "Cannot move someone of greater rank");
                    return;
                }
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            else
            {
                player2 = p;
            }
            try
            {
                ushort num = Convert.ToUInt16(message.Split(' ')[0]);
                ushort num2 = Convert.ToUInt16(message.Split(' ')[1]);
                ushort num3 = Convert.ToUInt16(message.Split(' ')[2]);
                num *= 32;
                num += 16;
                num2 *= 32;
                num2 += 32;
                num3 *= 32;
                num3 += 16;
                player2.SendPos(byte.MaxValue, num, num2, num3, p.rot[0], p.rot[1]);
                if (p != player2)
                {
                    Player.SendMessage(p, string.Format("Moved {0}", player2.color + player2.PublicName));
                }
            }
            catch
            {
                Player.SendMessage(p, "Invalid co-ordinates");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/move <player> <map> <x> <y> <z> - Move <player>");
            Player.SendMessage(p, "<map> must be blank if x, y or z is used and vice versa");
        }
    }
}