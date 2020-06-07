using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Mine : Cell
    { 
        public Mine(int x, int y) : base(x, y)
        {
           Ch = 'm';
           Color = ConsoleColor.Magenta;
        }

        public static void OnMine(Player pl)
        {
            pl.Lives--;
            Console.SetCursorPosition(33, 10);
            Console.Write($"Lives: {pl.Lives}");
        }
    }
}
