using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Public.Event
{
    public class OnStartEventArgs : EventArgs
    {
        public BoardTO BoardState { get; }

        public OnStartEventArgs(BoardTO boardState)
        {
            BoardState = boardState;
        }
    }
}
