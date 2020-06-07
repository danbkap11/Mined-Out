using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AntiLife : Cell
    { 
        public AntiLife(int x, int y) : base(x, y)
        {
            Ch = 'L';
            Color = ConsoleColor.DarkRed;
        }

        public static void OnAntiLife(Player pl)
        {
            pl.Lives--;
            Console.SetCursorPosition(33, 10);
            Console.Write($"Lives: {pl.Lives}");
        }
    }
}
