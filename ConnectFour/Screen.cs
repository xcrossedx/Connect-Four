using System;

namespace ConnectFour
{
    static class Screen
    {
        //Piece sprites
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

        //Piece maps
        public static PieceMap pieces;
        public static PieceMap oldPieces;
        

        //Game board outline
        public static string[] boardOutline =
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

            pieces = new PieceMap(PieceMap.def.Clone() as int[,]);
            oldPieces = new PieceMap(PieceMap.def.Clone() as int[,]);

            if (Program.test)
            {
                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 7; c++)
                    {
                        if (Program.testPieceMap[r, c] == -1)
                        {
                            emptySpaceCount += 1;
                        }
                        pieces.map[r, c] = Program.testPieceMap[r, c];
                    }
                }

                emptySpaceCount = 0;
            }
            else
            {
                emptySpaceCount = 42;
            }
        }

        public static void Draw(bool animating)
        {
            bool CPU = false;

            if (Game.players == 0 || (Game.players == 1 && Game.player == 2)) 
            {
                CPU = true;
            }

            //DRAWING THE BOARD IF ITS THE BEGINING OF A GAME
            if (!boardPlaced)
            {
                string board = "\n\n";

                for (int r = 0; r < 31; r++)
                {
                    board += $"{boardOutline[r]}\n";
                }

                Console.ForegroundColor = Program.colors.board;
                Console.Write(board);
                boardPlaced = true;
            }

            //DRAWING IN THE PIECE MAP
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if (oldPieces.map[r, c] != pieces.map[r, c])
                    {
                        string[] piece = Screen.piece.Clone() as string[];

                        if (pieces.map[r, c] == -1)
                        {
                            Console.ForegroundColor = Console.BackgroundColor;
                        }
                        else if (pieces.map[r, c] == 0)
                        {
                            Console.ForegroundColor = Program.colors.player1;
                        }
                        else if (pieces.map[r, c] == 1)
                        {
                            Console.ForegroundColor = Program.colors.player2;
                        }
                        else if (pieces.map[r, c] == 2)
                        {
                            piece = selectionPiece.Clone() as string[];

                            if (Game.player == 1)
                            {
                                Console.ForegroundColor = Program.colors.player1;
                            }
                            else if (Game.player == 2)
                            {
                                Console.ForegroundColor = Program.colors.player2;
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

                        oldPieces.map[r, c] = pieces.map[r, c];
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
                    if (Game.player == 1)
                    {
                        Console.ForegroundColor = Program.colors.player1;
                    }
                    else
                    {
                        Console.ForegroundColor = Program.colors.player2;
                    }
                    Console.SetCursorPosition(35, 40);
                    Console.Write("Place Piece");
                }
                else if (Game.over)
                {
                    if (Game.overState.Contains("1"))
                    {
                        Console.ForegroundColor = Program.colors.player1;
                    }
                    else if (Game.overState.Contains("2"))
                    {
                        Console.ForegroundColor = Program.colors.player2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    Console.SetCursorPosition(0, 36);
                    Console.WriteLine($"{Game.overState}");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"                                ~~~~~~~~~~~~~~~~~");
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
