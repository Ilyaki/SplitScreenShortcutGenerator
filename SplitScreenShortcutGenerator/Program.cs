using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

//the exe must be in StardewValley\Mods\SplitScreen



namespace SplitScreenShortcutGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			start:
			Console.WriteLine("How many keyboard shortcuts would you like? (Type a number then press Enter)");
			if (!int.TryParse(Console.ReadLine(), out int numKeyboards) || numKeyboards < 0)
			{
				Console.WriteLine("Unknown number, try again");
				goto start;
			}

			Console.WriteLine("Creating shortcuts...");

			string ssModFolder = Directory.GetCurrentDirectory();
			string sdvFolder = Path.GetFullPath(Path.Combine(ssModFolder, @"..\.."));


			CreateShortcut(sdvFolder, "Gamepad 1", "gamepad1", 1);
			CreateShortcut(sdvFolder, "Gamepad 2", "gamepad2", 2);
			CreateShortcut(sdvFolder, "Gamepad 3", "gamepad3", 3);
			CreateShortcut(sdvFolder, "Gamepad 4", "gamepad4", 4);

			for (int i = 1; i <= Math.Max(numKeyboards, 0); i++)
				CreateShortcut(sdvFolder, $"Keyboard {i}", $"keyboard{i}", 0);

			Console.WriteLine("Created all shortcuts. Press Enter to exit.");
			Console.ReadLine();
		}

		private static void CreateShortcut(string sdvFolder, string name, string logName, int playerIndex)
		{
			WshShell wsh = new WshShell();

			IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
				Path.Combine(sdvFolder, $"{name}.lnk")) as IWshRuntimeLibrary.IWshShortcut;

			shortcut.Arguments = $" --log-path \"{logName}.txt\" --player-index {playerIndex}";
			shortcut.TargetPath = Path.Combine(sdvFolder, "StardewModdingAPI.exe");
			shortcut.WindowStyle = 1;
			shortcut.Description = name;
			shortcut.WorkingDirectory = sdvFolder;
			//shortcut.IconLocation = "specify icon location";
			shortcut.Save();
		}
	}
}
