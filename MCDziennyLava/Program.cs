using System;
using System.IO;
using MCDzienny.Gui;

namespace Starter
{
    // Token: 0x02000002 RID: 2
    internal class Program
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
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

        // Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
        private static void openServer(string[] args)
        {
            Window.showWarning = false;
            MCDzienny_.Gui.Program.Main(args);
        }
    }
}