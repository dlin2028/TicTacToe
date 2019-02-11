using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMaxLib;

namespace TicTacToe
{
    class GameTree : MiniMaxLib.GameTree
    {
        public GameStatus CurrentStatus;
        protected override IGameStatus currentStatus => CurrentStatus;

        public GameState State
        {
            get
            {
                if (CurrentStatus.IsTerminal)
                {
                    if (CurrentStatus.Value == int.MaxValue)
                    {
                        return GameState.Win;
                    }
                    else if (CurrentStatus.Value == int.MinValue)
                    {
                        return GameState.Lose;
                    }
                    else
                    {
                        return GameState.Tie;
                    }

                }
                return GameState.Tie;
            }
        }

        public GameTree(bool humanFirst = true)
        {
            CurrentStatus = new GameStatus(humanFirst); 

            if(!humanFirst)
            {
                CurrentStatus = (GameStatus)BestMove(true);
            }
        }

        public bool Move(int position)
        {
            if (CurrentStatus.IsTerminal || CurrentStatus.Board[position] != 0)
            {
                return false;
            }
            CurrentStatus = CurrentStatus.Move(position);

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(false);
            }
            return true;
        }
    }
}
