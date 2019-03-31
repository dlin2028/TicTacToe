using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonteCarloLib;

namespace TicTacToe
{
//    enum GameState
//    {
//        Xwin,
//        Owin,
//        Tie
//    };

    class MCGameTree : MonteCarloLib.GameTree
    {
        public MCGameStatus CurrentStatus;
        protected override IGameStatus Current => CurrentStatus;

        public GameState State
        {
            get
            {
                if (CurrentStatus.IsTerminal)
                {
                    if (CurrentStatus.Value == 1)
                    {
                        return GameState.Xwin;
                    }
                    else if (CurrentStatus.Value == -1)
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

        public MCGameTree(bool humanFirst = true)
        {
            CurrentStatus = new MCGameStatus(); 

            if(!humanFirst)
            {
                CurrentStatus = (MCGameStatus)BestMove(true, 1000, 10);
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
                CurrentStatus = (MCGameStatus)BestMove(CurrentStatus.Player == TileState.X, 1000, 10);
            }
            return true;
        }
    }
}
