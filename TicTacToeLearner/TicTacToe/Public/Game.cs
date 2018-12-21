using MinmaxAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Internal;
using TicTacToe.Public.Event;
using Utils;

namespace TicTacToe.Public
{
    public class Game : IGame<Game, Move>
    {
        public delegate void OnMoveHandler(object sender, OnMoveEventArgs args);
        public delegate void OnWinHandler(object sender, OnWinEventArgs args);
        public delegate void OnStartHandler(object sender, OnStartEventArgs args);
        public delegate void OnShutdownHandler(object sender, EventArgs args);

        public event OnStartHandler RaiseOnStart;
        public event OnMoveHandler RaisePreMove;
        public event OnMoveHandler RaisePostMove;
        public event OnWinHandler RaiseOnWin;
        public event OnShutdownHandler RaiseShutdown;

        private Board board;
        private Player[] players;
        private int whoseTurn = 0;
        private IEnumerable<Symbol> availiableSymbols;
        Stack<Tuple<Board, int>> stack = new Stack<Tuple<Board, int>>();

        public Game(params Player[] players)
        {
            board = new Board(3, 3);

            this.players = players;   
            availiableSymbols = Enum.GetValues(typeof(Symbol)).Cast<Symbol>().Where(s => s != Symbol.Empty);
            
            if (players.Length > availiableSymbols.Count())
            {
                throw new InvalidOperationException("Cannot have more players than availiable symbols!");
            }

            foreach (Player player in players)
            {
                player.AddToGame(this);
            }
        }

        public void Start()
        {
            board.Clear();
            var unusedSymbols = availiableSymbols;

            foreach (Player player in this.players)
            {
                Symbol symbol;
                unusedSymbols = unusedSymbols.TakeRandom(out symbol);
                player.Symbol = symbol;
            }

            RaiseOnStart?.Invoke(this, new OnStartEventArgs(ViewBoard()));
            players[whoseTurn].OnTurn();
        }
        
        public void Shutdown()
        {
            RaiseShutdown?.Invoke(this, null);
        }
        
        public void MakeMove(Player player, Move move)
        {
            if (!IsPlayerTurn(player))
            {
                throw new InvalidOperationException(nameof(player));
            }

            if (!stack.Any())
            {
                RaisePreMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), move, players[whoseTurn]));
            }

            board.MakeMove(player.Symbol, move);

            if (!stack.Any())
            {
                RaisePostMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), move, players[whoseTurn]));
            }

            if (IsOver())
            {
                if (!stack.Any())
                {
                    RaiseOnWin?.Invoke(this, new OnWinEventArgs(ViewBoard(), move, GetWinner()));
                }
            }
            else 
            {
                whoseTurn = (whoseTurn + 1) % players.Length;
                if (!stack.Any())
                {
                    players[whoseTurn].OnTurn();
                }
            }
        }

        public Player GetWinner()
        {
            return IsWinningLine() ? players[whoseTurn] : null;
        }

        public bool IsOver()
        {
            return IsWinningLine() || !ValidMoves().Any();
        }

        private bool IsWinningLine()
        {
            var s = ViewBoard().Symbols;
            return Is3InARow(s, 0, 1, 2)
                || Is3InARow(s, 3, 4, 5)
                || Is3InARow(s, 6, 7, 8)
                || Is3InARow(s, 0, 3, 6)
                || Is3InARow(s, 1, 4, 7)
                || Is3InARow(s, 2, 5, 8)
                || Is3InARow(s, 0, 4, 8)
                || Is3InARow(s, 2, 4, 6);
        }

        private bool Is3InARow(Symbol[] symbols, int a, int b, int c)
        {
            return symbols[a] == symbols[b]
                && symbols[b] == symbols[c]
                && symbols[c] != Symbol.Empty;
        }

        public IEnumerable<Move> ValidMoves()
        {
            return board.GetValidMoves();
        }

        public bool IsPlayerTurn(Player player)
        {
            return players[whoseTurn] == player;
        }

        public BoardTO ViewBoard()
        {
            return board.AsBoardTO();
        }

        public void AddFakeMove(Move move)
        {
            stack.Push(new Tuple<Board, int>(board.Copy(), whoseTurn));
            MakeMove(players[whoseTurn], move);
            //RaisePostMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), move, players[whoseTurn]));
        }

        public void UndoFakeMove()
        {
            var prev = stack.Pop();
            board = prev.Item1;
            whoseTurn = prev.Item2;
            //RaisePostMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), null, players[whoseTurn]));
        }
    }
}
