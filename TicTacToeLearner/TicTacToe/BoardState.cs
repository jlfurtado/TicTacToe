using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public struct BoardState
    {
        public Symbol[] Squares { get; }

        public BoardState(int boardSize)
        {
            Squares = new Symbol[boardSize];
        }
    }
}
