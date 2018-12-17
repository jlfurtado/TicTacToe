using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;
using TicTacToe.Public.Event;
using TicTacToeLearner;

namespace TicTacToeConsoleUI
{
    public class BoardUI
    {
        private Game game;
        private Dictionary<Player, int> winCounts;

        private static readonly IReadOnlyDictionary<string, Func<string[], Player>> testPlayers = new Dictionary<string, Func<string[], Player>>()
        {
            { "console", args => new ConsolePlayer() },
            { "learning", args => new LearningAIPlayer(args.FirstOrDefault()) },
            { "first", args => new FirstMovePlayer() },
            { "last", args => new LastMovePlayer() },
            { "random", args => new RandomPlayer() }
        };

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
            int gamesLeft = SetupGame();
            while (gamesLeft-- > 0)
            {
                game.Start();
                if (gamesLeft == 0)
                {
                    game.Shutdown();
                    gamesLeft = SetupGame();
                }
            }
        }

        private int SetupGame()
        {
            int gamesLeft;
            Console.Write("How many more?: ");
            if (!int.TryParse(Console.ReadLine(), out gamesLeft))
            {
                return 0;
            }

            if (gamesLeft > 0)
            {
                Console.Write("Enter players: ");

                IEnumerable<Tuple<string, string[]>> players = Console.ReadLine().Split(' ').Select(s => {
                    string[] srcArgs = s.TrimEnd(')').Split('(');
                    return new Tuple<string, string[]>(srcArgs.FirstOrDefault(), srcArgs.LastOrDefault().Split(','));
                });

                game = new Game(players.Select(p => testPlayers[p.Item1](p.Item2)).ToArray());
                winCounts = new Dictionary<Player, int>();

                Console.Write("Display board after each turn? (Y/N): ");
                if (Console.ReadLine().ToUpper().StartsWith("Y"))
                {
                    game.RaisePostMove += OnMove;
                }

                game.RaiseOnWin += OnWin;

                Console.Write("Display board on start? (Y/N): ");
                if (Console.ReadLine().ToUpper().StartsWith("Y"))
                {
                    game.RaiseOnStart += OnGameStart;
                }
            }

            return gamesLeft;
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
