using System;

namespace MCDzienny
{
    public class CmdTempBan : Command
    {
        public override string name { get { return "tempban"; } }

        public override string shortcut { get { return "tb"; } }

        public override string type { get { return "moderation"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message.IndexOf(' ') == -1)
            {
                message += " 30";
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player");
                return;
            }
            if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot ban someone of the same rank");
                return;
            }
            int num;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, "Invalid minutes");
                return;
            }
            if (num > 60)
            {
                Player.SendMessage(p, "Cannot ban for more than an hour");
                return;
            }
            if (num < 1)
            {
                Player.SendMessage(p, "Cannot ban someone for less than a minute");
                return;
            }
            Server.TempBan item = default(Server.TempBan);
            item.name = player.name;
            item.allowedJoin = DateTime.Now.AddMinutes(num);
            Server.tempBans.Add(item);
            player.Kick(string.Format("Banned for {0} minutes!", num));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tempban <name> <minutes> - Bans <name> for <minutes>");
            Player.SendMessage(p, "Max time is 60. Default is 30");
            Player.SendMessage(p, "Temp bans will reset on server restart");
        }
    }
}