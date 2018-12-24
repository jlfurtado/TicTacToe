using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinmaxAI
{
    public class Mind<TGame, TMove> where TGame : IGame<TGame, TMove>
    {
        private class ScoredMove
        {
            public TMove Move { get; set; }
            public double Score { get; set; }             
        }

        private Func<TGame, int, double> scorer;
        private TGame game;

        public Mind(TGame game, Func<TGame, int, double> scorer)
        {
            this.scorer = scorer;
            this.game = game;
        }

        public TMove PickBestMove()
        {
            return Minmax(1, true).Move;
        }

        private ScoredMove Minmax(int depth, bool max)
        {
            if (game.IsOver())
            {
                return new ScoredMove()
                {
                    Move = default(TMove),
                    Score = scorer(game, depth)
                };
            }
            
            IEnumerable<ScoredMove> scoredMoves = game.ValidMoves().ToArray().Select(m => new ScoredMove()
            {
                Move = m,
                Score = FakeMoveScore(m, depth + 1, !max)
            });

            return scoredMoves.OrderBy(sm => sm.Score * (max ? -1.0 : 1.0)).First();
        }

        private double FakeMoveScore(TMove move, int depth, bool max)
        {
            game.AddFakeMove(move);
            var score = Minmax(depth, max).Score;
            game.UndoFakeMove();
            return score;
        }
    }
}
