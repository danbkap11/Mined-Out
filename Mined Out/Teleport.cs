using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Teleport : Cell
    {
        public override char Ch { get; set; }
        
        public override ConsoleColor Color { get; set; }

        public Teleport(int x, int y) : base(x, y)
        {
            Ch = 'O';
            Color = ConsoleColor.Blue;
        }
        public Teleport() : base()
        {

        }
    }
}
