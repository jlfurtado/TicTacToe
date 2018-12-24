using SimpleMinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;
using Utils;

namespace TicTacToeConsoleUI
{
    public class RandomPlayer : Player
    {
        private RandomMind mind = new RandomMind();

        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(mind.PickMove(Game.ValidMoves()));
        }
    }
}
