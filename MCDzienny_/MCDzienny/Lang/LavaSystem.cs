using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Lang
{
    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    class LavaSystem
    {
        static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("MCDzienny.Lang.LavaSystem", typeof(LavaSystem).Assembly);
                    resourceMan = resourceManager;
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string LavaStateCalm { get { return ResourceManager.GetString("LavaStateCalm", Culture); } }

        internal static string LavaStateDisturbed { get { return ResourceManager.GetString("LavaStateDisturbed", Culture); } }

        internal static string LavaStateFurious { get { return ResourceManager.GetString("LavaStateFurious", Culture); } }

        internal static string LavaStateWild { get { return ResourceManager.GetString("LavaStateWild", Culture); } }

        internal static string MapAuthor { get { return ResourceManager.GetString("MapAuthor", Culture); } }

        internal static string MapName { get { return ResourceManager.GetString("MapName", Culture); } }

        internal static string MapNext { get { return ResourceManager.GetString("MapNext", Culture); } }

        internal static string MapRatingResults { get { return ResourceManager.GetString("MapRatingResults", Culture); } }

        internal static string MapRatingTip { get { return ResourceManager.GetString("MapRatingTip", Culture); } }

        internal static string RewardMessageAboveSea { get { return ResourceManager.GetString("RewardMessageAboveSea", Culture); } }

        internal static string RewardMessageBelowSea { get { return ResourceManager.GetString("RewardMessageBelowSea", Culture); } }

        internal static string RewardMessageBelowSea2 { get { return ResourceManager.GetString("RewardMessageBelowSea2", Culture); } }

        internal static string Voted1 { get { return ResourceManager.GetString("Voted1", Culture); } }

        internal static string Voted2 { get { return ResourceManager.GetString("Voted2", Culture); } }

        internal static string Voted3 { get { return ResourceManager.GetString("Voted3", Culture); } }

        internal static string VoteForNextMap { get { return ResourceManager.GetString("VoteForNextMap", Culture); } }

        internal static string VoteOptions { get { return ResourceManager.GetString("VoteOptions", Culture); } }

        internal static string VoteResults { get { return ResourceManager.GetString("VoteResults", Culture); } }

        internal static string VoteResults2 { get { return ResourceManager.GetString("VoteResults2", Culture); } }

        internal static string VoteTip { get { return ResourceManager.GetString("VoteTip", Culture); } }

        internal static string WarningAlreadyVoted { get { return ResourceManager.GetString("WarningAlreadyVoted", Culture); } }

        internal static string WarningTooCloseToSpawn { get { return ResourceManager.GetString("WarningTooCloseToSpawn", Culture); } }

        internal static string YouHaveTheHighestRank { get { return ResourceManager.GetString("YouHaveTheHighestRank", Culture); } }
    }
}