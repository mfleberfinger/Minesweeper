using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game();
			UI ui = new UI(game);
			string input = "";

			Console.WriteLine("~~~ Minesweeper ~~~");
			Console.WriteLine();
			ui.InterpretCommand("help");
			while (true)
			{
				Console.WriteLine();
				Console.Write("> ");
				input = Console.ReadLine();
				Console.WriteLine();
				ui.InterpretCommand(input);
			}
		}
	}
}
