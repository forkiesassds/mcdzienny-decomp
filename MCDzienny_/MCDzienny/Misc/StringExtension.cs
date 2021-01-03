namespace MCDzienny.Misc
{
    // Token: 0x020001C9 RID: 457
    public static class StringExtension
    {
        // Token: 0x06000CC2 RID: 3266 RVA: 0x00049B98 File Offset: 0x00047D98
        public static bool IsNullOrWhiteSpaced(this string value)
        {
            if (value != null)
                for (var i = 0; i < value.Length; i++)
                    if (!char.IsWhiteSpace(value[i]))
                        return false;
            return true;
        }
    }
}