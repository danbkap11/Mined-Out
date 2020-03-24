using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct VisitedCell : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Ch { get => '.'; }

        public ConsoleColor Color { get => ConsoleColor.White; }
        public VisitedCell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
