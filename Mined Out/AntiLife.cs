using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AntiLife : Cell
    {
        public override char Ch { get; set; }

        public override ConsoleColor Color { get; set; }

        public AntiLife(int x, int y) : base(x, y)
        {
            Ch = 'L';
            Color = ConsoleColor.DarkRed;
        }
        public AntiLife() : base()
        {

        }
    }
}
