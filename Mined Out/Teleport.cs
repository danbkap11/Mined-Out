using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Teleport : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Ch { get => 'O'; }

        public ConsoleColor Color { get => ConsoleColor.Blue; }

        public Teleport(int y, int x)
        {
            X = x;
            Y = y;
        }
    }
}
