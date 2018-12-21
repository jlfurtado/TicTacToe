using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinmaxAI
{
    public interface IGame<TGame, TMove> where TGame : IGame<TGame, TMove>
    {
        bool IsOver();
        IEnumerable<TMove> ValidMoves();

        void AddFakeMove(TMove move);
        void UndoFakeMove();
    }
}
