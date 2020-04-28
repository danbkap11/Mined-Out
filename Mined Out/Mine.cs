using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Mine : Cell
    {
       public override char Ch { get; set; }
       public override ConsoleColor Color { get; set; }

       public Mine(int x, int y) : base(x, y)
       {
           Ch = 'm';
           Color = ConsoleColor.Magenta;
        }
       public Mine() : base()
       {

       }
    }
}
