using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AddPts : Cell
    {
        public static int Points { get; set; } = 0;
        public AddPts(int x, int y) : base(x, y)
        {
            Ch = 'P';
            Color = ConsoleColor.DarkGreen;
        }
        public static void SetPts(int points)
        {
            Points = points;
        }
    }
}
