using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Public;

namespace TicTacToe.Internal
{
    internal class Board
    {
        private BoardState boardState;
        private int boardWidth;
        private int boardHeight;

        public Board(int width, int height)
        {
            boardWidth = width;
            boardHeight = height;
            boardState = new BoardState(width * height);
        }

        public BoardTO AsBoardTO()
        {
            return new BoardTO(boardState.Squares);
        }

        public void MakeMove(Symbol player, Move move)
        {
            int moveLocation = MoveToIdx(move);
            if (boardState.Squares[moveLocation] != Symbol.Empty)
            {
                throw new InvalidOperationException(nameof(move));
            }

            boardState.Squares[moveLocation] = player;
        }

        public IEnumerable<Move> GetValidMoves()
        {
            for (int i = 0; i < boardState.Squares.Length; ++i)
            {
                if(boardState.Squares[i] == Symbol.Empty)
                {
                    yield return IdxToMove(i);
                }
            }
        }

        public void Clear()
        {
            Array.Clear(boardState.Squares, 0, boardState.Squares.Length);
        }

        private int MoveToIdx(Move move)
        {
            return move.Y * boardWidth + move.X;
        }

        private Move IdxToMove(int idx)
        {
            return new Move(idx % boardWidth, idx / boardWidth);
        }
    }
}
