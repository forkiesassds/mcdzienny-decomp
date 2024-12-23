using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Lang
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    class Player
    {
        static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("MCDzienny.Lang.Player", typeof(Player).Assembly);
                    resourceMan = resourceManager;
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string AfkMessage { get { return ResourceManager.GetString("AfkMessage", Culture); } }

        internal static string AfkNoLonger { get { return ResourceManager.GetString("AfkNoLonger", Culture); } }

        internal static string ChatAppended { get { return ResourceManager.GetString("ChatAppended", Culture); } }

        internal static string ChatIllegalCharacter { get { return ResourceManager.GetString("ChatIllegalCharacter", Culture); } }

        internal static string ChatModeration { get { return ResourceManager.GetString("ChatModeration", Culture); } }

        internal static string ChatMuted { get { return ResourceManager.GetString("ChatMuted", Culture); } }

        internal static string ChatNoMessageEntered { get { return ResourceManager.GetString("ChatNoMessageEntered", Culture); } }

        internal static string ChatTempMutedTime { get { return ResourceManager.GetString("ChatTempMutedTime", Culture); } }

        internal static string ChatToOps { get { return ResourceManager.GetString("ChatToOps", Culture); } }

        internal static string CommandFailed { get { return ResourceManager.GetString("CommandFailed", Culture); } }

        internal static string CommandJailWarning { get { return ResourceManager.GetString("CommandJailWarning", Culture); } }

        internal static string CommandNoBind { get { return ResourceManager.GetString("CommandNoBind", Culture); } }

        internal static string CommandNoEntered { get { return ResourceManager.GetString("CommandNoEntered", Culture); } }

        internal static string CommandNotAllowedToUse { get { return ResourceManager.GetString("CommandNotAllowedToUse", Culture); } }

        internal static string CommandNoUseWhenMuted { get { return ResourceManager.GetString("CommandNoUseWhenMuted", Culture); } }

        internal static string CommandPlayerNonexistent { get { return ResourceManager.GetString("CommandPlayerNonexistent", Culture); } }

        internal static string CommandUnknown { get { return ResourceManager.GetString("CommandUnknown", Culture); } }

        internal static string CommandUsedOneself { get { return ResourceManager.GetString("CommandUsedOneself", Culture); } }

        internal static string DiedTimesGlobalMessage { get { return ResourceManager.GetString("DiedTimesGlobalMessage", Culture); } }

        internal static string ErrorCommand { get { return ResourceManager.GetString("ErrorCommand", Culture); } }

        internal static string ErrorOccured { get { return ResourceManager.GetString("ErrorOccured", Culture); } }

        internal static string GlobalChatWorldTag { get { return ResourceManager.GetString("GlobalChatWorldTag", Culture); } }

        internal static string GlobalMessageKicked { get { return ResourceManager.GetString("GlobalMessageKicked", Culture); } }

        internal static string GlobalMessageLeftGame { get { return ResourceManager.GetString("GlobalMessageLeftGame", Culture); } }

        internal static string IrcMessageKicked { get { return ResourceManager.GetString("IrcMessageKicked", Culture); } }

        internal static string JoinGlobalMessage { get { return ResourceManager.GetString("JoinGlobalMessage", Culture); } }

        internal static string KickAfk { get { return ResourceManager.GetString("KickAfk", Culture); } }

        internal static string KickAlreadyLogged { get { return ResourceManager.GetString("KickAlreadyLogged", Culture); } }

        internal static string KickDisconnected { get { return ResourceManager.GetString("KickDisconnected", Culture); } }

        internal static string KickHacks { get { return ResourceManager.GetString("KickHacks", Culture); } }

        internal static string KickIllegalName { get { return ResourceManager.GetString("KickIllegalName", Culture); } }

        internal static string KickLoggedAsYou { get { return ResourceManager.GetString("KickLoggedAsYou", Culture); } }

        internal static string KickLoginFailed { get { return ResourceManager.GetString("KickLoginFailed", Culture); } }

        internal static string KickServerFull { get { return ResourceManager.GetString("KickServerFull", Culture); } }

        internal static string KickTempBan { get { return ResourceManager.GetString("KickTempBan", Culture); } }

        internal static string KickUnknownAction { get { return ResourceManager.GetString("KickUnknownAction", Culture); } }

        internal static string KickWrongVersion { get { return ResourceManager.GetString("KickWrongVersion", Culture); } }

        internal static string LatelyKnownAs { get { return ResourceManager.GetString("LatelyKnownAs", Culture); } }

        internal static string VoteDecisionNo { get { return ResourceManager.GetString("VoteDecisionNo", Culture); } }

        internal static string VoteDecisionNoShortcut { get { return ResourceManager.GetString("VoteDecisionNoShortcut", Culture); } }

        internal static string VoteDecisionYes { get { return ResourceManager.GetString("VoteDecisionYes", Culture); } }

        internal static string VoteDecisionYesShortcut { get { return ResourceManager.GetString("VoteDecisionYesShortcut", Culture); } }

        internal static string VoteThanks { get { return ResourceManager.GetString("VoteThanks", Culture); } }

        internal static string WarningBreakingBlocks { get { return ResourceManager.GetString("WarningBreakingBlocks", Culture); } }

        internal static string WarningBuiltTooFar { get { return ResourceManager.GetString("WarningBuiltTooFar", Culture); } }

        internal static string WarningBuiltTooLow { get { return ResourceManager.GetString("WarningBuiltTooLow", Culture); } }

        internal static string WarningCantBuildHere { get { return ResourceManager.GetString("WarningCantBuildHere", Culture); } }

        internal static string WarningCantDisturbBlock { get { return ResourceManager.GetString("WarningCantDisturbBlock", Culture); } }

        internal static string WarningDisallowedBlockType { get { return ResourceManager.GetString("WarningDisallowedBlockType", Culture); } }

        internal static string WarningNoMessageStored { get { return ResourceManager.GetString("WarningNoMessageStored", Culture); } }

        internal static string WarningPortalDestinationUnloaded { get { return ResourceManager.GetString("WarningPortalDestinationUnloaded", Culture); } }

        internal static string WarningPortalHasNoExit { get { return ResourceManager.GetString("WarningPortalHasNoExit", Culture); } }

        internal static string WarningTooHighPillar { get { return ResourceManager.GetString("WarningTooHighPillar", Culture); } }

        internal static string WelcomeAnotherVisit { get { return ResourceManager.GetString("WelcomeAnotherVisit", Culture); } }

        internal static string WelcomeAwards { get { return ResourceManager.GetString("WelcomeAwards", Culture); } }

        internal static string WelcomeFirstVisit { get { return ResourceManager.GetString("WelcomeFirstVisit", Culture); } }

        internal static string WelcomeInbox { get { return ResourceManager.GetString("WelcomeInbox", Culture); } }

        internal static string WelcomeLowLag { get { return ResourceManager.GetString("WelcomeLowLag", Culture); } }

        internal static string WelcomeModified { get { return ResourceManager.GetString("WelcomeModified", Culture); } }

        internal static string WelcomeMoney { get { return ResourceManager.GetString("WelcomeMoney", Culture); } }

        internal static string WelcomePlayerCount { get { return ResourceManager.GetString("WelcomePlayerCount", Culture); } }

        internal static string WelcomePlayersCount { get { return ResourceManager.GetString("WelcomePlayersCount", Culture); } }
    }
}