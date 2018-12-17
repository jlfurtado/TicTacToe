using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using TicTacToe.Public;

namespace TicTacToeLearner
{
    public class Knowledge
    {
        public BoardTO BoardState { get; }
        public Move Move { get; }
        public Symbol Symbol { get; }

        public double AverageValue
        {
            get
            {
                return totalValue / numExperiences;
            }
        }

        private int numExperiences;
        private double totalValue;

        private Knowledge(BoardTO boardState, Move move, Symbol symbol, int numExperiences, double totalValue)
        {
            BoardState = boardState;
            Move = move;
            Symbol = symbol;

            this.numExperiences = numExperiences;
            this.totalValue = totalValue;
        }

        public Knowledge(Experience experience)
        {
            BoardState = experience.BoardState;
            Move = experience.Move;
            Symbol = experience.Symbol;

            this.numExperiences = 1;
            this.totalValue = experience.Value;
        }

        public void AddExperience(Experience experience)
        {
            ++numExperiences;
            totalValue += experience.Value;
        }
        
        public static Knowledge FromString(string knowledge)
        {
            string[] values = knowledge.Split(',');
            return new Knowledge(
                new BoardTO(values[0].Select(s => s == 'X' ? Symbol.X : s == 'O' ? Symbol.O : Symbol.Empty)),
                new Move(int.Parse(values[1]), int.Parse(values[2])),
                (values[3] == "X" ? Symbol.X : values[3] == "O" ? Symbol.O : Symbol.Empty),
                int.Parse(values[4]),
                double.Parse(values[5])
            );
        }

        public override string ToString()
        {
            return $"{BoardState},{Move},{Symbol},{numExperiences},{totalValue}";
        }
    }
}
