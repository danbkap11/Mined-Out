using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static List<double> Scores = new List<double>();

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

        private static void annulate()
        {
            Seconds = 0;
            Coins = 0;
            Moves = 0;
            AntiPts.AntiPoints = 0;
            AddPts.Points = 0;
            MultiplePoint.Multiplicator = 1;
            aTimer.Dispose();
            aTimer = new System.Timers.Timer();
            Console.Clear();
        }

        private static void returnToMenu()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.M) Menu();
            }
        }
        public static void GameWon()
        {
            annulate();
            Console.WriteLine("Congratulations!!!");
            Console.WriteLine($"You won with score {ScoreSummary()}");
            Scores.Add(ScoreSummary());
            Victories++;
            Console.WriteLine("Press M to return to the main menu");
            Result = Score();
            returnToMenu();

        }

        public static void GameLost(Player player)
        {
            if(player.Lives == 0 || EscFlag)
            {
                EscFlag = false;
                annulate();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Oops...");
                Console.WriteLine("I regret to say that unfortunately you've lost.");
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Press M to return to the main menu");
                returnToMenu();
            }
        }

        private static int ScoreSummary()
        {
            double score = (Score() + AddPts.Points + AntiPts.AntiPoints) * MultiplePoint.Multiplicator;
            if (score > 0) return (int) Math.Floor(score);
            else return 0;
        }

        private static int Score()
        {
            int score = 1000 - 5 * Moves - 3 * Seconds + 100 * Coins;
            if (score < 0) return 0;
            return score;
        }

        static System.Timers.Timer aTimer = new System.Timers.Timer();

        public static void Sound()
        {
            Console.Beep(400, 100);
            Console.Beep(400, 100);
            Console.Beep(400, 100);
            Console.Beep(400, 100);
        }
        public static void PlayerControl(Field map)
        {
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
                    player.Move(0, -1, map);
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    player.Move(0, 1, map);
                }
                else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    player.Move(1, 0, map);
                }
                else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    player.Move(-1, 0, map);
                }

                if (map.field[player.Y, player.X] is Mine) 
                {
                    Sound();
                    Mine.OnMine(player);
                    GameLost(player);
                }

                if (map.field[player.Y, player.X] is Wall)
                {
                    Sound();
                    Wall.OnWall(player);
                    GameLost(player);
                }

                if (map.field[player.Y, player.X] is WinCell)
                {
                    Sound();
                    GameWon();
                }

                if (map.field[player.Y, player.X] is AddLife)
                {
                    AddLife.OnAddLife(player);
                }

                if (map.field[player.Y, player.X] is AntiLife)
                {
                    AntiLife.OnAntiLife(player);
                    GameLost(player);
                }

                if (map.field[player.Y, player.X] is MultiplePoint)
                {
                    MultiplePoint.SetMultiplicator(1.25);
                }

                if (map.field[player.Y, player.X] is AddPts)
                {
                    AddPts.SetPts(150);
                }

                if (map.field[player.Y, player.X] is AntiPts)
                {
                    AntiPts.SetAntiPts(-250);
                }

                if (map.field[player.Y, player.X] is Trap)
                {
                    Trap.OnTrap(player);
                }

                if (map.field[player.Y, player.X] is Teleport)
                {
                    Teleport.OnTeleport(player, map);
                }
            }
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

            public static void MenuDrawing(int X, int Y, int newY, string oldPointer, string newPointer)
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(oldPointer + "   ");
                Console.SetCursorPosition(X, newY);
                Console.Write(">>" + newPointer + "   ");
            }
            public static void LoadField(string mapName)
            {
                Field field = new Field();
                Console.Clear();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                string projectDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.FullName;
                using (StreamReader file = File.OpenText($@"{projectDirectory}\maps\{mapName}.json"))
                {
                    Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
                    field = (Field)serializer.Deserialize(file, typeof(Field));
                }
                field.PrintField();
                PlayerControl(field);
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
                                    MenuDrawing(3,3,5,"Start new game", "Instructions");
                                    break;
                                case MenuOption.Instructions:
                                    options = MenuOption.Results;
                                    MenuDrawing(3, 5, 7, "Instructions", "Your results");
                                    break;
                                case MenuOption.Results:
                                    options = MenuOption.Exit;
                                    MenuDrawing(3, 7, 9, "Your results", "Exit");
                                    break;
                                case MenuOption.Exit:
                                    options = MenuOption.StartGame;
                                    MenuDrawing(3, 9, 3, "Exit", "Start new game");
                                    break;
                            }

                            break;


                        case ConsoleKey.UpArrow:
                            switch (options)
                            {

                                case MenuOption.StartGame:
                                    options = MenuOption.Exit;
                                    MenuDrawing(3, 3, 9, "Start new game", "Exit");
                                    break;
                                case MenuOption.Instructions:
                                    options = MenuOption.StartGame;
                                    MenuDrawing(3, 5, 3, "Instructions", "Start new game");
                                    break;
                                case MenuOption.Results:
                                    options = MenuOption.Instructions;
                                    MenuDrawing(3, 7, 5, "Your results", "Instructions");
                                    break;
                                case MenuOption.Exit:
                                    options = MenuOption.Results;
                                    MenuDrawing(3, 9, 7, "Exit", "Your results");
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
                                                        MenuDrawing(3, 5, 7, "Easy", "Medium");
                                                        break;
                                                    case MenuOption.Medium:
                                                        options2 = MenuOption.Hard;
                                                        MenuDrawing(3, 7, 9, "Medium", "Hard");
                                                        break;
                                                    case MenuOption.Hard:
                                                        options2 = MenuOption.Easy;
                                                        MenuDrawing(3, 9, 5, "Hard", "Easy");
                                                        break;
                                                }

                                                break;


                                            case ConsoleKey.UpArrow:
                                                switch (options2)
                                                {

                                                    case MenuOption.Easy:
                                                        options2 = MenuOption.Hard;
                                                        MenuDrawing(3, 5, 9, "Easy", "Hard");
                                                        break;
                                                    case MenuOption.Medium:
                                                        options2 = MenuOption.Easy;
                                                        MenuDrawing(3, 7, 5, "Medium", "Easy");
                                                        break;
                                                    case MenuOption.Hard:
                                                        options2 = MenuOption.Medium;
                                                        MenuDrawing(3, 9, 7, "Hard", "Medium");
                                                        break;
                                                }

                                                break;
                                            case ConsoleKey.Enter:
                                            {
                                                switch (options2)
                                                {
                                                    case MenuOption.Easy:
                                                    {
                                                        LoadField("map1");
                                                        break;

                                                    }
                                                    case MenuOption.Medium:
                                                    {
                                                        LoadField("map2");
                                                        break;

                                                    }
                                                    case MenuOption.Hard:
                                                    {
                                                        LoadField("map3");
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
    


