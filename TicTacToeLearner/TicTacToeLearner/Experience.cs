using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;

namespace TicTacToeLearner
{
    public class Experience : IEquatable<Experience>
    {
        public BoardTO BoardState { get; }
        public Move Move { get; }
        public Symbol Symbol { get; }
        public double Value { get; set; }

        public Experience(BoardTO boardState, Move move, Symbol symbol)
        {
            BoardState = boardState;
            Move = move;
            Symbol = symbol;            
        }

        public bool Equals(Experience other)
        {
            return other == null ? false : other.BoardState.Equals(BoardState) && other.Move.Equals(Move) && other.Symbol == Symbol;
        }
       
        public override bool Equals(object obj)
        {
            return (object.ReferenceEquals(obj, this) || (GetType() == obj.GetType() && Equals(obj as Experience)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = BoardState.GetHashCode();
                hash = hash * 9 + Move.GetHashCode();
                hash = hash * 3 + (int)Symbol;
                return hash;
            }
        }


    }
}