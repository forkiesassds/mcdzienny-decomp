namespace MCDzienny.Plugins.KeyboardShortcuts
{
    public class CpeHotKeyInfo
    {
        public static readonly byte None = 0;

        public static readonly byte Ctrl = 1;

        public static readonly byte Shift = 2;

        public static readonly byte Alt = 4;

        public string Label { get; set; }

        public string Message { get; set; }

        public int KeyCode { get; set; }

        public byte KeyMod { get; set; }
    }
}