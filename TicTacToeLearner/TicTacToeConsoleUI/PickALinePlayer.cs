using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToeConsoleUI
{
    public class PickALinePlayer : Player
    {
        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(PickMove());
        }

        private Move PickMove()
        {
            var other = Symbol == Symbol.X ? Symbol.O : Symbol.X;
            var board = Game.ViewBoard();
            var lines = Game.GetAllLines();

            var lineSymbols = lines.Select(line => line.ToLookup(m => board.SymbolAt(m)));
            var linesWithValidMoves = lineSymbols.Where(line => line[Symbol.Empty].Any());
            var orderedLinesWithValidMoves = linesWithValidMoves.OrderBy(line => line[other].Count()).ThenByDescending(line => line[Symbol].Count());

            return orderedLinesWithValidMoves.First()[Symbol.Empty].First();
        }
    }
}
