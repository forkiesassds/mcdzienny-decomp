namespace MCDzienny
{
    public class CmdFill : Command
    {
        public override string name { get { return "fill"; } }

        public override string shortcut { get { return "f"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (StopConsoleUse(p))
            {
                return;
            }
            if (p.core == null)
            {
                p.core = new Core();
            }
            int num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }
            Core.IT iT = default(Core.IT);
            if (num == 2)
            {
                int num2 = message.IndexOf(' ');
                string arg = message.Substring(0, num2).ToLower();
                string text = message.Substring(num2 + 1).ToLower();
                iT.o = Block.Byte(arg);
                if (iT.o == byte.MaxValue)
                {
                    Player.SendMessage(p, string.Format("There is no block \"{0}\".", arg));
                    return;
                }
                if (!Block.canPlace(p, iT.o))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
                switch (text)
                {
                    case "up":
                        iT.ą = Core.Ę.ź;
                        break;
                    case "down":
                        iT.ą = Core.Ę.ń;
                        break;
                    case "layer":
                        iT.ą = Core.Ę.l;
                        break;
                    case "vertical_x":
                    case "layer_x":
                    case "layerx":
                        iT.ą = Core.Ę.u;
                        break;
                    case "vertical_z":
                    case "layer_z":
                    case "layerz":
                        iT.ą = Core.Ę.q;
                        break;
                    default:
                        Player.SendMessage(p, "Invalid fill type.");
                        return;
                }
            }
            else if (message != "")
            {
                message = message.ToLower();
                switch (message)
                {
                    case "up":
                        iT.ą = Core.Ę.ź;
                        iT.o = byte.MaxValue;
                        break;
                    case "down":
                        iT.ą = Core.Ę.ń;
                        iT.o = byte.MaxValue;
                        break;
                    case "layer":
                        iT.ą = Core.Ę.l;
                        iT.o = byte.MaxValue;
                        break;
                    case "vertical_x":
                    case "layer_x":
                    case "layerx":
                        iT.ą = Core.Ę.u;
                        iT.o = byte.MaxValue;
                        break;
                    case "vertical_z":
                    case "layer_z":
                    case "layerz":
                        iT.ą = Core.Ę.q;
                        iT.o = byte.MaxValue;
                        break;
                    default:
                        iT.o = Block.Byte(message);
                        if (iT.o == byte.MaxValue)
                        {
                            Player.SendMessage(p, "Invalid block or fill type");
                            return;
                        }
                        if (!Block.canPlace(p, iT.o))
                        {
                            Player.SendMessage(p, "Cannot place that.");
                            return;
                        }
                        iT.ą = Core.Ę.ć;
                        break;
                }
            }
            else
            {
                iT.o = byte.MaxValue;
                iT.ą = Core.Ę.ć;
            }
            iT.x = 0;
            iT.y = 0;
            iT.z = 0;
            p.blockchangeObject = iT;
            Player.SendMessage(p, "Destroy the block you wish to fill.");
            p.ClearBlockchange();
            p.Blockchange += p.core.BlockchangeF1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fill [block] [type] - Fills the area specified with [block].");
            Player.SendMessage(p, "[type] - up, down, layer, layer_x, layer_z");
        }
    }
}