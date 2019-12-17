using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    static class Menus
    {
        public static void MainMenu()
        {
            TitleCard();
            Controls();
            Prompt();
        }

        private static void TitleCard()
        {
            string[] titleBoard =
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
                "     ╚═════════╩═════════╩═════════╩═════════╩═════════╩═════════╩═════════╝"
            };

            string[,] letters =
            {
                {
                    "╔════",
                    "║",
                    "║",
                    "╚════"
                },
                {
                    "╔═══╗",
                    "║   ║",
                    "║   ║",
                    "╚═══╝"
                },
                {
                    "╔═╗ ╗",
                    "║ ║ ║",
                    "║ ║ ║",
                    "╝ ╚═╝"
                },
                {
                    "╔═╗ ╗",
                    "║ ║ ║",
                    "║ ║ ║",
                    "╝ ╚═╝"
                },
                {
                    "╔════",
                    "╠══",
                    "║",
                    "╚════"
                },
                {
                    "╔════",
                    "║",
                    "║",
                    "╚════"
                },
                {
                    "══╦══",
                    "  ║",
                    "  ║",
                    "  ╝"
                },
                {
                    "  ╔   ╔",
                    "  ║   ║",
                    "  ╚═══╬",
                    "      ╚"
                },
            };

            Console.Clear();

            //TITLE CARD
            //BOARD BACKGROUND
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(0, 2);
            string board = "";
            foreach (string s in titleBoard) { board += $"{s}\n"; }
            Console.Write(board);
            //LETTERS AND TILES
            //ROW 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    Console.SetCursorPosition((c * 10) + 8, r + 3);
                    Console.Write(letters[c, r]);
                }
            }
            //ROW 2
            for (int c = 0; c < 7; c++)
            {
                int pieceVal;

                if (c < 3)
                {
                    pieceVal = c % 2;
                }
                else if (c == 3)
                {
                    pieceVal = 7;
                }
                else
                {
                    pieceVal = (c + 1) % 2;
                }

                if (pieceVal == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (pieceVal == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                for (int r = 0; r < 4; r++)
                {
                    Console.SetCursorPosition((c * 10) + 6, r + 8);
                    if (pieceVal < 2)
                    {
                        Console.Write(Screen.piece[r]);
                    }
                    else
                    {
                        Console.Write(letters[7, r]);
                    }
                }
            }
        }

        private static int playerSelection = 1;

        private static void Controls()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(25, 37);
            Console.Write("Move Left             Move Right");
            Console.SetCursorPosition(34, 40);
            Console.Write("Select Option");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(37, 37);
            Console.Write("<     >");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(38, 38);
            Console.Write("ENTER");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(40, 37);
            Console.Write("+");
        }

        private static void Prompt()
        {
            bool selectedPlayers = false;

            string[] playersSelection = { "___            ", "      ___      ", "            ___" };

            //INITIAL PROMPT
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(32, 20);
            Console.Write("How many players?");
            Console.SetCursorPosition(34, 22);
            Console.Write("0     1     2");

            //SELECTING NUMBER OF PLAYERS
            while (!selectedPlayers)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(33, 23);
                Console.Write(playersSelection[playerSelection]);
                Console.SetCursorPosition(29, 25);
                if (playerSelection == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[0]     [0]                ");
                    Console.SetCursorPosition(33, 25);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("vs. ");
                    Console.SetCursorPosition(29, 26);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" o      ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" o                 ");
                }
                else if (playerSelection == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("     (o_o) ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("vs. ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[0]       ");
                    Console.SetCursorPosition(29, 26);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("       o       ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" o            ");
                }
                else if (playerSelection == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("          (o_o)     (o_o)");
                    Console.SetCursorPosition(45, 25);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("vs. ");
                    Console.SetCursorPosition(29, 26);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("            o       ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  o");
                }

                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.LeftArrow)
                {
                    if (playerSelection == 0)
                    {
                        playerSelection = 2;
                    }
                    else
                    {
                        playerSelection -= 1;
                    }
                }

                if (input == ConsoleKey.RightArrow)
                {
                    playerSelection = (playerSelection + 1) % 3;
                }

                if (input == ConsoleKey.Enter)
                {
                    Game.players = playerSelection;
                    selectedPlayers = true;
                }
            }

            if (Game.players < 2)
            {
                ClearPromptArea();
                SecondPrompt();
            }
            else
            {
                Game.Play();
            }
        }

        public static void SecondPrompt()
        {
            string[] difSelection = { "____                    ", "         ______         ", "                    ____" };
            bool backToInitialPrompt = false;

            bool chosenDif1 = false;
            int dif1 = 0;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(39, 36);
            Console.Write("ESC");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(37, 34);
            Console.Write("Go Back");

            WritePrompt();

            while (!chosenDif1 && !backToInitialPrompt)
            {
                if (dif1 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (dif1 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (dif1 == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.SetCursorPosition(28, 24);
                Console.WriteLine(difSelection[dif1]);

                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.LeftArrow)
                {
                    if (dif1 == 0)
                    {
                        dif1 = 2;
                    }
                    else
                    {
                        dif1 -= 1;
                    }
                }

                if (input == ConsoleKey.RightArrow)
                {
                    dif1 = (dif1 + 1) % 3;
                }

                if (input == ConsoleKey.Enter)
                {
                    Game.difficulties[0].level = (dif1 * 2) + 2;

                    if (dif1 == 2)
                    {
                        if (ExpertLogicPrompt(0))
                        {
                            chosenDif1 = true;
                        }
                        else
                        {
                            WritePrompt();
                        }
                    }
                    else
                    {
                        chosenDif1 = true;
                        Game.difficulties[0].expertLogic = false;
                    }
                }

                if (input == ConsoleKey.Escape)
                {
                    backToInitialPrompt = true;
                }
            }

            if (backToInitialPrompt)
            {
                ClearPromptArea();
                ClearEscControl();
                Prompt();
            }
            else if (Game.players == 0)
            {
                ThirdPrompt();
            }
            else
            {
                Game.Play();
            }

            void WritePrompt()
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (Game.players == 1)
                {
                    Console.SetCursorPosition(31, 21);
                    Console.Write("Select a difficulty:");
                }
                else
                {
                    Console.SetCursorPosition(24, 21);
                    Console.Write("Select a difficulty for   player:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(48, 21);
                    Console.Write("O");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                Console.SetCursorPosition(28, 23);
                Console.Write("Easy     Medium     Hard");
            }
        }

        public static void ThirdPrompt()
        {
            string[] difSelection = { "____                    ", "         ______         ", "                    ____" };
            bool backToSecondPrompt = false;

            bool chosenDif2 = false;
            int dif2 = 0;

            WritePrompt();

            while (!chosenDif2 && !backToSecondPrompt)
            {
                if (dif2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (dif2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (dif2 == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.SetCursorPosition(28, 24);
                Console.WriteLine(difSelection[dif2]);

                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.LeftArrow)
                {
                    if (dif2 == 0)
                    {
                        dif2 = 2;
                    }
                    else
                    {
                        dif2 -= 1;
                    }
                }

                if (input == ConsoleKey.RightArrow)
                {
                    dif2 = (dif2 + 1) % 3;
                }

                if (input == ConsoleKey.Enter)
                {
                    Game.difficulties[1].level = (dif2 * 2) + 2;

                    if (dif2 == 2)
                    {
                        if (ExpertLogicPrompt(1))
                        {
                            chosenDif2 = true;
                        }
                        else
                        {
                            WritePrompt();
                        }
                    }
                    else
                    {
                        chosenDif2 = true;
                        Game.difficulties[1].expertLogic = false;
                    }
                }

                if (input == ConsoleKey.Escape)
                {
                    backToSecondPrompt = true;
                }
            }

            if (backToSecondPrompt)
            {
                SecondPrompt();
            }
            else
            {
                Game.Play();
            }

            void WritePrompt()
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(24, 21);
                Console.Write("Select a difficulty for   player:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(48, 21);
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(28, 23);
                Console.Write("Easy     Medium     Hard");
            }
        }

        private static bool ExpertLogicPrompt(int player)
        {
            ClearPromptArea();

            string[] cursor = { "___      ", "       __" };

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(33, 21);
            Console.Write("Enable          ?");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(40, 21);
            Console.Write("Hard Plus");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(36, 23);
            Console.Write("YES    NO");

            bool selected = false;
            bool Cont = true;

            int selection = 1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(36, 24);
            Console.Write(cursor[1]);

            while (!selected && Cont)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                Console.SetCursorPosition(36, 24);

                if (key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                {
                    selection = (selection + 1) % 2;
                    if (selection == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(cursor[selection]);
                }
                else if (key == ConsoleKey.Enter)
                {
                    selected = true;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Cont = false;
                }
            }

            ClearPromptArea();

            if (selected)
            {
                if (selection == 0)
                {
                    Game.difficulties[player].expertLogic = true;
                }
                else
                {
                    Game.difficulties[player].expertLogic = false;
                }
            }

            return Cont;
        }

        private static void ClearPromptArea()
        {
            for (int i = 0; i <= 7; i++)
            {
                Console.SetCursorPosition(0, 20 + i);
                Console.Write("                                                            ");
            }
        }

        public static void ClearEscControl()
        {
            Console.SetCursorPosition(37, 34);
            Console.WriteLine("       ");
            Console.SetCursorPosition(39, 36);
            Console.Write("   ");
        }
    }
}
