using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToeLearner;

namespace TicTacToeConsoleUI
{
    public class BoardUI
    {
        private Game game;
        private Dictionary<Player, int> winCounts = new Dictionary<Player, int>();

        public BoardUI()
        {
            this.game = new Game(new RandomPlayer(), new LearningAIPlayer("five-million-overnight.txt"));
            //game.RaisePostMove += OnMove;
            game.RaiseOnWin += OnWin;
            //game.RaiseOnStart += OnGameStart;
        }

        private void OnMove(object sender, OnMoveEventArgs args)
        {
            DisplayBoard(args.BoardState);
        }

        private void OnGameStart(object sender, OnStartEventArgs args)
        {
            DisplayBoard(args.BoardState);
        }

        private void OnWin(object sender, OnWinEventArgs args)
        {
            var player = args.Player;

            if (player != null)
            {
                winCounts[player] = (winCounts.ContainsKey(player) ? winCounts[player] : 0) + 1;
            }

            Console.WriteLine(player == null ? "Cat's game!" : $"{player.Symbol} wins!");
            Console.WriteLine($"Win counts for players: ({string.Join(",", winCounts.Select(p => $"{p.Key}:{p.Value}"))})");
        }

        public void Start()
        {
            int gamesLeft = 1;
            while (gamesLeft-- > 0)
            {
                game.Start();
                if (gamesLeft == 0)
                {
                    game.Shutdown();
                    Console.Write("How many more?: ");
                    if (!int.TryParse(Console.ReadLine(), out gamesLeft))
                    {
                        gamesLeft = 0;
                    }
                }
            }
        }

        private void DisplayBoard(BoardTO board)
        {
            Console.WriteLine("---------");
            Console.WriteLine($"| {string.Join("|", board.Symbols.Take(3).Select(BoardDisplay))} |");
            Console.WriteLine("| ----- |");
            Console.WriteLine($"| {string.Join("|", board.Symbols.Skip(3).Take(3).Select(BoardDisplay))} |");
            Console.WriteLine("| ----- |");
            Console.WriteLine($"| {string.Join("|", board.Symbols.Skip(6).Take(3).Select(BoardDisplay))} |");
            Console.WriteLine("---------");
        }

        private string BoardDisplay(Symbol s)
        {
            return s == Symbol.Empty ? " " : s.ToString();
        }

    }
}
