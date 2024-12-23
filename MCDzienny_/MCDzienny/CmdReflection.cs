namespace MCDzienny
{
    class CmdReflection : Command
    {
        public override string name { get { return "reflection"; } }

        public override string shortcut { get { return "rr"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p.core == null)
            {
                p.core = new Core();
            }
            if (message != "")
            {
                if (message.ToLower() == "abort")
                {
                    p.core.ĄŻ(p);
                    p.ClearBlockchange2();
                }
                else if (message.ToLower() == "show")
                {
                    p.core.ŻĄ(p);
                    Player.SendMessage(p, "The reflection line is now displayed.");
                }
                else if (message.ToLower() == "hide")
                {
                    p.core.ĄŻ(p);
                    Player.SendMessage(p, "The reflection line is hidden now.");
                }
                else if (message.ToLower() == "even")
                {
                    p.core.mę = Core.ĘĘ.ę;
                }
                else if (message.ToLower() == "odd")
                {
                    p.core.mę = Core.ĘĘ.ć;
                }
                else if (message.ToLower() == "restore")
                {
                    p.core.ŻĄ(p);
                    if (p.core.mć == Core.ĆĆ.ł)
                    {
                        p.Blockchange2 += p.core.Blockchange3;
                    }
                    else
                    {
                        p.Blockchange2 += p.core.Blockchange5;
                    }
                }
                else if (message.ToLower() == "cross")
                {
                    p.core.ĄŻ(p);
                    p.ClearBlockchange2();
                    p.Blockchange2 += p.core.Blockchange4;
                }
                else
                {
                    Help(p);
                }
            }
            else
            {
                Core.CP cP = default(Core.CP);
                cP.type = byte.MaxValue;
                cP.x = 0;
                cP.y = 0;
                cP.z = 0;
                p.blockchangeObject = cP;
                Player.SendMessage(p, "Place two blocks to determine the edges.");
                p.ClearBlockchange2();
                p.Blockchange2 += p.core.Blockchange1;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reflection (/rr) - place two blocks to draw the line of reflection.");
            Player.SendMessage(p, "/reflection cross - place three blocks, two to determine the direction of the first line and the third to set the crossing line.");
            Player.SendMessage(p, "/reflection [show/hide] - shows or hides the line.");
            Player.SendMessage(p, "/reflection [even/odd] - switches between odd/even mode.");
            Player.SendMessage(p, "/reflection abort - stops mirroring blocks and hides the line.");
            Player.SendMessage(p, "/reflection restore - restores the most recent line/lines.");
        }
    }
}