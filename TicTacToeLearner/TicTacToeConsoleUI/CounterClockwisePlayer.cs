using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToeConsoleUI
{
    public class CounterClockwisePlayer : Player
    {
        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(PickMove());
        }

        private Move PickMove()
        {
            throw new NotImplementedException();
        }
    }
}
