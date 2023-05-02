using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            // game params
            Console.CursorVisible = false;

            // start menu
            MenuStart();

            var game = new GameState();
            int scoreF = 0;

            while (!game.IsGameOver())
            {
                Print(game);
                Thread.Sleep(500);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed =  Console.ReadKey(true);
                    if (keyPressed.Key == ConsoleKey.LeftArrow)
                    {
                        game.MoveBlockLeft();
                    }
                    else if (keyPressed.Key == ConsoleKey.RightArrow)
                    {
                        game.MoveBlockRight();
                    }
                    else if (keyPressed.Key == ConsoleKey.DownArrow)
                    {
                        game.MoveBlockDown();
                    }
                    else if (keyPressed.Key == ConsoleKey.R)
                    {
                        game.RotateBlockCW();
                    }
                    else if (keyPressed.Key == ConsoleKey.T)
                    {
                        game.RotateBlockCCW();
                    }
                }
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                game.MoveBlockDown();
                scoreF = game.GameGrid.score;
                Console.WriteLine($"     Score: {scoreF}");
            }
            SaveHighScore(scoreF);
            // Console.WriteLine($"Game over, final score {scoreF}");

        }

        public static void setRed() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void setYellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void setGreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public static void setBlue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void setMagenta()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        public static void setCyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        public static void setDarkGray()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        public static void setDarkYellow()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public static void SaveHighScore(int newScore) 
        {
            string fileName = "highScoreTetris.txt";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + fileName;

            string s = "";
            if (File.Exists(path))
            {
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {

                    s = sr.ReadLine();
                }
            }
            else
            {
                // Create the file
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes($"{newScore}");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            int result;
            if (!int.TryParse(s, out result))
            {
                result = 0;
            }
            if (result < newScore)
            {
                Console.WriteLine("New high score!!");
                Console.WriteLine($"Your score: {newScore}");
                Console.WriteLine($"(previous score: {result})");
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    outputFile.Write(newScore);
                }
            }
            else
            {
                Console.WriteLine($"Your score: {newScore}");
                Console.WriteLine($"High score: {result}");
            }
        }
        public static void PrintGrid(int[,] gameGrid)
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
            int x = (Console.WindowWidth) / 2 - gameGrid.GetLength(0);
            int y = (Console.WindowHeight) / 2 - gameGrid.GetLength(1);

            for (int r = 0; r < gameGrid.GetLength(0); r++)
            {
                Console.SetCursorPosition(x, y);
                for (int c = 0; c < gameGrid.GetLength(1); c++)
                {
                    if (gameGrid[r, c] == 0)
                    {
                        setRed();
                    }
                    if (gameGrid[r, c] == 1)
                    {
                        setYellow();
                    }
                    if (gameGrid[r, c] == 2)
                    {
                        setGreen();
                    }
                    if (gameGrid[r, c] == 3)
                    {
                        setCyan();
                    }
                    if (gameGrid[r, c] == 4)
                    {
                        setDarkGray();
                    }
                    if (gameGrid[r, c] == 5)
                    {
                        setDarkYellow();
                    }
                    if (gameGrid[r, c] == 6)
                    {
                        setBlue();
                    }
                    if (gameGrid[r, c] == 7)
                    {
                        setMagenta();
                    }
                    Console.Write($" {gameGrid[r, c]} ");
                }
                y += 1;
            }
        }
        public static void PrintBlock(Block block, GameGrid gameGrid)
        {
            int[,] pos = new int[gameGrid.Rows, gameGrid.Columns];

            for (int i = 0; i < gameGrid.Columns; i++)
            {
                for (int n = 0; n < gameGrid.Rows; n++)
                {
                    pos[n, i] = gameGrid[n, i];
                }
            }

            foreach (Position p in block.TilePositions())
            {
                pos[p.Row, p.Column] = block.Id;
            }
            PrintGrid(pos);
        }

        public static void Print(GameState gameState)
        {
            PrintBlock(gameState.CurBlock, gameState.GameGrid);
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