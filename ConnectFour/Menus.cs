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
            MainPrompt();
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
            Console.ForegroundColor = Program.colors.board;
            Console.SetCursorPosition(0, 2);
            string board = "";
            foreach (string s in titleBoard) { board += $"{s}\n"; }
            Console.Write(board);
            //LETTERS AND TILES
            //ROW 1
            Console.ForegroundColor = Program.colors.player2;
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
                if (c % 2 == 0)
                {
                    Console.ForegroundColor = Program.colors.player1;
                }
                else
                {
                    Console.ForegroundColor = Program.colors.player2;
                }

                for (int r = 0; r < 4; r++)
                {
                    Console.SetCursorPosition((c * 10) + 6, r + 8);
                    if (c != 3)
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

        private static void MainPrompt()
        {
            bool selected = false;
            int selection = 0;
            string[] selectors = { "_________", "_____________________", "_________" };

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(36, 20);
            Console.Write("Play Game");
            Console.SetCursorPosition(30, 24);
            Console.Write("Change Color Profile");
            Console.SetCursorPosition(36, 28);
            Console.Write("Exit Game");

            while (!selected)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (selection == 0)
                {
                    Console.SetCursorPosition(30, 25);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(selectors[1]);
                    Console.SetCursorPosition(36, 29);
                    Console.Write(selectors[2]);

                    Console.SetCursorPosition(36, 21);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(selectors[0]);
                }
                else if (selection == 1)
                {
                    Console.SetCursorPosition(36, 21);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(selectors[0]);
                    Console.SetCursorPosition(36, 29);
                    Console.Write(selectors[2]);

                    Random rng = new Random();

                    Console.SetCursorPosition(30, 25);
                    ConsoleColor[] colors = (ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor));

                    foreach (char c in selectors[1])
                    {
                        Console.ForegroundColor = colors[rng.Next(1, 15)];
                        Console.Write(c);
                    }
                }
                else if (selection == 2)
                {
                    Console.SetCursorPosition(30, 25);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(selectors[1]);
                    Console.SetCursorPosition(36, 21);
                    Console.Write(selectors[0]);

                    Console.SetCursorPosition(36, 29);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(selectors[2]);
                }

                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.UpArrow)
                {
                    if (selection == 0)
                    {
                        selection = 2;
                    }
                    else
                    {
                        selection -= 1;
                    }
                }

                if (input == ConsoleKey.DownArrow)
                {
                    if (selection == 2)
                    {
                        selection = 0;
                    }
                    else
                    {
                        selection += 1;
                    }
                }

                if (input == ConsoleKey.Enter)
                {
                    selected = true;
                }
            }

            DrawEscControl();
            ClearPromptArea();

            if (selection == 0)
            {
                PromptPlayers();
            }
            else if (selection == 1)
            {
                ClearControls();
                PromptColors();
            }
            else if (selection == 2)
            {
                System.Environment.Exit(0);
            }
        }

        private static int playerSelection = 1;

        private static void PromptPlayers()
        {
            bool selectedPlayers = false;
            bool backToInitialPrompt = false;

            string[] playersSelection = { "___            ", "      ___      ", "            ___" };

            //INITIAL PLAYER PROMPT
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(32, 20);
            Console.Write("How many players?");
            Console.SetCursorPosition(34, 22);
            Console.Write("0     1     2");

            //SELECTING NUMBER OF PLAYERS
            while (!selectedPlayers && !backToInitialPrompt)
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
                    Console.ForegroundColor = Program.colors.player1;
                    Console.Write(" o      ");
                    Console.ForegroundColor = Program.colors.player2;
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
                    Console.ForegroundColor = Program.colors.player1;
                    Console.Write("       o       ");
                    Console.ForegroundColor = Program.colors.player2;
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
                    Console.ForegroundColor = Program.colors.player1;
                    Console.Write("            o       ");
                    Console.ForegroundColor = Program.colors.player2;
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

                if (input == ConsoleKey.Escape)
                {
                    backToInitialPrompt = true;
                }
            }

            if (backToInitialPrompt)
            {
                ClearPromptArea();
                ClearEscControl();
                MainPrompt();
            }
            else if (Game.players < 2)
            {
                ClearPromptArea();
                PromptDif1();
            }
            else
            {
                Game.Play();
            }
        }

        private static void PromptColors()
        {
            DrawColorControls();

            bool finished = false;
            bool backToInitialPrompt = false;

            ConsoleColor[] colors = {ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Gray, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray };

            int[] selections = { Array.IndexOf(colors, Program.colors.player1), Array.IndexOf(colors, Program.colors.player2), Array.IndexOf(colors, Program.colors.board) };
            int currentSelection = 0;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(31, 18);
            Console.Write("Player 1 tile color");
            Console.SetCursorPosition(31, 22);
            Console.Write("Player 2 tile color");
            Console.SetCursorPosition(35, 26);
            Console.Write("Board color");

            while (!finished && !backToInitialPrompt)
            {
                for (int s = 0; s < 3; s++)
                {
                    Console.SetCursorPosition(36, 20 + (4 * s));
                    if (s == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write("<       >");
                    Console.SetCursorPosition(38, 20 + (4 * s));
                    Console.BackgroundColor = colors[selections[s]];
                    Console.Write("     ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.RightArrow)
                {
                    if (selections[currentSelection] == colors.Length - 1)
                    {
                        selections[currentSelection] = 0;
                    }
                    else
                    {
                        selections[currentSelection] += 1;
                    }
                }

                if (input == ConsoleKey.LeftArrow)
                {
                    if (selections[currentSelection] == 0)
                    {
                        selections[currentSelection] = colors.Length - 1;
                    }
                    else
                    {
                        selections[currentSelection] -= 1;
                    }
                }

                if (input == ConsoleKey.UpArrow)
                {
                    if (currentSelection == 0)
                    {
                        currentSelection = 2;
                    }
                    else
                    {
                        currentSelection -= 1;
                    }
                }

                if (input == ConsoleKey.DownArrow)
                {
                    if (currentSelection == 2)
                    {
                        currentSelection = 0;
                    }
                    else
                    {
                        currentSelection += 1;
                    }
                }

                if (input == ConsoleKey.Enter)
                {
                    finished = true;
                }

                if (input == ConsoleKey.Escape)
                {
                    backToInitialPrompt = true;
                }
            }

            ClearControls();

            if (backToInitialPrompt)
            {
                ClearPromptArea();
                MainMenu();
            }
            else
            {
                BuildProfile();
                MainMenu();
            }

            void BuildProfile()
            {
                Program.colors.player1 = colors[selections[0]];
                Program.colors.player2 = colors[selections[1]];
                Program.colors.board = colors[selections[2]];
            }
        }

        public static void DrawColorControls()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(55, 36);
            Console.Write("ESC");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(53, 34);
            Console.Write("Go Back");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(18, 37);
            Console.Write("Move Left         Move Right");
            Console.SetCursorPosition(28, 34);
            Console.Write("Move Up");
            Console.SetCursorPosition(27, 40);
            Console.Write("Move Down");
            Console.SetCursorPosition(50, 40);
            Console.Write("Apply Changes");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(29, 37);
            Console.Write("<   >");
            Console.SetCursorPosition(31, 36);
            Console.Write("^");
            Console.SetCursorPosition(31, 38);
            Console.Write("v");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(54, 38);
            Console.Write("ENTER");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(31, 37);
            Console.Write("+");
            Console.SetCursorPosition(56, 37);
            Console.Write("+");
        }

        public static void PromptDif1()
        {
            string[] difSelection = { "____                    ", "         ______         ", "                    ____" };
            bool backToPlayerPrompt = false;

            bool chosenDif1 = false;
            int dif1 = 0;

            WritePrompt();

            while (!chosenDif1 && !backToPlayerPrompt)
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
                        if (PromptExpertLogic(0))
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
                    backToPlayerPrompt = true;
                }
            }

            if (backToPlayerPrompt)
            {
                ClearPromptArea();
                PromptPlayers();
            }
            else if (Game.players == 0)
            {
                PromptDif2();
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
                    Console.ForegroundColor = Program.colors.player1;
                    Console.SetCursorPosition(48, 21);
                    Console.Write("O");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                Console.SetCursorPosition(28, 23);
                Console.Write("Easy     Medium     Hard");
            }
        }

        public static void PromptDif2()
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
                        if (PromptExpertLogic(1))
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
                PromptDif1();
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
                Console.ForegroundColor = Program.colors.player2;
                Console.SetCursorPosition(48, 21);
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(28, 23);
                Console.Write("Easy     Medium     Hard");
            }
        }

        private static bool PromptExpertLogic(int player)
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
            for (int i = 0; i <= 11; i++)
            {
                Console.SetCursorPosition(0, 18 + i);
                Console.Write("                                                            ");
            }
        }

        private static void DrawEscControl()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(39, 36);
            Console.Write("ESC");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(37, 34);
            Console.Write("Go Back");
        }

        public static void ClearEscControl()
        {
            Console.SetCursorPosition(37, 34);
            Console.WriteLine("       ");
            Console.SetCursorPosition(39, 36);
            Console.Write("   ");
        }

        public static void ClearControls()
        {
            for (int r = 34; r < 41; r++)
            {
                Console.SetCursorPosition(0, r);
                Console.WriteLine("                                                                           ");
            }
        }
    }
}
