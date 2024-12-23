using System;

namespace MCDzienny
{
    public class CmdReveal : Command
    {
        public override string name { get { return "reveal"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Freebuild | CommandScope.Home | CommandScope.MyMap; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                message = p.name;
            }
            if (message.ToLower() == "all")
            {
                if (p.group.Permission < LevelPermission.Operator)
                {
                    Player.SendMessage(p, "Reserved for OP+");
                    return;
                }
                Player.players.ForEach(delegate(Player who)
                {
                    if (who.level == p.level)
                    {
                        who.Loading = true;
                        Player.players.ForEach(delegate(Player pl)
                        {
                            if (who.level == pl.level && who != pl)
                            {
                                who.SendDie(pl.id);
                            }
                        });
                        PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
                        {
                            if (who.level == b.level)
                            {
                                who.SendDie(b.id);
                            }
                        });
                        Player.GlobalDie(who, self: true);
                        who.SendUserMOTD();
                        who.SendMap();
                        ushort x = (ushort)((0.5 + who.level.spawnx) * 32.0);
                        ushort y = (ushort)((1 + who.level.spawny) * 32);
                        ushort z = (ushort)((0.5 + who.level.spawnz) * 32.0);
                        if (!who.hidden)
                        {
                            Player.GlobalSpawn(who, x, y, z, who.level.rotx, who.level.roty, self: true);
                        }
                        else
                        {
                            who.SendPos(byte.MaxValue, x, y, z, who.level.rotx, who.level.roty);
                        }
                        Player.players.ForEach(delegate(Player pl)
                        {
                            if (pl.level == who.level && who != pl && !pl.hidden)
                            {
                                who.SendSpawn(pl.id, pl.color + pl.name, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2], pl.rot[0], pl.rot[1]);
                            }
                        });
                        PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
                        {
                            if (b.level == who.level)
                            {
                                who.SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
                            }
                        });
                        who.Loading = false;
                        who.SendMessage(string.Format("&bMap reloaded by {0}", p.name));
                        Player.SendMessage(p, string.Format("&4Finished reloading for {0}", who.name));
                    }
                });
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return;
            }
            Player who2 = Player.Find(message);
            if (who2 == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }
            if (who2.group.Permission > p.group.Permission && p != who2)
            {
                Player.SendMessage(p, "Cannot reload the map of someone higher than you.");
                return;
            }
            who2.Loading = true;
            Player.players.ForEach(delegate(Player pl)
            {
                if (who2.level == pl.level && who2 != pl)
                {
                    who2.SendDie(pl.id);
                }
            });
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (who2.level == b.level)
                {
                    who2.SendDie(b.id);
                }
            });
            Player.GlobalDie(who2, self: true);
            who2.SendUserMOTD();
            who2.SendMap();
            ushort x2 = (ushort)((0.5 + who2.level.spawnx) * 32.0);
            ushort y2 = (ushort)((1 + who2.level.spawny) * 32);
            ushort z2 = (ushort)((0.5 + who2.level.spawnz) * 32.0);
            if (!who2.hidden)
            {
                Player.GlobalSpawn(who2, x2, y2, z2, who2.level.rotx, who2.level.roty, self: true);
            }
            else
            {
                who2.SendPos(byte.MaxValue, x2, y2, z2, who2.level.rotx, who2.level.roty);
            }
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.level == who2.level && who2 != pl && !pl.hidden)
                {
                    who2.SendSpawn(pl.id, pl.color + pl.name, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2], pl.rot[0], pl.rot[1]);
                }
            });
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (b.level == who2.level)
                {
                    who2.SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
                }
            });
            who2.Loading = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            who2.SendMessage(string.Format("&bMap reloaded by {0}", p.name));
            Player.SendMessage(p, string.Format("&4Finished reloading for {0}", who2.name));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reveal <name> - Reveals the map for <name>.");
            Player.SendMessage(p, "/reveal all - Reveals for all in the map");
            Player.SendMessage(p, "Will reload the map for anyone. (incl. banned)");
        }
    }
}