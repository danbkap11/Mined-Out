using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AntiPts : Cell
    {
        public static int AntiPoints { get; set; } = 0;
        public AntiPts(int x, int y) : base(x, y)
        {
            Ch = 'P';
            Color = ConsoleColor.DarkRed;
        }
        public static void SetAntiPts(int points)
        {
            AntiPoints = points;
        }
    }
}
