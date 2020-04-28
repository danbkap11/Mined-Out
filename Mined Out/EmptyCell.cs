using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class EmptyCell : Cell
    {
        public override ConsoleColor Color { get; set; }

        public override char Ch { get; set; }

        public EmptyCell(int x, int y) : base(x, y)
        {
            Ch = ' ';
            Color = ConsoleColor.Magenta;
        }
        public EmptyCell() : base()
        {

        }

    }
}
