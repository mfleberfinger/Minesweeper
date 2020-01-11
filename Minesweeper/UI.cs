using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
	class UI
	{
		// Reference to a Game object. Provided by the code that instantiates this.
		private Game game;

		// Constructor
		public UI(Game game)
		{
			this.game = game;
		}

		/// <summary>
		/// Output the gameboard to the console as ascii art.
		/// </summary>
		/// <param name="lose">If <c>true</c>, output all tiles regardless of
		/// whether they were revealed. Otherwise, only output information for
		/// revealed tiles.</param>
		/// <remarks>
		/// An example of the ascii art gameboard is below:
		/// 
		///        1   2   3   4   5   6   7   8   9  10
		///		┌───┬───┬───┬───┬───┬───┬───┬───┬───┬───┐
		///	  1 │   │   │   │   │   │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  2 │   │   │   │   │   │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  3 │   │   │   │   │   │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  4 │   │   │   │   │   │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  5 │   │   │ 1 │ 1 │ 1 │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  6 │   │   │ 1 │ X │ 2 │ 1 │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  7 │   │   │ 1 │ 2 │ X │ 1 │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  8 │   │   │   │ 1 │ 1 │ 1 │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	  9 │   │   │   │   │   │   │   │   │   │   │
		///		├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
		///	 10 │   │   │   │   │   │   │   │   │   │   │
		///		└───┴───┴───┴───┴───┴───┴───┴───┴───┴───┘
		/// </remarks>
		private void DrawBoard(bool lose)
		{
			// First, output the column numbers.
			Console.Write("     ");
			for (int i = 1; i <= game.X; i++)
			{
				// Align the number with the top of the cell.
				if (i < 9)
					Console.Write("   ");
				else if (i < 100)
					Console.Write("  ");
				else
					Console.Write(" ");
				
				Console.Write(i);
			}

			// Build the board left to right, top to bottom.

			
		}


		public void MockupGameBoards()
		{
			Console.WriteLine("Remaining Mines: 8");
			Console.WriteLine();
			Console.WriteLine("       1   2   3   4   5   6   7   8   9  10");
			Console.WriteLine("    ┌───┬───┬───┬───┬───┬───┬───┬───┬───┬───┐");
			Console.WriteLine("  1 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  2 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  3 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  4 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  5 │   │   │ 1 │ 1 │ 1 │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  6 │   │   │ 1 │ X │ 2 │ 1 │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  7 │   │   │ 1 │ 2 │ X │ 1 │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  8 │   │   │   │ 1 │ 1 │ 1 │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine("  9 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤");
			Console.WriteLine(" 10 │   │   │   │   │   │   │   │   │   │   │");
			Console.WriteLine("    └───┴───┴───┴───┴───┴───┴───┴───┴───┴───┘");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││ 1 ││ 1 ││ 1 ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││ 1 ││ X ││ 2 ││ 1 ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││ 1 ││ 2 ││ X ││ 1 ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││ 1 ││ 1 ││ 1 ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");
			Console.WriteLine("┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐");
			Console.WriteLine("│   ││   ││   ││   ││   ││   ││   ││   ││   ││   │");
			Console.WriteLine("└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘└───┘");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("_ _ _ 1 1 1 _ _ _ _");
			Console.WriteLine("_ _ _ 1 X 2 1 _ _ _");
			Console.WriteLine("_ _ _ 1 2 X 1 _ _ _");
			Console.WriteLine("_ _ _ _ 1 1 1 _ _ _");
			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("_ _ _ _ _ _ _ _ _ _");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");
			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");
			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");
			Console.WriteLine("|   | 1 | 1 | 1 |   |   |   |   |   |");
			Console.WriteLine("|   | 1 | X | 2 | 1 |   |   |   |   |");
			Console.WriteLine("|   | 1 | 2 | X | 1 |   |   |   |   |");
			Console.WriteLine("|   |   | 1 | 1 | 1 |   |   |   |   |");
			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");
			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");
			Console.WriteLine("|   |   |   |   |   |   |   |   |   |");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine(" _ _ _ _ _ _ _ _ _ _");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
			Console.WriteLine("|_|1|1|1|_|_|_|_|_|_|");
			Console.WriteLine("|_|1|X|2|1|_|_|_|_|_|");
			Console.WriteLine("|_|1|2|X|1|_|_|_|_|_|");
			Console.WriteLine("|_|_|1|1|1|_|_|_|_|_|");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
			Console.WriteLine("|_|_|_|_|_|_|_|_|_|_|");
		}
	}
}
