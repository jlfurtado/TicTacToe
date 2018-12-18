using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;
using TicTacToeLearner.KnowledgeStorage;
using Utils;

namespace TicTacToeLearner
{
    public class Mind
    {
        private IKnowledgeStorer knowledgeStorer;
        private Dictionary<Experience, Knowledge> knowledge;

        public Mind(IKnowledgeStorer knowledgeStorer)
        {
            this.knowledgeStorer = knowledgeStorer;
            knowledge = knowledgeStorer.LoadAllKnowledge().ToDictionary(k => new Experience(k.BoardState, k.Move, k.Symbol), k => k);
        }

        public Move GetBestMove(BoardTO boardState, Symbol symbol, IEnumerable<Move> validMoves)
        {
            var validMoveExperiences = validMoves.Select(vm => new Experience(boardState, vm, symbol));
            var knownMoves = validMoveExperiences.Where(vm => knowledge.ContainsKey(vm));
            var unknownMoves = validMoves.Except(knownMoves.Select(km => km.Move));

            // TODO: clean this up
            var move = unknownMoves.FirstOrDefault();
            if (move != null)
            {
                return move;
            }

            var bestKnownMove = knownMoves.Select(km => knowledge[km]).OrderByDescending(km => km.AverageValue).FirstOrDefault();

            return unknownMoves.Count() == 0 || (bestKnownMove?.AverageValue ?? 0.0) > 0.25 ? bestKnownMove.Move : unknownMoves.Random();
        }

        public void LearnFromExperience(Experience experience)
        {
            if(knowledge.ContainsKey(experience))
            {
                knowledge[experience].AddExperience(experience);
            }
            else
            {
                knowledge[experience] = new Knowledge(experience);
            }           
        }

        public void CommitToLongTermMemory()
        {
            knowledgeStorer.WriteAllKnowledge(knowledge.Values);
        }
    }
}
