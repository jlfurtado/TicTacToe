using SimpleMinds;
using TicTacToe.Public;

namespace TicTacToeConsoleUI.Players
{
    public class LastMovePlayer : Player
    {
        private readonly LastMoveMind mind = new LastMoveMind();

        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(mind.PickMove(Game.ValidMoves()));
        }
    }
}
