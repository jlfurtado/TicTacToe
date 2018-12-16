using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using Utils;

namespace TicTacToeConsoleUI
{
    public class RandomPlayer : Player
    {
        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(Game.ValidMoves().Random());
        }
    }
}
