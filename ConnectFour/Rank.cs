using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Rank
    {
        public int thisRank = 0;
        public int oppRank = 0;
        public bool isCenterCol = false;
        public bool thisWins = false;
        public bool oppWins = false;
        public bool thisWinsNext = false;
        public bool oppWinsNext = false;
        public bool thisHasTrap = false;
        public bool oppHasTrap = false;
        public bool allowsTrap = false;
    }
}
