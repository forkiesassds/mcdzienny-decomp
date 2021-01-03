using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x0200005B RID: 91
    public class CmdSetModel : Command
    {
        // Token: 0x1700007E RID: 126
        // (get) Token: 0x06000221 RID: 545 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
        public override string name
        {
            get { return "setmodel"; }
        }

        // Token: 0x1700007F RID: 127
        // (get) Token: 0x06000222 RID: 546 RVA: 0x0000C6CC File Offset: 0x0000A8CC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000080 RID: 128
        // (get) Token: 0x06000223 RID: 547 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000081 RID: 129
        // (get) Token: 0x06000224 RID: 548 RVA: 0x0000C6DC File Offset: 0x0000A8DC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000082 RID: 130
        // (get) Token: 0x06000225 RID: 549 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000226 RID: 550 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
        public override void Use(Player p, string message)
        {
            var message2 = new Message(message);
            if (message2.Count != 2)
            {
                Help(p);
                return;
            }

            var name = message2.ReadStringLower();
            var player = Player.Find(name);
            if (player == null)
            {
                Player.SendMessage(p, "Couldn't find the given player named " + player);
                return;
            }

            var text = message2.ReadString();
            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "Model name contains an invalid character.");
                return;
            }

            if (text.Length > 64)
            {
                Player.SendMessage(p, "Model name can't be longer than 64 characters.");
                return;
            }

            player.ModelName = text;
            player.SavePlayerAppearance();
            Player.SendMessage(p, "Model for " + player.PublicName + " was set to " + text);
            if (player != p) Player.SendMessage(player, "Your character model was set to " + text);
            Player.GlobalDie(player, false);
            Player.GlobalSpawn(player);
        }

        // Token: 0x06000227 RID: 551 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setmodel [player] [name] - sets a model for a given player.");
        }
    }
}