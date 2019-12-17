using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConnectFour
{
    static class Game
    {
        //GAME OVER STATE
        public static bool over = false;
        public static string overState = "                                      Draw!";

        //CURRENT TURN
        public static string turn = "null";
        public static int turnNum = 0;

        //NUMBER OF PLAYERS
        public static int players = 0;

        //BOT DIFFICULTIES
        public static (int level, bool expertLogic)[] difficulties = { (-1, false), (-1, false) };

        //CURRENTLY SELECTED ROW DEFAULTS TO CENTER
        public static (int row, int col) sel;

        private static bool backToMenu = false;

        public static void Initialize()
        {
            Console.Clear();
            over = false;
            backToMenu = false;
            Board.Initialize();

            if (!Program.test)
            {
                Random rng = new Random();

                if (rng.Next() % 2 == 0)
                {
                    turn = "Red";
                }
                else
                {
                    turn = "Yellow";
                }

                turnNum = 0;
            }
            else
            {
                turn = "Yellow";
                turnNum = 42 - Board.emptySpaceCount;
            }

            Board.Draw(false);
        }

        public static void Play()
        {
            Initialize();

            while (!over && !backToMenu)
            {
                if (players == 2)
                {
                    Input();
                    turnNum += 1;
                }

                if (players == 1)
                {
                    if (turn == "Red")
                    {
                        Input();
                        turnNum += 1;
                    }
                    else
                    {
                        DrStrange.TakeTurn();
                        turnNum += 1;
                    }
                }

                if (players == 0)
                {
                    DrStrange.TakeTurn();
                    turnNum += 1;
                }

                Board.emptySpaceCount = 42 - turnNum;

                if (Board.emptySpaceCount == 0 && !over)
                {
                    over = true;
                    overState = "                                      Draw!";
                }
            }

            if (over)
            {
                Board.Draw(false);
                Console.ReadLine();
            }

            Menus.MainMenu();
        }

        public static void Input()
        {
            List<int> availableCols = new List<int>();
            List<int> availableRows = new List<int>();

            for (int c = 3; c < 10; c++)
            {
                if (Board.pieceMap[0, c % 7] == -1)
                {
                    availableCols.Add(c % 7);
                }
            }

            if (availableCols.Count() > 0)
            {
                foreach (int i in availableCols)
                {
                    for (int r = 5; r >= 0; r--)
                    {
                        if (Board.pieceMap[r, i] == -1)
                        {
                            availableRows.Add(r);
                            break;
                        }
                    }
                }
            }
            else
            {
                over = true;
                overState = "                                      Draw!";
            }

            if (!over)
            {
                bool placed = false;
                int selectedSpace = 0;

                while (!placed && !backToMenu)
                {
                    Board.pieceMap[availableRows[selectedSpace], availableCols[selectedSpace]] = 2;
                    Board.Draw(false);

                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.RightArrow)
                    {
                        Board.pieceMap[availableRows[selectedSpace], availableCols[selectedSpace]] = -1;

                        if (selectedSpace == availableCols.Count() - 1)
                        {
                            selectedSpace = 0;
                        }
                        else
                        {
                            selectedSpace += 1;
                        }

                        Board.Draw(false);
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        Board.pieceMap[availableRows[selectedSpace], availableCols[selectedSpace]] = -1;

                        if (selectedSpace == 0)
                        {
                            selectedSpace = availableCols.Count() - 1;
                        }
                        else
                        {
                            selectedSpace -= 1;
                        }

                        Board.Draw(false);
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        placed = true;
                        sel = (availableRows[selectedSpace], availableCols[selectedSpace]);
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        backToMenu = true;
                    }
                }

                if (!backToMenu)
                {
                    PlacePiece();
                }
            }
        }

        public static void PlacePiece()
        {
            (int row, int col) = sel;
            Board.pieceMap[row, col] = -1;

            //IF BLACK TURN PLACE BLACK PIECE AND SWITCH TURNS
            if (turn == "Red")
            {
                for (int r = 0; r < row; r++)
                {
                    Board.pieceMap[r, col] = 0;
                    Board.Draw(true);
                    Board.pieceMap[r, col] = -1;
                    Thread.Sleep(100);
                }

                Board.pieceMap[row, col] = 0;
                turn = "Yellow";
                Board.Draw(false);
            }

            //IF WHITE TURN PLACE WHITE PIECE AND SWITCH TURNS
            else if (turn == "Yellow")
            {
                for (int r = 0; r < row; r++)
                {
                    Board.pieceMap[r, col] = 1;
                    Board.Draw(true);
                    Board.pieceMap[r, col] = -1;
                    Thread.Sleep(100);
                }

                Board.pieceMap[row, col] = 1;
                turn = "Red";
                Board.Draw(false);
            }

            over = Check(sel, Board.pieceMap);
        }

        public static bool Check((int row, int col) space, int[,] pieceMap)
        {
            bool won = false;

            //SET THE VALUE BEING CHECKED FOR
            int checkVal = pieceMap[space.row, space.col];

            //MAKE LINES FROM CURRENT PIECE
            List<List<int>> checkList = new List<List<int>>();
            //UP AND DOWN LINE
            List<int> upDown = new List<int>();
            //LEFT AND RIGHT LINE
            List<int> leftRight = new List<int>();
            //DIAGONAL LINE UP AND TO THE RIGHT
            List<int> upRight = new List<int>();
            //DIAGONAL LINE UP AND TO THE LEFT
            List<int> upLeft = new List<int>();

            for (int r = 5; r >= 0; r--)
            {
                for (int c = 0; c <= 6; c++)
                {
                    //ADDING TO UP DOWN
                    if (c == space.col)
                    {
                        upDown.Add(pieceMap[r, c]);
                    }
                    //ADDING TO LEFT RIGHT
                    if (r == space.row)
                    {
                        leftRight.Add(pieceMap[r, c]);
                    }
                    //ADDING TO UP RIGHT
                    if ((Math.Abs(c - space.col) == Math.Abs(r - space.row) && !(c - space.col == r - space.row)) || (r == space.row && c == space.col))
                    {
                        upRight.Add(pieceMap[r, c]);
                    }
                    //ADDING TO UP LEFT
                    if (r - space.row == c - space.col)
                    {
                        upLeft.Add(pieceMap[r, c]);
                    }
                }
            }

            checkList.Add(upDown);
            checkList.Add(leftRight);
            checkList.Add(upRight);
            checkList.Add(upLeft);

            foreach (List<int> line in checkList)
            {
                int lineCount = 0;

                int maxCount = 0;

                foreach (int val in line)
                {
                    if (val == checkVal)
                    {
                        lineCount++;
                    }
                    else if (lineCount < 4)
                    {
                        if (lineCount > maxCount)
                        {
                            maxCount = lineCount;
                        }

                        lineCount = 0;
                    }
                }

                if (lineCount >= 4)
                {
                    if (checkVal == 0)
                    {
                        won = true;
                        overState = "                                    Red Wins!";
                    }
                    else if (checkVal == 1)
                    {
                        won = true;
                        overState = "                                  Yellow Wins!";
                    }
                }
            }

            return won;
        }
    }
}
