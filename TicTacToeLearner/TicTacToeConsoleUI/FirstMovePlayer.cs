using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;

namespace TicTacToeConsoleUI
{
    public class FirstMovePlayer : Player
    {
        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(Game.ValidMoves().First());
        }
    }
}
