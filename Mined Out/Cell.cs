using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    public abstract class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Ch { get; set; }

        public ConsoleColor Color { get; set; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Ch = ' ';
            Color = ConsoleColor.Magenta;
        }

        public Cell()
        {
            X = 0;
            Y = 0;
            Ch = ' ';
            Color = ConsoleColor.Magenta;
        }
    }
}
