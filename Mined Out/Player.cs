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
        public override char Ch { get; set; }

        public override ConsoleColor Color { get; set; }

        public Player(int x, int y, int lives) : base(x, y)
        {
            Lives = lives;
            Ch = '■';
            Color = ConsoleColor.Green;
        }
        public Player() : base()
        {
            Ch = '■';
            Color = ConsoleColor.Green;
        }


        public void Move(int dx, int dy, Field a)
        {
            var o = new object();
            X += dx;
            Y += dy;
            Console.ForegroundColor = ConsoleColor.Green;
            lock (o)
            {
                Console.SetCursorPosition(X, Y);
            Console.Write('■');
            if (!(a.field[Y - dy, X - dx] is Teleport) && !(a.field[Y - dy, X - dx] is Wall))
            {
                a.field[Y - dy, X - dx] = new VisitedCell(X - dx, Y - dy);
            }

            Console.SetCursorPosition(X - dx, Y - dy);
            Console.ForegroundColor = a.field[Y - dy, X - dx].Color;
            Console.Write(a.field[Y - dy, X - dx].Ch);
            Console.Beep(10000, 5);
            Console.SetCursorPosition(49, 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(AdjacentMines(a));
            Game.Moves++;
                Console.SetCursorPosition(40, 4);
                Console.Write(Game.Moves);
                if (a.field[Y, X] is Coin)
                {
                    Game.Coins++;
                    Console.SetCursorPosition(40, 8);
                    Console.Write(Game.Coins);
                }
            }
        }
        public int AdjacentMines(Field a)
        {
            int counter = 0;
            if (a.field[Y + 1, X] is Mine || a.field[Y + 1, X] is Wall)
            {
                counter++;
            }
            if (a.field[Y - 1, X] is Mine || a.field[Y - 1, X] is Wall)
            {
                counter++;
            }
            if (a.field[Y, X + 1] is Mine || a.field[Y, X + 1] is Wall)
            {
                counter++;
            } 
            if (a.field[Y, X - 1] is Mine || a.field[Y, X - 1] is Wall)
            {
                counter++;
            }
            return counter;
        }
    }
}
