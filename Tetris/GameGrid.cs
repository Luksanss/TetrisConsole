using System.Runtime.InteropServices;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get;  }
        public int Columns { get;  }

        // allow custom function of assignment 
        public int this[int r, int c] 
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            // creates a grid (filled with 0)
            grid = new int[Rows, Columns];
        }

        public bool IsInside(int r, int c)
        {
            // checks if row and column is inside the grid
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        public bool IsEmpty(int r, int c)
        {
            // check if given cell on row r and column c IsEmpty 
            return IsInside(r, c) && grid[r, c] == 0;
        }

        public bool RowIsFull(int r)
        {
            // check if row is full
            for (int i = 0; i < Columns; i++)
            {
                if (grid[r, i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool RowIsEmpty(int r)
        {
            // check if row is empty
            for (int i = 0; i < Columns; i++)
            {
                if (grid[r, i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearRow(int r)
        {
            // clear entire row -> set all elements to 0
            for (int i = 0; i < Columns; i++)
            {
                grid[r, i] = 0;
            } 
        }

        private void MoveRowDown(int r, int NumberOfRows)
        {
            // when row is cleared all rows above this are moved down 
            for (int i = 0; i < Columns; i++)
            {
                // move the row down
                grid[r + NumberOfRows, i] = grid[r, i];
                // leave nothing on place if move row
                grid[r, i] = 0;
            }
        }

        public int ClearRows() 
        {
            // clear rows that should be cleaned

            // keep track of how many rows were cleaned
            int cleared = 0;
            // start checking from the bottom if rows ale full
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (RowIsFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }

        public void PrintGrid()
        {
            int height = 60;
            int width = 160;
            // if operating system is Windows
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize(width, height);
            }
            else
            {
                // haha
            }
            int x = (Console.WindowWidth) / 2 - Rows;
            int y = (Console.WindowHeight) / 2 - Columns;

            for (int r = 0; r < Rows; r++)
            {
                Console.SetCursorPosition(x, y);
                for (int c = 0; c < Columns; c++)
                {
                    Console.Write($" {grid[r, c]} ");
                }
                y += 1;
            }
        }
    }
}