using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AddLife : Cell
    {
        public override char Ch { get; set; }

        public override ConsoleColor Color { get; set; }

        public AddLife(int x, int y) : base(x, y)
        {
            Ch = 'L';
            Color = ConsoleColor.DarkGreen;
        }
        public AddLife() : base()
        {

        }
    }
}
