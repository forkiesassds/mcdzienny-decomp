using System;

namespace MCDzienny.Script
{
    // Token: 0x02000207 RID: 519
    public static class NetFrameworkVersionExtensions
    {
        // Token: 0x06000E46 RID: 3654 RVA: 0x0004FCE4 File Offset: 0x0004DEE4
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