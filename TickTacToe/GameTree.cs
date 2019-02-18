using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMaxLib;

namespace TicTacToe
{
    enum GameState
    {
        Xwin,
        Owin,
        Tie
    };

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
                        return GameState.Xwin;
                    }
                    else if (CurrentStatus.Value == int.MinValue)
                    {
                        return GameState.Owin;
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
            CurrentStatus = new GameStatus(); 

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
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Player == TileState.X);
            }
            return true;
        }
    }
}
