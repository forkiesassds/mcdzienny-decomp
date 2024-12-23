namespace MonoTorrent.Common
{
    public enum Priority
    {
        DoNotDownload = 0,
        Lowest = 1,
        Low = 2,
        Normal = 4,
        High = 8,
        Highest = 0x10,
        Immediate = 0x20
    }
}