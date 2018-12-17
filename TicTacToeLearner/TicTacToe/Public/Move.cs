using System;

namespace TicTacToe.Public
{
    public class Move : IEquatable<Move>
    {
        public int X { get; }
        public int Y { get; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Move other)
        {
            return other == null ? false : other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {           
            return (object.ReferenceEquals(obj, this) || (GetType() == obj.GetType() && Equals(obj as Move)));
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X * 3 + Y; // A perfect hash because we only have 9 squares. Possibly change this later for dynamic board sizes?
            }
        }

    }
}
