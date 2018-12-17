using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToe.Internal
{
    internal struct BoardState
    {
        public Symbol[] Squares { get; }

        public BoardState(int boardSize)
        {
            Squares = new Symbol[boardSize];
        }
    }
}
