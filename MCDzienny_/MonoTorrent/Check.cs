using System;

namespace MonoTorrent
{
    // Token: 0x0200037B RID: 891
    public static class Check
    {
        // Token: 0x060019A7 RID: 6567 RVA: 0x000B5F2C File Offset: 0x000B412C
        private static void DoCheck(object toCheck, string name)
        {
            if (toCheck == null) throw new ArgumentNullException(name);
        }

        // Token: 0x060019A8 RID: 6568 RVA: 0x000B5F38 File Offset: 0x000B4138
        private static void IsNullOrEmpty(string toCheck, string name)
        {
            DoCheck(toCheck, name);
            if (toCheck.Length == 0) throw new ArgumentException("Cannot be empty", name);
        }

        // Token: 0x060019A9 RID: 6569 RVA: 0x000B5F58 File Offset: 0x000B4158
        public static void Address(object address)
        {
            DoCheck(address, "address");
        }

        // Token: 0x060019AA RID: 6570 RVA: 0x000B5F68 File Offset: 0x000B4168
        public static void AddressRange(object addressRange)
        {
            DoCheck(addressRange, "addressRange");
        }

        // Token: 0x060019AB RID: 6571 RVA: 0x000B5F78 File Offset: 0x000B4178
        public static void AddressRanges(object addressRanges)
        {
            DoCheck(addressRanges, "addressRanges");
        }

        // Token: 0x060019AC RID: 6572 RVA: 0x000B5F88 File Offset: 0x000B4188
        public static void Announces(object announces)
        {
            DoCheck(announces, "announces");
        }

        // Token: 0x060019AD RID: 6573 RVA: 0x000B5F98 File Offset: 0x000B4198
        public static void BaseDirectory(object baseDirectory)
        {
            DoCheck(baseDirectory, "baseDirectory");
        }

        // Token: 0x060019AE RID: 6574 RVA: 0x000B5FA8 File Offset: 0x000B41A8
        internal static void BaseType(Type baseType)
        {
            DoCheck(baseType, "baseType");
        }

        // Token: 0x060019AF RID: 6575 RVA: 0x000B5FB8 File Offset: 0x000B41B8
        internal static void Buffer(object buffer)
        {
            DoCheck(buffer, "buffer");
        }

        // Token: 0x060019B0 RID: 6576 RVA: 0x000B5FC8 File Offset: 0x000B41C8
        internal static void Cache(object cache)
        {
            DoCheck(cache, "cache");
        }

        // Token: 0x060019B1 RID: 6577 RVA: 0x000B5FD8 File Offset: 0x000B41D8
        public static void Data(object data)
        {
            DoCheck(data, "data");
        }

        // Token: 0x060019B2 RID: 6578 RVA: 0x000B5FE8 File Offset: 0x000B41E8
        public static void Destination(object destination)
        {
            DoCheck(destination, "destination");
        }

        // Token: 0x060019B3 RID: 6579 RVA: 0x000B5FF8 File Offset: 0x000B41F8
        public static void Endpoint(object endpoint)
        {
            DoCheck(endpoint, "endpoint");
        }

        // Token: 0x060019B4 RID: 6580 RVA: 0x000B6008 File Offset: 0x000B4208
        public static void File(object file)
        {
            DoCheck(file, "file");
        }

        // Token: 0x060019B5 RID: 6581 RVA: 0x000B6018 File Offset: 0x000B4218
        public static void Files(object files)
        {
            DoCheck(files, "files");
        }

        // Token: 0x060019B6 RID: 6582 RVA: 0x000B6028 File Offset: 0x000B4228
        public static void FileSource(object fileSource)
        {
            DoCheck(fileSource, "fileSource");
        }

        // Token: 0x060019B7 RID: 6583 RVA: 0x000B6038 File Offset: 0x000B4238
        public static void InfoHash(object infoHash)
        {
            DoCheck(infoHash, "infoHash");
        }

        // Token: 0x060019B8 RID: 6584 RVA: 0x000B6048 File Offset: 0x000B4248
        public static void Key(object key)
        {
            DoCheck(key, "key");
        }

        // Token: 0x060019B9 RID: 6585 RVA: 0x000B6058 File Offset: 0x000B4258
        public static void Limiter(object limiter)
        {
            DoCheck(limiter, "limiter");
        }

        // Token: 0x060019BA RID: 6586 RVA: 0x000B6068 File Offset: 0x000B4268
        public static void Listener(object listener)
        {
            DoCheck(listener, "listener");
        }

        // Token: 0x060019BB RID: 6587 RVA: 0x000B6078 File Offset: 0x000B4278
        public static void Location(object location)
        {
            DoCheck(location, "location");
        }

        // Token: 0x060019BC RID: 6588 RVA: 0x000B6088 File Offset: 0x000B4288
        public static void MagnetLink(object magnetLink)
        {
            DoCheck(magnetLink, "magnetLink");
        }

