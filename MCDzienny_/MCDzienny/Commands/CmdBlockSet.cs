namespace MCDzienny
{
    // Token: 0x020002EF RID: 751
    public class CmdBlockSet : Command
    {
        // Token: 0x17000862 RID: 2146
        // (get) Token: 0x06001549 RID: 5449 RVA: 0x000757F8 File Offset: 0x000739F8
        public override string name
        {
            get { return "blockset"; }
        }

        // Token: 0x17000863 RID: 2147
        // (get) Token: 0x0600154A RID: 5450 RVA: 0x00075800 File Offset: 0x00073A00
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000864 RID: 2148
        // (get) Token: 0x0600154B RID: 5451 RVA: 0x00075808 File Offset: 0x00073A08
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000865 RID: 2149
        // (get) Token: 0x0600154C RID: 5452 RVA: 0x00075810 File Offset: 0x00073A10
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000866 RID: 2150
        // (get) Token: 0x0600154D RID: 5453 RVA: 0x00075814 File Offset: 0x00073A14
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000867 RID: 2151
        // (get) Token: 0x0600154E RID: 5454 RVA: 0x00075818 File Offset: 0x00073A18
        public override string CustomName
        {
            get { return Lang.Command.BlockSetName; }
        }

        // Token: 0x06001550 RID: 5456 RVA: 0x00075828 File Offset: 0x00073A28
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            var foundBlock = Block.Byte(message.Split(' ')[0]);
            if (foundBlock == 255)
            {
                Player.SendMessage(p, Lang.Command.BlockSetMessage);
                return;
            }

            var levelPermission = Level.PermissionFromName(message.Split(' ')[1]);
            if (levelPermission == LevelPermission.Null)
            {
                Player.SendMessage(p, Lang.Command.BlockSetMessage1);
                return;
            }

            if (p != null && levelPermission > p.group.Permission)
            {
                Player.SendMessage(p, Lang.Command.BlockSetMessage2);
                return;
            }

            if (p != null && !Block.canPlace(p, foundBlock))
            {
                Player.SendMessage(p, Lang.Command.BlockSetMessage3);
                return;
            }

            var blocks = Block.BlockList.Find(bs => bs.type == foundBlock);
            blocks.lowestRank = levelPermission;
            Block.BlockList[Block.BlockList.FindIndex(bL => bL.type == foundBlock)] = blocks;
            Block.SaveBlocks(Block.BlockList);
            Player.GlobalMessage(string.Format(Lang.Command.BlockSetMessage4,
                Block.Name(foundBlock) + Server.DefaultColor, Level.PermissionToName(levelPermission)));
        }

        // Token: 0x06001551 RID: 5457 RVA: 0x00075974 File Offset: 0x00073B74
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BlockSetHelp);
            Player.SendMessage(p, Lang.Command.BlockSetHelp1);
            Player.SendMessage(p, string.Format(Lang.Command.BlockSetHelp2, Group.concatList()));
        }
    }
}