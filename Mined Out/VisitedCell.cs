using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class VisitedCell : Cell
    { 
        public VisitedCell(int x, int y) : base(x, y)
        {
            Ch = '.';
            Color = ConsoleColor.White;
        }
       
    }
}
