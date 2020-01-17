using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
	class Game
	{
		// The minesweeper game board. Each cell is either -1 to represent a mine
		//	or the number of mines adjacent to the cell.
		private int[][] gameBoard;
		// Keeps track of whether tiles have been revealed. True for revealedtiles.
		//	False otherwise.
		private bool[][] revealed;

		// The number of tiles that do not contain mines and have not been
		//	uncovered. When this reaches 0, the game is won.
		private int hiddenSafeTiles;

		// The x dimension of the board.
		public int X { get; private set; }

		// The y dimension of the board.
		public int Y { get; private set; }

		// The total number of mines on the board.
		public int Mines { get; private set; }

		// True if game is in progress. False otherwise.
		public bool InProgress { get; private set; }

		// True if the player has won. False otherwise.
		//	get should throw an exception if called when inProgress == true because
		//	this property is not meaningful in that case.
		public bool Win { get; private set; }

		/// <summary>
		/// This function will reset all of the game logic and start a new game
		/// according to the given parameters.
		/// </summary>
		/// <param name="mines">number of mines</param>
		/// <param name="x">horizontal size of board</param>
		/// <param name="y">vertical size of board</param>
		/// <remarks>Mines > x * y is invalid.</remarks>
		public void NewGame(int mines, int x, int y)
		{
			// Set the game state variables and UI helper variables.
			hiddenSafeTiles = x * y - mines;
			InProgress = true;
			Win = false;
			Mines = mines;
			X = x;
			Y = y;

			// Set up the game board.
			SetUpGameBoard(mines, x, y);
		}

		/// <summary>
		/// Create a 2D array representing the state of the game, where each cell
		/// in the array represents how many mines are adjacent to that cell or
		/// that the cell is a mine. If the cell is a mine, that cell will contain
		/// -1.
		/// </summary>
		/// <param name="mines">number of mines</param>
		/// <param name="x">horizontal size of board</param>
		/// <param name="y">vertical size of board</param>
		/// <remarks>Mines > x * y is invalid.</remarks>
		private void SetUpGameBoard(int mines, int x, int y)
		{
			if (x < 1)
				throw new ArgumentException("The x dimension of a gameboard must" +
					" be greater than 0.", "x");
			if (y < 1)
				throw new ArgumentException("The y dimension of a gameboard must" +
					" be greater than 0.", "y");
			if (mines > x * y)
				throw new ArgumentException(String.Format("It is not possible to " +
					"have {0} * {1} = {2} tiles and {3} mines.", x, y, x * y, mines));
			
			Random rand = new Random();
			int randX, randY;

			// Initialize the array of arrays.
			gameBoard = new int[x][];
			revealed = new bool[x][];
			for (int i = 0; i < x; i++)
			{
				gameBoard[i] = new int[y];
				revealed[i] = new bool[y];
			}
			
			// Randomly place all mines.
			randX = rand.Next(x);
			randY = rand.Next(y);
			for (int i = 0; i < mines; i++)
			{
				// Generate pairs until a tile without a mine is selected.
				while (gameBoard[randX][randY] == -1)
				{
					randX = rand.Next(x);
					randY = rand.Next(y);
				}
				// Place a mine.
				gameBoard[randX][randY] = -1;
			}


			// Iterate through the array and use the presence of the mines to
			//	set the values of the unmined cells.
			for (int i = 0; i < X; i++)
			{
				for (int j = 0; j < Y; j++)
				{
					if (gameBoard[i][j] != -1)
						gameBoard[i][j] = CountAdjacentMines(i, j);
				}
			}
		}

		/// <summary>
		/// Count all of the mines adjacent to the tile (x,y).
		/// </summary>
		/// <param name="x">x-coordinate</param>
		/// <param name="y">y-coordinate</param>
		/// <returns>The number of adjacent mines.</returns>
		private int CountAdjacentMines(int x, int y)
		{
			int adjacentMines = 0;

			// Iterate through the cells adjacent to (x,y).
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					// Ignore (i,j) pairs outside of the array bounds. Ignore the
					// current cell. Don't count cells without mines.
					if (i >= 0 && j >= 0 && i < X && j < Y && (i != x || j != y) && HasMine(i, j))
						adjacentMines++;
				}
			}
			
			return adjacentMines;
		}

		/// <summary>
		/// End the game in a win or a loss. Set the <c>InProgress</c> property to
		/// <c>false</c> and set the <c>Win</c> property as appropriate.
		/// Run any other necessary end of game logic.
		/// </summary>
		/// <param name="win">True if the player has won, false if the player
		/// has lost.</param>
		private void GameOver(bool win)
		{
			Win = win;
			InProgress = false;
		}

		/// <summary>
		/// Reveals a tile. Triggers the end of the game if a mine is hit or all
		/// safe tiles are uncovered.
		/// </summary>
		/// <param name="x">x-coordinate</param>
		/// <param name="y">y-coordinate</param>
		/// <returns>Return -1 if the tile is a mine. Otherwise, return the number
		///of adjacent mines.</returns>
		public int RevealTile(int x, int y)
		{
			int result;
			
			// Throw an exception if the game is not in progress.
			if (!InProgress)
				throw new InvalidOperationException("RevealTile() should not be" +
					" called while the game is not in progress (for example," +
					" after a game over or before the first new game is started).");

			// Get the number of adjacent mines (or whether this is a mine).
			// Throw an exception if the tile was already revealed.
			if (GetCellInfo(x, y, out result))
				throw new ArgumentException(String.Format("The tile at ({0},{1})" +
					" has already been revealed.", x, y));
			
			// Mark the tile as revealed.
			revealed[x][y] = true;

			if (HasMine(x, y))
				GameOver(false);
			else
			{
				hiddenSafeTiles--;
				if (hiddenSafeTiles == 0)
					GameOver(true);
			}

			return result;
		}
		
		/// <summary>
		/// Determine whether a given tile contains a mine.
		/// </summary>
		/// <param name="x">x-coordinate</param>
		/// <param name="y">y-coordinate</param>
		/// <returns><c>true</c> if the tile has a mine, <c>false</c> otherwise</returns>
		public bool HasMine(int x, int y)
		{
			return gameBoard[x][y] == -1;
		}

		/// <summary>
		/// Get the following informmation about the tile at (x,y): Whether the
		///	tile is hidden, whether a mine is present, and how many mines are
		///	adjacent to the tile.
		/// </summary>
		/// <param name="x">x-coordinate</param>
		/// <param name="y">y-coordinate</param>
		/// <param name="adjacentMines">The number of adjacent mines if the tile
		/// is safe or -1 if (x,y) is a mine.</param>
		/// <returns><c>true</c> if the tile is revealed, <c>false</c> otherwise.</returns>
		public bool GetCellInfo(int x, int y, out int adjacentMines)
		{
			adjacentMines = gameBoard[x][y];
			return revealed[x][y];
		}
	}
}
