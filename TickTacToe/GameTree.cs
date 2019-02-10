using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class GameTree : MiniMaxLib.GameTree
    {
        public override GameStatus currentStatus { get; set}

        public GameState State
        {
            get
            {
                if (currentStatus.IsTerminal)
                {
                    if (currentStatus.Value == int.MaxValue)
                    {
                        return GameState.Win;
                    }
                    else if (currentStatus.Value == int.MinValue)
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
            currentStatus = new GameStatus();


            if (!humanFirst)
            {
                currentStatus = (GameStatus)BestMove(true);
            }
        }

        public bool Move(int position)
        {
            if (currentStatus.IsTerminal || currentStatus.Board[position] != 0)
            {
                return false;
            }

            //human move
            currentStatus = currentStatus.Move(position);

            if (!currentStatus.IsTerminal)
            {
                //AI move if the game didn't finish
                currentStatus = (GameStatus)BestMove(currentStatus.Player == TileState.User);
            }
            return true;
        }
    }
}
