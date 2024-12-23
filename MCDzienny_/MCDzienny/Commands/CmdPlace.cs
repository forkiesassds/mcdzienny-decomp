using System;

namespace MCDzienny.Commands
{
    class CmdPlace : Command
    {
        public override string name { get { return "place"; } }

        public override string shortcut { get { return "pl"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            byte b = byte.MaxValue;
            ushort num = 0;
            ushort num2 = 0;
            ushort num3 = 0;
            Level level = null;
            if (p != null)
            {
                num = (ushort)(p.pos[0] / 32);
                num2 = (ushort)(p.pos[1] / 32 - 1);
                num3 = (ushort)(p.pos[2] / 32);
            }
            message.Trim();
            try
            {
                switch (message.Split(' ').Length)
                {
                    case 0:
                        b = 1;
                        break;
                    case 1:
                        b = Block.Byte(message);
                        break;
                    case 3:
                        num = Convert.ToUInt16(message.Split(' ')[0]);
                        num2 = Convert.ToUInt16(message.Split(' ')[1]);
                        num3 = Convert.ToUInt16(message.Split(' ')[2]);
                        break;
                    case 4:
                        b = Block.Byte(message.Split(' ')[0]);
                        num = Convert.ToUInt16(message.Split(' ')[1]);
                        num2 = Convert.ToUInt16(message.Split(' ')[2]);
                        num3 = Convert.ToUInt16(message.Split(' ')[3]);
                        break;
                    case 5:
                        b = Block.Byte(message.Split(' ')[0]);
                        num = Convert.ToUInt16(message.Split(' ')[1]);
                        num2 = Convert.ToUInt16(message.Split(' ')[2]);
                        num3 = Convert.ToUInt16(message.Split(' ')[3]);
                        level = message.Split(' ')[4] == "lava" ? LavaSystem.currentlvl : Level.Find(message.Split(' ')[4]);
                        if (level == null)
                        {
                            Player.SendMessage(p, "Level not found");
                            return;
                        }
                        break;
                    default:
                        Player.SendMessage(p, "Invalid parameters");
                        return;
                }
            }
            catch
            {
                Player.SendMessage(p, "Invalid parameters");
                return;
            }
            if (p != null)
            {
                if (b == byte.MaxValue)
                {
                    b = 1;
                }
                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that block type.");
                    return;
                }
                if (num2 >= p.level.height)
                {
                    num2 = (ushort)(p.level.height - 1);
                }
                p.level.Blockchange(p, num, num2, num3, b);
            }
            else
            {
                if (level == null)
                {
                    Player.SendMessage(p, "You didn't select a level.");
                    Help(p);
                    return;
                }
                level.Blockchange(num, num2, num3, b);
            }
            Player.SendMessage(p, string.Format("A block was placed at ({0}, {1}, {2}).", num, num2, num3));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/place [block] <x y z> - Places block at your feet or <x y z>");
            Player.SendMessage(p, "/place [block] <x y z> [level] - Places block in [level] at <x y z>");
        }
    }
}