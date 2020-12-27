namespace MCDzienny
{
    // Token: 0x02000246 RID: 582
    public static class c
    {
        public const string black = "&0";

        public const string navy = "&1";

        public const string green = "&2";

        public const string teal = "&3";

        public const string maroon = "&4";

        public const string purple = "&5";

        public const string gold = "&6";

        public const string silver = "&7";

        public const string gray = "&8";

        public const string blue = "&9";

        public const string lime = "&a";

        public const string aqua = "&b";

        public const string red = "&c";

        public const string pink = "&d";

        public const string yellow = "&e";

        public const string white = "&f";

        public static string Parse(string str)
        {
            switch (str.ToLower())
            {
                case "black":
                    return "&0";
                case "navy":
                    return "&1";
                case "green":
                    return "&2";
                case "teal":
                    return "&3";
                case "maroon":
                    return "&4";
                case "purple":
                    return "&5";
                case "gold":
                    return "&6";
                case "silver":
                    return "&7";
                case "gray":
                    return "&8";
                case "blue":
                    return "&9";
                case "lime":
                    return "&a";
                case "aqua":
                    return "&b";
                case "red":
                    return "&c";
                case "pink":
                    return "&d";
                case "yellow":
                    return "&e";
                case "white":
                    return "&f";
                default:
                    return "";
            }
        }

        public static string Name(string str)
        {
            switch (str)
            {
                case "&0":
                    return "black";
                case "&1":
                    return "navy";
                case "&2":
                    return "green";
                case "&3":
                    return "teal";
                case "&4":
                    return "maroon";
                case "&5":
                    return "purple";
                case "&6":
                    return "gold";
                case "&7":
                    return "silver";
                case "&8":
                    return "gray";
                case "&9":
                    return "blue";
                case "&a":
                    return "lime";
                case "&b":
                    return "aqua";
                case "&c":
                    return "red";
                case "&d":
                    return "pink";
                case "&e":
                    return "yellow";
                case "&f":
                    return "white";
                default:
                    return "";
            }
        }
    }
}