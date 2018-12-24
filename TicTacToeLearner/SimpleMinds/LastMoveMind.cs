using System.Collections.Generic;
using System.Linq;
using TicTacToe.Public;

namespace SimpleMinds
{
    public class LastMoveMind
    {
        public Move PickMove(IEnumerable<Move> moves)
        {
            return moves.LastOrDefault();
        }
    }
}
