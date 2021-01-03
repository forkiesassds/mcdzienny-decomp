using System;
using MCDzienny.InfectionSystem;

namespace MCDzienny
{
    // Token: 0x02000275 RID: 629
    public class CmdMe : Command
    {
        // Token: 0x170006A0 RID: 1696
        // (get) Token: 0x0600120D RID: 4621 RVA: 0x00063D48 File Offset: 0x00061F48
        public override string name
        {
            get { return "me"; }
        }

        // Token: 0x170006A1 RID: 1697
        // (get) Token: 0x0600120E RID: 4622 RVA: 0x00063D50 File Offset: 0x00061F50
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006A2 RID: 1698
        // (get) Token: 0x0600120F RID: 4623 RVA: 0x00063D58 File Offset: 0x00061F58
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170006A3 RID: 1699
        // (get) Token: 0x06001210 RID: 4624 RVA: 0x00063D60 File Offset: 0x00061F60
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006A4 RID: 1700
        // (get) Token: 0x06001211 RID: 4625 RVA: 0x00063D64 File Offset: 0x00061F64
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170006A5 RID: 1701
        // (get) Token: 0x06001212 RID: 4626 RVA: 0x00063D68 File Offset: 0x00061F68
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001214 RID: 4628 RVA: 0x00063D74 File Offset: 0x00061F74
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.level.mapType == MapType.Zombie)
                {
                    HandleZombieMode(p);
                    return;
                }

                Player.SendMessage(p, "You");
            }
            else
            {
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

                var flag = false;
                Player.OnPlayerChatEvent(p, ref message, ref flag);
                if (flag) return;
                if (Server.worldChat)
                    Player.GlobalChat(p, string.Concat(p.color, "*", p.PublicName, " ", message), false);
                else
                    Player.GlobalChatLevel(p, string.Concat(p.color, "*", p.PublicName, " ", message), false);
                Player.IRCSay("*" + p.PublicName + " " + message);
            }
        }

        // Token: 0x06001215 RID: 4629 RVA: 0x00063EA4 File Offset: 0x000620A4
        private void HandleZombieMode(Player p)
        {
            var timeToEnd = InfectionUtils.TimeToEnd;
            if (p.isZombie)
            {
                Player.SendMessage(p, "You are a %czombie%s.");
                if (p.ExtraData.ContainsKey("infector"))
                {
                    Player.SendMessage(p, "You were infected by %c" + p.ExtraData["infector"] + "%s.");
                    Player.SendMessage(p, "It happened %c{0}%s ago.", InfectionDurationString(p));
                }
                else if (p.ExtraData.ContainsKey("infection_time"))
                {
                    Player.SendMessage(p, "You were infected %c{0}%s ago.", InfectionDurationString(p));
                }

                Player.SendMessage(p, "Kills: %c{0}%s Time left: %c{1}:{2}", (int) (p.ExtraData["kills"] ?? 0),
                    timeToEnd.Minutes, timeToEnd.Seconds.ToString("00"));
                return;
            }

            Player.SendMessage(p, "You are a %ahuman%s.");
            Player.SendMessage(p, "Time left: %a{0}:{1}", timeToEnd.Minutes, timeToEnd.Seconds.ToString("00"));
        }

        // Token: 0x06001216 RID: 4630 RVA: 0x00064000 File Offset: 0x00062200
        private string InfectionDurationString(Player p)
        {
            var timeSpan = DateTime.Now.Subtract((DateTime) p.ExtraData["infection_time"]);
            if (timeSpan.TotalMinutes >= 1.0) return string.Format("{0}min {1}s", timeSpan.Minutes, timeSpan.Seconds);
            return string.Format("{0}s", timeSpan.Seconds);
        }

        // Token: 0x06001217 RID: 4631 RVA: 0x0006407C File Offset: 0x0006227C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "What do you need help with, m'boy?! Are you stuck down a well?!");
        }
    }
}