using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class AddLife : Cell
    { 
        public AddLife(int x, int y) : base(x, y)
        {
            Ch = 'L';
            Color = ConsoleColor.DarkGreen;
        }

        public static void OnAddLife(Player pl)
        {
            pl.Lives++;
            Console.SetCursorPosition(33, 10);
            Console.Write($"Lives: {pl.Lives}");
        }
    }
}
