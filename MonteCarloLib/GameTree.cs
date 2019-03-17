using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonteCarloLib
{
    public abstract class GameTree
    {
        /// <summary>
        /// An abstract game tree with functions for Monte Carlo
        /// </summary>
        public abstract class GameController
        {
            /// <summary>
            /// Root of the tree and current Status of the game
            /// Root will be moved by the user to progress the game
            /// </summary>
            protected virtual IGameStatus Current { get; }

            //Terminal Values
            public static int MAX_WIN => 1;
            public static int MIN_WIN => -1;
            public static int TIE => 0;

            private readonly Random random = new Random();

            /// <summary>
            /// Uses Monte Carlo Search Tree to determine the next move with the best score
            /// </summary>
            /// <param name="isMax">determines if current turn is maximizer or minimizer</param>
            /// <param name="playouts">the number of playouts for Monte Carlo to perform</param>
            /// <returns>The optimal game Status</returns>
            protected virtual IGameStatus OptimalMove(bool isMax, int playouts)
            {

            }

            /// <summary>
            /// The Monte Carlo Tree Search Method
            /// Peforms 1 playout of: SELECTION, EXPANSION, SIMULATION, BACKPROPAGATION phases
            /// </summary>
            /// <param name="curr">the current node in the algorithm</param>
            /// <param name="isMax">whether it is the maximizers turn or the minimizers turn</param>
            /// <returns>the terminal value from the simulation phase of the playout</returns>
            private int MonteCarlo(IGameStatus curr, bool isMax)
            {
                if(curr.IsTerminal)
                {
                    UpdateStatus(curr, isMax, curr.Value);
                    return curr.Value;
                }
                
                if(!curr.Moves.All(x =>  x.Visited ))
                {
                    IGameStatus child = Expand(curr);

                     Simulate(child);
                }
            }

            private double UCT(IGameStatus child, double parentSims, double rate = 1.41421356237)
            {
                return child.Wins/child.Simulations + rate * Math.Sqrt(Math.Log(parentSims, Math.E) / child.Simulations);
            }

            /// <summary>
            /// Returns a non-visited child from the given Status using
            /// the Default Policy
            /// </summary>
            /// <param name="Status">the game Status to expand upon</param>
            /// <returns>a non-visited child of the given Status</returns>
            private IGameStatus Expand(IGameStatus Status)
            {

            }

            /// <summary>
            /// Runs a game using the Default Policy until a terminal node is reached
            /// The children are then cleared and the terminal value returned
            /// </summary>
            /// <param name="startStatus">the game Status to simulate from</param>
            /// <returns>the value from the reached terminal node</returns>
            private int Simulate(IGameStatus startStatus)
            {

            }

            /// <summary>
            /// Updates the win & simulation count depending on the given arguments
            /// </summary>
            /// <param name="Status">the Status to update</param>
            /// <param name="isMax">if the Status is owed by the maximizer or minimizer</param>
            /// <param name="terminalValue">the terminal value we reached in the simulation phase</param>
            private void UpdateStatus(IGameStatus Status, bool isMax, int terminalValue)
            {

            }
        }
    }
}
