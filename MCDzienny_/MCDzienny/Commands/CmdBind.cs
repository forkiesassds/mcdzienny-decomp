namespace MCDzienny
{
    // Token: 0x02000254 RID: 596
    public class CmdBind : Command
    {
        // Token: 0x1700063F RID: 1599
        // (get) Token: 0x0600114F RID: 4431 RVA: 0x0005E458 File Offset: 0x0005C658
        public override string name
        {
            get { return "bind"; }
        }

        // Token: 0x17000640 RID: 1600
        // (get) Token: 0x06001150 RID: 4432 RVA: 0x0005E460 File Offset: 0x0005C660
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000641 RID: 1601
        // (get) Token: 0x06001151 RID: 4433 RVA: 0x0005E468 File Offset: 0x0005C668
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000642 RID: 1602
        // (get) Token: 0x06001152 RID: 4434 RVA: 0x0005E470 File Offset: 0x0005C670
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000643 RID: 1603
        // (get) Token: 0x06001153 RID: 4435 RVA: 0x0005E474 File Offset: 0x0005C674
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000644 RID: 1604
        // (get) Token: 0x06001154 RID: 4436 RVA: 0x0005E478 File Offset: 0x0005C678
        public override string CustomName
        {
            get { return Lang.Command.BindName; }
        }

        // Token: 0x06001156 RID: 4438 RVA: 0x0005E488 File Offset: 0x0005C688
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }

            message = message.ToLower();
            if (message == Lang.Command.BindParameter)
            {
                for (byte b = 0; b < 128; b += 1) p.bindings[b] = b;
                Player.SendMessage(p, Lang.Command.BindMessage);
                return;
            }

            var num = message.IndexOf(' ');
            if (num != -1)
            {
                var b2 = Block.Byte(message.Substring(0, num));
                var b3 = Block.Byte(message.Substring(num + 1));
                if (b2 == 255)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage1, message.Substring(0, num)));
                    return;
                }

                if (b3 == 255)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage2, message.Substring(num, 1)));
                    return;
                }

                if (!Block.Placable(b2))
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage3, Block.Name(b2)));
                    return;
                }

                if (!Block.canPlace(p, b3))
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage4, Block.Name(b3)));
                    return;
                }

                if (b2 > 64)
                {
                    Player.SendMessage(p, Lang.Command.BindMessage5);
                    return;
                }

                if (p.bindings[b2] == b3)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage6, Block.Name(b2), Block.Name(b3)));
                    return;
                }

                p.bindings[b2] = b3;
                message = string.Format(Lang.Command.BindMessage7, Block.Name(b2), Block.Name(b3));
                Player.SendMessage(p, message);
            }
            else
            {
                var b4 = Block.Byte(message);
                if (b4 > 100)
                {
                    Player.SendMessage(p, Lang.Command.BindMessage8);
                    return;
                }

                if (p.bindings[b4] == b4)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage9, Block.Name(b4)));
                    return;
                }

                p.bindings[b4] = b4;
                Player.SendMessage(p, string.Format(Lang.Command.BindMessage10, Block.Name(b4)));
            }
        }

        // Token: 0x06001157 RID: 4439 RVA: 0x0005E67C File Offset: 0x0005C87C
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BindHelp);
            Player.SendMessage(p, Lang.Command.BindHelp1);
        }
    }
}