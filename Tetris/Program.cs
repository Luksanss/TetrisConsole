using System.Runtime.CompilerServices;
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
}