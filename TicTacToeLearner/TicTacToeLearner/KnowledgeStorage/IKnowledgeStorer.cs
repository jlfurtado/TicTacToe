using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLearner.KnowledgeStorage
{
    public interface IKnowledgeStorer
    {
        IEnumerable<Knowledge> LoadAllKnowledge();
        void WriteAllKnowledge(IEnumerable<Knowledge> knowledge);
    }
}
