using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Lang
{
    // Token: 0x020000AC RID: 172
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Player
    {
        // Token: 0x0400025D RID: 605
        private static ResourceManager resourceMan;

        // Token: 0x0400025E RID: 606
        private static CultureInfo resourceCulture;

        // Token: 0x06000582 RID: 1410 RVA: 0x0001B7FC File Offset: 0x000199FC

        // Token: 0x17000240 RID: 576
        // (get) Token: 0x06000583 RID: 1411 RVA: 0x0001B804 File Offset: 0x00019A04
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    var resourceManager =
                        new ResourceManager("MCDzienny_.MCDzienny.Lang.Player", typeof(Player).Assembly);
                    resourceMan = resourceManager;
                }

                return resourceMan;
            }
        }

        // Token: 0x17000241 RID: 577
        // (get) Token: 0x06000584 RID: 1412 RVA: 0x0001B844 File Offset: 0x00019A44
        // (set) Token: 0x06000585 RID: 1413 RVA: 0x0001B84C File Offset: 0x00019A4C
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        // Token: 0x17000242 RID: 578
        // (get) Token: 0x06000586 RID: 1414 RVA: 0x0001B854 File Offset: 0x00019A54
        internal static string AfkMessage
        {
            get { return ResourceManager.GetString("AfkMessage", resourceCulture); }
        }

        // Token: 0x17000243 RID: 579
        // (get) Token: 0x06000587 RID: 1415 RVA: 0x0001B86C File Offset: 0x00019A6C
        internal static string AfkNoLonger
        {
            get { return ResourceManager.GetString("AfkNoLonger", resourceCulture); }
        }

        // Token: 0x17000244 RID: 580
        // (get) Token: 0x06000588 RID: 1416 RVA: 0x0001B884 File Offset: 0x00019A84
        internal static string ChatAppended
        {
            get { return ResourceManager.GetString("ChatAppended", resourceCulture); }
        }

        // Token: 0x17000245 RID: 581
        // (get) Token: 0x06000589 RID: 1417 RVA: 0x0001B89C File Offset: 0x00019A9C
        internal static string ChatIllegalCharacter
        {
            get { return ResourceManager.GetString("ChatIllegalCharacter", resourceCulture); }
        }

        // Token: 0x17000246 RID: 582
        // (get) Token: 0x0600058A RID: 1418 RVA: 0x0001B8B4 File Offset: 0x00019AB4
        internal static string ChatModeration
        {
            get { return ResourceManager.GetString("ChatModeration", resourceCulture); }
        }

        // Token: 0x17000247 RID: 583
        // (get) Token: 0x0600058B RID: 1419 RVA: 0x0001B8CC File Offset: 0x00019ACC
        internal static string ChatMuted
        {
            get { return ResourceManager.GetString("ChatMuted", resourceCulture); }
        }

        // Token: 0x17000248 RID: 584
        // (get) Token: 0x0600058C RID: 1420 RVA: 0x0001B8E4 File Offset: 0x00019AE4
        internal static string ChatNoMessageEntered
        {
            get { return ResourceManager.GetString("ChatNoMessageEntered", resourceCulture); }
        }

        // Token: 0x17000249 RID: 585
        // (get) Token: 0x0600058D RID: 1421 RVA: 0x0001B8FC File Offset: 0x00019AFC
        internal static string ChatTempMutedTime
        {
            get { return ResourceManager.GetString("ChatTempMutedTime", resourceCulture); }
        }

        // Token: 0x1700024A RID: 586
        // (get) Token: 0x0600058E RID: 1422 RVA: 0x0001B914 File Offset: 0x00019B14
        internal static string ChatToOps
        {
            get { return ResourceManager.GetString("ChatToOps", resourceCulture); }
        }

        // Token: 0x1700024B RID: 587
        // (get) Token: 0x0600058F RID: 1423 RVA: 0x0001B92C File Offset: 0x00019B2C
        internal static string CommandFailed
        {
            get { return ResourceManager.GetString("CommandFailed", resourceCulture); }
        }

        // Token: 0x1700024C RID: 588
        // (get) Token: 0x06000590 RID: 1424 RVA: 0x0001B944 File Offset: 0x00019B44
        internal static string CommandJailWarning
        {
            get { return ResourceManager.GetString("CommandJailWarning", resourceCulture); }
        }

        // Token: 0x1700024D RID: 589
        // (get) Token: 0x06000591 RID: 1425 RVA: 0x0001B95C File Offset: 0x00019B5C
        internal static string CommandNoBind
        {
            get { return ResourceManager.GetString("CommandNoBind", resourceCulture); }
        }

        // Token: 0x1700024E RID: 590
        // (get) Token: 0x06000592 RID: 1426 RVA: 0x0001B974 File Offset: 0x00019B74
        internal static string CommandNoEntered
        {
            get { return ResourceManager.GetString("CommandNoEntered", resourceCulture); }
        }

        // Token: 0x1700024F RID: 591
        // (get) Token: 0x06000593 RID: 1427 RVA: 0x0001B98C File Offset: 0x00019B8C
        internal static string CommandNotAllowedToUse
        {
            get { return ResourceManager.GetString("CommandNotAllowedToUse", resourceCulture); }
        }

        // Token: 0x17000250 RID: 592
        // (get) Token: 0x06000594 RID: 1428 RVA: 0x0001B9A4 File Offset: 0x00019BA4
        internal static string CommandNoUseWhenMuted
        {
            get { return ResourceManager.GetString("CommandNoUseWhenMuted", resourceCulture); }
        }

        // Token: 0x17000251 RID: 593
        // (get) Token: 0x06000595 RID: 1429 RVA: 0x0001B9BC File Offset: 0x00019BBC
        internal static string CommandPlayerNonexistent
        {
            get { return ResourceManager.GetString("CommandPlayerNonexistent", resourceCulture); }
        }

        // Token: 0x17000252 RID: 594
        // (get) Token: 0x06000596 RID: 1430 RVA: 0x0001B9D4 File Offset: 0x00019BD4
        internal static string CommandUnknown
        {
            get { return ResourceManager.GetString("CommandUnknown", resourceCulture); }
        }

        // Token: 0x17000253 RID: 595
        // (get) Token: 0x06000597 RID: 1431 RVA: 0x0001B9EC File Offset: 0x00019BEC
        internal static string CommandUsedOneself
        {
            get { return ResourceManager.GetString("CommandUsedOneself", resourceCulture); }
        }

        // Token: 0x17000254 RID: 596
        // (get) Token: 0x06000598 RID: 1432 RVA: 0x0001BA04 File Offset: 0x00019C04
        internal static string DiedTimesGlobalMessage
        {
            get { return ResourceManager.GetString("DiedTimesGlobalMessage", resourceCulture); }
        }

        // Token: 0x17000255 RID: 597
        // (get) Token: 0x06000599 RID: 1433 RVA: 0x0001BA1C File Offset: 0x00019C1C
        internal static string ErrorCommand
        {
            get { return ResourceManager.GetString("ErrorCommand", resourceCulture); }
        }

        // Token: 0x17000256 RID: 598
        // (get) Token: 0x0600059A RID: 1434 RVA: 0x0001BA34 File Offset: 0x00019C34
        internal static string ErrorOccured
        {
            get { return ResourceManager.GetString("ErrorOccured", resourceCulture); }
        }

        // Token: 0x17000257 RID: 599
        // (get) Token: 0x0600059B RID: 1435 RVA: 0x0001BA4C File Offset: 0x00019C4C
        internal static string GlobalChatWorldTag
        {
            get { return ResourceManager.GetString("GlobalChatWorldTag", resourceCulture); }
        }

        // Token: 0x17000258 RID: 600
        // (get) Token: 0x0600059C RID: 1436 RVA: 0x0001BA64 File Offset: 0x00019C64
        internal static string GlobalMessageKicked
        {
            get { return ResourceManager.GetString("GlobalMessageKicked", resourceCulture); }
        }

        // Token: 0x17000259 RID: 601
        // (get) Token: 0x0600059D RID: 1437 RVA: 0x0001BA7C File Offset: 0x00019C7C
        internal static string GlobalMessageLeftGame
        {
            get { return ResourceManager.GetString("GlobalMessageLeftGame", resourceCulture); }
        }

        // Token: 0x1700025A RID: 602
        // (get) Token: 0x0600059E RID: 1438 RVA: 0x0001BA94 File Offset: 0x00019C94
        internal static string IrcMessageKicked
        {
            get { return ResourceManager.GetString("IrcMessageKicked", resourceCulture); }
        }

        // Token: 0x1700025B RID: 603
        // (get) Token: 0x0600059F RID: 1439 RVA: 0x0001BAAC File Offset: 0x00019CAC
        internal static string JoinGlobalMessage
        {
            get { return ResourceManager.GetString("JoinGlobalMessage", resourceCulture); }
        }

        // Token: 0x1700025C RID: 604
        // (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001BAC4 File Offset: 0x00019CC4
        internal static string KickAfk
        {
            get { return ResourceManager.GetString("KickAfk", resourceCulture); }
        }

        // Token: 0x1700025D RID: 605
        // (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001BADC File Offset: 0x00019CDC
        internal static string KickAlreadyLogged
        {
            get { return ResourceManager.GetString("KickAlreadyLogged", resourceCulture); }
        }

        // Token: 0x1700025E RID: 606
        // (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001BAF4 File Offset: 0x00019CF4
        internal static string KickDisconnected
        {
            get { return ResourceManager.GetString("KickDisconnected", resourceCulture); }
        }

        // Token: 0x1700025F RID: 607
        // (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001BB0C File Offset: 0x00019D0C
        internal static string KickHacks
        {
            get { return ResourceManager.GetString("KickHacks", resourceCulture); }
        }

        // Token: 0x17000260 RID: 608
        // (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001BB24 File Offset: 0x00019D24
        internal static string KickIllegalName
        {
            get { return ResourceManager.GetString("KickIllegalName", resourceCulture); }
        }

        // Token: 0x17000261 RID: 609
        // (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001BB3C File Offset: 0x00019D3C
        internal static string KickLoggedAsYou
        {
            get { return ResourceManager.GetString("KickLoggedAsYou", resourceCulture); }
        }

        // Token: 0x17000262 RID: 610
        // (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001BB54 File Offset: 0x00019D54
        internal static string KickLoginFailed
        {
            get { return ResourceManager.GetString("KickLoginFailed", resourceCulture); }
        }

        // Token: 0x17000263 RID: 611
        // (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001BB6C File Offset: 0x00019D6C
        internal static string KickServerFull
        {
            get { return ResourceManager.GetString("KickServerFull", resourceCulture); }
        }

        // Token: 0x17000264 RID: 612
        // (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001BB84 File Offset: 0x00019D84
        internal static string KickTempBan
        {
            get { return ResourceManager.GetString("KickTempBan", resourceCulture); }
        }

        // Token: 0x17000265 RID: 613
        // (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001BB9C File Offset: 0x00019D9C
        internal static string KickUnknownAction
        {
            get { return ResourceManager.GetString("KickUnknownAction", resourceCulture); }
        }

        // Token: 0x17000266 RID: 614
        // (get) Token: 0x060005AA RID: 1450 RVA: 0x0001BBB4 File Offset: 0x00019DB4
        internal static string KickWrongVersion
        {
            get { return ResourceManager.GetString("KickWrongVersion", resourceCulture); }
        }

        // Token: 0x17000267 RID: 615
        // (get) Token: 0x060005AB RID: 1451 RVA: 0x0001BBCC File Offset: 0x00019DCC
        internal static string LatelyKnownAs
        {
            get { return ResourceManager.GetString("LatelyKnownAs", resourceCulture); }
        }

        // Token: 0x17000268 RID: 616
        // (get) Token: 0x060005AC RID: 1452 RVA: 0x0001BBE4 File Offset: 0x00019DE4
        internal static string VoteDecisionNo
        {
            get { return ResourceManager.GetString("VoteDecisionNo", resourceCulture); }
        }

        // Token: 0x17000269 RID: 617
        // (get) Token: 0x060005AD RID: 1453 RVA: 0x0001BBFC File Offset: 0x00019DFC
        internal static string VoteDecisionNoShortcut
        {
            get { return ResourceManager.GetString("VoteDecisionNoShortcut", resourceCulture); }
        }

        // Token: 0x1700026A RID: 618
        // (get) Token: 0x060005AE RID: 1454 RVA: 0x0001BC14 File Offset: 0x00019E14
        internal static string VoteDecisionYes
        {
            get { return ResourceManager.GetString("VoteDecisionYes", resourceCulture); }
        }

        // Token: 0x1700026B RID: 619
        // (get) Token: 0x060005AF RID: 1455 RVA: 0x0001BC2C File Offset: 0x00019E2C
        internal static string VoteDecisionYesShortcut
        {
            get { return ResourceManager.GetString("VoteDecisionYesShortcut", resourceCulture); }
        }

        // Token: 0x1700026C RID: 620
        // (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001BC44 File Offset: 0x00019E44
        internal static string VoteThanks
        {
            get { return ResourceManager.GetString("VoteThanks", resourceCulture); }
        }

        // Token: 0x1700026D RID: 621
        // (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001BC5C File Offset: 0x00019E5C
        internal static string WarningBreakingBlocks
        {
            get { return ResourceManager.GetString("WarningBreakingBlocks", resourceCulture); }
        }

        // Token: 0x1700026E RID: 622
        // (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001BC74 File Offset: 0x00019E74
        internal static string WarningBuiltTooFar
        {
            get { return ResourceManager.GetString("WarningBuiltTooFar", resourceCulture); }
        }

        // Token: 0x1700026F RID: 623
        // (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001BC8C File Offset: 0x00019E8C
        internal static string WarningBuiltTooLow
        {
            get { return ResourceManager.GetString("WarningBuiltTooLow", resourceCulture); }
        }

        // Token: 0x17000270 RID: 624
        // (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001BCA4 File Offset: 0x00019EA4
        internal static string WarningCantBuildHere
        {
            get { return ResourceManager.GetString("WarningCantBuildHere", resourceCulture); }
        }

        // Token: 0x17000271 RID: 625
        // (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001BCBC File Offset: 0x00019EBC
        internal static string WarningCantDisturbBlock
        {
            get { return ResourceManager.GetString("WarningCantDisturbBlock", resourceCulture); }
        }

        // Token: 0x17000272 RID: 626
        // (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001BCD4 File Offset: 0x00019ED4
        internal static string WarningDisallowedBlockType
        {
            get { return ResourceManager.GetString("WarningDisallowedBlockType", resourceCulture); }
        }

        // Token: 0x17000273 RID: 627
        // (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001BCEC File Offset: 0x00019EEC
        internal static string WarningNoMessageStored
        {
            get { return ResourceManager.GetString("WarningNoMessageStored", resourceCulture); }
        }

        // Token: 0x17000274 RID: 628
        // (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001BD04 File Offset: 0x00019F04
        internal static string WarningPortalDestinationUnloaded
        {
            get { return ResourceManager.GetString("WarningPortalDestinationUnloaded", resourceCulture); }
        }

        // Token: 0x17000275 RID: 629
        // (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001BD1C File Offset: 0x00019F1C
        internal static string WarningPortalHasNoExit
        {
            get { return ResourceManager.GetString("WarningPortalHasNoExit", resourceCulture); }
        }

        // Token: 0x17000276 RID: 630
        // (get) Token: 0x060005BA RID: 1466 RVA: 0x0001BD34 File Offset: 0x00019F34
        internal static string WarningTooHighPillar
        {
            get { return ResourceManager.GetString("WarningTooHighPillar", resourceCulture); }
        }

        // Token: 0x17000277 RID: 631
        // (get) Token: 0x060005BB RID: 1467 RVA: 0x0001BD4C File Offset: 0x00019F4C
        internal static string WelcomeAnotherVisit
        {
            get { return ResourceManager.GetString("WelcomeAnotherVisit", resourceCulture); }
        }

        // Token: 0x17000278 RID: 632
        // (get) Token: 0x060005BC RID: 1468 RVA: 0x0001BD64 File Offset: 0x00019F64
        internal static string WelcomeAwards
        {
            get { return ResourceManager.GetString("WelcomeAwards", resourceCulture); }
        }

        // Token: 0x17000279 RID: 633
        // (get) Token: 0x060005BD RID: 1469 RVA: 0x0001BD7C File Offset: 0x00019F7C
        internal static string WelcomeFirstVisit
        {
            get { return ResourceManager.GetString("WelcomeFirstVisit", resourceCulture); }
        }

        // Token: 0x1700027A RID: 634
        // (get) Token: 0x060005BE RID: 1470 RVA: 0x0001BD94 File Offset: 0x00019F94
        internal static string WelcomeInbox
        {
            get { return ResourceManager.GetString("WelcomeInbox", resourceCulture); }
        }

        // Token: 0x1700027B RID: 635
        // (get) Token: 0x060005BF RID: 1471 RVA: 0x0001BDAC File Offset: 0x00019FAC
        internal static string WelcomeLowLag
        {
            get { return ResourceManager.GetString("WelcomeLowLag", resourceCulture); }
        }

        // Token: 0x1700027C RID: 636
        // (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001BDC4 File Offset: 0x00019FC4
        internal static string WelcomeModified
        {
            get { return ResourceManager.GetString("WelcomeModified", resourceCulture); }
        }

        // Token: 0x1700027D RID: 637
        // (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001BDDC File Offset: 0x00019FDC
        internal static string WelcomeMoney
        {
            get { return ResourceManager.GetString("WelcomeMoney", resourceCulture); }
        }

        // Token: 0x1700027E RID: 638
        // (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001BDF4 File Offset: 0x00019FF4
        internal static string WelcomePlayerCount
        {
            get { return ResourceManager.GetString("WelcomePlayerCount", resourceCulture); }
        }

        // Token: 0x1700027F RID: 639
        // (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001BE0C File Offset: 0x0001A00C
        internal static string WelcomePlayersCount
        {
            get { return ResourceManager.GetString("WelcomePlayersCount", resourceCulture); }
        }
    }
}