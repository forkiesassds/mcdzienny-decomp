using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Lang
{
    [CompilerGenerated]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    class Store
    {
        static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("MCDzienny.Lang.Store", typeof(Store).Assembly);
                    resourceMan = resourceManager;
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string BoughtArmor { get { return ResourceManager.GetString("BoughtArmor", Culture); } }

        internal static string BoughtColor { get { return ResourceManager.GetString("BoughtColor", Culture); } }

        internal static string BoughtColorTip { get { return ResourceManager.GetString("BoughtColorTip", Culture); } }

        internal static string BoughtDoors { get { return ResourceManager.GetString("BoughtDoors", Culture); } }

        internal static string BoughtFarewell { get { return ResourceManager.GetString("BoughtFarewell", Culture); } }

        internal static string BoughtFarewellTip { get { return ResourceManager.GetString("BoughtFarewellTip", Culture); } }

        internal static string BoughtHammer { get { return ResourceManager.GetString("BoughtHammer", Culture); } }

        internal static string BoughtLife { get { return ResourceManager.GetString("BoughtLife", Culture); } }

        internal static string BoughtPromotionWarning { get { return ResourceManager.GetString("BoughtPromotionWarning", Culture); } }

        internal static string BoughtSponges { get { return ResourceManager.GetString("BoughtSponges", Culture); } }

        internal static string BoughtTeleport { get { return ResourceManager.GetString("BoughtTeleport", Culture); } }

        internal static string BoughtTeleportTip { get { return ResourceManager.GetString("BoughtTeleportTip", Culture); } }

        internal static string BoughtTitle { get { return ResourceManager.GetString("BoughtTitle", Culture); } }

        internal static string BoughtTitleColor { get { return ResourceManager.GetString("BoughtTitleColor", Culture); } }

        internal static string BoughtTitleColorTip { get { return ResourceManager.GetString("BoughtTitleColorTip", Culture); } }

        internal static string BoughtTitleTip { get { return ResourceManager.GetString("BoughtTitleTip", Culture); } }

        internal static string BoughtWater { get { return ResourceManager.GetString("BoughtWater", Culture); } }

        internal static string BoughtWelcome { get { return ResourceManager.GetString("BoughtWelcome", Culture); } }

        internal static string BoughtWelcomeTip { get { return ResourceManager.GetString("BoughtWelcomeTip", Culture); } }

        internal static string MoreItemsTip { get { return ResourceManager.GetString("MoreItemsTip", Culture); } }

        internal static string NotEnoughMoney { get { return ResourceManager.GetString("NotEnoughMoney", Culture); } }

        internal static string PromotionItem { get { return ResourceManager.GetString("PromotionItem", Culture); } }

        internal static string PromotionNotAvailable { get { return ResourceManager.GetString("PromotionNotAvailable", Culture); } }
    }
}