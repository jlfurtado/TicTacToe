using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;
using Utils;

namespace TicTacToeConsoleUI
{
    public class TryNewThingsPlayer : Player
    {
        private readonly Dictionary<BoardTO, ICollection<Move>> memory = new Dictionary<BoardTO, ICollection<Move>>();

        public override void OnTurn()
        {
            base.OnTurn();

            var board = Game.ViewBoard();
            if (!memory.ContainsKey(board))
            {
                memory[board] = new HashSet<Move>();
            }

            var moveMemories = memory[board];
            var validMoves = Game.ValidMoves();
            var move = validMoves.FirstOrDefault(vm => !moveMemories.Contains(vm));
                
            if (move != null)
            {
                moveMemories.Add(move);
                MakeMove(move);
            }
            else
            {
                MakeMove(validMoves.Random());
            }
        }
    }
}
