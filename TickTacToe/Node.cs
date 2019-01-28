using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum TileState { None = 2, Cpu = 1, User = -1 };
    public enum Player { Cpu = 1, User = 2 };
    public enum GameStatus { Tie = 0, Win = 1, Lose = -1, None = 2 }

    class Node
    {
        public TileState[] TileStates;
        public Node[] Children;

        //linq is cool
        public int Total => Children == null ? (int)Status : Children.Select(child => child.Total).Sum();
        public int Min => Children == null ? (int)Status * 10 : Children.Select(child => child.Min).Min() + 1;
        public int Max => Children == null ? (int)Status * 10 : Children.Select(child => child.Max).Max();
        public int Move;

        public GameStatus Status = GameStatus.None;

        public Node()
        {
            TileStates = new TileState[9] { TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None };
        }
        public Node(int position)
        {
            TileStates = new TileState[9] { TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None, TileState.None };
            TileStates[position] = TileState.User;
            Move = position;
        }
        public Node(Node prev, int position, TileState move)
        {
            TileStates = (TileState[])prev.TileStates.Clone();
            TileStates[position] = move;
            Move = position;

            if (!TileStates.Contains(TileState.None))
            {
                Status = GameStatus.Tie;
            }

            //0 1 2
            //3 4 5
            //6 7 8

            //horizontal wins
            var temp = TileStates.Take(3);
            for (int i = 0; i < 3; i++)
            {
                if (temp.All(x => x == temp.First()
                && x != TileState.None))
                {
                    Status = (GameStatus)temp.First();
                    return;
                }
                temp = temp.Take(3);
            }

            //vertical wins
            for (int i = 0; i < 3; i++)
            {
                if (TileStates[i] == TileStates[i + 3] && TileStates[i] == TileStates[i + 6]
                    && TileStates[i] != TileState.None)
                {
                    Status = (GameStatus)TileStates[i];
                    return;
                }
            }
            
            //diagonal wins
            if ((TileStates[0] == TileStates[4] && TileStates[4] == TileStates[8] ||
            TileStates[2] == TileStates[4] && TileStates[4] == TileStates[6])
            && TileStates[4] != TileState.None)
            {
                Status = (GameStatus)TileStates[4];
                return;
            }
        }
    }
}
