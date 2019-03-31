using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonteCarloLib;

namespace TicTacToe
{
    [DebuggerDisplay("Wins: {Wins}, Sims: {Simulations}, Vis: {Visited}")]
    public class MCGameStatus : MonteCarloLib.IGameStatus
    {
        private List<MCGameStatus> moves;

        //interface members
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
            set
            {
                moves = (List<MCGameStatus>)value;
            }
        }

        public double Wins { get; set; }
        public double Simulations { get; set; }
        public bool Visited { get; set; }
        public int Value { get; set; }
        public bool IsTerminal { get; set; }

        //class members
        public TileState Player;
        public TileState[] Board;

        
        public MCGameStatus Move(int move)
        {
            if (moves == null)
            {
                moves = GenerateMoves();
            }
            MCGameStatus status = moves.FirstOrDefault(x => x.Board[move] == Player);
            if (status == null)
            {
                return this;
            }
            return status;
        }

        public MCGameStatus()
        {
            Player = TileState.X;
            Board = new TileState[9];
            moves = null;

            Value = 0;
            IsTerminal = false;
        }
        public MCGameStatus(TileState[] prevBoard, int position, TileState player)
        {
            Board = (TileState[])prevBoard.Clone();
            Board[position] = player == TileState.X ? TileState.O : TileState.X;
            Player = player;
            moves = null;

            Value = 0;
            IsTerminal = false;

            CheckGameOver();
        }
        //public GameStatus(TileState[] board, TileState player)
        //{
        //    Player = player;
        //    Board = board;
        //    moves = null;

        //    CheckGameOver();
        //}

        public List<MCGameStatus> GenerateMoves()
        {
            if (IsTerminal)
            {
                return new List<MCGameStatus>();
            }

            //linq is cool
            return Board
                //make a new member for every tile state that is None, null if it's not None
                .Select((tile, index) =>
                {
                    return tile != TileState.None ? null : new MCGameStatus(Board, index, Player == TileState.X ? TileState.O : TileState.X);
                })
                //remove null members
                .Where(child => child != null).ToList();
        }

        public void CheckGameOver()
        {
            if (!Board.Contains(TileState.None))
            {
                Value = (int)TileState.None;
                IsTerminal = true;
            }

            //0 1 2
            //3 4 5
            //6 7 8

            //horizontal wins
            for (int i = 0; i < 3; i++)
            {
                var row = Board.Skip(i * 3).Take(3);

                if (row.All(x => x == row.First()
                && x != TileState.None))
                {
                    IsTerminal = true;
                    Value = (int)row.First();
                    return;
                }

            }

            //vertical wins
            for (int i = 0; i < 3; i++)
            {
                if (Board[i] == Board[i + 3] && Board[i] == Board[i + 6]
                    && Board[i] != TileState.None)
                {
                    IsTerminal = true;
                    Value = (int)Board[i];
                    return;
                }
            }

            //diagonal wins
            if ((
                (Board[0] == Board[4] && Board[4] == Board[8]) ||
                (Board[2] == Board[4] && Board[4] == Board[6])
            ) && Board[4] != TileState.None)
            {
                IsTerminal = true;
                Value = (int)Board[4];
                return;
            }
        }
    }
}
