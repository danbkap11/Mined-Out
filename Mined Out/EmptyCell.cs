using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class EmptyCell : Cell
    { 
        public EmptyCell(int x, int y) : base(x, y)
        {
            Ch = ' ';
            Color = ConsoleColor.Magenta;
        }
        
    }
}
