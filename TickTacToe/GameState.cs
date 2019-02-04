using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTacToe
{
    class GameState : MiniMaxLib.IGameState
    {
        //Interface members
        public int Player { get; set; }
        public double Value { get; set; }
        public bool IsTerminal { get; set; }
        public MiniMaxLib.IGameState[] Children { get; }
    }
}
