using System;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    public class CmdMe : Command
    {
        public override string name { get { return "me"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.level.mapType == MapType.Zombie)
                {
                    HandleZombieMode(p);
                }
                else
                {
                    Player.SendMessage(p, "You");
                }
                return;
            }
            if (Server.voteMode)
            {
                Player.SendMessage(p, "You can't use this command during the voting. Wait a moment.");
                return;
            }
            if (p.muted || p.IsTempMuted)
            {
                Player.SendMessage(p, "You are currently muted and cannot use this command.");
                return;
            }
            if (Server.chatmod && !p.voice)
            {
                Player.SendMessage(p, "Chat moderation is on, you cannot emote.");
                return;
            }
            bool stopIt = false;
            Player.OnPlayerChatEvent(p, ref message, ref stopIt);
            if (!stopIt)
            {
                if (Server.worldChat)
                {
                    Player.GlobalChat(p, p.color + "*" + p.PublicName + " " + message, showname: false);
                }
                else
                {
                    Player.GlobalChatLevel(p, p.color + "*" + p.PublicName + " " + message, showname: false);
                }
                Player.IRCSay("*" + p.PublicName + " " + message);
            }
        }

        void HandleZombieMode(Player p)
        {
            TimeSpan timeToEnd = InfectionUtils.TimeToEnd;
            if (p.isZombie)
            {
                Player.SendMessage(p, "You are a %czombie%s.");
                if (p.ExtraData.ContainsKey("infector"))
                {
                    Player.SendMessage(p, string.Concat("You were infected by %c", p.ExtraData["infector"], "%s."));
                    Player.SendMessage(p, "It happened %c{0}%s ago.", InfectionDurationString(p));
                }
                else if (p.ExtraData.ContainsKey("infection_time"))
                {
                    Player.SendMessage(p, "You were infected %c{0}%s ago.", InfectionDurationString(p));
                }
                Player.SendMessage(p, "Kills: %c{0}%s Time left: %c{1}:{2}", (int)(p.ExtraData["kills"] ?? 0), timeToEnd.Minutes, timeToEnd.Seconds.ToString("00"));
            }
            else
            {
                Player.SendMessage(p, "You are a %ahuman%s.");
                Player.SendMessage(p, "Time left: %a{0}:{1}", timeToEnd.Minutes, timeToEnd.Seconds.ToString("00"));
            }
        }

        string InfectionDurationString(Player p)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract((DateTime)p.ExtraData["infection_time"]);
            if (timeSpan.TotalMinutes >= 1.0)
            {
                return string.Format("{0}min {1}s", timeSpan.Minutes, timeSpan.Seconds);
            }
            return string.Format("{0}s", timeSpan.Seconds);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "What do you need help with, m'boy?! Are you stuck down a well?!");
        }
    }
}