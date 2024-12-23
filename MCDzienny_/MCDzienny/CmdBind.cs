namespace MCDzienny
{
    public class CmdBind : Command
    {
        public override string name { get { return "bind"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override string CustomName { get { return Lang.Command.BindName; } }

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
                for (byte b = 0; b < 128; b++)
                {
                    p.bindings[b] = b;
                }
                Player.SendMessage(p, Lang.Command.BindMessage);
                return;
            }
            int num = message.IndexOf(' ');
            if (num != -1)
            {
                byte b2 = Block.Byte(message.Substring(0, num));
                byte b3 = Block.Byte(message.Substring(num + 1));
                if (b2 == byte.MaxValue)
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BindMessage1, message.Substring(0, num)));
                    return;
                }
                if (b3 == byte.MaxValue)
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
                byte b4 = Block.Byte(message);
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BindHelp);
            Player.SendMessage(p, Lang.Command.BindHelp1);
        }
    }
}