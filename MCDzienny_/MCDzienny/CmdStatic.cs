namespace MCDzienny
{
    public class CmdStatic : Command
    {
        public override string name { get { return "static"; } }

        public override string shortcut { get { return "t"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Freebuild; } }

        public override void Use(Player p, string message)
        {
            p.staticCommands = !p.staticCommands;
            p.ClearBlockchange();
            p.BlockAction = 0;
            Player.SendMessage(p, string.Format("Static mode: &a{0}", p.staticCommands.ToString()));
            try
            {
                if (!(message != ""))
                {
                    return;
                }
                if (message.IndexOf(' ') == -1)
                {
                    if (p.group.CanExecute(all.Find(message)))
                    {
                        all.Find(message).Use(p, "");
                    }
                    else
                    {
                        Player.SendMessage(p, "Cannot use that command.");
                    }
                }
                else if (p.group.CanExecute(all.Find(message.Split(' ')[0])))
                {
                    all.Find(message.Split(' ')[0]).Use(p, message.Substring(message.IndexOf(' ') + 1));
                }
                else
                {
                    Player.SendMessage(p, "Cannot use that command.");
                }
            }
            catch
            {
                Player.SendMessage(p, "Could not find specified command");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/static [command] - Makes every command a toggle.");
            Player.SendMessage(p, "If [command] is given, then that command is used");
        }
    }
}