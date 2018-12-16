using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToeLearner.KnowledgeStorage;
using Utils;

namespace TicTacToeLearner
{
    public class LearningAIPlayer : Player
    {
        private Mind mind;
        private List<Experience> pendingExperiences = new List<Experience>(9); // 9 moves per game

        public LearningAIPlayer(string filePath)
        {
            mind = new Mind(new FileKnowledgeStorer(filePath));
        }

        public override void OnTurn()
        {
            base.OnTurn();            
            MakeMove(mind.GetBestMove(Game.ViewBoard(), Symbol, Game.ValidMoves()));
        }

        public override void AddToGame(Game game)
        {
            base.AddToGame(game);

            // game.RaiseOnStart +=
            game.RaisePreMove += OnMove;
            game.RaiseOnWin += OnWin;
            game.RaiseShutdown += OnShutdown;
        }

        private void OnShutdown(object sender, EventArgs args)
        {
            mind.CommitToLongTermMemory();
        }

        private void OnWin(object sender, OnWinEventArgs args)
        {
            ProcessPendingExperiences(args.Player);
        }

        private void OnMove(object sender, OnMoveEventArgs args)
        {
            AddPendingExperience(new Experience(args.BoardState, args.Move, args.Player.Symbol));
        }

        private void AddPendingExperience(Experience pendingExperience)
        {
            pendingExperiences.Add(pendingExperience);
        }

        private void ProcessPendingExperiences(Player winner)
        {
            if (winner != null) // Don't even bother tracking cats games as they make it harder to learn later by inflating our denominators
            {
                int numExperiences = pendingExperiences.Count;
                for (int i = 0; i < numExperiences; ++i)
                {
                    var experience = pendingExperiences[i];
                    experience.Value = ((i + 1.0) / (numExperiences + 1.0)) * ((winner.Symbol == experience.Symbol) ? 1.0 : -1.0);
                    mind.LearnFromExperience(experience);
                }

                pendingExperiences.Clear();
            }
        }

        private void LogPendingExperiences()
        { 
            Console.WriteLine("Log Pending Experiences: ");
            Console.WriteLine(string.Join("\n", pendingExperiences.Select(pe =>
            {
                return $"({string.Join("", pe.BoardState)}, {pe.Move}, {pe.Symbol}, {pe.Value})";
            })));
            Console.WriteLine("End Log Pending Experiences");
        }
    }
}
