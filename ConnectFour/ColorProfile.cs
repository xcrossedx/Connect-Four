using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class ColorProfile
    {
        public ConsoleColor player1;
        public ConsoleColor player2;
        public ConsoleColor board;

        public ColorProfile(ConsoleColor player1, ConsoleColor player2, ConsoleColor board)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.board = board;
        }
    }
}
