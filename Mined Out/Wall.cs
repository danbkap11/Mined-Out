using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Wall : Cell
    { 
        public Wall(int x, int y) : base(x, y)
        {
            Ch = '#';
            Color = ConsoleColor.Gray;
        }
        public static void OnWall(Player pl)
        {
            pl.Lives--;
            Console.SetCursorPosition(33, 10);
            Console.Write($"Lives: {pl.Lives}");
        }
    }
}
