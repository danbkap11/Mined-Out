using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Coin : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Ch { get => '■'; }

        public ConsoleColor Color { get => ConsoleColor.DarkBlue; }

        public Coin(int y, int x)
        {
            X = x;
            Y = y;
        }
    }
}
