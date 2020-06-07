using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mined_Out
{
    class Field
    {
        public int NumOfCoins;

        public Field(int coins, Cell[,] map)
        {
            NumOfCoins = coins;
            field = map;
        }
        public Field()
        {
            NumOfCoins = 2;
            field = new Cell[22, 32];
        }

        public Field(int x)
        {
            NumOfCoins = x;
            field = new Cell[22,32];
        }

        public Cell[,] field;

        public void PrintField()
        {
            int x = 80;
            int y = 26;
            Console.SetWindowSize(x + 1, y + 1);
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false;
            int width = 32;
            int height = 22;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!(field[i, j] is EmptyCell) && !(field[i, j] is Wall) && !(field[i, j] is Mine) && !(field[i, j] is WinCell) && !(field[i, j] is Player))
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else Console.BackgroundColor = field[i, j].Color;

                    Console.ForegroundColor = field[i, j].Color;
                    Console.Write(field[i, j].Ch);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White; 
            Console.SetCursorPosition(33, 2);
            Console.Write("Adjacent mines: 0");
            Console.SetCursorPosition(33, 4);
            Console.Write("Moves: 0");
            Console.SetCursorPosition(33, 6);
            Console.Write("Your time: 0");
            Console.SetCursorPosition(33, 8);
            Console.Write($"Coins: 0/{NumOfCoins}");
            Console.SetCursorPosition(33, 10);
            Console.Write("Lives: 2");
            Console.SetCursorPosition(33, 12);
            Console.Write("Score: 1000");


        }
            
    }
}
