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

    public class MCGameTree : MonteCarloLib.GameTree
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
                CurrentStatus = (MCGameStatus)BestMove(true, 1000);
            }
        }

        public int Move(int position)
        {
            if (CurrentStatus.IsTerminal || CurrentStatus.Board[position] != 0)
            {
                return -1;
            }
            CurrentStatus = CurrentStatus.Move(position);

            TileState[] lastBoard = (TileState[])CurrentStatus.Board.Clone();

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (MCGameStatus)BestMove(CurrentStatus.Player == TileState.X, 1000);
            }

            for (int i = 0; i < lastBoard.Length; i++)
            {
                if (lastBoard[i] != CurrentStatus.Board[i])
                    return i;
            }
            //you suck at progromming
            return -1;
        }
    }
}
