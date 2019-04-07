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
        /// <summary>
        /// Root of the tree and current Status of the game
        /// Root will be moved by the user to progress the game
        /// </summary>
        protected virtual IGameStatus Current { get; }

        private readonly Random random = new Random();

        /// <summary>
        /// Uses Monte Carlo Search Tree to determine the next move with the best score
        /// </summary>
        /// <param name="isMax">determines if current turn is maximizer or minimizer</param>
        /// <param name="playouts">the number of playouts for Monte Carlo to perform</param>
        /// <returns>The optimal game Status</returns>
        protected virtual IGameStatus BestMove(bool isMax, int playouts)
        {
            if (Current.IsTerminal)
            {
                return Current;
            }

            for (int i = 0; i < playouts; i++)
            {
                MonteCarlo(Current, isMax);
            }

            return Current.Moves.OrderByDescending(x => x.Simulations).First();
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
            if (curr.IsTerminal)
            {
                UpdateStatus(curr, isMax, curr.Value);
                return curr.Value;
            }

            if (!curr.Moves.All(x => x.Visited))
            {
                IGameStatus child = Expand(curr);

                int value = Simulate(child);

                UpdateStatus(child, !isMax, value);
                UpdateStatus(curr, isMax, value);
            }

            double[] uct = curr.Moves.Select(x => UCT(x, curr.Simulations)).ToArray();
            double maxUCT = uct.Max();

            var bestMoves = curr.Moves.Where((x, index) => uct[index] == maxUCT);

            var pickedMove = bestMoves.ElementAt(random.Next(0, bestMoves.Count()));

            int termValue = MonteCarlo(pickedMove, !isMax);

            UpdateStatus(curr, isMax, termValue);

            return termValue;
        }

        private double UCT(IGameStatus child, double parentSims, double rate = 1.41421356237)
        {
            return (child.Simulations - child.Wins) / child.Simulations + rate * Math.Sqrt(Math.Log(parentSims) / child.Simulations);
        }

        /// <summary>
        /// Returns a non-visited child from the given Status using
        /// the Default Policy
        /// </summary>
        /// <param name="Status">the game Status to expand upon</param>
        /// <returns>a non-visited child of the given Status</returns>
        private IGameStatus Expand(IGameStatus status)
        {
            //select all children who are not visited
            var unvisitedMoves = status.Moves.Where(s => !s.Visited).ToList();

            //return a random unvisited child of the given state
            return unvisitedMoves[random.Next(unvisitedMoves.Count)];
        }

        /// <summary>
        /// Runs a game using the Default Policy until a terminal node is reached
        /// The children are then cleared and the terminal value returned
        /// </summary>
        /// <param name="startStatus">the game Status to simulate from</param>
        /// <returns>the value from the reached terminal node</returns>
        private int Simulate(IGameStatus startStatus, int depth = -1)
        {
            startStatus.Visited = true;

            if (startStatus.IsTerminal)
            {
                return startStatus.Value;
            }

            IGameStatus curr = startStatus;
            int currDepth = 0;
            while (!curr.IsTerminal)
            {
                curr = curr.Moves.ElementAt(random.Next(0, curr.Moves.Count()));

                if (currDepth == depth)
                {
                    break;
                }
            }
            startStatus.Moves = null;

            return curr.Value;
        }

        /// <summary>
        /// Updates the win & simulation count depending on the given arguments
        /// </summary>
        /// <param name="Status">the Status to update</param>
        /// <param name="isMax">if the Status is owed by the maximizer or minimizer</param>
        /// <param name="terminalValue">the terminal value we reached in the simulation phase</param>
        private void UpdateStatus(IGameStatus status, bool isMax, int terminalValue)
        {
            if ((isMax && terminalValue == 1) || (!isMax && terminalValue == 1)) //win game for current player
            {
                status.Wins++;
            }
            else if (terminalValue == 0)
            {
                status.Wins += 0.5;
            }

            status.Simulations++;
        }
    }
}

