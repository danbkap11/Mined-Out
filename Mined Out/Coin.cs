using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Coin : Cell
    { 
        public Coin(int x, int y) : base(x, y)
        {
            Ch = '■';
            Color = ConsoleColor.DarkBlue;
        }
    }
}
