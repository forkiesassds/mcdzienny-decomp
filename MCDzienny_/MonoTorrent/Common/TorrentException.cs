using System;
using System.Runtime.Serialization;

namespace MonoTorrent.Common
{
    // Token: 0x0200038E RID: 910
    [Serializable]
    public class TorrentException : Exception
    {
        // Token: 0x060019FD RID: 6653 RVA: 0x000B65D8 File Offset: 0x000B47D8
        public TorrentException()
        {
        }

        // Token: 0x060019FE RID: 6654 RVA: 0x000B65E0 File Offset: 0x000B47E0
        public TorrentException(string message) : base(message)
        {
        }

        // Token: 0x060019FF RID: 6655 RVA: 0x000B65EC File Offset: 0x000B47EC
        public TorrentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Token: 0x06001A00 RID: 6656 RVA: 0x000B65F8 File Offset: 0x000B47F8
        public TorrentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}