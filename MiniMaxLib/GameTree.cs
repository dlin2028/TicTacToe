using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxLib
{
    public abstract class GameTree
    {
        protected virtual IGameStatus currentStatus { get; }
        
        protected virtual IGameStatus BestMove(bool isMax)
        {
            if (currentStatus.IsTerminal)
            {
                return currentStatus;
            }

            (int value, IGameStatus state) prime = (isMax ? int.MinValue : int.MaxValue, null);
            
            foreach (IGameStatus move in currentStatus.Moves)
            {
                int value = Minimax(move, !isMax);

                if (isMax && value > prime.value)
                {
                    prime.value = value;
                    prime.state = move;
                }
                else if (!isMax && value < prime.value)
                {
                    prime.value = value;
                    prime.state = move;
                }
            }

            return prime.state;
        }
        
        protected int Minimax(IGameStatus state, bool isMax)
        {
            if (state.IsTerminal)
            {
                return state.Value;
            }

            if (isMax)
            {
                int value = int.MinValue;
                foreach (IGameStatus move in state.Moves)
                {
                    value = Math.Max(value, Minimax(move, false));
                }
                return value;
            }
            else
            {
                int value = int.MaxValue;
                foreach (IGameStatus move in state.Moves)
                {
                    value = Math.Min(value, Minimax(move, true));
                }
                return value;
            }
        }
    }
}
