using System;

namespace MCDzienny.Script
{
    public static class NetFrameworkVersionExtensions
    {
        public static string GetVersionSignature(this NetFrameworkVersion version)
        {
            switch (version)
            {
                case NetFrameworkVersion.Net2:
                    return "v2.0";
                case NetFrameworkVersion.Net3:
                    return "v3.0";
                case NetFrameworkVersion.Net3_5:
                    return "v3.5";
                case NetFrameworkVersion.Net4:
                    return "v4.0";
                case NetFrameworkVersion.Net4_5:
                    return "v4.5";
                default:
                    throw new ArgumentException("Unknown value.", "version");
            }
        }
    }
}