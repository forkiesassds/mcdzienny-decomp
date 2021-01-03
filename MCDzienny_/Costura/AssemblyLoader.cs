using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Costura
{
    // Token: 0x020003AC RID: 940
    [CompilerGenerated]
    internal static class AssemblyLoader
    {
        // Token: 0x04000ED1 RID: 3793
        private static readonly Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

        // Token: 0x04000ED2 RID: 3794
        private static readonly Dictionary<string, string> symbolNames = new Dictionary<string, string>();

        // Token: 0x06001A8F RID: 6799 RVA: 0x000BA974 File Offset: 0x000B8B74
        // Note: this type is marked as 'beforefieldinit'.
        static AssemblyLoader()
        {
            assemblyNames.Add("log4net", "costura.log4net.dll.zip");
            assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.zip");
            assemblyNames.Add("supersocket.common", "costura.supersocket.common.dll.zip");
            assemblyNames.Add("supersocket.facility", "costura.supersocket.facility.dll.zip");
            assemblyNames.Add("supersocket.socketbase", "costura.supersocket.socketbase.dll.zip");
            assemblyNames.Add("supersocket.socketengine", "costura.supersocket.socketengine.dll.zip");
            assemblyNames.Add("superwebsocket", "costura.superwebsocket.dll.zip");
            assemblyNames.Add("system.threading", "costura.system.threading.dll.zip");
        }

        // Token: 0x06001A88 RID: 6792 RVA: 0x000BA714 File Offset: 0x000B8914
        private static Assembly ReadExistingAssembly(AssemblyName name)
        {
            var currentDomain = AppDomain.CurrentDomain;
            var assemblies = currentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var name2 = assembly.GetName();
                if (name2.Name == name.Name && Equals(name2.CultureInfo, name.CultureInfo)) return assembly;
            }

            return null;
        }

        // Token: 0x06001A89 RID: 6793 RVA: 0x000BA780 File Offset: 0x000B8980
        private static void CopyTo(Stream source, Stream destination)
        {
            var array = new byte[81920];
            int count;
            while ((count = source.Read(array, 0, array.Length)) != 0) destination.Write(array, 0, count);
        }

        // Token: 0x06001A8A RID: 6794 RVA: 0x000BA7B4 File Offset: 0x000B89B4
        private static Stream LoadStream(string fullname)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (fullname.EndsWith(".zip"))
                using (var manifestResourceStream = executingAssembly.GetManifestResourceStream(fullname))
                {
                    using (var deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
                    {
                        var memoryStream = new MemoryStream();
                        CopyTo(deflateStream, memoryStream);
                        memoryStream.Position = 0L;
                        return memoryStream;
                    }
                }

            return executingAssembly.GetManifestResourceStream(fullname);
        }

        // Token: 0x06001A8B RID: 6795 RVA: 0x000BA838 File Offset: 0x000B8A38
        private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
        {
            string fullname;
            if (resourceNames.TryGetValue(name, out fullname)) return LoadStream(fullname);
            return null;
        }

        // Token: 0x06001A8C RID: 6796 RVA: 0x000BA858 File Offset: 0x000B8A58
        private static byte[] ReadStream(Stream stream)
        {
            var array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }

        // Token: 0x06001A8D RID: 6797 RVA: 0x000BA880 File Offset: 0x000B8A80
        private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames,
            Dictionary<string, string> symbolNames, string name)
        {
            byte[] rawAssembly;
            using (var stream = LoadStream(assemblyNames, name))
            {
                if (stream == null) return null;
                rawAssembly = ReadStream(stream);
            }

            using (var stream2 = LoadStream(symbolNames, name))
            {
                if (stream2 != null)
                {
                    var rawSymbolStore = ReadStream(stream2);
                    return Assembly.Load(rawAssembly, rawSymbolStore);
                }
            }

            return Assembly.Load(rawAssembly);
        }

        // Token: 0x06001A8E RID: 6798 RVA: 0x000BA900 File Offset: 0x000B8B00
        public static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var assembly = ReadExistingAssembly(assemblyName);
            if (assembly != null) return assembly;
            var text = assemblyName.Name.ToLowerInvariant();
            if (assemblyName.CultureInfo != null && !string.IsNullOrEmpty(assemblyName.CultureInfo.Name))
                text = string.Format("{0}.{1}", assemblyName.CultureInfo.Name, text);
            return ReadFromEmbeddedResources(assemblyNames, symbolNames, text);
        }

        // Token: 0x06001A90 RID: 6800 RVA: 0x000BAA38 File Offset: 0x000B8C38
        public static void Attach()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += ResolveAssembly;
        }
    }
}