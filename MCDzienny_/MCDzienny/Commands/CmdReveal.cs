using System;

namespace MCDzienny
{
    // Token: 0x020002D1 RID: 721
    public class CmdReveal : Command
    {
        // Token: 0x170007E3 RID: 2019
        // (get) Token: 0x0600146E RID: 5230 RVA: 0x00070F5C File Offset: 0x0006F15C
        public override string name
        {
            get { return "reveal"; }
        }

        // Token: 0x170007E4 RID: 2020
        // (get) Token: 0x0600146F RID: 5231 RVA: 0x00070F64 File Offset: 0x0006F164
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007E5 RID: 2021
        // (get) Token: 0x06001470 RID: 5232 RVA: 0x00070F6C File Offset: 0x0006F16C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007E6 RID: 2022
        // (get) Token: 0x06001471 RID: 5233 RVA: 0x00070F74 File Offset: 0x0006F174
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007E7 RID: 2023
        // (get) Token: 0x06001472 RID: 5234 RVA: 0x00070F78 File Offset: 0x0006F178
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170007E8 RID: 2024
        // (get) Token: 0x06001473 RID: 5235 RVA: 0x00070F7C File Offset: 0x0006F17C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170007E9 RID: 2025
        // (get) Token: 0x06001474 RID: 5236 RVA: 0x00070F80 File Offset: 0x0006F180
        public override CommandScope Scope
        {
            get { return CommandScope.Freebuild | CommandScope.Home | CommandScope.MyMap; }
        }

        // Token: 0x06001476 RID: 5238 RVA: 0x00070F8C File Offset: 0x0006F18C
        public override void Use(Player p, string message)
        {
            if (message == "") message = p.name;
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
                            if (who.level == pl.level && who != pl) who.SendDie(pl.id);
                        });
                        PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
                        {
                            if (who.level == b.level) who.SendDie(b.id);
                        });
                        Player.GlobalDie(who, true);
                        who.SendUserMOTD();
                        who.SendMap();
                        var x2 = (ushort) ((0.5 + who.level.spawnx) * 32.0);
                        var y2 = (ushort) ((1 + who.level.spawny) * 32);
                        var z2 = (ushort) ((0.5 + who.level.spawnz) * 32.0);
                        if (!who.hidden)
                            Player.GlobalSpawn(who, x2, y2, z2, who.level.rotx, who.level.roty, true);
                        else
                            who.SendPos(byte.MaxValue, x2, y2, z2, who.level.rotx, who.level.roty);
                        Player.players.ForEach(delegate(Player pl)
                        {
                            if (pl.level == who.level && who != pl && !pl.hidden)
                                who.SendSpawn(pl.id, pl.color + pl.name, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2],
                                    pl.rot[0], pl.rot[1]);
                        });
                        PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
                        {
                            if (b.level == who.level)
                                who.SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
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

            var who2 = Player.Find(message);
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
                if (who2.level == pl.level && who2 != pl) who2.SendDie(pl.id);
            });
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (who2.level == b.level) who2.SendDie(b.id);
            });
            Player.GlobalDie(who2, true);
            who2.SendUserMOTD();
            who2.SendMap();
            var x = (ushort) ((0.5 + who2.level.spawnx) * 32.0);
            var y = (ushort) ((1 + who2.level.spawny) * 32);
            var z = (ushort) ((0.5 + who2.level.spawnz) * 32.0);
            if (!who2.hidden)
                Player.GlobalSpawn(who2, x, y, z, who2.level.rotx, who2.level.roty, true);
            else
                who2.SendPos(byte.MaxValue, x, y, z, who2.level.rotx, who2.level.roty);
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.level == who2.level && who2 != pl && !pl.hidden)
                    who2.SendSpawn(pl.id, pl.color + pl.name, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2], pl.rot[0],
                        pl.rot[1]);
            });
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (b.level == who2.level)
                    who2.SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
            });
            who2.Loading = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            who2.SendMessage(string.Format("&bMap reloaded by {0}", p.name));
            Player.SendMessage(p, string.Format("&4Finished reloading for {0}", who2.name));
        }

        // Token: 0x06001477 RID: 5239 RVA: 0x00071258 File Offset: 0x0006F458
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reveal <name> - Reveals the map for <name>.");
            Player.SendMessage(p, "/reveal all - Reveals for all in the map");
            Player.SendMessage(p, "Will reload the map for anyone. (incl. banned)");
        }
    }
}