using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxLib
{
    public interface IGameStatus
    {
        int Value { get; }
        bool IsTerminal { get; }
        IEnumerable<IGameStatus> Moves { get; }
    }
}
