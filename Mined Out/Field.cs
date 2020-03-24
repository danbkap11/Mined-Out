using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    struct Field
    {
        public static List<(int, int)> minesList = new List<(int, int)>
            {
                (2, 3), (3, 7), (3, 8), (3, 14), (3, 16), (3, 18),
                (4, 11), (5, 6), (5, 15), (5, 17), (6, 5), (6, 6), (6, 13), (6, 14),
                (7, 14), (9, 13), (10, 16), (11, 7), (11, 9), (12, 13), (13, 9),
                (16, 6), (16, 7), (16, 10), (17, 9), (18, 15), (19, 12), (20, 3),
                (21, 4), (21, 6), (21, 9), (22, 11), (23, 17), (24, 12), (24, 16), (25, 7),
                (26, 3), (27, 4), (27, 9), (27, 16), (28, 7), (28, 12), (29, 5),
                (29, 8), (20, 12), (25, 7), (15, 10)
            };
        public static ICell[,] field = new ICell[23, 32];
        public static void InitializeField()
        {
            int x = 80;
            int y = 26;

            Console.SetWindowSize(x + 1, y + 1);
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false;

            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (minesList.Contains((j,i)))
                    {
                        field[i, j] = new Mine(i, j);
                    }
                    else if ((j <= 1 && i != 0) || (j >= 30 && i != 0) || 
                    (i == 1 && (j < 14 || j > 17)) || (i == 19 && (j < 13 || j > 17)) || i == 21)
                    {
                        field[i, j] = new Wall(i, j);
                    }
                    else if (i == 20 && j == 15)
                    {
                        field[i, j] = new Player(i, j);
                    }
                    else if (i == 8 && j == 7 || i == 6 && j == 25)
                    {
                        field[i, j] = new Coin(i, j);
                    }
                    else if (i == 1 && j >= 14 && j <= 17)
                    {
                        field[i, j] = new WinCell(i, j);
                    }
                    else if (i == 14 && j == 17)
                    {
                        field[i, j] = new Teleport(i, j);
                    }
                    else
                    {
                        field[i, j] = new EmptyCell(i, j);
                    }
                }
            }
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    Console.BackgroundColor = field[i, j].Color;
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
            Console.Write("Coins: 0/2");
        }
            
    }
}
