using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Wall : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Ch { get => '#'; }
        public ConsoleColor Color { get => ConsoleColor.Gray; }
        public Wall(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
