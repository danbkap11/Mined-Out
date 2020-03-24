using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Mine : ICell
    {
       public int X { get; set; }
       public int Y { get; set; }
       public char Ch { get => 'm'; }
       public ConsoleColor Color { get => ConsoleColor.Magenta; }

   
        public Mine(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
