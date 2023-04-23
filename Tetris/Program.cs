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

            var game = new GameState();

            while (true)
            {
                Print(game);

                if (Console.KeyAvailable)
                {
                    ConsoleKey keyPressed = Console.ReadKey(true).Key;
                    if (keyPressed == ConsoleKey.LeftArrow)
                    {
                        game.MoveBlockLeft();
                    }
                    else if (keyPressed == ConsoleKey.RightArrow)
                    {
                        game.MoveBlockRight();
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        game.MoveBlockDown();
                    }
                    else if (keyPressed == ConsoleKey.R)
                    {
                        game.RotateBlockCW();
                    }
                    else if (keyPressed == ConsoleKey.T)
                    {
                        game.RotateBlockCCW();
                    }
                }
                game.MoveBlockDown();

                
            }

            Console.ReadKey();
        }


        public static void PrintGrid(GameGrid gameGrid)
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
            int x = (Console.WindowWidth) / 2 - gameGrid.Rows;
            int y = (Console.WindowHeight) / 2 - gameGrid.Columns;

            for (int r = 0; r < gameGrid.Rows; r++)
            {
                Console.SetCursorPosition(x, y);
                for (int c = 0; c < gameGrid.Columns; c++)
                {
                    Console.Write($" {gameGrid[r, c]} ");
                }
                y += 1;
            }
        }
        public static void PrintBlock(Block block, GameGrid gameGrid)
        {
            foreach (Position p in block.TilePositions())
            {
                gameGrid[p.Row, p.Column] = block.Id;
            }
            PrintGrid(gameGrid);
        }

        public static void Print(GameState gameState)
        {
            PrintBlock(gameState.CurBlock, gameState.GameGrid);
            PrintGrid(gameState.GameGrid);
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

            //Console.WriteLine("Resources:");

            //for (int i = 0; i < Console.WindowHeight / 4; i++)
            //{
            //    Console.WriteLine();
            //    height++;
            //}

            for (int i = 0; i < Console.WindowHeight * 2; i++)
            {
                // set cursor to middle of console
                Console.SetCursorPosition(width, height);

                if (i == 0)
                    Console.WriteLine("Made using: ");
                else if (i == 4)
                    Console.WriteLine("C# (dotnet 6.0)");
                else if (i == 5)
                    Console.WriteLine("IDE Visual Studio 2022");
                else if (i == 6)
                    Console.WriteLine("ChatGPT 3");
                else if (i == 10)
                    Console.WriteLine("Sources used: ");
                else if (i == 12)
                    Console.WriteLine("Youtube (OttoBotCode)");
                else if (i == 13)
                    Console.WriteLine("W3Schools");
                else if (i == 14)
                    Console.WriteLine("Stack Overflow");
                else if (i == 18)
                    Console.WriteLine("Made by: ");
                else if (i == 20)
                    Console.WriteLine("Lukas");
                else
                    Console.WriteLine();
                height++;
                Thread.Sleep(500);
            }
            Environment.Exit(0);
        }
    }
}