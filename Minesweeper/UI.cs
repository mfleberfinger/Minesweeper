using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Minesweeper
{
	class UI
	{
		// Reference to a Game object. Provided by the code that instantiates this.
		private Game game;
		// Keeps track of tiles the user has flagged.
		private bool[][] flags;

		// Constructor
		public UI(Game game)
		{
			this.game = game;
		}

		/// <summary>
		/// Parse user input and run valid commands.
		/// </summary>
		/// <param name="userInput">User input.</param>
		public void InterpretCommand(string userInput)
		{
			string input = userInput.Trim().ToLower();
			string commandName = Regex.Match(input, "^[^\\s]*").Value;

			switch (commandName)
			{
				case "help":
					ShowHelp(input);
					break;
				case "new":
					NewGame(input);
					break;
				case "reveal":
					RevealTile(input);
					break;
				case "flag":
					FlagMine(input);
					break;
				case "unflag":
					UnflagMine(input);
					break;
				default:
					Console.WriteLine("Command not recognized.");
					break;
			}
		}

		/// <summary>
		/// Determine whether a zero-indexed (x,y) is a tile on the gameboard.
		/// </summary>
		/// <param name="x">zero-indexed x-coordinate</param>
		/// <param name="y">zero-indexed y-coordinate</param>
		/// <returns></returns>
		private bool IsTile(int x, int y)
		{
			return x >= 0 && y >= 0 && x < game.X && y < game.Y;
		}

		private void DisplayArgumentError()
		{
			Console.WriteLine("Invalid argument(s).");
		}


		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void FlagMine(string input)
		{
			// Get the arguments. The calling function has already verified that
			//	this is the flag command.

		}
		/// <summary>
		/// Flag (x, y) as a mine.
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		/// <remarks>If the user enters an invalid tile, just ignore it.</remarks>
		private void FlagMine(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;

			if (IsTile(realX, realY))
				flags[realX][realY] = true;
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void UnflagMine(string input)
		{

		}
		/// <summary>
		/// Unflag (x, y).
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		/// <remarks>If the user enters an invalid tile, just ignore it.</remarks>
		private void UnflagMine(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void RevealTile(string input)
		{

		}
		/// <summary>
		/// Flag (x, y) as a mine, if it is a tile.
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		/// <remarks>If the user enters an invalid or flagged tile, just ignore it.</remarks>
		private void RevealTile(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void NewGame(string input)
		{
			string strMines = "";
			string strX = "";
			string strY = "";
			int mines = 0;
			int x = 0;
			int y = 0;
			GroupCollection arguments = null;
			bool parsed = false;

			// Capture the three arguments, assuming the command is formatted
			// like "new 5 10 20".
			arguments = Regex.Match(input, "^\\w+ (\\d+) (\\d+) (\\d+)$").Groups;

			// Apparently, C# defines the first group as the result of the
			// entire regular expression...
			parsed = arguments.Count == 4;
			if (parsed)
			{
				parsed = parsed && int.TryParse(arguments[1].Value, out mines);
				parsed = parsed && int.TryParse(arguments[2].Value, out x);
				parsed = parsed && int.TryParse(arguments[3].Value, out y);
			}

			if (parsed && mines > -1 && x > 0 && y > 0)
			{
				NewGame(mines, x, y);
				DrawBoard(false);
			}
			else
				DisplayArgumentError();
		}
		/// <summary>
		/// Start a new game.
		/// </summary>
		/// <param name="mines">number of mines to put on the board</param>
		/// <param name="x">horizontal size in tiles</param>
		/// <param name="y">vertical size in tiles</param>
		private void NewGame(int mines, int x, int y)
		{
			flags = new bool[x][];
			for (int i = 0; i < x; i++)
				flags[i] = new bool[y];
			
			game.NewGame(mines, x, y);
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void ShowHelp(string input)
		{
			ShowHelp();
		}
		/// <summary>
		/// Display the commands and their explanations.
		/// </summary>
		private void ShowHelp()
		{
			Console.WriteLine("Commands:");
			Console.WriteLine("\thelp");
			Console.WriteLine("\tnew (number of mines) (number of rows)" +
				" (number of columns)");
			Console.WriteLine("\treveal (column) (row)");
			Console.WriteLine("\tflag (column) (row)");
			Console.WriteLine("\tunflag (column) (row)");
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
		private void DrawBoard(bool lose)
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
