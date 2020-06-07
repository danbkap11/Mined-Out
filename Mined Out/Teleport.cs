using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mined_Out
{
    class Teleport : Cell
    { 
        public Teleport(int x, int y) : base(x, y)
        {
            Ch = 'O';
            Color = ConsoleColor.Blue;
        }
        public Teleport() : base()
        {

        }
        public static Tuple<int, int> Find(Cell[,] field, int Y, int X)
        {
            Tuple<int, int> res = new Tuple<int, int>(0, 0);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j].GetType().Equals(new Teleport().GetType()) && (i != Y || j != X))
                    {
                        Tuple<int, int> res2 = new Tuple<int, int>(i, j);
                        res = res2;
                    }
                    else continue;
                }
            }

            return res;
        }

        public static void OnTeleport(Player player, Field map)
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 10);
            if (x < 7)
            {
                if (x % 2 == 1)
                {
                    var secondTeleport = Find(map.field, player.Y, player.X);
                    player.Move(secondTeleport.Item2 - player.X, secondTeleport.Item1 - player.Y, map);

                }
                else
                {
                    while (true)
                    {
                        int x2 = rnd.Next(1, 30);
                        int y2 = rnd.Next(1, 19);
                        if (map.field[y2, x2] is EmptyCell || map.field[y2, x2] is VisitedCell)
                        {
                            player.Move(x2 - player.X, y2 - player.Y, map);
                            break;
                        }
                    }

                }
            }
        }
    }
}
