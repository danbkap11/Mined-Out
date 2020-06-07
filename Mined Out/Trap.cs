using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Trap : Cell
    { 
        public Trap(int x, int y) : base(x, y)
        {
            Ch = 'T';
            Color = ConsoleColor.DarkRed;
        }

        public static void OnTrap(Player player)
        {
            int counter = 0;
            while (counter < 4)
            {
                ConsoleKeyInfo keyTrap = Console.ReadKey(true);
                if (keyTrap.Key == ConsoleKey.W || keyTrap.Key == ConsoleKey.UpArrow
                                                || keyTrap.Key == ConsoleKey.A ||
                                                keyTrap.Key == ConsoleKey.LeftArrow
                                                || keyTrap.Key == ConsoleKey.S ||
                                                keyTrap.Key == ConsoleKey.DownArrow
                                                || keyTrap.Key == ConsoleKey.D ||
                                                keyTrap.Key == ConsoleKey.RightArrow)
                {
                    counter++;
                    Game.Moves++;
                    Console.SetCursorPosition(40, 4);
                    Console.Write(Game.Moves);
                }

                if (keyTrap.Key == ConsoleKey.Escape)
                {
                    Game.EscFlag = true;
                    Game.GameLost(player);
                }
            }
        }
    }
}
