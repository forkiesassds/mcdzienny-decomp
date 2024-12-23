using System;

namespace MCDzienny
{
    public class CmdBotRemove : Command
    {
        public string[,] botlist;

        public override string name { get { return "botremove"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override string CustomName { get { return Lang.Command.BotRemoveName; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            try
            {
                if (message.ToLower() == Lang.Command.BotRemoveParameter)
                {
                    for (int i = 0; i < PlayerBot.playerbots.Count; i++)
                    {
                        if (PlayerBot.playerbots[i].level == p.level)
                        {
                            PlayerBot playerBot = PlayerBot.playerbots[i];
                            playerBot.removeBot();
                            i--;
                        }
                    }
                }
                else
                {
                    PlayerBot playerBot2 = PlayerBot.Find(message);
                    if (playerBot2 == null)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BotRemoveMessage, playerBot2));
                        return;
                    }
                    if (p.level != playerBot2.level)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BotRemoveMessage1, playerBot2.name));
                        return;
                    }
                    playerBot2.removeBot();
                    Player.SendMessage(p, Lang.Command.BotRemoveMessage2);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotRemoveHelp);
        }
    }
}