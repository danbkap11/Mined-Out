using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class MultiplePoint : Cell
    {
        public static double Multiplicator { get; set; } = 1;
        public MultiplePoint(int x, int y) : base(x, y)
        {
            Ch = '*';
            Color = ConsoleColor.White;
        }

        public static void SetMultiplicator(double multiplicator)
        {
            Multiplicator = multiplicator;
        }
    }
}
