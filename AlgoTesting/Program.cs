using System;
using TicTacToe;

namespace AlgoTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GameTree minimax;
            MCGameTree monteCarlo;

            Random rng = new Random();

            int wins = 0;

            while (true)
            {
                bool miniFirst = rng.Next(0, 2) == 0;

                minimax = new GameTree(!miniFirst);
                monteCarlo = new MCGameTree(miniFirst);

                int miniMove = -1;
                int mcMove = -1;
                if (miniFirst)
                {
                    miniMove = Array.FindIndex(minimax.CurrentStatus.Board, t => t == TileState.X);
                }
                else
                {
                    mcMove = Array.FindIndex(monteCarlo.CurrentStatus.Board, t => t == TileState.X);
                }

                while (!minimax.CurrentStatus.IsTerminal)
                {
                    if (miniFirst)
                    {
                        mcMove = monteCarlo.Move(miniMove);
                        miniMove = minimax.Move(mcMove);
                    }
                    else
                    {
                        miniMove = minimax.Move(mcMove);
                        mcMove = monteCarlo.Move(miniMove);
                    }
                }
                if (miniFirst)
                {
                     mcMove = monteCarlo.Move(miniMove);
                    miniMove = minimax.Move(mcMove);
                }
                else
                {
                    miniMove = minimax.Move(mcMove);
                    mcMove = monteCarlo.Move(miniMove);
                }

                Console.WriteLine($"Minimax\t\t{miniFirst}\t{minimax.State}");
                Console.WriteLine($"MonteCarlo\t{!miniFirst}\t{monteCarlo.State}\n");
                Console.ReadKey(true);

            }
        }
    }
}
