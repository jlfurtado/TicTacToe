using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;

namespace TicTacToeConsoleUI
{
    public class ConsolePlayer : Player
    {
        private static readonly IReadOnlyDictionary<string, Move> squareNames = new Dictionary<string, Move>
        {
            { "TopLeft", new Move(0, 0) },
            { "TopCenter", new Move(1, 0) },
            { "TopRight", new Move(2, 0) },
            { "MiddleLeft", new Move(0, 1) },
            { "MiddleCenter", new Move(1, 1) },
            { "MiddleRight", new Move(2, 1) },
            { "BottomLeft", new Move(0, 2) },
            { "BottomCenter", new Move(1, 2) },
            { "BottomRight", new Move(2, 2) }
        };

        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(ReadMoveFromConsole());
        }

        private Move ReadMoveFromConsole()
        {
            var validMoves = Game.ValidMoves();

            string move = "";
            do
            {
                Console.Write($"{Symbol}: ");
                move = Console.ReadLine();
            }
            while (!squareNames.ContainsKey(move) || !validMoves.Contains(squareNames[move]));

            return squareNames[move];
        }
    }
}
