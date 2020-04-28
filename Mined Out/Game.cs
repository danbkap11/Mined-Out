using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using Newtonsoft.Json;

namespace Mined_Out
{
    class Game
    {
        public static bool EscFlag = false;

        public static int Moves = 0;

        public static int Seconds = 0;

        public static int Coins = 0;

        public static int Result = 0;

        public static int Victories = 0;

        public static List<double> Scores = new List<double>()
        {

        };

        public static object o = new object();

        private static void SecondsCounter(object obj, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            lock (o)
            {
                Console.SetCursorPosition(44, 6);
                Console.Write(++Game.Seconds + "      ");
                Console.SetCursorPosition(40, 12);
                Console.Write("  "+ $"{ScoreSummary()}" + "  ");
            }
        }

        private static void GameWon()
        {
            aTimer.Dispose();
            aTimer = new System.Timers.Timer();
            Console.Clear();
            Console.WriteLine("Congratulations!!!");
            Console.WriteLine($"You won with score {ScoreSummary()}");
            Scores.Add(ScoreSummary());
            Seconds = 0;
            Coins = 0;
            Moves = 0;
            AntiPts.AntiPoints = 0;
            AddPts.Points = 0;
            MultiplePoint.Multiplicator = 1;
            Victories++;
            Console.WriteLine("Press M to return to the main menu");
            Result = Score();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.M) Menu();
            }

        }

        private static void GameLost(Player player)
        {
            if(player.Lives == 0 || EscFlag)
            { 
            Seconds = 0;
            Coins = 0;
            Moves = 0;
            EscFlag = false;
            AntiPts.AntiPoints = 0;
            AddPts.Points = 0;
            MultiplePoint.Multiplicator = 1;
            aTimer.Dispose();
            aTimer = new System.Timers.Timer();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Oops...");
            Console.WriteLine("I regret to say that unfortunately you've lost.");
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("Press M to return to the main menu");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.M) Menu();
            }
            }
        }

        private static int ScoreSummary()
        {
            double x = (Score() + AddPts.Points + AntiPts.AntiPoints) * MultiplePoint.Multiplicator;
            if (x > 0) return (int) Math.Floor(x);
            else return 0;
        }

        private static int Score()
        {
            if (1000 - 5 * Moves - 3 * Seconds + 100 * Coins < 0) return 0;
            return 1000 - 5 * Moves - 3 * Seconds + 100 * Coins;
        }

        static System.Timers.Timer aTimer = new System.Timers.Timer();


        public static void PlayerControl(Field a)
        {
            Random rnd = new Random();
            aTimer.Elapsed += new ElapsedEventHandler(SecondsCounter);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            Player player = new Player(15, 20, 2);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    EscFlag = true;
                    GameLost(player);
                }

                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    player.Move(0, -1, a);
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    player.Move(0, 1, a);
                }
                else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    player.Move(1, 0, a);
                }
                else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    player.Move(-1, 0, a);
                }

                if (a.field[player.Y, player.X] is Mine || a.field[player.Y, player.X] is Wall)
                {
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    player.Lives--;
                    Console.SetCursorPosition(33, 10);
                    Console.Write($"Lives: {player.Lives}");
                    GameLost(player);
                }

                if (a.field[player.Y, player.X] is WinCell)
                {
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    GameWon();
                    break;
                }

                if (a.field[player.Y, player.X] is AddLife)
                {
                    player.Lives++;
                    Console.SetCursorPosition(33, 10);
                    Console.Write($"Lives: {player.Lives}");
                }

                if (a.field[player.Y, player.X] is AntiLife)
                {
                    player.Lives--;
                    Console.SetCursorPosition(33, 10);
                    Console.Write($"Lives: {player.Lives}");
                    GameLost(player);
                }

                if (a.field[player.Y, player.X] is MultiplePoint)
                {
                    MultiplePoint.Multiplicator = 1.25;
                }

                if (a.field[player.Y, player.X] is AddPts)
                {
                    AddPts.Points = 150;
                }

                if (a.field[player.Y, player.X] is AntiPts)
                {
                    AntiPts.AntiPoints = -250;
                }

                if (a.field[player.Y, player.X] is Trap)
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
                            EscFlag = true;
                            GameLost(player);
                        }
                    }
                }

                if (a.field[player.Y, player.X] is Teleport)
                    {
                        int x = rnd.Next(1, 10);
                        if (x < 7)
                        {
                            if (x % 2 == 1)
                            {
                                var help = Find(a.field, player.Y, player.X);
                                player.Move(help.Item2 - player.X, help.Item1 - player.Y, a);

                            }
                            else
                            {
                                while (true)
                                {
                                    int x2 = rnd.Next(1, 30);
                                    int y2 = rnd.Next(1, 19);
                                    if (a.field[y2, x2] is EmptyCell || a.field[y2, x2] is VisitedCell)
                                    {
                                        player.Move(x2 - player.X, y2 - player.Y, a);
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            public static Tuple<int, int> Find(Cell[,] a, int Y, int X)
            { 
                Tuple<int, int> res = new Tuple<int, int>(0, 0);
                for (int i = 0; i < a.GetLength(0); i++)
                {
                        for (int j = 0; j < a.GetLength(1); j++)
                        {
                            if (a[i, j].GetType().Equals(new Teleport().GetType()) && (i != Y || j != X))
                            {
                                Tuple<int, int> res2 = new Tuple<int, int>(i ,j);
                                res = res2;
                            }
                            else continue;
                        }
                }

                return res;
            }
            public static void Instruct()
            {
                Console.WriteLine("This is 'Mined Out' game.");
                Console.WriteLine("Your goal is to reach a top of the field, collecting some coins.");
                Console.WriteLine("There are mines on the field, which can kill you.");
                Console.WriteLine("Teleport is blue, it can teleport you to any cell except walls and mines");
                Console.WriteLine("Your total score is calculated using this magic formula:");
                Console.WriteLine("1000 - 5 * Moves - 3 * Seconds + 100 * Coins");
                Console.WriteLine("After every move the indicator at the right side from game field shows how");
                Console.WriteLine("many mines are adjacent to player.");
                Console.WriteLine("You can control, using:");
                Console.WriteLine(" - W or UpArrow to move up;");
                Console.WriteLine(" - A or LeftArrow to move left;");
                Console.WriteLine(" - S or DownArrow to move down;");
                Console.WriteLine(" - D or RightArrow to move right;");
                Console.WriteLine("Thank you for playing my first game ever.");
                Console.SetCursorPosition(0, 19);
                Console.WriteLine("Press 'M' to return to the main menu.");
            }



            public static void Menu()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.SetCursorPosition(3, 3);
                Console.Write(">>Start new game");
                Console.SetCursorPosition(3, 5);
                Console.Write("Instructions  ");
                Console.SetCursorPosition(3, 7);
                Console.Write("Your results   ");
                Console.SetCursorPosition(3, 9);
                Console.Write("Exit    ");
                Console.SetCursorPosition(3, 17);
                Console.Write("Use arrows to navigate");
                Console.SetCursorPosition(3, 19);
                Console.Write("Press Enter to select");

                MenuOption options = MenuOption.StartGame;
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {

                        case ConsoleKey.DownArrow:
                            switch (options)
                            {
                                case MenuOption.StartGame:
                                    options = MenuOption.Instructions;
                                    Console.SetCursorPosition(3, 3);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Start new game    ");
                                    Console.SetCursorPosition(3, 5);
                                    Console.Write(">>Instructions");
                                    break;
                                case MenuOption.Instructions:
                                    options = MenuOption.Results;
                                    Console.SetCursorPosition(3, 5);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Instructions    ");
                                    Console.SetCursorPosition(3, 7);
                                    Console.Write(">>Your results");
                                    break;
                                case MenuOption.Results:
                                    options = MenuOption.Exit;
                                    Console.SetCursorPosition(3, 7);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Your results    ");
                                    Console.SetCursorPosition(3, 9);
                                    Console.Write(">>Exit");
                                    break;
                                case MenuOption.Exit:
                                    options = MenuOption.StartGame;
                                    Console.SetCursorPosition(3, 9);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Exit    ");
                                    Console.SetCursorPosition(3, 3);
                                    Console.Write(">>Start new game");
                                    break;
                            }

                            break;


                        case ConsoleKey.UpArrow:
                            switch (options)
                            {

                                case MenuOption.StartGame:
                                    options = MenuOption.Exit;
                                    Console.SetCursorPosition(3, 3);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Start new game     ");
                                    Console.SetCursorPosition(3, 9);
                                    Console.Write(">>Exit");
                                    break;
                                case MenuOption.Instructions:
                                    options = MenuOption.StartGame;
                                    Console.SetCursorPosition(3, 5);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Instructions     ");
                                    Console.SetCursorPosition(3, 3);
                                    Console.Write(">>Start new game");
                                    break;
                                case MenuOption.Results:
                                    options = MenuOption.Instructions;
                                    Console.SetCursorPosition(3, 7);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Your results     ");
                                    Console.SetCursorPosition(3, 5);
                                    Console.Write(">>Instructions");
                                    break;
                                case MenuOption.Exit:
                                    options = MenuOption.Results;
                                    Console.SetCursorPosition(3, 9);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Exit     ");
                                    Console.SetCursorPosition(3, 7);
                                    Console.Write(">>Your results");
                                    break;
                            }

                            break;
                        case ConsoleKey.Enter:
                            switch (options)
                            {
                                case MenuOption.StartGame:
                                    Console.Clear();
                                    Console.SetCursorPosition(3, 3);
                                    Console.Write("Choose difficulty level");
                                    Console.SetCursorPosition(3, 5);
                                    Console.Write(">>Easy   ");
                                    Console.SetCursorPosition(3, 7);
                                    Console.Write("Medium   ");
                                    Console.SetCursorPosition(3, 9);
                                    Console.Write("Hard   ");
                                    MenuOption options2 = MenuOption.Easy;
                                    while (true)
                                    {
                                        ConsoleKeyInfo key2 = Console.ReadKey(true);
                                        switch (key2.Key)
                                        {

                                            case ConsoleKey.DownArrow:
                                                switch (options2)
                                                {
                                                    case MenuOption.Easy:
                                                        options2 = MenuOption.Medium;
                                                        Console.SetCursorPosition(3, 5);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Easy   ");
                                                        Console.SetCursorPosition(3, 7);
                                                        Console.Write(">>Medium   ");
                                                        break;
                                                    case MenuOption.Medium:
                                                        options2 = MenuOption.Hard;
                                                        Console.SetCursorPosition(3, 7);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Medium   ");
                                                        Console.SetCursorPosition(3, 9);
                                                        Console.Write(">>Hard   ");
                                                        break;
                                                    case MenuOption.Hard:
                                                        options2 = MenuOption.Easy;
                                                        Console.SetCursorPosition(3, 9);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Hard   ");
                                                        Console.SetCursorPosition(3, 5);
                                                        Console.Write(">>Easy   ");
                                                        break;
                                                }

                                                break;


                                            case ConsoleKey.UpArrow:
                                                switch (options2)
                                                {

                                                    case MenuOption.Easy:
                                                        options2 = MenuOption.Hard;
                                                        Console.SetCursorPosition(3, 5);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Easy   ");
                                                        Console.SetCursorPosition(3, 9);
                                                        Console.Write(">>Hard   ");
                                                        break;
                                                    case MenuOption.Medium:
                                                        options2 = MenuOption.Easy;
                                                        Console.SetCursorPosition(3, 7);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Medium   ");
                                                        Console.SetCursorPosition(3, 5);
                                                        Console.Write(">>Easy   ");
                                                        break;
                                                    case MenuOption.Hard:
                                                        options2 = MenuOption.Medium;
                                                        Console.SetCursorPosition(3, 9);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write("Hard   ");
                                                        Console.SetCursorPosition(3, 7);
                                                        Console.Write(">>Medium   ");
                                                        break;
                                                }

                                                break;
                                            case ConsoleKey.Enter:
                                            {
                                                switch (options2)
                                                {
                                                    case MenuOption.Easy:
                                                    {
                                                        Field field = new Field();
                                                        Console.Clear();
                                                        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                                                        string projectDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.FullName;
                                                        using (StreamReader file = File.OpenText($@"{projectDirectory}\maps\map1.json"))
                                                        {
                                                            Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
                                                            field = (Field)serializer.Deserialize(file, typeof(Field));
                                                        }
                                                        field.PrintField();
                                                        PlayerControl(field);
                                                        break;

                                                    }
                                                    case MenuOption.Medium:
                                                    {
                                                        Field map2 = new Field();
                                                        Console.Clear();
                                                        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                                                        string projectDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.FullName;
                                                        using (StreamReader file = File.OpenText($@"{projectDirectory}\maps\map2.json"))
                                                        {
                                                            Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
                                                            map2 = (Field)serializer.Deserialize(file, typeof(Field));
                                                        }
                                                        map2.PrintField();
                                                        PlayerControl(map2);
                                                        break;

                                                        }
                                                    case MenuOption.Hard:
                                                    {
                                                        Field map3 = new Field();
                                                        Console.Clear();
                                                        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                                                        string projectDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.FullName;
                                                        using (StreamReader file = File.OpenText($@"{projectDirectory}\maps\map3.json"))
                                                        {
                                                            Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
                                                            map3 = (Field)serializer.Deserialize(file, typeof(Field));
                                                        }
                                                        map3.PrintField();
                                                        PlayerControl(map3);
                                                        break;
                                                        }
                                                }

                                                break;
                                            }
                                        }
                                    }


                                case MenuOption.Instructions:
                                    Console.Clear();
                                    Instruct();
                                    while (true)
                                    {
                                        ConsoleKeyInfo instructKey = Console.ReadKey(true);
                                        if (instructKey.Key == ConsoleKey.M)
                                        {
                                            Console.Clear();
                                            Menu();
                                        }
                                    }


                                case MenuOption.Results:
                                    Console.Clear();
                                    Console.WriteLine("Press M to return to main menu.");
                                    Console.WriteLine("Your results for session:");
                                    if (Victories > 0)
                                    {
                                        for (int i = 0; i < Scores.Count; i++)
                                        {
                                            Console.WriteLine($"{i + 1}.{Scores[i]}");
                                        }
                                    }

                                    while (true)
                                    {
                                        ConsoleKeyInfo resultsKey = Console.ReadKey(true);
                                        if (resultsKey.Key == ConsoleKey.M)
                                        {
                                            Console.Clear();
                                            Menu();
                                        }
                                    }

                                case MenuOption.Exit:
                                    Environment.Exit(-1);
                                    break;
                            }

                            break;

                    }
                }
            }
            }
        }
    


