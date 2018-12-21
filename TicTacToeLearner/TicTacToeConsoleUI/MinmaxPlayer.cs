using MinmaxAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToeConsoleUI
{
    public class MinmaxPlayer : Player
    {
        private Mind<Game, Move> mind;

        public override void OnAttachToGame(Game game)
        {
            base.OnAttachToGame(game);
            mind = new Mind<Game, Move>(game, (g, depth) =>
            {
                var winner = g.GetWinner();
                return winner == null ? 0.0 : ((winner == this ? 1.0 : - 1.0) / depth);
            });
        }

        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(mind.PickBestMove());
        }
    }
}
