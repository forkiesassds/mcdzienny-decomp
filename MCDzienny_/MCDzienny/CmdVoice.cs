namespace MCDzienny
{
    public class CmdVoice : Command
    {
        public override string name { get { return "voice"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player != null)
            {
                if (player.voice)
                {
                    player.voice = false;
                    Player.SendMessage(p, string.Format("Removing voice status from {0}", player.PublicName));
                    player.SendMessage("Your voice status has been revoked.");
                    player.voicestring = "";
                }
                else
                {
                    player.voice = true;
                    Player.SendMessage(p, string.Format("Giving voice status to {0}", player.PublicName));
                    player.SendMessage("You have received voice status.");
                    player.voicestring = "&f+";
                }
            }
            else
            {
                Player.SendMessage(p, string.Format("There is no player online named \"{0}\"", message));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/voice <name> - Toggles voice status on or off for specified player.");
        }
    }
}