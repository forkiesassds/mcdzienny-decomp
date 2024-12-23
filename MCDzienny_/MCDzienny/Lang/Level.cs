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
    class Level
    {
        static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("MCDzienny.Lang.Level", typeof(Level).Assembly);
                    resourceMan = resourceManager;
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string ErrorMapSaveGlobalMessage { get { return ResourceManager.GetString("ErrorMapSaveGlobalMessage", Culture); } }

        internal static string MapUnloadedGlobalMessage { get { return ResourceManager.GetString("MapUnloadedGlobalMessage", Culture); } }

        internal static string NotAllowedToPlaceGoldRock { get { return ResourceManager.GetString("NotAllowedToPlaceGoldRock", Culture); } }

        internal static string OutOfDoors { get { return ResourceManager.GetString("OutOfDoors", Culture); } }

        internal static string OutOfSponges { get { return ResourceManager.GetString("OutOfSponges", Culture); } }

        internal static string OutOfWater { get { return ResourceManager.GetString("OutOfWater", Culture); } }

        internal static string PhysicsShutdownGlobalMessage { get { return ResourceManager.GetString("PhysicsShutdownGlobalMessage", Culture); } }

        internal static string PhysicsWarning { get { return ResourceManager.GetString("PhysicsWarning", Culture); } }

        internal static string WarningBlockOwned { get { return ResourceManager.GetString("WarningBlockOwned", Culture); } }

        internal static string WarningBlockProtectedByLevel { get { return ResourceManager.GetString("WarningBlockProtectedByLevel", Culture); } }

        internal static string WarningBuildPermission { get { return ResourceManager.GetString("WarningBuildPermission", Culture); } }

        internal static string ZoneBelongsTo { get { return ResourceManager.GetString("ZoneBelongsTo", Culture); } }

        internal static string ZoneBelongsToNoOne { get { return ResourceManager.GetString("ZoneBelongsToNoOne", Culture); } }

        internal static string ZoneDeleted { get { return ResourceManager.GetString("ZoneDeleted", Culture); } }

        internal static string ZoneNotFoundNotDeleted { get { return ResourceManager.GetString("ZoneNotFoundNotDeleted", Culture); } }
    }
}