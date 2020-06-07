using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Player : Cell
    {
        public int Lives { get; set; }
        public Player(int x, int y, int lives) : base(x, y)
        {
            Lives = lives;
            Ch = '■';
            Color = ConsoleColor.Green;
        }
        public Player() : this(0, 0, 1)
        {
            
        }

        public void Move(int dx, int dy, Field map)
        {
            var o = new object();
            X += dx;
            Y += dy;
            Console.ForegroundColor = ConsoleColor.Green;
            lock (o)
            { 
                Console.SetCursorPosition(X, Y);
                Console.Write('■');
                if (!(map.field[Y - dy, X - dx] is Teleport) && !(map.field[Y - dy, X - dx] is Wall))
                {
                    map.field[Y - dy, X - dx] = new VisitedCell(X - dx, Y - dy);
                }

                Console.SetCursorPosition(X - dx, Y - dy);
                Console.ForegroundColor = map.field[Y - dy, X - dx].Color;
                Console.Write(map.field[Y - dy, X - dx].Ch);
                Console.Beep(10000, 5);
                Console.SetCursorPosition(49, 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(AdjacentMines(map));
                Game.Moves++;
                Console.SetCursorPosition(40, 4);
                Console.Write(Game.Moves);
                if (map.field[Y, X] is Coin)
                {
                    Game.Coins++;
                    Console.SetCursorPosition(40, 8);
                    Console.Write(Game.Coins);
                }
            }
        }
        public int AdjacentMines(Field map)
        {
            int counter = 0;
            if (map.field[Y + 1, X] is Mine || map.field[Y + 1, X] is Wall)
            {
                counter++;
            }
            if (map.field[Y - 1, X] is Mine || map.field[Y - 1, X] is Wall)
            {
                counter++;
            }
            if (map.field[Y, X + 1] is Mine || map.field[Y, X + 1] is Wall)
            {
                counter++;
            } 
            if (map.field[Y, X - 1] is Mine || map.field[Y, X - 1] is Wall)
            {
                counter++;
            }
            return counter;
        }
    }
}
