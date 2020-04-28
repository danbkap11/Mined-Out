using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Trap : Cell
    {
        public override char Ch { get; set; }

        public override ConsoleColor Color { get; set; }

        public Trap(int x, int y) : base(x, y)
        {
            Ch = 'T';
            Color = ConsoleColor.DarkRed;
        }
        public Trap() : base()
        {

        }
    }
}
