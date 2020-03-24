using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    public interface ICell
    {
        int X { get; set; }
        int Y { get; set; }
        char Ch { get; }

        ConsoleColor Color { get; }
    }
}
