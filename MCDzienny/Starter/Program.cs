using System;
using System.IO;
using MCDzienny.Gui;

namespace Starter
{
    // Token: 0x02000005 RID: 5
    internal class Program
    {
        // Token: 0x0600000B RID: 11 RVA: 0x00002117 File Offset: 0x00000317
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0)
                openServer(args);
            else if (File.Exists("MCDzienny_.dll"))
                openServer(args);
            else
                Console.WriteLine("Can't find MCDzienny_.dll file.");
            Console.WriteLine("Bye!");
        }

        // Token: 0x0600000C RID: 12 RVA: 0x0000214F File Offset: 0x0000034F
        private static void openServer(string[] args)
        {
            Window.showWarning = false;
            Main(args);
        }
    }
}