using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace TicTacToe
{
    public class Game
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

        public Game(params Player[] players)
        {
            board = new Board(3, 3);

            this.players = players;   
            availiableSymbols = Enum.GetValues(typeof(Symbol)).Cast<Symbol>().Where(s => s != Symbol.Empty);

            foreach (Player player in players)
            {
                player.AddToGame(this);
            }
        }

        public void Start()
        {
            board.Clear();
            var unusedSymbols = availiableSymbols;
            
            if (players.Length > unusedSymbols.Count())
            {
                throw new InvalidOperationException("Cannot have more players than availiable symbols!");
            }

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

            RaisePreMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), move, players[whoseTurn]));
            board.MakeMove(player.Symbol, move);
            RaisePostMove?.Invoke(this, new OnMoveEventArgs(ViewBoard(), move, players[whoseTurn]));

            if (IsWinningLine())
            {
               RaiseOnWin?.Invoke(this, new OnWinEventArgs(ViewBoard(), move, players[whoseTurn]));
            }
            else if (ValidMoves().Count() == 0)
            {
                RaiseOnWin?.Invoke(this, new OnWinEventArgs(ViewBoard(), move, null));
            }
            else 
            {
                whoseTurn = (whoseTurn + 1) % players.Length;
                players[whoseTurn].OnTurn();
            }

        }
        
        private bool IsWinningLine()
        {
            return Is3InARow(0, 1, 2)
                || Is3InARow(3, 4, 5)
                || Is3InARow(6, 7, 8)
                || Is3InARow(0, 3, 6)
                || Is3InARow(1, 4, 7)
                || Is3InARow(2, 5, 8)
                || Is3InARow(0, 4, 8)
                || Is3InARow(2, 4, 6);
        }

        private bool Is3InARow(int a, int b, int c)
        {
            var symbols = ViewBoard().Symbols;
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
    }
}
