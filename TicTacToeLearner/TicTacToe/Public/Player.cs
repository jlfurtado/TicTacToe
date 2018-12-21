using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Public
{
    public class Player
    {
        private Symbol _symbol;
        public Symbol Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value != Symbol.Empty ? value : throw new ArgumentException(nameof(Symbol));
            }
        }

        public Game Game { get; private set; }

        protected void MakeMove(Move move)
        {
            Game.MakeMove(this, move);
        }

        public virtual void OnTurn()
        {
            
        }

        public virtual void OnAttachToGame(Game game)
        {

        }

        public void AddToGame(Game game)
        {
            Game = game;
            OnAttachToGame(game);
        }
    }
}
