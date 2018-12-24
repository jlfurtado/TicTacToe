using SimpleMinds;
using TicTacToe.Public;

namespace TicTacToeConsoleUI.Players
{
    public class FirstMovePlayer : Player
    {
        private readonly FirstMoveMind mind = new FirstMoveMind();

        public override void OnTurn()
        {
            base.OnTurn();
            MakeMove(mind.PickMove(Game.ValidMoves()));
        }
    }
}