        // Token: 0x060019BD RID: 6589 RVA: 0x000B6098 File Offset: 0x000B4298
        public static void Manager(object manager)
        {
            DoCheck(manager, "manager");
        }

        // Token: 0x060019BE RID: 6590 RVA: 0x000B60A8 File Offset: 0x000B42A8
        public static void Mappings(object mappings)
        {
            DoCheck(mappings, "mappings");
        }

        // Token: 0x060019BF RID: 6591 RVA: 0x000B60B8 File Offset: 0x000B42B8
        public static void Metadata(object metadata)
        {
            DoCheck(metadata, "metadata");
        }

        // Token: 0x060019C0 RID: 6592 RVA: 0x000B60C8 File Offset: 0x000B42C8
        public static void Name(object name)
        {
            DoCheck(name, "name");
        }

        // Token: 0x060019C1 RID: 6593 RVA: 0x000B60D8 File Offset: 0x000B42D8
        public static void Path(object path)
        {
            DoCheck(path, "path");
        }

        // Token: 0x060019C2 RID: 6594 RVA: 0x000B60E8 File Offset: 0x000B42E8
        public static void Paths(object paths)
        {
            DoCheck(paths, "paths");
        }

        // Token: 0x060019C3 RID: 6595 RVA: 0x000B60F8 File Offset: 0x000B42F8
        public static void PathNotEmpty(string path)
        {
            IsNullOrEmpty(path, "path");
        }

        // Token: 0x060019C4 RID: 6596 RVA: 0x000B6108 File Offset: 0x000B4308
        public static void Peer(object peer)
        {
            DoCheck(peer, "peer");
        }

        // Token: 0x060019C5 RID: 6597 RVA: 0x000B6118 File Offset: 0x000B4318
        public static void Peers(object peers)
        {
            DoCheck(peers, "peers");
        }

        // Token: 0x060019C6 RID: 6598 RVA: 0x000B6128 File Offset: 0x000B4328
        public static void Picker(object picker)
        {
            DoCheck(picker, "picker");
        }

        // Token: 0x060019C7 RID: 6599 RVA: 0x000B6138 File Offset: 0x000B4338
        public static void Result(object result)
        {
            DoCheck(result, "result");
        }

        // Token: 0x060019C8 RID: 6600 RVA: 0x000B6148 File Offset: 0x000B4348
        public static void SavePath(object savePath)
        {
            DoCheck(savePath, "savePath");
        }

        // Token: 0x060019C9 RID: 6601 RVA: 0x000B6158 File Offset: 0x000B4358
        public static void Settings(object settings)
        {
            DoCheck(settings, "settings");
        }

        // Token: 0x060019CA RID: 6602 RVA: 0x000B6168 File Offset: 0x000B4368
        internal static void SpecificType(Type specificType)
        {
            DoCheck(specificType, "specificType");
        }

        // Token: 0x060019CB RID: 6603 RVA: 0x000B6178 File Offset: 0x000B4378
        public static void Stream(object stream)
        {
            DoCheck(stream, "stream");
        }

        // Token: 0x060019CC RID: 6604 RVA: 0x000B6188 File Offset: 0x000B4388
        public static void Torrent(object torrent)
        {
            DoCheck(torrent, "torrent");
        }

        // Token: 0x060019CD RID: 6605 RVA: 0x000B6198 File Offset: 0x000B4398
        public static void TorrentInformation(object torrentInformation)
        {
            DoCheck(torrentInformation, "torrentInformation");
        }

        // Token: 0x060019CE RID: 6606 RVA: 0x000B61A8 File Offset: 0x000B43A8
        public static void TorrentSave(object torrentSave)
        {
            DoCheck(torrentSave, "torrentSave");
        }

        // Token: 0x060019CF RID: 6607 RVA: 0x000B61B8 File Offset: 0x000B43B8
        public static void Tracker(object tracker)
        {
            DoCheck(tracker, "tracker");
        }

        // Token: 0x060019D0 RID: 6608 RVA: 0x000B61C8 File Offset: 0x000B43C8
        public static void Url(object url)
        {
            DoCheck(url, "url");
        }

        // Token: 0x060019D1 RID: 6609 RVA: 0x000B61D8 File Offset: 0x000B43D8
        public static void Uri(Uri uri)
        {
            DoCheck(uri, "uri");
        }

        // Token: 0x060019D2 RID: 6610 RVA: 0x000B61E8 File Offset: 0x000B43E8
        public static void Value(object value)
        {
            DoCheck(value, "value");
        }

        // Token: 0x060019D3 RID: 6611 RVA: 0x000B61F8 File Offset: 0x000B43F8
        public static void Writer(object writer)
        {
            DoCheck(writer, "writer");
        }
    }
}