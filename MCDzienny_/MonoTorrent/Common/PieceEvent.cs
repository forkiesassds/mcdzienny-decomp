namespace MonoTorrent.Common
{
    public enum PieceEvent
    {
        BlockWriteQueued,
        BlockNotRequested,
        BlockWrittenToDisk,
        HashPassed,
        HashFailed
    }
}