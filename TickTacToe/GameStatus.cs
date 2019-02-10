using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMaxLib;

namespace TicTacToe
{
    public enum TileState { None = 2, Cpu = 1, User = -1 };
    public enum GameState { Tie = 0, Win = 1, Lose = -1 }

    public class GameStatus : IGameStatus
    {
        //saving stuff
        private IEnumerable<GameStatus> moves;

        //interface members
        public int Value { get; set; }
        public bool IsTerminal { get; set; }

        public IEnumerable<IGameStatus> Moves
        {
            get
            {
                if (moves is null)
                {
                    moves = GenerateMoves();
                }
                return moves;
            }
        }

        //class members
        public TileState Player;
        public TileState[] Board;

        public GameStatus Move(int move)
        {
            if(moves == null)
            {
                moves = GenerateMoves();
            }
            return moves.FirstOrDefault(x => x.Board[move] == TileState.User);
        }

        public GameStatus()
        {
            Player = TileState.Cpu;
            Board = new TileState[9];
            moves = null;

            Value = 0;
            IsTerminal = false;
        }
        public GameStatus(TileState[] prevBoard, int position, TileState move)
        {
            Board = (TileState[])prevBoard.Clone();
            Board[position] = move;
        }
        public GameStatus(TileState[] board, TileState move)
        {
            Player = move;
            Board = board;
            moves = null;

            Value = 0;
            CheckGameOver();
        }

        public IEnumerable<GameStatus> GenerateMoves()
        {
            //linq is cool
            return Board
                //make a new member for every tile state that is None, null if it's not None
                .Select((tile, index) => tile != TileState.None ? null : new GameStatus(Board, index, Player == TileState.Cpu ? TileState.User : TileState.Cpu))
                //remove null members
                .Where(child => child != null);
        }

        public void CheckGameOver()
        {
            if (!Board.Contains(TileState.None))
            {
                Value = (int)GameState.Tie;
            }

            //0 1 2
            //3 4 5
            //6 7 8

            //horizontal wins
            var temp = Board.Take(3);
            for (int i = 0; i < 3; i++)
            {
                if (temp.All(x => x == temp.First()
                && x != TileState.None))
                {
                    Value = (int)temp.First() * int.MaxValue;
                    return;
                }
                temp = temp.Take(3);
            }

            //vertical wins
            for (int i = 0; i < 3; i++)
            {
                if (Board[i] == Board[i + 3] && Board[i] == Board[i + 6]
                    && Board[i] != TileState.None)
                {
                    Value = (int)Board[i] * int.MaxValue;
                    return;
                }
            }

            //diagonal wins
            if ((Board[0] == Board[4] && Board[4] == Board[8] ||
            Board[2] == Board[4] && Board[4] == Board[6])
            && Board[4] != TileState.None)
            {
                Value = (int)Board[4] * int.MaxValue;
                return;
            }
        }
    }
}
