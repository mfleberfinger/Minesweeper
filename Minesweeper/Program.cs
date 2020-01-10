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
			UI ui = new UI();

			game.NewGame(10, 10, 10);
			ui.MockupGameBoards();


			while (true)
			{
				// meh
			}
		}
	}
}
