using System.Text;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x02000126 RID: 294
    public class CmdImpersonate : Command
    {
        // Token: 0x17000406 RID: 1030
        // (get) Token: 0x060008C8 RID: 2248 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000407 RID: 1031
        // (get) Token: 0x060008C9 RID: 2249 RVA: 0x0002C3E8 File Offset: 0x0002A5E8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000408 RID: 1032
        // (get) Token: 0x060008CA RID: 2250 RVA: 0x0002C3EC File Offset: 0x0002A5EC
        public override string name
        {
            get { return "impersonate"; }
        }

        // Token: 0x17000409 RID: 1033
        // (get) Token: 0x060008CB RID: 2251 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
        public override string shortcut
        {
            get { return "imp"; }
        }

        // Token: 0x1700040A RID: 1034
        // (get) Token: 0x060008CC RID: 2252 RVA: 0x0002C3FC File Offset: 0x0002A5FC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x060008CD RID: 2253 RVA: 0x0002C404 File Offset: 0x0002A604
        public void SendIt(Player p, string message, Player targetPlayer)
        {
            if (targetPlayer != null)
            {
                var stringBuilder = new StringBuilder();
                message = stringBuilder.Append(targetPlayer.IronChallengeTag).Append(targetPlayer.color)
                    .Append(targetPlayer.voicestring).Append(targetPlayer.Tag).Append(targetPlayer.color)
                    .Append(targetPlayer.prefix).Append(targetPlayer.Tier).Append(targetPlayer.LavaPrefix).Append(" ")
                    .Append(targetPlayer.color).Append(targetPlayer.PublicName).Append(": ").Replace("  ", " ")
                    .Append(MCColor.White).Append(message).ToString();
                Player.GlobalMessage(message);
                return;
            }

            Player.SendMessage(p, "The player wasn't found.");
        }

        // Token: 0x060008CE RID: 2254 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.Split(' ').Length < 2)
            {
                Player.SendMessage(p, "No message was given.");
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
            message = message.Substring(message.IndexOf(' ') + 1);
            if (player != null)
            {
                if (p == null)
                {
                    SendIt(p, message, player);
                    return;
                }

                if (player == p)
                {
                    SendIt(p, message, player);
                    return;
                }

                if (p.group.Permission > player.group.Permission)
                {
                    SendIt(p, message, player);
                    return;
                }

                Player.SendMessage(p, "You cannot impersonate a player of equal or greater rank.");
            }
            else
            {
                if (p == null)
                {
                    SendIt(p, message, null);
                    return;
                }

                if (p.group.Permission < LevelPermission.Admin)
                {
                    Player.SendMessage(p, "You are not allowed to impersonate offline players");
                    return;
                }

                if (Group.findPlayerGroup(message.Split(' ')[0]).Permission < p.group.Permission)
                {
                    SendIt(p, message, null);
                    return;
                }

                Player.SendMessage(p, "You cannot impersonate a player of equal or greater rank.");
            }
        }

        // Token: 0x060008CF RID: 2255 RVA: 0x0002C608 File Offset: 0x0002A808
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/impersonate <player> <message> - Sends a message as if it came from <player>");
        }
    }
}