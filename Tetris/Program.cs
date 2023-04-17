using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            // game params
            Console.CursorVisible = false;
            var GameGrid = new GameGrid(20,12);

            // start menu
            MenuStart();

            while (true)
            {
                GameGrid.PrintGrid();
            }

            Console.ReadKey();
        }

        static void MenuStart()
        {
            string[] menuItems = { "Start Game", "Credits", "Exit" };

            // Set the initial selected item
            int selectedItem = 0;

            int width = Console.WindowWidth / 2;
            int height = Console.WindowHeight / 2;
            while (true)
            {
                // Display the menu
                Console.Clear();

                for (int i = 0; i < menuItems.Length; i++)
                {
                    // set cursor to middle of console
                    Console.SetCursorPosition(width, height);

                    // highlight the selected item
                    if (i == selectedItem)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(menuItems[i]);
                    height += 1;
                    Console.ResetColor();
                }
                // reset the adjustment
                height -= 3;

                // Handle user input
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedItem > 0)
                            selectedItem--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedItem < menuItems.Length - 1)
                            selectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedItem == menuItems.Length - 1)
                            Environment.Exit(0);
                        if (selectedItem == menuItems.Length - 2)
                            Credits();
                        if (selectedItem == menuItems.Length - 3)
                        {
                            Console.Clear();
                            return;
                        }
                        else
                            Console.WriteLine("You selected: " + menuItems[selectedItem]);
                        break;
                }
            }
        }

        static void Credits()
        {
            Console.Clear();

            int width = Console.WindowWidth / 2;
            int height = Console.WindowHeight / 2;
            // set cursor to middle of console
            Console.SetCursorPosition(width, height);

            Console.WriteLine("Credits:");

            for (int i = 0; i < Console.WindowHeight / 2; i++)
            {
                Console.WriteLine();
                height++;
            }

            for (int i = 0; i < Console.WindowHeight * 2; i++)
            {
                // set cursor to middle of console
                Console.SetCursorPosition(width, height);

                if (i == 0)
                    Console.WriteLine("Lukas");
                if (i == Console.WindowHeight)
                    Console.WriteLine("only me...");
                else
                    Console.WriteLine();
                height++;
                Thread.Sleep(200);
            }
            Environment.Exit(0);

        }
    }


    public class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get;  }
        public int Columns { get;  }

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

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int r, int c)
        {
            Row = r;
            Column = c;
        }
    }

    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; set; }
        protected abstract Position StartOffset { get; set;  }
        public abstract int Id { get; }

        // to choose from Tiles
        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
            rotationState = 0;
        }
    }
}