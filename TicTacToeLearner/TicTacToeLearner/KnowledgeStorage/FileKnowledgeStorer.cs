using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLearner.KnowledgeStorage
{
    public class FileKnowledgeStorer : IKnowledgeStorer
    {
        private string path;

        public FileKnowledgeStorer(string path)
        {
            this.path = path;
            if(!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public IEnumerable<Knowledge> LoadAllKnowledge()
        {
            return File.ReadAllLines(path).Select(line => Knowledge.FromString(line));
        }

        public void WriteAllKnowledge(IEnumerable<Knowledge> knowledge)
        {
            File.WriteAllLines(path, knowledge.Select(k => k.ToString()));
        }
    }
}
