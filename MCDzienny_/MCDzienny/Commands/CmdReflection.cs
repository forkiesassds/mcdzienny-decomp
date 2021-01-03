namespace MCDzienny
{
    // Token: 0x020000D2 RID: 210
    internal class CmdReflection : Command
    {
        // Token: 0x170002FE RID: 766
        // (get) Token: 0x060006D7 RID: 1751 RVA: 0x00023228 File Offset: 0x00021428
        public override string name
        {
            get { return "reflection"; }
        }

        // Token: 0x170002FF RID: 767
        // (get) Token: 0x060006D8 RID: 1752 RVA: 0x00023230 File Offset: 0x00021430
        public override string shortcut
        {
            get { return "rr"; }
        }

        // Token: 0x17000300 RID: 768
        // (get) Token: 0x060006D9 RID: 1753 RVA: 0x00023238 File Offset: 0x00021438
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000301 RID: 769
        // (get) Token: 0x060006DA RID: 1754 RVA: 0x00023240 File Offset: 0x00021440
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000302 RID: 770
        // (get) Token: 0x060006DB RID: 1755 RVA: 0x00023244 File Offset: 0x00021444
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000303 RID: 771
        // (get) Token: 0x060006DC RID: 1756 RVA: 0x00023248 File Offset: 0x00021448
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060006DD RID: 1757 RVA: 0x0002324C File Offset: 0x0002144C
        public override void Use(Player p, string message)
        {
            if (p.core == null) p.core = new Core();
            if (!(message != ""))
            {
                Core.CP cp;
                cp.type = byte.MaxValue;
                cp.x = 0;
                cp.y = 0;
                cp.z = 0;
                p.blockchangeObject = cp;
                Player.SendMessage(p, "Place two blocks to determine the edges.");
                p.ClearBlockchange2();
                p.Blockchange2 += p.core.Blockchange1;
                return;
            }

            if (message.ToLower() == "abort")
            {
                p.core.ĄŻ(p);
                p.ClearBlockchange2();
                return;
            }

            if (message.ToLower() == "show")
            {
                p.core.ŻĄ(p);
                Player.SendMessage(p, "The reflection line is now displayed.");
                return;
            }

            if (message.ToLower() == "hide")
            {
                p.core.ĄŻ(p);
                Player.SendMessage(p, "The reflection line is hidden now.");
                return;
            }

            if (message.ToLower() == "even")
            {
                p.core.mę = Core.ĘĘ.ę;
                return;
            }

            if (message.ToLower() == "odd")
            {
                p.core.mę = Core.ĘĘ.ć;
                return;
            }

            if (message.ToLower() == "restore")
            {
                p.core.ŻĄ(p);
                if (p.core.mć == Core.ĆĆ.ł)
                {
                    p.Blockchange2 += p.core.Blockchange3;
                    return;
                }

                p.Blockchange2 += p.core.Blockchange5;
            }
            else
            {
                if (message.ToLower() == "cross")
                {
                    p.core.ĄŻ(p);
                    p.ClearBlockchange2();
                    p.Blockchange2 += p.core.Blockchange4;
                    return;
                }

                Help(p);
            }
        }

        // Token: 0x060006DE RID: 1758 RVA: 0x0002342C File Offset: 0x0002162C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reflection (/rr) - place two blocks to draw the line of reflection.");
            Player.SendMessage(p,
                "/reflection cross - place three blocks, two to determine the direction of the first line and the third to set the crossing line.");
            Player.SendMessage(p, "/reflection [show/hide] - shows or hides the line.");
            Player.SendMessage(p, "/reflection [even/odd] - switches between odd/even mode.");
            Player.SendMessage(p, "/reflection abort - stops mirroring blocks and hides the line.");
            Player.SendMessage(p, "/reflection restore - restores the most recent line/lines.");
        }
    }
}