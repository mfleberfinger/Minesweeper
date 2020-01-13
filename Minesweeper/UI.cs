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
		///     ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
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
		public void DrawBoard(bool lose) //TODO: This should be private.
		{
			// Number of mines adjacent to the current cell.
			int adjacentMines;
			// What to show the user at the current cell.
			string cellOutput = " ";
			// Whether the current cell is revealed.
			bool revealed;
			// First, output the column numbers.
			Console.Write("     ");
			for (int i = 1; i <= game.X; i++)
			{
				// Align the number with the top of the cell.
				WriteRowColNum(i);
			}
			Console.WriteLine();

			// Build the board left to right, top to bottom.
			// Top left corner
			Console.Write("     ┌");
			// Top Edge
			for (int i = 1; i < game.X; i++)
				Console.Write("───┬");
			// Top Right Corner
			Console.Write("───┐");

			for (int j = 1; j <= game.Y; j++)
			{
				Console.WriteLine();

				// Spaces and row number
				WriteRowColNum(j);
				Console.Write(" ");

				// Spaces, numbers and vertical lines
				for (int i = 1; i <= game.X; i++)
				{
					revealed = game.GetCellInfo(i - 1, j - 1, out adjacentMines);
					if (lose || (!lose && revealed))
					{
						if (adjacentMines == -1)
							cellOutput = "*";
						else
							cellOutput = adjacentMines.ToString();
					}
					else
						cellOutput = " ";
					
					Console.Write("|");
					SetConsoleColor(cellOutput);
					Console.Write(" ");
					Console.Write(cellOutput);
					Console.Write(" ");
					Console.ResetColor();
				}
				Console.WriteLine("|");
				
				if (j < game.Y)
				{
					// Left Edge
					Console.Write("     ├");
					// Horizontal Lines
					for (int i = 1; i < game.X; i++)
					{
						Console.Write("───┼");
					}
					// Right Edge
					Console.Write("───┤");
				}
			}
			
			// Bottom left corner
			Console.Write("     └");
			// Bottom edge
			for (int i = 1; i < game.X; i++)
				Console.Write("───┴");
			// Bottom right corner
			Console.Write("───┘");
		}

		private void SetConsoleColor(string cellOutput)
		{
			switch (cellOutput)
			{
				case "1":
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case "2":
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					break;
				case "3":
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case "4":
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case "5":
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
				case "6":
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case "7":
					Console.ForegroundColor = ConsoleColor.Magenta;
					break;
				case "8":
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				// Mine
				case "*":
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.DarkRed;
					break;
				// Flag
				case "#":
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.White;
					break;
			}

		}

		/// <summary>
		/// Output the appropriate number of spaces to replace digits of a row
		/// or column number in the output. Write the number after the appropriate
		/// number of spaces.
		/// </summary>
		/// <param name="number">The number to pad and write.</param>
		private void WriteRowColNum(int number)
		{
			if (number < 10)
				Console.Write("   ");
			else if (number < 100)
				Console.Write("  ");
			else
				Console.Write(" ");
			Console.Write(number);
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
