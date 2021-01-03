using System;

namespace MCDzienny.Commands
{
    // Token: 0x02000283 RID: 643
    internal class CmdPlace : Command
    {
        // Token: 0x170006DB RID: 1755
        // (get) Token: 0x06001272 RID: 4722 RVA: 0x00065B18 File Offset: 0x00063D18
        public override string name
        {
            get { return "place"; }
        }

        // Token: 0x170006DC RID: 1756
        // (get) Token: 0x06001273 RID: 4723 RVA: 0x00065B20 File Offset: 0x00063D20
        public override string shortcut
        {
            get { return "pl"; }
        }

        // Token: 0x170006DD RID: 1757
        // (get) Token: 0x06001274 RID: 4724 RVA: 0x00065B28 File Offset: 0x00063D28
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006DE RID: 1758
        // (get) Token: 0x06001275 RID: 4725 RVA: 0x00065B30 File Offset: 0x00063D30
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006DF RID: 1759
        // (get) Token: 0x06001276 RID: 4726 RVA: 0x00065B34 File Offset: 0x00063D34
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x06001277 RID: 4727 RVA: 0x00065B38 File Offset: 0x00063D38
        public override void Use(Player p, string message)
        {
            var b = byte.MaxValue;
            ushort num = 0;
            ushort num2 = 0;
            ushort num3 = 0;
            Level level = null;
            if (p != null)
            {
                num = (ushort) (p.pos[0] / 32);
                num2 = (ushort) (p.pos[1] / 32 - 1);
                num3 = (ushort) (p.pos[2] / 32);
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
                        level = message.Split(' ')[4] == "lava"
                            ? LavaSystem.currentlvl
                            : Level.Find(message.Split(' ')[4]);
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
                if (b == byte.MaxValue) b = 1;
                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that block type.");
                    return;
                }

                if (num2 >= p.level.height) num2 = (ushort) (p.level.height - 1);
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

        // Token: 0x06001278 RID: 4728 RVA: 0x00065E64 File Offset: 0x00064064
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/place [block] <x y z> - Places block at your feet or <x y z>");
            Player.SendMessage(p, "/place [block] <x y z> [level] - Places block in [level] at <x y z>");
        }
    }
}