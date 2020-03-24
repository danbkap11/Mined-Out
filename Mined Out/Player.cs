using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Player : ICell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Ch { get => '■'; }

        public ConsoleColor Color { get => ConsoleColor.Green; }

        public Player(int y, int x)
        {
            X = x;
            Y = y;
        }

        
        public void Move(int dx, int dy)
        {
            
            X += dx;
            Y += dy;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(X, Y);
            Console.Write(Ch);
            Field.field[Y - dy, X - dx] = new VisitedCell(X - dx, Y - dy);
            Console.SetCursorPosition(X - dx, Y - dy);
            Console.ForegroundColor = Field.field[Y - dy, X - dx].Color;
            Console.Write(Field.field[Y - dy, X - dx].Ch);
            Console.Beep(10000, 5);
            Console.SetCursorPosition(49, 2);
            Console.Write(AdjacentMines());
            Game.Moves++;
            Console.SetCursorPosition(40, 4);
            Console.Write(Game.Moves);
            if (Field.field[Y, X] is Coin)
            {
                Game.Coins++;
                Console.SetCursorPosition(40, 8);
                Console.Write(Game.Coins);
            }
        }
        public int AdjacentMines()
        {
            int counter = 0;
            if (Field.field[Y + 1, X] is Mine || Field.field[Y + 1, X] is Wall)
            {
                counter++;
            }
            if (Field.field[Y - 1, X] is Mine || Field.field[Y - 1, X] is Wall)
            {
                counter++;
            }
            if (Field.field[Y, X + 1] is Mine || Field.field[Y, X + 1] is Wall)
            {
                counter++;
            } 
            if (Field.field[Y, X - 1] is Mine || Field.field[Y, X - 1] is Wall)
            {
                counter++;
            }
            return counter;
        }
    }
}
