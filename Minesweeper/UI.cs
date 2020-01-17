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
					if (CheckRunning())
						RevealTile(input);
					break;
				case "flag":
					if (CheckRunning())
						FlagMine(input);
					break;
				case "unflag":
					if (CheckRunning())
						UnflagMine(input);
					break;
				default:
					Console.WriteLine("Command not recognized.");
					break;
			}
		}

		/// <summary>
		/// Check whether the game is still running and output an error message
		/// if not.
		/// </summary>
		/// <returns><c>true</c> if the game is still running, <c>false</c> otherwise</returns>
		private bool CheckRunning()
		{
			if (!game.InProgress)
				Console.WriteLine("Please start a new game to begin playing.");
			return game.InProgress;
		}

		/// <summary>
		/// Determine whether a one-indexed (x,y) is a tile on the gameboard.
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		/// <returns></returns>
		private bool IsTile(int x, int y)
		{
			return x >= 1 && y >= 1 && x <= game.X && y <= game.Y;
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
			// One-indexed x and y.
			int x = 0;
			int y = 0;
			GroupCollection arguments = null;
			bool parsed = false;

			// Capture the two arguments, assuming the command is formatted
			// like "flag 10 20".
			arguments = Regex.Match(input, "^\\w+ (\\d+) (\\d+)$").Groups;

			// Apparently, C# defines the first group as the result of the
			// entire regular expression...
			parsed = arguments.Count == 3;
			if (parsed)
			{
				parsed = parsed && int.TryParse(arguments[1].Value, out x);
				parsed = parsed && int.TryParse(arguments[2].Value, out y);
			}

			if (parsed && IsTile(x, y))
			{
				FlagMine(x, y);
				DrawBoard(false);
			}
			else
				DisplayArgumentError();
		}

		/// <summary>
		/// Flag (x, y) as a mine.
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		private void FlagMine(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;

			flags[realX][realY] = true;
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void UnflagMine(string input)
		{
			// One-indexed x and y.
			int x = 0;
			int y = 0;
			GroupCollection arguments = null;
			bool parsed = false;

			// Capture the two arguments, assuming the command is formatted
			// like "flag 10 20".
			arguments = Regex.Match(input, "^\\w+ (\\d+) (\\d+)$").Groups;

			// Apparently, C# defines the first group as the result of the
			// entire regular expression...
			parsed = arguments.Count == 3;
			if (parsed)
			{
				parsed = parsed && int.TryParse(arguments[1].Value, out x);
				parsed = parsed && int.TryParse(arguments[2].Value, out y);
			}

			if (parsed && IsTile(x, y))
			{
				UnflagMine(x, y);
				DrawBoard(false);
			}
			else
				DisplayArgumentError();
		}
		/// <summary>
		/// Unflag (x, y).
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		private void UnflagMine(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;

			flags[realX][realY] = false;
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void RevealTile(string input)
		{
			// One-indexed x and y.
			int x = 0;
			int y = 0;
			GroupCollection arguments = null;
			bool parsed = false;
			bool lose = false;

			// Capture the two arguments, assuming the command is formatted
			// like "flag 10 20".
			arguments = Regex.Match(input, "^\\w+ (\\d+) (\\d+)$").Groups;

			// Apparently, C# defines the first group as the result of the
			// entire regular expression...
			parsed = arguments.Count == 3;
			if (parsed)
			{
				parsed = parsed && int.TryParse(arguments[1].Value, out x);
				parsed = parsed && int.TryParse(arguments[2].Value, out y);
			}

			if (parsed && IsTile(x, y))
			{
				RevealTile(x, y);
				
				// Check for lose or win condition.
				lose = !(game.InProgress || game.Win);

				if (!game.InProgress && game.Win)
					Console.WriteLine("Victory!");
				else if (lose)
					Console.WriteLine("You lose!");

				DrawBoard(lose);
			}
			else
				DisplayArgumentError();
		}
		/// <summary>
		/// Flag (x, y) as a mine, if it is a tile.
		/// </summary>
		/// <param name="x">one-indexed x-coordinate</param>
		/// <param name="y">one-indexed y-coordinate</param>
		private void RevealTile(int x, int y)
		{
			// Convert to zero-indexing.
			int realX = x - 1;
			int realY = y - 1;
			int adjacentMines = 0;

			// If the tile was already revealed, don't attempt to reveal it
			//	again.
			if(!game.GetCellInfo(realX, realY, out adjacentMines))
				game.RevealTile(realX, realY);
		}

		/// <summary>
		/// Parse input and call the command's implementation.
		/// </summary>
		/// <param name="input">User input for this command.</param>
		private void NewGame(string input)
		{
			int mines = 0;
			// One-indexed x and y.
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
					else if (flags[i - 1][j - 1])
						cellOutput = "F";
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
				case "F":
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
