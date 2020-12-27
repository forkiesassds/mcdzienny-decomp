using MCDzienny;
using MCDzienny_.Gui;

namespace StarterCLI
{
    // Token: 0x02000002 RID: 2
    internal class ProgramCli
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private static void Main(string[] args)
        {
            Server.mono = true;
            Server.CLI = true;
            openServer(args);
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
        private static void openServer(string[] args)
        {
            Program.Main(args);
        }
    }
}