using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class BoardTO : IEquatable<BoardTO>
    {
        public Symbol[] Symbols { get; }

        public BoardTO(IEnumerable<Symbol> symbols)
        {
            Symbols = symbols.ToArray();
        }

        public bool Equals(BoardTO other)
        {
            return other == null ? false : Enumerable.SequenceEqual(other.Symbols, Symbols);
        }

        public override bool Equals(object obj)
        {
            return (object.ReferenceEquals(obj, this) || (GetType() == obj.GetType() && Equals(obj as BoardTO)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 0;
                foreach (var s in Symbols)
                {
                    hash = hash * 3 + (int)s;
                }
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Join(string.Empty, Symbols.Select(s => s == Symbol.Empty ? " " : s.ToString()));
        }
    }
}
