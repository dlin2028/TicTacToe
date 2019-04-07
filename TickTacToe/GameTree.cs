using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMaxLib;

namespace TicTacToe
{
    public enum GameState
    {
        Xwin = 1,
        Owin = -1,
        Tie = 0
    };

    public class GameTree : MiniMaxLib.GameTree
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

        public int Move(int position)
        {
            if (CurrentStatus.IsTerminal || CurrentStatus.Board[position] != 0)
            {
                return -1;
            }
            CurrentStatus = CurrentStatus.Move(position);

            TileState[] lastBoard = CurrentStatus.Board;

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Player == TileState.X);
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
