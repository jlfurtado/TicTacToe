using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class OnMoveEventArgs : EventArgs
    {
        public BoardTO BoardState { get; }
        public Move Move { get; }
        public Player Player { get; }

        public OnMoveEventArgs(BoardTO boardState, Move move, Player player)
        {
            BoardState = boardState;
            Move = move;
            Player = player;
        }
    }
}
