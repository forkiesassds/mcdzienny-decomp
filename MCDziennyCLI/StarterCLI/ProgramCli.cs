using MCDzienny;
using MCDzienny_.Gui;

namespace StarterCLI
{
	internal class ProgramCli
	{
		private static void Main(string[] args)
		{
			Server.mono = true;
			Server.CLI = true;
			openServer(args);
		}

		private static void openServer(string[] args)
		{
			Program.Main(args);
		}
	}
}
