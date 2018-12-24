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

        public Board Copy()
        {
            var board = new Board(boardWidth, boardHeight);
            Array.Copy(boardState.Squares, board.boardState.Squares, 9);
            return board;
        }

        public IEnumerable<IEnumerable<Move>> GetAllLines()
        {
            var horiz = Enumerable.Range(0, boardWidth).Select(x => Enumerable.Range(0, boardHeight).Select(y => new Move(x, y)));
            var vert = Enumerable.Range(0, boardHeight).Select(y => Enumerable.Range(0, boardWidth).Select(x => new Move(x, y)));
            var result = horiz.Concat(vert);

            if (boardWidth == boardHeight)
            {
                var diagonals = new IEnumerable<Move>[]
                {
                    Enumerable.Range(0, boardWidth).Select(z => new Move(z, z)),
                    Enumerable.Range(0, boardHeight).Select(z => new Move(z, boardHeight - z - 1))
                };
                result = result.Concat(diagonals);
            }

            return result;
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
