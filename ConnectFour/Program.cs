using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConnectFour
{
    class Program
    {
        public static bool test = false;
        public static bool debug = false;

        public static int[,] testPieceMap =
        {
            { -1, 1, -1, 1, -1, 0, -1 },
            { -1, 0, -1, 1, -1, 1, -1 },
            { -1, 1, -1, 0, -1, 0, -1 },
            { -1, 0, -1, 1,  0, 1, -1 },
            { -1, 0,  0, 1,  1, 1, -1 },
            { -1, 0,  1, 1,  0, 0, -1 }
        };

        public static ColorProfile colors = new ColorProfile(ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue);

        static void Main()
        {
            Initialize();
            Menus.MainMenu();
        }

        public static void Initialize()
        {
            Console.CursorVisible = false;
            Console.Title = "Connect Four";
            Console.SetWindowSize(79, 42);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
    }
}
