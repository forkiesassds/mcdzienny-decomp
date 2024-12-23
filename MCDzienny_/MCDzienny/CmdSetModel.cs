using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdSetModel : Command
    {
        public override string name { get { return "setmodel"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            Message message2 = new Message(message);
            if (message2.Count != 2)
            {
                Help(p);
                return;
            }
            string text = message2.ReadStringLower();
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, "Couldn't find the given player named " + player);
                return;
            }
            string text2 = message2.ReadString();
            if (!Player.ValidName(text2))
            {
                Player.SendMessage(p, "Model name contains an invalid character.");
                return;
            }
            if (text2.Length > 64)
            {
                Player.SendMessage(p, "Model name can't be longer than 64 characters.");
                return;
            }
            player.ModelName = text2;
            player.SavePlayerAppearance();
            Player.SendMessage(p, "Model for " + player.PublicName + " was set to " + text2);
            if (player != p)
            {
                Player.SendMessage(player, "Your character model was set to " + text2);
            }
            Player.GlobalDie(player, self: false);
            Player.GlobalSpawn(player);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setmodel [player] [name] - sets a model for a given player.");
        }
    }
}