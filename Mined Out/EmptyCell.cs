using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct EmptyCell : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ConsoleColor Color { get => ConsoleColor.Magenta;  }

        public char Ch { get => ' '; }

        public EmptyCell(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
