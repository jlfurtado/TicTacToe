using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Public;
using Utils;

namespace SimpleMinds
{
    public class RandomMind
    {
        private static readonly Random rand = new Random();

        public Move PickMove(IEnumerable<Move> moves)
        {
            return moves.ElementAtOrDefault(rand.Next(moves.Count()));
        }
    }
}
