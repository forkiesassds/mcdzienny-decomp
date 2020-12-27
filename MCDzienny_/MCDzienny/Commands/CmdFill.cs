namespace MCDzienny
{
    // Token: 0x02000143 RID: 323
    public class CmdFill : Command
    {
        // Token: 0x1700046E RID: 1134
        // (get) Token: 0x0600098E RID: 2446 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
        public override string name
        {
            get { return "fill"; }
        }

        // Token: 0x1700046F RID: 1135
        // (get) Token: 0x0600098F RID: 2447 RVA: 0x0002ECBC File Offset: 0x0002CEBC
        public override string shortcut
        {
            get { return "f"; }
        }

        // Token: 0x17000470 RID: 1136
        // (get) Token: 0x06000990 RID: 2448 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000471 RID: 1137
        // (get) Token: 0x06000991 RID: 2449 RVA: 0x0002ECCC File Offset: 0x0002CECC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000472 RID: 1138
        // (get) Token: 0x06000992 RID: 2450 RVA: 0x0002ECD0 File Offset: 0x0002CED0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000473 RID: 1139
        // (get) Token: 0x06000993 RID: 2451 RVA: 0x0002ECD4 File Offset: 0x0002CED4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000995 RID: 2453 RVA: 0x0002ECE0 File Offset: 0x0002CEE0
        public override void Use(Player p, string message)
        {
            if (StopConsoleUse(p)) return;
            if (p.core == null) p.core = new Core();
            var num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }

            Core.IT it;
            if (num == 2)
            {
                var num2 = message.IndexOf(' ');
                var text = message.Substring(0, num2).ToLower();
                var a = message.Substring(num2 + 1).ToLower();
                it.o = Block.Byte(text);
                if (it.o == 255)
                {
                    Player.SendMessage(p, string.Format("There is no block \"{0}\".", text));
                    return;
                }

                if (!Block.canPlace(p, it.o))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }

                if (a == "up")
                {
                    it.ą = Core.Ę.ź;
                }
                else if (a == "down")
                {
                    it.ą = Core.Ę.ń;
                }
                else if (a == "layer")
                {
                    it.ą = Core.Ę.l;
                }
                else if (a == "vertical_x" || a == "layer_x" || a == "layerx")
                {
                    it.ą = Core.Ę.u;
                }
                else
                {
                    if (!(a == "vertical_z") && !(a == "layer_z") && !(a == "layerz"))
                    {
                        Player.SendMessage(p, "Invalid fill type.");
                        return;
                    }

                    it.ą = Core.Ę.q;
                }
            }
            else if (message != "")
            {
                message = message.ToLower();
                if (message == "up")
                {
                    it.ą = Core.Ę.ź;
                    it.o = byte.MaxValue;
                }
                else if (message == "down")
                {
                    it.ą = Core.Ę.ń;
                    it.o = byte.MaxValue;
                }
                else if (message == "layer")
                {
                    it.ą = Core.Ę.l;
                    it.o = byte.MaxValue;
                }
                else if (message == "vertical_x" || message == "layer_x" || message == "layerx")
                {
                    it.ą = Core.Ę.u;
                    it.o = byte.MaxValue;
                }
                else if (message == "vertical_z" || message == "layer_z" || message == "layerz")
                {
                    it.ą = Core.Ę.q;
                    it.o = byte.MaxValue;
                }
                else
                {
                    it.o = Block.Byte(message);
                    if (it.o == 255)
                    {
                        Player.SendMessage(p, "Invalid block or fill type");
                        return;
                    }

                    if (!Block.canPlace(p, it.o))
                    {
                        Player.SendMessage(p, "Cannot place that.");
                        return;
                    }

                    it.ą = Core.Ę.ć;
                }
            }
            else
            {
                it.o = byte.MaxValue;
                it.ą = Core.Ę.ć;
            }

            it.x = 0;
            it.y = 0;
            it.z = 0;
            p.blockchangeObject = it;
            Player.SendMessage(p, "Destroy the block you wish to fill.");
            p.ClearBlockchange();
            p.Blockchange += p.core.BlockchangeF1;
        }

        // Token: 0x06000996 RID: 2454 RVA: 0x0002F024 File Offset: 0x0002D224
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fill [block] [type] - Fills the area specified with [block].");
            Player.SendMessage(p, "[type] - up, down, layer, layer_x, layer_z");
        }
    }
}