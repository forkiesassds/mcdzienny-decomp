namespace MCDzienny
{
    // Token: 0x02000102 RID: 258
    public class CmdKill : Command
    {
        // Token: 0x17000387 RID: 903
        // (get) Token: 0x060007E3 RID: 2019 RVA: 0x00027E9C File Offset: 0x0002609C
        public override string name
        {
            get { return "kill"; }
        }

        // Token: 0x17000388 RID: 904
        // (get) Token: 0x060007E4 RID: 2020 RVA: 0x00027EA4 File Offset: 0x000260A4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000389 RID: 905
        // (get) Token: 0x060007E5 RID: 2021 RVA: 0x00027EAC File Offset: 0x000260AC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700038A RID: 906
        // (get) Token: 0x060007E6 RID: 2022 RVA: 0x00027EB4 File Offset: 0x000260B4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700038B RID: 907
        // (get) Token: 0x060007E7 RID: 2023 RVA: 0x00027EB8 File Offset: 0x000260B8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060007E9 RID: 2025 RVA: 0x00027EC4 File Offset: 0x000260C4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var num = 0;
            Player player;
            string customMessage;
            if (message.IndexOf(' ') == -1)
            {
                player = Player.Find(message);
                if (p != null)
                    customMessage = string.Format(" was killed by {0}", p.color + p.PublicName);
                else
                    customMessage = " %cwas ambushed and killed.";
            }
            else
            {
                player = Player.Find(message.Split(' ')[0]);
                message = message.Substring(message.IndexOf(' ') + 1);
                if (message.IndexOf(' ') == -1)
                {
                    if (message.ToLower() == "explode")
                    {
                        if (p == null)
                            customMessage = "was exploded.";
                        else
                            customMessage = " was exploded by " + p.color + p.PublicName;
                        num = 1;
                    }
                    else
                    {
                        customMessage = " " + message;
                    }
                }
                else
                {
                    if (message.Split(' ')[0].ToLower() == "explode")
                    {
                        num = 1;
                        message = message.Substring(message.IndexOf(' ') + 1);
                    }

                    customMessage = " " + message;
                }
            }

            if (player == null)
            {
                if (p != null) p.HandleDeath(1, " killed itself in its confusion");
                Player.SendMessage(p, "Could not find player");
                return;
            }

            if (p != null && player.group.Permission > p.group.Permission)
            {
                p.HandleDeath(1, string.Format(" was killed by {0}", player.color + player.PublicName));
                Player.SendMessage(p, "Cannot kill someone of higher rank");
                return;
            }

            player.invincible = false;
            if (num == 1)
                player.HandleDeath(1, customMessage, true);
            else
                player.HandleDeath(1, customMessage);
            player.SubtractLife();
            player.invincible = false;
        }

        // Token: 0x060007EA RID: 2026 RVA: 0x00028088 File Offset: 0x00026288
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kill <name> [explode] <message>");
            Player.SendMessage(p, "Kills <name> with <message>. Causes explosion if [explode] is written");
        }
    }
}