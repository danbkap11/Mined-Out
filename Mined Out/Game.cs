using System;
using System.Collections.Generic;
using System.Timers;

namespace Mined_Out
{
    class Game
    {
        public static int Moves = 0;

        public static int Seconds = 0;

        public static int Coins = 0;

        public static int Result = 0;

        public static int Victories = 0;
        
        public static List<int> Scores = new List<int>()
        {
            
        };
       

        private static void SecondsCounter(object obj, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(44, 6);
            Console.Write(++Game.Seconds);
        }

        private static void GameWon()
        {
            aTimer.Dispose();
            aTimer = new System.Timers.Timer();
            Console.Clear();
            Console.WriteLine("Congratulations!!!");
            Console.WriteLine($"You won with score {Score()}");
            Scores.Add(Score());
            Seconds = 0;
            Coins = 0;
            Moves = 0;
            Victories++;
            Console.WriteLine("Press M to return to the main menu");
            Result = Score();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.M) Menu();
            }
            
        }

        private static void GameLost()
        {
            Seconds = 0;
            Coins = 0;
            Moves = 0;
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


        private static int Score()
        {
            if (1000 - 5 * Moves - 3 * Seconds + 100 * Coins < 0) return 0;
            return 1000 - 5 * Moves - 3 * Seconds + 100 * Coins;
        }

        static System.Timers.Timer aTimer = new System.Timers.Timer();


        public static void PlayerControl()
        {
            Random rnd = new Random();
            aTimer.Elapsed += new ElapsedEventHandler(SecondsCounter);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            Player player = new Player(20, 15);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    player.Move(0, -1);
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    player.Move(0, 1);
                }
                else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    player.Move(1, 0);
                }
                else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    player.Move(-1, 0);
                }

                if (Field.field[player.Y, player.X] is Mine || Field.field[player.Y, player.X] is Wall)
                {
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    GameLost();
                }
                if (Field.field[player.Y, player.X] is WinCell)
                {
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    Console.Beep(400, 100);
                    GameWon();
                    break;
                }
                if (Field.field[player.Y, player.X] is Teleport)
                {
                    while(true)
                    {
                        int x = rnd.Next(1, 30);
                        int y = rnd.Next(1, 19);
                        if (Field.field[y, x] is EmptyCell)
                        {
                            player.Move(x - player.X, y - player.Y);
                            break;
                        }
                        else continue;
                    }
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
                        switch(options)
                        {
                            case MenuOption.StartGame:
                                Console.Clear();
                                Field.InitializeField();
                                PlayerControl();
                                break;
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
                                        Console.WriteLine($"{i+1}.{Scores[i]}");
                                    }
                                }
                                while(true)
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


