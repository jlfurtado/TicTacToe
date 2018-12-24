using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToeConsoleUI.Players
{
    public class ClockwisePlayer : Player
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
