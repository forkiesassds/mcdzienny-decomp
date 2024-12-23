namespace MCDzienny
{
    public class CmdBlockSet : Command
    {
        public override string name { get { return "blockset"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override string CustomName { get { return Lang.Command.BlockSetName; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            byte foundBlock = Block.Byte(message.Split(' ')[0]);
            if (foundBlock == byte.MaxValue)
            {
                Player.SendMessage(p, Lang.Command.BlockSetMessage);
                return;
            }
            LevelPermission levelPermission = Level.PermissionFromName(message.Split(' ')[1]);
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
            Block.Blocks blocks = Block.BlockList.Find(bs => bs.type == foundBlock);
            blocks.lowestRank = levelPermission;
            Block.BlockList[Block.BlockList.FindIndex(bL => bL.type == foundBlock)] = blocks;
            Block.SaveBlocks(Block.BlockList);
            Player.GlobalMessage(string.Format(Lang.Command.BlockSetMessage4, Block.Name(foundBlock) + Server.DefaultColor, Level.PermissionToName(levelPermission)));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BlockSetHelp);
            Player.SendMessage(p, Lang.Command.BlockSetHelp1);
            Player.SendMessage(p, string.Format(Lang.Command.BlockSetHelp2, Group.concatList()));
        }
    }
}