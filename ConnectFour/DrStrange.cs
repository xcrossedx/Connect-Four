using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConnectFour
{
    public static class DrStrange
    {
        private static int thisVal = -1;
        private static int oppVal = -1;

        private static int[,] currentPieceMap =
        {
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, -1 }
        };

        private static List<(int row, int col)> rankChecklist;
        readonly private static List<Rank> ranks = new List<Rank>();

        private static (int row, int col) chosenSpace = (0, 0);

        readonly private static (int level, bool expertLogic)[] difficulty = { (0, false), (0, false) };

        public static void TakeTurn()
        {
            Screen.Draw(false);
            ClearRanks();
            TakeSnapshot();
            GenerateRanks();
            CompareRanks();
            PlacePiece();
        }

        private static void ClearRanks()
        {
            ranks.Clear();
            chosenSpace = (0, 0);
        }

        private static void TakeSnapshot()
        {
            if (Game.players == 1)
            {
                difficulty[0] = Game.difficulties[0];
                difficulty[1] = Game.difficulties[0];
            }
            else
            {
                (int level, bool expertLogic) dif1 = Game.difficulties[Game.turnNum % 2];
                (int level, bool expertLogic) dif2 = Game.difficulties[(Game.turnNum + 1) % 2];

                difficulty[0] = dif1;

                if (dif1.level < dif2.level)
                {
                    dif2.level = dif1.level;
                }

                if (!dif1.expertLogic && dif2.expertLogic)
                {
                    dif2.expertLogic = false;
                }

                difficulty[1] = dif2;
            }

            if (Game.turn == "Red")
            {
                thisVal = 0;
                oppVal = 1;
            }
            else
            {
                thisVal = 1;
                oppVal = 0;
            }

            currentPieceMap = Screen.pieces.map.Clone() as int[,];
        }

        private static List<(int row, int col)> MakeChecklist(int[,] pieceMap)
        {
            List<(int row, int col)> checklist = new List<(int row, int col)>();

            //ADDING TILES TO CHECK
            for (int c = 3; c < 10; c++)
            {
                //ONLYING ADDING NON FULL COLUMNS
                if (pieceMap[0, c % 7] == -1)
                {
                    //CHECKING SELECTION PLACEMENT FOR EACH COLUMN
                    for (int r = 5; r >= 0; r--)
                    {
                        if (pieceMap[r, c % 7] == -1)
                        {
                            checklist.Add((r, c % 7));
                            break;
                        }
                    }
                }
            }

            return checklist;
        }

        private static void GenerateRanks()
        {
            ranks.Clear();
            rankChecklist = MakeChecklist(currentPieceMap);

            Random rng = new Random();

            bool forced = false;

            for (int i = 0; i < rankChecklist.Count(); i++)
            {
                //INITIALIZING A NEW RANK FOR THE CHECK SPACE
                Rank rank = new Rank();

                //SETTING THE POSITION OF THE CHECK SPACE
                (int row, int col) = rankChecklist[i];

                for (int v = 0; v <= 1; v++)
                {
                    int val;

                    if (v == 0)
                    {
                        val = thisVal;
                    }
                    else
                    {
                        val = oppVal;
                    }

                    //SETTING STARTING BOARD STATE FOR EACH NEW ITEM ON THE CHECKLIST
                    int[,] initialPieceMap = currentPieceMap.Clone() as int[,];

                    //PLACING THE DR'S PIECE AT THE CHECKLIST SPACE
                    initialPieceMap[row, col] = val;

                    rank = CheckForce(rank, row, col, val, initialPieceMap);
                }

                ranks.Add(rank);
            }

            if (!forced)
            {
                //LOOKING AHEAD FOR EACH SPACE ON THE INITIAL CHECKLIST
                for (int i = 0; i < rankChecklist.Count(); i++)
                {
                    //SETTING THE POSITION OF THE CHECK SPACE
                    (int row, int col) = rankChecklist[i];

                    //SETTING CURRENT RANK
                    Rank rank = ranks[i];

                    //CHECKING FOR BOTH PLAYERS
                    for (int v = 0; v <= 1; v++)
                    {
                        int stepCount;
                        int val;

                        if (v == 0)
                        {
                            val = thisVal;
                        }
                        else
                        {
                            val = oppVal;
                        }

                        if (Screen.emptySpaceCount > difficulty[v].level)
                        {
                            stepCount = difficulty[v].level;
                        }
                        else
                        {
                            stepCount = Screen.emptySpaceCount;
                        }

                        //SETTING STARTING BOARD STATE FOR EACH NEW ITEM ON THE CHECKLIST
                        int[,] initialPieceMap = currentPieceMap.Clone() as int[,];

                        //RESETTING THE LIST OF BOARDSTATES
                        List<int[,]> boardStates = new List<int[,]>();

                        //PLACING THE DR'S PIECE AT THE CHECKLIST SPACE
                        initialPieceMap[row, col] = val;

                        //ADDING THE INITIAL MOVE TO THE BOARD STATE CHECKLIST
                        boardStates.Add(initialPieceMap.Clone() as int[,]);

                        //STEPPING THROUGH EACH TIME A PIECE CAN BE PLACED ON THE BOARD
                        for (int step = 1; step < stepCount; step++)
                        {
                            List<int[,]> newBoardStates = new List<int[,]>();

                            //CHECKING EACH STATE THAT THE BOARD COULD HAVE BEEN IN AFTER THE LAST STEP
                            foreach (int[,] board in boardStates)
                            {
                                List<(int row, int col)> currentChecklist = MakeChecklist(board);

                                //FOR EACH BOARDSTATE CHECK EVERY OPTION FROM THERE
                                foreach ((int row, int col) space in currentChecklist)
                                {
                                    int[,] tempPieceMap = board.Clone() as int[,];

                                    bool terminated = false;

                                    //IF THIS PLAYERS TURN
                                    if (step % 2 == 0)
                                    {
                                        //CHECK IF THE CURRENT SPACE WOULD WIN
                                        tempPieceMap[space.row, space.col] = val;

                                        if (Game.Check(space, tempPieceMap))
                                        {
                                            if (val == thisVal)
                                            {
                                                rank.thisRank += Screen.emptySpaceCount - step;
                                            }
                                            else
                                            {
                                                rank.oppRank += Screen.emptySpaceCount - step;
                                            }
                                            terminated = true;
                                        }

                                        if (difficulty[v].level >= 4)
                                        {
                                            //IF MEDIUM OR HIGHER CHECK IF THE OPPONENT WINS
                                            tempPieceMap[space.row, space.col] = (val + 1) % 2;

                                            if (Game.Check(space, tempPieceMap))
                                            {
                                                terminated = true;
                                            }

                                            if (!terminated && space.row > 0 && difficulty[v].level >= 6)
                                            {
                                                //IF HARD OR HIGHER CHECK IF THE PIECE ABOVE THE CURRENT PIECE MEANS A WIN FOR THE OPPONENT
                                                tempPieceMap[space.row, space.col] = val;
                                                tempPieceMap[space.row - 1, space.col] = (val + 1) % 2;

                                                if (Game.Check((space.row - 1, space.col), tempPieceMap))
                                                {
                                                    terminated = true;
                                                }

                                                //IF HARD PLUS CHECK IF THE PIECE ABOVE THE CURRENT PIECE MEANS A WIN FOR THE DR
                                                if (!terminated && difficulty[v].expertLogic)
                                                {
                                                    tempPieceMap[space.row, space.col] = (val + 1) % 2;
                                                    tempPieceMap[space.row - 1, space.col] = val;

                                                    if (Game.Check((space.row - 1, space.col), tempPieceMap))
                                                    {
                                                        terminated = true;
                                                    }
                                                }
                                            }
                                        }

                                        tempPieceMap[space.row, space.col] = val;

                                        if (space.row > 0)
                                        {
                                            tempPieceMap[space.row - 1, space.col] = -1;
                                        }
                                    }
                                    //IF OPPONENTS TURN
                                    else
                                    {
                                        tempPieceMap[space.row, space.col] = (val + 1) % 2;

                                        if (Game.Check(space, tempPieceMap))
                                        {
                                            if (val == thisVal)
                                            {
                                                rank.thisRank -= Screen.emptySpaceCount - step;
                                            }
                                            else
                                            {
                                                rank.oppRank -= Screen.emptySpaceCount - step;
                                            }
                                            terminated = true;
                                        }

                                        if (difficulty[(v + 1) % 2].level >= 4)
                                        {
                                            //IF MEDIUM OR HIGHER CHECK IF THE OPPONENT WINS
                                            tempPieceMap[space.row, space.col] = val;

                                            if (Game.Check(space, tempPieceMap))
                                            {
                                                terminated = true;
                                            }

                                            if (!terminated && space.row > 0 && difficulty[(v + 1) % 2].level >= 6)
                                            {
                                                //IF HARD OR HIGHER CHECK IF THE PIECE ABOVE THE CURRENT PIECE MEANS A WIN FOR THE OPPONENT
                                                tempPieceMap[space.row, space.col] = (val + 1) % 2;
                                                tempPieceMap[space.row - 1, space.col] = val;

                                                if (Game.Check((space.row - 1, space.col), tempPieceMap))
                                                {
                                                    terminated = true;
                                                }

                                                //IF HARD PLUS CHECK IF THE PIECE ABOVE THE CURRENT PIECE MEANS A WIN FOR THE DR
                                                if (!terminated && difficulty[(v + 1) % 2].expertLogic)
                                                {
                                                    tempPieceMap[space.row, space.col] = val;
                                                    tempPieceMap[space.row - 1, space.col] = (val + 1) % 2;

                                                    if (Game.Check((space.row - 1, space.col), tempPieceMap))
                                                    {
                                                        terminated = true;
                                                    }
                                                }
                                            }
                                        }

                                        tempPieceMap[space.row, space.col] = (val + 1) % 2;

                                        if (space.row > 0)
                                        {
                                            tempPieceMap[space.row - 1, space.col] = -1;
                                        }
                                    }

                                    //ONLY ADD BOARD STATES THAT WERE NOT TERMINATED
                                    if (!terminated)
                                    {
                                        newBoardStates.Add(tempPieceMap.Clone() as int[,]);
                                    }
                                }
                            }

                            //REPLACING THE OLD BOARD STATE LIST WITH THE NEW ONE
                            bool nextStep = true;

                            boardStates.Clear();

                            if (difficulty[0].level == 6 && step == stepCount - 1 && stepCount != Screen.emptySpaceCount && newBoardStates.Count() <= 2800 && newBoardStates.Count > 0)
                            {
                                stepCount += 1;
                            }

                            if (nextStep)
                            {
                                boardStates.AddRange(newBoardStates);

                                if (Program.debug)
                                {
                                    Debug(i, v, difficulty[v].level, step + 1, boardStates.Count());
                                }
                            }
                        }
                    }

                    if (rankChecklist.IndexOf((row, col)) == rankChecklist.Count() / 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.SetCursorPosition(44, 37);
                        Console.Write(".");
                    }
                    ranks[i] = rank;
                }
            }

            //CHECKING FOR A FORCED PIECE
            Rank CheckForce(Rank rank, int row, int col, int val, int[,] board)
            {
                //CHECKING IF THE INITIAL STATE IS A WIN
                if (Game.Check((row, col), board))
                {
                    //EASY
                    if (difficulty[0].level == 2)
                    {
                        if (rng.Next() % 10 < 7)
                        {
                            if (val == thisVal)
                            {
                                rank.thisWins = true;
                            }
                            else
                            {
                                rank.oppWins = true;
                            }
                            forced = true;
                        }
                    }
                    //MED, HARD, AND HARD PLUS
                    else
                    {
                        if (val == thisVal)
                        {
                            rank.thisWins = true;
                        }
                        else
                        {
                            rank.oppWins = true;
                        }
                        forced = true;
                    }
                }

                //CHECKING IF THE SPACE ABOVE THE CURRENT CHECK SPACE WOULD BE A WIN FOR THE OPPONENT
                if (difficulty[0].level >= 4 && !forced && row > 0)
                {
                    int[,] specialCaseBoard = board.Clone() as int[,];
                    specialCaseBoard[row - 1, col] = val;

                    if (Game.Check((row - 1, col), specialCaseBoard))
                    {
                        //MEDIUM
                        if (difficulty[0].level == 4)
                        {
                            if (rng.Next() % 10 < 7)
                            {
                                if (val == thisVal)
                                {
                                    rank.thisWinsNext = true;
                                }
                                else
                                {
                                    rank.oppWinsNext = true;
                                }
                            }
                        }
                        //HARD AND HARD PLUS
                        else
                        {
                            if (val == thisVal)
                            {
                                rank.thisWinsNext = true;
                            }
                            else
                            {
                                rank.oppWinsNext = true;
                            }
                        }

                        //HARD PLUS CHECKS IF THERE IS A TRAP SET BY THE OPPONENT
                        if (difficulty[0].expertLogic && row > 1)
                        {
                            specialCaseBoard[row - 2, col] = val;

                            if (Game.Check((row - 2, col), specialCaseBoard))
                            {
                                if (val == thisVal && rank.thisWinsNext)
                                {
                                    rank.thisHasTrap = true;
                                }
                                else if (val == oppVal && rank.oppWinsNext)
                                {
                                    rank.oppHasTrap = true;
                                }
                            }
                        }
                    }
                }

                //HARD PLUS CHECKS IF PLACING THE CURRENT PIECE MEANS THAT THE OPPONENT CAN FINISH SETTING UP A TRAP
                if (difficulty[0].expertLogic && !forced && Screen.emptySpaceCount > 5 && val == thisVal)
                {
                    bool allowsTrap = false;
                    bool cleared = true;

                    List<int[,]> specialBoardStates = new List<int[,]>();

                    //CLONES INITAL BOARDSTATE AND ADDS CURRENT RANK SPACE
                    int[,] specialBoard = board.Clone() as int[,];
                    specialBoard[row, col] = val;

                    if (row != 0)
                    {
                        specialBoard[row - 1, col] = (val + 1) % 2;

                        if (Game.Check((row - 1, col), specialBoard))
                        {
                            cleared = false;
                        }

                        specialBoard[row - 1, col] = -1;
                    }

                    if (cleared)
                    {
                        //MAKES CHECKLIST OFF INITIAL PIECE PLACEMENT FOR OPPONENTS TURN
                        List<(int row, int col)> specialChecklist = MakeChecklist(specialBoard);

                        //MAKES LIST OF BOARD STATES FROM OPPONENTS POSSIBLE TURNS
                        foreach ((int row, int col) space in specialChecklist)
                        {
                            specialBoard[space.row, space.col] = (val + 1) % 2;
                            specialBoardStates.Add(specialBoard.Clone() as int[,]);
                            specialBoard[space.row, space.col] = -1;
                        }

                        //FOR EACH OF THE OPPONENTS PLACED PIECES
                        foreach (int[,] state in specialBoardStates)
                        {
                            specialChecklist.Clear();
                            specialChecklist = MakeChecklist(state);

                            List<(int row, int col)> newSpecialChecklist = new List<(int row, int col)>();

                            foreach ((int row, int col) space in specialChecklist)
                            {
                                if (space.row != 0)
                                {
                                    specialBoard = state.Clone() as int[,];

                                    specialBoard[space.row, space.col] = val;
                                    specialBoard[space.row - 1, space.col] = (val + 1) % 2;

                                    if (!Game.Check((space.row - 1, space.col), specialBoard))
                                    {
                                        newSpecialChecklist.Add(space);
                                    }
                                }
                            }

                            if (newSpecialChecklist.Count > 0)
                            {
                                specialChecklist.Clear();
                                specialChecklist.AddRange(newSpecialChecklist);
                            }

                            foreach ((int row, int col) space in specialChecklist)
                            {
                                specialBoard = state.Clone() as int[,];

                                specialBoard[space.row, space.col] = val;

                                int extraSpecialWins = 0;

                                List<(int row, int col)> extraSpecialChecklist = MakeChecklist(specialBoard);

                                foreach ((int row, int col) extraSpace in extraSpecialChecklist)
                                {
                                    specialBoard[extraSpace.row, extraSpace.col] = (val + 1) % 2;

                                    if (Game.Check(extraSpace, specialBoard))
                                    {
                                        extraSpecialWins += 1;
                                    }

                                    specialBoard[extraSpace.row, extraSpace.col] = -1;
                                }

                                if (extraSpecialWins > 1)
                                {
                                    allowsTrap = true;
                                }

                                if (space.row >= 2)
                                {
                                    specialBoard[space.row - 1, space.col] = (val + 1) % 2;

                                    if (Game.Check((space.row - 1, space.col), specialBoard))
                                    {
                                        specialBoard[space.row, space.col] = (val + 1) % 2;
                                        specialBoard[space.row - 1, space.col] = val;
                                        specialBoard[space.row - 2, space.col] = (val + 1) % 2;

                                        if (Game.Check((space.row - 2, space.col), specialBoard))
                                        {
                                            allowsTrap = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (allowsTrap)
                        {
                            rank.allowsTrap = true;
                        }
                    }
                }

                //CHECKING IF THE CHECK SPACE IS IN THE CENTER COLUMN
                if (col == 3 && difficulty[0].level >= 6)
                {
                    rank.isCenterCol = true;

                    if (Screen.emptySpaceCount > 40 && val == thisVal)
                    {
                        forced = true;
                    }
                }

                return rank;
            }

            void Debug(int index, int player, int level, int step, int states)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(66, 0);
                Console.Write("      ");
                Console.SetCursorPosition(0, 0);
                Console.Write($" space index: {index}, player: {player}, level: {level}, step: {step}, # of board states: {states}");
                Console.BackgroundColor = ConsoleColor.Black;
                //Console.ReadKey(true);
            }
        }

        //COMPARE DR'S RANK WITH OPPONENT'S RANK FOR EACH SPACE IN THE INITIAL CHECKLIST
        private static void CompareRanks()
        {
            bool placed = false;

            //IF THERE IS ONLY ONE OPTION FOR PLACING A PIECE
            if (ranks.Count() == 1)
            {
                chosenSpace = rankChecklist[0];
                placed = true;
            }
            //IF DR CAN WIN THIS TURN
            else if (ranks.Exists(x => x.thisWins))
            {
                chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.thisWins))];
                placed = true;
            }
            //IF DR CAN'T WIN THIS TURN AND IF THE OPPONENT WOULD WIN THIS TURN
            else if (ranks.Exists(x => x.oppWins))
            {
                chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.oppWins))];
                placed = true;
            }
            //FOR HARD AND EXPERT BEGIN THE GAME WITH THE CENTER COLUMN
            else if (difficulty[0].level >= 6 && Screen.emptySpaceCount > 40)
            {
                chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.isCenterCol))];
                placed = true;
            }
            //FOR HARD AND EXPERT IF DR HAS A TRAP SET THEN FORCE IT
            else if (difficulty[0].level >= 6)
            {
                //FOR HARD SOMETIMES AVOID FORCING A TRAP IF THE OPPONENT HAS A COUNTER
                if (difficulty[0].level == 6 && !difficulty[0].expertLogic && ranks.Exists(x => x.thisHasTrap))
                {
                    Random rng = new Random();

                    if (rng.Next() % 10 < 7)
                    {
                        if (ranks.Exists(x => x.thisHasTrap & !x.oppWinsNext))
                        {
                            chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.thisHasTrap & !x.oppWinsNext))];
                            placed = true;
                        }
                    }
                    else
                    {
                        chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.thisHasTrap))];
                        placed = true;
                    }
                }
                //FOR EXPERT ONLY FORCE A TRAP IF THE OPPONENT DOESNT HAVE A COUNTER FOR IT
                else if (difficulty[0].expertLogic && ranks.Exists(x => x.thisHasTrap & !x.oppWinsNext))
                {
                    chosenSpace = rankChecklist[ranks.IndexOf(ranks.Find(x => x.thisHasTrap & !x.oppWinsNext))];
                    placed = true;
                }
            }
            

            //IF THE OPPONENT WOULDN'T WIN THIS TURN
            if (!placed)
            {
                List<int> safeSpaces = new List<int>();
                List<int> drRankVals = new List<int>();
                List<int> oppRankVals = new List<int>();
                List<int> Options = new List<int>();
                List<int> finalOptions = new List<int>();

                //MAKING A LIST OF INDEXES FOR THE SPACES THAT DONT LEAD TO THE OPPONENT WINNING
                for (int i = 0; i < ranks.Count(); i++)
                {
                    if (ranks.Exists(x => !x.oppWinsNext))
                    {
                        if (!ranks[i].oppWinsNext)
                        {
                            safeSpaces.Add(i);
                        }
                    }
                    else
                    {
                        safeSpaces.Add(i);
                    }
                }

                //HARD PLUS ALSO FILTERS OUT SPACES THAT NEED TO BE FORCED ON THE OPPONENT
                if (difficulty[0].expertLogic)
                {
                    List<int> newSafeSpaces = new List<int>();

                    foreach (int i in safeSpaces)
                    {
                        if (!ranks[i].thisWinsNext)
                        {
                            newSafeSpaces.Add(i);
                        }
                    }

                    if (newSafeSpaces.Count() > 0)
                    {
                        safeSpaces.Clear();
                        safeSpaces.AddRange(newSafeSpaces);
                    }
                }

                //HARD PLUS ALSO FILTERS MOVES THAT ALLOW THE OPPONENT TO MAKE A TRAP
                if (difficulty[0].expertLogic)
                {
                    List<int> newSafeSpaces = new List<int>();

                    foreach(int i in safeSpaces)
                    {
                        if (!ranks[i].allowsTrap)
                        {
                            newSafeSpaces.Add(i);
                        }
                    }

                    if (newSafeSpaces.Count() > 0)
                    {
                        safeSpaces.Clear();
                        safeSpaces.AddRange(newSafeSpaces);
                    }
                }

                foreach (int i in safeSpaces)
                {
                    drRankVals.Add(ranks[i].thisRank);
                    oppRankVals.Add(ranks[i].oppRank);
                }

                //IF THE DR IS DOING BETTER THAN THE OPPONENT OR THE DIFFICULTY IS TOO LOW TO BE A DICK
                if (drRankVals.Max() >= oppRankVals.Max() || difficulty[0].level <= 4)
                {
                    //MAKING A LIST OF SPACES WITH THE MAX RANK FOR THE DR WHERE THE OPPONENT WONT WIN NEXT
                    foreach (int i in safeSpaces)
                    {
                        if (ranks[i].thisRank == drRankVals.Max())
                        {
                            Options.Add(i);
                        }
                    }

                    if (difficulty[0].level > 2)
                    {
                        //MAKING A LIST OF THE OPPONENTS RANKS FOR THE SPACES WITH THE HIGHEST DR RANK
                        oppRankVals.Clear();

                        foreach (int space in Options)
                        {
                            oppRankVals.Add(ranks[space].oppRank);
                        }

                        //MAKING A NEW LIST OF THE REMAINING SPACES WITH THE HIGHEST RANK FOR THE OPPONENT
                        for (int i = 0; i < oppRankVals.Count(); i++)
                        {
                            if (oppRankVals[i] == oppRankVals.Max())
                            {
                                finalOptions.Add(Options[i]);
                            }
                        }
                    }
                    else
                    {
                        finalOptions.AddRange(Options);
                    }
                }
                //IF THE OPPONENT IS DOING BETTER THAN THE DR
                else
                {
                    //MAKING A LIST OF SPACES WITH THE MAX RANK FOR THE OPPONENT WHERE THE OPPONENT WONT WIN NEXT
                    foreach (int i in safeSpaces)
                    {
                        if (ranks[i].oppRank == oppRankVals.Max())
                        {
                            Options.Add(i);
                        }
                    }

                    //MAKING A LIST OF THE DRS RANKS FOR THE SPACES WITH THE HIGHEST OPPONENT RANK
                    drRankVals.Clear();

                    foreach (int space in Options)
                    {
                        drRankVals.Add(ranks[space].thisRank);
                    }

                    //MAKING A NEW LIST OF THE REMAINING SPACES WITH THE HIGHEST RANK FOR THE DR
                    for (int i = 0; i < drRankVals.Count(); i++)
                    {
                        if (drRankVals[i] == drRankVals.Max())
                        {
                            finalOptions.Add(Options[i]);
                        }
                    }
                }
                
                //HARD PLUS WILL PRIORITIZE THE CENTER COLOMN AFTER ALL ELSE IS CONSIDERED
                if (difficulty[0].expertLogic)
                {
                    bool found = false;
                    (int row, int col) centerSpace = (0, 0);

                    foreach (int i in finalOptions)
                    {
                        (int row, int col) = rankChecklist[i];

                        if (col == 3)
                        {
                            centerSpace = (row, 3);
                            found = true;
                        }
                    }

                    if (found)
                    {
                        finalOptions.Clear();
                        finalOptions.Add(rankChecklist.IndexOf(centerSpace));
                    }
                }

                //PICKING A RANDOM SPACE FROM THE FINAL OPTIONS
                Random r = new Random();

                chosenSpace = rankChecklist[finalOptions[r.Next(0, finalOptions.Count())]];

                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
        }

        //OFFICIALLY PLACES THE DESIRED PIECE
        private static void PlacePiece()
        {
            Console.SetCursorPosition(44, 37);
            Console.Write("..");
            Thread.Sleep(500);
            Game.sel = chosenSpace;
            Game.PlacePiece();
        }
    }
}
