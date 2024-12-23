namespace MonoTorrent.Common
{
    public enum TorrentState
    {
        Stopped,
        Paused,
        Downloading,
        Seeding,
        Hashing,
        Stopping,
        Error,
        Metadata
    }
}