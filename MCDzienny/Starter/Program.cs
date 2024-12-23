using System;
using System.IO;
using MCDzienny.Gui;

namespace Starter
{
	internal class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				openServer(args);
			}
			else if (File.Exists("MCDzienny_.dll"))
			{
				openServer(args);
			}
			else
			{
				Console.WriteLine("Can't find MCDzienny_.dll file.");
			}
			Console.WriteLine("Bye!");
		}

		private static void openServer(string[] args)
		{
			Window.showWarning = false;
			MCDzienny_.Gui.Program.Main(args);
		}
	}
}
