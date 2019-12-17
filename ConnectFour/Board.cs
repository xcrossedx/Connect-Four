using System;

namespace ConnectFour
{
    static class Board
    {
        public static string[] emptyPiece =
           {
            "         ",
            "         ",
            "         ",
            "         "
        };

        public static string[] piece =
        {
            "  ▓▓▓▓▓  ",
            "▓▓▓▓▓▓▓▓▓",
            "▓▓▓▓▓▓▓▓▓",
            "  ▓▓▓▓▓  "
        };

        public static string[] selectionPiece =
        {
            "   @@@   ",
            " @@@@@@@ ",
            " @@@@@@@ ",
            "   @@@   "
        };

        //PIECE MAP
        //-1 = EMPTY
        //0 = BLACK
        //1 = WHITE
        //2 = SELECTION
        public static int[,] pieceMap =
        {
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 }
        };

        public static int[,] oldPieceMap =
        {
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 }
        };

        //GAME BOARD
        public static string[] boardMap =
        {
            "     ╔═════════╦═════════╦═════════╦═════════╦═════════╦═════════╦═════════╗",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ╠═════════╬═════════╬═════════╬═════════╬═════════╬═════════╬═════════╣",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ╠═════════╬═════════╬═════════╬═════════╬═════════╬═════════╬═════════╣",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ╠═════════╬═════════╬═════════╬═════════╬═════════╬═════════╬═════════╣",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ╠═════════╬═════════╬═════════╬═════════╬═════════╬═════════╬═════════╣",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ╠═════════╬═════════╬═════════╬═════════╬═════════╬═════════╬═════════╣",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "     ║         ║         ║         ║         ║         ║         ║         ║",
            "    ═╩═════════╩═════════╩═════════╩═════════╩═════════╩═════════╩═════════╩═"
        };

        public static bool boardPlaced = false;

        public static int emptySpaceCount = 0;

        public static void Initialize()
        {
            Console.Clear();
            boardPlaced = false;

            if (Program.test)
            {
                emptySpaceCount = 0;
            }
            else
            {
                emptySpaceCount = 42;
            }

            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    oldPieceMap[r, c] = -1;

                    if (!Program.test)
                    {
                        pieceMap[r, c] = -1;
                    }
                    else
                    {
                        if (Program.testPieceMap[r, c] == -1)
                        {
                            emptySpaceCount += 1;
                        }
                        pieceMap[r, c] = Program.testPieceMap[r, c];
                    }
                }
            }
        }

        public static void Draw(bool animating)
        {
            bool CPU = false;

            if (Game.players == 0 || (Game.players == 1 && Game.turn == "Yellow")) 
            {
                CPU = true;
            }

            //DRAWING THE BOARD IF ITS THE BEGINING OF A GAME
            if (!boardPlaced)
            {
                string board = "\n\n";

                for (int r = 0; r < 31; r++)
                {
                    board += $"{boardMap[r]}\n";
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(board);
                Console.ForegroundColor = ConsoleColor.White;
                boardPlaced = true;
            }

            //DRAWING IN THE PIECE MAP
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if (oldPieceMap[r, c] != pieceMap[r, c])
                    {
                        string[] piece = Board.piece.Clone() as string[];

                        if (pieceMap[r, c] == -1)
                        {
                            Console.ForegroundColor = Console.BackgroundColor;
                        }
                        else if (pieceMap[r, c] == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (pieceMap[r, c] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (pieceMap[r, c] == 2)
                        {
                            piece = selectionPiece.Clone() as string[];

                            if (Game.turn == "Red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (Game.turn == "Yellow")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                        }

                        (int oRow, int col) = ((r * 5) + 3, (c * 10) + 6);

                        int row = oRow;

                        for (int pRow = 0; pRow < 4; pRow++)
                        {
                            Console.SetCursorPosition(col, row);
                            Console.Write(piece[pRow]);
                            row += 1;
                        }

                        oldPieceMap[r, c] = pieceMap[r, c];
                    }
                }
            }

            //CLEARING THE BOTTOM AREA OF THE SCREEN TO BE REPLACED
            ClearBottom();

            if (!animating)
            {
                if (!Game.over && !CPU)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(37, 34);
                    Console.Write("Go Back");
                    Console.SetCursorPosition(25, 37);
                    Console.Write("Move Left             Move Right");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(39, 36);
                    Console.Write("ESC");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(38, 37);
                    Console.Write("<   >");
                    Console.SetCursorPosition(40, 38);
                    Console.Write("v");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(40, 37);
                    Console.Write("+");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    if (Game.turn == "Red")
                    {
                        Console.SetCursorPosition(33, 40);
                        Console.Write("Place     Piece");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(39, 40);
                    }
                    else
                    {
                        Console.SetCursorPosition(32, 40);
                        Console.Write("Place        Piece");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(38, 40);
                    }
                    Console.Write(Game.turn);
                }
                else if (Game.over)
                {
                    if (Game.overState.Contains("Red"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (Game.overState.Contains("Yellow"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.SetCursorPosition(0, 36);
                    Console.WriteLine($"{Game.overState}");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"                                 ~~~~~~~~~~~~~~~");
                    Console.Write("                              Hit       to continue");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(34, 38);
                    Console.Write("ENTER");
                }
                else if (CPU)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(35, 37);
                    Console.WriteLine("Thinking.  ");
                }
            }
        }

        private static void ClearBottom()
        {
            for (int i = 0; i < 8; i ++)
            {
                Console.SetCursorPosition(0, 34 + i);
                Console.Write("                                                                           ");
            }
        }
    }
}
