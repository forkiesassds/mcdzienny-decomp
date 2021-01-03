using System.Collections.Generic;
using MCDzienny.Commands;

namespace MCDzienny
{
    // Token: 0x0200000F RID: 15
    public abstract class Command
    {
        // Token: 0x04000040 RID: 64
        public static CommandList all = new CommandList();

        // Token: 0x04000041 RID: 65
        public static CommandList core = new CommandList();

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000044 RID: 68
        public abstract string name { get; }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000045 RID: 69
        public abstract string shortcut { get; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000046 RID: 70
        public abstract string type { get; }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000047 RID: 71
        public abstract bool museumUsable { get; }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000048 RID: 72
        public abstract LevelPermission defaultRank { get; }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x0600004B RID: 75 RVA: 0x000036EC File Offset: 0x000018EC
        public virtual bool ConsoleAccess
        {
            get { return true; }
        }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600004C RID: 76 RVA: 0x000036F0 File Offset: 0x000018F0
        public virtual CommandScope Scope
        {
            get { return CommandScope.All; }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600004D RID: 77 RVA: 0x000036F4 File Offset: 0x000018F4
        public virtual bool HighSecurity
        {
            get { return false; }
        }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x0600004E RID: 78 RVA: 0x000036F8 File Offset: 0x000018F8
        public virtual string CustomName
        {
            get { return null; }
        }

        // Token: 0x06000049 RID: 73
        public abstract void Use(Player p, string message);

        // Token: 0x0600004A RID: 74
        public abstract void Help(Player p);

        // Token: 0x0600004F RID: 79 RVA: 0x000036FC File Offset: 0x000018FC
        public virtual void Init()
        {
        }

        // Token: 0x06000050 RID: 80 RVA: 0x00003700 File Offset: 0x00001900
        protected bool StopConsoleUse(Player p)
        {
            if (p == null)
            {
                Player.SendMessage(p, "You can't use this command from console.");
                return true;
            }

            return false;
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00003714 File Offset: 0x00001914
        public bool IsWithinScope(Player p)
        {
            return Scope == CommandScope.All ||
                   p.level.mapType == MapType.Lava && (Scope & CommandScope.Lava) == CommandScope.Lava ||
                   (p.level.mapType == MapType.Freebuild || p.level.mapType == MapType.Home ||
                    p.level.mapType == MapType.MyMap) && (Scope & CommandScope.Freebuild) == CommandScope.Freebuild ||
                   p.level.mapType == MapType.Zombie && (Scope & CommandScope.Zombie) == CommandScope.Zombie ||
                   p.level.mapType == MapType.Home && (Scope & CommandScope.Home) == CommandScope.Home ||
                   p.level.mapType == MapType.MyMap && (Scope & CommandScope.MyMap) == CommandScope.MyMap;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x000037D4 File Offset: 0x000019D4
        public static void InitAll()
        {
            all.Add(new CmdAbort());
            all.Add(new CmdAbout());
            all.Add(new CmdAfk());
            all.Add(new CmdBan());
            all.Add(new CmdBanip());
            all.Add(new CmdBind());
            all.Add(new CmdBlocks());
            all.Add(new CmdBlockSet());
            all.Add(new CmdBotAdd());
            all.Add(new CmdBotAI());
            all.Add(new CmdBotRemove());
            all.Add(new CmdBots());
            all.Add(new CmdBotSet());
            all.Add(new CmdBotSummon());
            all.Add(new CmdClearBlockChanges());
            all.Add(new CmdClick());
            all.Add(new CmdClones());
            all.Add(new CmdCmdBind());
            all.Add(new CmdCmdSet());
            all.Add(new CmdCmdUnload());
            all.Add(new CmdColor());
            all.Add(new CmdCopy());
            all.Add(new CmdCuboid());
            all.Add(new CmdDelete());
            all.Add(new CmdDeleteLvl());
            all.Add(new CmdDrop());
            all.Add(new CmdEmote());
            all.Add(new CmdFill());
            all.Add(new CmdFixGrass());
            all.Add(new CmdFlipHeads());
            all.Add(new CmdFly());
            all.Add(new CmdFollow());
            all.Add(new CmdFreeze());
            all.Add(new CmdGive());
            all.Add(new CmdGoto());
            all.Add(new CmdGun());
            all.Add(new CmdHacks());
            all.Add(new CmdHasirc());
            all.Add(new CmdHelp());
            all.Add(new CmdHide());
            all.Add(new CmdHighlight());
            all.Add(new CmdImport());
            all.Add(new CmdImageprint());
            all.Add(new CmdInbox());
            all.Add(new CmdInfo());
            all.Add(new CmdInvincible());
            all.Add(new CmdJail());
            all.Add(new CmdJoker());
            all.Add(new CmdKick());
            all.Add(new CmdKickban());
            all.Add(new CmdKill());
            all.Add(new CmdLastCmd());
            all.Add(new CmdLevels());
            all.Add(new CmdLimit());
            all.Add(new CmdLine());
            all.Add(new CmdLoad());
            all.Add(new CmdLowlag());
            all.Add(new CmdMap());
            all.Add(new CmdMapInfo());
            all.Add(new CmdMe());
            all.Add(new CmdMeasure());
            all.Add(new CmdMegaboid());
            all.Add(new CmdMessageBlock());
            all.Add(new CmdMissile());
            all.Add(new CmdMode());
            all.Add(new CmdModerate());
            all.Add(new CmdMove());
            all.Add(new CmdMuseum());
            all.Add(new CmdMute());
            all.Add(new CmdNewLvl());
            all.Add(new CmdOpChat());
            all.Add(new CmdOutline());
            all.Add(new CmdPaint());
            all.Add(new CmdPaste());
            all.Add(new CmdPause());
            all.Add(new CmdPay());
            all.Add(new CmdPCount());
            all.Add(new CmdPermissionBuild());
            all.Add(new CmdPermissionVisit());
            all.Add(new CmdPhysics());
            all.Add(new CmdPlace());
            all.Add(new CmdPlayers());
            all.Add(new CmdPortal());
            all.Add(new CmdPossess());
            all.Add(new CmdRainbow());
            all.Add(new CmdRedo());
            all.Add(new CmdRepeat());
            all.Add(new CmdReplace());
            all.Add(new CmdReplaceAll());
            all.Add(new CmdReplaceNot());
            all.Add(new CmdResetBot());
            all.Add(new CmdRestart());
            all.Add(new CmdRestartPhysics());
            all.Add(new CmdRestore());
            all.Add(new CmdRetrieve());
            all.Add(new CmdReveal());
            all.Add(new CmdRide());
            all.Add(new CmdRoll());
            all.Add(new CmdRules());
            all.Add(new CmdSave());
            all.Add(new CmdSay());
            all.Add(new CmdSend());
            all.Add(new CmdServerReport());
            all.Add(new CmdSetRank());
            all.Add(new CmdSetspawn());
            all.Add(new CmdSlap());
            all.Add(new CmdSpawn());
            all.Add(new CmdSpheroid());
            all.Add(new CmdSpin());
            all.Add(new CmdStairs());
            all.Add(new CmdStatic());
            all.Add(new CmdStore());
            all.Add(new CmdSummon());
            all.Add(new CmdTake());
            all.Add(new CmdTColor());
            all.Add(new CmdTempBan());
            all.Add(new CmdText());
            all.Add(new CmdTime());
            all.Add(new CmdTitle());
            all.Add(new CmdTnt());
            all.Add(new CmdTp());
            all.Add(new CmdTpZone());
            all.Add(new CmdTree());
            all.Add(new CmdTrust());
            all.Add(new CmdUnban());
            all.Add(new CmdUnbanip());
            all.Add(new CmdUndo());
            all.Add(new CmdUnload());
            all.Add(new CmdUnloaded());
            all.Add(new CmdUpdate());
            all.Add(new CmdView());
            all.Add(new CmdViewRanks());
            all.Add(new CmdVoice());
            all.Add(new CmdWhisper());
            if (Server.useWhitelist) all.Add(new CmdWhitelist());
            all.Add(new CmdWhoip());
            all.Add(new CmdWhois());
            all.Add(new CmdWhowas());
            all.Add(new CmdWrite());
            all.Add(new CmdZone());
            all.Add(new CmdCrashServer());
            all.Add(new CmdPromote());
            all.Add(new CmdDemote());
            all.Add(new CmdDrill());
            all.Add(new CmdAward());
            all.Add(new CmdAwards());
            all.Add(new CmdAwardMod());
            all.Add(new CmdCountdown());
            all.Add(new CmdTimeleft());
            all.Add(new CmdSetLava());
            all.Add(new CmdPoints());
            all.Add(new CmdAlive());
            all.Add(new CmdLives());
            all.Add(new CmdPosition());
            all.Add(new CmdScore());
            all.Add(new CmdTips());
            all.Add(new CmdTopten());
            all.Add(new CmdBestScores());
            all.Add(new CmdBuy());
            all.Add(new CmdChangePlayerExp());
            all.Add(new CmdSummonSpawn());
            all.Add(new CmdHammer());
            all.Add(new CmdItems());
            all.Add(new CmdXban());
            all.Add(new CmdPing());
            all.Add(new CmdCompile());
            all.Add(new CmdCmdCreate());
            all.Add(new CmdCmdLoad());
            all.Add(new CmdSaveLavaMap());
            all.Add(new CmdLoadLavaMap());
            all.Add(new CmdSetTitle());
            all.Add(new CmdClearChat());
            all.Add(new CmdFetch());
            all.Add(new CmdKickAll());
            all.Add(new CmdTimedMessage());
            all.Add(new CmdWelcome());
            all.Add(new CmdFarewell());
            all.Add(new CmdVoteKick());
            all.Add(new CmdVoteBan());
            all.Add(new CmdVote());
            all.Add(new CmdLike());
            all.Add(new CmdDislike());
            all.Add(new CmdResults());
            all.Add(new CmdReset());
            all.Add(new CmdSetColor());
            all.Add(new CmdTitleColor());
            all.Add(new CmdLavaPortal());
            all.Add(new CmdCodes());
            all.Add(new CmdInfection());
            all.Add(new CmdPillarRemover());
            all.Add(new CmdEllipsoid());
            all.Add(new CmdLoadZombieMap());
            all.Add(new CmdReflection());
            all.Add(new CmdMake());
            all.Add(new CmdRenameLvl());
            all.Add(new CmdShutdown());
            all.Add(new CmdMoveAll());
            all.Add(new CmdVoteAbort());
            all.Add(new CmdRankMsg());
            all.Add(new CmdEightBall());
            all.Add(new CmdTempMute());
            all.Add(new CmdImpersonate());
            all.Add(new CmdTriangle());
            all.Add(new CmdQuad());
            all.Add(new CmdWall());
            all.Add(new CmdReview());
            all.Add(new CmdWomid());
            all.Add(new CmdHome());
            all.Add(new CmdTest());
            all.Add(new CmdIronman());
            all.Add(new CmdIronwoman());
            all.Add(new CmdXundo());
            all.Add(new CmdStats());
            all.Add(new CmdSetZombie());
            all.Add(new CmdZombies());
            all.Add(new CmdHumans());
            all.Add(new CmdReferee());
            all.Add(new CmdDraw());
            all.Add(new CmdAka());
            all.Add(new CmdHighfive());
            all.Add(new CmdPoke());
            all.Add(new CmdFacepalm());
            all.Add(new CmdStars());
            all.Add(new CmdMyMap());
            all.Add(new CmdAccept());
            all.Add(new CmdFixMyMaps());
            all.Add(new CmdSetModel());
            all.Add(new CmdDebug());
            all.Add(new CmdEnvironment());
            all.Add(new CmdWeather());
            all.Add(new CmdTexture());
            all.Sort();
            core.commands = new List<Command>(all.commands);
            Scripting.Autoload();
        }
    }
}