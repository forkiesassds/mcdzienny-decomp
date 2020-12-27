namespace MonoTorrent.Common
{
    // Token: 0x02000380 RID: 896
    public enum TorrentState
    {
        // Token: 0x04000E02 RID: 3586
        Stopped,

        // Token: 0x04000E03 RID: 3587
        Paused,

        // Token: 0x04000E04 RID: 3588
        Downloading,

        // Token: 0x04000E05 RID: 3589
        Seeding,

        // Token: 0x04000E06 RID: 3590
        Hashing,

        // Token: 0x04000E07 RID: 3591
        Stopping,

        // Token: 0x04000E08 RID: 3592
        Error,

        // Token: 0x04000E09 RID: 3593
        Metadata
    }
}